// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    public class DicomRange<T>
        where T : IComparable<T>
    {
        public DicomRange(T min, T max)
        {
            Minimum = min;
            Maximum = max;
        }

        public T Minimum { get; set; }

        public T Maximum { get; set; }

        public bool Contains(T value)
        {
            return Minimum.CompareTo(value) <= 0 && Maximum.CompareTo(value) >= 0;
        }

        public void Join(T value)
        {
            if (Minimum.CompareTo(value) > 0)
            {
                Minimum = value;
            }
            if (Maximum.CompareTo(value) < 0)
            {
                Maximum = value;
            }
        }

    }
}
