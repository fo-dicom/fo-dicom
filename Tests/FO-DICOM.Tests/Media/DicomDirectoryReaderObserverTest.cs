// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Media
{

    [Collection(TestCollections.General)]
    public class DicomDirectoryReaderObserverTest
    {

        [Fact]
        public void Off_By_2_Offsets_Should_All_Match()
        {
            var offsets = new List<uint>(GenerateUniqueRandonmOffsets(155, 100000).Select(o => o * 5));

            var root = new Container() { Sequence = new DicomDataset() };
            root.CreateChildren(offsets, 5, 3, 3, 2);

            var allChildren = root.AllContainer.Skip(1).ToArray();
            var sequence = new DicomSequence(DicomTag.DirectoryRecordSequence);
            root.Sequence.AddOrUpdate(sequence);

            var byteSource = new TestByteSource();
            var observer = new DicomDirectoryReaderObserver(root.Sequence);
            observer.OnBeginSequence(byteSource, DicomTag.DirectoryRecordSequence, 0);
            foreach (var container in allChildren)
            {
                sequence.Items.Add(container.Sequence);
                byteSource.Position = container.Offset + 10;
                observer.OnBeginSequenceItem(byteSource, 0);
                observer.OnEndSequenceItem();
            }
            observer.OnEndSequence();

            var rootRecord = new DicomDirectoryRecord
            {
                LowerLevelDirectoryRecord = observer.BuildDirectoryRecords(),
            };

            root.AssertRecord(rootRecord);
        }

        [Fact]
        public void Duplicate_Offsets_Should_Not_Throw()
        {
            var offsets = new List<uint>(GenerateUniqueRandonmOffsets(155, 100000).Select(o => o * 5));
            for (int i = 0; i < offsets.Count - 2; i += 2)
            {
                offsets[i + 1] = offsets[i];
            }

            var root = new Container() { Sequence = new DicomDataset() };
            root.CreateChildren(offsets, 5, 3, 3, 2);

            var allChildren = root.AllContainer.Skip(1).ToArray();
            var sequence = new DicomSequence(DicomTag.DirectoryRecordSequence);
            root.Sequence.AddOrUpdate(sequence);

            var byteSource = new TestByteSource();
            var observer = new DicomDirectoryReaderObserver(root.Sequence);
            observer.OnBeginSequence(byteSource, DicomTag.DirectoryRecordSequence, 0);
            uint lastPosition = 0;
            foreach (var container in allChildren)
            {
                sequence.Items.Add(container.Sequence);
                var position = container.Offset + 8;
                if (position == lastPosition)
                {
                    position += 1;
                }

                lastPosition = position;

                byteSource.Position = position;
                observer.OnBeginSequenceItem(byteSource, 0);
                observer.OnEndSequenceItem();
            }
            observer.OnEndSequence();

            var rootRecord = new DicomDirectoryRecord
            {
                LowerLevelDirectoryRecord = observer.BuildDirectoryRecords(),
            };

            // Cannot assert all records when duplicate offsets exist.
            // root.AssertRecord(rootRecord);
        }


        [Fact]
        public void Exact_Offsets_Should_All_Match()
        {
            var offsets = GenerateUniqueRandonmOffsets(155, 500000);
            var root = new Container() { Sequence = new DicomDataset() };
            root.CreateChildren(offsets, 5, 3, 3, 2);

            var allChildren = root.AllContainer.Skip(1).ToArray();
            var sequence = new DicomSequence(DicomTag.DirectoryRecordSequence);
            root.Sequence.AddOrUpdate(sequence);

            var byteSource = new TestByteSource();
            var observer = new DicomDirectoryReaderObserver(root.Sequence);
            observer.OnBeginSequence(byteSource, DicomTag.DirectoryRecordSequence, 0);
            foreach (var container in allChildren)
            {
                sequence.Items.Add(container.Sequence);
                byteSource.Position = container.Offset + 8;
                observer.OnBeginSequenceItem(byteSource, 0);
                observer.OnEndSequenceItem();
            }
            observer.OnEndSequence();

            var rootRecord = new DicomDirectoryRecord
            {
                LowerLevelDirectoryRecord = observer.BuildDirectoryRecords(),
            };

            root.AssertRecord(rootRecord);
        }


        private static HashSet<uint> GenerateUniqueRandonmOffsets(int count, int max)
        {
            var offsets = new HashSet<uint>();
            var rnd = new Random();
            while (offsets.Count < count)
            {
                offsets.Add((uint)rnd.Next(1, max));
            }

            return offsets;
        }

    }


    #region Helper Classes

    internal class TestByteSource : IByteSource
    {
        public Endian Endian { get; set; }
        public long Position { get; set; }
        public long Marker { get; set; }
        public bool IsEOF { get; set; }
        public bool CanRewind { get; set; }
        public int MilestonesCount { get; set; }

        public IByteBuffer GetBuffer(uint count) => throw new NotImplementedException();
        public Task<IByteBuffer> GetBufferAsync(uint count) => throw new NotImplementedException();
        public void Skip(uint count) => throw new NotImplementedException();
        public void Skip(int count) => throw new NotImplementedException();
        public byte[] GetBytes(int count) => throw new NotImplementedException();
        public int GetBytes(byte[] buffer, int index, int count) => throw new NotImplementedException();
        public double GetDouble() => throw new NotImplementedException();
        public short GetInt16() => throw new NotImplementedException();
        public int GetInt32() => throw new NotImplementedException();
        public long GetInt64() => throw new NotImplementedException();
        public float GetSingle() => throw new NotImplementedException();
        public ushort GetUInt16() => throw new NotImplementedException();
        public uint GetUInt32() => throw new NotImplementedException();
        public ulong GetUInt64() => throw new NotImplementedException();
        public byte GetUInt8() => throw new NotImplementedException();
        public bool HasReachedMilestone() => throw new NotImplementedException();
        public void Mark() => throw new NotImplementedException();
        public void PopMilestone() => throw new NotImplementedException();
        public void PushMilestone(uint count) => throw new NotImplementedException();
        public bool Require(uint count) => throw new NotImplementedException();
        public bool Require(uint count, ByteSourceCallback callback, object state) => throw new NotImplementedException();
        public void Rewind() => throw new NotImplementedException();
        public Stream GetStream() => throw new NotImplementedException();
    }

    internal class Container
    {
        public int Id;
        public uint Offset;
        public DicomDataset Sequence;
        public Container Parent;
        public List<Container> Children = new List<Container>();
        public IEnumerable<Container> AllContainer
        {
            get
            {
                yield return this;
                foreach (var child in Children.SelectMany(child => child.AllContainer))
                {
                    yield return child;
                }
            }
        }

        public void CreateChildren(ICollection<uint> offsets, params int[] childrenPerLevel)
        {
            if (childrenPerLevel.Length > 0)
            {
                for (int i = 1; i <= childrenPerLevel[0]; i++)
                {
                    var id = Id * 10 + i;
                    var offset = offsets.First();
                    offsets.Remove(offset);
                    var child = new Container
                    {
                        Id = id,
                        Offset = offset,
                        Sequence = new DicomDataset(),
                        Parent = this,
                    };
                    child.Sequence.AddOrUpdate(DicomTag.DirectoryRecordType, string.Empty);
                    child.Sequence.AddOrUpdate(DicomTag.SOPInstanceUID, id.ToString());
                    child.Sequence.AddOrUpdate(DicomTag.OffsetOfTheNextDirectoryRecord, 0u);
                    child.Sequence.AddOrUpdate(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, 0u);
                    if (Children.Any())
                    {
                        Children.Last().Sequence.AddOrUpdate(DicomTag.OffsetOfTheNextDirectoryRecord, offset);
                    }
                    else
                    {
                        Sequence.AddOrUpdate(Parent == null ? DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity : DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, offset);
                    }

                    Children.Add(child);

                    child.CreateChildren(offsets, childrenPerLevel.Skip(1).ToArray());
                }
            }
        }

        public void AssertRecord(DicomDirectoryRecord record)
        {
            if (Id != 0)
            {
                Assert.Equal(Id.ToString(), record.GetSingleValue<string>(DicomTag.SOPInstanceUID));
            }

            DicomDirectoryRecord current = record.LowerLevelDirectoryRecord;
            foreach (var child in Children)
            {
                Assert.NotNull(current);
                child.AssertRecord(current);
                current = current.NextDirectoryRecord;
            }
            Assert.Null(current);
        }
    }

    #endregion

}
