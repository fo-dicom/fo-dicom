// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Tls
{
    /// <summary>
    /// Default implementation of ITlsInitiator.
    /// This implementation will use windows certificate store in order to validate the server certificate
    /// </summary>
    public class DefaultTlsInitiator : ITlsInitiator
    {

        /// <summary>
        /// Whether or not to ignore any certificate validation errors that occur when authenticating as a client over SSL
        /// </summary>
        public bool IgnoreSslPolicyErrors { get; set; }

        /// <summary>
        /// The timeout after which TLS authentication will be considered to have failed
        /// </summary>
        public TimeSpan SslHandshakeTimeout { get; set; } = TimeSpan.FromMinutes(1);

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

        public DefaultTlsInitiator() { }


        public Stream InitiateTls(Stream plainStream, string remoteAddress, int remotePort)
        {
            var certificates = Certificates ?? new X509CertificateCollection();
            var userCertificateValidationCallback = CertificateValidationCallback
                                 ?? ((sender, certificate, chain, errors) => errors == SslPolicyErrors.None || IgnoreSslPolicyErrors);

            var ssl = new SslStream(plainStream, false, userCertificateValidationCallback);

            var authenticationSucceeded = Task.Run(() => ssl.AuthenticateAsClientAsync(remoteAddress, certificates, Protocols, CheckCertificateRevocation)).Wait(SslHandshakeTimeout);

            if (!authenticationSucceeded)
            {
                throw new DicomNetworkException($"Client TLS authentication failed because it took longer than {SslHandshakeTimeout.TotalSeconds}s");
            }

            return ssl;
        }

    }
}
