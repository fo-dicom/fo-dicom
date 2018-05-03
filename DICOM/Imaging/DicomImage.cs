// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
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
        #region FIELDS

        private readonly object _lock = new object();

        private int _currentFrame;

        private double _scale;

        private bool _rerender;

        private readonly DicomDataset _dataset;

        private readonly DicomPixelData _pixelData;

        private DicomOverlayData[] _overlays;

        private IPixelData _pixels;

        private IPipeline _pipeline;

        private GrayscaleRenderOptions _renderOptions;

        private readonly IDictionary<int, int> _frameIndices;

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
            _frameIndices = new ConcurrentDictionary<int, int>();

            _dataset = DicomTranscoder.ExtractOverlays(dataset);
            _pixelData = CreateDicomPixelData(_dataset);
            _currentFrame = frame;
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

        /// <summary>
        /// Gets the dataset constituting the DICOM image.
        /// </summary>
        [Obsolete("Dataset should not be publicly accessible from DicomImage object.")]
        public DicomDataset Dataset => _dataset;

        /// <summary>
        /// Gets the pixel data header object associated with the image.
        /// </summary>
        [Obsolete("PixelData should not be publicly accessible from the DicomImage object.")]
        public DicomPixelData PixelData => _pixelData;

        [Obsolete("Use IsGrayscale to determine whether DicomImage object is grayscale or color.")]
        public PhotometricInterpretation PhotometricInterpretation => _pixelData.PhotometricInterpretation;

        /// <summary>Width of image in pixels</summary>
        public int Width => _pixelData.Width;

        /// <summary>Height of image in pixels</summary>
        public int Height => _pixelData.Height;

        /// <summary>Scaling factor of the rendered image</summary>
        public double Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                lock (_lock)
                {
                    _scale = value;
                    _rerender = true;
                }
            }
        }

        // Note that the NumberOfFrames getter accesses the dataset's attribute. This is because the corresponding
        // getter in the pixel data might not be complete in the case of encapsulated datasets, where the frames are
        // continuously added upon request.

        /// <summary>Number of frames contained in image data.</summary>
        public int NumberOfFrames => _dataset.Get(DicomTag.NumberOfFrames, (ushort)1);

        /// <summary>Gets or sets window width of rendered gray scale image.</summary>
        public virtual double WindowWidth
        {
            get
            {
                EstablishPipeline();
                return _renderOptions?.WindowWidth ?? 255;
            }
            set
            {
                EstablishPipeline();

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
                EstablishPipeline();
                return _renderOptions?.WindowCenter ?? 127;
            }
            set
            {
                EstablishPipeline();

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
                EstablishPipeline();
                return _renderOptions?.ColorMap;
            }
            set
            {
                EstablishPipeline();

                if (_renderOptions != null)
                {
                    _renderOptions.ColorMap = value;
                }
                else
                {
                    throw new DicomImagingException(
                        "Grayscale color map not applicable for photometric interpretation: {0}",
                        _pixelData.PhotometricInterpretation);
                }
            }
        }

        /// <summary>Gets or sets whether the image is gray scale.</summary>
        public virtual bool IsGrayscale
        {
            get
            {
                EstablishPipeline();
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
        public int CurrentFrame => _currentFrame;

        #endregion

        #region METHODS

        /// <summary>Renders DICOM image to <see cref="IImage"/>.</summary>
        /// <param name="frame">Zero indexed frame number.</param>
        /// <returns>Rendered image</returns>
        public virtual IImage RenderImage(int frame = 0)
        {
            bool load;
            lock (_lock)
            {
                load = frame >= 0 && (frame != CurrentFrame || _rerender);
                _currentFrame = frame;
                _rerender = false;
            }

            var frameIndex = GetFrameIndex(frame);
            if (load)
            {
                lock (_lock)
                {
                    _pixels = PixelDataFactory.Create(_pixelData, frameIndex).Rescale(_scale);
                }
            }

            if (ShowOverlays) EstablishGraphicsOverlays();

            IImage image;
            lock (_lock)
            { 
                var graphic = new ImageGraphic(_pixels);

                if (ShowOverlays)
                {
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

                image = graphic.RenderImage(_pipeline.LUT);
            }

            return image;
        }

        /// <summary>
        /// If necessary, prepare new frame data, and return appropriate frame index.
        /// </summary>
        /// <param name="frame">The frame number to create pixeldata for.</param>
        /// <returns>Index of the frame, might be diffrent than the frame number for encapsulated images.</returns>
        private int GetFrameIndex(int frame)
        {
            EstablishPipeline();

            if (_dataset.InternalTransferSyntax.IsEncapsulated)
            {
                int index;
                if (!_frameIndices.TryGetValue(frame, out index))
                {
                    // decompress single frame from source dataset
                    var transcoder = new DicomTranscoder(
                        _dataset.InternalTransferSyntax,
                        DicomTransferSyntax.ExplicitVRLittleEndian);
                    var buffer = transcoder.DecodeFrame(_dataset, frame);

                    lock (_lock)
                    {
                        // Additional check to ensure that frame has not been provided by other thread.
                        if (!_frameIndices.TryGetValue(frame, out index))
                        { 
                            // Get frame/index mapping for previously unstored frame.
                            index = _pixelData.NumberOfFrames;
                            _frameIndices.Add(frame, index);

                            _pixelData.AddFrame(buffer);
                        }
                    }
                }

                return index;
            }

            return frame;
        }

        private void EstablishPipeline()
        {
            bool create;
            lock (_lock) create = _pipeline == null;

            var tuple = create ? CreatePipelineData(_dataset, _pixelData) : null;

            lock (_lock)
            {
                if (_pipeline == null)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    _pipeline = tuple.Pipeline;
                    _renderOptions = tuple.RenderOptions;
                }
            }
        }

        private void EstablishGraphicsOverlays()
        {
            bool create;
            lock (_lock) create = _overlays == null;

            var overlays = create ? CreateGraphicsOverlays(_dataset) : null;

            lock (_lock)
            {
                if (_overlays == null) _overlays = overlays;
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

        /// <summary>
        /// Create array of graphics overlays from dataset.
        /// </summary>
        /// <param name="dataset">Dataset potentially containing overlays.</param>
        /// <returns>Array of overlays of type <see cref="DicomOverlayType.Graphics"/>.</returns>
        private static DicomOverlayData[] CreateGraphicsOverlays(DicomDataset dataset)
        {
            return DicomOverlayData.FromDataset(dataset)
                .Where(x => x.Type == DicomOverlayType.Graphics && x.Data != null)
                .ToArray();
        }

        /// <summary>
        /// Create image rendering pipeline according to the <see cref="DicomPixelData.PhotometricInterpretation">photometric interpretation</see>
        /// of the pixel data.
        /// </summary>
        private static PipelineData CreatePipelineData(DicomDataset dataset, DicomPixelData pixelData)
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

            IPipeline pipeline;
            GrayscaleRenderOptions renderOptions = null;
            if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2)
            {
                //Monochrome1 or Monochrome2 for grayscale image
                renderOptions = GrayscaleRenderOptions.FromDataset(dataset);
                pipeline = new GenericGrayscalePipeline(renderOptions);
            }
            else if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull
                || pi == PhotometricInterpretation.YbrFull422 || pi == PhotometricInterpretation.YbrPartial422)
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
                throw new DicomImagingException("Unsupported pipeline photometric interpretation: {0}", pi);
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
