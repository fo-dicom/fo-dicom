﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace Dicom.IO.Buffer
{
    public static class ByteBufferExtensions
    {
        public static IEnumerable<T> Enumerate<T>(this IByteBuffer buffer)
        {
            return ByteBufferEnumerator<T>.Create(buffer);
        }
    }
}
