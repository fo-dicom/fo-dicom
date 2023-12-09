// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.Codec.JpegLossless
{

    internal static class JaggedArrayFactory
    {

        public static T[][] Create<T>(int a, int b)
        {
            var ret = new T[a][];
            for (int x = 0; x < a; x++)
            {
                ret[x] = new T[b];
            }
            return ret;
        }


        public static T[,][] Create<T>(int a, int b, int c)
        {
            var ret = new T[a, b][];
            for (int x = 0; x < a; x++)
            {
                for (int y = 0; y < b; y++)
                {
                    ret[x, y] = new T[c];
                }
            }
            return ret;
        }


        public static T[,][,] Create<T>(int a, int b, int c, int d)
        {
            var ret = new T[a, b][,];
            for (int x = 0; x < a; x++)
            {
                for (int y = 0; y < b; y++)
                {
                    ret[x, y] = new T[c, d];
                }
            }
            return ret;
        }

    }
}
