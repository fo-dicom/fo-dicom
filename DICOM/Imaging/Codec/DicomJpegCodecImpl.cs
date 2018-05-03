// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    using System;
    using System.IO;
    using System.Linq;

    using BitMiracle.LibJpeg;

    using Dicom.Imaging;
    using Dicom.IO.Buffer;

    internal static class DicomJpegCodecImpl
    {
        internal static void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpegParams parameters)
        {
            if ((oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422)
                || (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial420))
            {
                throw new InvalidOperationException(
                    "Photometric Interpretation '" + oldPixelData.PhotometricInterpretation
                    + "' not supported by JPEG encoder");
            }

            var w = oldPixelData.Width;
            var h = oldPixelData.Height;

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var frameData = oldPixelData.GetFrame(frame);

                var nc = oldPixelData.SamplesPerPixel;
                var sgnd = oldPixelData.PixelRepresentation == PixelRepresentation.Signed;

                var rows = Enumerable.Range(0, h).Select(i => new short[w, nc]).ToArray();

                for (var c = 0; c < nc; c++)
                {
                    var pos = oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? (c * w * h) : c;
                    var offset = oldPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? 1 : nc;

                    if (oldPixelData.BytesAllocated == 1)
                    {
                        var data = frameData.Data;
                        if (sgnd)
                        {
                            if (oldPixelData.BitsStored < 8)
                            {
                                var sign = (byte)(1 << oldPixelData.HighBit);
                                var mask = (byte)(0xff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        var pixel = (sbyte)data[pos];
                                        rows[y][x, c] = (short)((pixel & sign) > 0 ? -(((-pixel) & mask) + 1) : pixel);
                                        pos += offset;
                                    }
                                }
                            }
                            else
                            {
                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        rows[y][x, c] = (sbyte)data[pos];
                                        pos += offset;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (var y = 0; y < h; ++y)
                            {
                                for (var x = 0; x < w; ++x)
                                {
                                    rows[y][x, c] = data[pos];
                                    pos += offset;
                                }
                            }
                        }
                    }
#if SUPPORT16BIT
                    else if (oldPixelData.BytesAllocated == 2)
                    {
                        if (sgnd)
                        {
                            if (oldPixelData.BitsStored < 16)
                            {
                                var frameData16 = new ushort[w * h];
                                Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                                var sign = (ushort)(1 << oldPixelData.HighBit);
                                var mask = (ushort)(0xffff >> (oldPixelData.BitsAllocated - oldPixelData.BitsStored));
                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        var pixel = frameData16[pos];
                                        rows[y][x, c] = (short)((pixel & sign) > 0 ? -(((-pixel) & mask) + 1) : pixel);
                                        pos += offset;
                                    }
                                }
                            }
                            else
                            {
                                var frameData16 = new short[w * h];
                                Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                                for (var y = 0; y < h; ++y)
                                {
                                    for (var x = 0; x < w; ++x)
                                    {
                                        rows[y][x, c] = frameData16[pos];
                                        pos += offset;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var frameData16 = new ushort[w * h];
                            Buffer.BlockCopy(frameData.Data, 0, frameData16, 0, (int)frameData.Size);

                            for (var y = 0; y < h; ++y)
                            {
                                for (var x = 0; x < w; ++x)
                                {
                                    rows[y][x, c] = (short)frameData16[pos];
                                    pos += offset;
                                }
                            }
                        }
                    }
#endif
                    else
                    {
                        throw new InvalidOperationException(
                            $"JPEG codec does not support Bits Allocated == {oldPixelData.BitsAllocated}");
                    }
                }

                try
                {
                    using (var stream = new MemoryStream())
                    {
                        var sampleRows =
                            rows.Select(
                                row =>
                                new SampleRow(
                                    ToRawRow(row, oldPixelData.BitsAllocated),
                                    w,
                                    (byte)oldPixelData.BitsStored,
                                    (byte)nc)).ToArray();
                        var colorSpace = GetColorSpace(oldPixelData.PhotometricInterpretation);
                        using (var encoder = new JpegImage(sampleRows, colorSpace))
                        {
                            encoder.WriteJpeg(stream, ToCompressionParameters(parameters));
                        }
                        newPixelData.AddFrame(new MemoryByteBuffer(stream.ToArray()));
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to JPEG encode image", e);
                }
            }

            if (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.Rgb)
            {
                newPixelData.PlanarConfiguration = PlanarConfiguration.Interleaved;
            }
        }

        internal static void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpegParams parameters)
        {
            var pixelCount = oldPixelData.Height * oldPixelData.Width;

            if (parameters.ConvertColorspaceToRGB)
            {
                if (oldPixelData.PixelRepresentation == PixelRepresentation.Signed)
                    throw new DicomCodecException(
                        "JPEG codec unable to perform colorspace conversion on signed pixel data");

                newPixelData.PhotometricInterpretation = PhotometricInterpretation.Rgb;
                newPixelData.PlanarConfiguration = PlanarConfiguration.Interleaved;
            }

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var jpegData = oldPixelData.GetFrame(frame);

                // Destination frame should be of even length
                var frameSize = newPixelData.UncompressedFrameSize;
                if ((frameSize & 1) == 1) ++frameSize;
                var destArray = new byte[frameSize];

                using (var stream = new MemoryStream(jpegData.Data))
                {
                    var decoder = new JpegImage(stream);
                    var w = decoder.Width;
                    var h = decoder.Height;

                    for (var c = 0; c < decoder.ComponentsPerSample; c++)
                    {
                        var pos = newPixelData.PlanarConfiguration == PlanarConfiguration.Planar ? c * pixelCount : c;
                        var offset = newPixelData.PlanarConfiguration == PlanarConfiguration.Planar
                                         ? 1
                                         : decoder.ComponentsPerSample;

                        if (newPixelData.BytesAllocated == 1)
                        {
                            for (var y = 0; y < h; ++y)
                            {
                                var row = decoder.GetRow(y);
                                for (var x = 0; x < w; ++x)
                                {
                                    destArray[pos] = (byte)row[x][c];
                                    pos += offset;
                                }
                            }
                        }
#if SUPPORT16BIT
                        else if (newPixelData.BytesAllocated == 2)
                        {
                            var destArray16 = new short[frameSize >> 1];
                            for (var y = 0; y < h; ++y)
                            {
                                var row = decoder.GetRow(y);
                                for (var x = 0; x < w; ++x)
                                {
                                    destArray16[pos] = row[x][c];
                                    pos += offset;
                                }
                            }

                            Buffer.BlockCopy(destArray16, 0, destArray, 0, frameSize);
                        }
#endif
                        else
                        {
                            throw new InvalidOperationException(
                                $"JPEG module does not support Bits Allocated == {newPixelData.BitsAllocated}!");
                        }
                    }

                    newPixelData.AddFrame(new MemoryByteBuffer(destArray));
                }
            }
        }

        private static Colorspace GetColorSpace(PhotometricInterpretation photometricInterpretation)
        {
            if (photometricInterpretation == PhotometricInterpretation.Rgb)
            {
                return Colorspace.RGB;
            }
            else if (photometricInterpretation == PhotometricInterpretation.Monochrome1
                     || photometricInterpretation == PhotometricInterpretation.Monochrome2)
            {
                return Colorspace.Grayscale;
            }
            else if (photometricInterpretation == PhotometricInterpretation.PaletteColor)
            {
                return Colorspace.Grayscale;
            }
            else if (photometricInterpretation == PhotometricInterpretation.YbrFull
                     || photometricInterpretation == PhotometricInterpretation.YbrFull422
                     || photometricInterpretation == PhotometricInterpretation.YbrPartial422)
            {
                return Colorspace.YCbCr;
            }

            return Colorspace.Unknown;
        }

        private static byte[] ToRawRow(short[,] row, int bitsAllocated)
        {
            byte[] bytes;
            switch (bitsAllocated)
            {
                case 8:
                    bytes = new byte[row.Length];
                    for (int x = 0, idx = 0; x < row.GetLength(0); ++x)
                    {
                        for (var c = 0; c < row.GetLength(1); ++c, ++idx)
                        {
                            bytes[idx] = (byte)row[x, c];
                        }
                    }
                    return bytes;
#if SUPPORT16BIT
                case 16:
                    bytes = new byte[2 * row.Length];
                    for (int x = 0, idx = 0; x < row.GetLength(0); ++x)
                    {
                        for (var c = 0; c < row.GetLength(1); ++c, ++idx)
                        {
                            bytes[idx] = (byte)row[x, c];
                        }
                    }
                    return bytes;
#endif
                default:
                    throw new InvalidOperationException($"JPEG codec does not support Bits Allocated == {bitsAllocated}");
            }
        }

        private static CompressionParameters ToCompressionParameters(DicomJpegParams parameters)
        {
            var compressionParams = new CompressionParameters();

            if (parameters != null)
            {
                compressionParams.Quality = parameters.Quality;
                compressionParams.SmoothingFactor = parameters.SmoothingFactor;
            }

            return compressionParams;
        }
    }
}
