// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Networking.Sockets;

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
        /// Initializes a new instance of the <see cref="WindowsNetworkListener"/> class. 
        /// </summary>
        /// <param name="port">TCP/IP port to listen to.</param>
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
        /// <returns>An await:able <see cref="Task"/>.</returns>
        public async Task StartAsync()
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
        /// <param name="token">Cancellation token.</param>
        /// <returns>Connected network stream.</returns>
        public Task<INetworkStream> AcceptNetworkStreamAsync(
            string certificateName,
            bool noDelay,
            CancellationToken token)
        {
            if (!string.IsNullOrWhiteSpace(certificateName))
            {
                throw new NotSupportedException(
                    "Authenticated server connections not supported on Windows Universal Platform.");
            }

            INetworkStream networkStream;
            try
            {
                this.handle.Wait(token);
                networkStream = this.socket == null ? null : new WindowsNetworkStream(this.socket);
            }
            catch (OperationCanceledException)
            {
                networkStream = null;
            }

            this.handle.Reset();

            return Task.FromResult(networkStream);
        }

        /// <summary>
        /// Event handler when connection received.
        /// </summary>
        /// <param name="sender">The sender, more specifically the listener object.</param>
        /// <param name="args">The connection received arguments; 
        /// <see cref="StreamSocketListenerConnectionReceivedEventArgs.Socket">Socket</see>/> property is saved for later use.</param>
        private void OnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            this.socket = args.Socket;
            this.handle.Set();
        }

        #endregion
    }
}
