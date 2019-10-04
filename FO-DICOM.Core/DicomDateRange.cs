// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
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
            var value = (Minimum == DateTime.MinValue ? String.Empty : Minimum.ToString(format)) + "-"
                        + (Maximum == DateTime.MaxValue ? String.Empty : Maximum.ToString(format));
            if (value == "-") return String.Empty;
            return value;
        }
    }
}
