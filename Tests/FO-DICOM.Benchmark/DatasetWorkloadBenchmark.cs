// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using BenchmarkDotNet.Attributes;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class DatasetWorkloadBenchmark
    {
        private static readonly string[] _decimalParts = new[]
        {
            "1.0", "2.5", "3.7", "4.2", "5.0", "6.3", "7.1", "8.9",
            "9.0", "10.5", "11.7", "12.2", "13.0", "14.3", "15.1", "16.9"
        };

        private static readonly string[] _decimalPartsComma = new[]
        {
            "1,0", "2,5", "3,7", "4,2", "5,0", "6,3", "7,1", "8,9",
            "9,0", "10,5", "11,7", "12,2", "13,0", "14,3", "15,1", "16,9"
        };

        private static readonly string[] _integerParts = new[]
        {
            "1", "2", "3", "4", "5", "6", "7", "8",
            "9", "10", "11", "12", "13", "14", "15", "16"
        };

        [Benchmark]
        public DicomDataset BuildDataset_ValidationOn()
        {
            var ds = new DicomDataset();
            ds.Add(DicomTag.SOPClassUID, "1.2.840.10008.5.1.4.1.1.2");
            ds.Add(DicomTag.SOPInstanceUID, "1.2.3.4.5.6.7.8.9.10");
            ds.Add(DicomTag.StudyDate, "20260513");
            ds.Add(DicomTag.StudyTime, "120000.000000");
            ds.Add(DicomTag.AccessionNumber, "ACC123456");
            ds.Add(DicomTag.Modality, "CT");
            ds.Add(DicomTag.Manufacturer, "fo-dicom");
            ds.Add(DicomTag.ReferringPhysicianName, "Smith^Jane^^Dr^");
            ds.Add(DicomTag.StudyInstanceUID, "1.2.3.4.5.6.7.8.9.11");
            ds.Add(DicomTag.SeriesInstanceUID, "1.2.3.4.5.6.7.8.9.12");
            ds.Add(DicomTag.PatientName, "Doe^John^^Mr^Jr");
            ds.Add(DicomTag.PatientID, "PID-001");
            ds.Add(DicomTag.PatientBirthDate, "19800101");
            ds.Add(DicomTag.PatientSex, "M");
            ds.Add(DicomTag.PatientAge, "045Y");
            ds.Add(DicomTag.StudyDescription, "Routine chest CT");
            ds.Add(DicomTag.SeriesDescription, "Axial 1mm");
            ds.Add(DicomTag.InstanceNumber, "1");
            ds.Add(DicomTag.PixelSpacing, "0.78125", "0.78125");
            ds.Add(DicomTag.SliceThickness, "1.0");
            return ds;
        }

        [Benchmark]
        public decimal[] GetMultiValuedDecimalArray()
        {
            var element = new DicomDecimalString(DicomTag.PixelSpacing, _decimalParts);
            return element.Get<decimal[]>();
        }

        [Benchmark]
        public decimal[] GetMultiValuedDecimalArrayWithComma()
        {
            var element = new DicomDecimalString(DicomTag.PixelSpacing, _decimalPartsComma);
            return element.Get<decimal[]>();
        }

        [Benchmark]
        public int[] GetMultiValuedIntegerArray()
        {
            var element = new DicomIntegerString(DicomTag.ReferencedFrameNumber, _integerParts);
            return element.Get<int[]>();
        }

        [Benchmark]
        public DicomDataset BuildAndRemoveBySelector()
        {
            var ds = BuildLargePrivateDataset();
            ds.Remove(item => item.Tag.Group == 0x3009);
            return ds;
        }

        [Benchmark]
        public DicomDataset BuildLargeDataset()
        {
            return BuildLargePrivateDataset();
        }

        private static DicomDataset BuildLargePrivateDataset()
        {
#pragma warning disable CS0618
            var ds = new DicomDataset { AutoValidate = false };
#pragma warning restore CS0618
            for (ushort grp = 0x0008; grp <= 0x0028; grp += 8)
            {
                for (ushort elem = 0x0001; elem < 0x0080; elem++)
                {
                    ds.Add(new DicomLongString(new DicomTag(grp, elem), "value"));
                }
            }
            for (ushort elem = 0x0001; elem < 0x0080; elem++)
            {
                ds.Add(new DicomLongString(new DicomTag(0x3009, elem), "value"));
            }
            return ds;
        }
    }
}
