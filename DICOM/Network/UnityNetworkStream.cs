﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using UnityEngine.Networking;

namespace Dicom.Network
{
    /// <summary>
    /// Unity implementation of <see cref="INetworkStream"/>.
    /// </summary>
    public sealed class UnityNetworkStream : INetworkStream
    {
        #region FIELDS

        private bool _disposed = false;

        private readonly NetworkClient _networkClient;

        private readonly Stream _networkStream;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a client instance of <see cref="UnityNetworkStream"/>.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        internal UnityNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            RemoteHost = host;
            RemotePort = port;
            _networkClient = new NetworkClient();
            _networkClient.Connect(host, port);

            Stream stream = _networkClient.GetStream();
            if (useTls)
            {
                var ssl = new SslStream(
                    stream,
                    false,
                    (sender, certificate, chain, errors) => errors == SslPolicyErrors.None || ignoreSslPolicyErrors);

                ssl.AuthenticateAsClient(host);
                stream = ssl;
            }

            LocalHost = _networkClient.connection.address;
            LocalPort = _networkClient.hostPort;

            _networkStream = stream;
        }

        /// <summary>
        /// Initializes a server instance of <see cref="UnityNetworkStream"/>.
        /// </summary>
        /// <param name="connection">TCP client.</param>
        /// <param name="certificate">Certificate for authenticated connection.</param>
        /// <remarks>Ownership of <paramref name="connection"/> remains with the caller, including responsibility for
        /// disposal. Therefore, a handle to <paramref name="connection"/> is <em>not</em> stored when <see cref="UnityNetworkStream"/>
        /// is initialized with this server-side constructor.</remarks>
        internal UnityNetworkStream(NetworkConnection connection, X509Certificate certificate)
        {
            LocalHost = ((IPEndPoint)connection.Client.LocalEndPoint).Address.ToString();
            LocalPort = ((IPEndPoint)connection.Client.LocalEndPoint).Port;
            RemoteHost = ((IPEndPoint)connection.Client.RemoteEndPoint).Address.ToString();
            RemotePort = ((IPEndPoint)connection.Client.RemoteEndPoint).Port;

            Stream stream = connection.GetStream();
            if (certificate != null)
            {
                var ssl = new SslStream(stream, false);
                ssl.AuthenticateAsServer(certificate, false, SslProtocols.Tls, false);
                stream = ssl;
            }

            _networkStream = stream;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~UnityNetworkStream()
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
            if (_disposed) return;

            if (_networkClient != null && _networkClient.isConnected)
            {
                _networkClient.Disconnect();
            }

            _disposed = true;
        }

        #endregion
    }
}
