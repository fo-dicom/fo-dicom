using FellowOakDicom.Log;
using System.Text;

namespace FellowOakDicom.Network.Client.Advanced
{
    public class OpenConnectionRequest
    {
        public NetworkStreamCreationOptions NetworkStreamCreationOptions { get; set; }

        public DicomServiceOptions DicomServiceOptions { get; set; }

        public IAdvancedDicomClientConnectionInterceptor ConnectionInterceptor { get; set; }

        public ILogger Logger { get; set; }

        public Encoding FallbackEncoding { get; set; }
    }
}