// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Microsoft.Extensions.Logging;
using System;
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

        private readonly IPEndPoint _endpoint;
        
        private X509Certificate _certificate;

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

            _endpoint = new IPEndPoint(addr, port);
            _listener = new TcpListener(_endpoint);
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
            ILogger logger,
            CancellationToken token)
        {
            try
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("Waiting for inbound client connection to {IPAddress}:{Port}",
                        _endpoint.Address.ToString(), _endpoint.Port);                
                }

                using var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(token);
                var acceptTcpClientTask = _listener.AcceptTcpClientAsync();
                var awaiter = await Task.WhenAny(acceptTcpClientTask, Task.Delay(-1, cancelSource.Token)).ConfigureAwait(false);
                cancelSource.Cancel();
                if (awaiter == acceptTcpClientTask)
                {
                    var tcpClient = await acceptTcpClientTask;
                    tcpClient.NoDelay = noDelay;
                    if (receiveBufferSize.HasValue)
                    {
                        tcpClient.ReceiveBufferSize = receiveBufferSize.Value;
                    }
                    if (sendBufferSize.HasValue)
                    {
                        tcpClient.SendBufferSize = sendBufferSize.Value;
                    }
                    
                    if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("Client connected to {IPAddress}:{Port}",
                            _endpoint.Address.ToString(), _endpoint.Port);                
                    }

                    if (!string.IsNullOrEmpty(certificateName) && _certificate == null)
                    {
                        _certificate = GetX509Certificate(certificateName);
                    }

                    // let DesktopNetworkStream dispose the TcpClient
                    return new DesktopNetworkStream(tcpClient, _certificate, true);
                }

                Stop();
                await acceptTcpClientTask.ConfigureAwait(false);

                return null;
            }
            catch (OperationCanceledException)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("Listener for {IPAddress}:{Port} has stopped because it was cancelled",
                        _endpoint.Address.ToString(), _endpoint.Port);
                }

                return null;
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.OperationAborted)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("Listener for {IPAddress}:{Port} has stopped because the connection was closed",
                        _endpoint.Address.ToString(), _endpoint.Port);
                }
                return null;
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "An error occurred while listening for inbound client connections to {IPAddress}:{Port}", 
                    _endpoint.Address.ToString(), _endpoint.Port);
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
