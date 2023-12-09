// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Algorithms;
using FellowOakDicom.Imaging.LUT;
using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// Pixel data interface implemented by various pixel format classes
    /// </summary>
    public interface IPixelData
    {
        /// <summary>
        /// Gets image width (columns) in pixels.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets image height (rows) in pixels.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets number of pixel components (normally 1 for grayscale, 1 for palette, and 3 for RGB and YBR).
        /// </summary>
        int Components { get; }

        /// <summary>
        /// Return the minimum and maximum pixel values from pixel data.
        /// The padding value is taken into account.
        /// </summary>
        /// <param name="padding">Padding value to ignore in min-max determination.</param>
        /// <returns>Range of calculated minimum and maximum values.</returns>
        DicomRange<double> GetMinMax(int padding);

        /// <summary>
        /// Return the minimum and maximum pixel values from pixel data.
        /// </summary>
        /// <returns>Range of calculated minimum and maximum values.</returns>
        DicomRange<double> GetMinMax();

        /// <summary>
        /// Gets the value of the pixel at the specified coordinates.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>Pixel value</returns>
        double GetPixel(int x, int y);

        /// <summary>
        /// Gets a rescaled copy of the pixel data.
        /// </summary>
        /// <param name="scale">Copy scale.</param>
        /// <returns>Rescaled copy of the pixel data.</returns>
        IPixelData Rescale(double scale);

        /// <summary>
        /// Render the pixel data after applying <paramref name="lut"/> to the output array (allocated by user)
        /// </summary>
        /// <param name="lut">Lookup table to render the pixels into output pixels</param>
        /// <param name="output">The output array to store the result in</param>
        void Render(ILUT lut, int[] output);

        /// <summary>
        /// Gets a histogram of the pixel data for a given <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The channel for which the histogram is requested.</param>
        /// <returns>Histogram of the pixel data for the given <paramref name="channel"/>.</returns>
        Histogram GetHistogram(int channel);
    }

    /// <summary>
    /// Pixel data factory to create <see cref="IPixelData"/> and <see cref="SingleBitPixelData"/> from 
    /// <see cref="DicomPixelData"/>
    /// </summary>
    public static class PixelDataFactory
    {
        /// <summary>
        /// Create <see cref="IPixelData"/> form <see cref="DicomPixelData"/> 
        /// according to the input <paramref name="pixelData"/> <see cref="PhotometricInterpretation"/>
        /// </summary>
        /// <param name="pixelData">Input pixel data</param>
        /// <param name="frame">Frame number (0 based)</param>
        /// <returns>Implementation of <see cref="IPixelData"/> according to <see cref="PhotometricInterpretation"/></returns>
        public static IPixelData Create(DicomPixelData pixelData, int frame)
        {
            PhotometricInterpretation pi = pixelData.PhotometricInterpretation;

            if (pi == null)
            {
                // generally ACR-NEMA
                var samples = pixelData.SamplesPerPixel;
                if (samples == 0 || samples == 1)
                {
                    pi = pixelData.Dataset.Contains(DicomTag.RedPaletteColorLookupTableData)
                        ? PhotometricInterpretation.PaletteColor
                        : PhotometricInterpretation.Monochrome2;
                }
                else
                {
                    // assume, probably incorrectly, that the image is RGB
                    pi = PhotometricInterpretation.Rgb;
                }
            }

            if (pixelData.BitsStored == 1)
            {
                if (pixelData.Dataset.GetSingleValue<DicomUID>(DicomTag.SOPClassUID)
                    == DicomUID.MultiFrameSingleBitSecondaryCaptureImageStorage)
                {
                    // Multi-frame Single Bit Secondary Capture is stored LSB -> MSB
                    return new SingleBitPixelData(
                        pixelData.Width,
                        pixelData.Height,
                        PixelDataConverter.ReverseBits(pixelData.GetFrame(frame)));
                }
                else
                {
                    // Need sample images to verify that this is correct
                    return new SingleBitPixelData(pixelData.Width, pixelData.Height, pixelData.GetFrame(frame));
                }
            }
            else if (pi == PhotometricInterpretation.Monochrome1 || pi == PhotometricInterpretation.Monochrome2
                     || pi == PhotometricInterpretation.PaletteColor)
            {
                if (pixelData.BitsAllocated == 8 && pixelData.HighBit == 7 && pixelData.BitsStored == 8)
                {
                    return new GrayscalePixelDataU8(pixelData.Width, pixelData.Height, pixelData.GetFrame(frame));
                }
                else if (pixelData.BitsAllocated <= 16)
                {
                    return pixelData.PixelRepresentation == PixelRepresentation.Signed
                        ? new GrayscalePixelDataS16(
                            pixelData.Width,
                            pixelData.Height,
                            pixelData.BitDepth,
                            pixelData.GetFrame(frame))
                        : (IPixelData)new GrayscalePixelDataU16(
                            pixelData.Width,
                            pixelData.Height,
                            pixelData.BitDepth,
                            pixelData.GetFrame(frame));
                }
                else if (pixelData.BitsAllocated <= 32)
                {
                    return pixelData.PixelRepresentation == PixelRepresentation.Signed
                        ? new GrayscalePixelDataS32(
                            pixelData.Width,
                            pixelData.Height,
                            pixelData.BitDepth,
                            pixelData.GetFrame(frame))
                        : (IPixelData)new GrayscalePixelDataU32(
                            pixelData.Width,
                            pixelData.Height,
                            pixelData.BitDepth,
                            pixelData.GetFrame(frame));
                }
                else
                {
                    throw new DicomImagingException($"Unsupported pixel data value for bits stored: {pixelData.BitsStored}");
                }
            }
            else if (pi == PhotometricInterpretation.Rgb || pi == PhotometricInterpretation.YbrFull
                     || pi == PhotometricInterpretation.YbrFull422 || pi == PhotometricInterpretation.YbrPartial422)
            {
                var buffer = pixelData.GetFrame(frame);

                if (pixelData.PlanarConfiguration == PlanarConfiguration.Planar)
                {
                    buffer = PixelDataConverter.PlanarToInterleaved24(buffer);
                }

                if (pi == PhotometricInterpretation.YbrFull)
                {
                    buffer = PixelDataConverter.YbrFullToRgb(buffer);
                }
                else if (pi == PhotometricInterpretation.YbrFull422)
                {
                    // Fix issue#1049: check for planar configuration in case of PhotometricInterpretation.YbrFull422 was never done
                    if (pixelData.PlanarConfiguration == PlanarConfiguration.Planar)
                    {
                        throw new DicomImagingException("Unsupported planar configuration for YBR_FULL_422");
                    }
                    buffer = PixelDataConverter.YbrFull422ToRgb(buffer, pixelData.Width);
                }
                else if (pi == PhotometricInterpretation.YbrPartial422)
                {
                    buffer = PixelDataConverter.YbrPartial422ToRgb(buffer, pixelData.Width);
                }

                return new ColorPixelData24(pixelData.Width, pixelData.Height, buffer);
            }
            else
            {
                throw new DicomImagingException($"Unsupported pixel data photometric interpretation: {pi.Value}");
            }
        }

        /// <summary>
        /// Create <see cref="SingleBitPixelData"/> form <see cref="DicomOverlayData"/> 
        /// according to the input <paramref name="overlayData"/>
        /// </summary>
        /// <param name="overlayData">The input overlay data</param>
        /// <returns>The result overlay stored in <see cref="SingleBitPixelData"/></returns>
        public static SingleBitPixelData Create(DicomOverlayData overlayData)
        {
            return new SingleBitPixelData(overlayData.Columns, overlayData.Rows, overlayData.Data);
        }
    }

    /// <summary>
    /// Grayscale unsigned 8 bits <see cref="IPixelData"/> implementation
    /// </summary>
    public class GrayscalePixelDataU8 : IPixelData
    {
        #region Private Members

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU8"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Byte buffer of data.</param>
        public GrayscalePixelDataU8(int width, int height, IByteBuffer data)
        {
            Width = width;
            Height = height;
            Data = data.Data;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU8"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Data byte array.</param>
        protected internal GrayscalePixelDataU8(int width, int height, byte[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }


        #endregion

        #region Public Properties

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public int Height { get; }

        /// <inheritdoc />
        public int Components => 1;

        /// <summary>
        /// Gets pixel data in internal format.
        /// </summary>
        public byte[] Data { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public DicomRange<double> GetMinMax(int padding)
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Where(v => v != padding).Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public DicomRange<double> GetMinMax()
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public double GetPixel(int x, int y)
        {
            var data = Data;
            return data[y * Width + x];
        }

        /// <inheritdoc />
        public virtual IPixelData Rescale(double scale)
        {
            var w = (int)(Width * scale);
            var h = (int)(Height * scale);
            if (w == Width && h == Height) return this;

            var data = BilinearInterpolation.RescaleGrayscale(Data, Width, Height, w, h);
            return new GrayscalePixelDataU8(w, h, data);
        }

        /// <inheritdoc />
        public void Render(ILUT lut, int[] output)
        {
            var data = Data;
            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = data[i];
                    }
                }
                );
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = (int)lut[data[i]];
                    }
                }
                );
            }
        }

        /// <inheritdoc />
        public virtual Histogram GetHistogram(int channel)
        {
            if (channel != 0) throw new ArgumentOutOfRangeException(nameof(channel), channel, "Expected channel 0 for grayscale image.");

            var histogram = new Histogram(byte.MinValue, byte.MaxValue);

            var data = Data;
            for (var i = 0; i < data.Length; i++) histogram.Add(data[i]);

            return histogram;
        }

        #endregion
    }

    /// <summary>
    /// Single bit pixel <see cref="IPixelData"/> implementation(for binary pixels) usually used for overlay pixel data
    /// </summary>
    public class SingleBitPixelData : GrayscalePixelDataU8
    {
        #region Public Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="SingleBitPixelData"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Byte data buffer.</param>
        public SingleBitPixelData(int width, int height, IByteBuffer data)
            : base(width, height, ExpandBits(width, height, data.Data))
        {
        }

        #endregion

        #region Static Methods

        private const byte One = 1;

        private const byte Zero = 0;

        private static byte[] ExpandBits(int width, int height, byte[] input)
        {
            var bits = new BitArray(input);
            var output = new byte[width * height];
            for (int i = 0, l = width * height; i < l; i++)
            {
                output[i] = bits[i] ? One : Zero;
            }
            return output;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override IPixelData Rescale(double scale)
        {
            var w = (int)(Width * scale);
            var h = (int)(Height * scale);
            if (w == Width && h == Height) return this;

            var data = NearestNeighborInterpolation.RescaleGrayscale(Data, Width, Height, w, h);
            return new GrayscalePixelDataU8(w, h, data);
        }

        /// <inheritdoc />
        public override Histogram GetHistogram(int channel)
        {
            if (channel != 0) throw new ArgumentOutOfRangeException(nameof(channel), channel, "Expected channel 0 for grayscale image.");

            var histogram = new Histogram(0, 1);

            var data = Data;
            for (var i = 0; i < data.Length; i++) histogram.Add(data[i]);

            return histogram;
        }

        #endregion
    }

    /// <summary>
    /// Grayscale signed 16 bits <see cref="IPixelData"/> implementation
    /// </summary>
    public class GrayscalePixelDataS16 : IPixelData
    {
        #region Private Members

        private readonly BitDepth _bits;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataS16"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="bitDepth">Bit depth of pixel data.</param>
        /// <param name="data">Byte data buffer.</param>
        public GrayscalePixelDataS16(int width, int height, BitDepth bitDepth, IByteBuffer data)
        {
            _bits = bitDepth;
            Width = width;
            Height = height;

            var shortData = ByteConverter.ToArray<short>(data, bitDepth.BitsAllocated);

            if (bitDepth.BitsStored != 16)
            {
                // Normally, HighBit == BitsStored-1, and thus shiftLeft == shiftRight, and the two
                // shifts in the loop below just replaces the top shift bits by the sign bit.
                // Separating shiftLeft from shiftRight handles exotic cases where low-order bits
                // should also be discarded.
                int shiftLeft = bitDepth.BitsAllocated - bitDepth.HighBit - 1;
                int shiftRight = bitDepth.BitsAllocated - bitDepth.BitsStored;
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        // Remove masked high and low bits by shifting them out of the data type,
                        // getting the sign correct using arithmetic (sign-extending) right shift.
                        var d = (short)(shortData[i] << shiftLeft);
                        shortData[i] = (short)(d >> shiftRight);
                    }
                }
                );
            }

            Data = shortData;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU32"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Pixel data in internal data format.</param>
        private GrayscalePixelDataS16(int width, int height, short[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public int Height { get; }

        /// <inheritdoc />
        public int Components => 1;

        /// <summary>
        /// Gets pixel data in internal format.
        /// </summary>
        public short[] Data { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public DicomRange<double> GetMinMax(int padding)
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Where(v => v != padding).Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public DicomRange<double> GetMinMax()
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public double GetPixel(int x, int y)
        {
            var data = Data;
            return data[y * Width + x];
        }

        /// <inheritdoc />
        public IPixelData Rescale(double scale)
        {
            var w = (int)(Width * scale);
            var h = (int)(Height * scale);
            if (w == Width && h == Height) return this;

            var data = BilinearInterpolation.RescaleGrayscale(Data, Width, Height, w, h);
            return new GrayscalePixelDataS16(w, h, data);
        }

        /// <inheritdoc />
        public void Render(ILUT lut, int[] output)
        {
            var data = Data;
            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = data[i];
                    }
                }
                );
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = (int)lut[data[i]];
                    }
                }
                );
            }
        }

        /// <inheritdoc />
        public Histogram GetHistogram(int channel)
        {
            if (channel != 0) throw new ArgumentOutOfRangeException(nameof(channel), channel, "Expected channel 0 for grayscale image.");

            var histogram = new Histogram(_bits.MinimumValue, _bits.MaximumValue);

            var data = Data;

            for (var i = 0; i < data.Length; i++) histogram.Add(data[i]);

            return histogram;
        }

        #endregion
    }

    /// <summary>
    /// Grayscale unsigned 16 bits <see cref="IPixelData"/> implementation
    /// </summary>
    public class GrayscalePixelDataU16 : IPixelData
    {
        #region Private Members

        private readonly BitDepth _bits;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU16"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="bitDepth">Bit depth of pixel data.</param>
        /// <param name="data">Byte data buffer.</param>
        public GrayscalePixelDataU16(int width, int height, BitDepth bitDepth, IByteBuffer data)
        {
            _bits = bitDepth;
            Width = width;
            Height = height;

            var ushortData = ByteConverter.ToArray<ushort>(data, bitDepth.BitsAllocated);

            if (bitDepth.BitsStored != 16)
            {
                // Normally, HighBit == BitsStored-1, and thus shiftLeft == shiftRight, and the two
                // shifts in the loop below just zeroes the top shift bits.
                // Separating shiftLeft from shiftRight handles exotic cases where low-order bits
                // should also be discarded.
                int shiftLeft = bitDepth.BitsAllocated - bitDepth.HighBit - 1;
                int shiftRight = bitDepth.BitsAllocated - bitDepth.BitsStored;

                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        // Remove masked high and low bits by shifting them out of the data type. 
                        var d = (ushort)(ushortData[i] << shiftLeft);
                        ushortData[i] = (ushort)(d >> shiftRight);
                    }
                }
                );
            }

            Data = ushortData;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU32"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Pixel data in internal data format.</param>
        private GrayscalePixelDataU16(int width, int height, ushort[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public int Height { get; }

        /// <inheritdoc />
        public int Components => 1;

        /// <summary>
        /// Gets pixel data in internal format.
        /// </summary>
        public ushort[] Data { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public DicomRange<double> GetMinMax(int padding)
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Where(v => v != padding).Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public DicomRange<double> GetMinMax()
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public double GetPixel(int x, int y)
        {
            var data = Data;
            return data[y * Width + x];
        }

        /// <inheritdoc />
        public IPixelData Rescale(double scale)
        {
            var w = (int)(Width * scale);
            var h = (int)(Height * scale);
            if (w == Width && h == Height) return this;

            var data = BilinearInterpolation.RescaleGrayscale(Data, Width, Height, w, h);
            return new GrayscalePixelDataU16(w, h, data);
        }

        /// <inheritdoc />
        public void Render(ILUT lut, int[] output)
        {
            var data = Data;
            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = data[i];
                    }
                }
                );
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = (int)lut[data[i]];
                    }
                }
                );
            }
        }

        /// <inheritdoc />
        public Histogram GetHistogram(int channel)
        {
            if (channel != 0) throw new ArgumentOutOfRangeException(nameof(channel), channel, "Expected channel 0 for grayscale image.");

            var histogram = new Histogram(_bits.MinimumValue, _bits.MaximumValue);

            var data = Data;
            for (var i = 0; i < data.Length; i++) histogram.Add(data[i]);

            return histogram;
        }

        #endregion
    }

    /// <summary>
    /// Grayscale signed 32 bits <see cref="IPixelData"/> implementation
    /// </summary>
    public class GrayscalePixelDataS32 : IPixelData
    {
        #region Private Members

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataS32"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="bitDepth">Bit depth of pixel data.</param>
        /// <param name="data">Byte data buffer.</param>
        public GrayscalePixelDataS32(int width, int height, BitDepth bitDepth, IByteBuffer data)
        {
            Width = width;
            Height = height;

            var intData = ByteConverter.ToArray<int>(data, bitDepth.BitsAllocated);

            // Normally, HighBit == BitsStored-1, and thus shiftLeft == shiftRight, and the two
            // shifts in the loop below just replaces the top shift bits by the sign bit.
            // Separating shiftLeft from shiftRight handles exotic cases where low-order bits
            // should also be discarded.
            int shiftLeft = bitDepth.BitsAllocated - bitDepth.HighBit - 1;
            int shiftRight = bitDepth.BitsAllocated - bitDepth.BitsStored;
            Parallel.For(0, Height, y =>
            {
                for (int i = Width * y, e = i + Width; i < e; i++)
                {
                    // Remove masked high and low bits by shifting them out of the data type,
                    // getting the sign correct using arithmetic (sign-extending) right shift.
                    var d = intData[i] << shiftLeft;
                    intData[i] = d >> shiftRight;
                }
            }
            );

            Data = intData;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU32"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Pixel data in internal data format.</param>
        private GrayscalePixelDataS32(int width, int height, int[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public int Height { get; }

        /// <inheritdoc />
        public int Components => 1;

        /// <summary>
        /// Gets pixel data in internal format.
        /// </summary>
        public int[] Data { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public DicomRange<double> GetMinMax(int padding)
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Where(v => v != padding).Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public DicomRange<double> GetMinMax()
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public double GetPixel(int x, int y)
        {
            return Data[y * Width + x];
        }

        /// <inheritdoc />
        public IPixelData Rescale(double scale)
        {
            var w = (int)(Width * scale);
            var h = (int)(Height * scale);
            if (w == Width && h == Height) return this;

            var data = BilinearInterpolation.RescaleGrayscale(Data, Width, Height, w, h);
            return new GrayscalePixelDataS32(w, h, data);
        }

        /// <inheritdoc />
        public void Render(ILUT lut, int[] output)
        {
            var data = Data;

            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = data[i];
                    }
                }
                );
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = (int)lut[data[i]];
                    }
                }
                );
            }
        }

        /// <inheritdoc />
        public Histogram GetHistogram(int channel)
        {
            throw new NotSupportedException("Histograms are not supported for signed 32-bit images.");
        }

        #endregion
    }

    /// <summary>
    /// Grayscale unsigned 32 bits <see cref="IPixelData"/> implementation
    /// </summary>
    public class GrayscalePixelDataU32 : IPixelData
    {
        #region Private Members

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU32"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="bitDepth">Bit depth of pixel data.</param>
        /// <param name="data">Byte data buffer.</param>
        public GrayscalePixelDataU32(int width, int height, BitDepth bitDepth, IByteBuffer data)
        {
            Width = width;
            Height = height;

            var uintData = ByteConverter.ToArray<uint>(data, bitDepth.BitsAllocated);

            if (bitDepth.BitsStored != 32)
            {
                // Normally, HighBit == BitsStored-1, and thus shiftLeft == shiftRight, and the two
                // shifts in the loop below just zeroes the top shift bits.
                // Separating shiftLeft from shiftRight handles exotic cases where low-order bits
                // should also be discarded.
                int shiftLeft = bitDepth.BitsAllocated - bitDepth.HighBit - 1;
                int shiftRight = bitDepth.BitsAllocated - bitDepth.BitsStored;

                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        // Remove masked high and low bits by shifting them out of the data type. 
                        var d = uintData[i] << shiftLeft;
                        uintData[i] = d >> shiftRight;
                    }
                }
                );
            }

            Data = uintData;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="GrayscalePixelDataU32"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Pixel data in internal data format.</param>
        private GrayscalePixelDataU32(int width, int height, uint[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public int Height { get; }

        /// <inheritdoc />
        public int Components => 1;

        /// <summary>
        /// Gets pixel data in internal format.
        /// </summary>
        public uint[] Data { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public DicomRange<double> GetMinMax(int padding)
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Where(v => v != padding).Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public DicomRange<double> GetMinMax()
        {
            if (Data == null || Data.Length == 0)
            {
                return default(DicomRange<double>);
            }

            var range = new DicomRange<double>(double.MaxValue, double.MinValue);
            Data.Each(v => range.Join(v));
            return range;
        }

        /// <inheritdoc />
        public double GetPixel(int x, int y)
        {
            return Data[y * Width + x];
        }

        /// <inheritdoc />
        public IPixelData Rescale(double scale)
        {
            var w = (int)(Width * scale);
            var h = (int)(Height * scale);
            if (w == Width && h == Height) return this;

            var data = BilinearInterpolation.RescaleGrayscale(Data, Width, Height, w, h);
            return new GrayscalePixelDataU32(w, h, data);
        }

        /// <inheritdoc />
        public void Render(ILUT lut, int[] output)
        {
            var data = Data;
            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = (int)data[i];
                    }
                }
                );
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width; i < e; i++)
                    {
                        output[i] = unchecked((int)lut[(int)data[i]]);
                    }
                }
                );
            }
        }

        /// <inheritdoc />
        public Histogram GetHistogram(int channel)
        {
            throw new NotSupportedException("Histograms are not supported for unsigned 32-bit images.");
        }

        #endregion
    }

    /// <summary>
    /// Color 24 bits <see cref="IPixelData"/> implementation used for RGB
    /// </summary>
    public class ColorPixelData24 : IPixelData
    {
        #region Private Members

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initializes an instance of the <see cref="ColorPixelData24"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Byte data buffer.</param>
        public ColorPixelData24(int width, int height, IByteBuffer data)
        {
            Width = width;
            Height = height;
            Data = data.Data;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ColorPixelData24"/> class.
        /// </summary>
        /// <param name="width">Pixel data width.</param>
        /// <param name="height">Pixel data height.</param>
        /// <param name="data">Pixel data in internal data format.</param>
        private ColorPixelData24(int width, int height, byte[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public int Height { get; }

        /// <inheritdoc />
        public int Components => 3;

        /// <summary>
        /// Gets pixel data in byte array format.
        /// </summary>
        public byte[] Data { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public DicomRange<double> GetMinMax(int padding)
        {
            throw new InvalidOperationException(
                "Calculation of min/max pixel values is not supported for 24-bit color pixel data.");
        }

        /// <inheritdoc />
        public DicomRange<double> GetMinMax()
        {
            throw new InvalidOperationException(
                "Calculation of min/max pixel values is not supported for 24-bit color pixel data.");
        }

        /// <inheritdoc />
        public double GetPixel(int x, int y)
        {
            var data = Data;
            var p = (y * Width + x) * 3;
            return (data[p++] << 16) | (data[p++] << 8) | data[p];
        }

        /// <inheritdoc />
        public IPixelData Rescale(double scale)
        {
            var w = (int)(Width * scale);
            var h = (int)(Height * scale);
            if (w == Width && h == Height) return this;

            var data = BilinearInterpolation.RescaleColor24(Data, Width, Height, w, h);
            return new ColorPixelData24(w, h, data);
        }

        /// <inheritdoc />
        public void Render(ILUT lut, int[] output)
        {
            const int alphaFF = 0xff << 24;

            var data = Data;
            if (lut == null)
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width, p = i * 3; i < e; i++)
                    {
                        output[i] = alphaFF | (data[p++] << 16) | (data[p++] << 8) | data[p++];
                    }
                }
                );
            }
            else
            {
                Parallel.For(0, Height, y =>
                {
                    for (int i = Width * y, e = i + Width, p = i * 3; i < e; i++)
                    {
                        output[i] = alphaFF | ((int)lut[data[p++]] << 16) | ((int)lut[data[p++]] << 8) | (int)lut[data[p++]];
                    }
                }
                );
            }
        }

        /// <inheritdoc />
        public Histogram GetHistogram(int channel)
        {
            if (channel < 0 || channel > 2)
                throw new ArgumentOutOfRangeException(
                    nameof(channel),
                    channel,
                    "Expected channel between 0 and 2 for 24-bit color image.");

            var histogram = new Histogram(byte.MinValue, byte.MaxValue);

            var data = Data;
            for (var i = channel; i < data.Length; i += 3) histogram.Add(data[i]);

            return histogram;
        }

        #endregion
    }
}
