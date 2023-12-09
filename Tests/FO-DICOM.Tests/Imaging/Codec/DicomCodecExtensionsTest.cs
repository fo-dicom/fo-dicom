// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO.Buffer;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Codec
{

    [Collection(TestCollections.WithTranscoder)]
    public class DicomCodecExtensionsTest
    {
        #region Unit tests

        [FactForNetCore]
        public void CheckLossyCompressionRatio_HasAddedMultiValueAfterCompression()
        {
            var file = DicomFile.Open(TestData.Resolve("GH538-JPEG1.dcm"));
            var oldRatios = file.Dataset.GetValues<string>(DicomTag.LossyImageCompressionRatio);
            var ds = file.Clone(DicomTransferSyntax.JPEGProcess1).Dataset;
            var newRatios = ds.GetValues<string>(DicomTag.LossyImageCompressionRatio);
            Assert.Equal(oldRatios.Length+1, newRatios.Length);
        }

        [FactForNetCore]
        public void CheckLossyCompressionRatio_HasSingleValueAfterCompression()
        {
            var file = DicomFile.Open(TestData.Resolve("GH538-JPEG14SV1.dcm"));
            var ds = file.Clone(DicomTransferSyntax.JPEGProcess1).Dataset;
            var ratios = ds.GetValues<string>(DicomTag.LossyImageCompressionRatio);
            Assert.Single(ratios);
        }

        [FactForNetCore]
        public void ChangeTransferSyntax_FileFromRLELosslessToJPEGProcess2_4()
        {
            var file = DicomFile.Open(TestData.Resolve("10200904.dcm"));
            var exception =
                Record.Exception(
                    () =>
                    file.Clone(DicomTransferSyntax.JPEGProcess2_4));
            Assert.Null(exception);
        }

        [FactForNetCore]
        public void ChangeTransferSyntax_FileFromRLELosslessToJPEGProcess2_4_WithParameters()
        {
            var file = DicomFile.Open(TestData.Resolve("10200904.dcm"));
            var exception =
                Record.Exception(
                    () =>
                    file.Clone(DicomTransferSyntax.JPEGProcess2_4, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }

        [FactForNetCore]
        public void ChangeTransferSyntax_FileFromJ2KToJPEGWithParameters_DoesNotThrow()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var exception =
                Record.Exception(
                    () =>
                    file.Clone(DicomTransferSyntax.JPEGProcess14, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }


        [FactForNetCore]
        public void ChangeTransferSyntax_DatasetFromJ2KToJPEGWithParameters_DoesNotThrow()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var exception =
                Record.Exception(
                    () =>
                    file.Dataset.Clone(DicomTransferSyntax.JPEGProcess14, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }


        [TheoryForNetCore(Skip = "Bug in Photometricinterpretation, to be fixed...")] // TODO: fix the bug
        [InlineData("TestPattern_RGB.dcm")]
        [InlineData("CR-MONO1-10-chest")]
        [InlineData("GH064.dcm")]
        public void ChangeTransferSyntax_UpdatePhotometricInterpretationOnJPEGBaseline(string filename)
        {
            var file = DicomFile.Open(TestData.Resolve("" + filename));

            // when converting to JpegBaseline the Photometric interpretation has to be updated
            var newDataset = file.Dataset.Clone(DicomTransferSyntax.JPEGProcess2_4, new DicomJpegParams { ConvertColorspaceToRGB=false, Quality = 90,  SampleFactor = DicomJpegSampleFactor.SF422 });
            Assert.Equal("YBR_FULL_422", newDataset.GetString(DicomTag.PhotometricInterpretation));

            // when converting to other jpeg coded that does not automatically convert to 8bitcolor the Photometric Interpretation has to stay the same.
            var newProcess14 = file.Dataset.Clone(DicomTransferSyntax.JPEGProcess14SV1);
            Assert.Equal(file.Dataset.GetString(DicomTag.PhotometricInterpretation), newProcess14.GetString(DicomTag.PhotometricInterpretation));
        }


        [Fact(Skip = "EncodeDecodeRLE16 causes test host process crashes. See Github issue #1072 at https://github.com/fo-dicom/fo-dicom/issues/1072")]
        public void EncodeDecodeRLE16()
        {
            var files = Directory.GetFiles(TestData.Resolve(""));
            foreach (var testFile in files)
            {
                try
                {
                    var myOriginalDicomFilePath = testFile;
                    var myOriginalDicomFile = DicomFile.Open(myOriginalDicomFilePath);
                    DicomFile myNewFile = myOriginalDicomFile.Clone(DicomTransferSyntax.RLELossless);
                    DicomFile myResFile = myNewFile.Clone(myOriginalDicomFile.Dataset.InternalTransferSyntax);

                    // Supporting 16bit encoded images
                    var myBitsAllocated = myResFile.Dataset.GetSingleValue<ushort>(DicomTag.BitsAllocated);
                    if (myBitsAllocated == 16)
                    {
                        byte[] myOriginalBytes = DicomPixelData.Create(myOriginalDicomFile.Dataset).GetFrame(0).Data;
                        byte[] myResBytes = DicomPixelData.Create(myResFile.Dataset).GetFrame(0).Data;

                        if (!myOriginalBytes.SequenceEqual(myResBytes))
                        {
                            // Number of different bytes
                            int myDiffCount = myResBytes.Where((inT, inI) => inT != myOriginalBytes[inI]).Count();
                            Assert.Equal(0, myDiffCount);
                            Trace.WriteLine("Diff count " + myDiffCount);

                            // Run through all image
                            for (var myIndex = 0; myIndex < myOriginalBytes.Length; myIndex += 2)
                            {
                                // Get Pixel value from Original image
                                byte myOriginalByte1 = myOriginalBytes[myIndex];
                                byte myOrginalByte2 = myOriginalBytes[myIndex + 1];
                                ushort myOriginalPixelValue =
                                    BitConverter.ToUInt16(new byte[] { myOriginalByte1, myOrginalByte2 }, 0);

                                // Get Pixel value from RoundTrip image
                                byte myResByte1 = myResBytes[myIndex];
                                byte myResByte2 = myResBytes[myIndex + 1];
                                ushort myResPixelValue = BitConverter.ToUInt16(new byte[] { myResByte1, myResByte2 }, 0);

                                // If Value are different
                                if (myOriginalPixelValue != myResPixelValue)
                                {
                                    Trace.Write("Diff:" + Math.Abs(myOriginalPixelValue - myResPixelValue));
                                    Trace.Write(
                                        $" Original Value: {myOriginalPixelValue} ({myOriginalByte1}, {myOrginalByte2})");
                                    Trace.WriteLine($" Res Value: {myResPixelValue} ({myResByte1}, {myResByte2})");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }

        /// <summary>
        /// Uses the RLELossless Transfer Syntax to encode and decode an image and makes sure
        /// that this is idempotent with respect to the pixel data.
        /// </summary>
        [Fact]
        public void EncodeDecodeTestRLE()
        {
            var bytes = new byte[] { 0, 0, 1, 0, 1, 0, 1, 0 };
            CheckData(4, 1, bytes, DicomTransferSyntax.RLELossless);
        }

        /// <summary>
        /// White box testing of the RLELossless TS codec.
        /// </summary>
        [Fact]
        public void EncodeDecodeTestRLE2()
        {
            var r = new Random();
            for (var i = 1; i < 1024; i++)
            {
                for (var k = 0; k < 100; k++)
                {
                    var bytes = new byte[i * 2];
                    for (var j = 0; j < 2*i; j++)
                    {
                        bytes[j] = (byte)(r.Next() % 2);
                    }

                    CheckData(i,1, bytes, DicomTransferSyntax.RLELossless);
                }
            }
        }

        /// <summary>
        /// Constructs a fake image of dimensions {w,h} with the given 2 byte per pixel data. Encodes and decodes
        /// that data using the given Transfer Syntax on a fake 16 bit CT image and checks the data has not changed.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="data">The data.</param>
        /// <param name="syntax">The syntax.</param>
        private void CheckData(int w, int h, byte[] data, DicomTransferSyntax syntax)
        {
            var memoryBB = new MemoryByteBuffer(data);
            var ds = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.Rows, h);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.Columns, w);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.BitsAllocated, 16);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.BitsStored, 16);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.HighBit, 15);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.PixelRepresentation, 1);
            ds.AddOrUpdate(DicomVR.CS, DicomTag.PhotometricInterpretation, "MONOCHROME2");
            ds.AddOrUpdate(DicomVR.IS, DicomTag.SamplesPerPixel, 1);
            var pixelData = DicomPixelData.Create(ds, true);
            pixelData.AddFrame(memoryBB);

            var ds2 = ds.Clone(syntax);
            var dsOrig = ds2.Clone(DicomTransferSyntax.ExplicitVRLittleEndian);

            var origPixData = DicomPixelData.Create(ds);
            var origPixData2 = DicomPixelData.Create(dsOrig);

            var byteBuffer = origPixData.GetFrame(0);
            var byteBuffer2 = origPixData2.GetFrame(0);

            var bytes1 = byteBuffer.Data;
            var bytes2 = byteBuffer2.Data;

            var pixelCount = origPixData.Width * origPixData.Height;
            var pixelCount2 = origPixData2.Width * origPixData2.Height;
            Assert.Equal(pixelCount, pixelCount2);

            for (var i = 0; i < pixelCount * 2; i++)
            {
                Assert.Equal(bytes1[i], bytes2[i]);
            }
        }

#endregion
    }
}
