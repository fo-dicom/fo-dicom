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
using FellowOakDicom.Network.Client.Advanced;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
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
        private readonly IAdvancedDicomClientFactory _advancedDicomClientFactory;
        
        internal ConcurrentQueue<StrongBox<DicomRequest>> QueuedRequests { get; }
        
        internal int AsyncInvoked { get; private set; }
        internal int AsyncPerformed { get; private set; }

        public string Host { get; }
        public int Port { get; }
        public bool UseTls { get; }
        public string CallingAe { get; }
        public string CalledAe { get; }
        
        public bool IsSendRequired => QueuedRequests.Any();
        
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
        /// <param name="advancedDicomClientFactory">The advanced DICOM client factory that will be used to actually send the requests</param>
        public DicomClient(string host, int port, bool useTls, string callingAe, string calledAe,
            DicomClientOptions clientOptions,
            DicomServiceOptions serviceOptions,
            IAdvancedDicomClientFactory advancedDicomClientFactory
        )
        {
            _advancedDicomClientFactory = advancedDicomClientFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientFactory));
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
        }

        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            AsyncInvoked = invoked;
            AsyncPerformed = performed;
        }

        public Task AddRequestAsync(DicomRequest dicomRequest)
        {
            QueuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
            
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

            return Task.CompletedTask;
        }

        public async Task SendAsync(CancellationToken cancellationToken = default,
            DicomClientCancellationMode cancellationMode = DicomClientCancellationMode.ImmediatelyReleaseAssociation)
        {
            var advancedDicomClient = _advancedDicomClientFactory.Create(new AdvancedDicomClientCreationRequest
            {
                Logger = Logger
            });
            var exception = (Exception)null;
            var maximumNumberOfRequestsPerAssociation = ClientOptions.MaximumNumberOfRequestsPerAssociation ?? int.MaxValue;

            while (!QueuedRequests.IsEmpty && exception == null)
            {
                var requests = new Queue<DicomRequest>();
                var numberOfRequests = 0;
            
                while (numberOfRequests < maximumNumberOfRequestsPerAssociation 
                       && QueuedRequests.TryDequeue(out var request))
                {
                    requests.Enqueue(request.Value);
                    numberOfRequests++;
                }
                
                AdvancedDicomClientAssociationRequest advancedDicomClientAssociationRequest = new AdvancedDicomClientAssociationRequest
                {
                    Connection = new AdvancedDicomClientConnectionRequest
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
                        }
                    },
                    CallingAE = CallingAe,
                    CalledAE = CalledAe,
                    MaxAsyncOpsInvoked = AsyncInvoked,
                    MaxAsyncOpsPerformed = AsyncPerformed,
                };

                foreach (var request in requests)
                {
                    advancedDicomClientAssociationRequest.PresentationContexts.AddFromRequest(request);
                    advancedDicomClientAssociationRequest.ExtendedNegotiations.AddFromRequest(request);
                }

                foreach (var context in AdditionalPresentationContexts)
                {
                    advancedDicomClientAssociationRequest.PresentationContexts.Add(
                        context.AbstractSyntax,
                        context.UserRole,
                        context.ProviderRole,
                        context.GetTransferSyntaxes().ToArray());
                }

                foreach (var extendedNegotiation in AdditionalExtendedNegotiations)
                {
                    advancedDicomClientAssociationRequest.ExtendedNegotiations.AddOrUpdate(
                        extendedNegotiation.SopClassUid,
                        extendedNegotiation.RequestedApplicationInfo,
                        extendedNegotiation.ServiceClassUid,
                        extendedNegotiation.RelatedGeneralSopClasses.ToArray());
                }

                IAdvancedDicomClientAssociation association = null;
                try
                {
                    // TODO make a separate abstraction for connection, so we can reliably emit a backwards compatible state change from connect -> request association
                    association = await advancedDicomClient.OpenAssociationAsync(advancedDicomClientAssociationRequest, cancellationToken);
                    while (requests.Count > 0 && exception == null)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var sendTasks = new List<IAsyncEnumerable<DicomResponse>>();

                        // Try to send all tasks immediately, this could work depending on the nr of requests and the async ops invoked setting
                        while (requests.Count > 0)
                        {
                            sendTasks.Add(association.SendRequestAsync(requests.Dequeue(), cancellationToken));
                        }

                        // Wait for all requests to complete
                        foreach (var sendTask in sendTasks)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            await foreach (var response in sendTask.WithCancellation(cancellationToken))
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                Logger.Debug("Received DICOM response {Status} for request [{RequestMessageId}]", response.Status.State, response.RequestMessageID);
                            }
                        }

                        // Linger behavior: if the queue is empty, wait for a bit before closing the association
                        if (exception != null
                            && numberOfRequests < maximumNumberOfRequestsPerAssociation
                            && QueuedRequests.IsEmpty
                            && ClientOptions.AssociationLingerTimeoutInMs > 0)
                        {
                            Logger.Debug($"Lingering on open association for {ClientOptions.AssociationLingerTimeoutInMs}ms");

                            await Task.Delay(ClientOptions.AssociationLingerTimeoutInMs, cancellationToken);
                        }

                        // If more requests were queued since we started, try to send those too over the same association
                        while (numberOfRequests < maximumNumberOfRequestsPerAssociation
                               && QueuedRequests.TryDequeue(out var request))
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            requests.Enqueue(request.Value);

                            numberOfRequests++;
                        }
                    }
                }
                catch (OperationCanceledException e)
                {
                    exception = e;

                    Logger.Warn("DICOM request sending was cancelled");

                    switch (cancellationMode)
                    {
                        case DicomClientCancellationMode.ImmediatelyReleaseAssociation:
                            if (association != null)
                            {
                                await association.ReleaseAsync(cancellationToken);

                                association = null;
                            }

                            break;
                        case DicomClientCancellationMode.ImmediatelyAbortAssociation:
                            if (association != null)
                            {
                                await association.AbortAsync(cancellationToken);

                                association = null;
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(cancellationMode), cancellationMode, null);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("An error occurred while sending DICOM requests: {Error}", e);

                    exception = e;
                }
                finally
                {
                    if (association != null)
                    {
                        await association.DisposeAsync();
                    }
                }
            }
        }
    }
}
