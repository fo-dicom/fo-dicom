// Copyright (c) 2012-2024 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using System;
using System.IO;
using Xunit;

namespace FellowOakDicom.Tests.IO
{

    [Collection("Windows")]
    public class TemporaryFileTest
    {
        #region Fields

        private readonly object _locker = new object();

        #endregion

        #region Unit tests

        [Fact]
        public void Create_StoragePathNull_FileAttributesContainTempFlag()
        {
            lock (_locker)
            {
                TemporaryFile.StoragePath = null;
                var path = TemporaryFile.Create().Name;
                Assert.True((File.GetAttributes(path) & FileAttributes.Temporary) == FileAttributes.Temporary);
            }
        }

        #endregion
    }
}
