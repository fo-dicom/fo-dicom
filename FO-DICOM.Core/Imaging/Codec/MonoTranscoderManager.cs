// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;
using System.Reflection;
using FellowOakDicom.Log;

namespace FellowOakDicom.Imaging.Codec
{

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
            Assembly assembly = null;
            if (search == null)
            {
#if NET35
                assembly = typeof(MonoTranscoderManager).Assembly;
#else
                assembly = IntrospectionExtensions.GetTypeInfo(typeof(MonoTranscoderManager)).Assembly;
#endif
            }
            else
            {
                var log = LogManager.GetLogger("Dicom.Imaging.Codec");
                log.Warn("Codec loading from external assemblies not yet implemented.");
            }

            if (assembly == null) return;

#if NET35
            var types =
                assembly.GetTypes().Where(
                    ti => ti.IsClass && !ti.IsAbstract && ti.GetInterfaces().Contains(typeof(IDicomCodec)));

            foreach (var type in types)
            {
                var codec = (IDicomCodec)Activator.CreateInstance(type);
                Codecs[codec.TransferSyntax] = codec;
            }
#else
            var types =
                assembly.DefinedTypes.Where(
                    ti => ti.IsClass && !ti.IsAbstract && ti.ImplementedInterfaces.Contains(typeof(IDicomCodec)));

            foreach (var ti in types)
            {
                var codec = (IDicomCodec)Activator.CreateInstance(ti.AsType());
                Codecs[codec.TransferSyntax] = codec;
            }
#endif
        }

        #endregion
    }
}
