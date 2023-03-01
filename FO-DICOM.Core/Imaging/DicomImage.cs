// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO.Buffer;
using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// DICOM Image class for image rendering.
    /// </summary>
    public class DicomImage
    {
        #region FIELDS

        private readonly object _lock = new object();

        private double _scale;

        private bool _rerender;

        // the source of data
        private readonly DicomDataset _dataset;

        private DicomOverlayData[] _overlays;

        private readonly DicomPixelData _pixelDataCache;

        // a cache of pixels, This cached data will be takten as long as _rerender is false or the CurrrentFrame does not change
        private readonly Dictionary<int, IPixelData> _pixelsCache = new Dictionary<int, IPixelData>();

        private IPipeline _pipeline;

        private GrayscaleRenderOptions _renderOptions;

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
            _rerender = true;

            _dataset = DicomTranscoder.ExtractOverlays(dataset);
            _pixelDataCache = CreateDicomPixelData(_dataset);
            CurrentFrame = frame;
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

        public CacheType CacheMode { get; set; } = CacheType.None;

        /// <summary>Width of image in pixels</summary>
        public int Width => _dataset.GetSingleValue<ushort>(DicomTag.Columns);

        /// <summary>Height of image in pixels</summary>
        public int Height => _dataset.GetSingleValue<ushort>(DicomTag.Rows);

        /// <summary>Scaling factor of the rendered image</summary>
        public double Scale
        {
            get => _scale;
            set
            {
                lock (_lock)
                {
                    _scale = value;
                    _rerender = true;
                }
            }
        }

        /// <summary>Number of frames contained in image data.</summary>
        public int NumberOfFrames => _dataset.GetSingleValueOrDefault(DicomTag.NumberOfFrames, (ushort)1);

        /// <summary>Gets or sets window width of rendered gray scale image.</summary>
        public virtual double WindowWidth
        {
            get
            {
                EstablishPipeline(CurrentFrame);
                return _renderOptions?.WindowWidth ?? 255;
            }
            set
            {
                EstablishPipeline(CurrentFrame);

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
                EstablishPipeline(CurrentFrame);
                return _renderOptions?.WindowCenter ?? 127;
            }
            set
            {
                EstablishPipeline(CurrentFrame);

                if (_renderOptions != null)
                {
                    _renderOptions.WindowCenter = value;
                }
            }
        }

        /// <summary>Gets or sets whether to use VOI LUT.</summary>
        public virtual bool UseVOILUT
        {
            get
            {
                EstablishPipeline(CurrentFrame);
                return _renderOptions?.UseVOILUT ?? false;
            }
            set
            {
                EstablishPipeline(CurrentFrame);

                if (_renderOptions != null)
                {
                    _renderOptions.UseVOILUT = value;
                    RecreatePipeline(_renderOptions);
                }
            }
        }

        /// <summary>Gets or sets the color map to be applied when rendering grayscale images.</summary>
        public virtual Color32[] GrayscaleColorMap
        {
            get
            {
                EstablishPipeline(CurrentFrame);
                return _renderOptions?.ColorMap;
            }
            set
            {
                EstablishPipeline(CurrentFrame);

                if (_renderOptions != null)
                {
                    _renderOptions.ColorMap = value;
                }
                else
                {
                    throw new DicomImagingException($"Grayscale color map not applicable for photometric interpretation: {_pixelDataCache.PhotometricInterpretation}");
                }
            }
        }

        /// <summary>Gets or sets whether the image is gray scale.</summary>
        public virtual bool IsGrayscale
        {
            get
            {
                EstablishPipeline(CurrentFrame);
                return _renderOptions != null;
            }
        }

        /// <summary>Show or hide DICOM overlays</summary>
        public bool ShowOverlays { get; set; }

        /// <summary>Gets or sets the color used for displaying DICOM overlays. Default is magenta.</summary>
        public int OverlayColor { get; set; } = unchecked((int)0xffff00ff);

        /// <summary>
        /// Gets the index of the current frame.
        /// </summary>
        public int CurrentFrame { get; private set; }

        #endregion

        #region METHODS

        /// <summary>Renders DICOM image to <see cref="IImage"/>.</summary>
        /// <param name="frame">Zero indexed frame number.</param>
        /// <returns>Rendered image</returns>
        public virtual IImage RenderImage(int frame = 0)
        {
            IPixelData pixels;
            lock (_lock)
            {
                var load = frame >= 0 && (_rerender || !_pixelsCache.ContainsKey(frame));
                CurrentFrame = frame;
                _rerender = false;

                if (load)
                {
                    // trigger recreating the pipeline
                    _pipeline = null;

                    pixels = GetFrameData(frame).Rescale(_scale);
                    _pixelsCache.Add(frame, pixels);
                }
                else
                {
                    pixels = _pixelsCache[frame];
                }
            }

            if (ShowOverlays)
            {
                EstablishGraphicsOverlays();
            }

            IImage image;
            var graphic = new ImageGraphic(pixels);

            if (ShowOverlays)
            {
                foreach (var overlay in _overlays)
                {
                    if (overlay.Data is EmptyBuffer) // fixed overlay.data is null, exception thrown
                    {
                        continue;
                    }

                    if (frame + 1 < overlay.OriginFrame
                        || frame + 1 > overlay.OriginFrame + overlay.NumberOfFrames - 1)
                    {
                        continue;
                    }

                    var og = new OverlayGraphic(
                        PixelDataFactory.Create(overlay),
                        overlay.OriginX - 1,
                        overlay.OriginY - 1,
                        OverlayColor);
                    graphic.AddOverlay(og);
                    og.Scale(_scale);
                }
            }

            image = graphic.RenderImage(_pipeline.LUT);

            return image;
        }


        /// <summary>
        /// If necessary, prepare new frame data, and return appropriate data.
        /// </summary>
        /// <param name="frame">The frame number to create pixeldata for.</param>
        /// <returns>Data the frame</returns>
        private IPixelData GetFrameData(int frame)
        {
            EstablishPipeline(frame);

            if (_dataset.InternalTransferSyntax.IsEncapsulated)
            {
                // decompress single frame from source dataset
                var transcoder = new DicomTranscoder(
                    _dataset.InternalTransferSyntax,
                    DicomTransferSyntax.ExplicitVRLittleEndian);
                var pixels = transcoder.DecodePixelData(_dataset, frame);

                return pixels;
            }
            else
            {
                return PixelDataFactory.Create(_pixelDataCache, frame);
            }
        }

        private void EstablishPipeline(int frame)
        {
            bool create;
            lock (_lock)
            {
                create = _pipeline == null;
            }

            (IPipeline pipeline, GrayscaleRenderOptions renderOptions) = create ? CreatePipelineData(_dataset, _pixelDataCache, frame) : (null, null);

            lock (_lock)
            {
                if (_pipeline == null)
                {
                    _pipeline = pipeline;
                    _renderOptions = renderOptions;
                }
            }
        }

        private void EstablishGraphicsOverlays()
        {
            bool create;
            lock (_lock)
            {
                create = _overlays == null;
            }

            var overlays = create ? CreateGraphicsOverlays(_dataset) : null;

            lock (_lock)
            {
                if (_overlays == null)
                {
                    _overlays = overlays;
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
            var inputTransferSyntax = dataset.InternalTransferSyntax;
            if (!inputTransferSyntax.IsEncapsulated)
            {
                return DicomPixelData.Create(dataset);
            }

            // Clone the encapsulated dataset because modifying the pixel data modifies the dataset
            var clone = dataset.Clone();
            clone.InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;

            var pixelData = DicomPixelData.Create(clone, true);

            // TODO: can this temporary fixes be removed now?

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

        /// <summary>
        /// Create array of graphics overlays from dataset.
        /// </summary>
        /// <param name="dataset">Dataset potentially containing overlays.</param>
        /// <returns>Array of overlays of type <see cref="DicomOverlayType.Graphics"/>.</returns>
        private static DicomOverlayData[] CreateGraphicsOverlays(DicomDataset dataset)
            => DicomOverlayData.FromDataset(dataset)
                .Where(x => x.Type == DicomOverlayType.Graphics && x.Data != null)
                .ToArray();

        /// <summary>
        /// Create image rendering pipeline according to the <see cref="DicomPixelData.PhotometricInterpretation">photometric interpretation</see>
        /// of the pixel data.
        /// </summary>
        private static (IPipeline pipeline, GrayscaleRenderOptions renderOptions) CreatePipelineData(DicomDataset dataset, DicomPixelData pixelData, int frame)
        {
            var pi = pixelData.PhotometricInterpretation;
            var samples = pixelData.SamplesPerPixel;

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
                // Monochrome1 or Monochrome2 for grayscale image
                renderOptions = GrayscaleRenderOptions.FromDataset(dataset, frame);
                pipeline = new GenericGrayscalePipeline(renderOptions);
            }
            else if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull
                || pi == PhotometricInterpretation.YbrFull422 || pi == PhotometricInterpretation.YbrPartial422)
            {
                // RGB for color image
                pipeline = new RgbColorPipeline();
            }
            else if (pi == PhotometricInterpretation.PaletteColor)
            {
                // PALETTE COLOR for Palette image
                pipeline = new PaletteColorPipeline(pixelData);
            }
            else
            {
                throw new DicomImagingException($"Unsupported pipeline photometric interpretation: {pi}");
            }

            return (pipeline, renderOptions);
        }

        private void RecreatePipeline(GrayscaleRenderOptions renderoptions)
        {
            if (_pipeline is GenericGrayscalePipeline)
            {
                _pipeline = new GenericGrayscalePipeline(renderoptions);
            }
        }

        #endregion

    }
}
