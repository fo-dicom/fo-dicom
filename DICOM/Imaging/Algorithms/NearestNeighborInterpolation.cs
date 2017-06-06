// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35
using System.Threading.Tasks;
#endif

namespace Dicom.Imaging.Algorithms
{
    public class NearestNeighborInterpolation
    {
        public static byte[] RescaleGrayscale(
            byte[] input,
            int inputWidth,
            int inputHeight,
            int outputWidth,
            int outputHeight)
        {
            var output = new byte[outputWidth * outputHeight];

            var xF = inputWidth / (double)outputWidth;
            var yF = inputHeight / (double)outputHeight;

            var xMax = inputWidth - 1;
            var yMax = inputHeight - 1;

            unchecked
            {
#if NET35
                for (var y = 0; y < outputHeight; ++y)
#else
                Parallel.For(0, outputHeight, y =>
#endif
                {
                    var oy0 = y * yF;
                    var oy1 = (int)oy0; // rounds down
                    var oy2 = oy1 == yMax ? oy1 : oy1 + 1;

                    var dy1 = oy0 - oy1;
                    var dy2 = 1.0 - dy1;

                    var yo0 = outputWidth * y;
                    var yo1 = inputWidth * oy1;
                    var yo2 = inputWidth * oy2;

                    for (var x = 0; x < outputWidth; x++)
                    {
                        var ox0 = x * xF;
                        var ox1 = (int)ox0;
                        var ox2 = ox1 == xMax ? ox1 : ox1 + 1;

                        var dx1 = ox0 - ox1;
                        var dx2 = 1.0 - dx1;

                        output[yo0 + x] = dy2 > dy1
                            ? (dx2 > dx1 ? input[yo1 + ox1] : input[yo1 + ox2])
                            : (dx2 > dx1 ? input[yo2 + ox1] : input[yo2 + ox2]);
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
