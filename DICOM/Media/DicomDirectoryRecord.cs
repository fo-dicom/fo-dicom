using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Media {
	public class DicomDirectoryRecord : DicomDataset {
		#region Properties

		public DicomDirectoryRecord LowerLevelDirectoryRecord { get; set; }

		public DicomDirectoryRecord NextDirectoryRecord { get; set; }

		public DicomDirectoryRecordCollection LowerLevelDirectoryRecordCollection {
			get { return new DicomDirectoryRecordCollection(LowerLevelDirectoryRecord); }
		}

		public uint Offset { get; internal set; }

		public string DirectoryRecordType {
			get { return Get<string>(DicomTag.DirectoryRecordType); }
		}

		#endregion

		public DicomDirectoryRecord() {
		}

		public DicomDirectoryRecord(IEnumerable<DicomItem> items) : base(items) {
		}


		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Directory Record Type: {0}, Lower level items:", DirectoryRecordType, LowerLevelDirectoryRecordCollection.Count());
			return sb.ToString();
		}
	}
}