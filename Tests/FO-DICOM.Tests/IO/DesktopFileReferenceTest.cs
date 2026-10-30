// Copyright (c) 2012-2026 fo-dicom contributors.
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
        
        [Fact]
        public void GetByteRange_PastEndOfFile_FillsAvailableBytes()
        {
            var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var path = Path.GetTempFileName();
            File.WriteAllBytes(path, data);
            var file = new FileReference(path) { IsTempFile = true };

            var output = new byte[10];
            file.GetByteRange(5, 10, output);

            Assert.Equal(data[5..], output[..5]);
        }

        #endregion
    }
}
