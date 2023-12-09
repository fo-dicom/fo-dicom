// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Tls;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Interface for listening to network stream connections.
    /// </summary>
    public interface INetworkListener
    {
        /// <summary>
        /// Start listening.
        /// </summary>
        /// <returns>An awaitable <see cref="System.Threading.Tasks.Task"/>.</returns>
        Task StartAsync();
        /// <summary>
        /// Stop listening.
        /// </summary>
        void Stop();

        /// <summary>
        /// Wait until a network stream is trying to connect, and return the accepted stream.
        /// </summary>
        /// <param name="tlsAcceptor">Handler to accept authenticated connections.</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="receiveBufferSize">The size of the receive buffer of the underlying TCP connection</param>
        /// <param name="sendBufferSize">The size of the send buffer of the underlying TCP connection</param>
        /// <param name="logger">The logger</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Connected network stream.</returns>
        Task<INetworkStream> AcceptNetworkStreamAsync(ITlsAcceptor tlsAcceptor, bool noDelay, int? receiveBufferSize, int? sendBufferSize, ILogger logger, CancellationToken token);
    }
}
