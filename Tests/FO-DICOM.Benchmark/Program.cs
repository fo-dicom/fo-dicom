// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace FellowOakDicom.Benchmark
{
    static class Program
    {
        static async Task Main()
        {
            // Run all benchmarks in assembly
            BenchmarkRunner.Run<ServerBenchmarks>();


        }
    }
}
