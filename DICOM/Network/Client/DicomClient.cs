using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Log;

namespace Dicom.Network.Client
{
    public interface IDicomClient
    {
        /// <summary>
        /// Gets or sets the logger that will be used by this DicomClient
        /// </summary>
        Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        DicomServiceOptions Options { get; set; }

        /// <summary>
        /// Gets or sets additional presentation contexts to negotiate with association.
        /// </summary>
        List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }

        /// <summary>
        /// Gets or sets the fallback encoding.
        /// </summary>
        Encoding FallbackEncoding { get; set; }

        /// <summary>
        /// Gets the state of this DICOM client
        /// </summary>
        IDicomClientState State { get; }

        /// <summary>
        /// Gets the DICOM requests that are queued for execution
        /// </summary>
        IEnumerable<DicomRequest> QueuedRequests { get; }

        /// <summary>
        /// Enqueues a new DICOM request for execution.
        /// </summary>
        /// <param name="dicomRequest">The DICOM request to send</param>
        void AddRequest(DicomRequest dicomRequest);

        /// <summary>
        /// Sends existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default);
    }

    public class DicomClient : IDicomClient
    {
        private readonly object _lock = new object();
        private readonly ConcurrentQueue<StrongBox<DicomRequest>> _queuedRequests;

        public IDicomClientState State { get; private set; }
        public IEnumerable<DicomRequest> QueuedRequests => _queuedRequests.Select(r => r.Value).ToList();

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        public DicomClient()
        {
            _queuedRequests = new ConcurrentQueue<StrongBox<DicomRequest>>();
            State = new DicomClientIdleState(this);
        }

        internal async Task Transition<TTransitionParameters>(
            IDicomClientState<TTransitionParameters> newState,
            TTransitionParameters transitionParameters,
            CancellationToken cancellationToken)
        {
            var oldState = State;

            State = newState ?? throw new ArgumentNullException(nameof(newState));

            await newState.OnTransition(transitionParameters, cancellationToken);

            if (oldState is IDisposable disposableState) disposableState.Dispose();
        }

        public Logger Logger { get; set; }

        public DicomServiceOptions Options { get; set; }

        public List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }

        public Encoding FallbackEncoding { get; set; }

        public void AddRequest(DicomRequest dicomRequest)
        {
            _queuedRequests.Enqueue(new StrongBox<DicomRequest>(dicomRequest));
        }

        public Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            return State.SendAsync(host, port, useTls, callingAe, calledAe, millisecondsTimeout, cancellationToken);
        }
    }
}
