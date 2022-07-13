// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Running;

namespace FellowOakDicom.Benchmark
{
    static class Program
    {
        static void Main()
        {
            // Run all benchmarks in assembly
            /*BenchmarkRunner.Run(typeof(Program).Assembly);*/
            /*BenchmarkRunner.Run<OpenFileBenchmarks>();*/
            for (var i = 0; i < 10000; i++)
            {
                using var _ = DicomFile2.Open(@"C:\Users\a.moerman\Downloads\teststudies\Demo_Ziekenhuis_1_-_34473\0a5f3d7d-9439-4908-9ce1-29c5dcce382c.dcm");
                Console.WriteLine(i);
            }
            /*var vrCodes = typeof(DicomVRCode)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null))
                .OfType<string>()
                .Select(code => new { Code = code, Bytes = Encoding.UTF8.GetBytes(code) })
                .GroupBy(code => code.Bytes[0])
                .ToList();

            foreach (var group in vrCodes)
            {
                Console.WriteLine($"case {group.Key}: ");
                foreach (var entry in group)
                {
                    Console.WriteLine($"  case {entry.Bytes[1]}: VR = {entry.Code}");
                }
            }*/
        }
    }
}
