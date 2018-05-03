// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// .NET implementation of <see cref="INetworkStream"/>.
    /// </summary>
    public sealed class DesktopNetworkStream : INetworkStream
    {
        #region FIELDS

        private bool disposed = false;

        private readonly TcpClient tcpClient;

        private readonly Stream networkStream;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a client instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        internal DesktopNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            this.RemoteHost = host;
            this.RemotePort = port;

#if NETSTANDARD
            this.tcpClient = new TcpClient { NoDelay = noDelay };
            this.tcpClient.ConnectAsync(host, port).Wait();
#else
            this.tcpClient = new TcpClient(host, port) { NoDelay = noDelay };
#endif

            Stream stream = this.tcpClient.GetStream();
            if (useTls)
            {
                var ssl = new SslStream(
                    stream,
                    false,
                    (sender, certificate, chain, errors) => errors == SslPolicyErrors.None || ignoreSslPolicyErrors);
#if NETSTANDARD
                ssl.AuthenticateAsClientAsync(host).Wait();
#else
                ssl.AuthenticateAsClient(host);
#endif
                stream = ssl;
            }

            this.LocalHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            this.LocalPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;

            this.networkStream = stream;
        }

        /// <summary>
        /// Initializes a server instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="tcpClient">TCP client.</param>
        /// <param name="certificate">Certificate for authenticated connection.</param>
        /// <remarks>Ownership of <paramref name="tcpClient"/> remains with the caller, including responsibility for
        /// disposal. Therefore, a handle to <paramref name="tcpClient"/> is <em>not</em> stored when <see cref="DesktopNetworkStream"/>
        /// is initialized with this server-side constructor.</remarks>
        internal DesktopNetworkStream(TcpClient tcpClient, X509Certificate certificate)
        {
            this.LocalHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            this.LocalPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;
            this.RemoteHost = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            this.RemotePort = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;

            Stream stream = tcpClient.GetStream();
            if (certificate != null)
            {
                var ssl = new SslStream(stream, false);
#if NETSTANDARD
                ssl.AuthenticateAsServerAsync(certificate, false, SslProtocols.Tls, false).Wait();
#else
                ssl.AuthenticateAsServer(certificate, false, SslProtocols.Tls, false);
#endif
                stream = ssl;
            }

            this.networkStream = stream;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~DesktopNetworkStream()
        {
            this.Dispose(false);
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
            return this.networkStream;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
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
            if (this.disposed) return;

            if (this.tcpClient != null)
            {
#if NETSTANDARD
                this.tcpClient.Dispose();
#else
                this.tcpClient.Close();
#endif
            }

            this.disposed = true;
        }

        #endregion
    }
}
