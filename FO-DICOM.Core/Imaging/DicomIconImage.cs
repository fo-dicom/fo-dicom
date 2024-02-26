// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// DICOM Image class for image rendering.
    /// </summary>
    public class DicomIconImage
    {
        /// <summary>
        /// Try to extract icon from dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="dicomIconImage">The Icon, ready to be rendered</param>
        /// <returns></returns>
        public static bool TryCreate(DicomDataset dataset, out DicomIconImage dicomIconImage)
        {
            var success = dataset.TryGetSequence(DicomTag.IconImageSequence, out var iconImageSequence);
            if (success && iconImageSequence.Items.Count == 1)
            {
                try
                {
                    dicomIconImage = new DicomIconImage(iconImageSequence.Items[0]);
                    return true;
                }
                catch
                { 
                    // Failed to create Icon from dataset
                }
            }

            dicomIconImage = null;
            return false;
        }

        #region FIELDS

        private readonly object _lock = new object();

        private readonly DicomDataset _dataset;

        private readonly DicomPixelData _pixelData;

        private IPipeline _pipeline;

        #endregion

        #region CONSTRUCTORS

        /// <summary>Creates an Icon image from the Icon Image Sequence item supplied</summary>
        /// <param name="iconImageSequenceItem">The icon image sequence item to create an image from</param>
        public DicomIconImage(DicomDataset iconImageSequenceItem)
        {
            // Clone the encapsulated dataset because modifying the pixel data modifies the dataset
            var clone = iconImageSequenceItem.Clone();

            _dataset = clone;
            _pixelData = CreateDicomPixelData(clone);
        }

        #endregion

        #region PROPERTIES

        /// <summary>Width of icon in pixels</summary>
        public int Width => _pixelData.Width;

        /// <summary>Height of icon in pixels</summary>
        public int Height => _pixelData.Height;

        #endregion

        #region METHODS

        /// <summary>Renders DICOM icon image to <see cref="IImage"/>.</summary>
        /// <returns>Rendered image</returns>
        public virtual IImage RenderImage()
        {
            EstablishPipeline();
            IPixelData pixels = PixelDataFactory.Create(_pixelData, 0);

            IImage image;
            var graphic = new ImageGraphic(pixels);

            image = graphic.RenderImage(_pipeline.LUT);
            return image;
        }

        private void EstablishPipeline()
        {
            bool create;
            lock (_lock)
            {
                create = _pipeline == null;
            }

            var tuple = create ? CreatePipelineData(_dataset, _pixelData) : null;

            lock (_lock)
            {
                if (_pipeline == null)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    _pipeline = tuple.Pipeline;
                }
            }
        }

        /// <summary>
        /// Create pixel data object based on <paramref name="dataset"/>.
        /// </summary>
        /// <param name="dataset">Dataset containing pixel data.</param>
        /// <returns>For non-encapsulated dataset, create pixel data object from original pixel data. For encapsulated dataset,
        /// create "empty" pixel data object to subsequentially be filled with uncompressed data for each frame.</returns>
        private static DicomPixelData CreateDicomPixelData(DicomDataset dataset)
        {
            // Validate the dataset
            var photometricInterpretation = PhotometricInterpretation.Parse(
                dataset.GetSingleValue<string>(DicomTag.PhotometricInterpretation));

            var isValid = (photometricInterpretation == PhotometricInterpretation.PaletteColor
                || photometricInterpretation == PhotometricInterpretation.Monochrome1
                || photometricInterpretation == PhotometricInterpretation.Monochrome2);

            if (!isValid)
                throw new ArgumentException("Photometric Interpretation for Icon Image Sequence must be either one of MONOCHROME 1, MONOCHROME 2 or PALETTE COLOR");

            if (dataset.InternalTransferSyntax.IsEncapsulated)
            {
                /* From PS3.5, A.4 Transfer Syntaxes For Encapsulation of Encoded Pixel Data (version 2022d):
                 * 
                 * "The distinction between fixed value length (native) and undefined value length (encapsulated) 
                 * is present so that the top level Data Set Pixel Data can be compressed (and hence encapsulated), 
                 * but the Pixel Data within an Icon Image Sequence may or may not be compressed."
                 */

                var pd = dataset.GetDicomItem<DicomElement>(DicomTag.PixelData);
                if (pd.Length != uint.MaxValue)
                {
                    // The pixel data is in native format here
                    // Since the outer dataset is encapsulated, this must be Explicit VR Little Endian
                    dataset.InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
                }
                else
                {
                    // This pixel data follows the parent encoding
                }
            }

            var pixelData = DicomPixelData.Create(dataset, false);

            // Number of frames is not part of the Image Icon Sequence
            // Set to 1 to be able to use the standard rendering functions
            pixelData.NumberOfFrames = 1;

            return pixelData;
        }

        /// <summary>
        /// Create image rendering pipeline according to the <see cref="DicomPixelData.PhotometricInterpretation">photometric interpretation</see>
        /// of the pixel data.
        /// </summary>
        private static PipelineData CreatePipelineData(DicomDataset dataset, DicomPixelData pixelData)
        {
            var pi = pixelData.PhotometricInterpretation;
            var samples = dataset.GetSingleValueOrDefault(DicomTag.SamplesPerPixel, (ushort)0);

            // temporary fix for JPEG compressed YBR images
            if ((dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess1
             || dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && samples == 3)
            {
                pi = PhotometricInterpretation.Rgb;
            }

            // temporary fix for JPEG 2000 Lossy images
            if (pi == PhotometricInterpretation.YbrIct || pi == PhotometricInterpretation.YbrRct)
            {
                pi = PhotometricInterpretation.Rgb;
            }

            if (pi == null)
            {
                // generally ACR-NEMA
                if (samples == 0 || samples == 1)
                {
                    pi = dataset.Contains(DicomTag.RedPaletteColorLookupTableData)
                        ? PhotometricInterpretation.PaletteColor
                        : PhotometricInterpretation.Monochrome2;
                }
                else
                {
                    // assume, probably incorrectly, that the image is RGB
                    pi = PhotometricInterpretation.Rgb;
                }
            }

            IPipeline pipeline;
            GrayscaleRenderOptions renderOptions = null;
            if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2)
            {
                //Monochrome1 or Monochrome2 for grayscale image
                renderOptions = GrayscaleRenderOptions.FromDataset(dataset, 0);
                pipeline = new GenericGrayscalePipeline(renderOptions);
            }
            else if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull
                || pi == PhotometricInterpretation.YbrFull422 || pi == PhotometricInterpretation.YbrPartial422
                || pi == PhotometricInterpretation.YbrIct || pi == PhotometricInterpretation.YbrRct)
            {
                //RGB for color image
                pipeline = new RgbColorPipeline();
            }
            else if (pi == PhotometricInterpretation.PaletteColor)
            {
                //PALETTE COLOR for Palette image
                pipeline = new PaletteColorPipeline(pixelData);
            }
            else
            {
                throw new DicomImagingException($"Unsupported pipeline photometric interpretation: {pi}");
            }

            return new PipelineData { Pipeline = pipeline, RenderOptions = renderOptions };
        }

        #endregion

        #region INNER TYPES

        private class PipelineData
        {
            #region PROPERTIES

            internal IPipeline Pipeline { get; set; }

            internal GrayscaleRenderOptions RenderOptions { get; set; }

            #endregion
        }

        #endregion
    }
}