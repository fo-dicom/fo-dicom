using FellowOakDicom.Log;
using FellowOakDicom.Network.Client;
using System;
using System.Net.Sockets;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Contains the necessary parameters to open a new network stream to another DICOM SCP
    /// </summary>
    public class NetworkStreamCreationOptions
    {
        /// <summary>
        /// The IP address or host name of the SCP
        /// </summary>
        public string Host { get; set; }
        
        /// <summary>
        /// The port on which the SCP is listening
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        /// Whether or not to disable the delay when buffers are not full
        /// </summary>
        /// <seealso cref="System.Net.Sockets.TcpClient.NoDelay"/>
        public bool NoDelay { get; set; }

        /// <summary>
        /// After how much time a write or read operation over the TCP connection must time out
        /// </summary>
        /// <seealso cref="NetworkStream.ReadTimeout"/>
        /// <seealso cref="NetworkStream.WriteTimeout"/>
        /// <seealso cref="System.Net.Security.SslStream.ReadTimeout"/>
        /// <seealso cref="System.Net.Security.SslStream.WriteTimeout"/>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Whether or not to use TLS (SslStream) to connect
        /// </summary>
        /// <see cref="TlsOptions"/>
        public bool UseTls { get; set; }

        /// <summary>
        /// Configures TLS authentication
        /// </summary>
        public DicomClientTlsOptions TlsOptions { get; set; } = new DicomClientTlsOptions();

        /// <summary>
        /// The logger to use
        /// </summary>
        public ILogger Logger { get; set; }
    }
}