using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using NLog;

namespace Dicom.Imaging.Codec
{
    public partial class DicomTranscoder
    {
        static DicomTranscoder()
        {
            LoadCodecs(null, "Dicom.Native*.dll");
        }

        public static void LoadCodecs(string path = null, string search = null)
        {
            if (path == null)
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var log = LogManager.GetLogger("Dicom.Imaging.Codec");

            var catalog = (search == null) ?
                              new DirectoryCatalog(path) :
                              new DirectoryCatalog(path, search);
            var container = new CompositionContainer(catalog);
            foreach (var lazy in container.GetExports<IDicomCodec>())
            {
                var codec = lazy.Value;
                log.Debug("Codec: {0}", codec.TransferSyntax.UID.Name);
                _codecs[codec.TransferSyntax] = codec;
            }
        }
    }
}