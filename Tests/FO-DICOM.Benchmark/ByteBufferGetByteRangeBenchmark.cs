// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class ByteBufferGetByteRangeBenchmark
    {
        // One 512x512 frame of 16-bit pixel data; the composites split it into four parts
        private const int BufferSize = 512 * 512 * 2;
        private const int PartSize = BufferSize / 4;
        private const long Offset = BufferSize / 8;

        private byte[] _output;
        private IByteBuffer _memory;
        private IByteBuffer _range;
        private IByteBuffer _compositeOfMemory;
        private IByteBuffer _compositeOfTempFiles;
        private IByteBuffer _evenLength;
        private IByteBuffer _evenLengthOfTempFile;
        private IByteBuffer _endianSwapped;
        private IByteBuffer _tempFile;

        // A single element value, and a bulk read crossing three of the four composite parts
        [Params(64, BufferSize / 2)]
        public int Count { get; set; }
        
        [GlobalSetup]
        public void Setup()
        {
            var data = new byte[BufferSize];
            new Random(42).NextBytes(data);
            _output = new byte[BufferSize];

            _memory = new MemoryByteBuffer(data);
            _range = new RangeByteBuffer(_memory, 0, BufferSize);

            var memoryParts = new IByteBuffer[4];
            var tempFileParts = new IByteBuffer[4];
            for (var i = 0; i < 4; i++)
            {
                var part = data.AsSpan(i * PartSize, PartSize).ToArray();
                memoryParts[i] = new MemoryByteBuffer(part);
                tempFileParts[i] = new TempFileBuffer(part);
            }
            _compositeOfMemory = new CompositeByteBuffer(memoryParts);
            _compositeOfTempFiles = new CompositeByteBuffer(tempFileParts);

            // EvenLengthBuffer only wraps an uneven buffer, so leave out the last byte
            var uneven = data.AsSpan(0, BufferSize - 1).ToArray();
            _evenLength = EvenLengthBuffer.Create(new MemoryByteBuffer(uneven));
            _evenLengthOfTempFile = EvenLengthBuffer.Create(new TempFileBuffer(uneven));

            // Big endian with 2-byte units, so every read swaps on a little-endian machine
            _endianSwapped = EndianByteBuffer.Create(_memory, Endian.Big, 2);

            _tempFile = new TempFileBuffer(data);
        }
        
        [Benchmark(Baseline = true)]
        public void Memory() => _memory.GetByteRange(Offset, Count, _output);

        [Benchmark]
        public void Range() => _range.GetByteRange(Offset, Count, _output);

        [Benchmark]
        public void CompositeOfMemory() => _compositeOfMemory.GetByteRange(Offset, Count, _output);

        [Benchmark]
        public void CompositeOfTempFiles() => _compositeOfTempFiles.GetByteRange(Offset, Count, _output);

        [Benchmark]
        public void EvenLength() => _evenLength.GetByteRange(Offset, Count, _output);
        
        [Benchmark]
        public void EvenLengthOfTempFile() => _evenLengthOfTempFile.GetByteRange(Offset, Count, _output);

        [Benchmark]
        public void EndianSwapped() => _endianSwapped.GetByteRange(Offset, Count, _output);

        [Benchmark]
        public void TempFile() => _tempFile.GetByteRange(Offset, Count, _output);
    }
}