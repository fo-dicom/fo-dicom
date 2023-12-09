// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

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
        private MemoryStream _mgData;
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
            _mgData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\mg.dcm")));
            _dicomdirData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\DICOMDIR")));
        }


        [Benchmark]
        public object CT_LE_Implicit() => ParseHeader(_ctData);

        [Benchmark]
        public object MR_LE_Implicit() => ParseHeader(_mrData);

        [Benchmark]
        public object MG_LE_Explicit() => ParseHeader(_mgData);

        [Benchmark]
        public object DICOMDIR() => ParseHeader(_dicomdirData);

        public static object ParseHeader(Stream content)
        {
            content.Position = 0;
            var file = DicomFile.Open(content);
            return file;
        }

    }
}
