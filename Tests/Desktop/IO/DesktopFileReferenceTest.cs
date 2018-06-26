// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.IO;

    using Xunit;

    [Collection("General")]
    public class DesktopFileReferenceTest
    {
        #region Unit tests

        [Fact]
        public void Constructor_TempFile_TempFileAttributeSet()
        {
            var path = @".\Test Data\tmp.tmp";
            File.Create(path).Dispose();

            var file = new DesktopFileReference(path) { IsTempFile = true };
            Assert.True((File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary);
        }

        [Fact]
        public void Constructor_RegularFile_TempFileAttributeNotSet()
        {
            var path = @".\Test Data\nontmp.tmp";
            File.Create(path).Dispose();

            var file = new DesktopFileReference(path);
            Assert.False((File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary);
        }

        #endregion
    }
}
