// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Render
{

    [Collection("ImageSharp")]
    public class RenderingTest
    {

        [Fact]
        public void RenderRTWithDoubleValues()
        {
            var myDicomFile = DicomFile.Open(TestData.Resolve("RI6XDosiGap.dcm"));

            myDicomFile.Dataset.AddOrUpdate(DicomTag.VOILUTFunction, "LINEAR_EXACT");

            var myDicomImage = new DicomImage(myDicomFile.Dataset);
            IImage myImg = myDicomImage.RenderImage(0);

            //var image = myImg.AsSharpImage();
            //using (var fs = new FileStream("d:\\image.png", FileMode.OpenOrCreate))
            //{
            //    image.Save(fs, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
            //}

            var col1 = myImg.GetPixel(myImg.Width / 2, myImg.Height / 2 );

            Assert.True(col1.R>0);
        }


    }
}
