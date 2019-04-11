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
        /// Set negotiation asynchronous operations.
        /// </summary>
        /// <param name="invoked">Asynchronous operations invoked.</param>
        /// <param name="performed">Asynchronous operations performed.</param>
        void NegotiateAsyncOps(int invoked = 0, int performed = 0);

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
        /// <param name="cancellationToken">The cancellation token that can abort the send process if necessary</param>
        Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default);
    }

    public class DicomClient : IDicomClient
    {
        public IDicomClientState State { get; private set; }
        internal ConcurrentQueue<StrongBox<DicomRequest>> QueuedRequests { get; }
        internal int AsyncInvoked { get; private set; }
        internal int AsyncPerformed { get; private set; }

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        public DicomClient()
        {
            QueuedRequests = new ConcurrentQueue<StrongBox<DicomRequest>>();
            AsyncInvoked = 1;
            AsyncPerformed = 1;
            State = new DicomClientIdleState(this);
        }

        internal async Task Transition(IDicomClientState newState, CancellationToken cancellationToken)
        {
            var oldState = State;

            Logger.Info($"Transitioning from '{oldState}' to '{newState}'");

            await oldState.OnExit(cancellationToken);

            if (oldState is IDisposable disposableState) disposableState.Dispose();

            State = newState ?? throw new ArgumentNullException(nameof(newState));

            await newState.OnEnter(cancellationToken).ConfigureAwait(false);
        }

        public Logger Logger { get; set; }

        public DicomServiceOptions Options { get; set; }

        public List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }

        public Encoding FallbackEncoding { get; set; }

        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            AsyncInvoked = invoked;
            AsyncPerformed = performed;
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            State.AddRequest(dicomRequest);
        }

        public Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, CancellationToken cancellationToken = default)
        {
            return State.SendAsync(host, port, useTls, callingAe, calledAe, millisecondsTimeout, cancellationToken);
        }
    }

    public static class ExtensionsForDicomClient
    {
        public static void Send(this IDicomClient dicomClient, string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs)
        {
            try
            {
                dicomClient.SendAsync(host, port, useTls, callingAe, calledAe, millisecondsTimeout, CancellationToken.None).Wait();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }
    }
}
