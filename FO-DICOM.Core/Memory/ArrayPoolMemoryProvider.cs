using Microsoft.Toolkit.HighPerformance.Buffers;

namespace FellowOakDicom.Memory
{
    public class ArrayPoolMemoryProvider : IMemoryProvider
    {
        public IMemory Provide(int length) => new ArrayPoolMemory(MemoryOwner<byte>.Allocate(length));
    }
}