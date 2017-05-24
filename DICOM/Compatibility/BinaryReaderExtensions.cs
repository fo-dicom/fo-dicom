// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace System.IO
{
    /// <summary>
    /// .NET 3.5 compatibility extension methods for <see cref="BinaryReader"/>.
    /// </summary>
    internal static class BinaryReaderExtensions
    {
        internal static void Dispose(this BinaryReader reader)
        {
            reader.Close();
        }
    }
}