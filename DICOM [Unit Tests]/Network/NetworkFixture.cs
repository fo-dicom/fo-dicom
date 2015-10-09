// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;

    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO;

    using Xunit;

    public class NetworkFixture : IDisposable
    {
        public NetworkFixture()
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

    [CollectionDefinition("Network")]
    public class NetworkCollection : ICollectionFixture<NetworkFixture>
    {
    }
}
