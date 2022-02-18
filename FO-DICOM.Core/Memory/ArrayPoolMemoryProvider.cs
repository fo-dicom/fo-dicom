// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Microsoft.Toolkit.HighPerformance.Buffers;

namespace FellowOakDicom.Memory
{
    public class ArrayPoolMemoryProvider : IMemoryProvider
    {
        public IMemory Provide(int length) => new ArrayPoolMemory(MemoryOwner<byte>.Allocate(length));
    }
}
