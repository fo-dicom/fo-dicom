// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO;
    using Dicom.Log;
    using Dicom.Network;

    /// <summary>
    /// Convenience class for enabling desktop managers.
    /// </summary>
    public static class DesktopManagers
    {
        /// <summary>
        /// Setup managers for desktop (.NET) session. Image and log managers are configurable.
        /// </summary>
        /// <param name="imageManagerImpl">Selected image manager implementation.</param>
        /// <param name="logManagerImpl">Selected log manager implementation.</param>
        public static void Setup(ImageManager imageManagerImpl = null, LogManager logManagerImpl = null)
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
