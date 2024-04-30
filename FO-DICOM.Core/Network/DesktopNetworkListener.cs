// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Tls;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
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
            ITlsAcceptor tlsAcceptor,
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
                    var tcpClient = await acceptTcpClientTask.ConfigureAwait(false);
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
                        logger.LogDebug("Client connected to {IPAddress}:{Port}", _endpoint.Address.ToString(), _endpoint.Port);                
                    }

                    // let DesktopNetworkStream dispose the TcpClient
                    return new DesktopNetworkStream(tcpClient, tlsAcceptor, true);
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

        #endregion
    }
}
