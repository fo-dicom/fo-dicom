// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO;
    using Dicom.Network;

    /// <summary>
    /// Convenience class for enabling Mono managers.
    /// </summary>
    public partial class Managers
    {
        /// <summary>
        /// Set standard implementations for Mono desktop applications.
        /// </summary>
        static Managers()
        {
            PlatformLogManagerImpl = null;
            PlatformIOManagerImpl = DesktopIOManager.Instance;
            PlatformNetworkManagerImpl = DesktopNetworkManager.Instance;
            PlatformTranscoderManagerImpl = MonoTranscoderManager.Instance;
            PlatformImageManagerImpl = WinFormsImageManager.Instance;
        }
    }
}
