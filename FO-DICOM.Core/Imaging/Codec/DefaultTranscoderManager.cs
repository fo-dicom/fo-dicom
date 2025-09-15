// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom.Imaging.Codec
{

    /// <summary>
    /// Implementation of <see cref="TranscoderManager"/> for Universal Windows Platform applications.
    /// </summary>
    public sealed class DefaultTranscoderManager : TranscoderManager
    {

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DefaultTranscoderManager"/>.
        /// </summary>
        public DefaultTranscoderManager()
        {
            LoadCodecs(null, null);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Implementation of method to load codecs from assembly(ies) at the specified <paramref name="path"/> and 
        /// with the specified <paramref name="search"/> pattern.
        /// </summary>
        /// <param name="path">Directory path to codec assemblies.</param>
        /// <param name="search">Search pattern for codec assemblies.</param>
        public override void LoadCodecs(string path, string search)
        {
            // previously there was a method that loaded the codecs via reflection, but this is not AOT-compilation compatible.
            // because this initialization will not be very dynamically and change often, it is propably fine to do a explicit initialization
            AddCodec(new JpegLosslessDecoderWrapperProcess14());
            AddCodec(new JpegLosslessDecoderWrapperProcess14SV1());
            AddCodec(new DicomRleCodecImpl());
        }

        private void AddCodec(IDicomCodec codec) => Codecs[codec.TransferSyntax] = codec;

        #endregion
    }
}
