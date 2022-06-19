// Copyright (c) 2012-2022 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.Codec;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection("General")]
    public class GH1403
    {
        [Theory]
        [InlineData("GH1403.dcm")]
        public void Transforming_FromToUncompressed_AddsPadding_IfDataSizeIsOdd(string fileName)
        {
            // RGB Dataset has 3 Rows and 3 Columns, and is encoded in Implicit Little Endian
            var file = DicomFile.Open(TestData.Resolve(fileName));
            // pixel data is OW because of the Implicit Little Endian Transfer Syntax
            var pixelData = file.Dataset.GetDicomItem<DicomOtherWord>(DicomTag.PixelData);
            // Length should be 3x3x3=27 bytes plus a padding byte
            Assert.Equal(28u, pixelData.Length);

            var transcoder = new DicomTranscoder(file.Dataset.InternalTransferSyntax,
                DicomTransferSyntax.ExplicitVRLittleEndian);
            var dataset = transcoder.Transcode(file.Dataset);
            // pixel data is OB because of Bits Allocated = 8
            var newPixelData = dataset.GetDicomItem<DicomOtherByte>(DicomTag.PixelData);
            // padding byte shall still be present after the transcoding
            Assert.Equal(28u, newPixelData.Length);
            Assert.Equal(pixelData.Buffer.Data, newPixelData.Buffer.Data);

            // test converting in the other direction
            transcoder = new DicomTranscoder(dataset.InternalTransferSyntax,
                DicomTransferSyntax.ImplicitVRLittleEndian);
            dataset = transcoder.Transcode(file.Dataset);
            pixelData = dataset.GetDicomItem<DicomOtherWord>(DicomTag.PixelData);
            Assert.Equal(28u, pixelData.Length);
            Assert.Equal(pixelData.Buffer.Data, newPixelData.Buffer.Data);

            // test converting to GEPrivateImplicitVRBigEndian
            transcoder = new DicomTranscoder(dataset.InternalTransferSyntax,
                DicomTransferSyntax.GEPrivateImplicitVRBigEndian);
            dataset = transcoder.Transcode(file.Dataset);
            // pixel data is always OB here for odd data size (otherwise it wouldn't be odd),
            // so no byte swapping is needed
            newPixelData = dataset.GetDicomItem<DicomOtherByte>(DicomTag.PixelData);
            Assert.Equal(28u, newPixelData.Length);
            Assert.Equal(pixelData.Buffer.Data, newPixelData.Buffer.Data);
        }
    }
}
