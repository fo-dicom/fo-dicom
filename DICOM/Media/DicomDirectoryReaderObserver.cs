// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Linq;

using Dicom.IO;
using Dicom.IO.Reader;
using Dicom.IO.Buffer;

namespace Dicom.Media
{
    public class DicomDirectoryReaderObserver : IDicomReaderObserver
    {
        private DicomSequence _directoryRecordSequence = null;

        private readonly Stack<DicomTag> _currentSequenceTag = new Stack<DicomTag>();

        private readonly Dictionary<uint, DicomDataset> _lookup = new Dictionary<uint, DicomDataset>();

        private readonly DicomDataset _dataset;

        public DicomDirectoryReaderObserver(DicomDataset dataset)
        {
            _dataset = dataset;
        }

        public DicomDirectoryRecord BuildDirectoryRecords()
        {
            var notFoundOffsets = new SortedSet<uint>();
            var offset = _dataset.GetSingleValue<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity);
            DicomDirectoryRecord root = ParseDirectoryRecord(offset, notFoundOffsets);

            if (_lookup.Count > 0)
            {
                // There are unresolved sequences. Try to resolve them with non exact offset match
                ParseDirectoryRecordNotExact(ref root, offset, notFoundOffsets);
            }

            return root;
        }

        private DicomDirectoryRecord ParseDirectoryRecord(uint offset, SortedSet<uint> notFoundOffsets)
        {
            DicomDirectoryRecord record = null;
            if (!_lookup.TryGetValue(offset, out var dataset))
            {
                notFoundOffsets.Add(offset);
            }
            else
            {
                record = new DicomDirectoryRecord(dataset)
                {
                    Offset = offset
                };

                _lookup.Remove(offset);

                record.NextDirectoryRecord =
                    ParseDirectoryRecord(record.GetSingleValue<uint>(DicomTag.OffsetOfTheNextDirectoryRecord), notFoundOffsets);

                record.LowerLevelDirectoryRecord =
                    ParseDirectoryRecord(record.GetSingleValue<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity), notFoundOffsets);
            }

            return record;
        }

        private void ParseDirectoryRecordNotExact(ref DicomDirectoryRecord root, uint rootOffset, SortedSet<uint> offsets)
        {
            // Collect all used offsets and turn them into a dictionary with the next larger offset as value
            foreach (var dataset in _lookup.Values)
            {
                var offset = dataset.GetSingleValue<uint>(DicomTag.OffsetOfTheNextDirectoryRecord);
                if (offset > 0)
                    offsets.Add(offset);
                offset = dataset.GetSingleValue<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity);
                if (offset > 0)
                    offsets.Add(offset);
            }
            var offsetIntervals = new Dictionary<uint, uint>(offsets.Count);
            var enumerator = offsets.GetEnumerator();
            if (enumerator.MoveNext())
            {
                uint currentOffset = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    var nextOffset = enumerator.Current;
                    offsetIntervals.Add(currentOffset, nextOffset);
                    currentOffset = nextOffset;
                }
                offsetIntervals.Add(currentOffset, uint.MaxValue);
            }

            ParseDirectoryRecord(ref root, rootOffset, offsetIntervals);
        }

        private void ParseDirectoryRecord(ref DicomDirectoryRecord record, uint offset, Dictionary<uint, uint> offsetIntervals)
        {
            if (record == null)
            {
                // Find the best matching existing offset for the given offset
                // Because we collected all used offsets, we know it is in the offsetIntervals dictionary and gives us
                // the next used offset as upper bound of the search interval
                offset = _lookup.Keys.FirstOrDefault(key => key >= offset && key < offsetIntervals[offset]);
                if (!_lookup.TryGetValue(offset, out var dataset))
                {
                    return;
                }

                record = new DicomDirectoryRecord(dataset)
                {
                    Offset = offset
                };

                _lookup.Remove(offset);
            }

            var nextOffset = record.GetSingleValue<uint>(DicomTag.OffsetOfTheNextDirectoryRecord);
            if (nextOffset > 0)
            {
                DicomDirectoryRecord next = record.NextDirectoryRecord;
                ParseDirectoryRecord(ref next, nextOffset, offsetIntervals);
                record.NextDirectoryRecord = next;
            }

            var lowerLevelOffset = record.GetSingleValue<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity);
            if (lowerLevelOffset > 0)
            {
                DicomDirectoryRecord lowerLevel = record.LowerLevelDirectoryRecord;
                ParseDirectoryRecord(ref lowerLevel, lowerLevelOffset, offsetIntervals);
                record.LowerLevelDirectoryRecord = lowerLevel;
            }
        }

        #region IDicomReaderObserver Implementation

        public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data)
        {
            // do nothing here
        }

        public void OnBeginSequence(IByteSource source, DicomTag tag, uint length)
        {
            _currentSequenceTag.Push(tag);
            if (tag == DicomTag.DirectoryRecordSequence)
            {
                _directoryRecordSequence = _dataset.GetDicomItem<DicomSequence>(tag);
            }
        }

        public void OnBeginSequenceItem(IByteSource source, uint length)
        {
            if (_currentSequenceTag.Peek() == DicomTag.DirectoryRecordSequence && _directoryRecordSequence != null)
            {
                _lookup.Add((uint)source.Position - 8, _directoryRecordSequence.LastOrDefault());
            }
        }

        public void OnEndSequenceItem()
        {
            // do nothing here
        }

        public void OnEndSequence()
        {
            _currentSequenceTag.Pop();
        }

        public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr)
        {
            // do nothing here
        }

        public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data)
        {
            // do nothing here
        }

        public void OnEndFragmentSequence()
        {
            // do nothing here
        }

        #endregion
    }
}
