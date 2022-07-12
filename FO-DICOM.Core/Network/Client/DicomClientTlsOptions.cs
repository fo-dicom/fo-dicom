using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace FellowOakDicom.Network.Client
{
    /// <summary>
    /// The options that configure TLS authentication for the DICOM client
    /// </summary>
    public class DicomClientTlsOptions
    {
        /// <summary>
        /// The certificates that will be used to authenticate the client itself
        /// </summary>
        public X509CertificateCollection Certificates { get; set; }

        /// <summary>
        /// The protocols that should be supported
        /// </summary>
        public SslProtocols Protocols { get; set; } = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
        
        /// <summary>
        /// Whether or not the certificate revocation list should be checked during authentication
        /// </summary>
        public bool CheckCertificateRevocation { get; set; }
            
        /// <summary>
        /// The callback that will be invoked after validating the certificate of the server
        /// </summary>
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; set; }

        /// <summary>
        /// The timeout after which TLS authentication will be considered to have failed
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);

    }
}