// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomDateRangeTest
    {
        [Fact]
        public void Add_DateRange_GetStringReturnsRangeString()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));
            dataset.Add(
                DicomTag.AcquisitionDate,
                new DicomDateRange(new DateTime(2016, 4, 20), new DateTime(2016, 4, 21)));

            const string expected = "20160420-20160421";
            var actual = dataset.GetSingleValue<string>(DicomTag.AcquisitionDate);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_DateRange_GetDateRangeReturnsValidRange()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

            var expected = new DicomDateRange(new DateTime(2016, 4, 20), new DateTime(2016, 4, 21));
            dataset.Add(DicomTag.AcquisitionDate, expected);

            var actual = dataset.GetSingleValue<DicomDateRange>(DicomTag.AcquisitionDate);
            Assert.Equal(expected.Minimum, actual.Minimum);
            Assert.Equal(expected.Maximum, actual.Maximum);
        }

        [Fact]
        public void Add_TimeRange_GetStringReturnsRangeString()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));
            dataset.Add(
                DicomTag.AcquisitionTime,
                new DicomDateRange(new DateTime(1, 1, 1, 5, 10, 5), new DateTime(1, 1, 1, 19, 0, 20)));

            const string expected = "051005-190020";
            var actual = dataset.GetSingleValue<string>(DicomTag.AcquisitionTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_TimeRange_GetDateRangeReturnsValidRange()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

            var expected = new DicomDateRange(new DateTime(1, 1, 1, 5, 10, 5), new DateTime(1, 1, 1, 19, 0, 20));
            dataset.Add(DicomTag.AcquisitionTime, expected);

            var actual = dataset.GetSingleValue<DicomDateRange>(DicomTag.AcquisitionTime);
            Assert.Equal(expected.Minimum, actual.Minimum);
            Assert.Equal(expected.Maximum, actual.Maximum);
        }

        [Fact]
        public void Add_DateTimeRange_GetStringReturnsRangeString()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));
            dataset.Add(
                DicomTag.AcquisitionDateTime,
                new DicomDateRange(new DateTime(2016, 4, 20, 10, 20, 30), new DateTime(2016, 4, 21, 8, 50, 5)));

            var expected = $"20160420102030-20160421085005";
            var actual = dataset.GetString(DicomTag.AcquisitionDateTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_DateTimeRange_GetDateRangeReturnsValidRange()
        {
            var dataset =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

            var expected = new DicomDateRange(
                new DateTime(2016, 4, 20, 10, 20, 30),
                new DateTime(2016, 4, 21, 8, 50, 5));
            dataset.Add(DicomTag.AcquisitionDateTime, expected);

            var actual = dataset.GetSingleValue<DicomDateRange>(DicomTag.AcquisitionDateTime);
            Assert.Equal(expected.Minimum, actual.Minimum);
            Assert.Equal(expected.Maximum, actual.Maximum);
        }
    }
}
