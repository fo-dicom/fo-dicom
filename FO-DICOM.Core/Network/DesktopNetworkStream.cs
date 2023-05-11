﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// .NET implementation of <see cref="INetworkStream"/>.
    /// </summary>
    public sealed class DesktopNetworkStream : INetworkStream
    {
        #region FIELDS

        private static readonly TimeSpan _sslHandshakeTimeout = TimeSpan.FromMinutes(1);

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
            if (options.UseTls)
            {
                var ssl = new SslStream(
                    stream,
                    false,

                    (sender, certificate, chain, errors) => errors == SslPolicyErrors.None || options.IgnoreSslPolicyErrors);
                ssl.ReadTimeout = (int) options.Timeout.TotalMilliseconds;
                ssl.WriteTimeout = (int) options.Timeout.TotalMilliseconds;

                var authenticationSucceeded = Task.Run(async () => await ssl.AuthenticateAsClientAsync(options.Host).ConfigureAwait(false)).Wait(_sslHandshakeTimeout);

                if (!authenticationSucceeded)
                {
                    throw new DicomNetworkException($"SSL client authentication took longer than {_sslHandshakeTimeout.TotalSeconds}s");
                }

                stream = ssl;
            }

            LocalHost = ((IPEndPoint)_tcpClient.Client.LocalEndPoint).Address.ToString();
            LocalPort = ((IPEndPoint)_tcpClient.Client.LocalEndPoint).Port;

            _networkStream = stream;
        }

        /// <summary>
        /// Initializes a client instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds</param>
        internal DesktopNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors, int millisecondsTimeout)
            : this(new NetworkStreamCreationOptions { Host = host, Port = port, UseTls = useTls, NoDelay = noDelay, IgnoreSslPolicyErrors = ignoreSslPolicyErrors, Timeout = TimeSpan.FromMilliseconds(millisecondsTimeout) })
        {
            
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
        internal DesktopNetworkStream(TcpClient tcpClient, X509Certificate certificate, bool ownsTcpClient = false)
        {
            LocalHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            LocalPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;
            RemoteHost = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            RemotePort = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;

            Stream stream = tcpClient.GetStream();
            if (certificate != null)
            {
                var ssl = new SslStream(stream, false);

                var authenticationSucceeded = Task.Run(
                    async () => await ssl.AuthenticateAsServerAsync(certificate, false, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, false).ConfigureAwait(false)
                    ).Wait(_sslHandshakeTimeout);

                if (!authenticationSucceeded)
                {
                    throw new DicomNetworkException($"SSL server authentication took longer than {_sslHandshakeTimeout.TotalSeconds}s");
                }

                stream = ssl;
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
        /// Get corresponding <see cref="Stream"/> object.
        /// </summary>
        /// <returns>Network stream as <see cref="Stream"/> object.</returns>
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
