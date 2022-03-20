// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection("General")]
    public class DicomDatasetExtensionsTest
    {
        #region Unit tests

        [Fact]
        public void GetDateTime_DateAndTimeAvailable_ReturnsSpecifiedDateTime()
        {
            var expected = new DateTime(2016, 5, 25, 15, 54, 31);

            var dataset = new DicomDataset(
                new DicomDate(DicomTag.CreationDate, "20160525"),
                new DicomTime(DicomTag.CreationTime, "155431"));
            var actual = dataset.GetDateTime(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTime_DateAndTimeMissing_ReturnsMinimumDateTime()
        {
            var expected = DateTime.MinValue;

            var dataset = new DicomDataset();
            var actual = dataset.GetDateTime(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTime_DateMissingTimeAvailable_ReturnsMinimumDateSpecifiedTime()
        {
            var expected = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 16, 2, 15);

            var dataset = new DicomDataset(
                new DicomTime(DicomTag.CreationTime, "160215"));
            var actual = dataset.GetDateTime(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTime_DateAvaliableTimeMissing_ReturnsSpecifiedDateMinimumTime()
        {
            var expected = new DateTime(2016, 5, 25, DateTime.MinValue.Hour, DateTime.MinValue.Minute, DateTime.MinValue.Second);

            var dataset = new DicomDataset(
                new DicomDate(DicomTag.CreationDate, "20160525"));
            var actual = dataset.GetDateTime(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTimeOffset_DateAndTimeAndTimezoneAvailable_ReturnsSpecifiedDateTime()
        {
            var expected = new DateTimeOffset(2016, 5, 25, 15, 54, 31, new TimeSpan(04, 00,00));

            var dataset = new DicomDataset(
                new DicomDate(DicomTag.CreationDate, "20160525"),
                new DicomTime(DicomTag.CreationTime, "155431"),
                new DicomShortString(DicomTag.TimezoneOffsetFromUTC, "+0400"));

            var actual = dataset.GetDateTimeOffset(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTimeOffset_DateAndTimeAndNegativeTimezoneAvailable_ReturnsSpecifiedDateTime()
        {
            var expected = new DateTimeOffset(2016, 5, 25, 15, 54, 31, new TimeSpan(-01, 00, 00));

            var dataset = new DicomDataset(
                new DicomDate(DicomTag.CreationDate, "20160525"),
                new DicomTime(DicomTag.CreationTime, "155431"),
                new DicomShortString(DicomTag.TimezoneOffsetFromUTC, "-0100"));

            var actual = dataset.GetDateTimeOffset(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTimeOffset_DateAndTimeAndNoTimezoneAvailable_ReturnsSpecifiedDateTimeInLocalTimezone()
        {
            var local = DateTimeOffset.Now.Offset;
            var expected = new DateTimeOffset(2016, 5, 25, 15, 54, 31, local);

            var dataset = new DicomDataset(
                new DicomDate(DicomTag.CreationDate, "20160525"),
                new DicomTime(DicomTag.CreationTime, "155431"));

            var actual = dataset.GetDateTimeOffset(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTimeOffset_MissingTimeWithTimezone_ReturnsSpecifiedDate()
        {
            var expected = new DateTimeOffset(2016, 5, 25, 0, 0, 0, new TimeSpan(-09, 00, 00));

            var dataset = new DicomDataset(
                new DicomDate(DicomTag.CreationDate, "20160525"),
                new DicomShortString(DicomTag.TimezoneOffsetFromUTC, "-0900"));

            var actual = dataset.GetDateTimeOffset(DicomTag.CreationDate, DicomTag.CreationTime);

            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
