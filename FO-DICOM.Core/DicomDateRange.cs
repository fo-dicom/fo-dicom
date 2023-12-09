// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    public class DicomDateRange : DicomRange<DateTime>
    {
        public DicomDateRange()
            : base(DateTime.MinValue, DateTime.MaxValue)
        {
        }

        public DicomDateRange(DateTime min, DateTime max)
            : base(min, max)
        {
        }

        public override string ToString()
        {
            return ToString("yyyyMMddHHmmss");
        }

        public string ToString(string format)
        {
            var value = (Minimum == DateTime.MinValue ? string.Empty : Minimum.ToString(format)) + "-"
                        + (Maximum == DateTime.MaxValue ? string.Empty : Maximum.ToString(format));
            if (value == "-") return string.Empty;
            return value;
        }
    }
}
