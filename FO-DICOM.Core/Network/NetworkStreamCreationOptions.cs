using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

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
        /// Whether or not to use TLS (SSLStream) when opening the TCP connection
        /// </summary>
        public bool UseTls { get; set; }
        
        /// <summary>
        /// Whether or not to disable the delay when buffers are not full
        /// </summary>
        /// <seealso cref="System.Net.Sockets.TcpClient.NoDelay"/>
        public bool NoDelay { get; set; }
        
        /// <summary>
        /// Whether or not to ignore any certificate validation errors that occur when authenticating as a client over SSL
        /// </summary>
        public bool IgnoreSslPolicyErrors { get; set; }
        
        /// <summary>
        /// After how much time a write or read operation over the TCP connection must time out
        /// </summary>
        /// <seealso cref="System.Net.Security.SslStream.ReadTimeout"/>
        /// <seealso cref="System.Net.Security.SslStream.WriteTimeout"/>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// The certificates to use for SSL client authentication
        /// </summary>
        public X509CertificateCollection ClientCertificates { get; set; }

        /// <summary>
        /// The SSL protocols that should be used
        /// </summary>
        public SslProtocols? ClientSslProtocols { get; set; }

        /// <summary>
        /// The SSL protocols that should be used
        /// </summary>
        public bool? CheckClientCertificateRevocation { get; set; } = false;

        /// <summary>
        /// The callback function that will be invoked the client certificate validation results
        /// </summary>
        public RemoteCertificateValidationCallback ClientCertificateValidationCallback { get; set; }
    }
}