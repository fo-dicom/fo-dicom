using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

using Dicom;
using Dicom.IO.Buffer;

using Dicom.Imaging.Algorithms;
using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render {
	public interface IPixelData {
		int Width { get; }
		int Height { get; }
		int Components { get; }
		IPixelData Rescale(double scale);
		void Render(ILUT lut, int[] output);
	}

	public static class PixelDataFactory {
		public static IPixelData Create(DicomPixelData pixelData, int frame) {
			PhotometricInterpretation pi = pixelData.PhotometricInterpretation;
			if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2 || pi == PhotometricInterpretation.PaletteColor) {
				if (pixelData.BitsStored <= 8)
					return new GrayscalePixelDataU8(pixelData.Width, pixelData.Height, pixelData.GetFrame(frame));
				else if (pixelData.BitsStored <= 16) {
					if (pixelData.PixelRepresentation == PixelRepresentation.Signed)
						return new GrayscalePixelDataS16(pixelData.Width, pixelData.Height, pixelData.BitDepth, pixelData.GetFrame(frame));
					else
						return new GrayscalePixelDataU16(pixelData.Width, pixelData.Height, pixelData.BitDepth, pixelData.GetFrame(frame));
				} else
					throw new DicomImagingException("Unsupported pixel data value for bits stored: {0}", pixelData.BitsStored);
			} else if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull) {
				var buffer = pixelData.GetFrame(frame);
				if (pixelData.PlanarConfiguration == PlanarConfiguration.Planar)
					buffer = PixelDataConverter.PlanarToInterleaved24(buffer);
				return new ColorPixelData24(pixelData.Width, pixelData.Height, buffer);
			} else {
				throw new DicomImagingException("Unsupported pixel data photometric interpretation: {0}", pi.Value);
			}
		}

		public static SingleBitPixelData Create(DicomOverlayData overlayData) {
			return new SingleBitPixelData(overlayData.Columns, overlayData.Rows, overlayData.Data);
		}
	}

	public class GrayscalePixelDataU8 : IPixelData {
		#region Private Members
		int _width;
		int _height;
		byte[] _data;
		#endregion

		#region Public Constructor
		public GrayscalePixelDataU8(int width, int height, IByteBuffer data) {
			_width = width;
			_height = height;
			_data = data.Data;
		}

		private GrayscalePixelDataU8(int width, int height, byte[] data) {
			_width = width;
			_height = height;
			_data = data;
		}
		#endregion

		#region Public Properties
		public int Width {
			get { return _width; }
		}

		public int Height {
			get { return _height; }
		}

		public int Components {
			get { return 1; }
		}

		public byte[] Data {
			get { return _data; }
		}
		#endregion

		#region Public Methods
		public IPixelData Rescale(double scale) {
			if (scale == 1.0)
				return this;
			int w = (int)(Width * scale);
			int h = (int)(Height * scale);
			byte[] data = BilinearInterpolation.RescaleGrayscale(_data, Width, Height, w, h);
			return new GrayscalePixelDataU8(w, h, data);
		}

		public void Render(ILUT lut, int[] output) {
			if (lut == null) {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width; i < e; i++) {
						output[i] = _data[i];
					}
				});
			} else {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width; i < e; i++) {
						output[i] = lut[_data[i]];
					}
				});
			}
		}
		#endregion
	}

	public class SingleBitPixelData : GrayscalePixelDataU8 {
		#region Public Constructor
		public SingleBitPixelData(int width, int height, IByteBuffer data) : base(width, height, new MemoryByteBuffer(ExpandBits(width, height, data.Data))) {
		}
		#endregion

		#region Static Methods
		private const byte One = 1;
		private const byte Zero = 0;

		private static byte[] ExpandBits(int width, int height, byte[] input) {
			BitArray bits = new BitArray(input);
			byte[] output = new byte[width * height];
			for (int i = 0, l = width * height; i < l; i++) {
				output[i] = bits[i] ? One : Zero;
			}
			return output;
		}
		#endregion
	}

	public class GrayscalePixelDataS16 : IPixelData {
		#region Private Members
		int _width;
		int _height;
		short[] _data;
		#endregion

		#region Public Constructor
		public GrayscalePixelDataS16(int width, int height, BitDepth bitDepth, IByteBuffer data) {
			_width = width;
			_height = height;
			_data = ByteBufferEnumerator<short>.Create(data).ToArray();

			if (bitDepth.BitsStored != 16) {
				int sign = 1 << bitDepth.HighBit;
				int mask = sign - 1;

				Parallel.For(0, _data.Length, (int i) => {
					short d = _data[i];
					if ((d & sign) != 0)
						_data[i] = (short)-(d & mask);
					else
						_data[i] = (short)(d & mask);
				});
			}
		}

		private GrayscalePixelDataS16(int width, int height, short[] data) {
			_width = width;
			_height = height;
			_data = data;
		}
		#endregion

		#region Public Properties
		public int Width {
			get { return _width; }
		}

		public int Height {
			get { return _height; }
		}

		public int Components {
			get { return 1; }
		}

		public short[] Data {
			get { return _data; }
		}
		#endregion

		#region Public Methods
		public IPixelData Rescale(double scale) {
			if (scale == 1.0)
				return this;
			int w = (int)(Width * scale);
			int h = (int)(Height * scale);
			short[] data = BilinearInterpolation.RescaleGrayscale(_data, Width, Height, w, h);
			return new GrayscalePixelDataS16(w, h, data);
		}

		public void Render(ILUT lut, int[] output) {
			if (lut == null) {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width; i < e; i++) {
						output[i] = _data[i];
					}
				});
			} else {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width; i < e; i++) {
						output[i] = lut[_data[i]];
					}
				});
			}
		}
		#endregion
	}

	public class GrayscalePixelDataU16 : IPixelData {
		#region Private Members
		int _width;
		int _height;
		ushort[] _data;
		#endregion

		#region Public Constructor
		public GrayscalePixelDataU16(int width, int height, BitDepth bitDepth, IByteBuffer data) {
			_width = width;
			_height = height;
			_data = ByteBufferEnumerator<ushort>.Create(data).ToArray();

			if (bitDepth.BitsStored != 16) {
				int mask = (1 << (bitDepth.HighBit + 1)) - 1;

				Parallel.For(0, _data.Length, (int i) => {
					_data[i] = (ushort)(_data[i] & mask);
				});
			}
		}

		private GrayscalePixelDataU16(int width, int height, ushort[] data) {
			_width = width;
			_height = height;
			_data = data;
		}
		#endregion

		#region Public Properties
		public int Width {
			get { return _width; }
		}

		public int Height {
			get { return _height; }
		}

		public int Components {
			get { return 1; }
		}

		public ushort[] Data {
			get { return _data; }
		}
		#endregion

		#region Public Methods
		public IPixelData Rescale(double scale) {
			if (scale == 1.0)
				return this;
			int w = (int)(Width * scale);
			int h = (int)(Height * scale);
			ushort[] data = BilinearInterpolation.RescaleGrayscale(_data, Width, Height, w, h);
			return new GrayscalePixelDataU16(w, h, data);
		}

		public void Render(ILUT lut, int[] output) {
			if (lut == null) {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width; i < e; i++) {
						output[i] = _data[i];
					}
				});
			} else {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width; i < e; i++) {
						output[i] = lut[_data[i]];
					}
				});
			}
		}
		#endregion
	}

	public class ColorPixelData24 : IPixelData {
		#region Private Members
		int _width;
		int _height;
		byte[] _data;
		#endregion

		#region Public Constructor
		public ColorPixelData24(int width, int height, IByteBuffer data) {
			_width = width;
			_height = height;
			_data = data.Data;
		}

		private ColorPixelData24(int width, int height, byte[] data) {
			_width = width;
			_height = height;
			_data = data;
		}
		#endregion

		#region Public Properties
		public int Width {
			get { return _width; }
		}

		public int Height {
			get { return _height; }
		}

		public int Components {
			get { return 3; }
		}

		public byte[] Data {
			get { return _data; }
		}
		#endregion

		#region Public Methods
		public IPixelData Rescale(double scale) {
			if (scale == 1.0)
				return this;
			int w = (int)(Width * scale);
			int h = (int)(Height * scale);
			byte[] data = BilinearInterpolation.RescaleColor24(_data, Width, Height, w, h);
			return new ColorPixelData24(w, h, data);
		}

		public void Render(ILUT lut, int[] output) {
			if (lut == null) {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width, p = i * 3; i < e; i++) {
						output[i] = (_data[p++] << 16) | (_data[p++] << 8) | _data[p++];
					}
				});
			} else {
				Parallel.For(0, Height, y => {
					for (int i = Width * y, e = i + Width, p = i * 3; i < e; i++) {
						output[i] = (lut[_data[p++]] << 16) | (lut[_data[p++]] << 8) | lut[_data[p++]];
					}
				});
			}
		}
		#endregion
	}
}
