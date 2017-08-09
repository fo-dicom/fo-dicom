// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Linq;

using Dicom.Imaging.Codec;
using Dicom.Imaging.Render;

namespace Dicom.Imaging
{
    /// <summary>
    /// DICOM Image class for image rendering.
    /// </summary>
    public class DicomImage
    {
        #region Private Members

        private double _scale;

        private readonly DicomDataset _dataset;

        private readonly DicomPixelData _dcmPixelData;

        private DicomOverlayData[] _overlays;

        private IPixelData _pixelData;

        private IPipeline _pipeline;

        private GrayscaleRenderOptions _renderOptions;

        private readonly object _lock = new object();

        private readonly IDictionary<int, int> _encapsulatedFrameIndex;

        #endregion

        #region CONSTRUCTORS

        /// <summary>Creates DICOM image object from dataset</summary>
        /// <param name="dataset">Source dataset</param>
        /// <param name="frame">Zero indexed frame number. If <paramref name="frame"/> is set to a negative number, the
        /// <see cref="DicomImage"/> object will remain in a partly initialized state, allowing for <see cref="WindowCenter"/>,
        /// <see cref="WindowWidth"/> and <see cref="GrayscaleColorMap"/> to be configured prior to rendering the image frames.</param>
        public DicomImage(DicomDataset dataset, int frame = 0)
        {
            ShowOverlays = true;

            _scale = 1.0;
            _encapsulatedFrameIndex = new Dictionary<int, int>();

            _dataset = DicomTranscoder.ExtractOverlays(dataset);
            _dcmPixelData = CreateDicomPixelData(_dataset);
            Load(frame);
        }

        /// <summary>Creates DICOM image object from file</summary>
        /// <param name="fileName">Source file</param>
        /// <param name="frame">Zero indexed frame number</param>
        public DicomImage(string fileName, int frame = 0)
            : this(DicomFile.Open(fileName).Dataset, frame)
        {
        }

        #endregion

        #region PROPERTIES

        /// <summary>Width of image in pixels</summary>
        public int Width => _dcmPixelData.Width;

        /// <summary>Height of image in pixels</summary>
        public int Height => _dcmPixelData.Height;

        /// <summary>Scaling factor of the rendered image</summary>
        public double Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                _pixelData = null;
            }
        }

        /// <summary>Number of frames contained in image data.</summary>
        public int NumberOfFrames => _dcmPixelData.NumberOfFrames;

        /// <summary>Gets or sets window width of rendered gray scale image.</summary>
        public virtual double WindowWidth
        {
            get
            {
                if (_pipeline == null)
                {
                    _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
                }

                return _renderOptions?.WindowWidth ?? 255;
            }
            set
            {
                if (_pipeline == null)
                {
                    _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
                }

                if (_renderOptions != null)
                {
                    _renderOptions.WindowWidth = value;
                }
            }
        }

        /// <summary>Gets or sets window center of rendered gray scale image.</summary>
        public virtual double WindowCenter
        {
            get
            {
                if (_pipeline == null)
                {
                    _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
                }

                return _renderOptions?.WindowCenter ?? 127;
            }
            set
            {
                if (_pipeline == null)
                {
                    _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
                }

                if (_renderOptions != null)
                {
                    _renderOptions.WindowCenter = value;
                }
            }
        }

        /// <summary>Gets or sets the color map to be applied when rendering grayscale images.</summary>
        public virtual Color32[] GrayscaleColorMap
        {
            get
            {
                if (_pipeline == null)
                {
                    _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
                }

                return _renderOptions?.ColorMap;
            }
            set
            {
                if (_pipeline == null)
                {
                    _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
                }

                if (_renderOptions != null)
                {
                    _renderOptions.ColorMap = value;
                }
                else
                {
                    throw new DicomImagingException(
                        "Grayscale color map not applicable for photometric interpretation: {0}",
                        _dcmPixelData.PhotometricInterpretation);
                }
            }
        }

        /// <summary>Gets or sets whether the image is gray scale.</summary>
        public virtual bool IsGrayscale
        {
            get
            {
                if (_pipeline == null)
                {
                    _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
                }

                return _renderOptions != null;
            }
        }

        /// <summary>Show or hide DICOM overlays</summary>
        public bool ShowOverlays { get; set; }

        /// <summary>Color used for displaying DICOM overlays. Default is magenta.</summary>
        public int OverlayColor { get; set; } = unchecked((int)0xffff00ff);

        public int CurrentFrame { get; private set; }

        #endregion

        #region METHODS

        /// <summary>Renders DICOM image to <see cref="IImage"/>.</summary>
        /// <param name="frame">Zero indexed frame number.</param>
        /// <returns>Rendered image</returns>
        public virtual IImage RenderImage(int frame = 0)
        {
            if (frame != CurrentFrame || _pixelData == null) Load(frame);

            var graphic = new ImageGraphic(_pixelData);

            if (ShowOverlays)
            {
                if (_overlays == null)
                {
                    _overlays = CreateOverlays(_dataset);
                }

                foreach (var overlay in _overlays)
                {
                    if (frame + 1 < overlay.OriginFrame
                        || frame + 1 > overlay.OriginFrame + overlay.NumberOfFrames - 1) continue;

                    var og = new OverlayGraphic(
                        PixelDataFactory.Create(overlay),
                        overlay.OriginX - 1,
                        overlay.OriginY - 1,
                        OverlayColor);
                    graphic.AddOverlay(og);
                    og.Scale(_scale);
                }
            }

            return graphic.RenderImage(_pipeline.LUT);
        }

        /// <summary>
        /// Loads the pixel data for specified frame and set the internal dataset
        /// </summary>
        /// <param name="frame">The frame number to create pixeldata for</param>
        private void Load(int frame)
        {
            if (frame < 0)
            {
                CurrentFrame = frame;
                return;
            }

            int index;
            if (_dataset.InternalTransferSyntax.IsEncapsulated)
            {
                if (!_encapsulatedFrameIndex.TryGetValue(frame, out index))
                {
                    // decompress single frame from source dataset
                    var transcoder = new DicomTranscoder(
                        _dataset.InternalTransferSyntax,
                        DicomTransferSyntax.ExplicitVRLittleEndian);
                    var buffer = transcoder.DecodeFrame(_dataset, frame);

                    // Get frame/index mapping for previously unstored frame.
                    index = _dcmPixelData.NumberOfFrames;
                    _encapsulatedFrameIndex[frame] = index;

                    _dcmPixelData.AddFrame(buffer);
                }
            }
            else
            {
                index = frame;
            }

            _pixelData = PixelDataFactory.Create(_dcmPixelData, index).Rescale(_scale);

            CurrentFrame = frame;

            if (_pipeline == null)
            {
                _pipeline = CreatePipeline(_dataset, _dcmPixelData, ref _renderOptions);
            }
        }

        private static DicomPixelData CreateDicomPixelData(DicomDataset dataset)
        {
            var inputTransferSyntax = dataset.InternalTransferSyntax;
            if (!inputTransferSyntax.IsEncapsulated) return DicomPixelData.Create(dataset);

            // Clone the encapsulated dataset because modifying the pixel data modifies the dataset
            var clone = dataset.Clone();
            clone.InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;

            var pixelData = DicomPixelData.Create(clone, true);

            // temporary fix for JPEG compressed YBR images, according to enforcement above
            if ((inputTransferSyntax == DicomTransferSyntax.JPEGProcess1
                 || inputTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && pixelData.SamplesPerPixel == 3)
            {
                // When converting to RGB in Dicom.Imaging.Codec.Jpeg.i, PlanarConfiguration is set to Interleaved
                pixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
                pixelData.PlanarConfiguration = PlanarConfiguration.Interleaved;
            }

            // temporary fix for JPEG 2000 Lossy images
            if ((inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossy
                 && pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrIct)
                || (inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossless
                    && pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrRct))
            {
                // Converted to RGB in Dicom.Imaging.Codec.Jpeg2000.cpp
                pixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
            }

            // temporary fix for JPEG2000 compressed YBR images
            if ((inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossless
                 || inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossy)
                && (pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull
                    || pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422
                    || pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422))
            {
                // For JPEG2000 YBR type images in Dicom.Imaging.Codec.Jpeg2000.cpp, 
                // YBR_FULL is applied and PlanarConfiguration is set to Planar
                pixelData.PhotometricInterpretation = PhotometricInterpretation.YbrFull;
                pixelData.PlanarConfiguration = PlanarConfiguration.Planar;
            }

            return pixelData;
        }

        private static DicomOverlayData[] CreateOverlays(DicomDataset dataset)
        {
            return DicomOverlayData.FromDataset(dataset)
                .Where(x => x.Type == DicomOverlayType.Graphics && x.Data != null)
                .ToArray();
        }

        /// <summary>
        /// Create image rendering pipeline according to the <see cref="DicomPixelData.PhotometricInterpretation">photometric interpretation</see>
        /// of the pixel data.
        /// </summary>
        private static IPipeline CreatePipeline(DicomDataset dataset, DicomPixelData pixelData, ref GrayscaleRenderOptions renderOptions)
        {
            var pi = pixelData.PhotometricInterpretation;
            var samples = dataset.Get<ushort>(DicomTag.SamplesPerPixel, 0, 0);

            // temporary fix for JPEG compressed YBR images
            if ((dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess1
                 || dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && samples == 3) pi = PhotometricInterpretation.Rgb;

            // temporary fix for JPEG 2000 Lossy images
            if (pi == PhotometricInterpretation.YbrIct || pi == PhotometricInterpretation.YbrRct) pi = PhotometricInterpretation.Rgb;

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

            if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2)
            {
                //Monochrome1 or Monochrome2 for grayscale image
                if (renderOptions == null) renderOptions = GrayscaleRenderOptions.FromDataset(dataset);
                return new GenericGrayscalePipeline(renderOptions);
            }
            if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull
                || pi == PhotometricInterpretation.YbrFull422 || pi == PhotometricInterpretation.YbrPartial422)
            {
                //RGB for color image
                return new RgbColorPipeline();
            }
            if (pi == PhotometricInterpretation.PaletteColor)
            {
                //PALETTE COLOR for Palette image
                return new PaletteColorPipeline(pixelData);
            }

            throw new DicomImagingException("Unsupported pipeline photometric interpretation: {0}", pi.Value);
        }

        #endregion
    }
}
