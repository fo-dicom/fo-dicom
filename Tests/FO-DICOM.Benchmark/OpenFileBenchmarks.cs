using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
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
        public void OpenFile()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"));
        }

        [Benchmark]
        public void OpenFileReadAll()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"), FileReadOption.ReadAll);
        }

        [Benchmark]
        public void OpenFileAndRender()
        {
            var file = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"));
            var image = new DicomImage(file.Dataset);
            var rendered = image.RenderImage();
        }

        [Benchmark]
        public void JsonSerialization()
        {
            var json = JsonConvert.SerializeObject(_dicomDir, new JsonDicomConverter());
            var reconstituatedDataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
        }

    }
}
