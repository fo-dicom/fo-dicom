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
        public void Transform_FromToUncompressed_AddsPadding_IfNeeded(string fileName)
        {
            // RGB Dataset has 3 Rows and 3 Columns, encoded in Implicit Little Endian
            var file = DicomFile.Open(TestData.Resolve(fileName));
            var pixelData = file.Dataset.GetDicomItem<DicomOtherWord>(DicomTag.PixelData);
            // Length should be 3x3x3=27 bytes plus a padding byte
            Assert.Equal(28u, pixelData.Length);

            var transcoder = new DicomTranscoder(file.Dataset.InternalTransferSyntax,
                DicomTransferSyntax.ExplicitVRLittleEndian);
            var dataset = transcoder.Transcode(file.Dataset);
            var newPixelData = dataset.GetDicomItem<DicomOtherByte>(DicomTag.PixelData);
            // padding byte shall still be present after the transcoding
            Assert.Equal(28u, newPixelData.Length);
        }
    }
}
