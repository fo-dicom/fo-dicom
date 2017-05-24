﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Dicom.Network
{
    /// <summary>
    /// Unity implementation of the <see cref="INetworkListener"/>.
    /// </summary>
    public class UnityNetworkListener : INetworkListener
    {
        #region FIELDS

        private readonly TcpListener _listener;

        private X509Certificate _certificate = null;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityNetworkListener"/> class. 
        /// </summary>
        /// <param name="port">
        /// TCP/IP port to listen to.
        /// </param>
        internal UnityNetworkListener(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Start listening.
        /// </summary>
        public void Start()
        {
            _listener.Start();
        }

        /// <summary>
        /// Stop listening.
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
        }

        /// <summary>
        /// Wait until a network stream is trying to connect, and return the accepted stream.
        /// </summary>
        /// <param name="certificateName">Certificate name of authenticated connections.</param>
        /// <param name="noDelay">No delay?</param>
        /// <returns>Connected network stream.</returns>
        public INetworkStream AcceptNetworkStream(string certificateName, bool noDelay)
        {
            try
            {
                var tcpClient = _listener.AcceptTcpClient();
                tcpClient.NoDelay = noDelay;

                if (!string.IsNullOrEmpty(certificateName) && _certificate == null)
                {
                    _certificate = GetX509Certificate(certificateName);
                }

                return new UnityNetworkStream(tcpClient, _certificate);
            }
            catch
            {
                return null;
            }
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
