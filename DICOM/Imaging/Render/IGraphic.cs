using System;
#if !SILVERLIGHT
using System.Drawing;
#endif
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	/// <summary>
	/// Graphic interface
	/// </summary>
	public interface IGraphic {
		/// <summary>
		/// The original image width
		/// </summary>
		int OriginalWidth { get; }
		/// <summary>
		/// The original image height
		/// </summary>
		int OriginalHeight { get; }
		/// <summary>
		/// The original image offset in X direction
		/// </summary>
		int OriginalOffsetX { get; }
		/// <summary>
		/// The original image offset in Y direction
		/// </summary>
		int OriginalOffsetY { get; }
		/// <summary>
		/// The image scale factor
		/// </summary>
		double ScaleFactor { get; }
		/// <summary>
		/// The scaled image width
		/// </summary>
		int ScaledWidth { get; }
		/// <summary>
		/// The scaled image height
		/// </summary>
		int ScaledHeight { get; }
		/// <summary>
		/// The scaled image offset in X direction
		/// </summary>
		int ScaledOffsetX { get; }
		/// <summary>
		/// The scaled image offset in Y direction
		/// </summary>
		int ScaledOffsetY { get; }

		/// <summary>
		/// The Z Order
		/// </summary>
		int ZOrder { get; }

		/// <summary>
		/// Reset the tranformation to default
		/// </summary>
		void Reset();
		/// <summary>
		/// Scale the image
		/// </summary>
		/// <param name="scale">scale factor</param>
		void Scale(double scale);

		/// <summary>
		/// Auto calculate the scale factor to fit the image in the specified width and height
		/// </summary>
		/// <param name="width">Destination width</param>
		/// <param name="height">Destination height</param>
		void BestFit(int width, int height);

		/// <summary>
		/// Rotate the image arround its center 
		/// </summary>
		/// <param name="angle">Rotation angle</param>
		void Rotate(int angle);
		/// <summary>
		/// Flip the image vertically arround its X-Axis
		/// </summary>
		void FlipX();
		/// <summary>
		/// Flip the image horizontally arround its Y-Axis
		/// </summary>
		void FlipY();

		/// <summary>
		/// Transform the image 
		/// </summary>
		/// <param name="scale">Scale factor</param>
		/// <param name="rotation">Rotation angle</param>
		/// <param name="flipx">True to flip vertically</param>
		/// <param name="flipy">True to flip horizontally</param>
		void Transform(double scale, int rotation, bool flipx, bool flipy);
#if !SILVERLIGHT
		/// <summary>
		/// Render the image and return the result as <seealso cref="Image"/>
		/// </summary>
		/// <param name="lut">The image LUT </param>
		/// <returns>Image after applying LUT and transformation</returns>
		Image RenderImage(ILUT lut);
#endif
		/// <summary>
		/// Render the image and return the result as <seealso cref="BitmapSource"/>
		/// </summary>
		/// <param name="lut">The image LUT </param>
		/// <returns>Image after applying LUT and transformation</returns>
		BitmapSource RenderImageSource(ILUT lut);
	}
}
