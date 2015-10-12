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

    public class SetupFixture : IDisposable
    {
        public SetupFixture()
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
        }
    }

    [CollectionDefinition("General")]
    public class GeneralCollection : ICollectionFixture<SetupFixture>
    {
    }

    [CollectionDefinition("Imaging")]
    public class ImagingCollection : ICollectionFixture<SetupFixture>
    {
    }

    [CollectionDefinition("Network")]
    public class NetworkCollection : ICollectionFixture<SetupFixture>
    {
    }
}
