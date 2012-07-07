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

		private IPixelData _pixelData;
		private IPipeline _pipeline;

		private GrayscaleRenderOptions _renderOptions;

		private DicomOverlayData[] _overlays;
		#endregion

		/// <summary>Creates DICOM image object from dataset</summary>
		/// <param name="dataset">Source dataset</param>
		public DicomImage(DicomDataset dataset) {
			Load(dataset);
		}

#if !SILVERLIGHT
		/// <summary>Creates DICOM image object from file</summary>
		/// <param name="fileName">Source file</param>
		public DicomImage(string fileName) {
			var file = DicomFile.Open(fileName);
			Load(file.Dataset);
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

		/// <summary>Renders DICOM image to System.Drawing.Image</summary>
		/// <returns>Rendered image</returns>
#if !SILVERLIGHT
		public Image RenderImage() {
			CreatePipeline();

			ImageGraphic graphic = new ImageGraphic(_pixelData);

			foreach (var overlay in _overlays) {
				OverlayGraphic og = new OverlayGraphic(PixelDataFactory.Create(overlay), overlay.OriginX, overlay.OriginY, OverlayColor);
				graphic.AddOverlay(og);
			}

			return graphic.RenderImage(_pipeline.LUT);
		}
#endif

		public ImageSource RenderImageSource() {
			CreatePipeline();

			ImageGraphic graphic = new ImageGraphic(_pixelData);

			foreach (var overlay in _overlays) {
				OverlayGraphic og = new OverlayGraphic(PixelDataFactory.Create(overlay), overlay.OriginX, overlay.OriginY, OverlayColor);
				graphic.AddOverlay(og);
			}

			return graphic.RenderImageSource(_pipeline.LUT);
		}

		private void Load(DicomDataset dataset) {
			Dataset = dataset;
			if (Dataset.InternalTransferSyntax.IsEncapsulated)
				Dataset = Dataset.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian, null);

			DicomPixelData pixelData = DicomPixelData.Create(dataset);
			_pixelData = PixelDataFactory.Create(pixelData, 0);
			_overlays = DicomOverlayData.FromDataset(Dataset);
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
