using System;
using System.Threading.Tasks;

using Dicom.Imaging.Algorithms;

namespace Dicom.Imaging.Render {
	public class OverlayGraphic {
		#region Private Members
		private SingleBitPixelData _originalData;
		private IPixelData _scaledData;
		private int _offsetX;
		private int _offsetY;
		private int _color;
		private double _scale;
		#endregion

		#region Public Constructors
		public OverlayGraphic(SingleBitPixelData pixelData, int offsetx, int offsety, int color) {
			_originalData = pixelData;
			_scaledData = _originalData;
			_offsetX = offsetx;
			_offsetY = offsety;
			_color = color;
			_scale = 1.0;
		}
		#endregion

		#region Public Methods
		public void Scale(double scale) {
			if (Math.Abs(scale - _scale) <= Double.Epsilon)
				return;

			_scale = scale;
			_scaledData = null;
		}

		public void Render(int[] pixels, int width, int height) {
			byte[] data = null;

			if (_scaledData == null)
				_scaledData = _originalData.Rescale(_scale);

			data = (_scaledData as GrayscalePixelDataU8).Data;

			int ox = (int)(_offsetX * _scale);
			int oy = (int)(_offsetY * _scale);

			Parallel.For(0, _scaledData.Height, y => {
				for (int i = _scaledData.Width * y, e = i + _scaledData.Width; i < e; i++) {
					if (data[i] > 0) {
						int p = (oy * width) + ox + i;
						pixels[p] = _color;
					}
				}
			});
		}
		#endregion
	}
}
