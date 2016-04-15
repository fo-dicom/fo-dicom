// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    using System;

#if NET35
    using System.Linq;
#else
    using System.ComponentModel.Composition.Hosting;
#endif

    using System.IO;
    using System.Reflection;

    using Dicom.Log;

    /// <summary>
    /// Implementation of <see cref="TranscoderManager"/> for Windows desktop (.NET) applications.
    /// </summary>
    public sealed class DesktopTranscoderManager : TranscoderManager
    {
        #region FIELDS

        /// <summary>
        /// Singleton instance of the <see cref="DesktopTranscoderManager"/>.
        /// </summary>
        public static readonly TranscoderManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="DesktopTranscoderManager"/>.
        /// </summary>
        static DesktopTranscoderManager()
        {
            Instance = new DesktopTranscoderManager();
        }

        /// <summary>
        /// Initializes an instance of <see cref="DesktopTranscoderManager"/>.
        /// </summary>
        public DesktopTranscoderManager()
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
            log.Debug("Searching {path}\\{wildcard} for Dicom codecs", path, search);

            var foundAnyCodecs = false;

#if NET35
            var assemblyPaths = Directory.GetFiles(path, search);

            foreach (var assemblyPath in assemblyPaths)
            {
                try
                {
                    var assembly = Assembly.LoadFile(assemblyPath);

                    var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(IDicomCodec)));
                    var codecs = types.Select(t => (IDicomCodec)Activator.CreateInstance(t));

                    foreach (var codec in codecs)
                    {
                        foundAnyCodecs = true;
                        log.Debug("Codec: {codecName}", codec.TransferSyntax.UID.Name);
                        Codecs[codec.TransferSyntax] = codec;
                    }
                }
                catch (Exception ex)
                {
                    log.Warn("Could not load assembly '{path}' due to '{message}'", assemblyPath, ex.Message);
                }
            }
#else
            DirectoryCatalog catalog;
            try
            {
                catalog = new DirectoryCatalog(path, search);
            }
            catch (Exception ex)
            {
                log.Error(
                    "Error encountered creating new DirectCatalog({path}, {search}) - {@exception}",
                    path,
                    search,
                    ex);
                throw;
            }

            var container = new CompositionContainer(catalog);
            foreach (var lazy in container.GetExports<IDicomCodec>())
            {
                foundAnyCodecs = true;
                var codec = lazy.Value;
                log.Debug("Codec: {codecName}", codec.TransferSyntax.UID.Name);
                Codecs[codec.TransferSyntax] = codec;
            }
#endif

            if (!foundAnyCodecs)
            {
                log.Warn("No Dicom codecs were found after searching {path}\\{wildcard}", path, search);
            }
        }

        #endregion
    }
}