// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    using System;
    using System.IO;
    using System.Reflection;

    using Dicom.Log;

    /// <summary>
    /// Implementation of <see cref="TranscoderManager"/> for Mono applications.
    /// </summary>
    public sealed class MonoTranscoderManager : TranscoderManager
    {
        #region FIELDS

        /// <summary>
        /// Singleton instance of the <see cref="MonoTranscoderManager"/>.
        /// </summary>
        public static readonly TranscoderManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="MonoTranscoderManager"/>.
        /// </summary>
        static MonoTranscoderManager()
        {
            Instance = new MonoTranscoderManager();
        }

        /// <summary>
        /// Initializes an instance of <see cref="MonoTranscoderManager"/>.
        /// </summary>
        public MonoTranscoderManager()
        {
            this.LoadCodecsImpl(null, null);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Implementation of method to load codecs from assembly(ies) at the specified <paramref name="path"/> and 
        /// with the specified <paramref name="search"/> pattern.
        /// </summary>
        /// <param name="path">Directory path to codec assemblies.</param>
        /// <param name="search">Search pattern for codec assemblies.</param>
        protected override void LoadCodecsImpl(string path, string search)
        {
            if (path == null) path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().EscapedCodeBase).LocalPath);

            if (search == null) search = "Dicom.Native*.dll";

            var log = LogManager.GetLogger("Dicom.Imaging.Codec");
            log.Warn("Codec loading for Mono not yet implemented.");
        }

        #endregion
    }
}
