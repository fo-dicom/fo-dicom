// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Bugs
{

    public class GH553
    {
        [Fact(Skip = "Fails for unknown reason when run on CI server.")]
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

            var height = oldPixelData.Height;
            var width = oldPixelData.Width;

            using (var oldImage = new DicomImage(oldFile.Dataset).RenderImage().AsSharedBitmap())
            using (var newImage = new DicomImage(newFile.Dataset).RenderImage().AsSharedBitmap())
            {
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
