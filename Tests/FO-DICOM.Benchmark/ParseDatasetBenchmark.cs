// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using BenchmarkDotNet.Attributes;
using System.IO;
using System.Reflection;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class ParseDatasetBenchmark
    {
        private readonly string _rootpath;

        private MemoryStream ctData;
        private MemoryStream mrData;
        private MemoryStream dicomdirData;


        public ParseDatasetBenchmark()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [GlobalSetup]
        public void Setup()
        {
            ctData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\ct.dcm")));
            mrData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\mr.dcm")));
            dicomdirData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\DICOMDIR")));
        }


        [Benchmark]
        public object ParseCT() => ParseHeader(ctData);

        [Benchmark]
        public object ParseMR() => ParseHeader(mrData);

        [Benchmark]
        public object ParseDicomdir() => ParseHeader(dicomdirData);


        public static object ParseHeader(Stream content)
        {
            content.Position = 0;
            var file = DicomFile.Open(content);
            return file;
        }

    }
}
