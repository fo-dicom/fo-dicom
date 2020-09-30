using System;
using Xunit;

namespace Dicom.Imaging.Algorithms
{
    public class BilinearInterpolationTest
    {
        [Fact]
        public void RescaleGrayscale2x2()
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
        public void RescaleGrayscale4x4()
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
                    0, 0, 0, 0, 0, 0, 0, 0,
                    2, 2, 2, 2, 2, 2, 2, 2,
                    5, 5, 5, 5, 5, 5, 5, 5,
                    7, 7, 7, 7, 7, 7, 7, 7,
                    10, 10, 10, 10, 10, 10, 10, 10,
                    12, 12, 12, 12, 12, 12, 12, 12,
                    15, 15, 15, 15, 15, 15, 15, 15,
                    15, 15, 15, 15, 15, 15, 15, 15
                },
                output);

        }

    }
}
