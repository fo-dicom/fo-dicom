// Copyright (c) 2012-2019 fo-dicom contributors.
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
        {
            return new DicomDataset(dataset, false)
            {
                InternalTransferSyntax = dataset.InternalTransferSyntax
            };
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
            var dd = dataset.GetDicomItem<DicomDate>(date);
            var dt = dataset.GetDicomItem<DicomTime>(time);

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
