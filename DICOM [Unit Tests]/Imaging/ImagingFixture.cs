// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;

    using Dicom.Imaging.Codec;
    using Dicom.IO;
    using Dicom.Network;

    using Xunit;

    public class ImagingFixture : IDisposable
    {
        public ImagingFixture()
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

    [CollectionDefinition("Imaging")]
    public class ImagingCollection : ICollectionFixture<ImagingFixture>
    {
    }
}
