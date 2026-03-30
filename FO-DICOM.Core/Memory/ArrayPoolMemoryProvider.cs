// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using CommunityToolkit.HighPerformance.Buffers;

namespace FellowOakDicom.Memory
{
    public class ArrayPoolMemoryProvider : IMemoryProvider
    {
        public IMemory Provide(int length) => new ArrayPoolMemory(MemoryOwner<byte>.Allocate(length));
    }
}
