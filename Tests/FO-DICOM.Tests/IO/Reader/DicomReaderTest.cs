// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Reader;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Memory;
using Xunit;

namespace FellowOakDicom.Tests.IO.Reader
{

    [Collection(TestCollections.General)]
    public class DicomReaderTest
    {
        #region Unit tests

        [Theory]
        [MemberData(nameof(ValidExplicitVRData))]
        public void Read_ValidExplicitVRData_YieldsSuccess(DicomTag tag, DicomVR vr, string data, byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            var source = new StreamByteSource(stream);
            var reader = new DicomReader(new ArrayPoolMemoryProvider()) { IsExplicitVR = true };

            var observer = new LastElementObserver();
            var result = reader.Read(source, observer);

            Assert.Equal(DicomReaderResult.Success, result);
            Assert.Equal(tag, observer.Tag);
            Assert.Equal(vr, observer.VR);
            Assert.Equal(data, observer.Data);
        }

        [Theory]
        [MemberData(nameof(ValidExplicitVRSequences))]
        public void Read_ValidExplicitVRSequence_YieldsSuccess(byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            var source = new StreamByteSource(stream);
            var reader = new DicomReader(new ArrayPoolMemoryProvider()) { IsExplicitVR = true };

            var observer = new MockObserver();
            var result = reader.Read(source, observer);

            Assert.Equal(DicomReaderResult.Success, result);
        }

        [Fact]
        public void FreezeReadingZeroLengthSequence()
        {
            string filename = TestData.Resolve("FreezePattern.dcm");
            var file = DicomFile.Open(filename);

            Assert.NotNull(file);
        }

        [Fact]
        public void GivenDeflatedDicomFileWithSequence_WhenOpenFile_ThenShouldSucceed()
        {
            // This is for regression bug https://github.com/fo-dicom/fo-dicom/issues/1097
            string filename = TestData.Resolve("Issue1097_FailToOpenDeflatedFileWithSQ.dcm");

            // Verify should be able to open file without exception
            DicomFile.Open(filename);
        }

        [Fact]
        public async Task GivenDeflatedDicomFileWithSequence_WhenOpenFileAsync_ThenShouldSucceed()
        {
            // This is for regression bug https://github.com/fo-dicom/fo-dicom/issues/1097
            string filename = TestData.Resolve("Issue1097_FailToOpenDeflatedFileWithSQ.dcm");

            // Verify should be able to open file without exception
            await DicomFile.OpenAsync(filename);
        }

        [Fact]
        public void ReadWithCustomLargeObjectSize()
        {
            var ds = new DicomDataset
            {
                { DicomTag.SOPInstanceUID, DicomUID.Generate() },
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.ImageComments, " ".PadLeft(8000) }
            };
            var df = new DicomFile(ds);
            var memoryStream = new MemoryStream();
            df.Save(memoryStream);

            // read with default option
            memoryStream.Position = 0;
            var defaultFile = DicomFile.Open(memoryStream);
            var imageComment = defaultFile.Dataset.GetDicomItem<DicomLongText>(DicomTag.ImageComments);

            Assert.IsType<MemoryByteBuffer>(imageComment.Buffer);

            // read with custom option
            memoryStream.Position = 0;
            var customFile = DicomFile.Open(memoryStream, largeObjectSize: 4000);
            imageComment = customFile.Dataset.GetDicomItem<DicomLongText>(DicomTag.ImageComments);

            Assert.IsType<StreamByteBuffer>(imageComment.Buffer);
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> ValidExplicitVRData
        {
            get
            {
                yield return
                    new object[]
                        {
                            DicomTag.StudyDate, DicomVR.DA, "20150721",
                            new byte[]
                                {
                                    0x08, 0x00, 0x20, 0x00, 0x44, 0x41, 0x08, 0x00, 0x32, 0x30, 0x31, 0x35, 0x30, 0x37, 0x32,
                                    0x31
                                }
                        };
                yield return
                    new object[]
                        {
                            DicomTag.BeamDose, DicomVR.DS, "2.015 ",
                            new byte[] { 0x0a, 0x30, 0x84, 0x00, 0x44, 0x53, 0x06, 0x00, 0x32, 0x2e, 0x30, 0x31, 0x35, 0x20 }
                        };
                yield return    // Same as previous, but VR data omitted and length spans 4 bytes
                    new object[]
                        {
                            DicomTag.BeamDose, DicomVR.DS, "2.015 ",
                            new byte[] { 0x0a, 0x30, 0x84, 0x00, 0x06, 0x00, 0x00, 0x00, 0x32, 0x2e, 0x30, 0x31, 0x35, 0x20 }
                        };
            }
        }

        public static IEnumerable<object[]> ValidExplicitVRSequences
        {
            get
            {
                yield return
                    new object[]
                        {
                            new byte[]
                                {
                                    0x54, 0x00, 0x63, 0x00, 0x53, 0x51, 0x00, 0x00, 0x24, 0x00, 0x00, 0x00, 0xfe, 0xff,
                                    0x00, 0xe0, 0x1c, 0x00, 0x00, 0x00, 0x08, 0x00, 0x16, 0x00, 0x55, 0x49, 0x06, 0x00,
                                    0x31, 0x2e, 0x32, 0x2e, 0x38, 0x34, 0x08, 0x00, 0x18, 0x00, 0x55, 0x49, 0x06, 0x00,
                                    0x31, 0x2e, 0x33, 0x2e, 0x34, 0x00
                                }
                        };
                yield return
                    new object[]
                        {
                            new byte[]
                                {
                                    0x0b, 0x20, 0x9f, 0x70, 0xff, 0xff, 0xff, 0xff, 0xfe, 0xff, 0x00, 0xe0, 0xff, 0xff,
                                    0xff, 0xff, 0x08, 0x00, 0x16, 0x00, 0x06, 0x00, 0x00, 0x00, 0x31, 0x2e, 0x32, 0x2e,
                                    0x38, 0x34, 0x08, 0x00, 0x18, 0x00, 0x06, 0x00, 0x00, 0x00, 0x31, 0x2e, 0x33, 0x2e,
                                    0x34, 0x00, 0xfe, 0xff, 0x0d, 0xe0, 0x00, 0x00, 0x00, 0x00, 0xfe, 0xff, 0xdd, 0xe0,
                                    0x00, 0x00, 0x00, 0x00
                                }
                        };
            }
        }

        #endregion

        #region Support types

        private class LastElementObserver : IDicomReaderObserver
        {
            #region Properties

            public DicomTag Tag { get; private set; }

            public DicomVR VR { get; private set; }

            public string Data { get; private set; }

            #endregion

            #region Interface implementation

            public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data)
            {
                Tag = tag;
                VR = vr;
                Data = Encoding.UTF8.GetString(data.Data);
            }

            public void OnBeginSequence(IByteSource source, DicomTag tag, uint length)
            {
            }

            public void OnBeginSequenceItem(IByteSource source, uint length)
            {
            }

            public void OnEndSequenceItem()
            {
            }

            public void OnEndSequence()
            {
            }

            public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr)
            {
            }

            public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data)
            {
            }

            public void OnEndFragmentSequence()
            {
            }

            #endregion
        }

        #endregion
    }
}
