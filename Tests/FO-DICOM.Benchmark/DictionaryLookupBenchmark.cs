// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Attributes;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class DictionaryLookupBenchmark
    {
        private DicomDictionary _dictionary;

        // Common tags that are looked up during every DICOM parse
        private static readonly DicomTag[] _commonTags = new[]
        {
            DicomTag.PatientID,
            DicomTag.PatientName,
            DicomTag.StudyInstanceUID,
            DicomTag.SeriesInstanceUID,
            DicomTag.SOPInstanceUID,
            DicomTag.Modality,
            DicomTag.Rows,
            DicomTag.Columns,
            DicomTag.BitsAllocated,
            DicomTag.BitsStored,
            DicomTag.PixelRepresentation,
            DicomTag.PixelData,
            DicomTag.TransferSyntaxUID,
            DicomTag.SpecificCharacterSet,
            DicomTag.ImageType,
            DicomTag.StudyDate,
        };

        private static readonly string[] _commonKeywords = new[]
        {
            "PatientID",
            "PatientName",
            "StudyInstanceUID",
            "SeriesInstanceUID",
            "SOPInstanceUID",
            "Modality",
            "Rows",
            "Columns",
            "BitsAllocated",
            "BitsStored",
            "PixelRepresentation",
            "PixelData",
            "TransferSyntaxUID",
            "SpecificCharacterSet",
            "ImageType",
            "StudyDate",
        };

        [GlobalSetup]
        public void Setup()
        {
            _dictionary = DicomDictionary.Default;
        }

        [Benchmark]
        public DicomDictionaryEntry Dictionary_LookupByTag()
        {
            DicomDictionaryEntry result = null;
            foreach (var tag in _commonTags)
            {
                result = _dictionary[tag];
            }
            return result;
        }

        [Benchmark]
        public DicomTag Dictionary_LookupByKeyword()
        {
            DicomTag result = null;
            foreach (var keyword in _commonKeywords)
            {
                result = _dictionary[keyword];
            }
            return result;
        }
    }
}
