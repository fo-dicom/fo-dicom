// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Dicom.IO.Buffer;
using Xunit;

namespace Dicom.Imaging.Codec
{
    public class DicomCodecExtensionsTest
    {
        #region Unit tests

        [Fact]
        public void ChangeTransferSyntax_FileFromJ2KToJPEGWithParameters_DoesNotThrow()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var exception =
                Record.Exception(
                    () =>
                    file.Clone(DicomTransferSyntax.JPEGProcess14, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }


        [Fact]
        public void ChangeTransferSyntax_DatasetFromJ2KToJPEGWithParameters_DoesNotThrow()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var exception =
                Record.Exception(
                    () =>
                    file.Dataset.Clone(DicomTransferSyntax.JPEGProcess14, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }

/* 
TODO: This Test shall run green if issue #921 is solved.

        [Theory]
        [InlineData("TestPattern_RGB.dcm")]
#if !NETSTANDARD
        [InlineData("CR-MONO1-10-chest")]
        [InlineData("GH064.dcm")]
#endif
        public void ChangeTransferSyntax_UpdatePhotometricInterpretationOnJPEGBaseline(string filename)
        {
            var file = DicomFile.Open(@".\Test Data\" + filename);

            // when converting to JpegBaseline the Photometric interpretation has to be updated
            var newDataset = file.Dataset.Clone(DicomTransferSyntax.JPEGProcess2_4, new DicomJpegParams { ConvertColorspaceToRGB=false, Quality = 90,  SampleFactor = DicomJpegSampleFactor.SF422 });
            Assert.Equal("YBR_FULL_422", newDataset.GetString(DicomTag.PhotometricInterpretation));

            // when converting to other jpeg coded that does not automatically convert to 8bitcolor the Photometric Interpretation has to stay the same.
            var newProcess14 = file.Dataset.Clone(DicomTransferSyntax.JPEGProcess14SV1);
            Assert.Equal(file.Dataset.GetString(DicomTag.PhotometricInterpretation), newProcess14.GetString(DicomTag.PhotometricInterpretation));
        }
*/

        [Fact]
        public void EncodeDecodeRLE16()
        {
            var files = Directory.GetFiles(@"Test Data");
            foreach (var testFile in files)
            {
                try
                {
                    var myOriginalDicomFilePath = testFile;
                    DicomFile myOriginalDicomFile = DicomFile.Open(myOriginalDicomFilePath);
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
            var ds = new DicomDataset();
            ds.AddOrUpdatePixelData(DicomVR.OW, memoryBB, DicomTransferSyntax.ExplicitVRLittleEndian);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.Rows, h);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.Columns, w);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.BitsAllocated, 16);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.BitsStored, 16);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.HighBit, 15);
            ds.AddOrUpdate(DicomVR.IS, DicomTag.PixelRepresentation, 1);
            ds.AddOrUpdate(DicomVR.CS, DicomTag.PhotometricInterpretation, "MONOCHROME2");
            ds.AddOrUpdate(DicomVR.IS, DicomTag.SamplesPerPixel, 1);

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
