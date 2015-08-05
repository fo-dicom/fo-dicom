// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Buffer
{
    public interface IByteBuffer
    {
        bool IsMemory { get; }

        uint Size { get; }

        byte[] Data { get; }

        byte[] GetByteRange(int offset, int count);
    }
}
