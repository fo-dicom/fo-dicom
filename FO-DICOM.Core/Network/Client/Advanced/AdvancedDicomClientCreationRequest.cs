using FellowOakDicom.Log;

namespace FellowOakDicom.Network.Client.Advanced
{
    public class AdvancedDicomClientCreationRequest
    {
        /// <summary>
        /// Gets or sets the logger that will be used by this DicomClient
        /// </summary>
        public ILogger Logger { get; set; }
    }
}