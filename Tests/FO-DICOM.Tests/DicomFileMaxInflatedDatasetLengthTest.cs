// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Reader;
using System;
using System.IO;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.GlobalSettings)]
    public class DicomFileMaxInflatedDatasetLengthTest
    {

        [Fact]
        public void MaxInflatedDatasetLength_DefaultsToHalfAGibibyte()
        {
            // The reader tests inject a small limit, so without this nothing pins the value
            // that actually ships, and raising it would silently weaken the guard.
            Assert.Equal(512L * 1024 * 1024, DicomFile.MaxInflatedDatasetLength);
        }

        [Theory]
        [InlineData(0L)]
        [InlineData(-1L)]
        public void MaxInflatedDatasetLength_NonPositiveValue_IsRejected(long value)
        {
            var before = DicomFile.MaxInflatedDatasetLength;

            Assert.Throws<ArgumentOutOfRangeException>(() => DicomFile.MaxInflatedDatasetLength = value);
            Assert.Equal(before, DicomFile.MaxInflatedDatasetLength);
        }

        [Fact]
        public void Open_DeflatedFile_RespectsTheConfiguredLimit()
        {
            // 256 KiB of zeroes deflates to a few hundred bytes, so whether the file opens
            // depends only on the configured limit.
            const int payloadLength = 256 * 1024;
            var file = SaveDeflated(new byte[payloadLength]);
            var original = DicomFile.MaxInflatedDatasetLength;
            try
            {
                DicomFile.MaxInflatedDatasetLength = 64 * 1024;
                var exception = Assert.Throws<DicomFileException>(() => DicomFile.Open(new MemoryStream(file)));
                Assert.IsType<DicomReaderException>(exception.InnerException);

                DicomFile.MaxInflatedDatasetLength = 1024 * 1024;
                var reopened = DicomFile.Open(new MemoryStream(file));
                Assert.Equal(payloadLength, reopened.Dataset.GetDicomItem<DicomElement>(DicomTag.ICCProfile).Buffer.Data.Length);
            }
            finally
            {
                DicomFile.MaxInflatedDatasetLength = original;
            }
        }

        private static byte[] SaveDeflated(byte[] payload)
        {
            var dataset = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUID.Generate() },
                new DicomOtherByte(DicomTag.ICCProfile, payload)
            };
            dataset.InternalTransferSyntax = DicomTransferSyntax.DeflatedExplicitVRLittleEndian;

            using var stream = new MemoryStream();
            new DicomFile(dataset).Save(stream);
            return stream.ToArray();
        }

    }
}
