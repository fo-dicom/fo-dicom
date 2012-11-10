using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Media
{
    public class DirectoryRecordSequenceItem : DicomDataset
    {
        #region Properties

        public DirectoryRecordSequenceItem LowerLevelDirectoryRecord { get; set; }

        public DirectoryRecordSequenceItem NextDirectoryRecord { get; set; }

        public DirectoryRecordCollection LowerLevelDirectoryRecordCollection
        {
            get { return new DirectoryRecordCollection(LowerLevelDirectoryRecord); }
        }
        public uint Offset { get; internal set; }

        public DirectoryRecordType DirectoryRecordType
        {
            get { return null; }
        }

        #endregion

        public DirectoryRecordSequenceItem()
        {

        }
        public DirectoryRecordSequenceItem(IEnumerable<DicomItem> items)
            : base(items)
        {
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }
    }
}
