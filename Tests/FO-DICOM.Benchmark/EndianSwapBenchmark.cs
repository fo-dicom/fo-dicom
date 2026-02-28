// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.IO;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class EndianSwapBenchmark
    {
        private byte[] _data2;
        private byte[] _data4;

        [GlobalSetup]
        public void Setup()
        {
            // Simulate typical pixel data sizes
            // 512x512 image with 2 bytes per pixel = 524,288 bytes
            _data2 = new byte[512 * 512 * 2];
            _data4 = new byte[512 * 512 * 4];

            var rng = new Random(42);
            rng.NextBytes(_data2);
            rng.NextBytes(_data4);
        }

        [Benchmark]
        public void SwapBytes2_PixelData()
        {
            Endian.SwapBytes2(_data2, _data2.Length);
        }

        [Benchmark]
        public void SwapBytes4_PixelData()
        {
            Endian.SwapBytes4(_data4, _data4.Length);
        }

        [Benchmark]
        public float SwapFloat()
        {
            float result = 0;
            for (int i = 0; i < 1000; i++)
            {
                result = Endian.Swap(3.14159f + i);
            }
            return result;
        }

        [Benchmark]
        public double SwapDouble()
        {
            double result = 0;
            for (int i = 0; i < 1000; i++)
            {
                result = Endian.Swap(3.14159265358979 + i);
            }
            return result;
        }
    }
}
