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
    public static class Managers
    {
        /// <summary>
        /// Setup implementations for all managers.
        /// </summary>
        /// <param name="ioManagerImpl">I/O manage implementation.</param>
        /// <param name="networkManagerImpl">Network manager implementation.</param>
        /// <param name="transcoderManagerImpl">DICOM transcoder manager implementation.</param>
        /// <param name="imageManagerImpl">Image manager implementation.</param>
        /// <param name="logManagerImpl">Log manager implementation.</param>
        public static void Setup(
            IOManager ioManagerImpl,
            NetworkManager networkManagerImpl,
            TranscoderManager transcoderManagerImpl,
            ImageManager imageManagerImpl,
            LogManager logManagerImpl)
        {
            LogManager.SetImplementation(logManagerImpl);
            IOManager.SetImplementation(ioManagerImpl);
            TranscoderManager.SetImplementation(transcoderManagerImpl);
            NetworkManager.SetImplementation(networkManagerImpl);
            ImageManager.SetImplementation(imageManagerImpl);
        }
    }
}