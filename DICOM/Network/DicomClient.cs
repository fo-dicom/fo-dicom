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

        #region PROPERTIES

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
                return this.requests.Count > 0 || this.AdditionalPresentationContexts.Count > 0;
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
            if (this.service != null && this.service.IsConnected)
            {
                this.service.SendRequest(request);
            }
            else this.requests.Add(request);
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
            if (!this.CanSend) return;

            var noDelay = this.Options != null ? this.Options.TcpNoDelay : DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = (this.Options ?? DicomServiceOptions.Default).IgnoreSslPolicyErrors;

            this.networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = host,
                RemotePort = port
            };

            this.DoSend(this.networkStream, assoc);
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
            if (!this.CanSend) Task.FromResult(false);   // TODO Replace with Task.CompletedTask when moving to .NET 4.6

            var noDelay = this.Options != null ? this.Options.TcpNoDelay : DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = (this.Options ?? DicomServiceOptions.Default).IgnoreSslPolicyErrors;

            this.networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = host,
                RemotePort = port
            };

            return this.DoSendAsync(this.networkStream, assoc);
        }

        /// <summary>
        /// Synchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="stream">Established network stream.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public void Send(INetworkStream stream, string callingAe, string calledAe)
        {
            if (!this.CanSend) return;

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = stream.Host,
                RemotePort = stream.Port
            };

            this.DoSend(stream, assoc);
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
            if (!this.CanSend) return Task.FromResult(false);   // TODO Replace with Task.CompletedTask when moving to .NET 4.6

            var assoc = new DicomAssociation(callingAe, calledAe)
            {
                MaxAsyncOpsInvoked = this.asyncInvoked,
                MaxAsyncOpsPerformed = this.asyncPerformed,
                RemoteHost = stream.Host,
                RemotePort = stream.Port
            };

            return this.DoSendAsync(stream, assoc);
        }

        /// <summary>
        /// Synchronously wait for association.
        /// </summary>
        /// <param name="millisecondsTimeout">Milliseconds to wait for association to occur.</param>
        /// <returns>True if association is established, false otherwise.</returns>
        public bool WaitForAssociation(int millisecondsTimeout = 5000)
        {
            return this.associateNotifier != null && this.associateNotifier.Task.Wait(millisecondsTimeout)
                   && this.associateNotifier.Task.Result;
        }

        /// <summary>
        /// Asynchronously wait for association.
        /// </summary>
        /// <param name="millisecondsTimeout">Milliseconds to wait for association to occur.</param>
        /// <returns>True if association is established, false otherwise.</returns>
        public async Task<bool> WaitForAssociationAsync(int millisecondsTimeout = 5000)
        {
            if (this.associateNotifier == null) return false;
            var task = await Task.WhenAny(this.associateNotifier.Task, Task.Delay(millisecondsTimeout)).ConfigureAwait(false);
            return task is Task<bool> && ((Task<bool>)task).Result;
        }

        /// <summary>
        /// Synchronously release association.
        /// </summary>
        public void Release(int millisecondsTimeout = 10000)
        {
            try
            {
                this.service._SendAssociationReleaseRequest();
                this.completeNotifier.Task.Wait(millisecondsTimeout);
            }
            catch
            {
            }
            finally
            {
                this.Abort();
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
                this.service._SendAssociationReleaseRequest();
                await Task.WhenAny(this.completeNotifier.Task, Task.Delay(millisecondsTimeout)).ConfigureAwait(false);
            }
            catch
            {
            }
            finally
            {
                this.Abort();
            }
        }

        /// <summary>
        /// Abort DICOM service connection.
        /// </summary>
        public void Abort()
        {
            if (this.aborted) return;

            if (this.associateNotifier != null && !this.associateNotifier.Task.IsCompleted)
            {
                this.associateNotifier.TrySetResult(false);
            }
            if (this.completeNotifier != null) this.completeNotifier.TrySetResult(true);

            if (this.networkStream != null)
            {
                try
                {
                    this.networkStream.Dispose();
                }
                catch
                {
                }
            }

            this.service = null;
            this.networkStream = null;

            this.aborted = true;
        }

        private void DoSend(INetworkStream stream, DicomAssociation assoc)
        {
            try
            {
                this.InitializeSend(stream, assoc);
                this.completeNotifier.Task.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    throw e;
                }
            }
            finally
            {
                this.FinalizeSend();
            }
        }

        private async Task DoSendAsync(INetworkStream stream, DicomAssociation assoc)
        {
            this.InitializeSend(stream, assoc);
            await this.completeNotifier.Task.ConfigureAwait(false);
            this.FinalizeSend();
        }

        private void InitializeSend(INetworkStream stream, DicomAssociation association)
        {
            foreach (var request in this.requests)
            {
                association.PresentationContexts.AddFromRequest(request);
            }
            foreach (var context in this.AdditionalPresentationContexts)
            {
                association.PresentationContexts.Add(context.AbstractSyntax, context.GetTransferSyntaxes().ToArray());
            }

            this.associateNotifier = new TaskCompletionSource<bool>();
            this.completeNotifier = new TaskCompletionSource<bool>();

            this.service = new DicomServiceUser(this, stream, association, this.Options, this.FallbackEncoding, this.Logger);
        }

        private void FinalizeSend()
        {
            if (!this.associateNotifier.Task.IsCompleted)
            {
                this.associateNotifier.TrySetResult(true);
            }

            if (this.networkStream != null)
            {
                try
                {
                    this.networkStream.Dispose();
                }
                catch
                {
                }
            }

            this.service = null;
            this.networkStream = null;
        }

        #endregion

        #region INNER TYPES

        private class DicomServiceUser : DicomService, IDicomServiceUser
        {
            #region FIELDS

            private const int ReleaseTimeout = 2500;

            private readonly DicomClient client;

            private bool isLingering;

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
                this.isLingering = false;
                if (options != null) this.Options = options;
                this.SendAssociationRequest(association);
            }

            #endregion

            #region METHODS

            public void OnReceiveAssociationAccept(DicomAssociation association)
            {
                foreach (var ctx in this.client.AdditionalPresentationContexts)
                {
                    foreach (
                        var item in
                            association.PresentationContexts.Where(pc => pc.AbstractSyntax == ctx.AbstractSyntax))
                    {
                        ctx.SetResult(item.Result, item.AcceptedTransferSyntax);
                    }
                }

                this.client.associateNotifier.TrySetResult(true);

                if (this.client.requests.Count > 0)
                {
                    foreach (var request in this.client.requests) this.SendRequest(request);
                    this.client.requests.Clear();
                }
                else
                {
                    this._SendAssociationReleaseRequest();
                }
            }

            public void OnReceiveAssociationReject(
                DicomRejectResult result,
                DicomRejectSource source,
                DicomRejectReason reason)
            {
                this.SetComplete(new DicomAssociationRejectedException(result, source, reason));
            }

            public void OnReceiveAssociationReleaseResponse()
            {
                this.SetComplete();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
                this.SetComplete(new DicomAssociationAbortedException(source, reason));
            }

            public void OnConnectionClosed(Exception exception)
            {
                this.SetComplete();
            }

            protected override void OnSendQueueEmpty()
            {
                this.OnLingerTimeout();
            }

            internal void _SendAssociationReleaseRequest()
            {
                try
                {
                    this.SendAssociationReleaseRequest();
                }
                catch
                {
                    // may have already disconnected
                    this.SetComplete();
                    return;
                }

                this.OnReleaseTimeout();
            }

            private async void OnLingerTimeout()
            {
                if (this.isLingering) return;

                this.isLingering = true;
                var disconnected =
                    await
                    this.WaitForDisconnect(this.client.Linger == Timeout.Infinite ? 0 : this.client.Linger)
                        .ConfigureAwait(false);
                this.isLingering = false;

                if (disconnected || !this.IsSendQueueEmpty) return;

                this._SendAssociationReleaseRequest();
            }

            private async void OnReleaseTimeout()
            {
                if (!await this.WaitForDisconnect(ReleaseTimeout).ConfigureAwait(false))
                {
                    this.SetComplete();
                }
            }

            private async Task<bool> WaitForDisconnect(int millisecondsDelay)
            {
                try
                {
                    using (var cancellationSource = new CancellationTokenSource(millisecondsDelay))
                    {
                        do
                        {
                            if (!this.IsConnected)
                            {
                                this.SetComplete();
                                return true;
                            }
                            await Task.Delay(1, cancellationSource.Token).ConfigureAwait(false);
                        }
                        while (!cancellationSource.IsCancellationRequested);
                    }
                }
                catch (TaskCanceledException)
                {
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
