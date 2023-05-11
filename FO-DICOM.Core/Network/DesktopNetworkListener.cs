﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// .NET implementation of the <see cref="INetworkListener"/>.
    /// </summary>
    public class DesktopNetworkListener : INetworkListener
    {
        #region FIELDS

        private readonly TcpListener _listener;

        private X509Certificate _certificate = null;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DesktopNetworkListener"/> class. 
        /// </summary>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">
        /// TCP/IP port to listen to.
        /// </param>
        internal DesktopNetworkListener(string ipAddress, int port)
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress addr))
            {
                addr = IPAddress.Any;
            }

            _listener = new TcpListener(addr, port);
        }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public Task StartAsync()
        {
            _listener.Start();
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public void Stop() => _listener.Stop();

        /// <inheritdoc />
        public async Task<INetworkStream> AcceptNetworkStreamAsync(
            string certificateName,
            bool noDelay,
            int? receiveBufferSize,
            int? sendBufferSize,
            CancellationToken token)
        {
            try
            {
                Task awaiter;
                Task<TcpClient> acceptTcpClientTask;
                using var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(token);
                acceptTcpClientTask = _listener.AcceptTcpClientAsync();
                awaiter = await Task.WhenAny(acceptTcpClientTask, Task.Delay(-1, cancelSource.Token)).ConfigureAwait(false);
                cancelSource.Cancel();
                if (awaiter is Task<TcpClient> tcpClientTask)
                {
                    var tcpClient = tcpClientTask.Result;
                    tcpClient.NoDelay = noDelay;
                    if (receiveBufferSize.HasValue)
                    {
                        tcpClient.ReceiveBufferSize = receiveBufferSize.Value;
                    }
                    if (sendBufferSize.HasValue)
                    {
                        tcpClient.SendBufferSize = sendBufferSize.Value;
                    }
                    if (!string.IsNullOrEmpty(certificateName) && _certificate == null)
                    {
                        _certificate = GetX509Certificate(certificateName);
                    }

                    //  let DesktopNetworkStream to dispose tcpClient
                    return new DesktopNetworkStream(tcpClient, _certificate, true);
                }

                Stop();
                await acceptTcpClientTask.ConfigureAwait(false);

                return null;
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
            store.Dispose();

            if (certs.Count == 0)
            {
                throw new DicomNetworkException("Unable to find certificate for " + certificateName);
            }

            return certs[0];
        }

        #endregion
    }
}
