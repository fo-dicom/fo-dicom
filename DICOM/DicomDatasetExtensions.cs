// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            var ds = new DicomDataset(dataset);
            ds.InternalTransferSyntax = dataset.InternalTransferSyntax;
            return ds;
        }

        /// <summary>
        /// Get a composite <see cref="DateTime"/> instance based on <paramref name="date"/> and <paramref name="time"/> values.
        /// </summary>
        /// <param name="dataset">Dataset from which data should be retrieved.</param>
        /// <param name="date">Tag associated with date value.</param>
        /// <param name="time">Tag associated with time value.</param>
        /// <returns>Composite <see cref="DateTime"/>.</returns>
        public static DateTime GetDateTime(this DicomDataset dataset, DicomTag date, DicomTag time)
        {
            var dd = dataset.Contains(date) ? dataset.Get<DicomDate>(date) : null;
            var dt = dataset.Contains(time) ? dataset.Get<DicomTime>(time) : null;

            var da = dd != null && dd.Count > 0 ? dd.Get<DateTime>(0) : DateTime.MinValue;
            var tm = dt != null && dt.Count > 0 ? dt.Get<DateTime>(0) : DateTime.MinValue;

            return new DateTime(da.Year, da.Month, da.Day, tm.Hour, tm.Minute, tm.Second);
        }

        /// <summary>
        /// Enumerates DICOM items matching mask.
        /// </summary>
        /// <param name="dataset">Dataset from which masked items should be retrieved.</param>
        /// <param name="mask">Requested mask.</param>
        /// <returns>Enumeration of masked DICOM items.</returns>
        public static IEnumerable<DicomItem> EnumerateMasked(this DicomDataset dataset, DicomMaskedTag mask)
        {
            return dataset.Where(x => mask.IsMatch(x.Tag));
        }

        /// <summary>
        /// Enumerates DICOM items for specified group.
        /// </summary>
        /// <param name="dataset">Dataset from which group items should be retrieved.</param>
        /// <param name="group">Requested group.</param>
        /// <returns>Enumeration of DICOM items for specified <paramref name="group"/>.</returns>
        public static IEnumerable<DicomItem> EnumerateGroup(this DicomDataset dataset, ushort group)
        {
            return dataset.Where(x => x.Tag.Group == group && x.Tag.Element != 0x0000);
        }
    }
}
