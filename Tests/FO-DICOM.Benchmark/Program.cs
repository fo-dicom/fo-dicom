// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using BenchmarkDotNet.Running;

namespace FellowOakDicom.Benchmark
{
    static class Program
    {
        static void Main()
        {
            // Run all benchmarks in assembly
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
