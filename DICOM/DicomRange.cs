﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
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
    }
}
