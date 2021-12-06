// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH1261
    {

        [Fact]
        public void SuccessfullyRenderImageWithSingleColor()
        {
            var ex = Record.Exception(() =>
            {

                var image = new DicomImage(TestData.Resolve("GH1261.dcm"));
                var renderedImage = image.RenderImage();
                Assert.NotNull(renderedImage);
                Assert.True(renderedImage.Width > 1);
            });
            Assert.Null(ex);
        }

    }

}
