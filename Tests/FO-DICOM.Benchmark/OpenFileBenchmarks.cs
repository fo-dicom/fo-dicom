// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class OpenFileBenchmarks
    {

        private readonly string _rootpath;
        private DicomDataset _dicomDir;

        [Params("DicomFile", "DicomFile2")]
        public string Type { get; set; }

        private Func<string, FileReadOption, DicomFile> _open;
        private Action<DicomFile> _dispose;

        public OpenFileBenchmarks()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [GlobalSetup]
        public void Setup()
        {
            switch (Type)
            {
                case "DicomFile":
                    _open = (path, option) => DicomFile.Open(path, option);
                    _dispose = _ => { };
                    break;
                case "DicomFile2":
                    _open = (path, option) => DisposableDicomFile.Open(path, option);
                    _dispose = file => ((DisposableDicomFile)file).Dispose();
                    break;
            }
            /*
            _dicomDir = DicomFile.Open(Path.Combine(_rootpath, "Data\\DICOMDIR")).Dataset;
        */
        }

        [Benchmark]
        public void OpenFile()
        {
            _dispose(_open(Path.Combine(_rootpath, "Data\\GH355.dcm"), FileReadOption.Default));
        }

        /*[Benchmark]
        public DicomFile OpenDeflatedFile()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\Deflated.dcm"));
            return file;
        }*/

        [Benchmark]
        public void OpenFileReadAll()
        {
            _dispose(_open(Path.Combine(_rootpath, "Data\\GH355.dcm"), FileReadOption.ReadAll));
        }

        // [Benchmark]
        // public IImage OpenFileAndRender()
        // {
        //     var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"));
        //     var image = new DicomImage(file.Dataset);
        //     var rendered = image.RenderImage();
        //     return rendered;
        // }

        /*
        [Benchmark]
        public DicomDataset JsonSerialization()
        {
            var json = JsonConvert.SerializeObject(_dicomDir, new JsonDicomConverter());
            var reconstituatedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            return reconstituatedDataset;
        }
        */

    }
}
