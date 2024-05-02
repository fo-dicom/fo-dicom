// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH328
    {

        [Fact]
        public void DicomDateTime_FractionalSecondsAndTimezone_SupportedFormat()
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmmss.ffffffzzz");
            if (dt.Contains("-"))
            {
                // the test is currently executed in a time zone with negative offset.
                // negative time offset are not allowed in DateTimeRange since the "-" is
                // reserved for the range-delimiter
                dt = dt.Replace('-', '+');
            }

            // Remove invalid ':' character from the time zone
            dt = dt.Replace(":", string.Empty);

            var dataset = new DicomDataset { new DicomDateTime(DicomTag.ScheduledProcedureStepStartDateTime, dt) };

            var exception = Record.Exception(() => dataset.GetSingleValue<DicomDateRange>(DicomTag.ScheduledProcedureStepStartDateTime));
            Assert.Null(exception);
        }

    }
}
