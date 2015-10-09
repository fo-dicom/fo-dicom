// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;

    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO;
    using Dicom.Network;

    using Xunit;

    public class GeneralFixture : IDisposable
    {
        public GeneralFixture()
        {
            Managers.Setup(
                DesktopIOManager.Instance,
                DesktopNetworkManager.Instance,
                DesktopTranscoderManager.Instance,
                WinFormsImageManager.Instance,
                null);
        }

        public void Dispose()
        {
            Managers.Setup(null, null, null, null, null);
        }
    }

    [CollectionDefinition("General")]
    public class GeneralCollection : ICollectionFixture<GeneralFixture>
    {
    }
}
