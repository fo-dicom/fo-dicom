// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// DICOM Image class for image rendering.
    /// </summary>
    public class DicomImage
    {
        #region FIELDS

        private double _scale;
        private bool _showOverlays;
        private int _overlayColor = unchecked((int)0xffff00ff);


        // the source of data
        private readonly DicomDataset _dataset;
        private readonly DicomPixelData _pixelDataCache;
        private readonly Lazy<DicomOverlayData[]> _overlays;
        private readonly PhotometricInterpretation _pi;

        // a cache of uncompressed raw pixels
        private readonly ConcurrentDictionary<int, IPixelData> _pixelsCache = new ConcurrentDictionary<int, IPixelData>();

        private readonly ConcurrentDictionary<int, IPipeline> _pipelineCache = new ConcurrentDictionary<int, IPipeline>();

        private readonly ConcurrentDictionary<int, IImage> _renderedImageCache = new ConcurrentDictionary<int, IImage>();

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

            _dataset = DicomTranscoder.ExtractOverlays(dataset);
            _pixelDataCache = CreateDicomPixelData(_dataset);
            _overlays = new Lazy<DicomOverlayData[]>(() => CreateGraphicsOverlays(_dataset), true);

            _pi = _pixelDataCache.PhotometricInterpretation;
            var samples = _pixelDataCache.SamplesPerPixel;

            if (_pi == null)
            {
                // generally ACR-NEMA
                if (samples == 0 || samples == 1)
                {
                    _pi = dataset.Contains(DicomTag.RedPaletteColorLookupTableData)
                        ? PhotometricInterpretation.PaletteColor
                        : PhotometricInterpretation.Monochrome2;
                }
                else
                {
                    // assume, probably incorrectly, that the image is RGB
                    _pi = PhotometricInterpretation.Rgb;
                }
            }

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

        public CacheType CacheMode { get; set; } = CacheType.PixelData;

        /// <summary>Width of image in pixels</summary>
        public int Width => _dataset.GetSingleValue<ushort>(DicomTag.Columns);

        /// <summary>Height of image in pixels</summary>
        public int Height => _dataset.GetSingleValue<ushort>(DicomTag.Rows);

        /// <summary>Number of frames contained in image data.</summary>
        public int NumberOfFrames => _dataset.GetSingleValueOrDefault(DicomTag.NumberOfFrames, (ushort)1);

        /// <summary>Gets or sets whether the image is gray scale.</summary>
        public virtual bool IsGrayscale => _pi == PhotometricInterpretation.Monochrome1 || _pi == PhotometricInterpretation.Monochrome2;

        /// <summary>Scaling factor of the rendered image</summary>
        public double Scale
        {
            get => _scale;
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    ResetAllRenderedImages();
                }
            }
        }

        /// <summary>Gets or sets window width of rendered gray scale image.</summary>
        public virtual double WindowWidth
        {
            get => (GetOrCreateCachedFramePipeline(CurrentFrame) as GenericGrayscalePipeline)?.WindowWidth ?? 255;
            set
            {
                if (GetOrCreateCachedFramePipeline(CurrentFrame) is GenericGrayscalePipeline pipeline && pipeline.WindowWidth != value)
                {
                    pipeline.WindowWidth = value;
                    ResetRenderedImage(CurrentFrame);
                }
            }
        }

        /// <summary>Gets or sets window center of rendered gray scale image.</summary>
        public virtual double WindowCenter
        {
            get => (GetOrCreateCachedFramePipeline(CurrentFrame) as GenericGrayscalePipeline)?.WindowCenter ?? 255;
            set
            {
                if (GetOrCreateCachedFramePipeline(CurrentFrame) is GenericGrayscalePipeline pipeline && pipeline.WindowCenter != value)
                {
                    pipeline.WindowCenter = value;
                    ResetRenderedImage(CurrentFrame);
                }
            }
        }

        /// <summary>Gets or sets whether to use VOI LUT.</summary>
        public virtual bool UseVOILUT
        {
            get => (GetOrCreateCachedFramePipeline(CurrentFrame) as GenericGrayscalePipeline)?.UseVOILUT ?? false;
            set
            {
                if (GetOrCreateCachedFramePipeline(CurrentFrame) is GenericGrayscalePipeline pipeline && pipeline.UseVOILUT != value)
                {
                    pipeline.UseVOILUT = value;
                    ResetRenderedImage(CurrentFrame);
                }
            }
        }

        /// <summary>Gets or sets whether to render in inverted grey.</summary>
        public virtual bool Invert
        {
            get => (GetOrCreateCachedFramePipeline(CurrentFrame) as GenericGrayscalePipeline)?.Invert ?? false;
            set
            {
                if (GetOrCreateCachedFramePipeline(CurrentFrame) is GenericGrayscalePipeline pipeline && pipeline.Invert != value)
                {
                    pipeline.Invert = value;
                    ResetRenderedImage(CurrentFrame);
                }
            }
        }


        /// <summary>Gets or sets the color map to be applied when rendering grayscale images.</summary>
        public virtual Color32[] GrayscaleColorMap
        {
            get => (GetOrCreateCachedFramePipeline(CurrentFrame) as GenericGrayscalePipeline)?.GrayscaleColorMap;
            set
            {
                if (GetOrCreateCachedFramePipeline(CurrentFrame) is GenericGrayscalePipeline pipeline)
                {
                    pipeline.GrayscaleColorMap = value;
                    ResetRenderedImage(CurrentFrame);
                }
            }
        }

        /// <summary>Show or hide DICOM overlays</summary>
        public bool ShowOverlays
        { 
            get => _showOverlays;
            set
            {
                if (_showOverlays != value)
                {
                    _showOverlays = value;
                    ResetAllRenderedImages();
                }
            }
        }

        /// <summary>Gets or sets the color used for displaying DICOM overlays. Default is magenta.</summary>
        public int OverlayColor
        {
            get => _overlayColor;
            set
            {
                if (_overlayColor != value)
                {
                    _overlayColor = value;
                    ResetAllRenderedImages();
                }
            }
        }

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
            CurrentFrame = frame;

            if (_renderedImageCache.TryGetValue(frame, out var image))
            {
                return image;
            }

            var pixels = GetOrCreateCachedPixelData(frame).Rescale(_scale);

            var pipeline = GetOrCreateCachedFramePipeline(frame);

            var graphic = new ImageGraphic(pixels);

            if (ShowOverlays)
            {
                foreach (var overlay in _overlays.Value)
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

            image = graphic.RenderImage(pipeline.LUT);

            if (CacheMode.HasFlag(CacheType.Display))
            {
                _renderedImageCache.TryAdd(frame, image);
            }

            // now after rendering, the pipeline should remove the data, if caching is not configured
            if (!CacheMode.HasFlag(CacheType.LookupTables))
            {
                pipeline.ClearCache();
            }

            return image;
        }

        private IPixelData GetOrCreateCachedPixelData(int frame)
        {
            if (CacheMode.HasFlag(CacheType.PixelData))
            {
                // get the pixel data from cache, or add it there
                return _pixelsCache.GetOrAdd(frame, f => GetFrameData(f));
            }
            else
            {
                // no caching, so generate the pixel data without caching
                return GetFrameData(frame);
            }
        }

        private IPipeline GetOrCreateCachedFramePipeline(int frame)
        {
            // even if caching of the lookuptable is disabled, the pipeline has to be created anyway.
            // it is because of the rendering options, that the user wants to configure in pipeline before the actual rendering
            return _pipelineCache.GetOrAdd(frame, f => CreatePipelineData(f));
        }

        /// <summary>
        /// If necessary, prepare new frame data, and return appropriate data.
        /// </summary>
        /// <param name="frame">The frame number to create pixeldata for.</param>
        /// <returns>Data the frame</returns>
        private IPixelData GetFrameData(int frame)
        {
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

        /// <summary>
        /// If caching of rendered images is activated, then the image has to be reset, because one of the renderparameters changed
        /// </summary>
        /// <param name="frame"></param>
        private void ResetRenderedImage(int frame)
        {
            // if 
            _renderedImageCache.TryRemove(frame, out _);
        }

        /// <summary>
        /// If caching of rendered images is activated, then the image has to be reset, because one of the renderparameters changed
        /// </summary>
        /// <param name="frame"></param>
        private void ResetAllRenderedImages()
        {
            // if 
            _renderedImageCache.Clear();
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

            //// TODO: can this temporary fixes be removed now?

            //// temporary fix for JPEG compressed YBR images, according to enforcement above
            //if ((inputTransferSyntax == DicomTransferSyntax.JPEGProcess1
            //     || inputTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && pixelData.SamplesPerPixel == 3)
            //{
            //    // When converting to RGB in Dicom.Imaging.Codec.Jpeg.i, PlanarConfiguration is set to Interleaved
            //    pixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
            //    pixelData.PlanarConfiguration = PlanarConfiguration.Interleaved;
            //}

            //// temporary fix for JPEG 2000 Lossy images
            //if ((inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossy
            //     && pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrIct)
            //    || (inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossless
            //        && pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrRct))
            //{
            //    // Converted to RGB in Dicom.Imaging.Codec.Jpeg2000.cpp
            //    pixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
            //}

            //// temporary fix for JPEG2000 compressed YBR images
            //if ((inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossless
            //     || inputTransferSyntax == DicomTransferSyntax.JPEG2000Lossy)
            //    && (pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull
            //        || pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422
            //        || pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422))
            //{
            //    // For JPEG2000 YBR type images in Dicom.Imaging.Codec.Jpeg2000.cpp, 
            //    // YBR_FULL is applied and PlanarConfiguration is set to Planar
            //    pixelData.PhotometricInterpretation = PhotometricInterpretation.YbrFull;
            //    pixelData.PlanarConfiguration = PlanarConfiguration.Planar;
            //}

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
        private IPipeline CreatePipelineData(int frame)
        {
            IPipeline pipeline;
            GrayscaleRenderOptions renderOptions = null;
            if (_pi == PhotometricInterpretation.Monochrome1 || _pi == PhotometricInterpretation.Monochrome2)
            {
                // Monochrome1 or Monochrome2 for grayscale image
                renderOptions = GrayscaleRenderOptions.FromDataset(_dataset, frame);
                pipeline = new GenericGrayscalePipeline(renderOptions);
            }
            else if (_pi == PhotometricInterpretation.Rgb || _pi == PhotometricInterpretation.YbrFull
                || _pi == PhotometricInterpretation.YbrFull422 || _pi == PhotometricInterpretation.YbrPartial422)
            {
                // RGB for color image
                pipeline = new RgbColorPipeline();
            }
            else if (_pi == PhotometricInterpretation.PaletteColor)
            {
                // PALETTE COLOR for Palette image
                pipeline = new PaletteColorPipeline(_pixelDataCache);
            }
            else
            {
                throw new DicomImagingException($"Unsupported pipeline photometric interpretation: {_pi}");
            }

            return pipeline;
        }

        #endregion

    }
}
