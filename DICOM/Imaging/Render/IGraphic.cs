using System;
#if !SILVERLIGHT
using System.Drawing;
#endif
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	public interface IGraphic {
		int OriginalWidth { get; }
		int OriginalHeight { get; }
		int OriginalOffsetX { get; }
		int OriginalOffsetY { get; }
		double ScaleFactor { get; }
		int ScaledWidth { get; }
		int ScaledHeight { get; }
		int ScaledOffsetX { get; }
		int ScaledOffsetY { get; }

		int ZOrder { get; }

		void Reset();
		void Scale(double scale);
		void BestFit(int width, int height);
		void Rotate(int angle);
		void FlipX();
		void FlipY();
		void Transform(double scale, int rotation, bool flipx, bool flipy);
#if !SILVERLIGHT
		Image RenderImage(ILUT lut);
#endif
		BitmapSource RenderImageSource(ILUT lut);
	}
}
