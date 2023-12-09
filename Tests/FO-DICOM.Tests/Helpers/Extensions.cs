// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Tests.Helpers
{
    public static class Extensions
    {

        public static bool ContainsSequence(this byte[] wholeArray, byte[] sequence)
        {
            for (var start = 0; start <= wholeArray.Length - sequence.Length; start++)
            {
                var ok = true;
                for (var i = 0; i < sequence.Length; i++)
                {
                    if (sequence[i] != wholeArray[start + i])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
