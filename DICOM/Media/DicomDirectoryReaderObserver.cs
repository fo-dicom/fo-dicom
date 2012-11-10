using Dicom.IO.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Media
{
    public class DicomDirectoryReaderObserver : IDicomReaderObserver
    {
        private DicomSequence _directoryRecordSequence = null;
        private Stack<DicomTag> _currentSequenceTag = new Stack<DicomTag>();
        private Dictionary<uint, DicomDataset> _lookup = new Dictionary<uint, DicomDataset>();
        private DicomDataset _dataset;

        public DicomDirectoryReaderObserver(DicomDataset dataset)
        {
            _dataset = dataset;
        }

        public DirectoryRecordSequenceItem BuildDirectoryRecords()
        {
            uint offset = 0;
            offset = _dataset.Get<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity);

            return ParseDirectoryRecord(offset);
        }

        private DirectoryRecordSequenceItem ParseDirectoryRecord(uint offset)
        {
            DirectoryRecordSequenceItem record = null;
            if (_lookup.ContainsKey(offset))
            {
                record = new DirectoryRecordSequenceItem(_lookup[offset]);
                record.Offset = offset;

                record.NextDirectoryRecord = ParseDirectoryRecord(record.Get<uint>(DicomTag.OffsetOfTheNextDirectoryRecord));

                record.LowerLevelDirectoryRecord = ParseDirectoryRecord(record.Get<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity));
            }

            return record;
        }

        #region IDicomReaderObserver Implementation

        public void OnElement(IO.IByteSource source, DicomTag tag, DicomVR vr, IO.Buffer.IByteBuffer data)
        {

        }

        public void OnBeginSequence(IO.IByteSource source, DicomTag tag, uint length)
        {
            _currentSequenceTag.Push(tag);
            if (tag == DicomTag.DirectoryRecordSequence)
            {
                _directoryRecordSequence = _dataset.Get<DicomSequence>(tag);
            }
        }

        public void OnBeginSequenceItem(IO.IByteSource source, uint length)
        {
            if (_currentSequenceTag.Peek() == DicomTag.DirectoryRecordSequence && _directoryRecordSequence != null)
            {
                _lookup.Add((uint)source.Position - 8, _directoryRecordSequence.LastOrDefault());
            }
        }

        public void OnEndSequenceItem()
        {

        }

        public void OnEndSequence()
        {

        }

        public void OnBeginFragmentSequence(IO.IByteSource source, DicomTag tag, DicomVR vr)
        {

        }

        public void OnFragmentSequenceItem(IO.IByteSource source, IO.Buffer.IByteBuffer data)
        {

        }

        public void OnEndFragmentSequence()
        {

        }

        #endregion
    }
}
