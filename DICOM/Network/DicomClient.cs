// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dicom.Log;

namespace Dicom.Network
{
    #region DELEGATES

    /// <summary>
    /// Delegate for client handling the C-STORE request immediately.
    /// </summary>
    /// <param name="request">C-STORE request subject to handling.</param>
    /// <returns>Response from handling the C-STORE <paramref name="request"/>.</returns>
    public delegate DicomCStoreResponse CStoreRequestHandler(DicomCStoreRequest request);

    #endregion

    #region EVENT ARGS CLASSES

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

    #endregion

    /// <summary>
    /// General client class for DICOM services.
    /// </summary>
    [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
    public class DicomClient
    {
        #region FIELDS

        private const int DefaultLinger = 50;

        private const int DefaultAssociationTimeout = 5000;

        private const int DefaultReleaseTimeout = 10000;

        private readonly object _lock = new object();

        private readonly AsyncManualResetEvent _hasRequestsFlag;

        private readonly AsyncManualResetEvent _hasAssociationFlag;

        private readonly AsyncManualResetEvent<Exception> _completionFlag;

        private readonly AsyncManualResetEvent _isCleanupStartedFlag;

        private readonly AsyncManualResetEvent _isCleanupFinishedFlag;

        private readonly ConcurrentQueue<StrongBox<DicomRequest>> _requests;

        private DicomServiceUser _service;

        private Task _serviceRunnerTask;

        private int _asyncInvoked;

        private int _asyncPerformed;

        private INetworkStream _networkStream;

        private bool _aborted;

        private Logger _logger;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
        public DicomClient()
        {
            AdditionalPresentationContexts = new List<DicomPresentationContext>();

            _requests = new ConcurrentQueue<StrongBox<DicomRequest>>();
            _asyncInvoked = 1;
            _asyncPerformed = 1;
            Linger = DefaultLinger;

            _hasRequestsFlag = new AsyncManualResetEvent();
            _hasAssociationFlag = new AsyncManualResetEvent();
            _completionFlag = new AsyncManualResetEvent<Exception>();
            _isCleanupStartedFlag = new AsyncManualResetEvent();
            _isCleanupFinishedFlag = new AsyncManualResetEvent();
        }

        #endregion

        #region EVENTS

        /// <summary>
        /// Representation of the DICOM association accepted event.
        /// </summary>
        public event EventHandler<AssociationAcceptedEventArgs> AssociationAccepted = delegate { };

        /// <summary>
        /// Representation of the DICOM association rejected event.
        /// </summary>
        public event EventHandler<AssociationRejectedEventArgs> AssociationRejected = delegate { };

        /// <summary>
        /// Representation of the DICOM association released event.
        /// </summary>
        public event EventHandler AssociationReleased = delegate { };

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the handler of a client C-STORE request.
        /// </summary>
        public CStoreRequestHandler OnCStoreRequest { get; set; }

        /// <summary>
        /// Gets or sets time in milliseconds to keep connection alive for additional requests.
        /// </summary>
        public int Linger { get; set; }

        /// <summary>
        /// Gets or sets the fallback encoding.
        /// </summary>
        public Encoding FallbackEncoding { get; set; }

        /// <summary>
        /// Gets or sets logger that is passed to the underlying <see cref="DicomService"/> implementation.
        /// </summary>
        public Logger Logger
        {
            get
            {
                return _logger ?? (_logger = LogManager.GetLogger("Dicom.Network"));
            }
            set
            {
                _logger = value;
            }
        }

        /// <summary>
        /// Gets or sets options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        public DicomServiceOptions Options { get; set; }

        /// <summary>
        /// Gets or sets additional presentation contexts to negotiate with association.
        /// </summary>
        public List<DicomPresentationContext> AdditionalPresentationContexts { get; set; }

        /// <summary>
        /// Gets whether send functionality is activated or not.
        /// </summary>
        public bool CanSend
        {
            get
            {
                bool canSend;
                lock (_lock)
                {
                    canSend = _requests.Count > 0 || AdditionalPresentationContexts.Count > 0;
                }

                return canSend;
            }
        }

        /// <summary>
        /// Gets whether a new send invocation is required. Send needs to be called if there are requests in queue and client is not connected.
        /// </summary>
        public bool IsSendRequired
        {
            get
            {
                bool hasRequests;
                lock (_lock)
                {
                    hasRequests = _requests.Count > 0;
                }

                return hasRequests && !IsConnected;
            }
        }

        private bool IsConnected
        {
            get
            {
                bool connected;
                lock (_lock)
                {
                    connected = _service != null && _service.IsConnected;
                }

                return connected;
            }
        }

        private bool HasAssociation => _hasAssociationFlag.IsSet;

        #endregion

        #region METHODS

        /// <summary>
        /// Set negotiation asynchronous operations.
        /// </summary>
        /// <param name="invoked">Asynchronous operations invoked.</param>
        /// <param name="performed">Asynchronous operations performed.</param>
        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            _asyncInvoked = invoked;
            _asyncPerformed = performed;
        }

        /// <summary>
        /// Add DICOM service request.
        /// </summary>
        /// <param name="request">DICOM request.</param>
        public void AddRequest(DicomRequest request)
        {
            _requests.Enqueue(new StrongBox<DicomRequest>(request));
            if(!_hasRequestsFlag.IsSet) _hasRequestsFlag.Set();
        }

        /// <summary>
        /// Synchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
        public void Send(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DefaultAssociationTimeout)
        {
            if (!CanSend) return;

            var noDelay = Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = Options?.IgnoreSslPolicyErrors
                                        ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;

            _networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = _asyncInvoked,
                MaxAsyncOpsPerformed = _asyncPerformed,
                RemoteHost = host,
                RemotePort = port
            };

            try
            {
                DoSendAsync(_networkStream, assoc, millisecondsTimeout, true).Wait();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchonously send existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        /// <returns>Awaitable task.</returns>
        [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
        public Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe,
            int millisecondsTimeout = DefaultAssociationTimeout)
        {
            if (!CanSend) Task.FromResult(false); // TODO Replace with Task.CompletedTask when moving to .NET 4.6

            var noDelay = Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = Options?.IgnoreSslPolicyErrors
                                        ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;

            _networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors, millisecondsTimeout);

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = _asyncInvoked,
                MaxAsyncOpsPerformed = _asyncPerformed,
                RemoteHost = host,
                RemotePort = port
            };

            return DoSendAsync(_networkStream, assoc, millisecondsTimeout, true);
        }

        /// <summary>
        /// Synchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="stream">Established network stream.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
        public void Send(INetworkStream stream, string callingAe, string calledAe,
            int millisecondsTimeout = DefaultAssociationTimeout)
        {
            if (!CanSend) return;

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = _asyncInvoked,
                MaxAsyncOpsPerformed = _asyncPerformed,
                RemoteHost = stream.RemoteHost,
                RemotePort = stream.RemotePort
            };

            try
            {
                DoSendAsync(stream, assoc, millisecondsTimeout, false).Wait();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="stream">Established network stream.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds for establishing association.</param>
        /// <returns>Awaitable task.</returns>
        [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
        public Task SendAsync(INetworkStream stream, string callingAe, string calledAe,
            int millisecondsTimeout = DefaultAssociationTimeout)
        {
            if (!CanSend) return Task.FromResult(false); // TODO Replace with Task.CompletedTask when moving to .NET 4.6

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = _asyncInvoked,
                MaxAsyncOpsPerformed = _asyncPerformed,
                RemoteHost = stream.RemoteHost,
                RemotePort = stream.RemotePort
            };

            return DoSendAsync(stream, assoc, millisecondsTimeout, false);
        }

        /// <summary>
        /// Synchronously wait for association.
        /// </summary>
        /// <param name="millisecondsTimeout">Milliseconds to wait for association to occur.</param>
        /// <returns>True if association is established, false otherwise.</returns>
        [Obsolete(
            "Use AssociationAccepted and AssociationRejected events to be notified of association status change.")]
        public bool WaitForAssociation(int millisecondsTimeout = DefaultAssociationTimeout)
        {
            try
            {
                return WaitForAssociationAsync(millisecondsTimeout).GetAwaiter().GetResult();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchronously wait for association.
        /// </summary>
        /// <param name="millisecondsTimeout">Milliseconds to wait for association to occur.</param>
        /// <returns>True if association is established, false otherwise.</returns>
        [Obsolete(
            "Use AssociationAccepted and AssociationRejected events to be notified of association status change.")]
        public async Task<bool> WaitForAssociationAsync(int millisecondsTimeout = DefaultAssociationTimeout)
        {
            if (_hasAssociationFlag.IsSet)
                return true;

            var hasAssociationTask = _hasAssociationFlag.WaitAsync();
            var timeoutTask = Task.Delay(millisecondsTimeout);
            var firstCompletedTask = await Task.WhenAny(hasAssociationTask, timeoutTask).ConfigureAwait(false);
            return firstCompletedTask == hasAssociationTask;
        }

        /// <summary>
        /// Synchronously release association.
        /// </summary>
        [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
        public void Release(int millisecondsTimeout = DefaultReleaseTimeout)
        {
            try
            {
                ReleaseAsync(millisecondsTimeout).GetAwaiter().GetResult();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchronously release association.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use Dicom.Network.Client.DicomClient instead")]
        public async Task ReleaseAsync(int millisecondsTimeout = DefaultReleaseTimeout)
        {
            try
            {
                if (IsConnected)
                {
                    await _service.DoSendAssociationReleaseRequestAsync(millisecondsTimeout).ConfigureAwait(false);
                }
            }
            finally
            {
                await CleanupAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Abort DICOM service connection.
        /// </summary>
        public void Abort()
        {
            try
            {
                AbortAsync().Wait();
            }
            catch (AggregateException e)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchronously abort DICOM service connection.
        /// </summary>
        public async Task AbortAsync()
        {
            if (_aborted) return;

            try
            {
                if (_service != null)
                {
                    await _service.DoSendAbortAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                _aborted = true;
                await CleanupAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Requests an association, sends all the requests from the queue and sends an association release request.
        /// </summary>
        /// <param name="stream">The stream that is used to send the messages</param>
        /// <param name="association"></param>
        /// <param name="millisecondsTimeout">Timeout for Responses on Association Request or Accociation Release</param>
        /// <param name="forceDisconnect">if true, then the network stream is forced to be closed after the association has been released.</param>
        /// <returns></returns>
        private async Task DoSendAsync(INetworkStream stream, DicomAssociation association, int millisecondsTimeout, bool forceDisconnect)
        {
            try
            {
                if (_service?.IsAssociationReleasing == true)
                {
                    Logger.Debug($"Still releasing previous association, waiting for that first");
                    await _completionFlag.WaitAsync().ConfigureAwait(false);
                    Logger.Debug("OK association is released");
                }

                if (_isCleanupStartedFlag.IsSet)
                {
                    Logger.Debug("Still cleaning up previous association / connection, waiting for that first");
                    await _isCleanupFinishedFlag.WaitAsync().ConfigureAwait(false);
                    Logger.Debug("OK previous association / connection is cleaned up");
                }

                if (!IsConnected || _completionFlag.IsSet)
                {
                    _hasAssociationFlag.Reset();
                    _completionFlag.Reset();
                    _isCleanupStartedFlag.Reset();
                    _isCleanupFinishedFlag.Reset();

                    _service = new DicomServiceUser(this, stream, association, Options, FallbackEncoding, Logger);
                    _serviceRunnerTask = _service.RunAsync();
                }

                await Task.WhenAny(_serviceRunnerTask, SendOrReleaseAsync(millisecondsTimeout)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to send due to: {@error}", e);
                _hasAssociationFlag.Reset();
                _completionFlag.Set();

                throw;
            }
            finally
            {
                await CleanupAsync(forceDisconnect).ConfigureAwait(false);
            }
        }

        private async Task SendOrReleaseAsync(int millisecondsTimeout)
        {
#pragma warning disable 618
            var associated = await WaitForAssociationAsync(millisecondsTimeout).ConfigureAwait(false);
#pragma warning restore 618

            bool send;
            lock (_lock)
            {
                send = associated && _requests.Count > 0;
            }

            if (send)
            {
                await SendQueuedRequestsAsync().ConfigureAwait(false);
            }
            else if (associated)
            {
                await _service.DoSendAssociationReleaseRequestAsync(millisecondsTimeout).ConfigureAwait(false);
            }
        }

        private async Task<bool> SendQueuedRequestsAsync()
        {
            if (!_hasRequestsFlag.IsSet)
            {
                await _hasRequestsFlag.WaitAsync().ConfigureAwait(false);
            }

            bool requestsWereSent = false;

            while (IsConnected && HasAssociation && !_service.IsAssociationReleasing && _requests.TryDequeue(out var request))
            {
                await _service.SendRequestAsync(request.Value).ConfigureAwait(false);

                request.Value = null;
                requestsWereSent = true;
            }

            if (_requests.IsEmpty)
            {
                _hasRequestsFlag.Reset();
            }

            return requestsWereSent;
        }

        private async Task CleanupAsync(bool force)
        {
            if(!_isCleanupStartedFlag.IsSet) _isCleanupStartedFlag.Set();

            var completionTask = _completionFlag.WaitAsync();

            while(IsConnected)
            {
                await Task.WhenAny(completionTask, Task.Delay(1000)).ConfigureAwait(false);

                if (completionTask.IsCompleted || completionTask.IsFaulted || completionTask.IsCanceled)
                    break;

                Logger.Warn("Waited 1 second to cleanup but completion flag is still not set. Trying to flush next message");

                await _service.SendNextMessageAsync().ConfigureAwait(false);
            }

            var completedException = completionTask.IsCompleted ? completionTask.Result : null;

            if (completedException != null || force)
            {
                var reason = force ? "'force' is true" : $"an exception caused the completion flag to be set: {completedException}";
                Logger.Warn($"Disposing network stream because {reason}");

                if (_networkStream != null)
                {
                    try
                    {
                        _networkStream.Dispose();
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("Failed to dispose network stream, reason: {@error}", e);
                    }

                    _networkStream = null;
                }

                if (_service != null)
                {
                    try
                    {
                        _service.Dispose();
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("Failed to dispose service, reason: {@error}", e);
                    }

                    _service = null;
                }

                // Wait until listener realizes connection is gone. If DicomServiceUser's constructor threw, _serviceRunnerTask can be null
                if (_serviceRunnerTask != null)
                {
                    await _serviceRunnerTask.ConfigureAwait(false);
                }
            }

            if (_hasAssociationFlag.IsSet) _hasAssociationFlag.Reset();

            if (!_isCleanupFinishedFlag.IsSet) _isCleanupFinishedFlag.Set();

            if (completedException != null)
            {
                throw completedException;
            }
        }

        #endregion

        #region INNER TYPES

        private class DicomServiceUser : DicomService, IDicomServiceUser
        {
            #region FIELDS

            private readonly DicomClient _client;

            private readonly DicomAssociation _association;

            private bool _isInitialized;

            private int _releaseRequested;

            #endregion

            #region PROPERTIES

            public bool IsAssociationReleasing => _releaseRequested == 1;

            #endregion

            #region CONSTRUCTORS

            internal DicomServiceUser(
                DicomClient client,
                INetworkStream stream,
                DicomAssociation association,
                DicomServiceOptions options,
                Encoding fallbackEncoding,
                Logger log)
                : base(stream, fallbackEncoding, log)
            {
                _client = client;

                if (options != null)
                {
                    Options = options;
                }

                List<DicomRequest> requests;
                lock (_client._lock)
                {
                    requests = _client._requests.Select(s => s.Value).ToList();
                }

                foreach (var request in requests)
                {
                    association.PresentationContexts.AddFromRequest(request);
                }

                foreach (var context in client.AdditionalPresentationContexts)
                {
                    association.PresentationContexts.Add(
                        context.AbstractSyntax,
                        context.UserRole,
                        context.ProviderRole,
                        context.GetTransferSyntaxes().ToArray());
                }

                _association = association;
                _isInitialized = false;
                _releaseRequested = 0;
            }

            #endregion

            #region METHODS

            /// <inheritdoc />
            public void OnReceiveAssociationAccept(DicomAssociation association)
            {
                foreach (var ctx in _client.AdditionalPresentationContexts)
                {
                    foreach (var item in
                        association.PresentationContexts.Where(pc => pc.AbstractSyntax == ctx.AbstractSyntax))
                    {
                        ctx.SetResult(item.Result, item.AcceptedTransferSyntax);
                    }
                }

                SetHasAssociationFlag(true);
                _client.AssociationAccepted(_client, new AssociationAcceptedEventArgs(association));
            }

            /// <inheritdoc />
            public void OnReceiveAssociationReject(
                DicomRejectResult result,
                DicomRejectSource source,
                DicomRejectReason reason)
            {
                SetHasAssociationFlag(false);
                _client.AssociationRejected(_client, new AssociationRejectedEventArgs(result, source, reason));

                SetCompletionFlag(new DicomAssociationRejectedException(result, source, reason));
            }

            /// <inheritdoc />
            public void OnReceiveAssociationReleaseResponse()
            {
                SetCompletionFlag();

                _client.AssociationReleased(_client, EventArgs.Empty);
            }

            /// <inheritdoc />
            public override Task RunAsync()
            {
                if (_isInitialized) return Task.FromResult(false); // TODO Replace with Task.CompletedTask when moving to .NET 4.6
                _isInitialized = true;

                return Task.WhenAll(base.RunAsync(),
                    SendAssociationRequestAsync(_association));
            }

            /// <inheritdoc />
            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
                SetCompletionFlag(new DicomAssociationAbortedException(source, reason));
            }

            /// <inheritdoc />
            public void OnConnectionClosed(Exception exception)
            {
                SetCompletionFlag(exception);
            }

            /// <inheritdoc />
            public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
            {
                return _client.OnCStoreRequest == null
                           ? new DicomCStoreResponse(request, DicomStatus.StorageStorageOutOfResources)
                           : _client.OnCStoreRequest(request);
            }

            internal async Task DoSendAssociationReleaseRequestAsync(int millisecondsTimeout)
            {
                try
                {
                    if (!IsConnected)
                    {
                        Logger.Warn($"Tried to release association but we're no longer connected");
                        return;
                    }

                    if (Interlocked.Exchange(ref _releaseRequested, 1) != 0)
                    {
                        Logger.Warn("Tried to release association from multiple threads in parallel, this thread lost the race and won't try to release the association");
                        return;
                    }

                    var sendAssociationReleaseRequestTask = SendAssociationReleaseRequestAsync();
                    var sendAssociationReleaseRequestTimeoutTask = Task.Delay(millisecondsTimeout);
                    var waitUntilDisconnectionTask = _isDisconnectedFlag.WaitAsync();

                    var firstCompletedTask = await Task.WhenAny(
                        sendAssociationReleaseRequestTask,
                        waitUntilDisconnectionTask,
                        sendAssociationReleaseRequestTimeoutTask
                    ).ConfigureAwait(false);

                    if (firstCompletedTask == sendAssociationReleaseRequestTimeoutTask)
                    {
                        Logger.Debug($"Timeout while trying to release the association");
                        SetCompletionFlag();
                    }
                    else if (firstCompletedTask == waitUntilDisconnectionTask)
                    {
                        Logger.Debug($"Disconnected while trying to release the association");
                        SetCompletionFlag();
                    }
                    else if (firstCompletedTask == sendAssociationReleaseRequestTask)
                    {
                        Logger.Debug("Association release request was sent successfully");
                    }
                }
                catch (Exception e)
                {
                    _releaseRequested = 0;
                    Logger.Warn("Attempt to send association release request failed due to: {@error}", e);
                    SetCompletionFlag(e);
                }
            }

            internal async Task DoSendAbortAsync()
            {
                if (IsConnected)
                {
                    await SendAbortAsync(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified)
                        .ConfigureAwait(false);
                }

                SetCompletionFlag();
            }

            /// <inheritdoc />
            protected override async Task OnSendQueueEmptyAsync()
            {
                var lingerCancellationTokenSource = new CancellationTokenSource();

                void CancelLingering(string reason)
                {
                    if (!lingerCancellationTokenSource.IsCancellationRequested)
                    {
                        Logger.Debug($"Won't linger association because {reason}");
                        lingerCancellationTokenSource.Cancel();
                    }
                }


                var newRequestsComeIn = _client._hasRequestsFlag.WaitAsync()
                    .ContinueWith(_ => CancelLingering("another DICOM request was just added"),
                        TaskContinuationOptions.OnlyOnRanToCompletion);
                var nextRequestsAreSent = _client.SendQueuedRequestsAsync()
                    .ContinueWith(requestsWereSent =>
                    {
                        if(requestsWereSent.Result) CancelLingering("another DICOM request was just sent");
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                var lingerAssociation = Task.Delay(_client.Linger, lingerCancellationTokenSource.Token)
                    .ContinueWith(async _ => await LingerAsync(DefaultReleaseTimeout), TaskContinuationOptions.OnlyOnRanToCompletion);

                await Task.WhenAny(newRequestsComeIn, nextRequestsAreSent, lingerAssociation)
                    .ConfigureAwait(false);
            }

            private async Task LingerAsync(int millisecondsTimeout)
            {
                if (!IsConnected)
                {
                    Logger.Debug($"Not releasing association after linger because the connection is already gone");
                    return;
                }

                if (!_client._requests.IsEmpty)
                {
                    Logger.Debug($"Not releasing association after linger because at least one request is waiting to be processed");
                    return;
                }

                if (!IsSendQueueEmpty)
                {
                    Logger.Debug($"Not releasing association because at least one request is being processed");
                    return;
                }

                if (IsAssociationReleasing)
                {
                    Logger.Debug($"Not releasing association after linger because something else already triggered an association release request");
                    return;
                }

                if (!_client.HasAssociation)
                {
                    Logger.Debug($"Not releasing association after linger because there is no active association anymore");
                    return;
                }

                Logger.Info($"Automatically releasing association after linger timeout of {_client.Linger}ms");

                await DoSendAssociationReleaseRequestAsync(millisecondsTimeout).ConfigureAwait(false);
            }

            private void SetHasAssociationFlag(bool isAssociated)
            {
                if (isAssociated)
                {
                    if(!_client._hasAssociationFlag.IsSet)
                        _client._hasAssociationFlag.Set();
                }
                else
                {
                    _client._hasAssociationFlag.Reset();
                }
            }

            private void SetCompletionFlag(Exception exception = null)
            {
                Interlocked.Exchange(ref _releaseRequested, 0);

                if (_client._completionFlag.IsSet)
                    return;
                _client._completionFlag.Set(exception);
            }

            #endregion
        }

        #endregion
    }
}

#endif
