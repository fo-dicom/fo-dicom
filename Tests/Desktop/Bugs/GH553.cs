// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging;
using Dicom.IO.Buffer;

using Xunit;

namespace Dicom.Bugs
{
    public class GH553
    {
        [Fact]
        public void DicomPixelDataCreate_NewPixelDataFromOldFragmentedFile_ReproducesData()
        {
            var oldFile = DicomFile.Open(@"Test Data\D_CLUNIE_CT1_RLE_FRAGS.dcm");
            var newFile = oldFile.Clone();

            var oldPixelData = DicomPixelData.Create(oldFile.Dataset);
            var newPixelData = DicomPixelData.Create(newFile.Dataset, true);

            Assert.Equal(oldFile.Dataset.InternalTransferSyntax, newPixelData.Syntax);

            var oldBuffer = oldPixelData.GetFrame(0);
            Assert.IsType<CompositeByteBuffer>(oldBuffer);

            newPixelData.AddFrame(oldBuffer);

            using (var oldImage = new DicomImage(oldFile.Dataset).RenderImage().AsBitmap())
            using (var newImage = new DicomImage(newFile.Dataset).RenderImage().AsBitmap())
            {
                var height = oldImage.Height;
                var width = oldImage.Width;

                for (var j = 0; j < height; ++j)
                {
                    for (var i = 0; i < width; ++i)
                    {
                        Assert.Equal(oldImage.GetPixel(i, j), newImage.GetPixel(i, j));
                    }
                }
            }
            newFile.Save("GH553.dcm");
        }
    }
}
