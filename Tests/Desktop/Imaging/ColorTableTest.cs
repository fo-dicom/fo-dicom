﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.IO;

    using Xunit;

    [Collection("General")]
    public class ColorTableTest
    {
        #region Unit tests

        [Fact]
        public void SaveLut_ValidTable_Succeeds()
        {
            var path = @".\Test Data\monochrome1.lut";
            if (File.Exists(path)) File.Delete(path);

            ColorTable.SaveLUT(path, ColorTable.Monochrome1);
            Assert.True(File.Exists(path));
        }

        [Fact]
        public void LoadLut_SavedMonochrome2_ReproduceMonochrome2Field()
        {
            var path = @".\Test Data\monochrome2.lut";
            ColorTable.SaveLUT(path, ColorTable.Monochrome2);

            var expected = ColorTable.Monochrome2;
            var actual = ColorTable.LoadLUT(path);

            for (var i = 0; i < actual.Length; ++i)
            {
                Assert.Equal(expected[i].A, actual[i].A);
                Assert.Equal(expected[i].R, actual[i].R);
                Assert.Equal(expected[i].G, actual[i].G);
                Assert.Equal(expected[i].B, actual[i].B);
            }
        }

        #endregion
    }
}
