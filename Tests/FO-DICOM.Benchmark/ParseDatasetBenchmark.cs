// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
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

        private Action<Stream> _parse;

        public ParseDatasetBenchmark()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [Params("DicomFile", "DisposableDicomFile")]
        public string Type { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _ctData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\ct.dcm")));
            _mrData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\mr.dcm")));
            _mgData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\mg.dcm")));
            _dicomdirData = new MemoryStream(File.ReadAllBytes(Path.Combine(_rootpath, "Data\\DICOMDIR")));

            switch (Type)
            {
                case "DicomFile":
                    _parse = stream => DicomFile.Open(stream);
                    break;
                case "DisposableDicomFile":
                    _parse = stream =>
                    {
                        using var _ = DisposableDicomFile.Open(stream);
                    };
                    break;
            }
        }


        [Benchmark]
        public void CT_LE_Implicit() => ParseHeader(_ctData);

        [Benchmark]
        public void MR_LE_Implicit() => ParseHeader(_mrData);

        [Benchmark]
        public void MG_LE_Explicit() => ParseHeader(_mgData);

        [Benchmark]
        public void DICOMDIR() => ParseHeader(_dicomdirData);

        public void ParseHeader(Stream content)
        {
            content.Position = 0;
            _parse(content);
        }

    }
}
