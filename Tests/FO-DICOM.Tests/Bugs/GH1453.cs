// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.IO.Buffer;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH1453
    {
        private readonly ITestOutputHelper _output;

        // 4_294_967_298 = 4 GB + 2 bytes
        // This will ensure the file will be larger than uint.MaxValue
        private const long PixelDataLength = 4_294_967_298;

        // 4_294_967_298 / 2 = 2 GB + 1 byte, so each frame will be larger than int.MaxValue but smaller than uint.MaxValue
        private const int NumberOfFrames = 2;

        public GH1453(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        private DicomFile PrepareLargeDicomFile(List<IDisposable> disposables)
        {
            var frameLength = PixelDataLength / NumberOfFrames;
            var pixelData = new DicomOtherByteFragment(DicomTag.PixelData);
            for (int i = 0; i < NumberOfFrames; i++)
            {
                var frameDataStream = new ConstantValueStream(frameLength, 255);
                pixelData.Fragments.Add(new StreamByteBuffer(frameDataStream, 0, frameLength));
                disposables.Add(frameDataStream);
            }
            var largeDicomFile = new DicomFile();
            largeDicomFile.FileMetaInfo.TransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            var sopInstanceUid = DicomUIDGenerator.GenerateDerivedFromUUID();
            largeDicomFile.Dataset.AddOrUpdate(DicomTag.SOPInstanceUID, sopInstanceUid);
            largeDicomFile.Dataset.AddOrUpdate(DicomTag.BitsAllocated, (ushort)8);
            largeDicomFile.Dataset.AddOrUpdate(DicomTag.BitsStored, (ushort)8);
            largeDicomFile.Dataset.AddOrUpdate(DicomTag.NumberOfFrames, NumberOfFrames);
            largeDicomFile.Dataset.AddOrUpdate(pixelData);

            return largeDicomFile;
        }

        [Theory]
        [InlineData(FileReadOption.ReadLargeOnDemand)]
        [InlineData(FileReadOption.ReadAll)]
        [InlineData(FileReadOption.SkipLargeTags)]
        public async Task LargeDicomFile_SavingAndOpeningFromFile_ShouldWork(FileReadOption readOption)
        {
            var tempFileName = Path.GetTempFileName();
            var disposables = new List<IDisposable>();
            try
            {
                // Arrange
                var largeDicomFile = PrepareLargeDicomFile(disposables);
                var sopInstanceUid = largeDicomFile.Dataset.GetSingleValue<string>(DicomTag.SOPInstanceUID);
                var fragments = largeDicomFile.Dataset.GetDicomItem<DicomOtherByteFragment>(DicomTag.PixelData).Fragments;
                var firstFragmentLength = fragments[0].Size;
                var lastFragmentLength = fragments[fragments.Count - 1].Size;

                // Act
                await largeDicomFile.SaveAsync(tempFileName);
                var largeDicomFileSize = new FileInfo(tempFileName).Length;
                var largeDicomFileSizeInMegaBytes = (int)Math.Ceiling((double)largeDicomFileSize / 1024 / 1024);

                // Guard against insufficient memory, don't fail the test if the system cannot load the file due to not enough memory
                using var _ = new MemoryFailPoint(largeDicomFileSizeInMegaBytes);
                var openedDicomFile = await DicomFile.OpenAsync(tempFileName, readOption);

                // Assert
                Assert.Equal(sopInstanceUid, openedDicomFile.Dataset.GetSingleValue<string>(DicomTag.SOPInstanceUID));
                if (readOption != FileReadOption.SkipLargeTags)
                {
                    var openedPixelData =
                        openedDicomFile.Dataset.GetDicomItem<DicomOtherByteFragment>(DicomTag.PixelData);
                    Assert.Equal(NumberOfFrames, openedPixelData.Fragments.Count);
                    Assert.Equal(firstFragmentLength, openedPixelData.Fragments[0].Size);
                    Assert.Equal(lastFragmentLength, openedPixelData.Fragments[NumberOfFrames - 1].Size);
                }
            }
            catch (InsufficientMemoryException)
            {
                _output.WriteLine("Test skipped because system has insufficient memory to load large DICOM file in memory");
            }
            finally
            {
                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }

                File.Delete(tempFileName);
            }
        }

        [Theory]
        [InlineData(FileReadOption.ReadLargeOnDemand)]
        [InlineData(FileReadOption.ReadAll)]
        [InlineData(FileReadOption.SkipLargeTags)]
        public async Task LargeDicomFile_SavingAndOpeningFromStream_ShouldWork(FileReadOption readOption)
        {
            var tempFileName = Path.GetTempFileName();
            var disposables = new List<IDisposable>();
            try
            {
                // Arrange
                var largeDicomFile = PrepareLargeDicomFile(disposables);
                var sopInstanceUid = largeDicomFile.Dataset.GetSingleValue<string>(DicomTag.SOPInstanceUID);
                var fragments = largeDicomFile.Dataset.GetDicomItem<DicomOtherByteFragment>(DicomTag.PixelData).Fragments;
                var firstFragmentLength = fragments[0].Size;
                var lastFragmentLength = fragments[fragments.Count - 1].Size;

                // Act
                await largeDicomFile.SaveAsync(tempFileName);
                var largeDicomFileSize = new FileInfo(tempFileName).Length;
                var largeDicomFileSizeInMegaBytes = (int)Math.Ceiling((double)largeDicomFileSize / 1024 / 1024);

                // Guard against insufficient memory, don't fail the test if the system cannot load the file due to not enough memory
                using var _ = new MemoryFailPoint(largeDicomFileSizeInMegaBytes);
                using var fileStream = File.OpenRead(tempFileName);
                var openedDicomFile = await DicomFile.OpenAsync(fileStream, readOption);

                // Assert
                Assert.Equal(sopInstanceUid, openedDicomFile.Dataset.GetSingleValue<string>(DicomTag.SOPInstanceUID));
                if (readOption != FileReadOption.SkipLargeTags)
                {
                    var openedPixelData = openedDicomFile.Dataset.GetDicomItem<DicomOtherByteFragment>(DicomTag.PixelData);
                    Assert.Equal(NumberOfFrames, openedPixelData.Fragments.Count);
                    Assert.Equal(firstFragmentLength, openedPixelData.Fragments[0].Size);
                    Assert.Equal(lastFragmentLength, openedPixelData.Fragments[NumberOfFrames - 1].Size);
                }
            }
            catch (InsufficientMemoryException)
            {
                _output.WriteLine("Test skipped because system has insufficient memory to load large DICOM file in memory");
            }
            finally
            {
                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }

                File.Delete(tempFileName);
            }
        }

        /// <summary>
        /// Helper class to produce a huge amount of data.
        /// This stream repeatedly produces the same constant value, with a configurable length
        /// </summary>
        private class ConstantValueStream : Stream, IAsyncDisposable
        {
            public ConstantValueStream(long length, byte value)
            {
                Length = length;
                Value = value;
            }

            public override bool CanRead => true;
            public override bool CanSeek => true;
            public override bool CanWrite => false;
            public override long Length { get; }
            public byte Value { get; }
            public override long Position { get; set; }

            public override void Flush() { }

            public override int Read(byte[] buffer, int offset, int count)
            {
                var numberOfBytesToRead = (int)Math.Min(count, Length - Position);
                if (numberOfBytesToRead == 0)
                {
                    return 0;
                }

                var max = Math.Min(offset + count, buffer.Length);
                for (var i = offset; i < max; i++)
                {
                    buffer[i] = Value;
                }

                Position += numberOfBytesToRead;
                return count;
            }

            public override Task<int> ReadAsync(byte[] buffer, int offset, int count,
                CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var numberOfBytesToRead = (int)Math.Min(count, Length - Position);
                if (numberOfBytesToRead == 0)
                {
                    return Task.FromResult(0);
                }

                var max = Math.Min(offset + count, buffer.Length);
                for (var i = offset; i < max; i++)
                {
                    buffer[i] = Value;
                }

                Position += numberOfBytesToRead;
                return Task.FromResult(count);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        Position = offset;
                        break;
                    case SeekOrigin.Current:
                        Position += offset;
                        break;
                    case SeekOrigin.End:
                        Position = Length - offset;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(origin), origin, null);
                }

                return Position;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

#if NET462
            public ValueTask DisposeAsync()
            {
                return default;
            }
#else
            public override ValueTask DisposeAsync()
            {
                return default;
            }
#endif
        }
    }
}
