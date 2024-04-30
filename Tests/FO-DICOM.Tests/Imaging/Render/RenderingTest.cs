// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Render
{

    [Collection(TestCollections.ImageSharp)]
    public class RenderingTest
    {

        [Fact]
        public void RenderRTWithDoubleValues()
        {
            var myDicomFile = DicomFile.Open(TestData.Resolve("RI6XDosiGap.dcm"));

            myDicomFile.Dataset.AddOrUpdate(DicomTag.VOILUTFunction, "LINEAR_EXACT");

            var myDicomImage = new DicomImage(myDicomFile.Dataset);
            IImage myImg = myDicomImage.RenderImage(0);

            var col1 = myImg.GetPixel(myImg.Width / 2, myImg.Height / 2 );

            Assert.True(col1.R>0);
        }

        [FactForNetCore]
        public void RenderJpeg2000_YBR_RCT()
        {
            var myDicomFile = DicomFile.Open(TestData.Resolve("VL5_J2KI.dcm"));

            myDicomFile.Dataset.AddOrUpdate(DicomTag.VOILUTFunction, "LINEAR_EXACT");

            var myDicomImage = new DicomImage(myDicomFile.Dataset);
            IImage myImg = myDicomImage.RenderImage(0);

            var col1 = myImg.GetPixel(myImg.Width / 2, myImg.Height / 2);

            Assert.True(col1.R > 0);
        }

    }
}
