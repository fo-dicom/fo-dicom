using FellowOakDicom.Log;

namespace FellowOakDicom.Network.Client.Advanced
{
    /// <summary>
    /// Represents the parameters that modify the creation of an advanced DICOM client
    /// </summary>
    public class AdvancedDicomClientCreationRequest
    {
        /// <summary>
        /// Gets or sets the logger that will be used by this DicomClient
        /// </summary>
        public ILogger Logger { get; set; }
    }
}