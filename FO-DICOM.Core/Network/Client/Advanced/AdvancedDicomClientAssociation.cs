using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    public interface IAdvancedDicomClientAssociation : IAsyncDisposable
    {
        IAsyncEnumerable<DicomResponse> SendRequestAsync(DicomRequest dicomRequest, CancellationToken cancellationToken);
    }

    public class AdvancedDicomClientAssociation : IAdvancedDicomClientAssociation
    {
        private readonly Task _eventCollector;
        private readonly CancellationTokenSource _eventCollectorCts;
        private readonly ConcurrentDictionary<int, Channel<IAdvancedDicomClientConnectionEvent>> _requestChannels;
        
        private AdvancedDicomClientOptions Options { get; }
        public IAdvancedDicomClientConnection Connection { get; }
        public DicomAssociation Association { get; }

        public AdvancedDicomClientAssociation(AdvancedDicomClientOptions advancedDicomClientOptions, IAdvancedDicomClientConnection connection,
            DicomAssociation association)
        {
            _eventCollectorCts = new CancellationTokenSource();
            _eventCollector = Task.Run(CollectEvents);
            _requestChannels = new ConcurrentDictionary<int, Channel<IAdvancedDicomClientConnectionEvent>>();

            Options = advancedDicomClientOptions ?? throw new ArgumentNullException(nameof(advancedDicomClientOptions));
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Association = association ?? throw new ArgumentNullException(nameof(association));
        }

        private async Task CollectEvents()
        {
            await foreach (var @event in Connection.Callbacks.GetEvents(_eventCollectorCts.Token))
            {
                switch (@event)
                {
                    case SendQueueEmptyEvent _:
                    {
                        if (Connection.IsSendNextMessageRequired)
                        {
                            await Connection.SendNextMessageAsync().ConfigureAwait(false);
                        }
                        break;
                    }
                    case RequestPendingEvent requestPendingEvent:
                    {
                        if (_requestChannels.TryGetValue(requestPendingEvent.Request.MessageID, out var requestChannel))
                        {
                            await requestChannel.Writer.WriteAsync(requestPendingEvent).ConfigureAwait(false);
                        }
                        break;
                    }
                    case RequestCompletedEvent requestCompletedEvent:
                    {
                        if (_requestChannels.TryGetValue(requestCompletedEvent.Request.MessageID, out var requestChannel))
                        {
                            await requestChannel.Writer.WriteAsync(requestCompletedEvent).ConfigureAwait(false);
                        }
                        if (Connection.IsSendNextMessageRequired)
                        {
                            await Connection.SendNextMessageAsync().ConfigureAwait(false);
                        }
                        break;
                    }
                    case RequestTimedOutEvent requestTimedOutEvent:
                    {
                        if (_requestChannels.TryGetValue(requestTimedOutEvent.Request.MessageID, out var requestChannel))
                        {
                            await requestChannel.Writer.WriteAsync(requestTimedOutEvent).ConfigureAwait(false);
                        }
                        if (Connection.IsSendNextMessageRequired)
                        {
                            await Connection.SendNextMessageAsync().ConfigureAwait(false);
                        }
                        break;
                    }
                    case DicomAbortedEvent dicomAbortedEvent:
                    {
                        foreach (var requestChannel in _requestChannels.Values)
                        {
                            await requestChannel.Writer.WriteAsync(dicomAbortedEvent).ConfigureAwait(false);
                        }
                        return;
                    }
                    case ConnectionClosedEvent connectionClosedEvent:
                    {
                        foreach (var requestChannel in _requestChannels.Values)
                        {
                            await requestChannel.Writer.WriteAsync(connectionClosedEvent).ConfigureAwait(false);
                            
                            requestChannel.Writer.Complete();
                        }
                        return;
                    }
                }
            }        
        }

        public async IAsyncEnumerable<DicomResponse> SendRequestAsync(DicomRequest dicomRequest, [EnumeratorCancellation] CancellationToken cancellationToken) 
        {
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
                await Connection.SendRequestAsync(dicomRequest).ConfigureAwait(false);

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
                                yield return requestPendingEvent.Response;
                                break;
                            }
                            case RequestCompletedEvent requestCompletedEvent:
                            {
                                yield return requestCompletedEvent.Response;
                                break;
                            }
                            case RequestTimedOutEvent requestTimedOutEvent:
                            {
                                throw new DicomRequestTimedOutException(requestTimedOutEvent.Request, requestTimedOutEvent.Timeout);
                            }
                            case DicomAbortedEvent dicomAbortedEvent:
                            {
                                throw new DicomAssociationAbortedException(dicomAbortedEvent.Source, dicomAbortedEvent.Reason);
                            }
                            case ConnectionClosedEvent connectionClosedEvent:
                            {
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
                    throw new DicomNetworkException($"This DICOM request has already been cleaned up: [{messageId}] {dicomRequest.GetType()}");
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await Connection.SendAssociationReleaseRequestAsync().ConfigureAwait(false);

                using var timeoutCts = new CancellationTokenSource(Options.AssociationReleaseTimeout);
                
                IAsyncEnumerable<IAdvancedDicomClientConnectionEvent> events = Connection.Callbacks.GetEvents(timeoutCts.Token);
                
                await WaitForAssociationRelease(events, timeoutCts.Token).ConfigureAwait(false);
            }
            finally
            {
                Connection?.Dispose();

                if (!_eventCollector.IsCompleted)
                {
                    _eventCollectorCts.Cancel();
                }

                _eventCollectorCts.Dispose();
            }

            async Task WaitForAssociationRelease(IAsyncEnumerable<IAdvancedDicomClientConnectionEvent> events, CancellationToken cancellationToken)
            {
                await foreach (var @event in events.WithCancellation(cancellationToken))
                {
                    switch (@event)
                    {
                        case DicomAssociationReleasedEvent _:
                            return;
                        case DicomAbortedEvent _:
                            return;
                        case ConnectionClosedEvent _:
                            return;
                    }
                }
            }
        }
    }
}