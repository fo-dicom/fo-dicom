// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
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
        public void CheckData(int w, int h, byte[] data, DicomTransferSyntax syntax)
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
