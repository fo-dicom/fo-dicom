// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Connection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Association
{
    /// <summary>
    /// Represents an open DICOM association.
    /// Disposing it is equivalent to calling <see cref="ReleaseAsync"/>
    /// </summary>
    public interface IAdvancedDicomClientAssociation: IDisposable
    {
        /// <summary>
        /// Contains information about the DICOM association that was opened
        /// </summary>
        DicomAssociation Association { get; }
        
        /// <summary>
        /// Whether or not this association is already disposed.
        /// </summary>
        bool IsDisposed { get; }
        
        /// <summary>
        /// Sends a request over this association and returns the received responses from the other AE
        /// There is no guarantee that the message will be sent immediately.
        /// As part of the association handshake, a maximum number of asynchronously invoked DICOM requests is agreed upon.
        /// If the maximum number of pending DICOM requests has already been reached, this request will be enqueued and sent later, once older requests have been processed.
        /// </summary>
        /// <param name="dicomRequest">The request to send</param>
        /// <param name="cancellationToken">The token that immediately cancels the request</param>
        /// <returns>
        /// An asynchronous enumerable of responses.
        /// C-FIND or C-MOVE requests will typically receive back a pending response per SOP instance, and finally one last success response
        /// C-STORE and C-ECHO requests on the other hand typically only receives one response
        /// </returns>
        /// <exception cref="OperationCanceledException">When the request is cancelled using the <paramref name="cancellationToken"/></exception>
        /// <exception cref="System.IO.IOException">When connection/socket issues occur</exception>
        /// <exception cref="DicomNetworkException">When DICOM protocol issues occur</exception>
        /// <exception cref="ObjectDisposedException">When the association is already disposed</exception>
        IAsyncEnumerable<DicomResponse> SendRequestAsync(DicomRequest dicomRequest, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a request to the other AE to release this association.
        /// Note that most PACS software close the open TCP connection immediately after an association release.
        /// Releasing an association is the proper way of cleaning it up after it is no longer necessary.
        /// </summary>
        /// <param name="cancellationToken">The token that immediately cancels the request. Note that the association will be left in an unusable state regardless.</param>
        /// <returns>A task that will complete when the association release has been acknowledged, the association is aborted or the connection is closed by the other AE.</returns>
        /// <exception cref="ObjectDisposedException">When the association is already disposed</exception>
        ValueTask ReleaseAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Sends a notification to the other AE that this association will be aborted
        /// Unlike an association release, an abort is not acknowledged by the other AE and can be performed at any time.
        /// Aborting an association may incur a loss of information, if for example it is performed while sending C-STORE requests.
        /// </summary>
        /// <param name="cancellationToken">The token that immediately cancels the abort notification. Note that the association will be left in an unusable state regardless.</param>
        /// <returns>A task that will complete when the association is aborted or the connection is closed by the other AE</returns>
        /// <exception cref="ObjectDisposedException">When the association is already disposed</exception>
        ValueTask AbortAsync(CancellationToken cancellationToken);
    }

    /// <inheritdoc cref="IAdvancedDicomClientAssociation"/>>
    public class AdvancedDicomClientAssociation : IAdvancedDicomClientAssociation
    {
        private const string _responseChannelIsGoneNote = "(Note: the response channel is gone. This can happen when the request is cancelled after it has been sent)";
        private const string _responseChannelDoesNotHaveUnlimitedCapacity = "Failed to write to the response channel. This should never happen, because response channels should be created with unlimited capacity";
        private const string _associationChannelDoesNotHaveUnlimitedCapacity = "Failed to write to the association channel. This should never happen, because the association channel should be created with unlimited capacity";
        
        private readonly ILogger _logger;
        private readonly Task _eventCollector;
        private readonly CancellationTokenSource _eventCollectorCts;
        private readonly ConcurrentDictionary<int, Channel<IAdvancedDicomClientEvent>> _requestChannels;
        private readonly Channel<IAdvancedDicomClientEvent> _associationChannel;
        private readonly IAdvancedDicomClientConnection _connection;
        
        private long _isDisposed;
        private ConnectionClosedEvent _connectionClosedEvent;
        
        public bool IsDisposed => Interlocked.Read(ref _isDisposed) > 0;
        
        /// <inheritdoc cref="IAdvancedDicomClientAssociation.Association"/>
        public DicomAssociation Association { get; }

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public AdvancedDicomClientAssociation(IAdvancedDicomClientConnection connection, DicomAssociation association, ILogger logger)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventCollectorCts = new CancellationTokenSource();
            _eventCollector = Task.Run(() => CollectEventsAsync(_eventCollectorCts.Token));
            _requestChannels = new ConcurrentDictionary<int, Channel<IAdvancedDicomClientEvent>>();
            _associationChannel = Channel.CreateUnbounded<IAdvancedDicomClientEvent>(new UnboundedChannelOptions
            {
                SingleReader = false, 
                SingleWriter = false, 
                AllowSynchronousContinuations = false
            });

            Association = association ?? throw new ArgumentNullException(nameof(association));
        }
        
        /// <summary>
        /// The finalizer will be called when this instance is not disposed properly.
        /// </summary>
        /// <remarks>Failing to dispose indicates wrong usage</remarks>
        ~AdvancedDicomClientAssociation() => Dispose(false);

        private async Task CollectEventsAsync(CancellationToken cancellationToken)
        {
            await foreach (var @event in _connection.Callbacks.GetEvents(cancellationToken).ConfigureAwait(false))
            {
                switch (@event)
                {
                    case SendQueueEmptyEvent _:
                    {
                        if (_connection.IsSendNextMessageRequired)
                        {
                            await _connection.SendNextMessageAsync().ConfigureAwait(false);
                        }
                        break;
                    }
                    case RequestPendingEvent requestPendingEvent:
                    {
                        if (_requestChannels.TryGetValue(requestPendingEvent.Request.MessageID, out var requestChannel))
                        {
                            _logger.Debug("Request [{MessageID}]: {Status}", requestPendingEvent.Request.MessageID, requestPendingEvent.Response.Status.State);

                            if (!requestChannel.Writer.TryWrite(requestPendingEvent))
                            {
                                throw new DicomNetworkException(_responseChannelDoesNotHaveUnlimitedCapacity);
                            }
                        }
                        else
                        {
                            _logger.Debug($"Request [{{MessageID}}]: {{Status}} {_responseChannelIsGoneNote}", requestPendingEvent.Request.MessageID, requestPendingEvent.Response.Status.State);
                        }
                        break;
                    }
                    case RequestCompletedEvent requestCompletedEvent:
                    {
                        if (_requestChannels.TryGetValue(requestCompletedEvent.Request.MessageID, out var requestChannel))
                        {
                            _logger.Debug("Request [{MessageID}]: {Status}", requestCompletedEvent.Request.MessageID, requestCompletedEvent.Response.Status.State);

                            if (!requestChannel.Writer.TryWrite(requestCompletedEvent))
                            {
                                throw new DicomNetworkException(_responseChannelDoesNotHaveUnlimitedCapacity);
                            }
                            
                            requestChannel.Writer.TryComplete();
                        }
                        else
                        {
                            _logger.Debug($"Request [{{MessageID}}]: {{Status}} {_responseChannelIsGoneNote}", requestCompletedEvent.Request.MessageID, requestCompletedEvent.Response.Status.State);
                        }
                        
                        if (_connection.IsSendNextMessageRequired)
                        {
                            await _connection.SendNextMessageAsync().ConfigureAwait(false);
                        }
                        break;
                    }
                    case RequestTimedOutEvent requestTimedOutEvent:
                    {
                        if (_requestChannels.TryGetValue(requestTimedOutEvent.Request.MessageID, out var requestChannel))
                        {
                            _logger.Debug("Request [{MessageID}]: Time-Out after {Timeout}", requestTimedOutEvent.Request.MessageID, requestTimedOutEvent.Timeout);

                            if (!requestChannel.Writer.TryWrite(requestTimedOutEvent))
                            {
                                throw new DicomNetworkException(_responseChannelDoesNotHaveUnlimitedCapacity);
                            }
                            
                            requestChannel.Writer.TryComplete();
                        }
                        else
                        {
                            _logger.Debug($"Request [{{MessageID}}]: Time-Out after {{Timeout}} {_responseChannelIsGoneNote}", requestTimedOutEvent.Request.MessageID, requestTimedOutEvent.Timeout);
                        }
                        
                        if (_connection.IsSendNextMessageRequired)
                        {
                            await _connection.SendNextMessageAsync().ConfigureAwait(false);
                        }
                        break;
                    }
                    case DicomAbortedEvent dicomAbortedEvent:
                    {
                        if (!_associationChannel.Writer.TryWrite(dicomAbortedEvent))
                        {
                            throw new DicomNetworkException(_associationChannelDoesNotHaveUnlimitedCapacity);
                        }
                        
                        foreach (var messageId in _requestChannels.Keys)
                        {
                            if (_requestChannels.TryGetValue(messageId, out var requestChannel))
                            {
                                _logger.Debug("Request [{MessageID}]: Aborted", messageId);

                                if (!requestChannel.Writer.TryWrite(dicomAbortedEvent))
                                {
                                    throw new DicomNetworkException(_responseChannelDoesNotHaveUnlimitedCapacity);
                                }

                                requestChannel.Writer.TryComplete();
                            }
                            else
                            {
                                _logger.Debug($"Request [{{MessageID}}]: Aborted {_responseChannelIsGoneNote}", messageId);
                            }
                        }

                        break;
                    }
                    case DicomAssociationReleasedEvent dicomAssociationReleasedEvent:
                    {
                        _logger.Debug("Association {Association} released", AssociationToString(Association));

                        if (!_associationChannel.Writer.TryWrite(dicomAssociationReleasedEvent))
                        {
                            throw new DicomNetworkException(_associationChannelDoesNotHaveUnlimitedCapacity);
                        }

                        break;
                    }
                    case ConnectionClosedEvent connectionClosedEvent:
                    {
                        if (Interlocked.CompareExchange(ref _connectionClosedEvent, connectionClosedEvent, null) != null)
                        {
                            // Already disconnected
                            return;
                        }
                        
                        _logger.Debug("Connection closed");

                        if (!_associationChannel.Writer.TryWrite(connectionClosedEvent))
                        {
                            throw new DicomNetworkException(_associationChannelDoesNotHaveUnlimitedCapacity);
                        }
                        
                        foreach (var messageId in _requestChannels.Keys)
                        {
                            if (_requestChannels.TryGetValue(messageId, out var requestChannel))
                            {
                                _logger.Debug("Request [{MessageID}]: Connection closed", messageId);
                                
                                if (!requestChannel.Writer.TryWrite(connectionClosedEvent))
                                {
                                    throw new DicomNetworkException(_responseChannelDoesNotHaveUnlimitedCapacity);
                                }

                                requestChannel.Writer.TryComplete();
                            }
                            else
                            {
                                _logger.Debug($"Request [{{MessageID}}]: Connection closed {_responseChannelIsGoneNote}", messageId);
                            }
                        }
                        break;
                    }
                }
            }        
        }
        
        /// <inheritdoc cref="IAdvancedDicomClientAssociation.SendRequestAsync"/>
        public async IAsyncEnumerable<DicomResponse> SendRequestAsync(DicomRequest dicomRequest, [EnumeratorCancellation] CancellationToken cancellationToken) 
        {
            if (dicomRequest == null)
            {
                throw new ArgumentNullException(nameof(dicomRequest));
            }
            
            ThrowIfAlreadyDisposed();

            cancellationToken.ThrowIfCancellationRequested();
            
            var requestChannel = Channel.CreateUnbounded<IAdvancedDicomClientEvent>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = true,
                AllowSynchronousContinuations = false
            });

            var messageId = dicomRequest.MessageID;
            
            if (!_requestChannels.TryAdd(messageId, requestChannel))
            {
                throw new DicomNetworkException($"This DICOM request is already being sent: [{messageId}] {dicomRequest.GetType()}");
            }
            try
            {
                ThrowIfAlreadyDisconnected();

                await _connection.SendRequestAsync(dicomRequest).ConfigureAwait(false);

                while (await WaitToReadAsync(requestChannel, cancellationToken).ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    while (requestChannel.Reader.TryRead(out IAdvancedDicomClientEvent @event))
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        switch (@event)
                        {
                            case RequestPendingEvent requestPendingEvent:
                            {
                                _logger.Debug("{Request}: {Response}", dicomRequest.ToString(), requestPendingEvent.Response.ToString());

                                yield return requestPendingEvent.Response;
                                break;
                            }
                            case RequestCompletedEvent requestCompletedEvent:
                            {
                                _logger.Debug("{Request}: {Response}", dicomRequest.ToString(), requestCompletedEvent.Response.ToString());

                                yield return requestCompletedEvent.Response;
                                yield break;
                            }
                            case RequestTimedOutEvent requestTimedOutEvent:
                            {
                                _logger.Debug("{Request}: Time-Out after {Timeout}", dicomRequest.ToString(), requestTimedOutEvent.Timeout);

                                throw new DicomRequestTimedOutException(requestTimedOutEvent.Request, requestTimedOutEvent.Timeout);
                            }
                            case DicomAbortedEvent dicomAbortedEvent:
                            {
                                _logger.Debug("{Request}: Association was aborted", dicomRequest.ToString());

                                throw new DicomAssociationAbortedException(dicomAbortedEvent.Source, dicomAbortedEvent.Reason);
                            }
                            case ConnectionClosedEvent connectionClosedEvent:
                            {
                                _logger.Debug("{Request}: Connection was closed", dicomRequest.ToString());

                                connectionClosedEvent.ThrowException();

                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if(!_requestChannels.TryRemove(messageId, out _))
                {
                    throw new DicomNetworkException($"The response channel {dicomRequest} has already been cleaned up, this should never happen");
                }
            }

            async Task<bool> WaitToReadAsync(Channel<IAdvancedDicomClientEvent> channel, CancellationToken token)
            {
                /*
                 * This method calls the WaitToReadAsync method on the request channel, which returns true asynchronously when a new event is ready
                 * In the unlikely scenario that no event is produced ever again, we exit the WaitToReadAsync call and check for disconnection or disposal
                 * In usual circumstances, a disconnection would be communicated via an event, but if for some reason this never happens, we have an escape hatch here
                 */
                while (true)
                {
                    using var timeoutCts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
                    using var combinedCts = CancellationTokenSource.CreateLinkedTokenSource(timeoutCts.Token, token);

                    try
                    {
                        return await channel.Reader.WaitToReadAsync(combinedCts.Token).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        ThrowIfAlreadyDisconnected();
                        ThrowIfAlreadyDisposed();
                    }
                }
            }
        }

        /// <inheritdoc cref="IAdvancedDicomClientAssociation.ReleaseAsync"/>
        public async ValueTask ReleaseAsync(CancellationToken cancellationToken)
        {
            ThrowIfAlreadyDisposed();

            try
            {
                if (IsDisconnected)
                {
                    return;
                }

                await _connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

                await WaitForAssociationRelease(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _eventCollectorCts.Cancel();
            }
        }

        /// <inheritdoc cref="IAdvancedDicomClientAssociation.AbortAsync"/>
        public async ValueTask AbortAsync(CancellationToken cancellationToken)
        {
            ThrowIfAlreadyDisposed();

            try
            {
                if (IsDisconnected)
                {
                    return;
                }

                await _connection.SendAbortAsync(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified).ConfigureAwait(false);

                await WaitForAssociationRelease(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _eventCollectorCts.Cancel();
            }
        }

        private async Task WaitForAssociationRelease(CancellationToken cancellationToken)
        {
            _logger.Debug("Waiting for association {Association} to be released", AssociationToString(Association));

            while (await _associationChannel.Reader.WaitToReadAsync(cancellationToken).ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();

                while (_associationChannel.Reader.TryRead(out IAdvancedDicomClientEvent @event))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    switch (@event)
                    {
                        case DicomAssociationReleasedEvent _:
                            _logger.Debug("Association {Association} has been released", AssociationToString(Association));
                            return;
                        case DicomAbortedEvent _:
                            _logger.Debug("Association {Association} has been aborted", AssociationToString(Association));
                            return;
                        case ConnectionClosedEvent _:
                            _logger.Debug("Connection has closed");
                            return;
                    }
                }
            }
        }

        private bool IsDisconnected => Interlocked.CompareExchange(ref _connectionClosedEvent, null, null) != null;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfAlreadyDisconnected()
        {
            var connectionClosedEvent = Interlocked.CompareExchange(ref _connectionClosedEvent, null, null);
            connectionClosedEvent?.ThrowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfAlreadyDisposed()
        {
            if (!IsDisposed)
                return;
            ThrowDisposedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowDisposedException() => throw new ObjectDisposedException("This DICOM association is already disposed and can no longer be used");

        /// <inheritdoc cref="IAdvancedDicomClientAssociation.Dispose"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            // Guard against multiple disposals
            if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) != 0)
            {
                return;
            }
            
            _eventCollectorCts.Cancel();
            _eventCollectorCts.Dispose();

            if (!disposing)
            {
                _logger.Warn($"DICOM association {AssociationToString(Association)} was not disposed correctly, but was garbage collected instead");
            }
        }

        private static string AssociationToString(DicomAssociation association)
        {
            var callingAE = association.CallingAE ?? "<no calling AE>";
            var calledAE = association.CalledAE ?? "<no called AE>";
            var remoteHost = association.RemoteHost ?? "<no remote host>";
            var remotePort = association.RemotePort;
            return $"from {callingAE} to {calledAE} @{remoteHost}:{remotePort}";
        }
    }
}
