// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Network.Client.EventArguments;
using FellowOakDicom.Network.Client.States;
using FellowOakDicom.Network.Tls;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// DICOM client still provides some obsolete APIs that should not be removed yet, but should also not provide obsolete compiler warnings
#pragma warning disable CS0618
#pragma warning disable CS0612

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
        /// A handler to initiate TLS security, if null then TLS is not enabled.
        /// </summary>
        ITlsInitiator TlsInitiator { get; }

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
        /// Gets or sets whether to require a successful user identity negotiation during association.
        /// </summary>
        bool RequireSuccessfulUserIdentityNegotiation { get; set; }

        /// <summary>
        /// Gets or sets the user identity to negotiate with association.
        /// </summary>
        DicomUserIdentityNegotiation UserIdentityNegotiation { get; set; }

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
        /// The current DICOM client implementation is no longer state based, and has been rewritten as a wrapper around the new <see cref="FellowOakDicom.Network.Client.Advanced.Connection.IAdvancedDicomClientConnection"/>
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
        /// Set negotiation of user identity.
        /// </summary>
        /// <param name="userIdentityNegotiation">User identity negotiation information.</param>
        void NegotiateUserIdentity(DicomUserIdentityNegotiation userIdentityNegotiation);

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
        private readonly Tools.AsyncManualResetEvent _hasMoreRequests;

        internal ConcurrentQueue<StrongBox<DicomRequest>> QueuedRequests { get; }
        
        internal int AsyncInvoked { get; private set; }
        internal int AsyncPerformed { get; private set; }

        public string Host { get; }
        public int Port { get; }
        public ITlsInitiator TlsInitiator { get; }
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
        public bool RequireSuccessfulUserIdentityNegotiation { get; set; }
        public DicomUserIdentityNegotiation UserIdentityNegotiation { get; set; }
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
        /// <param name="tlsInitiator">if null then no TLS is enabled, otherwise the handler to initiate TLS security.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="clientOptions">The options that further modify the behavior of this DICOM client</param>
        /// <param name="serviceOptions">The options that modify the behavior of the base DICOM service</param>
        /// <param name="loggerFactory">The log manager that will be used to extract a default logger</param>
        /// <param name="advancedDicomClientConnectionFactory">The advanced DICOM client factory that will be used to actually send the requests</param>
        public DicomClient(string host, int port, ITlsInitiator tlsInitiator, string callingAe, string calledAe,
            DicomClientOptions clientOptions,
            DicomServiceOptions serviceOptions,
            ILoggerFactory loggerFactory,
            IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory)
        {
            Host = host;
            Port = port;
            TlsInitiator = tlsInitiator;
            CallingAe = callingAe;
            CalledAe = calledAe;
            ClientOptions = clientOptions;
            ServiceOptions = serviceOptions;
            QueuedRequests = new ConcurrentQueue<StrongBox<DicomRequest>>();
            AdditionalPresentationContexts = new List<DicomPresentationContext>();
            AdditionalExtendedNegotiations = new List<DicomExtendedNegotiation>();
            RequireSuccessfulUserIdentityNegotiation = true;
            AsyncInvoked = 1;
            AsyncPerformed = 1;
            
            _logger = loggerFactory.CreateLogger(Log.LogCategories.Network);
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _state = DicomClientIdleState.Instance;
            _isSending = 0;
            _hasMoreRequests = new Tools.AsyncManualResetEvent();
        }

        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            AsyncInvoked = invoked;
            AsyncPerformed = performed;
        }

        public void NegotiateUserIdentity(DicomUserIdentityNegotiation userIdentity)
        {
            if (userIdentity != null)
            {
                userIdentity.Validate();
            }

            UserIdentityNegotiation = userIdentity;
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
                                TlsInitiator = TlsInitiator,
                                NoDelay = ServiceOptions.TcpNoDelay,
                                ReceiveBufferSize = ServiceOptions.TcpReceiveBufferSize,
                                SendBufferSize = ServiceOptions.TcpSendBufferSize,
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

                        var requestsToSend = new Queue<DicomRequest>(requestsToRetry);
                        requestsToRetry.Clear();
                        var numberOfRequests = requestsToSend.Count;

                        while (numberOfRequests < maximumNumberOfRequestsPerAssociation
                               && QueuedRequests.TryDequeue(out var request))
                        {
                            requestsToSend.Enqueue(request.Value);
                            numberOfRequests++;
                        }
                        
                        _hasMoreRequests.Reset();

                        var associationRequest = new AdvancedDicomClientAssociationRequest
                        {
                            CallingAE = CallingAe,
                            CalledAE = CalledAe,
                            MaxAsyncOpsInvoked = AsyncInvoked,
                            MaxAsyncOpsPerformed = AsyncPerformed,
                            UserIdentityNegotiation = UserIdentityNegotiation
                        };

                        foreach (var request in requestsToSend)
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
                                requestsToRetry.AddRange(requestsToSend);

                                // try again
                                continue;
                            }
                        }

                        // Validate successful user identity negotiation response
                        if (RequireSuccessfulUserIdentityNegotiation &&
                            association.Association.UserIdentityNegotiation != null &&
                            association.Association.UserIdentityNegotiation.ServerResponse == null)
                        {
                            if (association.Association.UserIdentityNegotiation.PositiveResponseRequested)
                            {
                                throw new DicomNetworkException($"A positive response requested for user identity type {association.Association.UserIdentityNegotiation.UserIdentityType} but server response was null");
                            }

                            _logger.LogWarning("Successful user identity negotiation with type {UserIdentityType} was required but server response was null", association.Association.UserIdentityNegotiation.UserIdentityType);
                        }

                        AssociationAccepted?.Invoke(this, new AssociationAcceptedEventArgs(association.Association));

                        while (requestsToSend.Count > 0 && exception == null)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            if (!connection.CanStillProcessPDataTF)
                            {
                                _logger.LogDebug("The current association can no longer accept P-DATA-TF messages, a new association will have to be created for the remaining requests");
                                requestsToRetry.AddRange(requestsToSend);
                                break;
                            }

                            SetState(DicomClientSendingRequestsState.Instance);

                            /*
                             * Now we will send the DICOM requests
                             * Depending on the agreed upon AsyncInvoked setting, we can have x outstanding DICOM requests
                             * This means we can immediately send x DICOM requests in parallel
                             * Then, whenever one of the parallel DICOM requests complete, we can send another DICOM request
                             * Furthermore, after each DICOM request completes, we also check if more requests were queued into this DICOM client
                             * This should result in a maximum throughput of DICOM requests, always utilizing the maximum of async invoked requests
                             */
                            _logger.LogDebug("Sending {NumberOfRequests} requests", requestsToSend.Count);
                            var maximumNumberOfParallelRequests = association.Association.MaxAsyncOpsInvoked > 0
                                ? association.Association.MaxAsyncOpsInvoked
                                : int.MaxValue;
                            var parallelRequests = new List<Task>(Math.Min(requestsToSend.Count, maximumNumberOfParallelRequests));
                            while (parallelRequests.Count < maximumNumberOfParallelRequests
                                   && requestsToSend.Count > 0
                                   && connection.CanStillProcessPDataTF)
                            {
                                var nextRequest = requestsToSend.Dequeue();
                                var sendTask = SendRequestAsync(association, nextRequest, cancellationToken);
                                parallelRequests.Add(sendTask);
                                // Wait until the request is fully sent or until the request completes with an error or cancellation
                                await Task.WhenAny(nextRequest.AllPDUsSent, sendTask).ConfigureAwait(false);
                            }
                            
                            while (parallelRequests.Count > 0)
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                var finishedRequest = await Task.WhenAny(parallelRequests).ConfigureAwait(false);
                                await finishedRequest.ConfigureAwait(false);
                                parallelRequests.Remove(finishedRequest);
                                
                                // Check if more requests were queued in the meantime that we could possibly also send over the current association
                                while (numberOfRequests < maximumNumberOfRequestsPerAssociation
                                       && connection.CanStillProcessPDataTF
                                       && QueuedRequests.TryDequeue(out var request))
                                {
                                    requestsToSend.Enqueue(request.Value);

                                    numberOfRequests++;
                                }

                                if (requestsToSend.Count > 0 && connection.CanStillProcessPDataTF)
                                {
                                    var nextRequest = requestsToSend.Dequeue();
                                    var sendTask = SendRequestAsync(association, nextRequest, cancellationToken);
                                    parallelRequests.Add(sendTask);
                                    // Wait until the request is fully sent or until the request completes with an error or cancellation
                                    await Task.WhenAny(nextRequest.AllPDUsSent, sendTask).ConfigureAwait(false);
                                }
                            }
                            
                            _hasMoreRequests.Reset();
                            
                            // Linger behavior: if the queue is empty, wait for a bit before closing the association
                            if (requestsToSend.Count == 0
                                && numberOfRequests < maximumNumberOfRequestsPerAssociation
                                && ClientOptions.AssociationLingerTimeoutInMs > 0
                                && connection.CanStillProcessPDataTF)
                            {
                                _logger.LogDebug("Lingering on open association for {AssociationLingerTimeoutInMs}ms", ClientOptions.AssociationLingerTimeoutInMs);

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

                                    requestsToSend.Enqueue(request.Value);

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
                        _logger.LogWarning("DICOM request sending was cancelled");

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
                        _logger.LogError(e, "An error occurred while sending DICOM requests");

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
            
            _logger.LogDebug("{Request} is being sent", request.ToString());

            try
            {
                await using var enumerator = association.SendRequestAsync(request, cancellationToken).GetAsyncEnumerator(cancellationToken);

                while (await enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                _logger.LogDebug("{Request} has completed", request.ToString());
            }
            catch (DicomRequestTimedOutException e)
            {
                RequestTimedOut?.Invoke(this, new RequestTimedOutEventArgs(e.Request, e.TimeOut));

                _logger.LogDebug("{Request} has timed out", request.ToString());
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
            
            _logger.LogDebug("[{OldState}] --> [{NewState}]", oldState, newState);

            StateChanged?.Invoke(this, new StateChangedEventArgs(oldState, newState));
        }

        private async Task ReleaseAssociationAsync(IAdvancedDicomClientAssociation association)
        {
            SetState(DicomClientReleaseAssociationState.Instance);

            using (var cts = new CancellationTokenSource(ClientOptions.AssociationReleaseTimeoutInMs))
            {
                try
                {
                    await association.ReleaseAsync(cts.Token).ConfigureAwait(false);
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
                    await association.AbortAsync(cts.Token).ConfigureAwait(false);
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
