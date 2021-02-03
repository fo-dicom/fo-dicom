// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using BenchmarkDotNet.Running;


namespace FellowOakDicom.Benchmark
{
    static class Program
    {

        static void Main(string[] args)
        {
            // benchmark opening files
            BenchmarkRunner.Run<OpenFileBenchmarks>();

            // bencharm instanciating a server
            BenchmarkRunner.Run<ServerBenchmarks>();

            // benchmark json serialization and deserialization
            BenchmarkRunner.Run<JsonBenchmarks>();

            // benchmark parsing datasets
            BenchmarkRunner.Run<ParseDatasetBenchmark>();
        }

    }
}
