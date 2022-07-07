// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
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

        private bool _disposed;

        private readonly TcpClient _tcpClient;

        private readonly Stream _networkStream;

        #endregion

        #region CONSTRUCTORS

        public DesktopNetworkStream(string localHost, int localPort, string remoteHost, int remotePort,
            Stream networkStream, TcpClient tcpClient)
        {
            _networkStream = networkStream ?? throw new ArgumentNullException(nameof(networkStream));
            _tcpClient = tcpClient;
            RemoteHost = remoteHost;
            LocalHost = localHost;
            RemotePort = remotePort;
            LocalPort = localPort;
        }

        /// <summary>
        /// Initializes a server instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="tcpClient">TCP client.</param>
        /// <param name="certificate">Certificate for authenticated connection.</param>
        /// <param name="ownsTcpClient">dispose tcpClient on Dispose</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Ownership of <paramref name="tcpClient"/> is controlled by <paramref name="ownsTcpClient"/>.
        /// 
        /// if <paramref name="ownsTcpClient"/> is false, <paramref name="tcpClient"/> must be disposed by caller.
        /// this is default so that compatible with older versions.
        /// 
        /// if <paramref name="ownsTcpClient"/> is true, <paramref name="tcpClient"/> will be disposed altogether on DesktopNetworkStream's disposal.
        /// </remarks>
        internal static async Task<DesktopNetworkStream> CreateAsServerAsync(TcpClient tcpClient, X509Certificate certificate, bool ownsTcpClient, CancellationToken cancellationToken)
        {
            var localHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            var localPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;
            var remoteHost = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            var remotePort = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;

            Stream stream = tcpClient.GetStream();
            if (certificate != null)
            {
                var ssl = new SslStream(stream, false);

                var sslHandshake = Task.Run(() => ssl.AuthenticateAsServerAsync(certificate, true, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, false), cancellationToken);
                var sslHandshakeTimeout = Task.Delay(_sslHandshakeTimeout, cancellationToken);

                if (await Task.WhenAny(sslHandshake, sslHandshakeTimeout).ConfigureAwait(false) == sslHandshakeTimeout)
                {
                    throw new DicomNetworkException($"SSL server authentication took longer than {_sslHandshakeTimeout.TotalSeconds}s");
                }

                await sslHandshake.ConfigureAwait(false);

                stream = ssl;
            }

            return new DesktopNetworkStream(localHost, localPort, remoteHost, remotePort, stream, ownsTcpClient ? tcpClient : null);
        }

        /// <summary>
        /// Initializes a client instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="options">The options that specify how to open a connection</param>
        /// <param name="cancellationToken">The cancellation token that cancels the connection</param>
        internal static async Task<DesktopNetworkStream> CreateAsClientAsync(NetworkStreamCreationOptions options, CancellationToken cancellationToken)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var remoteHost = options.Host;
            var remotePort = options.Port;

            var tcpClient = new TcpClient { NoDelay = options.NoDelay };

            await tcpClient.ConnectAsync(options.Host, options.Port).ConfigureAwait(false);

            Stream stream = tcpClient.GetStream();
            if (options.UseTls)
            {
                var userCertificateValidationCallback = options.ClientCertificateValidationCallback
                    ?? ((sender, certificate, chain, errors) => errors == SslPolicyErrors.None || options.IgnoreSslPolicyErrors);
                var ssl = new SslStream(
                    stream,
                    false,
                    userCertificateValidationCallback);
                ssl.ReadTimeout = (int) options.Timeout.TotalMilliseconds;
                ssl.WriteTimeout = (int) options.Timeout.TotalMilliseconds;

                var sslProtocols = options.ClientSslProtocols ?? (SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12);
                var checkClientCertificateRevocation = options.CheckClientCertificateRevocation ?? false;
                var sslHandshake = options.ClientCertificates != null && options.ClientCertificates.Count > 0
                    ? Task.Run(() => ssl.AuthenticateAsClientAsync(options.Host, options.ClientCertificates, sslProtocols, checkClientCertificateRevocation), cancellationToken)
                    : Task.Run(() => ssl.AuthenticateAsClientAsync(options.Host), cancellationToken);
                
                var sslHandshakeTimeout = Task.Delay(_sslHandshakeTimeout, cancellationToken);
                
                if (await Task.WhenAny(sslHandshake, sslHandshakeTimeout).ConfigureAwait(false) == sslHandshakeTimeout)
                {
                    throw new DicomNetworkException($"SSL client authentication took longer than {_sslHandshakeTimeout.TotalSeconds}s");
                }

                await sslHandshake.ConfigureAwait(false);

                stream = ssl;
            }

            var localHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            var localPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;
            
            return new DesktopNetworkStream(localHost, localPort, remoteHost, remotePort, stream, tcpClient);
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

            _tcpClient?.Dispose();
            _disposed = true;
        }

        #endregion
    }
}
