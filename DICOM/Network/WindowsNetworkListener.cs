// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Windows.Networking;

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

        private readonly HostName _ipAddress;

        private readonly string _port;

        private TaskCompletionSource<INetworkStream> _streamTcs;
        private CancellationTokenRegistration _tokenRegistration;

        private StreamSocketListener _listener;

        private StreamSocket _socket;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsNetworkListener"/> class. 
        /// </summary>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">TCP/IP port to listen to.</param>
        internal WindowsNetworkListener(string ipAddress, int port)
        {
            HostName addr;
            try
            {
                addr = new HostName(ipAddress);
            }
            catch
            {
                addr = new HostName(NetworkManager.IPv4Any);
            }
            _ipAddress = addr.CanonicalName == NetworkManager.IPv4Any ? null : addr;

            _port = port.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public async Task StartAsync()
        {
            _listener = new StreamSocketListener();
            _listener.ConnectionReceived += OnConnectionReceived;

            await _listener.BindEndpointAsync(_ipAddress, _port).AsTask().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void Stop()
        {
            _streamTcs?.TrySetCanceled();
            _streamTcs = null;
            _tokenRegistration.Dispose();
            _listener.ConnectionReceived -= OnConnectionReceived;
            _listener.Dispose();
            _socket?.Dispose();
            _socket = null;
        }

        /// <inheritdoc />
        public async Task<INetworkStream> AcceptNetworkStreamAsync(
            string certificateName,
            bool noDelay,
            CancellationToken token)
        {
            if (!string.IsNullOrWhiteSpace(certificateName))
            {
                throw new NotSupportedException(
                    "Authenticated server connections not supported on Windows Universal Platform.");
            }

            await (_streamTcs?.Task ?? Task.FromResult(default(INetworkStream))).ConfigureAwait(false);
            _tokenRegistration.Dispose();

            _streamTcs = new TaskCompletionSource<INetworkStream>();
            _tokenRegistration = token.Register(() => _streamTcs?.TrySetCanceled(token), false);

            return await _streamTcs.Task;
        }

        /// <summary>
        /// Event handler when connection received.
        /// </summary>
        /// <param name="sender">The sender, more specifically the listener object.</param>
        /// <param name="args">The connection received arguments; 
        /// <see cref="StreamSocketListenerConnectionReceivedEventArgs.Socket">Socket</see>/> property is saved for later use.</param>
        private void OnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            _socket?.Dispose();
            _socket = args.Socket;
            _tokenRegistration.Dispose();
            _streamTcs?.TrySetResult(new WindowsNetworkStream(_socket));
        }

        #endregion
    }
}
