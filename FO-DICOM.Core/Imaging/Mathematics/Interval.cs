// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Represents an interval of double values
    /// </summary>
    public class IntervalD
    {

        /// <summary>
        /// The lower bound of the interval
        /// </summary>
        public double Min { get; private set; }

        /// <summary>
        /// The upper bound of the interval
        /// </summary>
        public double Max { get; private set; }


        public IntervalD(double min, double max)
        {
            Min = min;
            Max = max;
        }


        /// <summary>
        /// Returns true if the value is between Min and Max including the boundries
        /// </summary>
        public bool Contains(double value)
            => Min <= value && value <= Max;

        public double Center
            => (Min + Max) / 2;

        public double Width
            => Max - Min;

    }
}
