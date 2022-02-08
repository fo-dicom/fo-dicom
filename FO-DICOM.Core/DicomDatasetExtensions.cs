// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
            => new DicomDataset(dataset, false)
            {
                InternalTransferSyntax = dataset.InternalTransferSyntax
            };

        /// <summary>
        /// Get a composite <see cref="DateTime"/> instance based on <paramref name="date"/> and <paramref name="time"/> values.
        /// </summary>
        /// <param name="dataset">Dataset from which data should be retrieved.</param>
        /// <param name="date">Tag associated with date value.</param>
        /// <param name="time">Tag associated with time value.</param>
        /// <returns>Composite <see cref="DateTime"/>.</returns>
        public static DateTime GetDateTime(this DicomDataset dataset, DicomTag date, DicomTag time)
        {
            var dd = dataset.GetDicomItem<DicomDate>(date);
            var dt = dataset.GetDicomItem<DicomTime>(time);

            var da = dd != null && dd.Count > 0 ? dd.Get<DateTime>(0) : DateTime.MinValue;
            var tm = dt != null && dt.Count > 0 ? dt.Get<DateTime>(0) : DateTime.MinValue;

            return new DateTime(da.Year, da.Month, da.Day, tm.Hour, tm.Minute, tm.Second);
        }

        /// <summary>
        /// Get a composite <see cref="DateTimeOffset"/> instance based on <paramref name="date"/> and <paramref name="time"/> values.
        /// This will take any time zone information specified in the dataset into account
        /// </summary>
        /// <param name="dataset">Dataset from which data should be retrieved.</param>
        /// <param name="date">Tag associated with date value.</param>
        /// <param name="time">Tag associated with time value.</param>
        /// <returns>Composite <see cref="DateTimeOffset"/>.</returns>
        public static DateTimeOffset GetDateTimeOffset(this DicomDataset dataset, DicomTag date, DicomTag time)
        {
            var dd = dataset.GetDicomItem<DicomDate>(date);
            var dt = dataset.GetDicomItem<DicomTime>(time);
            var tz = dataset.GetDicomItem<DicomShortString>(DicomTag.TimezoneOffsetFromUTC);

            var da = dd != null && dd.Count > 0 ? dd.Get<DateTime>(0) : DateTime.MinValue;
            var tm = dt != null && dt.Count > 0 ? dt.Get<DateTime>(0) : DateTime.MinValue;

            TimeSpan offset;
            if (tz != null && tz.Count > 0)
            {
                // Explicit timezone information present in dataset
                string s = tz.Get<string>();
                int hh = int.Parse(s.Substring(0, 3));
                int mm = int.Parse(s.Substring(3, 2));

                offset = new TimeSpan(hh, mm, 00);
            }
            else
            {
                // Use local timezone when no explicit timezone info in dataset
                offset = DateTimeOffset.Now.Offset;
            }

            return new DateTimeOffset(da.Year, da.Month, da.Day, tm.Hour, tm.Minute, tm.Second, offset);
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
