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
    /// Convenience class for enabling fo-dicom managers on Mono.
    /// </summary>
    public static class MonoManagers
    {
        /// <summary>
        /// Setup managers for Mono session. Log managers is configurable.
        /// </summary>
        /// <param name="logManagerImpl">Selected log manager implementation.</param>
        public static void Setup(LogManager logManagerImpl = null)
        {
            Managers.Setup(
                DesktopIOManager.Instance,
                DesktopNetworkManager.Instance,
                MonoTranscoderManager.Instance,
				WinFormsImageManager.Instance,
                logManagerImpl);
        }
    }
}
