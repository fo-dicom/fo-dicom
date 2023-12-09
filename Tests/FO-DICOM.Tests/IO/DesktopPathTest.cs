// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using Xunit;

namespace FellowOakDicom.Tests.IO
{

    [Collection(TestCollections.General)]
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
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var actual = Path.GetDirectoryName(path);
                Assert.Equal(expected, actual);
            }
        }

#endregion
    }
}
