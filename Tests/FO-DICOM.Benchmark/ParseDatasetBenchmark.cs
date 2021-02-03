// Copyright (c) 2012-2021 fo-dicom contributors.
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

        private MemoryStream _ctData;
        private MemoryStream _mrData;
        private MemoryStream _dicomdirData;


        public ParseDatasetBenchmark()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [GlobalSetup]
        public void Setup()
        {
            _ctData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\ct.dcm")));
            _mrData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\mr.dcm")));
            _dicomdirData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\DICOMDIR")));
        }


        [Benchmark]
        public object ParseCT() => ParseHeader(_ctData);

        [Benchmark]
        public object ParseMR() => ParseHeader(_mrData);

        [Benchmark]
        public object ParseDicomdir() => ParseHeader(_dicomdirData);


        public static object ParseHeader(Stream content)
        {
            content.Position = 0;
            var file = DicomFile.Open(content);
            return file;
        }

    }
}
