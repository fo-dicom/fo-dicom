// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;

namespace FellowOakDicom.Imaging.Codec
{
    public interface ITranscoderManager
    {
        /// <summary>
        /// Checks whether transcoder provides codec for specified <paramref name="syntax">transfer syntax</paramref>.
        /// </summary>
        /// <param name="syntax">Transfer syntax.</param>
        /// <returns>True if transcoder provides codec for <paramref name="syntax"/>, false otherwise.</returns>
        bool HasCodec(DicomTransferSyntax syntax);

        /// <summary>
        /// Checks whether transcoder can convert from <paramref name="inSyntax"/> to <paramref name="outSyntax"/>.
        /// </summary>
        /// <param name="inSyntax">Input (decode) transfer syntax.</param>
        /// <param name="outSyntax">Output (encode) transfer syntax.</param>
        /// <returns>True if transcoder can convert from <paramref name="inSyntax"/> to <paramref name="outSyntax"/>, 
        /// false otherwise.</returns>
        bool CanTranscode(DicomTransferSyntax inSyntax, DicomTransferSyntax outSyntax);

        /// <summary>
        /// Get codec associated with specified DICOM transfer syntax.
        /// </summary>
        /// <param name="syntax">Transfer syntax.</param>
        /// <returns>Codec associated with <paramref name="syntax"/>.</returns>
        /// <exception cref="DicomCodecException">if no codec is available for the specified <paramref name="syntax"/>.</exception>
        IDicomCodec GetCodec(DicomTransferSyntax syntax);

        /// <summary>
        /// Load codecs from assembly(ies) at the specified <paramref name="path"/> and with the specified <paramref name="search"/> pattern.
        /// </summary>
        /// <param name="path">Directory path to codec assemblies.</param>
        /// <param name="search">Search pattern for codec assemblies.</param>
        void LoadCodecs(string path = null, string search = null);
    }

    /// <summary>
    /// Abstract manager class for DICOM transcoder operations.
    /// </summary>
    public abstract class TranscoderManager : ITranscoderManager
    {
        #region FIELDS

        /// <summary>
        /// Collection of known transfer syntaxes and their associated codecs.
        /// </summary>
        protected readonly Dictionary<DicomTransferSyntax, IDicomCodec> Codecs =
            new Dictionary<DicomTransferSyntax, IDicomCodec>();

        #endregion

        #region METHODS

        /// <summary>
        /// Checks whether transcoder provides codec for specified <paramref name="syntax">transfer syntax</paramref>.
        /// </summary>
        /// <param name="syntax">Transfer syntax.</param>
        /// <returns>True if transcoder provides codec for <paramref name="syntax"/>, false otherwise.</returns>
        public bool HasCodec(DicomTransferSyntax syntax)
            => Codecs.ContainsKey(syntax);

        /// <summary>
        /// Checks whether transcoder can convert from <paramref name="inSyntax"/> to <paramref name="outSyntax"/>.
        /// </summary>
        /// <param name="inSyntax">Input (decode) transfer syntax.</param>
        /// <param name="outSyntax">Output (encode) transfer syntax.</param>
        /// <returns>True if transcoder can convert from <paramref name="inSyntax"/> to <paramref name="outSyntax"/>, 
        /// false otherwise.</returns>
        public bool CanTranscode(DicomTransferSyntax inSyntax, DicomTransferSyntax outSyntax)
            => (!inSyntax.IsEncapsulated || Codecs.ContainsKey(inSyntax))
            && (!outSyntax.IsEncapsulated || Codecs.ContainsKey(outSyntax));

        /// <summary>
        /// Get codec associated with specified DICOM transfer syntax.
        /// </summary>
        /// <param name="syntax">Transfer syntax.</param>
        /// <returns>Codec associated with <paramref name="syntax"/>.</returns>
        /// <exception cref="DicomCodecException">if no codec is available for the specified <paramref name="syntax"/>.</exception>
        public IDicomCodec GetCodec(DicomTransferSyntax syntax)
        {
            if (!Codecs.TryGetValue(syntax, out IDicomCodec codec))
            {
                throw new DicomCodecException($"No codec registered for tranfer syntax: {syntax}");
            }

            return codec;
        }

        /// <summary>
        /// Load codecs from assembly(ies) at the specified <paramref name="path"/> and with the specified <paramref name="search"/> pattern.
        /// </summary>
        /// <param name="path">Directory path to codec assemblies.</param>
        /// <param name="search">Search pattern for codec assemblies.</param>
        public abstract void LoadCodecs(string path = null, string search = null);

        #endregion

    }
}
