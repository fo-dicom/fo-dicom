// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Dicom.Log;

    /// <summary>
    /// General client class for DICOM services.
    /// </summary>
    public class DicomClient
    {
        #region FIELDS

        private readonly object locker = new object();

        private TaskCompletionSource<bool> completeNotifier;

        private TaskCompletionSource<bool> associateNotifier;

        private readonly List<DicomRequest> requests;

        private DicomServiceUser service;

        private int asyncInvoked;

        private int asyncPerformed;

        private INetworkStream networkStream;

        private bool aborted;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        public DicomClient()
        {
            this.requests = new List<DicomRequest>();
            this.AdditionalPresentationContexts = new List<DicomPresentationContext>();
            this.asyncInvoked = 1;
            this.asyncPerformed = 1;
            this.Linger = 50;
        }

        #endregion

        #region DELEGATES

        /// <summary>
        /// Delegate for client handling the C-STORE request immediately.
        /// </summary>
        /// <param name="request">C-STORE request subject to handling.</param>
        /// <returns>Response from handling the C-STORE <paramref name="request"/>.</returns>
        public delegate DicomCStoreResponse CStoreRequestHandler(DicomCStoreRequest request);

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
        public Logger Logger { get; set; }

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
                int requestsCount;
                lock (this.locker)
                {
                    requestsCount = this.requests.Count;
                }

                return requestsCount > 0 || this.AdditionalPresentationContexts.Count > 0;
            }
        }

        /// <summary>
        /// Gets whether a new send invocation is required. Send needs to be called if there are requests in queue and client is not connected.
        /// </summary>
        public bool IsSendRequired
        {
            get
            {
                bool required;
                lock (this.locker)
                {
                    required = this.requests.Count > 0 && (this.service == null || !this.service.IsConnected);
                }

                return required;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Set negotiation asynchronous operations.
        /// </summary>
        /// <param name="invoked">Asynchronous operations invoked.</param>
        /// <param name="performed">Asynchronous operations performed.</param>
        public void NegotiateAsyncOps(int invoked = 0, int performed = 0)
        {
            this.asyncInvoked = invoked;
            this.asyncPerformed = performed;
        }

        /// <summary>
        /// Add DICOM service request.
        /// </summary>
        /// <param name="request">DICOM request.</param>
        public void AddRequest(DicomRequest request)
        {
            lock (this.locker)
            {
                this.requests.Add(request);
            }
        }

        /// <summary>
        /// Synchonously send existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public void Send(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            if (!CanSend) return;

            var noDelay = Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = Options?.IgnoreSslPolicyErrors
                                        ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;

            this.networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = host,
                RemotePort = port
            };

            try
            {
                DoSendAsync(this.networkStream, assoc).Wait();
            }
            catch (AggregateException e)
            {
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
        /// <returns>Awaitable task.</returns>
        public Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            if (!CanSend) Task.FromResult(false);   // TODO Replace with Task.CompletedTask when moving to .NET 4.6

            var noDelay = Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = Options?.IgnoreSslPolicyErrors
                                        ?? DicomServiceOptions.Default.IgnoreSslPolicyErrors;

            this.networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = host,
                RemotePort = port
            };

            return DoSendAsync(this.networkStream, assoc);
        }

        /// <summary>
        /// Synchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="stream">Established network stream.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public void Send(INetworkStream stream, string callingAe, string calledAe)
        {
            if (!CanSend) return;

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = stream.RemoteHost,
                RemotePort = stream.RemotePort
            };

            try
            {
                DoSendAsync(stream, assoc).Wait();
            }
            catch (AggregateException e)
            {
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="stream">Established network stream.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <returns>Awaitable task.</returns>
        public Task SendAsync(INetworkStream stream, string callingAe, string calledAe)
        {
            if (!CanSend) return Task.FromResult(false);   // TODO Replace with Task.CompletedTask when moving to .NET 4.6

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = stream.RemoteHost,
                RemotePort = stream.RemotePort
            };

            return DoSendAsync(stream, assoc);
        }

        /// <summary>
        /// Synchronously wait for association.
        /// </summary>
        /// <param name="millisecondsTimeout">Milliseconds to wait for association to occur.</param>
        /// <returns>True if association is established, false otherwise.</returns>
        public bool WaitForAssociation(int millisecondsTimeout = 5000)
        {
            try
            {
                return WaitForAssociationAsync(millisecondsTimeout).Result;
            }
            catch (AggregateException e)
            {
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchronously wait for association.
        /// </summary>
        /// <param name="millisecondsTimeout">Milliseconds to wait for association to occur.</param>
        /// <returns>True if association is established, false otherwise.</returns>
        public async Task<bool> WaitForAssociationAsync(int millisecondsTimeout = 5000)
        {
            try
            {
                using (var cancellationSource = new CancellationTokenSource(millisecondsTimeout))
                {
                    while (true)
                    {
                        if (this.associateNotifier != null && this.associateNotifier.Task.IsCompleted)
                        {
                            return this.associateNotifier.Task.Status == TaskStatus.RanToCompletion
                                   && this.associateNotifier.Task.Result;
                        }

                        await Task.Delay(50, cancellationSource.Token).ConfigureAwait(false);
                    }
                }
            }
            catch (TaskCanceledException)
            {
            }

            return false;
        }

        /// <summary>
        /// Synchronously release association.
        /// </summary>
        public void Release(int millisecondsTimeout = 10000)
        {
            try
            {
                ReleaseAsync(millisecondsTimeout).Wait();
            }
            catch (AggregateException e)
            {
                throw e.Flatten().InnerException;
            }
        }

        /// <summary>
        /// Asynchronously release association.
        /// </summary>
        /// <returns></returns>
        public async Task ReleaseAsync(int millisecondsTimeout = 10000)
        {
            try
            {
                if (this.service != null)
                {
                    await this.service.DoSendAssociationReleaseRequestAsync(millisecondsTimeout).ConfigureAwait(false);
                }
            }
            finally
            {
                Cleanup();
            }
        }

        /// <summary>
        /// Abort DICOM service connection.
        /// </summary>
        public void Abort()
        {
            if (this.aborted) return;

            this.service?.DoSendAbort();
            this.aborted = true;

            Cleanup();
        }

        private async Task DoSendAsync(INetworkStream stream, DicomAssociation association)
        {
            try
            {
                while (this.service != null)
                {
                    await Task.Delay(50).ConfigureAwait(false);
                }

                this.associateNotifier = new TaskCompletionSource<bool>();
                this.completeNotifier = new TaskCompletionSource<bool>();

                this.service = new DicomServiceUser(this, stream, association, Options, FallbackEncoding, Logger);

                await this.completeNotifier.Task.ConfigureAwait(false);
            }
            finally
            {
                Cleanup();
            }
        }

        private void Cleanup()
        {
            // If not already set, set notifiers here to signal competion to awaiters
            this.associateNotifier?.TrySetResult(false);
            this.completeNotifier?.TrySetResult(true);

            if (this.networkStream != null)
            {
                try
                {
                    this.networkStream.Dispose();
                }
                catch (Exception e)
                {
                    Logger.Warn("Failed to dispose network stream, reason: {@error}", e);
                }
            }

            var lingerException = this.service?.LingerTask?.Exception;

            this.service = null;
            this.networkStream = null;

            if (lingerException != null)
            {
                throw lingerException.Flatten().InnerException;
            }
        }

        #endregion

        #region INNER TYPES

        private class DicomServiceUser : DicomService, IDicomServiceUser
        {
            #region FIELDS

            private const int ReleaseTimeout = 2500;

            private readonly DicomClient client;

            private readonly object locker = new object();

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
                this.client = client;

                if (options != null)
                {
                    Options = options;
                }

                List<DicomRequest> requests;
                lock (this.client.locker)
                {
                    requests = new List<DicomRequest>(this.client.requests);
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

                SendAssociationRequest(association);
            }

            #endregion

            #region PROPERTIES

            internal Task LingerTask { get; private set; }

            #endregion

            #region METHODS

            /// <summary>
            /// Callback for handling association accept scenarios.
            /// </summary>
            /// <param name="association">Accepted association.</param>
            public void OnReceiveAssociationAccept(DicomAssociation association)
            {
                foreach (var ctx in this.client.AdditionalPresentationContexts)
                {
                    foreach (var item in
                        association.PresentationContexts.Where(pc => pc.AbstractSyntax == ctx.AbstractSyntax))
                    {
                        ctx.SetResult(item.Result, item.AcceptedTransferSyntax);
                    }
                }

                this.client.associateNotifier.TrySetResult(true);

                List<DicomRequest> requests;
                lock (this.client.locker)
                {
                    requests = new List<DicomRequest>(this.client.requests);
                    this.client.requests.Clear();
                }

                if (requests.Count == 0)
                {
                    DoSendAssociationReleaseRequestAsync(ReleaseTimeout).Wait();
                }
                else
                {
                    while (requests.Count > 0)
                    {
                        foreach (var request in requests)
                        {
                            SendRequest(request);
                        }

                        // Have any new requests been added while sending previous requests?
                        lock (this.client.locker)
                        {
                            requests = new List<DicomRequest>(this.client.requests);
                            this.client.requests.Clear();
                        }
                    }
                }
            }

            /// <summary>
            /// Callback for handling association reject scenarios.
            /// </summary>
            /// <param name="result">Specification of rejection result.</param>
            /// <param name="source">Source of rejection.</param>
            /// <param name="reason">Detailed reason for rejection.</param>
            public void OnReceiveAssociationReject(
                DicomRejectResult result,
                DicomRejectSource source,
                DicomRejectReason reason)
            {
                this.client.associateNotifier.TrySetResult(false);
                SetComplete(new DicomAssociationRejectedException(result, source, reason));
            }

            /// <summary>
            /// Callback on response from an association release.
            /// </summary>
            public void OnReceiveAssociationReleaseResponse()
            {
                SetComplete();
            }

            /// <summary>
            /// Callback on recieving an abort message.
            /// </summary>
            /// <param name="source">Abort source.</param>
            /// <param name="reason">Detailed reason for abort.</param>
            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
                SetComplete(new DicomAssociationAbortedException(source, reason));
            }

            /// <summary>
            /// Callback when connection is closed.
            /// </summary>
            /// <param name="exception">Exception, if any, that forced connection to close.</param>
            public void OnConnectionClosed(Exception exception)
            {
                SetComplete();
            }

            /// <summary>
            /// Callback for handling a client related C-STORE request, typically emanating from the client's C-GET request.
            /// </summary>
            /// <param name="request">
            /// C-STORE request.
            /// </param>
            /// <returns>
            /// The <see cref="DicomCStoreResponse"/> related to the C-STORE <paramref name="request"/>.
            /// </returns>
            public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
            {
                return this.client.OnCStoreRequest == null
                           ? new DicomCStoreResponse(request, DicomStatus.StorageStorageOutOfResources)
                           : this.client.OnCStoreRequest(request);
            }

            internal async Task DoSendAssociationReleaseRequestAsync(int millisecondsTimeout)
            {
                try
                {
                    SendAssociationReleaseRequest();
                    await WaitForDisconnectAsync(millisecondsTimeout).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Logger.Warn("Attempt to send association release request failed due to: {@error}", e);
                }
                finally
                {
                    SetComplete();
                }
            }

            internal void DoSendAbort()
            {
                if (IsConnected)
                {
                    SendAbort(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
                }
            }

            /// <summary>
            /// Action to perform when send queue is empty.
            /// </summary>
            protected override void OnSendQueueEmpty()
            {
                lock (this.locker)
                {
                    if (LingerTask == null || LingerTask.IsCompleted)
                    {
                        LingerTask = LingerAsync();
                    }
                }
            }

            private async Task LingerAsync()
            {
                var linger = this.client.Linger == Timeout.Infinite ? 0 : this.client.Linger;
                var disconnected = await WaitForDisconnectAsync(linger).ConfigureAwait(false);

                if (disconnected)
                {
                    SetComplete();
                }
                else if (IsSendQueueEmpty)
                {
                    await DoSendAssociationReleaseRequestAsync(ReleaseTimeout).ConfigureAwait(false);
                }
            }

            private async Task<bool> WaitForDisconnectAsync(int millisecondsDelay)
            {
                try
                {
                    using (var cancellationSource = new CancellationTokenSource(millisecondsDelay))
                    {
                        while (true)
                        {
                            if (!IsConnected || !IsSendQueueEmpty)
                            {
                                cancellationSource.Cancel();
                                break;
                            }
                            await Task.Delay(50, cancellationSource.Token).ConfigureAwait(false);
                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    return !IsConnected;
                }

                return false;
            }

            private void SetComplete(Exception ex = null)
            {
                if (this.client.completeNotifier != null)
                {
                    if (ex == null)
                    {
                        this.client.completeNotifier.TrySetResult(true);
                    }
                    else
                    {
                        this.client.completeNotifier.TrySetException(ex);
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}
