// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using BenchmarkDotNet.Running;


namespace FellowOakDicom.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //var summary2 = BenchmarkRunner.Run<ServerBenchmarks>();
            //var summary = BenchmarkRunner.Run<OpenFileBenchmarks>();

            //Console.Write(summary.Table.ToString());
            //Console.Write(summary2.Table.ToString());

            var summary3 = BenchmarkRunner.Run<JsonBenchmarks>();
            Console.Write(summary3.Table.ToString());
        }
    }
}
