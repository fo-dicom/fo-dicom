// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System.IO;

namespace FellowOakDicom.Imaging.Codec.JpegLossless
{

    /// <summary>
    /// This class provides the conversion of a byte buffer
    /// containing a JPEGLossless to an MemoryBuffer.
    /// Therefore it uses a.Net port of the rii-mango JPEGLosslessDecoder
    /// Library written in Java(https://github.com/rii-mango/JPEGLosslessDecoder )
    /// By Hermann Kroll
    ///
    /// Take care, that only the following lossless formats are supported
    /// 1.2.840.10008.1.2.4.57 JPEG Lossless, Nonhierarchical (Processes 14)
    /// 1.2.840.10008.1.2.4.70 JPEG Lossless, Nonhierarchical (Processes 14[Selection 1])
    ///
    /// Currently the following conversions are supported
    /// 	- 24Bit, RGB       -> BufferedImage.TYPE_INT_RGB
    ///  -  8Bit, Grayscale -> BufferedImage.TYPE_BYTE_GRAY
    ///  - 16Bit, Grayscale -> BufferedImage.TYPE_USHORT_GRAY
    ///  
    /// </summary>
    public abstract class DicomJpegLosslessDecoder : DicomJpegCodec
    {


        /**
         * Converts a byte buffer (containing a jpeg lossless) 
         * to an Java BufferedImage
         * Currently the following conversions are supported
         * 	- 24Bit, RGB       -> BufferedImage.TYPE_INT_RGB
         *  -  8Bit, Grayscale -> BufferedImage.TYPE_BYTE_GRAY
         *  - 16Bit, Grayscale -> BufferedImage.TYPE_USHORT_GRAY
         * 
         * @param data byte buffer which contains a jpeg lossless
         * @return if successfully a BufferedImage is returned
         * @throws IOException is thrown if the decoder failed or a conversion is not supported
         */
        /// <summary>
        /// Converts the ByteBuffer (containing a jpeg lossless) to an MemoryBuffer
        /// </summary>
        /// <param name="data">ByteBuffer which contains a jpeg lossess</param>
        /// <returns>if successfully a MemoryStream contained the codeded data is returned</returns>
        /// <exception cref="System.IO.IOException"></exception>
        private MemoryStream ReadImage(IByteBuffer data)
        {
            using var decoder = new DicomJpegLosslessDecoderImpl(data);
            int[][] decoded = decoder.Decode();
            int width = decoder.DimX;
            int height = decoder.DimY;

            if (decoder.NumComponents == 1)
            {
                switch (decoder.Precision)
                {
                    case int prec when prec <= 8:
                        return Read8Bit1ComponentGrayScale(decoded, width, height);
                    case int prec when (prec > 8 && prec <= 16):
                        return Read16Bit1ComponentGrayScale(decoded, width, height);
                    default:
                        throw new IOException("JPEG Lossless with " + decoder.Precision + " bit precision and 1 component cannot be decoded");
                }
            }
            //rgb
            if (decoder.NumComponents == 3)
            {
                switch (decoder.Precision)
                {
                    case 24:
                    case 8:
                        return Read24Bit3ComponentRGB(decoded, width, height);

                    default:
                        throw new IOException("JPEG Lossless with " + decoder.Precision + " bit precision and 3 components cannot be decoded");
                }
            }

            throw new IOException("JPEG Lossless with " + decoder.Precision + " bit precision and " + decoder.NumComponents + " component(s) cannot be decoded");
        }


        private MemoryStream Read16Bit1ComponentGrayScale(int[][] decoded, int width, int height)
        {
            var resultBuffer = new MemoryStream();
            var resultWriter = new BinaryWriter(resultBuffer);
            int i = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++, i++)
                {
                    resultWriter.Write((ushort)decoded[0][i]);
                }
            }
            resultBuffer.Seek(0, SeekOrigin.Begin);
            return resultBuffer;
        }


        private MemoryStream Read8Bit1ComponentGrayScale(int[][] decoded, int width, int height)
        {
            var resultBuffer = new MemoryStream();
            var resultWriter = new BinaryWriter(resultBuffer);
            int i = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++, i++)
                {
                    resultWriter.Write((byte)decoded[0][i]);
                }
            }
            resultBuffer.Seek(0, SeekOrigin.Begin);
            return resultBuffer;
        }


        private MemoryStream Read24Bit3ComponentRGB(int[][] decoded, int width, int height)
        {
            var resultBuffer = new MemoryStream();
            var resultWriter = new BinaryWriter(resultBuffer);
            int i = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++, i++)
                {
                    resultWriter.Write((byte)decoded[0][i]);
                    resultWriter.Write((byte)decoded[1][i]);
                    resultWriter.Write((byte)decoded[2][i]);
                }
            }
            resultBuffer.Seek(0, SeekOrigin.Begin);
            return resultBuffer;
        }


        public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
        {
            throw new DicomCodecException($"Encoding of transfersyntax {TransferSyntax} is not supported");
        }


        public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
        {
            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var encodedData = oldPixelData.GetFrame(frame);

                var decodedData = ReadImage(encodedData);

                newPixelData.AddFrame(new StreamByteBuffer(decodedData, 0, (uint)decodedData.Length));
            }
        }

    }
}
