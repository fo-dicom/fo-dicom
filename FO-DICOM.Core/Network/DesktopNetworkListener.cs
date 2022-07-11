// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// .NET implementation of the <see cref="INetworkListener"/>.
    /// </summary>
    public class DesktopNetworkListener : INetworkListener
    {
        private readonly IDesktopNetworkStreamFactory _desktopNetworkStreamFactory;
        private readonly NetworkListenerCreationOptions _options;

        #region FIELDS

        private TcpListener _listener;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DesktopNetworkListener"/> class. 
        /// </summary>
        /// <param name="desktopNetworkStreamFactory">The factory that can create server desktop network streams</param>
        /// <param name="options">The options that specify how the listener must be initialized</param>
        internal DesktopNetworkListener(
            IDesktopNetworkStreamFactory desktopNetworkStreamFactory,
            NetworkListenerCreationOptions options)
        {
            _desktopNetworkStreamFactory = desktopNetworkStreamFactory;
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public Task StartAsync()
        {
            if (_listener != null)
            {
                throw new DicomNetworkException("Cannot start a network listener that was already started");
            }
            
            if (!IPAddress.TryParse(_options.IpAddress, out IPAddress ipAddress))
            {
                ipAddress = IPAddress.Any;
            }

            _listener = new TcpListener(ipAddress, _options.Port);
            _listener.Start();
            
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Stop()
        {
            if (_listener == null)
            {
                throw new DicomNetworkException("Cannot stop a network listener that was never started");
            }
            
            _listener?.Stop();
        }

        public async Task<INetworkStream> AcceptNetworkStreamAsync(X509Certificate certificate, bool noDelay, CancellationToken token)
        {
            if (_listener == null)
            {
                throw new DicomNetworkException("Cannot accept an incoming network stream because the listener has not been started yet");
            }
            
            try
            {
                using var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(token);
                var acceptTcpClientTask = _listener.AcceptTcpClientAsync();
                var awaiter = await Task.WhenAny(acceptTcpClientTask, Task.Delay(-1, cancelSource.Token)).ConfigureAwait(false);
                cancelSource.Cancel();
                if (awaiter == acceptTcpClientTask)
                {
                    var tcpClient = await acceptTcpClientTask; // No need for ConfigureAwait(false) here because the task has already completed
                    tcpClient.NoDelay = noDelay;

                    //  let DesktopNetworkStream dispose the TCP Client when it is disposed
                    return await _desktopNetworkStreamFactory.CreateAsServerAsync(tcpClient, certificate, true, _options, token);
                }

                Stop();
                await acceptTcpClientTask.ConfigureAwait(false);

                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
