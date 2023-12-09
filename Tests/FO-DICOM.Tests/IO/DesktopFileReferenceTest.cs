// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using System;
using System.IO;
using Xunit;

namespace FellowOakDicom.Tests.IO
{

    [Collection(TestCollections.General)]
    public class DesktopFileReferenceTest
    {
        #region Unit tests

        [Fact]
        public void Constructor_TempFile_TempFileAttributeSet()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var path = TestData.Resolve("tmp.tmp");
                File.Create(path).Dispose();

                var file = new FileReference(path) { IsTempFile = true };
                Assert.True((File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary);
            }
        }

        [Fact]
        public void Constructor_RegularFile_TempFileAttributeNotSet()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var path = TestData.Resolve("nontmp.tmp");
                File.Create(path).Dispose();

                var file = new FileReference(path);
                Assert.False((File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary);
            }
        }

        #endregion
    }
}
