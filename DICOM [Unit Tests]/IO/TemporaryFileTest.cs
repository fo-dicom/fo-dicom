// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.IO;

    using Xunit;

    public class TemporaryFileTest
    {
        [Fact]
        public void StoragePath_Setter_DirectoryCreatedIfNonExisting()
        {
            var path = @".\Test Data\Temporary Path";
            if (Directory.Exists(path)) Directory.Delete(path);

            TemporaryFile.StoragePath = path;
            Assert.True(Directory.Exists(path));
        }

        [Fact]
        public void StoragePath_Setter_NullShouldNotThrow()
        {
            TemporaryFile.StoragePath = null;
        }
    }
}