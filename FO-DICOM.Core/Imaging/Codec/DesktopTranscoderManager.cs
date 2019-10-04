// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

using System.ComponentModel.Composition.Hosting;

using System.IO;
using System.Reflection;

using Dicom.Log;

namespace Dicom.Imaging.Codec
{
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

            if (!foundAnyCodecs)
            {
                log.Warn("No Dicom codecs were found after searching {path}\\{wildcard}", path, search);
            }
        }

        #endregion
    }
}
