// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Numerics;

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Represents an interval of double values
    /// </summary>
    public class Interval<T> where T : INumber<T>
    {

        /// <summary>
        /// The lower bound of the interval
        /// </summary>
        public T Min { get; private set; }

        /// <summary>
        /// The upper bound of the interval
        /// </summary>
        public T Max { get; private set; }


        public Interval(T min, T max)
        {
            Min = min;
            Max = max;
        }


        /// <summary>
        /// Returns true if the value is between Min and Max including the boundries
        /// </summary>
        public bool Contains(T value)
            => Min <= value && value <= Max;

        public T Center
            => (Min + Max) / (T.One + T.One);

        public T Width
            => Max - Min;

    }

}
