// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Tls;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// .NET implementation of <see cref="INetworkStream"/>.
    /// </summary>
    public sealed class DesktopNetworkStream : INetworkStream
    {
        #region FIELDS

        private bool _disposed = false;

        private readonly TcpClient _tcpClient;

        private readonly Stream _networkStream;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a client instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="options">The various options that specify how the network stream must be created</param>
        internal DesktopNetworkStream(NetworkStreamCreationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            RemoteHost = options.Host;
            RemotePort = options.Port;

            _tcpClient = new TcpClient
            {
                NoDelay = options.NoDelay
            };
            if (options.ReceiveBufferSize.HasValue)
            {
                _tcpClient.ReceiveBufferSize = options.ReceiveBufferSize.Value;
            }
            if (options.SendBufferSize.HasValue)
            {
                _tcpClient.SendBufferSize = options.SendBufferSize.Value;
            }
            _tcpClient.ConnectAsync(options.Host, options.Port).Wait();

            Stream stream = _tcpClient.GetStream();
            if (options.TlsInitiator != null)
            {
                stream = options.TlsInitiator.InitiateTls(stream, RemoteHost, RemotePort);
            }
            if (options.Timeout.TotalMilliseconds > 0)
            {
                stream.ReadTimeout = (int)options.Timeout.TotalMilliseconds;
                stream.WriteTimeout = (int)options.Timeout.TotalMilliseconds;
            }

            LocalHost = ((IPEndPoint)_tcpClient.Client.LocalEndPoint).Address.ToString();
            LocalPort = ((IPEndPoint)_tcpClient.Client.LocalEndPoint).Port;

            _networkStream = stream;
        }

        /// <summary>
        /// Initializes a server instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="tcpClient">TCP client.</param>
        /// <param name="certificate">Certificate for authenticated connection.</param>
        /// <param name="ownsTcpClient">dispose tcpClient on Dispose</param>
        /// <remarks>
        /// Ownership of <paramref name="tcpClient"/> is controlled by <paramref name="ownsTcpClient"/>.
        ///
        /// if <paramref name="ownsTcpClient"/> is false, <paramref name="tcpClient"/> must be disposed by caller.
        /// this is default so that compatible with older versions.
        ///
        /// if <paramref name="ownsTcpClient"/> is true, <paramref name="tcpClient"/> will be disposed altogether on DesktopNetworkStream's disposal.
        /// </remarks>
        internal DesktopNetworkStream(TcpClient tcpClient, ITlsAcceptor tlsAcceptor, bool ownsTcpClient = false)
        {
            LocalHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            LocalPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;
            RemoteHost = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            RemotePort = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;

            Stream stream = tcpClient.GetStream();
            if (tlsAcceptor != null)
            {
                stream = tlsAcceptor.AcceptTls(stream, RemoteHost, LocalPort);
            }

            if (ownsTcpClient)
            {
                _tcpClient = tcpClient;
            }

            _networkStream = stream;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~DesktopNetworkStream()
        {
            Dispose(false);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the remote host of the network stream.
        /// </summary>
        public string RemoteHost { get; }

        /// <summary>
        /// Gets the local host of the network stream.
        /// </summary>
        public string LocalHost { get; }

        /// <summary>
        /// Gets the remote port of the network stream.
        /// </summary>
        public int RemotePort { get; }

        /// <summary>
        /// Gets the local port of the network stream.
        /// </summary>
        public int LocalPort { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Get corresponding <see cref="System.IO.Stream"/> object.
        /// </summary>
        /// <returns>Network stream as <see cref="System.IO.Stream"/> object.</returns>
        public Stream AsStream()
        {
            return _networkStream;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Do the actual disposal.
        /// </summary>
        /// <param name="disposing">True if called from <see cref="Dispose()"/>, false otherwise.</param>
        /// <remarks>The underlying stream is normally passed on to a <see cref="DicomService"/> implementation that
        /// is responsible for disposing the stream when appropriate. Therefore, the stream should not be disposed here.</remarks>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (_tcpClient != null)
            {
                _tcpClient.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
