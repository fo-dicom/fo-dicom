// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace FellowOakDicom.Benchmark
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Support --filter arg from command line, otherwise run all benchmarks
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(args,
                    ManualConfig.Create(DefaultConfig.Instance)
                        .WithOptions(ConfigOptions.JoinSummary)
                        .WithOptions(ConfigOptions.DisableOptimizationsValidator));
        }
    }
}
