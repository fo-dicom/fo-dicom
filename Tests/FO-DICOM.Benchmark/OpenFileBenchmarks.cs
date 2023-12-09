// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.Imaging;
using FellowOakDicom.Serialization;
using Newtonsoft.Json;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class OpenFileBenchmarks
    {

        private readonly string _rootpath;
        private DicomDataset _dicomDir;

        public OpenFileBenchmarks()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [GlobalSetup]
        public void Setup()
        {
            _dicomDir = DicomFile.Open(Path.Combine(_rootpath, "Data\\DICOMDIR")).Dataset;
        }

        [Benchmark]
        public DicomFile OpenFile()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"));
            return file;
        }

        [Benchmark]
        public DicomFile OpenDeflatedFile()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\Deflated.dcm"));
            return file;
        }

        [Benchmark]
        public DicomFile OpenFileReadAll()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"), FileReadOption.ReadAll);
            return file;
        }

        [Benchmark]
        public IImage OpenFileAndRender()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"));
            var image = new DicomImage(file.Dataset);
            var rendered = image.RenderImage();
            return rendered;
        }

        [Benchmark]
        public DicomDataset JsonSerialization()
        {
            var json = JsonConvert.SerializeObject(_dicomDir, new JsonDicomConverter());
            var reconstituatedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
            return reconstituatedDataset;
        }

    }
}
