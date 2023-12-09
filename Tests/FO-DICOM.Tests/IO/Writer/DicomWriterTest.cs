// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Writer;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit;

namespace FellowOakDicom.Tests.IO.Writer
{

    [Collection(TestCollections.General)]
    public class DicomWriterTest
    {
        #region Unit tests

        [Fact]
        public void OnElement_LargeObject_ReturnValueTrue()
        {
            const string expected = "ELECTRON_SQUARE";

            var e = new ManualResetEventSlim(false);
            var element = new DicomCodeString(DicomTag.ApplicatorType, expected);

            using var stream = new MemoryStream();
            var target = new StreamByteTarget(stream);
            var writer = new DicomWriter(
                DicomTransferSyntax.ExplicitVRLittleEndian,
                new DicomWriteOptions { LargeObjectSize = 14 },
                target);
            writer.OnBeginWalk();
            Assert.True(writer.OnElement(element));

            e.Wait(100);

            stream.Seek(8, SeekOrigin.Begin);
            using var reader = new StreamReader(stream);
            var actual = reader.ReadToEnd().Trim();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OnElement_SmallObject_ReturnValueTrue()
        {
            const string expected = "STEREOTACTIC";

            var e = new ManualResetEventSlim(false);
            var element = new DicomCodeString(DicomTag.ApplicatorType, expected);

            using var stream = new MemoryStream();
            var target = new StreamByteTarget(stream);
            var writer = new DicomWriter(
                DicomTransferSyntax.ExplicitVRLittleEndian,
                new DicomWriteOptions { LargeObjectSize = 14 },
                target);
            writer.OnBeginWalk();
            Assert.True(writer.OnElement(element));

            e.Wait(100);

            stream.Seek(8, SeekOrigin.Begin);
            using var reader = new StreamReader(stream);
            var actual = reader.ReadToEnd().Trim();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OnFragmentItem_LargeBuffer_ReturnValueTrue()
        {
            var expected = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();

            var e = new ManualResetEventSlim(false);

            using var stream = new MemoryStream();
            var target = new StreamByteTarget(stream);
            var writer = new DicomWriter(
                DicomTransferSyntax.ExplicitVRLittleEndian,
                new DicomWriteOptions { LargeObjectSize = 200 },
                target);
            writer.OnBeginWalk();
            Assert.True(writer.OnFragmentItem(new MemoryByteBuffer(expected)));

            e.Wait(100);

            var actual = new byte[expected.Length];
            stream.Seek(8, SeekOrigin.Begin);
            stream.Read(actual, 0, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OnFragmentItem_SmallBuffer_ReturnValueTrue()
        {
            var expected = Enumerable.Range(0, 198).Select(i => (byte)i).ToArray();
            var e = new ManualResetEventSlim(false);

            using var stream = new MemoryStream();
            var target = new StreamByteTarget(stream);
            var writer = new DicomWriter(
                DicomTransferSyntax.ExplicitVRLittleEndian,
                new DicomWriteOptions { LargeObjectSize = 200 },
                target);
            writer.OnBeginWalk();
            Assert.True(writer.OnFragmentItem(new MemoryByteBuffer(expected)));

            e.Wait(100);

            var actual = new byte[expected.Length];
            stream.Seek(8, SeekOrigin.Begin);
            stream.Read(actual, 0, actual.Length);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
