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
    /// Helper class for initializing manager implementations.
    /// </summary>
    public partial class Managers
    {
        #region FIELDS

        protected static LogManager PlatformLogManagerImpl;

        protected static readonly IOManager PlatformIOManagerImpl;

        protected static readonly NetworkManager PlatformNetworkManagerImpl;

        protected static readonly TranscoderManager PlatformTranscoderManagerImpl;

        protected static ImageManager PlatformImageManagerImpl;

        #endregion

        #region METHODS

        /// <summary>
        /// Setup implementations for all managers.
        /// </summary>
        /// <param name="logManagerImpl">Log manager implementation.</param>
        /// <param name="ioManagerImpl">I/O manage implementation.</param>
        /// <param name="networkManagerImpl">Network manager implementation.</param>
        /// <param name="transcoderManagerImpl">DICOM transcoder manager implementation.</param>
        /// <param name="imageManagerImpl">Image manager implementation.</param>
        public static void Setup(
            LogManager logManagerImpl,
            IOManager ioManagerImpl,
            NetworkManager networkManagerImpl,
            TranscoderManager transcoderManagerImpl,
            ImageManager imageManagerImpl)
        {
            LogManager.SetImplementation(logManagerImpl);
            IOManager.SetImplementation(ioManagerImpl);
            NetworkManager.SetImplementation(networkManagerImpl);
            TranscoderManager.SetImplementation(transcoderManagerImpl);
            ImageManager.SetImplementation(imageManagerImpl);
        }

        /// <summary>
        /// Setup specified log and image managers, and platform specific managers for remaining implementations.
        /// </summary>
        /// <param name="logManagerImpl">Selected log manager implementation.</param>
        /// <param name="imageManagerImpl">Selected image manager implementation.</param>
        public static void Setup(LogManager logManagerImpl, ImageManager imageManagerImpl)
        {
            if (logManagerImpl != null) PlatformLogManagerImpl = logManagerImpl;
            if (imageManagerImpl != null) PlatformImageManagerImpl = imageManagerImpl;

            Setup(
                logManagerImpl,
                PlatformIOManagerImpl,
                PlatformNetworkManagerImpl,
                PlatformTranscoderManagerImpl,
                imageManagerImpl);
        }

        /// <summary>
        /// Setup specified log manager, and platform specific managers for remaining implementations.
        /// </summary>
        /// <param name="logManagerImpl">Selected log manager implementation.</param>
        public static void Setup(LogManager logManagerImpl)
        {
            if (logManagerImpl != null) PlatformLogManagerImpl = logManagerImpl;

            Setup(
                logManagerImpl,
                PlatformIOManagerImpl,
                PlatformNetworkManagerImpl,
                PlatformTranscoderManagerImpl,
                PlatformImageManagerImpl);
        }

        /// <summary>
        /// Setup specified image manager, and platform specific managers for remaining implementations.
        /// </summary>
        /// <param name="imageManagerImpl">Selected image manager implementation.</param>
        public static void Setup(ImageManager imageManagerImpl)
        {
            if (imageManagerImpl != null) PlatformImageManagerImpl = imageManagerImpl;

            Setup(
                PlatformLogManagerImpl,
                PlatformIOManagerImpl,
                PlatformNetworkManagerImpl,
                PlatformTranscoderManagerImpl,
                imageManagerImpl);
        }

        /// <summary>
        /// Setup platform specific managers for all implementations.
        /// </summary>
        public static void Setup()
        {
            Setup(
                PlatformLogManagerImpl,
                PlatformIOManagerImpl,
                PlatformNetworkManagerImpl,
                PlatformTranscoderManagerImpl,
                PlatformImageManagerImpl);
        }

        #endregion
    }
}