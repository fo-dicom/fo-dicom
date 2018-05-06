// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;

    using Xunit;

    public class DicomDateTimeTest
    {
        [Fact]
        public void Get_String_ReturnStringOnDicomValidFormat()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"),
                    new DicomDateTime(DicomTag.AcquisitionDateTime, new DateTime(2016, 4, 20, 10, 20, 30)));
            
            var expected = $"20160420102030";
            var actual = dataset.Get<string>(DicomTag.AcquisitionDateTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_StringWithDicomValidTimeZone_ShouldNotThrow()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

            var zone = new DateTime(2016, 4, 20).ToString("yyyyMMddHHmmsszzz").Substring(14).Replace(":", string.Empty);

            var exception = Record.Exception(() => dataset.Add(DicomTag.AcquisitionDateTime, $"20160420102030{zone}"));
            Assert.Null(exception);

            var expected = new DateTime(2016, 4, 20, 10, 20, 30);
            var actual = dataset.Get<DateTime>(DicomTag.AcquisitionDateTime);

            Assert.Equal(expected, actual);
        }
    }
}
