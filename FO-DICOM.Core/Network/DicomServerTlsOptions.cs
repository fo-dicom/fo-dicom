using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// The options that configure TLS authentication for the DICOM server
    /// </summary>
    public class DicomServerTlsOptions
    {
        /// <summary>
        /// The certificate to use for authenticated connections
        /// </summary>
        public X509Certificate Certificate { get; set; }

        /// <summary>
        /// The protocols that should be supported
        /// </summary>
        public SslProtocols Protocols { get; set; } = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
        
        /// <summary>
        /// Whether or not to require mutual TLS authentication, i.e. the client must present a valid certificate as well
        /// </summary>
        public bool RequireMutualAuthentication { get; set; } 
            
        /// <summary>
        /// Whether or not the certificate revocation list should be checked during authentication
        /// </summary>
        public bool CheckCertificateRevocation { get; set; }
            
        /// <summary>
        /// The callback that will be invoked after validating the certificate of an incoming client connection
        /// </summary>
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; set; }

        /// <summary>
        /// The timeout after which TLS authentication will be considered to have failed
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);

    }
}