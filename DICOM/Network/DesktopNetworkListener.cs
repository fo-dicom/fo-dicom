// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.Net;
    using System.Net.Sockets;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// .NET implementation of the <see cref="INetworkListener"/>.
    /// </summary>
    public class DesktopNetworkListener : INetworkListener
    {
        #region FIELDS

        private readonly TcpListener listener;

        private X509Certificate certificate = null;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DesktopNetworkListener"/>.
        /// </summary>
        /// <param name="port"></param>
        internal DesktopNetworkListener(int port)
        {
            this.listener = new TcpListener(IPAddress.Any, port);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Start listening.
        /// </summary>
        public void Start()
        {
            this.listener.Start();
        }

        /// <summary>
        /// Stop listening.
        /// </summary>
        public void Stop()
        {
            this.listener.Stop();
        }

        /// <summary>
        /// Wait until a network stream is trying to connect, and return the accepted stream.
        /// </summary>
        /// <param name="certificateName">Certificate name of authenticated connections.</param>
        /// <param name="noDelay">No delay?</param>
        /// <returns>Connected network stream.</returns>
        public INetworkStream AcceptNetworkStream(string certificateName, bool noDelay)
        {
            var tcpClient = this.listener.AcceptTcpClient();
            tcpClient.NoDelay = noDelay;

            if (!string.IsNullOrEmpty(certificateName) && this.certificate == null) this.certificate = GetX509Certificate(certificateName);

            return new DesktopNetworkStream(tcpClient, this.certificate);
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
            store.Close();

            if (certs.Count == 0)
            {
                throw new DicomNetworkException("Unable to find certificate for " + certificateName);
            }

            return certs[0];
        }

        #endregion
    }
}
