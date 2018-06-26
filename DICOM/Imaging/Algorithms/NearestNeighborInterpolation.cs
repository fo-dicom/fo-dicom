// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35
using System.Threading.Tasks;
#endif

namespace Dicom.Imaging.Algorithms
{
    /// <summary>
    /// 2D interpolation methods using the nearest neighbor algorithm.
    /// </summary>
    public static class NearestNeighborInterpolation
    {
        /// <summary>
        /// Rescale 2D 8-bit "grayscale" image using nearest neighbor interpolation.
        /// </summary>
        /// <param name="input">Input 8-bit 2D "grayscale" image grid.</param>
        /// <param name="inputWidth">Width of ipnut grid.</param>
        /// <param name="inputHeight">Height of input grid.</param>
        /// <param name="outputWidth">Requested width of output grid.</param>
        /// <param name="outputHeight">Requested height of output grid.</param>
        /// <returns>Nearest neighbor interpolated grid with output <paramref name="outputWidth">width</paramref>
        /// and <paramref name="outputHeight">height</paramref>.</returns>
        public static byte[] RescaleGrayscale(
            byte[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            var output = new byte[outputWidth * outputHeight];

            var xF = inputWidth / (double) outputWidth;
            var yF = inputHeight / (double) outputHeight;

            var xMax = inputWidth - 1;
            var yMax = inputHeight - 1;

#if NET35
            for (var y = 0; y < outputHeight; ++y)
#else
            Parallel.For(0, outputHeight, y =>
#endif
                {
                    var iy0 = y * yF;
                    var iy1 = (int) iy0; // rounds down
                    var iy2 = iy1 == yMax ? iy1 : iy1 + 1;
                    var iyx0 = inputWidth * (iy0 - iy1 < 0.5 ? iy1 : iy2);

                    for (int yx = outputWidth * y, x = 0; x < outputWidth; x++, yx++)
                    {
                        var ix0 = x * xF;
                        var ix1 = (int) ix0;
                        var ix2 = ix1 == xMax ? ix1 : ix1 + 1;
                        var ix = ix0 - ix1 < 0.5 ? ix1 : ix2;

                        output[yx] = input[iyx0 + ix];
                    }
                }
#if !NET35
            );
#endif

            return output;
        }
    }
}
