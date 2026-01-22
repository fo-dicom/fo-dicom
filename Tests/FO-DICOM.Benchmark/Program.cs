// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace FellowOakDicom.Benchmark
{
    static class Program
    {
        static void Main()
        {
            var config = DefaultConfig.Instance.AddJob(Job.MediumRun.WithToolchain(InProcessNoEmitToolchain.Instance));

            // Run all benchmarks in assembly
            BenchmarkRunner.Run(typeof(Program).Assembly,
 //           BenchmarkRunner.Run<ParseDatasetBenchmark>(
                ManualConfig.Create(config)
                .WithOptions(ConfigOptions.JoinSummary)
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                );
        }
    }
}
