// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Attributes;
using FellowOakDicom.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace FellowOakDicom.Benchmark
{

    [MemoryDiagnoser]
    public class JsonBenchmarks
    {
        private readonly string _rootpath;
        private DicomDataset _dicomDir;
        private DicomDataset _dicomFile;
        private string _dicomDirJson;
        private string _dicomFileJson;


        public JsonBenchmarks()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [GlobalSetup]
        public void Setup()
        {
            _dicomDir = DicomFile.Open(Path.Combine(_rootpath, "Data\\DICOMDIR"), FileReadOption.ReadAll).Dataset;
            _dicomFile = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"), FileReadOption.ReadAll).Dataset;
            _dicomDirJson = JsonConvert.SerializeObject(_dicomDir, new JsonDicomConverter());
            _dicomFileJson = JsonConvert.SerializeObject(_dicomDir, new JsonDicomConverter());
        }

        [Benchmark]
        public string SerializeDicomDirWithNewtonsoft()
        {
            var json = JsonConvert.SerializeObject(_dicomDir, new JsonDicomConverter());
            return json;
        }

        [Benchmark]
        public string SerializeFileWithNewtonsoft()
        {
            var json = JsonConvert.SerializeObject(_dicomFile, new JsonDicomConverter());
            return json;
        }

        [Benchmark]
        public DicomDataset DeserializeDicomDirWithNewtonsoft()
        {
            var ds = JsonConvert.DeserializeObject<DicomDataset>(_dicomDirJson, new JsonDicomConverter());
            return ds;
        }

        [Benchmark]
        public DicomDataset DeserializeFileWithNewtonsoft()
        {
            var ds = JsonConvert.DeserializeObject<DicomDataset>(_dicomFileJson, new JsonDicomConverter());
            return ds;
        }

        [Benchmark]
        public string SerializeDicomDirWithMicrosoft()
        {
            var json = DicomJson.ConvertDicomToJson(_dicomDir);
            return json;
        }

        [Benchmark]
        public string SerializeFileWithMicrosoft()
        {
            var json = DicomJson.ConvertDicomToJson(_dicomFile);
            return json;
        }

        [Benchmark]
        public DicomDataset DeserializeDicomDirWithMicrosoft()
        {
            var ds = DicomJson.ConvertJsonToDicom(_dicomDirJson);
            return ds;
        }

        [Benchmark]
        public DicomDataset DeserializeFileWithMicrosoft()
        {
            var ds = DicomJson.ConvertJsonToDicom(_dicomFileJson);
            return ds;
        }


    }
}
