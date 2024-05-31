// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Attributes;
using System.IO;
using System.Reflection;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class CompareDatasetBenchmark
    {
        private readonly string _rootpath;

        private MemoryStream _ctData;
        private DicomDataset _ctDataset;
        private DicomDataset _ctDataset1;
        private DicomDataset _ctDataset2;
        private DicomDataset _ctDatasetClone;
        private MemoryStream _mrData;
        private DicomDataset _mrDataset;
        private DicomDataset _mrDataset1;
        private DicomDataset _mrDataset2;
        private DicomDataset _mrDatasetClone;

        private MemoryStream _mgData;
        private DicomDataset _mgDataset;
        private DicomDataset _mgDataset1;
        private DicomDataset _mgDataset2;
        private DicomDataset _mgDatasetClone;

        private MemoryStream _dicomdirData;


        public CompareDatasetBenchmark()
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

        [IterationSetup]
        public void IterationSetup()
        {
            _ctDataset = DicomFile.Open(Path.Combine(_rootpath, "Data\\ct.dcm")).Dataset;
            _mrDataset = DicomFile.Open(Path.Combine(_rootpath, "Data\\mr.dcm")).Dataset;
            _mgDataset = DicomFile.Open(Path.Combine(_rootpath, "Data\\mg.dcm")).Dataset;
            _ctDataset1 = DicomFile.Open(Path.Combine(_rootpath, "Data\\ct.dcm")).Dataset;
            _mrDataset1 = DicomFile.Open(Path.Combine(_rootpath, "Data\\mr.dcm")).Dataset;
            _mgDataset1 = DicomFile.Open(Path.Combine(_rootpath, "Data\\mg.dcm")).Dataset;
            _ctDataset2 = DicomFile.Open(Path.Combine(_rootpath, "Data\\ct.dcm")).Dataset;
            _mrDataset2 = DicomFile.Open(Path.Combine(_rootpath, "Data\\mr.dcm")).Dataset;
            _mgDataset2 = DicomFile.Open(Path.Combine(_rootpath, "Data\\mg.dcm")).Dataset;
            _ctDatasetClone = _ctDataset.Clone();
            _mrDatasetClone = _mrDataset.Clone();
            _mgDatasetClone = _mgDataset.Clone();
        }

        [Benchmark]
        public object CompareCt() => _ctDataset1 == _ctDataset2;

        [Benchmark]
        public object CompareMR() => _mrDataset1 == _mrDataset2;

        [Benchmark]
        public object CompareMg() => _mgDataset1 == _mgDataset2;

        [Benchmark]
        public object HashCt() => _ctDataset.GetHashCode();

        [Benchmark]
        public object HashMr() => _mrDataset.GetHashCode();

        [Benchmark]
        public object HashMg() => _mgDataset.GetHashCode();

        [Benchmark]
        public object CompareCloneCt() => _ctDataset == _ctDatasetClone;

        [Benchmark]
        public object CompareCloneMR() => _mrDataset == _mrDatasetClone;

        [Benchmark]
        public object CompareCloneMg() => _mgDataset == _mgDatasetClone;

    }
}
