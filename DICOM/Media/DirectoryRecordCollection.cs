using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Media
{
    public class DirectoryRecordCollection : IEnumerable<DirectoryRecordSequenceItem>
    {
        #region Properties and Attributes
        private DirectoryRecordSequenceItem _firstRecord;
        #endregion

        #region Constructors
        internal DirectoryRecordCollection(DirectoryRecordSequenceItem firstRecord)
        {
            _firstRecord = firstRecord;
        }
        #endregion

        #region Enumerator Class

        internal class DirectoryRecordEnumerator : IEnumerator<DirectoryRecordSequenceItem>
        {
            #region Properties and Attributes

            private readonly DirectoryRecordSequenceItem _head;
            private DirectoryRecordSequenceItem _current;
            private bool _atEnd;

            #endregion

            #region Constructors

            internal DirectoryRecordEnumerator(DirectoryRecordSequenceItem head)
            {
                _head = head;
            }

            #endregion

            #region IEnumerator<DirectoryRecordSequenceItem> Members

            public DirectoryRecordSequenceItem Current
            {
                get { return _current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                _current = null;
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return _current; }
            }

            public bool MoveNext()
            {
                if (_head == null)
                    return false;

                if (_atEnd)
                    return false;

                if (_current == null)
                {
                    _current = _head;
                    return true;
                }

                if (_current.NextDirectoryRecord == null)
                {
                    _atEnd = true;
                    return false;
                }

                _current = _current.NextDirectoryRecord;

                return true;
            }

            public void Reset()
            {
                _current = null;
                _atEnd = false;
            }

            #endregion
        }

        #endregion

        #region IEnumerable<DirectoryRecordSequenceItem> Members

        public IEnumerator<DirectoryRecordSequenceItem> GetEnumerator()
        {
            return new DirectoryRecordEnumerator(_firstRecord);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
