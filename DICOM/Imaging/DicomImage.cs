using System;
using System.Collections.Generic;
#if !SILVERLIGHT
using System.Drawing;
using System.Drawing.Imaging;
#endif
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text;

using Dicom;
using Dicom.Imaging.Codec;
using Dicom.Imaging.LUT;
using Dicom.Imaging.Render;

namespace Dicom.Imaging {
	/// <summary>
	/// DICOM Image calss with capability of
	/// </summary>
	public class DicomImage {
		#region Private Members
		private int _currentFrame;
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
		public DicomImage(DicomDataset dataset, int frame = 0) {
			_scale = 1.0;
			ShowOverlays = true;
			Load(dataset, frame);
		}

#if !SILVERLIGHT
		/// <summary>Creates DICOM image object from file</summary>
		/// <param name="fileName">Source file</param>
		/// <param name="frame">Zero indexed frame number</param>
		public DicomImage(string fileName, int frame = 0) {
			_scale = 1.0;
			ShowOverlays = true;
			var file = DicomFile.Open(fileName);
			Load(file.Dataset, frame);
		}
#endif

		/// <summary>Source DICOM dataset</summary>
		public DicomDataset Dataset {
			get;
			private set;
		}

		/// <summary>DICOM pixel data</summary>
		public DicomPixelData PixelData {
			get;
			private set;
		}

		/// <summary>Width of image in pixels</summary>
		public int Width {
			get { return PixelData.Width; }
		}

		/// <summary>Height of image in pixels</summary>
		public int Height {
			get { return PixelData.Height; }
		}

		/// <summary>Scaling factor of the rendered image</summary>
		public double Scale {
			get { return _scale; }
			set {
				_scale = value;
				_pixelData = null;
			}
		}
		/// <summary>Photometric interpretation of pixel data.</summary>
		public PhotometricInterpretation PhotometricInterpretation {
			get;
			private set;
		}

		/// <summary>Number of frames contained in image data.</summary>
		public int NumberOfFrames {
			get { return PixelData.NumberOfFrames; }
		}

		/// <summary>Window width of rendered gray scale image </summary>
		public double WindowWidth {
			get {
				return _renderOptions != null ? _renderOptions.WindowWidth : 0;
			}
			set {
				if (_renderOptions != null)
					_renderOptions.WindowWidth = value;
			}
		}

		/// <summary>Window center of rendered gray scale image </summary>
		public double WindowCenter {
			get {
				return _renderOptions != null ? _renderOptions.WindowCenter : 0;
			}
			set {
				if (_renderOptions != null)
					_renderOptions.WindowCenter = value;
			}
		}

		/// <summary>Show or hide DICOM overlays</summary>
		public bool ShowOverlays {
			get;
			set;
		}

		/// <summary>Color used for displaying DICOM overlays. Default is magenta.</summary>
		public int OverlayColor {
			get { return _overlayColor; }
			set { _overlayColor = value; }
		}

#if !SILVERLIGHT
		/// <summary>Renders DICOM image to System.Drawing.Image</summary>
		/// <param name="frame">Zero indexed frame number</param>
		/// <returns>Rendered image</returns>
		public Image RenderImage(int frame = 0) {
			if (frame != _currentFrame || _pixelData == null)
				Load(Dataset, frame);

			var graphic = new ImageGraphic(_pixelData);

			if (ShowOverlays) {
				foreach (var overlay in _overlays) {
					if ((frame + 1) < overlay.OriginFrame || (frame + 1) > (overlay.OriginFrame + overlay.NumberOfFrames - 1))
						continue;

					var og = new OverlayGraphic(PixelDataFactory.Create(overlay), overlay.OriginX - 1, overlay.OriginY - 1, OverlayColor);
					graphic.AddOverlay(og);
				}
			}

			return graphic.RenderImage(_pipeline.LUT);
		}
#endif

		/// <summary>
		/// Renders DICOM image to <see cref="System.Windows.Media.ImageSource"/> 
		/// </summary>
		/// <param name="frame">Zero indexed frame nu,ber</param>
		/// <returns>Rendered image</returns>
		public ImageSource RenderImageSource(int frame = 0) {
			if (frame != _currentFrame || _pixelData == null)
				Load(Dataset, frame);

			var graphic = new ImageGraphic(_pixelData);

			if (ShowOverlays) {
				foreach (var overlay in _overlays) {
					if ((frame + 1) < overlay.OriginFrame || (frame + 1) > (overlay.OriginFrame + overlay.NumberOfFrames - 1))
						continue;

					var og = new OverlayGraphic(PixelDataFactory.Create(overlay), overlay.OriginX - 1, overlay.OriginY - 1, OverlayColor);
					graphic.AddOverlay(og);
				}
			}

			return graphic.RenderImageSource(_pipeline.LUT);
		}


		/// <summary>
		/// Loads the pixel data for specified frame and set the internal dataset
		/// 
		/// </summary>
		/// <param name="dataset">dataset to load pixeldata from</param>
		/// <param name="frame">The frame number to create pixeldata for</param>
		private void Load(DicomDataset dataset, int frame) {
			Dataset = DicomTranscoder.ExtractOverlays(dataset);

			if (PixelData == null) {
				PixelData = DicomPixelData.Create(Dataset);
				PhotometricInterpretation = PixelData.PhotometricInterpretation;
			}

			if (Dataset.InternalTransferSyntax.IsEncapsulated) {
				// decompress single frame from source dataset
				DicomCodecParams cparams = null;
				if (Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess1 || Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) {
					cparams = new DicomJpegParams {
						ConvertColorspaceToRGB = true
					};
				}

				var transcoder = new DicomTranscoder(Dataset.InternalTransferSyntax, DicomTransferSyntax.ExplicitVRLittleEndian);
				transcoder.InputCodecParams = cparams;
				transcoder.OutputCodecParams = cparams;
				var buffer = transcoder.DecodeFrame(Dataset, frame);

				// clone the dataset because modifying the pixel data modifies the dataset
				var clone = Dataset.Clone();
				clone.InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;

				var pixelData = DicomPixelData.Create(clone, true);
				pixelData.AddFrame(buffer);

				// temporary fix for JPEG compressed YBR images
				if ((Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess1 || Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && pixelData.SamplesPerPixel == 3)
					pixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;

				// temporary fix for JPEG 2000 Lossy images
				if (pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrIct || pixelData.PhotometricInterpretation == PhotometricInterpretation.YbrRct)
					pixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;

				_pixelData = PixelDataFactory.Create(pixelData, 0);
			} else {
				// pull uncompressed frame from source pixel data
				_pixelData = PixelDataFactory.Create(PixelData, frame);
			}

			_pixelData = _pixelData.Rescale(_scale);

			_overlays = DicomOverlayData.FromDataset(Dataset).Where(x => x.Type == DicomOverlayType.Graphics && x.Data != null).ToArray();

			_currentFrame = frame;

			CreatePipeline();
		}

		/// <summary>
		/// Create image rendering pipeline according to the Dataset <see cref="PhotometricInterpretation"/>.
		/// </summary>
		private void CreatePipeline() {
			if (_pipeline != null)
				return;

			var pi = Dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation);
			var samples = Dataset.Get<ushort>(DicomTag.SamplesPerPixel, 0, 0);

			// temporary fix for JPEG compressed YBR images
			if ((Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess1 || Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess2_4) && samples == 3)
				pi = PhotometricInterpretation.Rgb;

			// temporary fix for JPEG 2000 Lossy images
			if (pi == PhotometricInterpretation.YbrIct || pi == PhotometricInterpretation.YbrRct)
				pi = PhotometricInterpretation.Rgb;

			if (pi == null) {
				// generally ACR-NEMA
				if (samples == 0 || samples == 1) {
					if (Dataset.Contains(DicomTag.RedPaletteColorLookupTableData))
						pi = PhotometricInterpretation.PaletteColor;
					else
						pi = PhotometricInterpretation.Monochrome2;
				} else {
					// assume, probably incorrectly, that the image is RGB
					pi = PhotometricInterpretation.Rgb;
				}
			}

			if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2) {
				//Monochrom1 or Monochrome2 for grayscale image
				if (_renderOptions == null)
					_renderOptions = GrayscaleRenderOptions.FromDataset(Dataset);
				_pipeline = new GenericGrayscalePipeline(_renderOptions);
			} else if (pi == PhotometricInterpretation.Rgb) {
				//RGB for color image
				_pipeline = new RgbColorPipeline();
			} else if (pi == PhotometricInterpretation.PaletteColor) {
				//PALETTE COLOR for Palette image
				_pipeline = new PaletteColorPipeline(PixelData);
			} else {
				throw new DicomImagingException("Unsupported pipeline photometric interpretation: {0}", pi.Value);
			}
		}
	}
}
