// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.EventArguments;
using FellowOakDicom.Network.Client.States;

namespace FellowOakDicom.Network.Client
{

    public interface IDicomClient
    {
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
        /// Gets the network manager that will be used to open connections.
        /// </summary>
        INetworkManager NetworkManager { get; }

        /// <summary>
        /// Gets the log manager that will be used to log information to.
        /// </summary>
        ILogManager LogManager { get; }

        /// <summary>
        /// Gets the transcoder manager that will be used to transcode incoming or outgoing DICOM files.
        /// </summary>
        ITranscoderManager TranscoderManager { get; }

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
        /// Whenever the DICOM client changes state, an event will be emitted containing the old state and the new state.
        /// </summary>
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
        private readonly SemaphoreSlim _transitionLock = new SemaphoreSlim(1, 1);
        private IDicomClientState State { get; set; }

        internal ConcurrentQueue<StrongBox<DicomRequest>> QueuedRequests { get; }
        internal int AsyncInvoked { get; private set; }
        internal int AsyncPerformed { get; private set; }

        public string Host { get; }
        public int Port { get; }
        public bool UseTls { get; }
        public string CallingAe { get; }
        public string CalledAe { get; }
        public bool IsSendRequired => State is DicomClientIdleState && QueuedRequests.Any();
        public ILogger Logger { get; set; }
        public DicomClientOptions ClientOptions { get; set; }
        public DicomServiceOptions ServiceOptions { get; set; }
        public List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }
        public List<DicomExtendedNegotiation> AdditionalExtendedNegotiations { get; set; }
        public Encoding FallbackEncoding { get; set; }
        public DicomClientCStoreRequestHandler OnCStoreRequest { get; set; }
        public DicomClientNEventReportRequestHandler OnNEventReportRequest { get; set; }
        public INetworkManager NetworkManager { get; }
        public ILogManager LogManager { get; }
        public ITranscoderManager TranscoderManager { get; }

        public event EventHandler<AssociationAcceptedEventArgs> AssociationAccepted;
        public event EventHandler<AssociationRejectedEventArgs> AssociationRejected;
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
        /// <param name="networkManager">The network manager that will be used to connect to the DICOM server</param>
        /// <param name="logManager">The log manager that will be used to extract a default logger</param>
        /// <param name="transcoderManager">The transcoder manager that will be used to transcode incoming or outgoing DICOM files</param>
        public DicomClient(string host, int port, bool useTls, string callingAe, string calledAe,
            DicomClientOptions clientOptions,
            DicomServiceOptions serviceOptions,
            INetworkManager networkManager,
            ILogManager logManager,
            ITranscoderManager transcoderManager)
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
            State = new DicomClientIdleState(this);
            NetworkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            TranscoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
            LogManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            Logger = logManager.GetLogger("FellowOakDicom.Network");
        }

        private async Task ExecuteWithinTransitionLock(Func<Task> task)
        {
            await _transitionLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await task().ConfigureAwait(false);
            }
            finally
            {
                _transitionLock.Release();
            }
        }

        internal async Task<IDicomClientState> Transition(IDicomClientState newState, DicomClientCancellation cancellation)
        {
            Task InternalTransition()
            {
                var oldState = State;

                Logger.Debug($"[{oldState}] --> [{newState}]");

                oldState.Dispose();

                State = newState ?? throw new ArgumentNullException(nameof(newState));

                StateChanged?.Invoke(this, new StateChangedEventArgs(oldState, newState));

                return Task.CompletedTask;
            }

            await ExecuteWithinTransitionLock(InternalTransition).ConfigureAwait(false);

            return await newState.GetNextStateAsync(cancellation).ConfigureAwait(false);
        }

        internal void NotifyAssociationAccepted(AssociationAcceptedEventArgs eventArgs)
            => AssociationAccepted?.Invoke(this, eventArgs);

        internal void NotifyAssociationRejected(AssociationRejectedEventArgs eventArgs)
            => AssociationRejected?.Invoke(this, eventArgs);

        internal void NotifyAssociationReleased()
            => AssociationReleased?.Invoke(this, EventArgs.Empty);

        internal void NotifyRequestTimedOut(RequestTimedOutEventArgs eventArgs)
            => RequestTimedOut?.Invoke(this, eventArgs);

        internal Task OnSendQueueEmptyAsync()
            => State.OnSendQueueEmptyAsync();

        internal Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
            => State.OnRequestCompletedAsync(request, response);

        internal Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout)
            => State.OnRequestTimedOutAsync(request, timeout);

        internal Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
            => ExecuteWithinTransitionLock(() => State.OnReceiveAssociationAcceptAsync(association));

        internal Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
            => ExecuteWithinTransitionLock(() => State.OnReceiveAssociationRejectAsync(result, source, reason));

        internal Task OnReceiveAssociationReleaseResponseAsync()
            => ExecuteWithinTransitionLock(() => State.OnReceiveAssociationReleaseResponseAsync());

        internal Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
            => ExecuteWithinTransitionLock(() => State.OnReceiveAbortAsync(source, reason));

        internal Task OnConnectionClosedAsync(Exception exception)
            => ExecuteWithinTransitionLock(() => State.OnConnectionClosedAsync(exception));

        internal async Task<DicomResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
        {
            if (OnCStoreRequest == null)
            {
                return new DicomCStoreResponse(request, DicomStatus.StorageStorageOutOfResources);
            }

            return await OnCStoreRequest(request).ConfigureAwait(false);
        }

        internal async Task<DicomResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request)
        {
            if (OnNEventReportRequest == null)
            {
                return new DicomNEventReportResponse(request, DicomStatus.AttributeListError);
            }

            return await OnNEventReportRequest(request).ConfigureAwait(false);
        }

        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            AsyncInvoked = invoked;
            AsyncPerformed = performed;
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
            => State.AddRequestAsync(dicomRequest);

        public async Task AddRequestsAsync(IEnumerable<DicomRequest> dicomRequests)
        {
            if (dicomRequests == null)
            {
                return;
            }

            var requests = dicomRequests.ToList();

            if (!requests.Any())
            {
                return;
            }

            foreach (var request in requests)
            {
                await State.AddRequestAsync(request).ConfigureAwait(false);
            }
        }

        public async Task AddRequestsAsync(params DicomRequest[] requests)
        {
            if (requests == null)
            {
                return;
            }

            if (!requests.Any())
            {
                return;
            }

            foreach (var request in requests)
            {
                await State.AddRequestAsync(request).ConfigureAwait(false);
            }
        }

        public async Task SendAsync(CancellationToken cancellationToken = default(CancellationToken),
            DicomClientCancellationMode cancellationMode = DicomClientCancellationMode.ImmediatelyReleaseAssociation)
        {
            var cancellation = new DicomClientCancellation(cancellationToken, cancellationMode);
            await State.SendAsync(cancellation).ConfigureAwait(false);
        }
    }
}
