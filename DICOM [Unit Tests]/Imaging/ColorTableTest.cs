// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.IO;

    using Xunit;

    public class ColorTableTest
    {
        [Fact]
        public void SaveLut_ValidTable_Succeeds()
        {
            var path = @".\Test Data\monochrome1.lut";
            if (File.Exists(path)) File.Delete(path);

            ColorTable.SaveLUT(path, ColorTable.Monochrome1);
            Assert.True(File.Exists(path));
        }
    }
}