// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    NextDirectoryRecord?.Dispose();
                    LowerLevelDirectoryRecord?.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (NextDirectoryRecord != null)
                    {
                        await NextDirectoryRecord.DisposeAsync();
                    }

                    if (LowerLevelDirectoryRecord != null)
                    {
                        await LowerLevelDirectoryRecord.DisposeAsync();
                    }
                }
            }
            finally
            {
                await base.DisposeAsync(disposing);
            }
        }
        
        public override string ToString()
        {
            return $"Directory Record Type: {DirectoryRecordType}, Lower level items: {LowerLevelDirectoryRecordCollection.Count()}";
        }
    }
}
