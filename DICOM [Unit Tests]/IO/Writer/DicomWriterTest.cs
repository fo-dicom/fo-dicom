// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Writer
{
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Dicom.IO.Buffer;

    using Xunit;

    public class DicomWriterTest
    {
        #region Unit tests

        [Fact]
        public void OnElement_LargeObject_CallbackCalled()
        {
            const string expected = "ELECTRON_SQUARE";

            var callbacks = 0;
            var e = new ManualResetEventSlim(false);
            var element = new DicomCodeString(DicomTag.ApplicatorType, expected);

            using (var stream = new MemoryStream())
            {
                var target = new StreamByteTarget(stream);
                var writer = new DicomWriter(
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    new DicomWriteOptions { LargeObjectSize = 14 },
                    target);
                writer.OnBeginWalk(
                    new DicomDatasetWalker(new DicomItem[] { element }),
                    () =>
                        {
                            callbacks += 1;
                            e.Set();
                        });
                writer.OnElement(element);

                e.Wait(100);
                Assert.True(callbacks == 1);

                stream.Seek(8, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd().Trim();
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void OnElement_SmallObject_CallbackNotCalled()
        {
            const string expected = "STEREOTACTIC";

            var callbacks = 0;
            var e = new ManualResetEventSlim(false);
            var element = new DicomCodeString(DicomTag.ApplicatorType, expected);

            using (var stream = new MemoryStream())
            {
                var target = new StreamByteTarget(stream);
                var writer = new DicomWriter(
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    new DicomWriteOptions { LargeObjectSize = 14 },
                    target);
                writer.OnBeginWalk(
                    new DicomDatasetWalker(new DicomItem[] { element }),
                    () =>
                    {
                        callbacks += 1;
                        e.Set();
                    });
                writer.OnElement(element);

                e.Wait(100);
                Assert.True(callbacks == 0);

                stream.Seek(8, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd().Trim();
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void OnFragmentItem_LargeBuffer_CallbackCalled()
        {
            var expected = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();

            var callbacks = 0;
            var e = new ManualResetEventSlim(false);

            using (var stream = new MemoryStream())
            {
                var target = new StreamByteTarget(stream);
                var writer = new DicomWriter(
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    new DicomWriteOptions { LargeObjectSize = 200 },
                    target);
                writer.OnBeginWalk(
                    new DicomDatasetWalker(new DicomItem[0]),
                    () =>
                    {
                        callbacks += 1;
                        e.Set();
                    });
                writer.OnFragmentItem(new MemoryByteBuffer(expected));

                e.Wait(100);
                Assert.True(callbacks == 1);

                var actual = new byte[expected.Length];
                stream.Seek(8, SeekOrigin.Begin);
                stream.Read(actual, 0, actual.Length);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void OnFragmentItem_SmallBuffer_CallbackNotCalled()
        {
            var expected = Enumerable.Range(0, 198).Select(i => (byte)i).ToArray();
            var callbacks = 0;
            var e = new ManualResetEventSlim(false);

            using (var stream = new MemoryStream())
            {
                var target = new StreamByteTarget(stream);
                var writer = new DicomWriter(
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    new DicomWriteOptions { LargeObjectSize = 200 },
                    target);
                writer.OnBeginWalk(
                    new DicomDatasetWalker(new DicomItem[0]),
                    () =>
                    {
                        callbacks += 1;
                        e.Set();
                    });
                writer.OnFragmentItem(new MemoryByteBuffer(expected));

                e.Wait(100);
                Assert.True(callbacks == 0);

                var actual = new byte[expected.Length];
                stream.Seek(8, SeekOrigin.Begin);
                stream.Read(actual, 0, actual.Length);
                Assert.Equal(expected, actual);
            }
        }

        #endregion
    }
}