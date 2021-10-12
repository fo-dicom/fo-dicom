﻿using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Network.Client.Advanced.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
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
        DicomAssociation Association { get; }
        
        bool IsDisposed { get; }
        
        IAsyncEnumerable<DicomResponse> SendRequestAsync(DicomRequest dicomRequest, CancellationToken cancellationToken);

        ValueTask ReleaseAsync(CancellationToken cancellationToken);
        
        ValueTask AbortAsync(CancellationToken cancellationToken);
    }

    public class AdvancedDicomClientAssociation : IAdvancedDicomClientAssociation
    {
        private const string _responseChannelIsGoneNote = "(Note: the response channel is gone. This can happen when the request is cancelled after it has been sent)";
        private const string _responseChannelDoesNotHaveUnlimitedCapacity = "Failed to write to the response channel. This should never happen, because response channels should be created with unlimited capacity";
        private const string _associationChannelDoesNotHaveUnlimitedCapacity = "Failed to write to the association channel. This should never happen, because the association channel should be created with unlimited capacity";
        
        private readonly ILogger _logger;
        private readonly Task _eventCollector;
        private readonly CancellationTokenSource _eventCollectorCts;
        private readonly ConcurrentDictionary<int, Channel<IAdvancedDicomClientConnectionEvent>> _requestChannels;
        private readonly Channel<IAdvancedDicomClientConnectionEvent> _associationChannel;
        private readonly IAdvancedDicomClientConnection _connection;
        
        private long _isDisposed;
        public bool IsDisposed => (ulong)Interlocked.Read(ref _isDisposed) > 0;
        
        public DicomAssociation Association { get; }

        public AdvancedDicomClientAssociation(IAdvancedDicomClientConnection connection, DicomAssociation association, ILogger logger)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventCollectorCts = new CancellationTokenSource();
            _eventCollector = Task.Run(() => CollectEventsAsync(_eventCollectorCts.Token));
            _requestChannels = new ConcurrentDictionary<int, Channel<IAdvancedDicomClientConnectionEvent>>();
            _associationChannel = Channel.CreateUnbounded<IAdvancedDicomClientConnectionEvent>(new UnboundedChannelOptions
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
                        _logger.Debug("Connection released");

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

        public async IAsyncEnumerable<DicomResponse> SendRequestAsync(DicomRequest dicomRequest, [EnumeratorCancellation] CancellationToken cancellationToken) 
        {
            ThrowIfAlreadyDisposed();

            if (dicomRequest == null)
            {
                throw new ArgumentNullException(nameof(dicomRequest));
            }

            cancellationToken.ThrowIfCancellationRequested();
            
            var requestChannel = Channel.CreateUnbounded<IAdvancedDicomClientConnectionEvent>(new UnboundedChannelOptions
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
                await _connection.SendRequestAsync(dicomRequest).ConfigureAwait(false);

                while (await requestChannel.Reader.WaitToReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    while (requestChannel.Reader.TryRead(out IAdvancedDicomClientConnectionEvent @event))
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
                                break;
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

                                if (connectionClosedEvent.Exception != null)
                                {
                                    ExceptionDispatchInfo.Capture(connectionClosedEvent.Exception).Throw();
                                }
                                else
                                {
                                    throw new ConnectionClosedPrematurelyException();
                                }

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
        }

        public async ValueTask ReleaseAsync(CancellationToken cancellationToken)
        {
            ThrowIfAlreadyDisposed();

            try
            {
                await _connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

                await WaitForAssociationRelease(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _eventCollectorCts.Cancel();
            }
        }

        public async ValueTask AbortAsync(CancellationToken cancellationToken)
        {
            ThrowIfAlreadyDisposed();

            try
            {
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

                while (_associationChannel.Reader.TryRead(out IAdvancedDicomClientConnectionEvent @event))
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfAlreadyDisposed()
        {
            if (!IsDisposed)
                return;
            ThrowDisposedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowDisposedException() => throw new ObjectDisposedException("This DICOM association is already disposed and can no longer be used");

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