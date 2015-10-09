// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO;
    using Dicom.Log;
    using Dicom.Network;

    public static class DesktopManagers
    {
        public static void Setup(ImageManager imageManagerImpl, LogManager logManagerImpl = null)
        {
            Managers.Setup(
                DesktopIOManager.Instance,
                DesktopNetworkManager.Instance,
                DesktopTranscoderManager.Instance,
                imageManagerImpl,
                logManagerImpl);
        }
    }
}
