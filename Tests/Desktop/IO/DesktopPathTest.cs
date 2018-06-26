// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using Xunit;

    [Collection("General")]
    public class DesktopPathTest
    {
        #region Unit tests

        [Theory]
        [InlineData(@"C:\MyDir\MySubDir\myfile.ext", @"C:\MyDir\MySubDir")]
        [InlineData(@"C:\MyDir\MySubDir", @"C:\MyDir")]
        [InlineData(@"C:\MyDir\", @"C:\MyDir")]
        [InlineData(@"C:\MyDir", @"C:\")]
        [InlineData(@"C:\", null)]
        public void GetDirectoryName_VariousInput_YieldsExpectedOutput(string path, string expected)
        {
            var actual = DesktopPath.Instance.GetDirectoryName(path);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
