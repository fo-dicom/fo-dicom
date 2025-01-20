// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using BenchmarkDotNet.Attributes;
using System;
using System.IO;
using System.Reflection;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class DicomTagBenchmark
    {
        private readonly string _rootPath;
        private DicomDataset _dataset;

        public DicomTagBenchmark()
        {
            _rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [GlobalSetup]
        public void Setup()
        {
            _dataset = DicomFile.Open(Path.Combine(_rootPath, "Data\\ct.dcm")).Dataset;
            var studydesc = _dataset.GetSingleValue<string>(DicomTag.StudyDescription);
        }

        [Benchmark]
        public string AccessStringOld()
        {
            var studydesc = _dataset.GetSingleValue<string>(DicomTag.StudyDescription);
            return studydesc;
        }

        [Benchmark]
        public string AccessStringNew()
        {
            var studydesc = _dataset.GetItem(DicomTag.StudyDescription).Value;
            return studydesc;
        }

        [Benchmark]
        public void SetStringOld()
        {
            _dataset.AddOrUpdate(DicomTag.StudyDescription, "new study description");
        }

        [Benchmark]
        public void SetStringNew()
        {
            _dataset.SetItem(DicomTag.StudyDescription, "new study description");
        }

        [Benchmark]
        public DateTime AccessDateTimeOld()
        {
            var studydesc = _dataset.GetSingleValue<DateTime>(DicomTag.StudyDate);
            return studydesc;
        }

        [Benchmark]
        public DateTime AccessDateTimeNew()
        {
            var studydesc = _dataset.GetItem(DicomTag.StudyDate).Value;
            return studydesc ?? DateTime.MinValue;
        }

        [Benchmark]
        public string AccessDateTimeStringOld()
        {
            var studydesc = _dataset.GetSingleValue<string>(DicomTag.StudyDate);
            return studydesc;
        }

        [Benchmark]
        public string AccessDateTimeStringNew()
        {
            var studydesc = _dataset.GetItem(DicomTag.StudyDate).StringValues[0];
            return studydesc;
        }

        [Benchmark]
        public void SetDateTimeOld()
        {
            _dataset.AddOrUpdate(DicomTag.StudyDate, DateTime.Now);
        }

        [Benchmark]
        public void SetDateTimeNew()
        {
            _dataset.SetItem(DicomTag.StudyDate, DateTime.Now);
        }

    }
}
