// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Runtime.CompilerServices;
using FellowOakDicom.IO.Buffer;


namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Convert pixels from presentation from interleaved to planar and from planar to interleaved
    /// </summary>
    public static class PixelDataConverter
    {
        /// <summary>
        /// Convert 24 bits pixels from interleaved (RGB) to planar (RRR...GGG...BBB...)
        /// </summary>
        /// <param name="data">Pixels data in interleaved format (RGB)</param>
        /// <returns>Pixels data in planar format (RRR...GGG...BBB...)</returns>
        public static IByteBuffer InterleavedToPlanar24(IByteBuffer data)
        {
            var oldPixels = data.Data;
            var newPixels = new byte[oldPixels.Length];
            var pixelCount = newPixels.Length / 3;

            unchecked
            {
                for (var n = 0; n < pixelCount; n++)
                {
                    newPixels[n + pixelCount * 0] = oldPixels[n * 3 + 0];
                    newPixels[n + pixelCount * 1] = oldPixels[n * 3 + 1];
                    newPixels[n + pixelCount * 2] = oldPixels[n * 3 + 2];
                }
            }

            return new MemoryByteBuffer(newPixels);
        }

        /// <summary>
        /// Convert 24 bits pixels from planar (RRR...GGG...BBB...) to interleaved (RGB)
        /// </summary>
        /// <param name="data">Pixels data in planar format (RRR...GGG...BBB...)</param>
        /// <returns>Pixels data in interleaved format (RGB)</returns>
        public static IByteBuffer PlanarToInterleaved24(IByteBuffer data)
        {
            var oldPixels = data.Data;
            var newPixels = new byte[oldPixels.Length];
            var pixelCount = newPixels.Length / 3;

            unchecked
            {
                for (var n = 0; n < pixelCount; n++)
                {
                    newPixels[n * 3 + 0] = oldPixels[n + pixelCount * 0];
                    newPixels[n * 3 + 1] = oldPixels[n + pixelCount * 1];
                    newPixels[n * 3 + 2] = oldPixels[n + pixelCount * 2];
                }
            }

            return new MemoryByteBuffer(newPixels);
        }

        /// <summary>
        /// Convert YBR_FULL photometric interpretation pixels to RGB.
        /// </summary>
        /// <param name="data">Array of YBR_FULL photometric interpretation pixels.</param>
        /// <returns>Array of pixel data in RGB photometric interpretation.</returns>
        public static IByteBuffer YbrFullToRgb(IByteBuffer data)
        {
            var oldPixels = data.Data;
            var newPixels = new byte[oldPixels.Length];

            unchecked
            {
                for (var n = 0; n < newPixels.Length; n += 3)
                {
                    int y = oldPixels[n + 0];
                    int b = oldPixels[n + 1];
                    int r = oldPixels[n + 2];

                    newPixels[n + 0] = ToByte(y + 1.4020 * (r - 128) + 0.5);
                    newPixels[n + 1] = ToByte(y - 0.3441 * (b - 128) - 0.7141 * (r - 128) + 0.5);
                    newPixels[n + 2] = ToByte(y + 1.7720 * (b - 128) + 0.5);
                }
            }

            return new MemoryByteBuffer(newPixels);
        }

        /// <summary>
        /// Convert YBR_FULL_422 photometric interpretation pixels to RGB.
        /// </summary>
        /// <param name="data">Array of YBR_FULL_422 photometric interpretation pixels.</param>
        /// <param name="width">Image width.</param>
        /// <returns>Array of pixel data in RGB photometric interpretation.</returns>
        public static IByteBuffer YbrFull422ToRgb(IByteBuffer data, int width)
        {
            var oldPixels = data.Data;
            var newPixels = new byte[oldPixels.Length / 4 * 2 * 3];

            unchecked
            {
                for (int n = 0, p = 0, col = 0; n < oldPixels.Length;)
                {
                    int y1 = oldPixels[n++];
                    int y2 = oldPixels[n++];
                    int cb = oldPixels[n++];
                    int cr = oldPixels[n++];

                    newPixels[p++] = ToByte(y1 + 1.4020 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(y1 - 0.3441 * (cb - 128) - 0.7141 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(y1 + 1.7720 * (cb - 128) + 0.5);

                    if (++col == width)
                    {
                        // Issue #471: for uneven width images (i.e. when col equals width after first of two pixels), 
                        // ignore last pixel in each row.
                        col = 0;
                        continue;
                    }

                    newPixels[p++] = ToByte(y2 + 1.4020 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(y2 - 0.3441 * (cb - 128) - 0.7141 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(y2 + 1.7720 * (cb - 128) + 0.5);

                    if (++col == width) col = 0;
                }
            }

            return new MemoryByteBuffer(newPixels);
        }

        /// <summary>
        /// Convert YBR_PARTIAL_422 photometric interpretation pixels to RGB.
        /// </summary>
        /// <param name="data">Array of YBR_PARTIAL_422 photometric interpretation pixels.</param>
        /// <param name="width">Image width.</param>
        /// <returns>Array of pixel data in RGB photometric interpretation.</returns>
        public static IByteBuffer YbrPartial422ToRgb(IByteBuffer data, int width)
        {
            var oldPixels = data.Data;
            var newPixels = new byte[oldPixels.Length / 4 * 2 * 3];

            unchecked
            {
                for (int n = 0, p = 0, col = 0; n < oldPixels.Length;)
                {
                    int y1 = oldPixels[n++];
                    int y2 = oldPixels[n++];
                    int cb = oldPixels[n++];
                    int cr = oldPixels[n++];

                    newPixels[p++] = ToByte(1.1644 * (y1 - 16) + 1.5960 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(1.1644 * (y1 - 16) - 0.3917 * (cb - 128) - 0.8130 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(1.1644 * (y1 - 16) + 2.0173 * (cb - 128) + 0.5);

                    if (++col == width)
                    {
                        // Issue #471: for uneven width images (i.e. when col equals width after first of two pixels), 
                        // ignore last pixel in each row.
                        col = 0;
                        continue;
                    }

                    newPixels[p++] = ToByte(1.1644 * (y2 - 16) + 1.5960 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(1.1644 * (y2 - 16) - 0.3917 * (cb - 128) - 0.8130 * (cr - 128) + 0.5);
                    newPixels[p++] = ToByte(1.1644 * (y2 - 16) + 2.0173 * (cb - 128) + 0.5);

                    if (++col == width) col = 0;
                }
            }

            return new MemoryByteBuffer(newPixels);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte ToByte(double x)
        {
            return (byte)(x < 0.0 ? 0.0 : x > 255.0 ? 255.0 : x);
        }

        private static readonly byte[] BitReverseTable =
            {
                0x00, 0x80, 0x40, 0xc0, 0x20, 0xa0, 0x60, 0xe0, 0x10, 0x90,
                0x50, 0xd0, 0x30, 0xb0, 0x70, 0xf0, 0x08, 0x88, 0x48, 0xc8,
                0x28, 0xa8, 0x68, 0xe8, 0x18, 0x98, 0x58, 0xd8, 0x38, 0xb8,
                0x78, 0xf8, 0x04, 0x84, 0x44, 0xc4, 0x24, 0xa4, 0x64, 0xe4,
                0x14, 0x94, 0x54, 0xd4, 0x34, 0xb4, 0x74, 0xf4, 0x0c, 0x8c,
                0x4c, 0xcc, 0x2c, 0xac, 0x6c, 0xec, 0x1c, 0x9c, 0x5c, 0xdc,
                0x3c, 0xbc, 0x7c, 0xfc, 0x02, 0x82, 0x42, 0xc2, 0x22, 0xa2,
                0x62, 0xe2, 0x12, 0x92, 0x52, 0xd2, 0x32, 0xb2, 0x72, 0xf2,
                0x0a, 0x8a, 0x4a, 0xca, 0x2a, 0xaa, 0x6a, 0xea, 0x1a, 0x9a,
                0x5a, 0xda, 0x3a, 0xba, 0x7a, 0xfa, 0x06, 0x86, 0x46, 0xc6,
                0x26, 0xa6, 0x66, 0xe6, 0x16, 0x96, 0x56, 0xd6, 0x36, 0xb6,
                0x76, 0xf6, 0x0e, 0x8e, 0x4e, 0xce, 0x2e, 0xae, 0x6e, 0xee,
                0x1e, 0x9e, 0x5e, 0xde, 0x3e, 0xbe, 0x7e, 0xfe, 0x01, 0x81,
                0x41, 0xc1, 0x21, 0xa1, 0x61, 0xe1, 0x11, 0x91, 0x51, 0xd1,
                0x31, 0xb1, 0x71, 0xf1, 0x09, 0x89, 0x49, 0xc9, 0x29, 0xa9,
                0x69, 0xe9, 0x19, 0x99, 0x59, 0xd9, 0x39, 0xb9, 0x79, 0xf9,
                0x05, 0x85, 0x45, 0xc5, 0x25, 0xa5, 0x65, 0xe5, 0x15, 0x95,
                0x55, 0xd5, 0x35, 0xb5, 0x75, 0xf5, 0x0d, 0x8d, 0x4d, 0xcd,
                0x2d, 0xad, 0x6d, 0xed, 0x1d, 0x9d, 0x5d, 0xdd, 0x3d, 0xbd,
                0x7d, 0xfd, 0x03, 0x83, 0x43, 0xc3, 0x23, 0xa3, 0x63, 0xe3,
                0x13, 0x93, 0x53, 0xd3, 0x33, 0xb3, 0x73, 0xf3, 0x0b, 0x8b,
                0x4b, 0xcb, 0x2b, 0xab, 0x6b, 0xeb, 0x1b, 0x9b, 0x5b, 0xdb,
                0x3b, 0xbb, 0x7b, 0xfb, 0x07, 0x87, 0x47, 0xc7, 0x27, 0xa7,
                0x67, 0xe7, 0x17, 0x97, 0x57, 0xd7, 0x37, 0xb7, 0x77, 0xf7,
                0x0f, 0x8f, 0x4f, 0xcf, 0x2f, 0xaf, 0x6f, 0xef, 0x1f, 0x9f,
                0x5f, 0xdf, 0x3f, 0xbf, 0x7f, 0xff
            };

        /// <summary>
        /// Reverses bits for each byte in buffer.
        /// </summary>
        /// <param name="data">Original data subject to reversal.</param>
        /// <returns>Buffer of reversed data.</returns>
        public static IByteBuffer ReverseBits(IByteBuffer data)
        {
            byte[] oldPixels = data.Data;
            byte[] newPixels = new byte[oldPixels.Length];

            unchecked
            {
                for (var n = 0; n < oldPixels.Length; n++)
                {
                    newPixels[n] = BitReverseTable[oldPixels[n]];
                }
            }

            return new MemoryByteBuffer(newPixels);
        }
    }
}
