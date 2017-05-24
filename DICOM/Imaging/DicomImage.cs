// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.Linq;

    using Dicom.Imaging.Codec;
    using Dicom.Imaging.Render;

    /// <summary>
    /// DICOM Image class for image rendering.
    /// </summary>
    public class DicomImage
    {
        #region Private Members

        private IPixelData _pixelData;

        private IPipeline _pipeline;

        private double _scale;

        private GrayscaleRenderOptions _renderOptions;

        private DicomOverlayData[] _overlays;

        private int _overlayColor = unchecked((int)0xffff00ff);

        #endregion

        /// <summary>Creates DICOM image object from dataset</summary>
        /// <param name="dataset">Source dataset</param>
        /// <param name="frame">Zero indexed frame number</param>
        public DicomImage(DicomDataset dataset, int frame = 0)
        {
            _scale = 1.0;
            ShowOverlays = true;
            Load(dataset, frame);
        }

        /// <summary>Creates DICOM image object from file</summary>
        /// <param name="fileName">Source file</param>
        /// <param name="frame">Zero indexed frame number</param>
        public DicomImage(string fileName, int frame = 0)
        {
            _scale = 1.0;
            ShowOverlays = true;
            var file = DicomFile.Open(fileName);
            Load(file.Dataset, frame);
        }

        /// <summary>Source DICOM dataset</summary>
        public DicomDataset Dataset { get; private set; }

        /// <summary>DICOM pixel data</summary>
        public DicomPixelData PixelData { get; private set; }

        /// <summary>Width of image in pixels</summary>
        public int Width
        {
            get
            {
                return PixelData.Width;
            }
        }

        /// <summary>Height of image in pixels</summary>
        public int Height
        {
            get
            {
                return PixelData.Height;
            }
        }

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

        /// <summary>Photometric interpretation of pixel data.</summary>
        public PhotometricInterpretation PhotometricInterpretation { get; private set; }

        /// <summary>Number of frames contained in image data.</summary>
        public int NumberOfFrames
        {
            get
            {
                return PixelData.NumberOfFrames;
            }
        }

        /// <summary>Window width of rendered gray scale image </summary>
        public virtual double WindowWidth
        {
            get
            {
                if (_pipeline == null)
                {
                    CreatePipeline();
                }
                return _pipeline is GenericGrayscalePipeline ? _renderOptions.WindowWidth : 255;
            }
            set
            {
                if (_pipeline == null)
                {
                    CreatePipeline();
                }
                if (_pipeline is GenericGrayscalePipeline)
                {
                    _renderOptions.WindowWidth = value;
                }
            }
        }

        /// <summary>Window center of rendered gray scale image </summary>
        public virtual double WindowCenter
        {
            get
            {
                if (_pipeline == null)
                {
                    CreatePipeline();
                }
                return _pipeline is GenericGrayscalePipeline ? _renderOptions.WindowCenter : 127;
            }
            set
            {
                if (_pipeline == null)
                {
                    CreatePipeline();
                }
                if (_pipeline is GenericGrayscalePipeline)
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
                    CreatePipeline();
                }
                return _pipeline is GenericGrayscalePipeline ? _renderOptions.ColorMap : null;
            }
            set
            {
                if (_pipeline == null)
                {
                    CreatePipeline();
                }
                if (_pipeline is GenericGrayscalePipeline)
                {
                    _renderOptions.ColorMap = value;
                }
                else
                {
                    throw new DicomImagingException(
                        "Grayscale color map not applicable for photometric interpretation: {0}",
                        this.Dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation));
                }
            }
        }

        /// <summary>Show or hide DICOM overlays</summary>
        public bool ShowOverlays { get; set; }

        /// <summary>Color used for displaying DICOM overlays. Default is magenta.</summary>
        public int OverlayColor
        {
            get
            {
                return _overlayColor;
            }
            set
            {
                _overlayColor = value;
            }
        }


        public int CurrentFrame { get; private set; }

        /// <summary>Renders DICOM image to <see cref="IImage"/>.</summary>
        /// <param name="frame">Zero indexed frame number</param>
        /// <returns>Rendered image</returns>
        public virtual IImage RenderImage(int frame = 0)
        {
            if (frame != CurrentFrame || _pixelData == null) Load(Dataset, frame);

            var graphic = new ImageGraphic(_pixelData);

            if (ShowOverlays)
            {
                foreach (var overlay in _overlays)
                {
                    if ((frame + 1) < overlay.OriginFrame
                        || (frame + 1) > (overlay.OriginFrame + overlay.NumberOfFrames - 1)) continue;

                    var og = new OverlayGraphic(
                        PixelDataFactory.Create(overlay),
                        overlay.OriginX - 1,
                        overlay.OriginY - 1,
                        OverlayColor);
                    graphic.AddOverlay(og);
                    og.Scale(this._scale);
                }
            }

            return graphic.RenderImage(this._pipeline.LUT);
        }

        /// <summary>
        /// Loads the pixel data for specified frame and set the internal dataset
        /// 
        /// </summary>
        /// <param name="dataset">dataset to load pixeldata from</param>
        /// <param name="frame">The frame number to create pixeldata for</param>
        private void Load(DicomDataset dataset, int frame)
        {
            Dataset = DicomTranscoder.ExtractOverlays(dataset);

            if (PixelData == null)
            {
                PixelData = DicomPixelData.Create(Dataset);
                PhotometricInterpretation = PixelData.PhotometricInterpretation;
            }
            if (frame < 0)
            {
                CurrentFrame = frame;
                return;
            }

            if (Dataset.InternalTransferSyntax.IsEncapsulated)
            {
                // decompress single frame from source dataset
                var transcoder = new DicomTranscoder(
                                     this.Dataset.InternalTransferSyntax,
                                     DicomTransferSyntax.ExplicitVRLittleEndian);
                var buffer = transcoder.DecodeFrame(Dataset, frame);

                // clone the dataset because modifying the pixel data modifies the dataset
                var clone = Dataset.Clone();
                clone.InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;

                var pixelData = DicomPixelData.Create(clone, true);
                TrimDecodedPixelDataProperties(pixelData, Dataset.InternalTransferSyntax);
                pixelData.AddFrame(buffer);

                _pixelData = PixelDataFactory.Create(pixelData, 0);
            }
            else
            {
                // pull uncompressed frame from source pixel data
                _pixelData = PixelDataFactory.Create(PixelData, frame);
            }

            _pixelData = _pixelData.Rescale(_scale);

            _overlays =
                DicomOverlayData.FromDataset(Dataset)
                    .Where(x => x.Type == DicomOverlayType.Graphics && x.Data != null)
                    .ToArray();

            CurrentFrame = frame;

            if (_pipeline == null)
            {
                CreatePipeline();
            }
        }

        private static void TrimDecodedPixelDataProperties(
            DicomPixelData decodedPixelData,
            DicomTransferSyntax inputTransferSyntax)
        {
            if (!inputTransferSyntax.IsEncapsulated) return;

            // temporary fix for JPEG compressed YBR images, according to enforcement above
            if ((inputTransferSyntax == DicomTransferSyntax.JPEGProcess1
                 || inputTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && decodedPixelData.SamplesPerPixel == 3)
            {
                // When converting to RGB in Dicom.Imaging.Codec.Jpeg.i, PlanarConfiguration is set to Interleaved
                decodedPixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
                decodedPixelData.PlanarConfiguration = PlanarConfiguration.Interleaved;
            }

            // temporary fix for JPEG 2000 Lossy images
            if ((inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossy
                 && decodedPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrIct)
                || (inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossless
                    && decodedPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrRct))
            {
                // Converted to RGB in Dicom.Imaging.Codec.Jpeg2000.cpp
                decodedPixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
            }

            // temporary fix for JPEG2000 compressed YBR images
            if ((inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossless
                 || inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossy)
                && (decodedPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull
                    || decodedPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422
                    || decodedPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422))
            {
                // For JPEG2000 YBR type images in Dicom.Imaging.Codec.Jpeg2000.cpp, 
                // YBR_FULL is applied and PlanarConfiguration is set to Planar
                decodedPixelData.PhotometricInterpretation = PhotometricInterpretation.YbrFull;
                decodedPixelData.PlanarConfiguration = PlanarConfiguration.Planar;
            }
        }

        /// <summary>
        /// Create image rendering pipeline according to the Dataset <see cref="PhotometricInterpretation"/>.
        /// </summary>
        private void CreatePipeline()
        {
            var pi = Dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation);
            var samples = Dataset.Get<ushort>(DicomTag.SamplesPerPixel, 0, 0);

            // temporary fix for JPEG compressed YBR images
            if ((Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess1
                 || Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && samples == 3) pi = PhotometricInterpretation.Rgb;

            // temporary fix for JPEG 2000 Lossy images
            if (pi == PhotometricInterpretation.YbrIct || pi == PhotometricInterpretation.YbrRct) pi = PhotometricInterpretation.Rgb;

            if (pi == null)
            {
                // generally ACR-NEMA
                if (samples == 0 || samples == 1)
                {
                    if (Dataset.Contains(DicomTag.RedPaletteColorLookupTableData)) pi = PhotometricInterpretation.PaletteColor;
                    else pi = PhotometricInterpretation.Monochrome2;
                }
                else
                {
                    // assume, probably incorrectly, that the image is RGB
                    pi = PhotometricInterpretation.Rgb;
                }
            }

            if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2)
            {
                //Monochrom1 or Monochrome2 for grayscale image
                if (_renderOptions == null) _renderOptions = GrayscaleRenderOptions.FromDataset(Dataset);
                _pipeline = new GenericGrayscalePipeline(_renderOptions);
            }
            else if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull
                     || pi == PhotometricInterpretation.YbrFull422 || pi == PhotometricInterpretation.YbrPartial422)
            {
                //RGB for color image
                _pipeline = new RgbColorPipeline();
            }
            else if (pi == PhotometricInterpretation.PaletteColor)
            {
                //PALETTE COLOR for Palette image
                _pipeline = new PaletteColorPipeline(PixelData);
            }
            else
            {
                throw new DicomImagingException("Unsupported pipeline photometric interpretation: {0}", pi.Value);
            }
        }
    }
}
