// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
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
            var expected = new DateTimeOffset(new DateTime(2016, 5, 25, 15, 54, 31));

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

        [Fact]
        public void GetDateTimeOffset_NeitherDateNorTime_ReturnsMinValue()
        {
            var expected = DateTimeOffset.MinValue;

            var dataset = new DicomDataset(
                new DicomShortString(DicomTag.TimezoneOffsetFromUTC, "-0900"));

            var actual = dataset.GetDateTimeOffset(DicomTag.StudyDate, DicomTag.StudyTime);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTimeOffset_TopLevelDataset_With_TimeZone_Is_Applied()
        {
            var expected = new DateTimeOffset(2016, 5, 25, 14, 30, 0, new TimeSpan(-09, 00, 00));

            var scheduledProcedure = new DicomDataset()
            {
                { DicomTag.ScheduledProcedureStepStartDate, "20160525" },
                { DicomTag.ScheduledProcedureStepStartTime, "143000" }
            };

            var dataset = new DicomDataset(
                new DicomDate(DicomTag.CreationDate, "20160524"),
                new DicomShortString(DicomTag.TimezoneOffsetFromUTC, "-0900"),
                new DicomSequence(DicomTag.ScheduledProcedureStepSequence, scheduledProcedure));

            var actual = scheduledProcedure.GetDateTimeOffset(DicomTag.ScheduledProcedureStepStartDate, DicomTag.ScheduledProcedureStepStartTime, dataset);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
