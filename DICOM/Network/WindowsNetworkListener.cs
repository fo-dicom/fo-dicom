// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Globalization;
    using System.Threading;

    using Windows.Networking.Sockets;
    using Windows.Security.Cryptography.Certificates;

    /// <summary>
    /// Universal Windows Platform implementation of the <see cref="INetworkListener"/>.
    /// </summary>
    public class WindowsNetworkListener : INetworkListener
    {
        #region FIELDS

        private readonly string port;

        private readonly ManualResetEventSlim handle;

        private StreamSocketListener listener;

        private StreamSocket socket;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="WindowsNetworkListener"/>.
        /// </summary>
        /// <param name="port"></param>
        internal WindowsNetworkListener(int port)
        {
            this.port = port.ToString(CultureInfo.InvariantCulture);
            this.handle = new ManualResetEventSlim(false);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Start listening.
        /// </summary>
        public async void Start()
        {
            this.listener = new StreamSocketListener();
            this.listener.ConnectionReceived += this.OnConnectionReceived;

            this.socket = null;
            this.handle.Reset();
            await this.listener.BindServiceNameAsync(this.port).AsTask().ConfigureAwait(false);
        }

        /// <summary>
        /// Stop listening.
        /// </summary>
        public void Stop()
        {
            this.listener.ConnectionReceived -= this.OnConnectionReceived;
            this.listener.Dispose();
            this.handle.Set();
        }

        /// <summary>
        /// Wait until a network stream is trying to connect, and return the accepted stream.
        /// </summary>
        /// <param name="certificateName">Certificate name of authenticated connections.</param>
        /// <param name="noDelay">No delay? Not applicable here, since no delay flag needs to be set before connection is established.</param>
        /// <returns>Connected network stream.</returns>
        public INetworkStream AcceptNetworkStream(string certificateName, bool noDelay)
        {
            this.handle.Wait();
            if (this.socket == null) return null;

            if (!string.IsNullOrEmpty(certificateName) && this.socket.Control.ClientCertificate == null)
            {
                //this.socket.Control.ClientCertificate = GetCertificate(certificateName);
            }

            var networkStream = new WindowsNetworkStream(this.socket);
            this.handle.Reset();

            return networkStream;
        }

        private void OnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            this.socket = args.Socket;
            this.handle.Set();
        }

        /// <summary>
        /// Get X509 certificate from the certificate store.
        /// </summary>
        /// <param name="certificateName">Certificate name.</param>
        /// <returns>Certificate with the specified name.</returns>
        private static Certificate GetCertificate(string certificateName)
        {
            var certs =
                CertificateStores.FindAllAsync(
                    new CertificateQuery { FriendlyName = certificateName, StoreName = "MY" }).GetResults();

            if (certs.Count == 0)
            {
                throw new DicomNetworkException("Unable to find certificate for " + certificateName);
            }

            return certs[0];
        }

        #endregion
    }
}
