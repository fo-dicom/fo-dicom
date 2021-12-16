using Microsoft.Toolkit.HighPerformance.Buffers;
using System;

namespace FellowOakDicom.Memory
{
    public class ArrayPoolMemory : IMemory
    {
        private readonly MemoryOwner<byte> _memoryOwner;

        public ArrayPoolMemory(MemoryOwner<byte> memoryOwner)
        {
            _memoryOwner = memoryOwner ?? throw new ArgumentNullException(nameof(memoryOwner));
            Bytes = _memoryOwner.DangerousGetArray().Array;
        }

        ~ArrayPoolMemory() => Dispose();

        public int Length => _memoryOwner.Length;
        public byte[] Bytes { get; }
        public Span<byte> Span => _memoryOwner.Span;
        public Memory<byte> Memory => _memoryOwner.Memory;
        
        public void Dispose() => _memoryOwner.Dispose();
    }
}