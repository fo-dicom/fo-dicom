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
        /// Gets or sets the timeout (in ms) to wait for an association response after sending an association request
        /// </summary>
        int AssociationRequestTimeoutInMs { get; }

        /// <summary>
        /// Gets or sets the timeout (in ms) to wait for an association release response after sending an association release request
        /// </summary>
        int AssociationReleaseTimeoutInMs { get; }

        /// <summary>
        /// Gets or sets the timeout (in ms) that associations need to be held open after all requests have been processed
        /// </summary>
        int AssociationLingerTimeoutInMs { get; }

        /// <summary>
        /// Representation of the DICOM association accepted event.
        /// </summary>
        event EventHandler<AssociationAcceptedEventArgs> AssociationAccepted;

        /// <summary>
        /// Representation of the DICOM association rejected event.
        /// </summary>
        event EventHandler<AssociationRejectedEventArgs> AssociationRejected;

        /// <summary>
        /// Representation of the DICOM association released event.
        /// </summary>
        event EventHandler AssociationReleased;

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
        /// <param name="cancellationToken">The cancellation token that can abort the send process if necessary</param>
        Task SendAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Container class for arguments associated with the <see cref="DicomClient.AssociationAccepted"/> event.
    /// </summary>
    public class AssociationAcceptedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AssociationAcceptedEventArgs"/> class.
        /// </summary>
        /// <param name="association">Accepted association.</param>
        public AssociationAcceptedEventArgs(DicomAssociation association)
        {
            Association = association;
        }

        /// <summary>
        /// Gets the accepted association.
        /// </summary>
        public DicomAssociation Association { get; }
    }

    /// <summary>
    /// Container class for arguments associated with the <see cref="DicomClient.AssociationRejected"/> event.
    /// </summary>
    public class AssociationRejectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AssociationRejectedEventArgs"/> class.
        /// </summary>
        /// <param name="result">Association rejection result.</param>
        /// <param name="source">Source of association rejection.</param>
        /// <param name="reason">Reason for association rejection.</param>
        public AssociationRejectedEventArgs(DicomRejectResult result, DicomRejectSource source,
            DicomRejectReason reason)
        {
            Result = result;
            Source = source;
            Reason = reason;
        }

        /// <summary>
        /// Gets the association rejection result.
        /// </summary>
        public DicomRejectResult Result { get; }

        /// <summary>
        /// Gets the source of the association rejection.
        /// </summary>
        public DicomRejectSource Source { get; }

        /// <summary>
        /// Gets the reason for the association rejection.
        /// </summary>
        public DicomRejectReason Reason { get; }
    }

    public class DicomClient : IDicomClient
    {
        public string Host { get; }
        public int Port { get; }
        public bool UseTls { get; }
        public string CallingAe { get; }
        public string CalledAe { get; }
        public int AssociationRequestTimeoutInMs { get; set; }
        public int AssociationReleaseTimeoutInMs { get; set; }
        public int AssociationLingerTimeoutInMs { get; set; }

        internal IDicomClientState State { get; private set; }

        internal ConcurrentQueue<StrongBox<DicomRequest>> QueuedRequests { get; }

        internal int AsyncInvoked { get; private set; }

        internal int AsyncPerformed { get; private set; }

        public bool IsSendRequired => State is DicomClientIdleState && QueuedRequests.Any();

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="associationRequestTimeoutInMs">Timeout in milliseconds for establishing association.</param>
        /// <param name="associationReleaseTimeoutInMs">Timeout in milliseconds to break off association</param>
        /// <param name="associationLingerTimeoutInMs">Timeout in milliseconds to keep open association after all requests have been processed.</param>
        public DicomClient(string host, int port, bool useTls, string callingAe, string calledAe,
            int associationRequestTimeoutInMs = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs,
            int associationReleaseTimeoutInMs = DicomClientDefaults.DefaultAssociationReleaseTimeoutInMs,
            int associationLingerTimeoutInMs = DicomClientDefaults.DefaultAssociationLingerInMs)
        {
            Host = host;
            Port = port;
            UseTls = useTls;
            CallingAe = callingAe;
            CalledAe = calledAe;
            AssociationRequestTimeoutInMs = associationRequestTimeoutInMs;
            AssociationReleaseTimeoutInMs = associationReleaseTimeoutInMs;
            AssociationLingerTimeoutInMs = associationLingerTimeoutInMs;
            QueuedRequests = new ConcurrentQueue<StrongBox<DicomRequest>>();
            AdditionalPresentationContexts = new List<DicomPresentationContext>();
            AsyncInvoked = 1;
            AsyncPerformed = 1;
            State = new DicomClientIdleState(this);
        }

        internal async Task Transition(IDicomClientState newState, CancellationToken cancellationToken)
        {
            var oldState = State;

            Logger.Info($"DICOM client transition: '{oldState}' --> '{newState}'");

            await oldState.OnExit(cancellationToken).ConfigureAwait(false);

            if (oldState is IDisposable disposableState) disposableState.Dispose();

            State = newState ?? throw new ArgumentNullException(nameof(newState));

            await newState.OnEnter(cancellationToken).ConfigureAwait(false);
        }

        internal void NotifyAssociationAccepted(AssociationAcceptedEventArgs eventArgs)
        {
            AssociationAccepted?.Invoke(this, eventArgs);
        }

        internal void NotifyAssociationRejected(AssociationRejectedEventArgs eventArgs)
        {
            AssociationRejected?.Invoke(this, eventArgs);
        }

        internal void NotifyAssociationReleased()
        {
            AssociationReleased?.Invoke(this, EventArgs.Empty);
        }

        public Logger Logger { get; set; }

        public DicomServiceOptions Options { get; set; }

        public List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }

        public Encoding FallbackEncoding { get; set; }

        public event EventHandler<AssociationAcceptedEventArgs> AssociationAccepted;
        public event EventHandler<AssociationRejectedEventArgs> AssociationRejected;
        public event EventHandler AssociationReleased;

        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            AsyncInvoked = invoked;
            AsyncPerformed = performed;
        }

        public void AddRequest(DicomRequest dicomRequest)
        {
            State.AddRequest(dicomRequest);
        }

        public async Task SendAsync(CancellationToken cancellationToken = default)
        {
            await State.SendAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public static class ExtensionsForDicomClient
    {
        [Obsolete]
        public static void Send(this DicomClient dicomClient)
        {
            try
            {
                dicomClient.SendAsync(CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }
        [Obsolete]
        public static bool WaitForAssociation(this DicomClient dicomClient, int timeoutInMs = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs)
        {
            try
            {
                return dicomClient.WaitForAssociationAsync(timeoutInMs).GetAwaiter().GetResult();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }

        [Obsolete]
        public static Task<bool> WaitForAssociationAsync(this DicomClient dicomClient, int timeoutInMs = DicomClientDefaults.DefaultAssociationRequestTimeoutInMs)
        {
            if (dicomClient.State is DicomClientWithAssociationState)
                return Task.FromResult(true);

            var cancellationTokenSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<bool>();

            void OnAssociationAccepted(object sender, AssociationAcceptedEventArgs eventArgs)
            {
                dicomClient.AssociationAccepted -= OnAssociationAccepted;

                cancellationTokenSource.Cancel();
                taskCompletionSource.TrySetResult(true);
            }

            dicomClient.AssociationAccepted += OnAssociationAccepted;

            Task.Delay(timeoutInMs, cancellationTokenSource.Token)
                .ContinueWith(_ =>
                {
                    dicomClient.AssociationAccepted -= OnAssociationAccepted;
                    taskCompletionSource.TrySetResult(false);

                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            return taskCompletionSource.Task;
        }

        [Obsolete("Use cancellation token instead")]
        public static void Abort(this DicomClient dicomClient)
        {
            if (dicomClient.State is DicomClientWithConnectionState dicomClientWithConnectionState)
            {
                var abortParameters = new DicomClientAbortState.InitialisationParameters(dicomClientWithConnectionState.Connection);
                var abortState = new DicomClientAbortState(dicomClient, abortParameters);
                dicomClient.Transition(abortState, CancellationToken.None).GetAwaiter().GetResult();
            }
        }

        [Obsolete("Use cancellation token instead")]
        public static async Task ReleaseAsync(this DicomClient dicomClient)
        {
            if (dicomClient.State is DicomClientWithAssociationState dicomClientWithAssociationState)
            {
                var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(
                    dicomClientWithAssociationState.Association, dicomClientWithAssociationState.Connection);
                var releaseAssociationState = new DicomClientReleaseAssociationState(dicomClient, releaseAssociationParameters);

                await dicomClient.Transition(releaseAssociationState, CancellationToken.None);
            }
        }

        [Obsolete("Use cancellation token instead")]
        public static void Release(this DicomClient dicomClient)
        {
            if (dicomClient.State is DicomClientWithAssociationState dicomClientWithAssociationState)
            {
                var releaseAssociationParameters = new DicomClientReleaseAssociationState.InitialisationParameters(
                    dicomClientWithAssociationState.Association, dicomClientWithAssociationState.Connection);
                var releaseAssociationState = new DicomClientReleaseAssociationState(dicomClient, releaseAssociationParameters);

                dicomClient.Transition(releaseAssociationState, CancellationToken.None).GetAwaiter().GetResult();
            }
        }
    }
}
