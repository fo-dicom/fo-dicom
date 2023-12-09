// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.IO.Writer
{

    public static class DicomDatasetExtensions
    {

        /// <summary>
        /// Recalculates the group length element for the specified group. Removes the group length element if no elements exist for the specified group.
        /// </summary>
        /// <remarks>For most use cases, this method will be called by the library as needed and will not need to be called by the user.</remarks>
        /// <param name="dataset">DICOM dataset</param>
        /// <param name="group">DICOM tag group</param>
        /// <param name="createIfMissing">If set to <c>true</c>, the group length element will be created if missing. If set to <c>false</c>, the group length will only be calculated if the group length element exists.</param>
        public static void RecalculateGroupLength(this DicomDataset dataset, ushort group, bool createIfMissing = true)
        {
            var items = dataset.EnumerateGroup(group).ToList();

            if (items.Count == 0)
            {
                // no items exist for the specified group; remove
                dataset.Remove(new DicomTag(group, 0x0000));
                return;
            }

            var calculator = new DicomWriteLengthCalculator(dataset.InternalTransferSyntax, DicomWriteOptions.Default);

            uint length = calculator.Calculate(items);
            dataset.AddOrUpdate(new DicomTag(group, 0x0000), length);
        }

        /// <summary>
        /// Recalculates the group lengths for all groups in the <paramref name="dataset"/>. Removes any group lengths for groups with out elements.
        /// </summary>
        /// <remarks>For most use cases, this method will be called by the library as needed and will not need to be called by the user.</remarks>
        /// <param name="dataset">DICOM dataset</param>
        /// <param name="createIfMissing">If set to <c>true</c>, group length elements will be created if missing. If set to <c>false</c>, only groups with existing group lengths will be recalculated.</param>
        public static void RecalculateGroupLengths(this DicomDataset dataset, bool createIfMissing = true)
        {
            // recalculate group lengths for sequences first
            foreach (var sq in dataset.Where(x => x.ValueRepresentation == DicomVR.SQ).Cast<DicomSequence>())
            {
                foreach (var item in sq.Items)
                {
                    item.RecalculateGroupLengths(createIfMissing);
                }
            }

            var calculator = new DicomWriteLengthCalculator(dataset.InternalTransferSyntax, DicomWriteOptions.Default);

            IEnumerable<ushort> groups = null;
            if (createIfMissing) groups = dataset.Select(x => x.Tag.Group).Distinct();
            else groups = dataset.Where(x => x.Tag.Element == 0x0000).Select(x => x.Tag.Group);

            foreach (var group in groups.ToList())
            {
                var items = dataset.EnumerateGroup(group).ToList();

                if (items.Count == 0)
                {
                    // no items exist for the specified group; remove
                    dataset.Remove(new DicomTag(group, 0x0000));
                    return;
                }

                uint length = calculator.Calculate(items);
                dataset.AddOrUpdate(new DicomTag(group, 0x0000), length);
            }
        }

        /// <summary>
        /// Removes group length elements from the DICOM dataset.
        /// </summary>
        /// <remarks>For most use cases, this method will be called by the library as needed and will not need to be called by the user.</remarks>
        /// <param name="dataset">DICOM dataset</param>
        /// <param name="firstLevelOnly">If set to <c>true</c>, group length elements will only be removed from the first level of the dataset. If set to <c>false</c>, group lengths will also be removed from sequence items.</param>
        public static void RemoveGroupLengths(this DicomDataset dataset, bool firstLevelOnly = false)
        {
            dataset.Remove(x => x.Tag.Element == 0x0000);

            if (!firstLevelOnly)
            {
                // remove group lengths from sequences
                foreach (var sq in dataset.Where(x => x.ValueRepresentation == DicomVR.SQ).Cast<DicomSequence>())
                {
                    foreach (var item in sq.Items)
                    {
                        item.RemoveGroupLengths(firstLevelOnly);
                    }
                }
            }
        }
    }
}
