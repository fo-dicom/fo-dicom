// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Linq;

namespace Dicom.Imaging.Codec
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Implementation of <see cref="TranscoderManager"/> for Universal Windows Platform applications.
    /// </summary>
    public sealed class WindowsTranscoderManager : TranscoderManager
    {
        #region FIELDS

        /// <summary>
        /// Singleton instance of the <see cref="WindowsTranscoderManager"/>.
        /// </summary>
        public static readonly TranscoderManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="WindowsTranscoderManager"/>.
        /// </summary>
        static WindowsTranscoderManager()
        {
            Instance = new WindowsTranscoderManager();
        }

        /// <summary>
        /// Initializes an instance of <see cref="WindowsTranscoderManager"/>.
        /// </summary>
        public WindowsTranscoderManager()
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
            var assembly = typeof(WindowsTranscoderManager).GetTypeInfo().Assembly;
            var types =
                assembly.DefinedTypes.Where(
                    ti => ti.IsClass && !ti.IsAbstract && ti.ImplementedInterfaces.Contains(typeof(IDicomCodec)));

            foreach (var ti in types)
            {
                var codec = (IDicomCodec)Activator.CreateInstance(ti.AsType());
                Codecs[codec.TransferSyntax] = codec;
            }
        }

        #endregion
    }
}
