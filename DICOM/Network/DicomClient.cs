// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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

        private bool abort;

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
        /// <param name="useTls">Treu if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public void Send(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            if (this.requests.Count == 0) return;

            var noDelay = this.Options != null ? this.Options.TcpNoDelay : DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = (this.Options ?? DicomServiceOptions.Default).IgnoreSslPolicyErrors;

            this.networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);
            this.InitializeSend(this.networkStream.AsStream(), callingAe, calledAe);

            this.completeNotifier.Task.Wait();
            this.FinalizeSend();
        }

        /// <summary>
        /// Asynchonously send existing requests to DICOM service.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">Treu if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <returns>Awaitable task.</returns>
        public async Task SendAsync(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            if (this.requests.Count == 0) return;

            var noDelay = this.Options != null ? this.Options.TcpNoDelay : DicomServiceOptions.Default.TcpNoDelay;
            var ignoreSslPolicyErrors = (this.Options ?? DicomServiceOptions.Default).IgnoreSslPolicyErrors;

            this.networkStream = NetworkManager.CreateNetworkStream(host, port, useTls, noDelay, ignoreSslPolicyErrors);
            this.InitializeSend(this.networkStream.AsStream(), callingAe, calledAe);

            await this.completeNotifier.Task.ConfigureAwait(false);
            this.FinalizeSend();
        }

        /// <summary>
        /// Synchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="stream">Established network stream.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public void Send(Stream stream, string callingAe, string calledAe)
        {
            if (this.requests.Count == 0) return;

            this.InitializeSend(stream, callingAe, calledAe);
            this.completeNotifier.Task.Wait();
            this.FinalizeSend();
        }

        /// <summary>
        /// Asynchronously send existing requests to DICOM service.
        /// </summary>
        /// <param name="stream">Established network stream.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <returns>Awaitable task.</returns>
        public async Task SendAsync(Stream stream, string callingAe, string calledAe)
        {
            if (this.requests.Count == 0) return;

            this.InitializeSend(stream, callingAe, calledAe);
            await this.completeNotifier.Task.ConfigureAwait(false);
            this.FinalizeSend();
        }

        /// <summary>
        /// Synchronously wait for association.
        /// </summary>
        /// <param name="millisecondsTimeout">Milliseconds to wait for association to occur.</param>
        /// <returns>True if association is established, false otherwise.</returns>
        public bool WaitForAssociation(int millisecondsTimeout = 5000)
        {
            return this.associateNotifier != null && this.associateNotifier.Task.Wait(millisecondsTimeout);
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
            return task is Task<bool>;
        }

        /// <summary>
        /// Synchronously release association.
        /// </summary>
        public void Release()
        {
            try
            {
                this.service._SendAssociationReleaseRequest();
                this.completeNotifier.Task.Wait(10000);
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
        public async Task ReleaseAsync()
        {
            try
            {
                this.service._SendAssociationReleaseRequest();
                await Task.WhenAny(this.completeNotifier.Task, Task.Delay(10000)).ConfigureAwait(false);
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
            if (this.abort) return;

            try
            {
                this.abort = true;
                this.networkStream.Dispose();
            }
            catch
            {
            }
            finally
            {
                this.networkStream = null;
                if (this.completeNotifier != null) this.completeNotifier.TrySetResult(true);
            }
        }

        private void InitializeSend(Stream stream, string callingAe, string calledAe)
        {
            var assoc = new DicomAssociation(callingAe, calledAe)
                            {
                                MaxAsyncOpsInvoked = this.asyncInvoked,
                                MaxAsyncOpsPerformed = this.asyncPerformed
                            };
            foreach (var request in this.requests)
            {
                assoc.PresentationContexts.AddFromRequest(request);
            }
            foreach (var context in this.AdditionalPresentationContexts)
            {
                assoc.PresentationContexts.Add(context.AbstractSyntax, context.GetTransferSyntaxes().ToArray());
            }

            this.service = new DicomServiceUser(this, stream, assoc, this.Options, this.Logger);
            this.associateNotifier = new TaskCompletionSource<bool>();
            this.completeNotifier = new TaskCompletionSource<bool>();
        }

        private void FinalizeSend()
        {
            this.associateNotifier.TrySetResult(true);

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

            private readonly DicomClient client;

            private Timer timer;

            #endregion

            #region CONSTRUCTORS

            internal DicomServiceUser(
                DicomClient client,
                Stream stream,
                DicomAssociation association,
                DicomServiceOptions options,
                Logger log)
                : base(stream, log)
            {
                this.client = client;
                if (options != null) this.Options = options;
                this.SendAssociationRequest(association);
            }

            #endregion

            #region METHODS

            public void OnReceiveAssociationAccept(DicomAssociation association)
            {
                this.client.associateNotifier.TrySetResult(true);

                foreach (var request in this.client.requests) base.SendRequest(request);
                this.client.requests.Clear();
            }

            public void OnReceiveAssociationReject(
                DicomRejectResult result,
                DicomRejectSource source,
                DicomRejectReason reason)
            {
                this.DisableTimer();

                throw new DicomAssociationRejectedException(result, source, reason);
            }

            public void OnReceiveAssociationReleaseResponse()
            {
                this.DisableTimer();
                this.SignalCompleted();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
                this.DisableTimer();

                throw new DicomAssociationAbortedException(source, reason);
            }

            public void OnConnectionClosed(Exception exception)
            {
                this.DisableTimer();
                this.SignalCompleted();
            }

            public override void SendRequest(DicomRequest request)
            {
                this.DisableTimer();
                base.SendRequest(request);
            }

            protected override void OnSendQueueEmpty()
            {
                this.timer = new Timer(this.OnLingerTimeout,
                    null,
                    this.client.Linger == Timeout.Infinite ? 0 : this.client.Linger,
                    Timeout.Infinite);
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
                    this.SignalCompleted();
                    return;
                }

                this.timer = new Timer(this.OnReleaseTimeout, null, 2500, Timeout.Infinite);
            }

            private void OnLingerTimeout(object state)
            {
                if (!this.IsSendQueueEmpty) return;
                if (this.IsConnected) this._SendAssociationReleaseRequest();
            }

            private void OnReleaseTimeout(object state)
            {
                this.DisableTimer();
                this.SignalCompleted();
            }

            private void DisableTimer()
            {
                if (this.timer != null)
                {
                    this.timer.Change(Timeout.Infinite, Timeout.Infinite);
                }
            }

            private void SignalCompleted()
            {
                if (this.client.completeNotifier != null)
                {
                    this.client.completeNotifier.TrySetResult(true);
                }
            }

            #endregion
        }

        #endregion
    }
}
