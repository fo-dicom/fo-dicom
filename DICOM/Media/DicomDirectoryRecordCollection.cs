using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Media {
	public class DicomDirectoryRecordCollection : IEnumerable<DicomDirectoryRecord> {
		#region Properties and Attributes
		private DicomDirectoryRecord _firstRecord;
		#endregion

		#region Constructors
		internal DicomDirectoryRecordCollection(DicomDirectoryRecord firstRecord) {
			_firstRecord = firstRecord;
		}
		#endregion

		#region Enumerator Class

		internal class DicomDirectoryRecordEnumerator : IEnumerator<DicomDirectoryRecord> {
			#region Properties and Attributes

			private readonly DicomDirectoryRecord _head;
			private DicomDirectoryRecord _current;
			private bool _atEnd;

			#endregion

			#region Constructors

			internal DicomDirectoryRecordEnumerator(DicomDirectoryRecord head) {
				_head = head;
			}

			#endregion

			#region IEnumerator<DirectoryRecordSequenceItem> Members

			public DicomDirectoryRecord Current {
				get { return _current; }
			}

			#endregion

			#region IDisposable Members

			public void Dispose() {
				_current = null;
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current {
				get { return _current; }
			}

			public bool MoveNext() {
				if (_head == null)
					return false;

				if (_atEnd)
					return false;

				if (_current == null) {
					_current = _head;
					return true;
				}

				if (_current.NextDirectoryRecord == null) {
					_atEnd = true;
					return false;
				}

				_current = _current.NextDirectoryRecord;

				return true;
			}

			public void Reset() {
				_current = null;
				_atEnd = false;
			}

			#endregion
		}

		#endregion

		#region IEnumerable<DirectoryRecordSequenceItem> Members

		public IEnumerator<DicomDirectoryRecord> GetEnumerator() {
			return new DicomDirectoryRecordEnumerator(_firstRecord);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		#endregion
	}
}