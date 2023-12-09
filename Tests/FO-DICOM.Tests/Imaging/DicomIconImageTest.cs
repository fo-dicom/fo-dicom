// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Log;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{
    [Collection(TestCollections.Imaging)]
    public class DicomIconImageTest
    {
        [Fact]
        public void Read_IconImageSequence_From_Native_Dataset()
        {
            var file = DicomFile.Open(TestData.Resolve("GH195.dcm"));
            var success = DicomIconImage.TryCreate(file.Dataset, out var icon);
            Assert.True(success);
            Assert.Equal(132, icon.Width);
            Assert.Equal(74, icon.Height);
        }

        [Fact]
        public void Read_Native_IconImageSequence_From_Encapsulated_Dataset()
        {
            var file = DicomFile.Open(TestData.Resolve("JPEGwithIcon.dcm"));
            var success = DicomIconImage.TryCreate(file.Dataset, out var icon);
            Assert.True(success);
            Assert.Equal(64, icon.Width);
            Assert.Equal(64, icon.Height);

            // Render image to verify palette is correctly used
            var buffer = icon.RenderImage().As<byte[]>();

            // Verify that the palette has been successfully applied by checking a specific pixel value
            // ARGB (x:=39, y:=24) == [255, 122, 76, 89]
            // Position within buffer: (24 * 64 + 39) * 4 = 6300

            // Components stored BGRA in buffer
            Assert.Equal(89, buffer[6300]);
            Assert.Equal(76, buffer[6301]);
            Assert.Equal(122, buffer[6302]);
            Assert.Equal(255, buffer[6303]);
        }

        //public void Read_Encapsulated_IconImageSequence_From_Encapsulated_Dataset()
        //{
        //    // Get a sample image to test this!
        //}
    }
}

