// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Media
{

    public class DicomDirectoryRecord : DicomDataset
    {

        #region Properties

        public DicomDirectoryRecord LowerLevelDirectoryRecord { get; set; }

        public DicomDirectoryRecord NextDirectoryRecord { get; set; }

        public DicomDirectoryRecordCollection LowerLevelDirectoryRecordCollection
            => new DicomDirectoryRecordCollection(LowerLevelDirectoryRecord);

        public uint Offset { get; internal set; }

        public string DirectoryRecordType
            => GetSingleValue<string>(DicomTag.DirectoryRecordType);

        #endregion

        public DicomDirectoryRecord()
        {
        }

        internal DicomDirectoryRecord(bool validateItems)
            : base(Enumerable.Empty<DicomItem>(), validateItems)
        {
            ValidateItems = validateItems;
        }

        public DicomDirectoryRecord(DicomDataset dataset)
            : base(dataset, dataset.ValidateItems)
        {
        }

        public DicomDirectoryRecord(IEnumerable<DicomItem> items)
            : base(items)
        {
        }


        public override string ToString()
        {
            return $"Directory Record Type: {DirectoryRecordType}, Lower level items: {LowerLevelDirectoryRecordCollection.Count()}";
        }
    }
}
