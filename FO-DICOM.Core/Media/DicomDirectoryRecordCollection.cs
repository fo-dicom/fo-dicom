// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;

namespace FellowOakDicom.Media
{

    public class DicomDirectoryRecordCollection : IEnumerable<DicomDirectoryRecord>
    {

        #region Properties and Attributes

        private readonly DicomDirectoryRecord _firstRecord;

        #endregion

        #region Constructors

        internal DicomDirectoryRecordCollection(DicomDirectoryRecord firstRecord)
        {
            _firstRecord = firstRecord;
        }

        #endregion

        #region Enumerator Class

        internal class DicomDirectoryRecordEnumerator : IEnumerator<DicomDirectoryRecord>
        {
            #region Properties and Attributes

            private readonly DicomDirectoryRecord _head;
            private bool _atEnd;

            #endregion

            #region Constructors

            internal DicomDirectoryRecordEnumerator(DicomDirectoryRecord head)
            {
                _head = head;
            }

            #endregion

            #region IEnumerator<DirectoryRecordSequenceItem> Members

            public DicomDirectoryRecord Current { get; private set; }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                Current = null;
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_head == null) return false;

                if (_atEnd) return false;

                if (Current == null)
                {
                    Current = _head;
                    return true;
                }

                if (Current.NextDirectoryRecord == null)
                {
                    _atEnd = true;
                    return false;
                }

                Current = Current.NextDirectoryRecord;

                return true;
            }

            public void Reset()
            {
                Current = null;
                _atEnd = false;
            }

            #endregion
        }

        #endregion

        #region IEnumerable<DirectoryRecordSequenceItem> Members

        public IEnumerator<DicomDirectoryRecord> GetEnumerator()
        {
            return new DicomDirectoryRecordEnumerator(_firstRecord);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
