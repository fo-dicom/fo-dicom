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
using FellowOakDicom.Network.Client.EventArguments;

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
        private readonly IAdvancedDicomClient _advancedDicomClient;
        
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
        /// <param name="advancedDicomClient">The advanced DICOM client that will be used to actually send the requests</param>
        public DicomClient(string host, int port, bool useTls, string callingAe, string calledAe,
            DicomClientOptions clientOptions,
            DicomServiceOptions serviceOptions,
            IAdvancedDicomClient advancedDicomClient
        )
        {
            _advancedDicomClient = advancedDicomClient ?? throw new ArgumentNullException(nameof(advancedDicomClient));
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
            var requests = new Queue<DicomRequest>();
            
            while (QueuedRequests.TryDequeue(out var request))
            {
                requests.Enqueue(request.Value);
            }

            var exception = (Exception)null;

            while (requests.Count > 0 && exception == null)
            {
                OpenAssociationRequest openAssociationRequest = new OpenAssociationRequest
                {
                    ConnectionToOpen = new OpenConnectionRequest
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
                    AssociationToOpen = new DicomAssociation
                    {
                        CallingAE = CallingAe,
                        CalledAE = CalledAe,
                        RemoteHost = Host,
                        RemotePort = Port,
                        Options = ServiceOptions,
                        MaxAsyncOpsInvoked = AsyncInvoked,
                        MaxAsyncOpsPerformed = AsyncPerformed,
                    }
                };

                foreach (var request in requests)
                {
                    openAssociationRequest.AssociationToOpen.PresentationContexts.AddFromRequest(request);
                    openAssociationRequest.AssociationToOpen.ExtendedNegotiations.AddFromRequest(request);
                }

                foreach (var context in AdditionalPresentationContexts)
                {
                    openAssociationRequest.AssociationToOpen.PresentationContexts.Add(
                        context.AbstractSyntax,
                        context.UserRole,
                        context.ProviderRole,
                        context.GetTransferSyntaxes().ToArray());
                }

                foreach (var extendedNegotiation in AdditionalExtendedNegotiations)
                {
                    openAssociationRequest.AssociationToOpen.ExtendedNegotiations.AddOrUpdate(
                        extendedNegotiation.SopClassUid,
                        extendedNegotiation.RequestedApplicationInfo,
                        extendedNegotiation.ServiceClassUid,
                        extendedNegotiation.RelatedGeneralSopClasses.ToArray());
                }

                IAdvancedDicomClientAssociation association = null;
                try
                {
                    association = await _advancedDicomClient.OpenAssociationAsync(openAssociationRequest, cancellationToken);

                    var sendTasks = new List<IAsyncEnumerable<DicomResponse>>();

                    // Try to send all tasks immediately, this could work depending on the nr of requests and the async ops invoked setting
                    while(requests.Count > 0)
                    {
                        sendTasks.Add(association.SendRequestAsync(requests.Dequeue(), cancellationToken));
                    }

                    // Wait for all requests to complete
                    foreach (var sendTask in sendTasks)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        await foreach (var response in sendTask.WithCancellation(cancellationToken))
                        {
                            Logger.Debug("Received DICOM response {Status} for request [{RequestMessageId}]", response.Status.State, response.RequestMessageID);
                        }
                    }
                    
                    
                }
                catch (OperationCanceledException)
                {
                    Logger.Warn("DICOM request sending was cancelled");
                }
                finally
                {
                    if (association != null)
                    {
                        // TODO association release timeout?
                        // TODO release / abort depending on cancellation mode
                        await association.DisposeAsync();
                    }
                }
            }
        }
    }
}
