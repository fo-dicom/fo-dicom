using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

using Dicom;
using Dicom.IO.Buffer;

using Dicom.Imaging.Algorithms;
using Dicom.Imaging.LUT;
using Dicom.Imaging.Mathematics;

namespace Dicom.Imaging.Render {
	/// <summary>
	/// Pixel data interface implemented by various pixel format classes
	/// </summary>
	public interface IPixelData {
		/// <summary>
		/// Image width (columns) in pixels
		/// </summary>
		int Width { get; }

		/// <summary>
		/// Image height (rows) in pixels
		/// </summary>
		int Height { get; }

		/// <summary>
		/// Number for pixel comopnents (normally 1 for grayscale, 1 for palette, and 3 for RGB and YBR
		/// </summary>
		int Components { get; }

		/// <summary>
		/// return the minimum and maximum pixel values from pixel data
		/// </summary>
		/// <param name="padding"></param>
		/// <returns>Range of claculated minimum and max</returns>
		DicomRange<double> GetMinMax(int padding);

		/// <summary>
		/// Gets the value of the pixel at the specified coordinates.
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <returns>Pixel value</returns>
		double GetPixel(int x, int y);

		IPixelData Rescale(double scale);

		/// <summary>
		/// Render the pixel data after applying <paramref name="lut"/> to the output array (allocated by user)
		/// </summary>
		/// <param name="lut">Lookup table to render the pixels into output pixels</param>
		/// <param name="output">The output array to store the result in</param>
		void Render(ILUT lut, int[] output);

		Histogram GetHistogram(int channel);
	}

	/// <summary>
	/// Pixel data factory to create <seealso cref="IPixelData"/> and <seealso cref="SingleBitPixelData"/> from 
	/// <seealso cref="DicomPixelData"/>
	/// </summary>
	public static class PixelDataFactory {
		/// <summary>
		/// Create <see cref="IPixelData"/> form <see cref="DicomPixelData"/> 
		/// according to the input <paramref name="pixelData"/> <seealso cref="PhotometricInterpretation"/>
		/// </summary>
		/// <param name="pixelData">Input pixel data</param>
		/// <param name="frame">Frame number (0 based)</param>
		/// <returns>Implementation of <seealso cref="IPixelData"/> according to <seealso cref="PhotometricInterpretation"/></returns>
		public static IPixelData Create(DicomPixelData pixelData, int frame) {
			PhotometricInterpretation pi = pixelData.PhotometricInterpretation;

			if (pi == null) {
				// generally ACR-NEMA
				var samples = pixelData.SamplesPerPixel;
				if (samples == 0 || samples == 1) {
					if (pixelData.Dataset.Contains(DicomTag.RedPaletteColorLookupTableData))
						pi = PhotometricInterpretation.PaletteColor;
					else
						pi = PhotometricInterpretation.Monochrome2;
				} else {
					// assume, probably incorrectly, that the image is RGB
					pi = PhotometricInterpretation.Rgb;
				}
			}

			if (pixelData.BitsStored == 1) {
				if (pixelData.Dataset.Get<DicomUID>(DicomTag.SOPClassUID) == DicomUID.MultiFrameSingleBitSecondaryCaptureImageStorage)
					// Multi-frame Single Bit Secondary Capture is stored LSB -> MSB
					return new SingleBitPixelData(pixelData.Width, pixelData.Height, PixelDataConverter.ReverseBits(pixelData.GetFrame(frame)));
				else
					// Need sample images to verify that this is correct
					return new SingleBitPixelData(pixelData.Width, pixelData.Height, pixelData.GetFrame(frame));
			} else if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2 || pi == PhotometricInterpretation.PaletteColor) {
				if (pixelData.BitsAllocated <= 8)
					return new GrayscalePixelDataU8(pixelData.Width, pixelData.Height, pixelData.GetFrame(frame));
				else if (pixelData.BitsAllocated <= 16) {
					if (pixelData.PixelRepresentation == PixelRepresentation.Signed)
						return new GrayscalePixelDataS16(pixelData.Width, pixelData.Height, pixelData.BitDepth, pixelData.GetFrame(frame));
					else
						return new GrayscalePixelDataU16(pixelData.Width, pixelData.Height, pixelData.BitDepth, pixelData.GetFrame(frame));
				} else if (pixelData.BitsAllocated <= 32) {
                    if (pixelData.PixelRepresentation == PixelRepresentation.Signed)
						return new GrayscalePixelDataS32(pixelData.Width, pixelData.Height, pixelData.BitDepth, pixelData.GetFrame(frame));
                    else
						return new GrayscalePixelDataU32(pixelData.Width, pixelData.Height, pixelData.BitDepth, pixelData.GetFrame(frame));
				} else
					throw new DicomImagingException("Unsupported pixel data value for bits stored: {0}", pixelData.BitsStored);
			} else if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull || pi == PhotometricInterpretation.YbrFull422 || pi == PhotometricInterpretation.YbrPartial422) {
				var buffer = pixelData.GetFrame(frame);

				if (pixelData.PlanarConfiguration == PlanarConfiguration.Planar)
					buffer = PixelDataConverter.PlanarToInterleaved24(buffer);

				if (pi == PhotometricInterpretation.YbrFull)
					buffer = PixelDataConverter.YbrFullToRgb(buffer);
				else if (pi == PhotometricInterpretation.YbrFull422)
					buffer = PixelDataConverter.YbrFull422ToRgb(buffer);
				else if (pi == PhotometricInterpretation.YbrPartial422)
					buffer = PixelDataConverter.YbrPartial422ToRgb(buffer);

				return new ColorPixelData24(pixelData.Width, pixelData.Height, buffer);
			} else {
				throw new DicomImagingException("Unsupported pixel data photometric interpretation: {0}", pi.Value);
			}
		}

		/// <summary>
		/// Create <see cref="SingleBitPixelData"/> form <see cref="DicomOverlayData"/> 
		/// according to the input <paramref name="overlayData"/>
		/// </summary>
		/// <param name="overlayData">The input overlay data</param>
		/// <returns>The result overlay stored in <seealso cref="SingleBitPixelData"/></returns>
		public static SingleBitPixelData Create(DicomOverlayData overlayData) {
			return new SingleBitPixelData(overlayData.Columns, overlayData.Rows, overlayData.Data);
		}
	}

	/// <summary>
	/// Grayscale unsigned 8 bits <seealso cref="IPixelData"/> implementation
	/// </summary>
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

		public DicomRange<double> GetMinMax(int padding) {
			var min = Double.MaxValue;
			var max = Double.MinValue;

			for (int i = 0; i < _data.Length; i++) {
				if (_data[i] == padding)
					continue;
				else if (_data[i] > max)
					max = _data[i];
				else if (_data[i] < min)
					min = _data[i];
			}

			return new DicomRange<double>(min, max);
		}

		public double GetPixel(int x, int y) {
			return (double)_data[(y * Width) + x];
		}

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

		public virtual Histogram GetHistogram(int channel) {
			if (channel != 0)
				throw new ArgumentOutOfRangeException("channel", channel, "Expected channel 0 for grayscale image.");

			var histogram = new Histogram(Byte.MinValue, Byte.MaxValue);

			for (int i = 0; i < Data.Length; i++)
				histogram.Add(Data[i]);

			return histogram;
		}
		#endregion
	}

	/// <summary>
	/// Single bit pixel <seealso cref="IPixelData"/> implementation(for binary pixels) usually used for overlay pixel data
	/// </summary>
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

		#region Public Methods
		public override Histogram GetHistogram(int channel) {
			if (channel != 0)
				throw new ArgumentOutOfRangeException("channel", channel, "Expected channel 0 for grayscale image.");

			var histogram = new Histogram(0, 1);

			for (int i = 0; i < Data.Length; i++)
				histogram.Add(Data[i]);

			return histogram;
		}
		#endregion
	}

	/// <summary>
	/// Grayscale signed 16 bits <seealso cref="IPixelData"/> implementation
	/// </summary>
	public class GrayscalePixelDataS16 : IPixelData {
		#region Private Members
		BitDepth _bits;
		int _width;
		int _height;
		short[] _data;
		#endregion

		#region Public Constructor
		public GrayscalePixelDataS16(int width, int height, BitDepth bitDepth, IByteBuffer data) {
			_bits = bitDepth;
			_width = width;
			_height = height;
			_data = ByteBufferEnumerator<short>.Create(data).ToArray();

			if (bitDepth.BitsStored != 16) {
				int sign = 1 << bitDepth.HighBit;
				int mask = (UInt16.MaxValue >> (bitDepth.BitsAllocated - bitDepth.BitsStored));

				Parallel.For(0, _data.Length, (int i) => {
					short d = _data[i];
					if ((d & sign) != 0)
						_data[i] = (short)-(((-d) & mask) + 1);
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
		public DicomRange<double> GetMinMax(int padding) {
			var min = Double.MaxValue;
			var max = Double.MinValue;

			for (int i = 0; i < _data.Length; i++) {
				if (_data[i] == padding)
					continue;
				else if (_data[i] > max)
					max = _data[i];
				else if (_data[i] < min)
					min = _data[i];
			}

			return new DicomRange<double>(min, max);
		}

		public double GetPixel(int x, int y) {
			return (double)_data[(y * Width) + x];
		}

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

		public Histogram GetHistogram(int channel) {
			if (channel != 0)
				throw new ArgumentOutOfRangeException("channel", channel, "Expected channel 0 for grayscale image.");

			var histogram = new Histogram(_bits.MinimumValue, _bits.MaximumValue);

			for (int i = 0; i < Data.Length; i++)
				histogram.Add(Data[i]);

			return histogram;
		}
		#endregion
	}

	/// <summary>
	/// Grayscale unsigned 16 bits <seealso cref="IPixelData"/> implementation
	/// </summary>
	public class GrayscalePixelDataU16 : IPixelData {
		#region Private Members
		BitDepth _bits;
		int _width;
		int _height;
		ushort[] _data;
		#endregion

		#region Public Constructor
		public GrayscalePixelDataU16(int width, int height, BitDepth bitDepth, IByteBuffer data) {
			_bits = bitDepth;
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
		public DicomRange<double> GetMinMax(int padding) {
			var min = Double.MaxValue;
			var max = Double.MinValue;

			for (int i = 0; i < _data.Length; i++) {
				if (_data[i] == padding)
					continue;
				else if (_data[i] > max)
					max = _data[i];
				else if (_data[i] < min)
					min = _data[i];
			}

			return new DicomRange<double>(min, max);
		}

		public double GetPixel(int x, int y) {
			return (double)_data[(y * Width) + x];
		}

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

		public Histogram GetHistogram(int channel) {
			if (channel != 0)
				throw new ArgumentOutOfRangeException("channel", channel, "Expected channel 0 for grayscale image.");

			var histogram = new Histogram(_bits.MinimumValue, _bits.MaximumValue);

			for (int i = 0; i < Data.Length; i++)
				histogram.Add(Data[i]);

			return histogram;
		}
		#endregion
	}

	/// <summary>
	/// Grayscale signed 32 bits <seealso cref="IPixelData"/> implementation
	/// </summary>
    public class GrayscalePixelDataS32 : IPixelData {
        #region Private Members
        int _width;
        int _height;
        int[] _data;
        #endregion

        #region Public Constructor
        public GrayscalePixelDataS32(int width, int height, BitDepth bitDepth, IByteBuffer data) {
            _width = width;
            _height = height;
            _data = ByteBufferEnumerator<int>.Create(data).ToArray();

            int sign = 1 << bitDepth.HighBit;
			uint mask = (UInt32.MaxValue >> (bitDepth.BitsAllocated - bitDepth.BitsStored));

            Parallel.For(0, _data.Length, (int i) => {
                int d = _data[i];
                if ((d & sign) != 0)
					_data[i] = (int)-(((-d) & mask) + 1);
                else
                    _data[i] = (int)(d & mask);
            });
        }

        private GrayscalePixelDataS32(int width, int height, int[] data) {
            _width = width;
            _height = height;
            _data = data;
        }
        #endregion

        #region Public Properties
        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Components
        {
            get { return 1; }
        }

        public int[] Data
        {
            get { return _data; }
        }
        #endregion

        #region Public Methods
		public DicomRange<double> GetMinMax(int padding) {
			var min = Double.MaxValue;
			var max = Double.MinValue;

			for (int i = 0; i < _data.Length; i++) {
				if (_data[i] > max)
					max = _data[i];
				else if (_data[i] < min)
					min = _data[i];
			}

			return new DicomRange<double>(min, max);
		}

		public double GetPixel(int x, int y) {
			return (double)_data[(y * Width) + x];
		}

        public IPixelData Rescale(double scale)
        {
            if (scale == 1.0)
                return this;
            int w = (int)(Width * scale);
            int h = (int)(Height * scale);
            int[] data = BilinearInterpolation.RescaleGrayscale(_data, Width, Height, w, h);
            return new GrayscalePixelDataS32(w, h, data);
        }

        public void Render(ILUT lut, int[] output)
        {
            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = _data[i];
                    }
                });
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = lut[_data[i]];
                    }
                });
            }
        }

		public Histogram GetHistogram(int channel) {
			throw new NotSupportedException("Histograms are not supported for signed 32-bit images.");
		}
        #endregion
    }

	/// <summary>
	/// Grayscale unsgiend 32 bits <seealso cref="IPixelData"/> implementation
	/// </summary>
    public class GrayscalePixelDataU32 : IPixelData
    {
        #region Private Members
        int _width;
        int _height;
        uint[] _data;
        #endregion

        #region Public Constructor
        public GrayscalePixelDataU32(int width, int height, BitDepth bitDepth, IByteBuffer data)
        {
            _width = width;
            _height = height;
            _data = ByteBufferEnumerator<uint>.Create(data).ToArray();

            if (bitDepth.BitsStored != 32)
            {
                int mask = (1 << (bitDepth.HighBit + 1)) - 1;

                Parallel.For(0, _data.Length, (int i) =>
                {
                    _data[i] = (uint)(_data[i] & mask);
                });
            }
        }

        private GrayscalePixelDataU32(int width, int height, uint[] data)
        {
            _width = width;
            _height = height;
            _data = data;
        }
        #endregion

        #region Public Properties
        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Components
        {
            get { return 1; }
        }

        public uint[] Data
        {
            get { return _data; }
        }
        #endregion

        #region Public Methods
		public DicomRange<double> GetMinMax(int padding) {
			var min = Double.MaxValue;
			var max = Double.MinValue;

			for (int i = 0; i < _data.Length; i++) {
				if (_data[i] > max)
					max = _data[i];
				else if (_data[i] < min)
					min = _data[i];
			}

			return new DicomRange<double>(min, max);
		}

		public double GetPixel(int x, int y) {
			return (double)_data[(y * Width) + x];
		}

        public IPixelData Rescale(double scale)
        {
            if (scale == 1.0)
                return this;
            int w = (int)(Width * scale);
            int h = (int)(Height * scale);
            uint[] data = BilinearInterpolation.RescaleGrayscale(_data, Width, Height, w, h);
            return new GrayscalePixelDataU32(w, h, data);
        }

        public void Render(ILUT lut, int[] output)
        {
            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = (int)_data[i];
                    }
                });
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = lut[(int)_data[i]];
                    }
                });
            }
        }

		public Histogram GetHistogram(int channel) {
			throw new NotSupportedException("Histograms are not supported for unsigned 32-bit images.");
		}
        #endregion
    }

	/// <summary>
	/// Color 24 bits <seealso cref="IPixelData"/> implementation used for RGB
	/// </summary>
    public class ColorPixelData24 : IPixelData
    {
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
		public DicomRange<double> GetMinMax(int padding) {
			throw new InvalidOperationException("Calculation of min/max pixel values is not supported for 24-bit color pixel data.");
		}

		public double GetPixel(int x, int y) {
			var p = ((y * Width) + x) * 3;
			return (double)((_data[p++] << 16) | (_data[p++] << 8) | _data[p++]);
		}

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

		public Histogram GetHistogram(int channel) {
			if (channel < 0 || channel > 2)
				throw new ArgumentOutOfRangeException("channel", channel, "Expected channel between 0 and 2 for 24-bit color image.");

			var histogram = new Histogram(Byte.MinValue, Byte.MaxValue);

			for (int i = channel; i < Data.Length; i += 3)
				histogram.Add(Data[i]);

			return histogram;
		}
		#endregion
	}
}
