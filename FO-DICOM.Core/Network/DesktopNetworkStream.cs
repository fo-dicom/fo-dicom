// Copyright (c) 2012-2019 fo-dicom contributors.
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

        private static readonly TimeSpan SslHandshakeTimeout = TimeSpan.FromMinutes(1);

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
        internal DesktopNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors, int millisecondsTimeout)
        {
            this.RemoteHost = host;
            this.RemotePort = port;

            this.tcpClient = new TcpClient { NoDelay = noDelay };
            this.tcpClient.ConnectAsync(host, port).Wait();

            Stream stream = this.tcpClient.GetStream();
            if (useTls)
            {
                var ssl = new SslStream(
                    stream,
                    false,
                    (sender, certificate, chain, errors) => errors == SslPolicyErrors.None || ignoreSslPolicyErrors);
                ssl.ReadTimeout = millisecondsTimeout;
                ssl.WriteTimeout = millisecondsTimeout;

                var authenticationSucceeded = Task.Run(async () => await ssl.AuthenticateAsClientAsync(host).ConfigureAwait(false)).Wait(SslHandshakeTimeout);
                if (!authenticationSucceeded)
                {
                    throw new DicomNetworkException($"SSL client authentication took longer than {SslHandshakeTimeout.TotalSeconds}s");
                }

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
            this.LocalHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            this.LocalPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;
            this.RemoteHost = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            this.RemotePort = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;

            Stream stream = tcpClient.GetStream();
            if (certificate != null)
            {
                var ssl = new SslStream(stream, false);

                var authenticationSucceeded = Task.Run(
                    async () => await ssl.AuthenticateAsServerAsync(certificate, false, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, false).ConfigureAwait(false)
                    ).Wait(SslHandshakeTimeout);

                if (!authenticationSucceeded)
                {
                    throw new DicomNetworkException($"SSL server authentication took longer than {SslHandshakeTimeout.TotalSeconds}s");
                }

                stream = ssl;
            }

            if (ownsTcpClient)
            {
                this.tcpClient = tcpClient;
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
                this.tcpClient.Dispose();
            }

            this.disposed = true;
        }

        #endregion
    }
}
