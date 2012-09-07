using System;
using System.Collections.Generic;
#if !SILVERLIGHT
using System.Drawing;
using System.Drawing.Imaging;
#endif
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text;
using Dicom;
using Dicom.Imaging.Codec;
using Dicom.Imaging.LUT;
using Dicom.Imaging.Render;

namespace Dicom.Imaging {
	/// <summary>
	/// DICOM Image
	/// </summary>
	public class DicomImage {
		#region Private Members
		private const int OverlayColor = unchecked((int)0xffff00ff);

		private int _currentFrame;
		private IPixelData _pixelData;
		private IPipeline _pipeline;

		private GrayscaleRenderOptions _renderOptions;

		private DicomOverlayData[] _overlays;
		#endregion

		/// <summary>Creates DICOM image object from dataset</summary>
		/// <param name="dataset">Source dataset</param>
		/// <param name="frame">Zero indexed frame number</param>
		public DicomImage(DicomDataset dataset, int frame = 0) {
			Load(dataset, frame);
		}

#if !SILVERLIGHT
		/// <summary>Creates DICOM image object from file</summary>
		/// <param name="fileName">Source file</param>
		/// <param name="frame">Zero indexed frame number</param>
		public DicomImage(string fileName, int frame = 0) {
			var file = DicomFile.Open(fileName);
			Load(file.Dataset, frame);
		}
#endif

		/// <summary>Source DICOM dataset</summary>
		public DicomDataset Dataset {
			get;
			private set;
		}

		/// <summary>Width of image in pixels</summary>
		public int Width {
			get { return _pixelData.Width; }
		}

		/// <summary>Height of image in pixels</summary>
		public int Height {
			get { return _pixelData.Height; }
		}

		/// <summary>Number of frames contained in image data.</summary>
		public int NumberOfFrames {
			get { return Dataset.Get<int>(DicomTag.NumberOfFrames, 0, 1); }
		}

		/// <summary>Renders DICOM image to System.Drawing.Image</summary>
		/// <param name="frame">Zero indexed frame number</param>
		/// <returns>Rendered image</returns>
#if !SILVERLIGHT
		public Image RenderImage(int frame = 0) {
			if (frame != _currentFrame)
				Load(Dataset, frame);

			CreatePipeline();

			ImageGraphic graphic = new ImageGraphic(_pixelData);

			foreach (var overlay in _overlays) {
				OverlayGraphic og = new OverlayGraphic(PixelDataFactory.Create(overlay), overlay.OriginX, overlay.OriginY, OverlayColor);
				graphic.AddOverlay(og);
			}

			return graphic.RenderImage(_pipeline.LUT);
		}
#endif

		public ImageSource RenderImageSource(int frame = 0) {
			if (frame != _currentFrame)
				Load(Dataset, frame);

			CreatePipeline();

			ImageGraphic graphic = new ImageGraphic(_pixelData);

			foreach (var overlay in _overlays) {
				OverlayGraphic og = new OverlayGraphic(PixelDataFactory.Create(overlay), overlay.OriginX, overlay.OriginY, OverlayColor);
				graphic.AddOverlay(og);
			}

			return graphic.RenderImageSource(_pipeline.LUT);
		}

		private void Load(DicomDataset dataset, int frame) {
			Dataset = dataset;
			if (Dataset.InternalTransferSyntax.IsEncapsulated) {
				DicomCodecParams cparams = null;
				if (Dataset.InternalTransferSyntax == DicomTransferSyntax.JPEGProcess1) {
					cparams = new DicomJpegParams {
						ConvertColorspaceToRGB = true
					};
				}
				Dataset = Dataset.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian, cparams);
			}

			DicomPixelData pixelData = DicomPixelData.Create(Dataset);
			_pixelData = PixelDataFactory.Create(pixelData, frame);
			_overlays = DicomOverlayData.FromDataset(Dataset);
			_currentFrame = frame;
		}

		private void CreatePipeline() {
			if (_pipeline != null)
				return;

			var pi = Dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation);
			if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2) {
				if (_renderOptions == null)
					_renderOptions = GrayscaleRenderOptions.FromDataset(Dataset);
				_pipeline = new GenericGrayscalePipeline(_renderOptions);
			} else if (pi == PhotometricInterpretation.Rgb) {
				_pipeline = new RgbColorPipeline();
			} else {
				throw new DicomImagingException("Unsupported pipeline photometric interpretation: {0}", pi.Value);
			}
		}
	}
}
