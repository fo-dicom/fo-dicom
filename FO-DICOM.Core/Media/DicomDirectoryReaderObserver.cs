// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;
using FellowOakDicom.IO;
using FellowOakDicom.IO.Reader;
using FellowOakDicom.IO.Buffer;
using System;

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

            if (_lookup.Count > 0)
            {
                // There are unresolved sequences. Try to resolve them with non exact offset match
                root = ParseDirectoryRecordNotExact(root, offset, notFoundOffsets);
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


        private DicomDirectoryRecord ParseDirectoryRecordNotExact(DicomDirectoryRecord root, uint rootOffset, List<uint> offsets)
        {
            // Collect all used offsets, and sort them (they can contain duplicates)
            foreach (var dataset in _lookup.Values)
            {
                var offset = dataset.GetSingleValue<uint>(DicomTag.OffsetOfTheNextDirectoryRecord);
                if (offset > 0)
                {
                    offsets.Add(offset);
                }

                offset = dataset.GetSingleValue<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity);
                if (offset > 0)
                {
                    offsets.Add(offset);
                }
            }
            var offsetArray = offsets.ToArray();
            Array.Sort(offsetArray);

            return ParseDirectoryRecord(root, rootOffset, offsetArray);
        }

        private DicomDirectoryRecord ParseDirectoryRecord(DicomDirectoryRecord record, uint offset, uint[] offsets)
        {
            if (record == null)
            {
                // Find the best matching existing offset for the given offset
                // The given offset should be in offsets and the following entry in that sorted array gives
                // us the next used offset. That's the upper bound for the search

                var index = Array.BinarySearch(offsets, offset);
                if (index < 0)
                {
                    return null;
                }
                uint maxOffset;
                int maxIndex = offsets.Length - 1;
                // Deal with the rare possibility that offsets can contain duplicates
                while (true)
                {
                    if (index >= maxIndex)
                    {
                        maxOffset = uint.MaxValue;
                        break;
                    }
                    maxOffset = offsets[++index];
                    if (maxOffset != offset)
                    {
                        break;
                    }
                }
                offset = _lookup.Keys.FirstOrDefault(key => key >= offset && key < maxOffset);
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
                record.NextDirectoryRecord = ParseDirectoryRecord(record.NextDirectoryRecord, nextOffset, offsets);
            }

            var lowerLevelOffset = record.GetSingleValue<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity);
            if (lowerLevelOffset > 0)
            {
                record.LowerLevelDirectoryRecord = ParseDirectoryRecord(record.LowerLevelDirectoryRecord, lowerLevelOffset, offsets);
            }

            return record;
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
