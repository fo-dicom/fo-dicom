// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

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

        public int Port
        {
            get
            {
                return _listener.LocalEndpoint is IPEndPoint localEndPoint ? localEndPoint.Port : -1;
            }
        }
        /// <inheritdoc />
        public void Stop() => _listener.Stop();

        /// <inheritdoc />
        public async Task<TcpClient> AcceptTcpClientAsync(
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
                    logger.DebugWaitForInboundConnection(_endpoint.Address, _endpoint.Port);
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
                        logger.DebugClientConnected(_endpoint.Address, _endpoint.Port);
                    }

                    return tcpClient;
                }

                Stop();
                await acceptTcpClientTask.ConfigureAwait(false);

                return null;
            }
            catch (OperationCanceledException)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.DebugConnectionCancelled(_endpoint.Address, _endpoint.Port);
                }

                return null;
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.OperationAborted)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.DebugConnectionClosed(_endpoint.Address, _endpoint.Port);
                }
                return null;
            }
            catch(Exception exception)
            {
                logger.ErrorWhileListeningForConnection(exception, _endpoint.Address, _endpoint.Port);
                return null;
            }
        }

        #endregion
    }
}
