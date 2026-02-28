// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.IO;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class EndianArraySwapBenchmark
    {
        private short[] _shortArray;
        private int[] _intArray;

        [GlobalSetup]
        public void Setup()
        {
            // Simulate typical pixel data: 512x512 image
            _shortArray = new short[512 * 512];
            _intArray = new int[512 * 512];

            var rng = new Random(42);
            for (int i = 0; i < _shortArray.Length; i++)
            {
                _shortArray[i] = (short)rng.Next(short.MinValue, short.MaxValue);
            }
            for (int i = 0; i < _intArray.Length; i++)
            {
                _intArray[i] = rng.Next();
            }
        }

        [Benchmark]
        public void SwapShortArray()
        {
            Endian.Swap(_shortArray);
        }

        [Benchmark]
        public void SwapIntArray()
        {
            Endian.Swap(_intArray);
        }
    }
}
