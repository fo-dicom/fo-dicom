﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Algorithms
{
#if !NET35
    using System.Threading.Tasks;
#endif

    public static class BilinearInterpolation
    {
        public static byte[] RescaleGrayscale(
            byte[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            byte[] output = new byte[outputWidth * outputHeight];

            double xF = (double)inputWidth / (double)outputWidth;
            double yF = (double)inputHeight / (double)outputHeight;

            int xMax = inputWidth - 1;
            int yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    double oy0 = y * yF;
                    int oy1 = (int)oy0; // rounds down
                    int oy2 = (oy1 == yMax) ? oy1 : oy1 + 1;

                    double dy1 = oy0 - oy1;
                    double dy2 = 1.0 - dy1;

                    int yo0 = outputWidth * y;
                    int yo1 = inputWidth * oy1;
                    int yo2 = inputWidth * oy2;

                    double ox0, dx1, dx2;
                    int ox1, ox2;

                    for (int x = 0; x < outputWidth; x++)
                    {
                        ox0 = x * xF;
                        ox1 = (int)ox0;
                        ox2 = (ox1 == xMax) ? ox1 : ox1 + 1;

                        dx1 = ox0 - ox1;
                        dx2 = 1.0 - dx1;

                        output[yo0 + x] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                    }
                }
#if !NET35
                );
#endif
            }

            return output;
        }

        public static short[] RescaleGrayscale(
            short[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            short[] output = new short[outputWidth * outputHeight];

            double xF = (double)inputWidth / (double)outputWidth;
            double yF = (double)inputHeight / (double)outputHeight;

            int xMax = inputWidth - 1;
            int yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    double oy0 = y * yF;
                    int oy1 = (int)oy0; // rounds down
                    int oy2 = (oy1 == yMax) ? oy1 : oy1 + 1;

                    double dy1 = oy0 - oy1;
                    double dy2 = 1.0 - dy1;

                    int yo0 = outputWidth * y;
                    int yo1 = inputWidth * oy1;
                    int yo2 = inputWidth * oy2;

                    double ox0, dx1, dx2;
                    int ox1, ox2;

                    for (int x = 0; x < outputWidth; x++)
                    {
                        ox0 = x * xF;
                        ox1 = (int)ox0;
                        ox2 = (ox1 == xMax) ? ox1 : ox1 + 1;

                        dx1 = ox0 - ox1;
                        dx2 = 1.0 - dx1;

                        output[yo0 + x] =
                            (short)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                    }
                }
#if !NET35
                );
#endif
            }

            return output;
        }

        public static ushort[] RescaleGrayscale(
            ushort[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            ushort[] output = new ushort[outputWidth * outputHeight];

            double xF = (double)inputWidth / (double)outputWidth;
            double yF = (double)inputHeight / (double)outputHeight;

            int xMax = inputWidth - 1;
            int yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    double oy0 = y * yF;
                    int oy1 = (int)oy0; // rounds down
                    int oy2 = (oy1 == yMax) ? oy1 : oy1 + 1;

                    double dy1 = oy0 - oy1;
                    double dy2 = 1.0 - dy1;

                    int yo0 = outputWidth * y;
                    int yo1 = inputWidth * oy1;
                    int yo2 = inputWidth * oy2;

                    double ox0, dx1, dx2;
                    int ox1, ox2;

                    for (int x = 0; x < outputWidth; x++)
                    {
                        ox0 = x * xF;
                        ox1 = (int)ox0;
                        ox2 = (ox1 == xMax) ? ox1 : ox1 + 1;

                        dx1 = ox0 - ox1;
                        dx2 = 1.0 - dx1;

                        output[yo0 + x] =
                            (ushort)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                    }
                }
#if !NET35
                );
#endif
            }

            return output;
        }

        public static int[] RescaleGrayscale(
            int[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            int[] output = new int[outputWidth * outputHeight];

            double xF = (double)inputWidth / (double)outputWidth;
            double yF = (double)inputHeight / (double)outputHeight;

            int xMax = inputWidth - 1;
            int yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    double oy0 = y * yF;
                    int oy1 = (int)oy0; // rounds down
                    int oy2 = (oy1 == yMax) ? oy1 : oy1 + 1;

                    double dy1 = oy0 - oy1;
                    double dy2 = 1.0 - dy1;

                    int yo0 = outputWidth * y;
                    int yo1 = inputWidth * oy1;
                    int yo2 = inputWidth * oy2;

                    double ox0, dx1, dx2;
                    int ox1, ox2;

                    for (int x = 0; x < outputWidth; x++)
                    {
                        ox0 = x * xF;
                        ox1 = (int)ox0;
                        ox2 = (ox1 == xMax) ? ox1 : ox1 + 1;

                        dx1 = ox0 - ox1;
                        dx2 = 1.0 - dx1;

                        output[yo0 + x] =
                            (int)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                    }
                }
#if !NET35
                );
#endif
            }

            return output;
        }

        public static uint[] RescaleGrayscale(
            uint[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            uint[] output = new uint[outputWidth * outputHeight];

            double xF = (double)inputWidth / (double)outputWidth;
            double yF = (double)inputHeight / (double)outputHeight;

            int xMax = inputWidth - 1;
            int yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    double oy0 = y * yF;
                    int oy1 = (int)oy0; // rounds down
                    int oy2 = (oy1 == yMax) ? oy1 : oy1 + 1;

                    double dy1 = oy0 - oy1;
                    double dy2 = 1.0 - dy1;

                    int yo0 = outputWidth * y;
                    int yo1 = inputWidth * oy1;
                    int yo2 = inputWidth * oy2;

                    double ox0, dx1, dx2;
                    int ox1, ox2;

                    for (int x = 0; x < outputWidth; x++)
                    {
                        ox0 = x * xF;
                        ox1 = (int)ox0;
                        ox2 = (ox1 == xMax) ? ox1 : ox1 + 1;

                        dx1 = ox0 - ox1;
                        dx2 = 1.0 - dx1;

                        output[yo0 + x] =
                            (uint)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                    }
                }
#if !NET35
                );
#endif
            }

            return output;
        }

        public static byte[] RescaleColor24(
            byte[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            byte[] output = new byte[outputWidth * outputHeight * 3];

            double xF = (double)inputWidth / (double)outputWidth;
            double yF = (double)inputHeight / (double)outputHeight;

            int xMax = inputWidth - 1;
            int yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    double oy0 = y * yF;
                    int oy1 = (int)oy0; // rounds down
                    int oy2 = (oy1 == yMax) ? oy1 : oy1 + 1;

                    double dy1 = oy0 - oy1;
                    double dy2 = 1.0 - dy1;

                    int yo0 = outputWidth * y * 3;
                    int yo1 = inputWidth * oy1 * 3;
                    int yo2 = inputWidth * oy2 * 3;

                    double ox0, dx1, dx2;
                    int ox1, ox2;

                    for (int x = 0, px = 0; x < outputWidth; x++)
                    {
                        ox0 = x * xF;
                        ox1 = (int)ox0;
                        ox2 = (ox1 == xMax) ? ox1 : ox1 + 1;

                        dx1 = ox0 - ox1;
                        dx2 = 1.0 - dx1;

                        int x1 = ox1 * 3; //first byte of first pixel
                        int x2 = ox2 * 3; //first byte of second pixel

                        output[yo0 + px] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + x1]) + (dx1 * input[yo1 + x2])))
                                + (dy1 * ((dx2 * input[yo2 + x1]) + (dx1 * input[yo2 + x2]))));
                        px++;
                        x1++;
                        x2++;

                        output[yo0 + px] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + x1]) + (dx1 * input[yo1 + x2])))
                                + (dy1 * ((dx2 * input[yo2 + x1]) + (dx1 * input[yo2 + x2]))));
                        px++;
                        x1++;
                        x2++;

                        output[yo0 + px] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + x1]) + (dx1 * input[yo1 + x2])))
                                + (dy1 * ((dx2 * input[yo2 + x1]) + (dx1 * input[yo2 + x2]))));
                        px++;
                        x1++;
                        x2++;
                    }
                }
#if !NET35
                );
#endif
            }

            return output;
        }

        public static byte[] RescaleColor32(
            byte[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            byte[] output = new byte[outputWidth * outputHeight * 4];

            double xF = (double)inputWidth / (double)outputWidth;
            double yF = (double)inputHeight / (double)outputHeight;

            int xMax = inputWidth - 1;
            int yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    double oy0 = y * yF;
                    int oy1 = (int)oy0; // rounds down
                    int oy2 = (oy1 == yMax) ? oy1 : oy1 + 1;

                    double dy1 = oy0 - oy1;
                    double dy2 = 1.0 - dy1;

                    int yo0 = outputWidth * y * 4;
                    int yo1 = inputWidth * oy1 * 4;
                    int yo2 = inputWidth * oy2 * 4;

                    double ox0, dx1, dx2;
                    int ox1, ox2;

                    for (int x = 0, px = 0; x < outputWidth; x++)
                    {
                        ox0 = x * xF;
                        ox1 = (int)ox0;
                        ox2 = (ox1 == xMax) ? ox1 : ox1 + 1;

                        dx1 = ox0 - ox1;
                        dx2 = 1.0 - dx1;

                        output[yo0 + px] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                        px++;
                        ox1++;
                        ox2++;

                        output[yo0 + px] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                        px++;
                        ox1++;
                        ox2++;

                        output[yo0 + px] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                        px++;
                        ox1++;
                        ox2++;

                        output[yo0 + px] =
                            (byte)
                            ((dy2 * ((dx2 * input[yo1 + ox1]) + (dx1 * input[yo1 + ox2])))
                                + (dy1 * ((dx2 * input[yo2 + ox1]) + (dx1 * input[yo2 + ox2]))));
                        px++;
                        ox1++;
                        ox2++;
                    }
                }
#if !NET35
                );
#endif
            }

            return output;
        }
    }
}
