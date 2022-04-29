// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Network.Client.EventArguments;
using FellowOakDicom.Network.Client.States;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client
{
    public interface IDicomClient
    {
        /// <summary>
        /// DICOM host.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Port.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// True if TLS security should be enabled, false otherwise.
        /// </summary>
        bool UseTls { get; }

        /// <summary>
        /// Calling Application Entity Title.
        /// </summary>
        string CallingAe { get; }

        /// <summary>
        /// Called Application Entity Title.
        /// </summary>
        string CalledAe { get; }

        /// <summary>
        /// Gets or sets the logger that will be used by this DicomClient
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        DicomServiceOptions ServiceOptions { get; }

        /// <summary>
        /// Gets or sets options to control behavior of this DICOM client.
        /// </summary>
        DicomClientOptions ClientOptions { get; }

        /// <summary>
        /// Gets or sets additional presentation contexts to negotiate with association.
        /// </summary>
        List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }

        /// <summary>
        /// Gets or sets extended negotiation items to negotiate with association.
        /// </summary>
        List<DicomExtendedNegotiation> AdditionalExtendedNegotiations { get; set; }

        /// <summary>
        /// Gets or sets the fallback encoding.
        /// </summary>
        Encoding FallbackEncoding { get; set; }

        /// <summary>
        /// Gets or sets the handler of a client C-STORE request.
        /// </summary>
        DicomClientCStoreRequestHandler OnCStoreRequest { get; set; }

        /// <summary>
        /// Gets or sets the handler of  client N-EVENT-REPORT-RQ
        /// </summary>
        DicomClientNEventReportRequestHandler OnNEventReportRequest { get; set; }

        /// <summary>
        /// Gets whether a new send invocation is required. Send needs to be called if there are requests in queue and client is not connected.
        /// </summary>
        bool IsSendRequired { get; }

        /// <summary>
        /// Triggers when an association is accepted
        /// </summary>
        event EventHandler<AssociationAcceptedEventArgs> AssociationAccepted;

        /// <summary>
        /// Triggers when an association is rejected.
        /// </summary>
        event EventHandler<AssociationRejectedEventArgs> AssociationRejected;

        /// <summary>
        /// Representation of the DICOM association released event.
        /// </summary>
        event EventHandler AssociationReleased;

        /// <summary>
        /// Representation of the DICOM association request timed out event.
        /// </summary>
        event EventHandler<AssociationRequestTimedOutEventArgs> AssociationRequestTimedOut;

        /// <summary>
        /// Whenever the DICOM client changes state, an event will be emitted containing the old state and the new state.
        /// The current DICOM client implementation is no longer state based, and has been rewritten as a wrapper around the new <see cref="IAdvancedDicomClientConnection"/>
        /// This event handler is still supported for backwards compatibility reasons, but may be removed in the future.
        /// </summary>
        [Obsolete(nameof(StateChanged) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
        event EventHandler<StateChangedEventArgs> StateChanged;

        /// <summary>
        /// Triggered when a DICOM request times out.
        /// </summary>
        event EventHandler<RequestTimedOutEventArgs> RequestTimedOut;

        /// <summary>
        /// Set negotiation asynchronous operations.
        /// </summary>
        /// <param name="invoked">Asynchronous operations invoked.</param>
        /// <param name="performed">Asynchronous operations performed.</param>
        void NegotiateAsyncOps(int invoked = 0, int performed = 0);

        /// <summary>
        /// Enqueues a new DICOM request for execution.
        /// </summary>
        /// <param name="dicomRequest">The DICOM request to send</param>
        Task AddRequestAsync(DicomRequest dicomRequest);

        /// <summary>
        /// Enqueues new DICOM requests for execution.
        /// When you have many requests, this method is recommended over calling <see cref="AddRequestAsync"/> multiple times.
        /// </summary>
        /// <param name="dicomRequests">The DICOM requests to send</param>
        Task AddRequestsAsync(IEnumerable<DicomRequest> dicomRequests);

        /// <summary>
        /// Sends existing requests to DICOM service. Note that subsequent calls, when the DICOM client is already sending its requests, will be completely ignored.
        /// If you want to cancel the process, be sure to cancel the cancellation token that was passed into the first call.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that can abort the send process if necessary</param>
        /// <param name="cancellationMode">The cancellation mode that determines the cancellation behavior</param>
        Task SendAsync(CancellationToken cancellationToken = default(CancellationToken),
            DicomClientCancellationMode cancellationMode = DicomClientCancellationMode.ImmediatelyReleaseAssociation);
    }      

    public class DicomClient : IDicomClient
    {
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;
        private ILogger _logger;
        private DicomClientState _state;
        private long _isSending;
        private readonly Tasks.AsyncManualResetEvent _hasMoreRequests;

        internal ConcurrentQueue<StrongBox<DicomRequest>> QueuedRequests { get; }
        
        internal int AsyncInvoked { get; private set; }
        internal int AsyncPerformed { get; private set; }

        public string Host { get; }
        public int Port { get; }
        public bool UseTls { get; }
        public string CallingAe { get; }
        public string CalledAe { get; }
        
        public bool IsSendRequired => _isSending == 0 && QueuedRequests.Any();

        public ILogger Logger
        {
            get => _logger;
            set => _logger = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DicomClientOptions ClientOptions { get; set; }
        public DicomServiceOptions ServiceOptions { get; set; }
        public List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }
        public List<DicomExtendedNegotiation> AdditionalExtendedNegotiations { get; set; }
        public Encoding FallbackEncoding { get; set; }
        public DicomClientCStoreRequestHandler OnCStoreRequest { get; set; }
        public DicomClientNEventReportRequestHandler OnNEventReportRequest { get; set; }

        public event EventHandler<AssociationAcceptedEventArgs> AssociationAccepted;
        public event EventHandler<AssociationRejectedEventArgs> AssociationRejected;
        public event EventHandler<AssociationRequestTimedOutEventArgs> AssociationRequestTimedOut;
        public event EventHandler AssociationReleased;
        
        public event EventHandler<StateChangedEventArgs> StateChanged;
        public event EventHandler<RequestTimedOutEventArgs> RequestTimedOut;

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="clientOptions">The options that further modify the behavior of this DICOM client</param>
        /// <param name="serviceOptions">The options that modify the behavior of the base DICOM service</param>
        /// <param name="logger">The logger</param>
        /// <param name="advancedDicomClientConnectionFactory">The advanced DICOM client factory that will be used to actually send the requests</param>
        public DicomClient(string host, int port, bool useTls, string callingAe, string calledAe,
            DicomClientOptions clientOptions,
            DicomServiceOptions serviceOptions,
            ILogger logger,
            IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory)
        {
            Host = host;
            Port = port;
            UseTls = useTls;
            CallingAe = callingAe;
            CalledAe = calledAe;
            ClientOptions = clientOptions;
            ServiceOptions = serviceOptions;
            QueuedRequests = new ConcurrentQueue<StrongBox<DicomRequest>>();
            AdditionalPresentationContexts = new List<DicomPresentationContext>();
            AdditionalExtendedNegotiations = new List<DicomExtendedNegotiation>();
            AsyncInvoked = 1;
            AsyncPerformed = 1;
            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _state = DicomClientIdleState.Instance;
            _isSending = 0;
            _hasMoreRequests = new Tasks.AsyncManualResetEvent();
        }

        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            AsyncInvoked = invoked;
            AsyncPerformed = performed;
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
        {
            QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            
            _hasMoreRequests.Set();
            
            return Task.CompletedTask;
        }

        public Task AddRequestsAsync(IEnumerable<DicomRequest> dicomRequests)
        {
            if (dicomRequests == null)
            {
                return Task.CompletedTask;
            }

            foreach (DicomRequest dicomRequest in dicomRequests)
            {
                if (dicomRequest == null)
                {
                    continue;
                }

                QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            }
            
            _hasMoreRequests.Set();

            return Task.CompletedTask;
        }



        public async Task SendAsync(CancellationToken cancellationToken = default,
            DicomClientCancellationMode cancellationMode = DicomClientCancellationMode.ImmediatelyReleaseAssociation)
        {
            if (Interlocked.CompareExchange(ref _isSending, 1, 0) != 0)
            {
                // Already sending
                return;
            }
            
            try
            {
                var exception = (Exception)null;
                var maximumNumberOfRequestsPerAssociation = ClientOptions.MaximumNumberOfRequestsPerAssociation ?? int.MaxValue;
                var maximumNumberOfConsecutiveTimedOutAssociationRequests = ClientOptions.MaximumNumberOfConsecutiveTimedOutAssociationRequests;
                var numberOfConsecutiveTimedOutAssociationRequests = 0;
                var requestsToRetry = new List<DicomRequest>();

                while (!cancellationToken.IsCancellationRequested
                       && !(QueuedRequests.IsEmpty && requestsToRetry.Count == 0)
                       && exception == null)
                {
                    IAdvancedDicomClientConnection connection = null;
                    IAdvancedDicomClientAssociation association = null;
                    try
                    {
                        var connectionRequest = new AdvancedDicomClientConnectionRequest
                        {
                            Logger = Logger,
                            FallbackEncoding = FallbackEncoding,
                            DicomServiceOptions = ServiceOptions,
                            NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                            {
                                Host = Host,
                                Port = Port,
                                UseTls = UseTls,
                                NoDelay = ServiceOptions.TcpNoDelay,
                                IgnoreSslPolicyErrors = ServiceOptions.IgnoreSslPolicyErrors,
                                Timeout = TimeSpan.FromMilliseconds(ClientOptions.AssociationRequestTimeoutInMs)
                            },
                            RequestHandlers = new AdvancedDicomClientConnectionRequestHandlers
                            {
                                OnCStoreRequest = OnCStoreRequest,
                                OnNEventReportRequest = OnNEventReportRequest
                            }
                        };

                        SetState(DicomClientConnectState.Instance);

                        connection = await _advancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken).ConfigureAwait(false);

                        SetState(DicomClientRequestAssociationState.Instance);

                        var requests = new List<DicomRequest>();
                        requests.AddRange(requestsToRetry);
                        requestsToRetry.Clear();
                        var numberOfRequests = requests.Count;

                        while (numberOfRequests < maximumNumberOfRequestsPerAssociation
                               && QueuedRequests.TryDequeue(out var request))
                        {
                            requests.Add(request.Value);
                            numberOfRequests++;
                        }
                        
                        _hasMoreRequests.Reset();

                        var associationRequest = new AdvancedDicomClientAssociationRequest
                        {
                            CallingAE = CallingAe,
                            CalledAE = CalledAe,
                            MaxAsyncOpsInvoked = AsyncInvoked,
                            MaxAsyncOpsPerformed = AsyncPerformed,
                        };

                        foreach (var request in requests)
                        {
                            associationRequest.PresentationContexts.AddFromRequest(request);
                            associationRequest.ExtendedNegotiations.AddFromRequest(request);
                        }

                        foreach (var context in AdditionalPresentationContexts)
                        {
                            associationRequest.PresentationContexts.Add(
                                context.AbstractSyntax,
                                context.UserRole,
                                context.ProviderRole,
                                context.GetTransferSyntaxes().ToArray());
                        }

                        foreach (var extendedNegotiation in AdditionalExtendedNegotiations)
                        {
                            associationRequest.ExtendedNegotiations.AddOrUpdate(
                                extendedNegotiation.SopClassUid,
                                extendedNegotiation.RequestedApplicationInfo,
                                extendedNegotiation.ServiceClassUid,
                                extendedNegotiation.RelatedGeneralSopClasses.ToArray());
                        }

                        using (var timeoutCts = new CancellationTokenSource(TimeSpan.FromMilliseconds(ClientOptions.AssociationRequestTimeoutInMs)))
                        using (var combinedCts = CancellationTokenSource.CreateLinkedTokenSource(timeoutCts.Token, cancellationToken))
                        {
                            try
                            {
                                association = await connection.OpenAssociationAsync(associationRequest, combinedCts.Token).ConfigureAwait(false);
                                numberOfConsecutiveTimedOutAssociationRequests = 0;
                            }
                            catch (OperationCanceledException)
                            {
                                // If the original cancellation token triggered, stop sending ASAP
                                if (cancellationToken.IsCancellationRequested)
                                {
                                    throw;
                                }

                                // If the original cancellation token did not trigger, it must have been a timeout that lead us here
                                // we keep track of how many consecutive times this happens
                                numberOfConsecutiveTimedOutAssociationRequests++;
                                
                                AssociationRequestTimedOut?.Invoke(this, new AssociationRequestTimedOutEventArgs(
                                    ClientOptions.AssociationRequestTimeoutInMs,
                                    numberOfConsecutiveTimedOutAssociationRequests,
                                    maximumNumberOfConsecutiveTimedOutAssociationRequests
                                ));

                                if (numberOfConsecutiveTimedOutAssociationRequests >= maximumNumberOfConsecutiveTimedOutAssociationRequests)
                                {
                                    // stop sending when we reach the limit
                                    throw new DicomAssociationRequestTimedOutException(
                                        ClientOptions.AssociationRequestTimeoutInMs,
                                numberOfConsecutiveTimedOutAssociationRequests
                                    );
                                }

                                // Save the requests to retry, otherwise they are lost because we already extracted them from QueuedRequests
                                requestsToRetry.AddRange(requests);

                                // try again
                                continue;
                            }
                        }

                        AssociationAccepted?.Invoke(this, new AssociationAcceptedEventArgs(association.Association));

                        while (requests.Count > 0 && exception == null)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            SetState(DicomClientSendingRequestsState.Instance);

                            _logger.Debug("Queueing {NumberOfRequests} requests", requests.Count);
                            var sendTasks = new List<Task>(requests.Count);
                            
                            // Try to send all tasks immediately, this could work depending on the nr of requests and the async ops invoked setting
                            foreach(var request in requests)
                            {
                                sendTasks.Add(SendRequestAsync(association, request, cancellationToken));
                            }

                            // Now wait for the requests to complete
                            await Task.WhenAll(sendTasks).ConfigureAwait(false);
                            
                            requests.Clear();

                            // If more requests were queued since we started, try to send those too over the same association
                            while (numberOfRequests < maximumNumberOfRequestsPerAssociation
                                   && QueuedRequests.TryDequeue(out var request))
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                requests.Add(request.Value);

                                numberOfRequests++;
                            }

                            _hasMoreRequests.Reset();
                            
                            // Linger behavior: if the queue is empty, wait for a bit before closing the association
                            if (requests.Count == 0
                                && numberOfRequests < maximumNumberOfRequestsPerAssociation
                                && ClientOptions.AssociationLingerTimeoutInMs > 0)
                            {
                                _logger.Debug($"Lingering on open association for {ClientOptions.AssociationLingerTimeoutInMs}ms");

                                SetState(DicomClientLingeringState.Instance);
                                
                                await Task.WhenAny(
                                    _hasMoreRequests.WaitAsync(),
                                    Task.Delay(ClientOptions.AssociationLingerTimeoutInMs, cancellationToken)
                                ).ConfigureAwait(false);
                                
                                // Add requests that were added after lingering
                                while (numberOfRequests < maximumNumberOfRequestsPerAssociation
                                       && QueuedRequests.TryDequeue(out var request))
                                {
                                    cancellationToken.ThrowIfCancellationRequested();

                                    requests.Add(request.Value);

                                    numberOfRequests++;
                                }
                                
                                _hasMoreRequests.Reset();
                            }
                        }

                        await ReleaseAssociationAsync(association).ConfigureAwait(false);
                    }
                    catch (DicomAssociationRejectedException e)
                    {
                        numberOfConsecutiveTimedOutAssociationRequests = 0;
                        
                        AssociationRejected?.Invoke(this, new AssociationRejectedEventArgs(e.RejectResult, e.RejectSource, e.RejectReason));

                        exception = e;
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.Warn("DICOM request sending was cancelled");

                        if (association != null && association.IsDisposed == false)
                        {
                            switch (cancellationMode)
                            {
                                case DicomClientCancellationMode.ImmediatelyReleaseAssociation:
                                    await ReleaseAssociationAsync(association).ConfigureAwait(false);
                                    
                                    break;
                                case DicomClientCancellationMode.ImmediatelyAbortAssociation:
                                    await AbortAssociationAsync(association).ConfigureAwait(false);

                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(nameof(cancellationMode), cancellationMode, null);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error("An error occurred while sending DICOM requests: {Error}", e);

                        exception = e;

                        if (association != null && association.IsDisposed == false)
                        {
                            await AbortAssociationAsync(association).ConfigureAwait(false);
                        }
                    }
                    finally
                    {
                        association?.Dispose();
                        connection?.Dispose();
                    }
                }

                if (exception != null)
                {
                    ExceptionDispatchInfo.Capture(exception).Throw();
                }
            }
            finally
            {
                SetState(DicomClientIdleState.Instance);

                _isSending = 0;
            }
        }

        /// <summary>
        /// Helper method that sends a DICOM request over an advanced DICOM association
        /// Timeout exceptions are caught and emitted as an event for backwards compatibility
        /// </summary>
        /// <param name="association">The association over which to send the request</param>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The cancellation token that stops the entire process</param>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> of <see cref="DicomResponse"/></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="association"/> or <paramref name="request"/> are null</exception>
        private async Task SendRequestAsync(IAdvancedDicomClientAssociation association, DicomRequest request, CancellationToken cancellationToken)
        {
            if (association == null)
            {
                throw new ArgumentNullException(nameof(association));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            _logger.Debug("{Request} is being enqueued for sending", request.ToString());

            try
            {
                await using var enumerator = association.SendRequestAsync(request, cancellationToken).GetAsyncEnumerator(cancellationToken);

                while (await enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                _logger.Debug("{Request} has completed", request.ToString());
            }
            catch (DicomRequestTimedOutException e)
            {
                RequestTimedOut?.Invoke(this, new RequestTimedOutEventArgs(e.Request, e.TimeOut));

                _logger.Debug("{Request} has timed out", request.ToString());
            }
        }
        
        /// <summary>
        /// Helper method that 'sets' the state of the DicomClient<br/>
        /// This exists for backwards compatibility reasons. In the past, DicomClient was implemented using a state pattern.<br/>
        /// While most of this was hidden away for consumers, DicomClient did expose a <see cref="StateChanged"/> event for expert scenarios.
        /// In order to not break the existing consumers of this event, we still expose this event and properly emit the correct "states", even though the states itself are now empty
        /// </summary>
        /// <param name="state">The new state that should be set</param>
        [Obsolete]
        private void SetState(DicomClientState state)
        {
            DicomClientState oldState;
            DicomClientState newState = state;
            
            oldState = _state;

            if (oldState == newState)
            {
                return;
            }

            _state = state;
            
            _logger.Debug($"[{oldState}] --> [{newState}]");

            StateChanged?.Invoke(this, new StateChangedEventArgs(oldState, newState));
        }

        private async Task ReleaseAssociationAsync(IAdvancedDicomClientAssociation association)
        {
            SetState(DicomClientReleaseAssociationState.Instance);

            using (var cts = new CancellationTokenSource(ClientOptions.AssociationReleaseTimeoutInMs))
            {
                try
                {
                    await association.ReleaseAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // Ignored
                }
                finally
                {
                    association.Dispose();
                }
            }

            AssociationReleased?.Invoke(this, EventArgs.Empty);
        }
        
        private async Task AbortAssociationAsync(IAdvancedDicomClientAssociation association)
        {
            SetState(DicomClientAbortState.Instance);

            using (var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100)))
            {
                try
                {
                    await association.AbortAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // Ignored
                }
                finally
                {
                    association.Dispose();
                }
            }
            
            AssociationReleased?.Invoke(this, EventArgs.Empty);
        }
    }
}
