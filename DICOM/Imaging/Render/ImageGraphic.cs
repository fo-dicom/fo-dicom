using System;
using System.Collections.Generic;
#if !SILVERLIGHT
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
#endif
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;

using Dicom.Imaging.Algorithms;
using Dicom.Imaging.LUT;
using Dicom.Imaging.Render;
using Dicom.IO;

namespace Dicom.Imaging.Render {
	/// <summary>
	/// The Image Graphic implementation of <seealso cref="IGraphic"/>
	/// </summary>
	public class ImageGraphic : IGraphic {
		#region Protected Members
		protected IPixelData _originalData;
		protected IPixelData _scaledData;

		protected PinnedIntArray _pixels;
#if SILVERLIGHT
		protected WriteableBitmap _bitmap;
#else
	    private const int DPI = 96;
		protected BitmapSource _bitmapSource;
		protected Bitmap _bitmap;
#endif

		protected double _scaleFactor;
		protected int _rotation;
		protected bool _flipX;
		protected bool _flipY;
		protected int _offsetX;
		protected int _offsetY;

		protected int _zorder;
		protected bool _applyLut;

		protected List<OverlayGraphic> _overlays;
		#endregion

		#region Public Properties
		/// <summary>
		/// Number of pixel componenets (samples)
		/// </summary>
		public int Components {
			get { return OriginalData.Components; }
		}

		/// <summary>
		/// Original pixel data
		/// </summary>
		public IPixelData OriginalData {
			get { return _originalData; }
		}

		public int OriginalWidth {
			get { return _originalData.Width; }
		}
		public int OriginalHeight {
			get { return _originalData.Height; }
		}
		public int OriginalOffsetX {
			get { return _offsetX; }
		}

		public int OriginalOffsetY {
			get { return _offsetY; }
		}

		public double ScaleFactor {
			get { return _scaleFactor; }
		}

		/// <summary>
		/// Scaled pixel data
		/// </summary>
		public IPixelData ScaledData {
			get {
				if (_scaledData == null) {
					if (Math.Abs(_scaleFactor - 1.0) <= Double.Epsilon)
						_scaledData = _originalData;
					else
						_scaledData = OriginalData.Rescale(_scaleFactor);
				}
				return _scaledData;
			}
		}
		public int ScaledWidth {
			get { return ScaledData.Width; }
		}
		public int ScaledHeight {
			get { return ScaledData.Height; }
		}
		public int ScaledOffsetX {
			get { return (int)(_offsetX * _scaleFactor); }
		}

		public int ScaledOffsetY {
			get { return (int)(_offsetY * _scaleFactor); }
		}

		public int ZOrder {
			get { return _zorder; }
			set { _zorder = value; }
		}
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialize new instance of <seealso cref="ImageGraphic"/>
		/// </summary>
		/// <param name="pixelData">Pixel data</param>
		public ImageGraphic(IPixelData pixelData) : this() {
			_originalData = pixelData;
			Scale(1.0);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		protected ImageGraphic() {
			_zorder = 255;
			_applyLut = true;
			_overlays = new List<OverlayGraphic>();
		}
		#endregion

		#region Public Members
		/// <summary>
		/// Add overlay to render over the resulted image 
		/// </summary>
		/// <param name="overlay">Overlay graphic </param>
		public void AddOverlay(OverlayGraphic overlay) {
			_overlays.Add(overlay);
			overlay.Scale(_scaleFactor);
		}

		public void Reset() {
			Scale(1.0);
			_rotation = 0;
			_flipX = false;
			_flipY = false;
		}

		public void Scale(double scale) {
			if (Math.Abs(scale - _scaleFactor) <= Double.Epsilon)
				return;

			_scaleFactor = scale;
			if (_bitmap != null) {
				_scaledData = null;
				_pixels.Dispose();
				_pixels = null;
				_bitmap = null;
			}

			foreach (var overlay in _overlays) {
				overlay.Scale(scale);
			}
		}

		public void BestFit(int width, int height) {
			double xF = (double)width / (double)OriginalWidth;
			double yF = (double)height / (double)OriginalHeight;
			Scale(Math.Min(xF, yF));
		}

		public void Rotate(int angle) {
			if (angle > 0) {
				if (angle <= 90)
					_rotation += 90;
				else if (angle <= 180)
					_rotation += 180;
				else if (angle <= 270)
					_rotation += 270;
			} else if (angle < 0) {
				if (angle >= -90)
					_rotation -= 90;
				else if (angle >= -180)
					_rotation -= 180;
				else if (angle >= -270)
					_rotation -= 270;
			}
			if (angle != 0) {
				if (_rotation >= 360)
					_rotation -= 360;
				else if (_rotation < 0)
					_rotation += 360;
			}
		}

		public void FlipX() {
			_flipX = !_flipX;
		}

		public void FlipY() {
			_flipY = !_flipY;
		}

		public void Transform(double scale, int rotation, bool flipx, bool flipy) {
			Scale(scale);
			Rotate(rotation);
			_flipX = flipx;
			_flipY = flipy;
		}

#if SILVERLIGHT
		public BitmapSource RenderImageSource(ILUT lut)
		{
			bool render = false;

			if (_bitmap == null) {
				_pixels = new PinnedIntArray(ScaledData.Width * ScaledData.Height);
				_bitmap = new WriteableBitmap(ScaledData.Width, ScaledData.Height);
				render = true;
			}

			if (_applyLut && lut != null && !lut.IsValid) {
				lut.Recalculate();
				render = true;
			}

			if (render) {
				ScaledData.Render((_applyLut ? lut : null), _pixels.Data);

				foreach (var overlay in _overlays) {
					overlay.Render(_pixels.Data, ScaledData.Width, ScaledData.Height);
				}
			}

			MultiThread.For(0, _pixels.Count, delegate(int i) { _bitmap.Pixels[i] = _pixels.Data[i]; });

			_bitmap.Rotate(_rotation);

			if (_flipX) _bitmap.Flip(WriteableBitmapExtensions.FlipMode.Horizontal);
			if (_flipY) _bitmap.Flip(WriteableBitmapExtensions.FlipMode.Vertical);

			_bitmap.Invalidate();

			return _bitmap;
		}
#else
		public BitmapSource RenderImageSource(ILUT lut) {
			bool render = false;

			if (_applyLut && lut != null && !lut.IsValid) {
				lut.Recalculate();
				render = true;
			}

			if (_bitmapSource == null || render) {
				_pixels = new PinnedIntArray(ScaledData.Width * ScaledData.Height);

				ScaledData.Render((_applyLut ? lut : null), _pixels.Data);

				foreach (var overlay in _overlays) {
					overlay.Render(_pixels.Data, ScaledData.Width, ScaledData.Height);
				}

				_bitmapSource = RenderBitmapSource(ScaledData.Width, ScaledData.Height, _pixels.Data);
			}

			if (_rotation != 0 || _flipX || _flipY) {
				TransformGroup rotFlipTransform = new TransformGroup();
				rotFlipTransform.Children.Add(new RotateTransform(_rotation));
				rotFlipTransform.Children.Add(new ScaleTransform(_flipX ? -1 : 1, _flipY ? -1 : 1));
				_bitmapSource = new TransformedBitmap(_bitmapSource, rotFlipTransform);
			}

			return _bitmapSource;
		}

		private static BitmapSource RenderBitmapSource(int iWidth, int iHeight, int[] iPixelData) {
			var bitmap = new WriteableBitmap(iWidth, iHeight, DPI, DPI, PixelFormats.Bgr32, null);

			// Reserve the back buffer for updates.
			bitmap.Lock();

			Marshal.Copy(iPixelData, 0, bitmap.BackBuffer, iPixelData.Length);

			// Specify the area of the bitmap that changed.
			bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)bitmap.Width, (int)bitmap.Height));

			// Release the back buffer and make it available for display.
			bitmap.Unlock();

			return bitmap;
		}

		public Image RenderImage(ILUT lut) {
			bool render = false;

			if (_bitmap == null) {
				System.Drawing.Imaging.PixelFormat format = Components == 4 
					? System.Drawing.Imaging.PixelFormat.Format32bppArgb 
					: System.Drawing.Imaging.PixelFormat.Format32bppRgb;
				_pixels = new PinnedIntArray(ScaledData.Width * ScaledData.Height);
				_bitmap = new Bitmap(ScaledData.Width, ScaledData.Height, ScaledData.Width * 4, format, _pixels.Pointer);
				render = true;
			}

			if (_applyLut && lut != null && !lut.IsValid) {
				lut.Recalculate();
				render = true;
			}

			_bitmap.RotateFlip(RotateFlipType.RotateNoneFlipNone);

			if (render) {
				ScaledData.Render((_applyLut ? lut : null), _pixels.Data);

				foreach (var overlay in _overlays) {
					overlay.Render(_pixels.Data, ScaledData.Width, ScaledData.Height);
				}
			}

			_bitmap.RotateFlip(GetRotateFlipType());

			return _bitmap;
		}

		protected RotateFlipType GetRotateFlipType() {
			if (_flipX && _flipY) {
				switch (_rotation) {
				case  90: return RotateFlipType.Rotate90FlipXY;
				case 180: return RotateFlipType.Rotate180FlipXY;
				case 270: return RotateFlipType.Rotate270FlipXY;
				default: return RotateFlipType.RotateNoneFlipXY;
				}
			} else if (_flipX) {
				switch (_rotation) {
				case  90: return RotateFlipType.Rotate90FlipX;
				case 180: return RotateFlipType.Rotate180FlipX;
				case 270: return RotateFlipType.Rotate270FlipX;
				default: return RotateFlipType.RotateNoneFlipX;
				}
			} else if (_flipY) {
				switch (_rotation) {
				case  90: return RotateFlipType.Rotate90FlipY;
				case 180: return RotateFlipType.Rotate180FlipY;
				case 270: return RotateFlipType.Rotate270FlipY;
				default: return RotateFlipType.RotateNoneFlipY;
				}
			} else {
				switch (_rotation) {
				case  90: return RotateFlipType.Rotate90FlipNone;
				case 180: return RotateFlipType.Rotate180FlipNone;
				case 270: return RotateFlipType.Rotate270FlipNone;
				default: return RotateFlipType.RotateNoneFlipNone;
				}
			}
		}
#endif
		#endregion
	}
}
