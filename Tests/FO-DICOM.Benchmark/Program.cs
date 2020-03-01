using System;
using BenchmarkDotNet.Running;


namespace FellowOakDicom.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary2 = BenchmarkRunner.Run<ServerBenchmarks>();
            var summary = BenchmarkRunner.Run<OpenFileBenchmarks>();

            Console.Write(summary.Table.ToString());
            Console.Write(summary2.Table.ToString());
        }
    }
}
