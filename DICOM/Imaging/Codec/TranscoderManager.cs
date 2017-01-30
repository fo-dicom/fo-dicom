// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    using System.Collections.Generic;

    /// <summary>
    /// Abstract manager class for DICOM transcoder operations.
    /// </summary>
    public abstract class TranscoderManager
    {
        #region FIELDS

        /// <summary>
        /// Selected DICOM transcoder implementation.
        /// </summary>
        private static TranscoderManager implementation;

        /// <summary>
        /// Collection of known transfer syntaxes and their associated codecs.
        /// </summary>
        protected static readonly Dictionary<DicomTransferSyntax, IDicomCodec> Codecs =
            new Dictionary<DicomTransferSyntax, IDicomCodec>();

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the single platform-specific transcode manager.
        /// </summary>
        static TranscoderManager()
        {
            SetImplementation(Setup.GetSinglePlatformInstance<TranscoderManager>());
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Set the implementation to use for DICOM transcoder management.
        /// </summary>
        /// <param name="impl"></param>
        public static void SetImplementation(TranscoderManager impl)
        {
            implementation = impl;
        }

        /// <summary>
        /// Get codec associated with specified DICOM transfer syntax.
        /// </summary>
        /// <param name="syntax">Transfer syntax.</param>
        /// <returns>Codec associated with <paramref name="syntax"/>.</returns>
        /// <exception cref="DicomCodecException">if no codec is available for the specified <paramref name="syntax"/>.</exception>
        public static IDicomCodec GetCodec(DicomTransferSyntax syntax)
        {
            IDicomCodec codec;
            if (!Codecs.TryGetValue(syntax, out codec)) throw new DicomCodecException("No codec registered for tranfer syntax: {0}", syntax);
            return codec;
        }

        /// <summary>
        /// Load codecs from assembly(ies) at the specified <paramref name="path"/> and with the specified <paramref name="search"/> pattern.
        /// </summary>
        /// <param name="path">Directory path to codec assemblies.</param>
        /// <param name="search">Search pattern for codec assemblies.</param>
        public static void LoadCodecs(string path = null, string search = null)
        {
            implementation.LoadCodecsImpl(path, search);
        }

        /// <summary>
        /// Implementation of method to load codecs from assembly(ies) at the specified <paramref name="path"/> and 
        /// with the specified <paramref name="search"/> pattern.
        /// </summary>
        /// <param name="path">Directory path to codec assemblies.</param>
        /// <param name="search">Search pattern for codec assemblies.</param>
        protected abstract void LoadCodecsImpl(string path, string search);

        #endregion
    }
}