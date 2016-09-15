// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

using Xunit;

namespace Dicom.Bugs
{
    [Collection("General")]
    public class GH328
    {
        [Fact]
        public void DicomDateTime_FractionalSecondsAndTimezone_SupportedFormat()
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmmss.ffffffzzz");
            var dataset = new DicomDataset { new DicomDateTime(DicomTag.ScheduledProcedureStepStartDateTime, dt) };

            var exception = Record.Exception(() => dataset.Get<DicomDateRange>(DicomTag.ScheduledProcedureStepStartDateTime));
            Assert.Null(exception);
        }
    }
}
