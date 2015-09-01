// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System.IO;

    using Xunit;

    public class TemporaryFileTest
    {
        #region Unit tests

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

        [Fact]
        public void StoragePath_Getter_DefaultEqualToUserTemp()
        {
            var expected = Path.GetTempPath();

            TemporaryFile.StoragePath = null;
            var actual = TemporaryFile.StoragePath;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_StoragePathNull_LocatedInUserTemp()
        {
            TemporaryFile.StoragePath = null;
            var temp = TemporaryFile.Create();

            var expected = Path.GetTempPath().TrimEnd('\\');
            var actual = Path.GetDirectoryName(temp);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_StoragePathNonNull_LocatedInSpecDirectory()
        {
            var expected = @".\Test Data\Temporary Directory";
            TemporaryFile.StoragePath = expected;

            var temp = TemporaryFile.Create();

            var actual = Path.GetDirectoryName(temp);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}