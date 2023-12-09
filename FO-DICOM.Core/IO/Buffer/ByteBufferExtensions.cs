// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;

namespace FellowOakDicom.IO.Buffer
{

    public static class ByteBufferExtensions
    {
        public static IEnumerable<T> Enumerate<T>(this IByteBuffer buffer)
        {
            return ByteBufferEnumerator<T>.Create(buffer);
        }
    }
}
