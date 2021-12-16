using System;

namespace FellowOakDicom.Memory
{
    public interface IMemory : IDisposable
    {
        byte[] Bytes { get; }
        int Length { get; }
        Span<byte> Span { get; }
        Memory<byte> Memory { get; }
    }
}