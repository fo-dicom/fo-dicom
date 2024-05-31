// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom
{

    /// <summary>
    /// <see cref="DicomDataset"/> extension methods.
    /// </summary>
    public static class DicomDatasetExtensions
    {

        /// <summary>
        /// Clone a dataset.
        /// </summary>
        /// <param name="dataset">Dataset to be cloned.</param>
        /// <returns>Clone of dataset.</returns>
        public static DicomDataset Clone(this DicomDataset dataset)
        {
            var clone = new DicomDataset(dataset, false)
            {
                InternalTransferSyntax = dataset.InternalTransferSyntax,
                FallbackEncodings = dataset.FallbackEncodings
            };
            return clone;
        }

        /// <summary>
        /// Get a composite <see cref="System.DateTime"/> instance based on <paramref name="date"/> and <paramref name="time"/> values.
        /// </summary>
        /// <param name="dataset">Dataset from which data should be retrieved.</param>
        /// <param name="date">Tag associated with date value.</param>
        /// <param name="time">Tag associated with time value.</param>
        /// <returns>Composite <see cref="System.DateTime"/>.</returns>
        public static DateTime GetDateTime(this DicomDataset dataset, DicomTag date, DicomTag time)
        {
            var dd = dataset.GetDicomItem<DicomDate>(date);
            var dt = dataset.GetDicomItem<DicomTime>(time);

            var da = dd != null && dd.Count > 0 ? dd.Get<DateTime>(0) : DateTime.MinValue;
            var tm = dt != null && dt.Count > 0 ? dt.Get<DateTime>(0) : DateTime.MinValue;

            return new DateTime(da.Year, da.Month, da.Day, tm.Hour, tm.Minute, tm.Second);
        }

        /// <summary>
        /// Get a composite <see cref="System.DateTimeOffset"/> instance based on <paramref name="date"/> and <paramref name="time"/> values.
        /// This will take any time zone information specified in the dataset into account. 
        /// If the dataset is a child sequence item, the <paramref name="topLevelDataset"/> must be specified to find time zone information.
        /// </summary>
        /// <param name="dataset">Dataset from which data should be retrieved.</param>
        /// <param name="date">Tag associated with date value.</param>
        /// <param name="time">Tag associated with time value.</param>
        /// <param name="topLevelDataset">The top-level dataset (if different from the current dataset). This is where the time zone information will be located</param>
        /// <returns>Composite <see cref="System.DateTimeOffset"/>.</returns>
        public static DateTimeOffset GetDateTimeOffset(this DicomDataset dataset, DicomTag date, DicomTag time, DicomDataset topLevelDataset = null)
        {
            var datetime = GetDateTime(dataset, date, time);
            if (datetime == DateTime.MinValue)
            {
                // Don't specify timezone when no date or time is present. This will make
                // the behavior consistent with GetDateTime extension
                return DateTimeOffset.MinValue;
            }

            var timezone = (topLevelDataset ?? dataset).GetDicomItem<DicomShortString>(DicomTag.TimezoneOffsetFromUTC);
            if (timezone != null && timezone.Count > 0)
            {
                // Explicit timezone information present in dataset
                string s = timezone.Get<string>();
                int hh = int.Parse(s.Substring(0, 3));
                int mm = int.Parse(s.Substring(3, 2));

                var offset = new TimeSpan(hh, mm, 00);
                return new DateTimeOffset(datetime, offset);
            }
            else
            {
                // Use local timezone when no explicit timezone info in dataset
                // Note! The local timezone may change with daylight savings, thus we need 
                // to get the offset for the specific date. Fortunately, this is
                // the default of this class
                return new DateTimeOffset(datetime);
            }
        }

        /// <summary>
        /// Enumerates DICOM items matching mask.
        /// </summary>
        /// <param name="dataset">Dataset from which masked items should be retrieved.</param>
        /// <param name="mask">Requested mask.</param>
        /// <returns>Enumeration of masked DICOM items.</returns>
        public static IEnumerable<DicomItem> EnumerateMasked(this DicomDataset dataset, DicomMaskedTag mask)
            => dataset.Where(x => mask.IsMatch(x.Tag));

        /// <summary>
        /// Enumerates DICOM items for specified group.
        /// </summary>
        /// <param name="dataset">Dataset from which group items should be retrieved.</param>
        /// <param name="group">Requested group.</param>
        /// <returns>Enumeration of DICOM items for specified <paramref name="group"/>.</returns>
        public static IEnumerable<DicomItem> EnumerateGroup(this DicomDataset dataset, ushort group)
            => dataset.Where(x => x.Tag.Group == group && x.Tag.Element != 0x0000);


        /// <summary>
        /// Turns off validation on the passed DicomDataset and returns this Dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static DicomDataset NotValidated(this DicomDataset dataset)
        {
            dataset.ValidateItems = false;
            return dataset;
        }

        /// <summary>
        /// Turns on validation on the passed DicomDataset and returns this Dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static DicomDataset Validated(this DicomDataset dataset)
        {
            dataset.ValidateItems = true;
            return dataset;
        }


    }
}
