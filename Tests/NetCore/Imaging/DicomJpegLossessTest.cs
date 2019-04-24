// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging.Codec;
using Xunit;

namespace Dicom.Imaging
{

    public class DicomJpegLossessTest
    {

        [Theory]
        [InlineData("GH538-jpeg1.dcm")]
        [InlineData("GH538-jpeg14sv1.dcm")]
        [InlineData("CR-MONO1-10-chest")]
        public void DicomJpegLosses_DecodingAvailable(string filename)
        {
            ImageManager.SetImplementation(RawImageManager.Instance);
            var file = DicomFile.Open(@".\Test Data\" + filename, FileReadOption.ReadAll);
            var dcmImage = new DicomImage(file.Dataset);
            var image = dcmImage.RenderImage();
            Assert.NotNull(image);
        }

        [Fact]
        public void DicomJpegLosses_EncodingShallThrow()
        {
            var file = DicomFile.Open(@".\Test Data\GH538-jpeg14sv1.dcm", FileReadOption.ReadAll);
            var decoded = file.Dataset.Clone(DicomTransferSyntax.ImplicitVRLittleEndian);
            var ex = Record.Exception(() =>
            {
                var encoded = decoded.Clone(DicomTransferSyntax.JPEGProcess14);
            });
            Assert.IsType<DicomCodecException>(ex);
        }


    }
}
