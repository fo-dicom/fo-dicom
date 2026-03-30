// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Reader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Media
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
            var notFoundOffsets = new List<uint>();
            var offset = _dataset.GetSingleValue<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity);
            var root = ParseDirectoryRecord(offset, notFoundOffsets);

            if (_lookup.Count > 0 && notFoundOffsets.Count > 0)
            {
                // There are unresolved sequences. Try to resolve them with non exact offset match
                root = ParseDirectoryRecordNotExact(root, offset);
            }

            return root;
        }


        private DicomDirectoryRecord ParseDirectoryRecord(uint offset, List<uint> notFoundOffsets)
        {
            DicomDirectoryRecord record = null;
            if (_lookup.TryGetValue(offset, out var dataset))
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
            else
            {
                if (offset != 0)
                {
                    notFoundOffsets.Add(offset);
                }
            }

            return record;
        }

        private DicomDirectoryRecord ParseDirectoryRecordNotExact(DicomDirectoryRecord record, uint offset)
        {
            if (record == null)
            {
                // Find the closest existing offset to the given wrong offset
                uint? bestOffset = null;
                uint bestDistance = uint.MaxValue;
                foreach (var key in _lookup.Keys)
                {
                    var currentDistance = key > offset ? key - offset : offset - key; Math.Abs((int)key - (int)offset);
                    if (bestOffset == null || currentDistance < bestDistance)
                    {
                        bestOffset = key;
                        bestDistance = currentDistance;
                    }
                }

                offset = bestOffset ?? _lookup.Keys.FirstOrDefault();

                if (!_lookup.TryGetValue(offset, out var dataset))
                {
                    return null;
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
                record.NextDirectoryRecord = ParseDirectoryRecordNotExact(record.NextDirectoryRecord, nextOffset);
            }

            var lowerLevelOffset = record.GetSingleValue<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity);
            if (lowerLevelOffset > 0)
            {
                record.LowerLevelDirectoryRecord = ParseDirectoryRecordNotExact(record.LowerLevelDirectoryRecord, lowerLevelOffset);
            }

            return record;
        }


        #region IDicomReaderObserver Implementation


        public void OnElement(IByteSource source, long position, DicomTag tag, DicomVR vr, IByteBuffer data)
        {
            // do nothing here
        }

        public void OnBeginSequence(IByteSource source, long position, DicomTag tag, uint length)
        {
            _currentSequenceTag.Push(tag);
            if (tag == DicomTag.DirectoryRecordSequence)
            {
                _directoryRecordSequence = _dataset.GetDicomItem<DicomSequence>(tag);
            }
        }

        public void OnBeginSequenceItem(IByteSource source, long position, uint length)
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

        public void OnBeginFragmentSequence(IByteSource source, long position, DicomTag tag, DicomVR vr)
        {
            // do nothing here
        }

        public void OnFragmentSequenceItem(IByteSource source, long position, IByteBuffer data)
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
