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
    /// Default implementation of ITlsAcceptor
    /// This class requires an X509Certificate that can be passed as name of an certificate that is stored in windows certificate storage or as a certificate file
    /// </summary>
    public class DefaultTlsAcceptor : ITlsAcceptor
    {

        /// <summary>
        /// The certificate to use for authenticated connections
        /// </summary>
        public X509Certificate Certificate { get; set; }

        /// <summary>
        /// The timeout after which TLS authentication will be considered to have failed
        /// </summary>
        public TimeSpan SslHandshakeTimeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// The protocols that should be supported
        /// </summary>
        public SslProtocols Protocols { get; set; } = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

        /// <summary>
        /// Whether or not to require mutual TLS authentication, i.e. the client must present a valid certificate as well
        /// </summary>
        public bool RequireMutualAuthentication { get; set; } = false;

        /// <summary>
        /// Whether or not the certificate revocation list should be checked during authentication
        /// </summary>
        public bool CheckCertificateRevocation { get; set; } = false;

        /// <summary>
        /// The callback that will be invoked after validating the certificate of an incoming client connection
        /// </summary>
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; set; } = null;


        public DefaultTlsAcceptor(string certificateName)
        {
            Certificate = GetX509Certificate(certificateName);
        }

        public DefaultTlsAcceptor(string certificateFilename, string password)
        {
            Certificate = new X509Certificate2(certificateFilename, password);
        }

        public DefaultTlsAcceptor(X509Certificate certificate)
        {
            Certificate = certificate;
        }


        public Stream AcceptTls(Stream encryptedStream, string remoteAddress, int localPort)
        {
            var userCertificateValidationCallback = CertificateValidationCallback
                                    ?? new RemoteCertificateValidationCallback((sender, _, chain, errors) =>
                                    {
                                        if (!RequireMutualAuthentication)
                                        {
                                            errors &= ~SslPolicyErrors.RemoteCertificateNotAvailable;
                                        }
                                        return errors == SslPolicyErrors.None;
                                    });

            var ssl = new SslStream(encryptedStream, false, userCertificateValidationCallback);

            var authenticationSucceeded = Task.Run(
                async () => await ssl.AuthenticateAsServerAsync(Certificate, RequireMutualAuthentication, Protocols, CheckCertificateRevocation).ConfigureAwait(false)
                ).Wait(SslHandshakeTimeout);

            if (!authenticationSucceeded)
            {
                throw new DicomNetworkException($"SSL server authentication took longer than {SslHandshakeTimeout.TotalSeconds}s");
            }

            if (RequireMutualAuthentication && !ssl.IsMutuallyAuthenticated)
            {
                throw new DicomNetworkException("Client TLS mutual authentication failed");
            }

            return ssl;
        }

        /// <summary>
        /// Get X509 certificate from the certificate store.
        /// </summary>
        /// <param name="certificateName">Certificate name.</param>
        /// <returns>Certificate with the specified name.</returns>
        private static X509Certificate GetX509Certificate(string certificateName)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindBySubjectName, certificateName, false);
            store.Dispose();

            if (certs.Count == 0)
            {
                throw new DicomNetworkException("Unable to find certificate for " + certificateName);
            }

            return certs[0];
        }

    }
}
