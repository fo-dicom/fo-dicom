// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Globalization;
    using System.IO;

    using Windows.Networking;
    using Windows.Networking.Sockets;

    /// <summary>
    /// Universal Windows Platform implementation of <see cref="INetworkStream"/>.
    /// </summary>
    public sealed class WindowsNetworkStream : INetworkStream
    {
        #region FIELDS

        private bool disposed = false;

        private readonly StreamSocket socket;

        private readonly Stream networkStream;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a client instance of <see cref="WindowsNetworkStream"/>.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        internal WindowsNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            this.socket = new StreamSocket();
            this.socket.Control.NoDelay = noDelay;
            // TODO Update socket.Control.IgnorableServerCertificateErrors with all possible errors if ignoreSslPolicyErrors is true?

            this.socket.ConnectAsync(
                new HostName(host),
                port.ToString(CultureInfo.InvariantCulture),
                useTls ? SocketProtectionLevel.Tls10 : SocketProtectionLevel.PlainSocket).GetResults();

            if (useTls)
            {
                this.socket.UpgradeToSslAsync(SocketProtectionLevel.Tls10, new HostName(host)).GetResults();
            }

            this.networkStream = this.socket.OutputStream.AsStreamForWrite();
        }

        /// <summary>
        /// Initializes a server instance of <see cref="WindowsNetworkStream"/>.
        /// </summary>
        /// <param name="socket">TCP socket.</param>
        /// <remarks>Ownership of <paramref name="socket"/> remains with the caller, including responsibility for
        /// disposal. Therefore, a handle to <paramref name="socket"/> is <em>not</em> stored when <see cref="WindowsNetworkStream"/>
        /// is initialized with this server-side constructor.</remarks>
        internal WindowsNetworkStream(StreamSocket socket)
        {
            this.networkStream = socket.InputStream.AsStreamForRead();
            this.socket = null;
        }

        /// <summary>
        /// Destrutor.
        /// </summary>
        ~WindowsNetworkStream()
        {
            this.Dispose(false);
        }

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
        /// <param name="disposing">True if called from <see cref="Dispose"/>, false otherwise.</param>
        /// <remarks>The underlying stream is normally passed on to a <see cref="DicomService"/> implementation that
        /// is responsible for disposing the stream when appropriate. Therefore, the stream should not be disposed here.</remarks>
        private void Dispose(bool disposing)
        {
            if (this.disposed) return;

            if (this.socket != null)
            {
                this.socket.Dispose();
            }

            this.disposed = true;
        }

        #endregion
    }
}
