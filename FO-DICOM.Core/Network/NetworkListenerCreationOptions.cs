using FellowOakDicom.Log;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Contains the necessary parameters to start a new network listener
    /// </summary>
    public class NetworkListenerCreationOptions
    {
        /// <summary>
        /// IP address(es) to listen to
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Network port to listen on
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The service options of this listener
        /// </summary>
        public DicomServiceOptions ServiceOptions { get; set; }

        /// <summary>
        /// The logger to use
        /// </summary>
        public ILogger Logger { get; set; }
    }
}