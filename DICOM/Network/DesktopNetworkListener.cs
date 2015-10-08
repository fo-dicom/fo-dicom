// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.Net;
    using System.Net.Sockets;
    using System.Security.Cryptography.X509Certificates;

    public class DesktopNetworkListener : INetworkListener
    {
        #region FIELDS

        private readonly TcpListener listener;

        private X509Certificate certificate = null;

        #endregion

        #region CONSTRUCTORS

        internal DesktopNetworkListener(int port)
        {
            this.listener = new TcpListener(IPAddress.Any, port);
        }

        #endregion

        #region METHODS

        public void Start()
        {
            this.listener.Start();
        }

        public void Stop()
        {
            this.listener.Stop();
        }

        public INetworkStream AcceptNetworkStream(int port, string certificateName, bool noDelay)
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
