using System;
using Xunit;

namespace Dicom.Imaging.Algorithms
{
    public class BilinearInterpolationTest
    {
        [Fact]
        public void RescaleGrayscale2x2_to_4x4()
        {
            byte[] input = { 0, 0, 10, 10 };

            const int inputWidth = 2,
                inputHeight = inputWidth;

            const int outputWidth = 4, outputHeight = 4;

            byte[] output = BilinearInterpolation.RescaleGrayscale(input, inputWidth, inputHeight, outputWidth, outputHeight);

            Assert.Equal(
                new byte[]
                {
                    0, 0, 0, 0,
                    5, 5, 5, 5,
                    10, 10, 10, 10,
                    10, 10, 10, 10
                },
                output);
        }

        [Fact]
        public void RescaleGrayscale3x3_to_5x5()
        {
            byte[] input =
            {
                0, 0, 0,
                5, 5, 5,
                10, 10, 10
            };

            const int inputWidth = 3,
                inputHeight = inputWidth;

            const int outputWidth = 5, outputHeight = 5;

            byte[] output = BilinearInterpolation.RescaleGrayscale(input, inputWidth, inputHeight, outputWidth, outputHeight);

            Assert.Equal(
                new byte[]
                {
                    0, 0, 0, 0, 0,
                    3, 3, 3, 3, 3,
                    6, 6, 6, 6, 6,
                    9, 9, 9, 9, 9,
                    10, 10, 10, 10, 10
                },
                output);

            // non uniform input array

            input = new byte [] {
                0, 1, 2,
                3, 4, 5,
                6, 7, 8
            };

            output = BilinearInterpolation.RescaleGrayscale(input, inputWidth, inputHeight, outputWidth, outputHeight);

            Assert.Equal(
                new byte[]
                {
                    0, 0, 1, 1, 2,
                    1, 2, 3, 3, 3,
                    3, 4, 4, 5, 5,
                    5, 6, 6, 7, 7,
                    6, 6, 7, 7, 8
                },
                output);
        }

        [Fact]
        public void RescaleGrayscale4x4_to_8x8()
        {
            byte[] input =
            {
                0, 0, 0, 0,
                5, 5, 5, 5,
                10, 10, 10, 10,
                15, 15, 15, 15
            };

            const int inputWidth = 4,
                inputHeight = inputWidth;

            const int outputWidth = 8, outputHeight = 8;

            byte[] output = BilinearInterpolation.RescaleGrayscale(input, inputWidth, inputHeight, outputWidth, outputHeight);

            Assert.Equal(
                new byte[]
                {
                    0,  0,  0,  0,  0,  0,  0,  0,
                    2,  2,  2,  2,  2,  2,  2,  2,
                    5,  5,  5,  5,  5,  5,  5,  5,
                    7,  7,  7,  7,  7,  7,  7,  7,
                    10, 10, 10, 10, 10, 10, 10, 10,
                    12, 12, 12, 12, 12, 12, 12, 12,
                    15, 15, 15, 15, 15, 15, 15, 15,
                    15, 15, 15, 15, 15, 15, 15, 15
                },
                output);

            // non uniform input array

            input = new byte[]
            {
                0,  1,  2,  3,
                4,  5,  6,  7,
                8,  9,  10, 11,
                12, 13, 14, 15
            };

            output = BilinearInterpolation.RescaleGrayscale(input, inputWidth, inputHeight, outputWidth, outputHeight);

            Assert.Equal(
                new byte[]
                {
                    0,  0,  1,  1,  2,  2,  3,  3,
                    2,  2,  3,  3,  4,  4,  5,  5,
                    4,  4,  5,  5,  6,  6,  7,  7,
                    6,  6,  7,  7,  8,  8,  9,  9,
                    8,  8,  9,  9,  10, 10, 11, 11,
                    10, 10, 11, 11, 12, 12, 13, 13,
                    12, 12, 13, 13, 14, 14, 15, 15,
                    12, 12, 13, 13, 14, 14, 15, 15
                },
                output);
        }

    }
}
