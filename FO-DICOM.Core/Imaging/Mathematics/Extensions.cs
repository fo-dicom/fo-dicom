// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Imaging.Mathematics
{

    public static class Extensions
    {

        public static bool IsOdd(this byte v) => (v & 1) == 1;

        public static bool IsOdd(this short v) => (v & 1) == 1;

        public static bool IsOdd(this ushort v) => (v & 1) == 1;

        public static bool IsOdd(this int v) => (v & 1) == 1;

        public static bool IsOdd(this uint v) => (v & 1) == 1;


        public static bool IsEven(this byte v) => (v & 1) == 0;

        public static bool IsEven(this short v) => (v & 1) == 0;

        public static bool IsEven(this ushort v) => (v & 1) == 0;

        public static bool IsEven(this int v) => (v & 1) == 0;

        public static bool IsEven(this uint v) => (v & 1) == 0;


        public static bool IsNearlyZero(this double v) => Math.Abs(v) < Constants.Epsilon;

        public static bool IsNearlyZero(this float v) => Math.Abs(v) < Constants.Epsilon;

    }
}
