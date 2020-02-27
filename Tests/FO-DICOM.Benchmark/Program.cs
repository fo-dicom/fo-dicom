using System;
using BenchmarkDotNet.Running;


namespace FellowOakDicom.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<OpenFileBenchmarks>();

            Console.Write(summary.AllRuntimes);
        }
    }
}
