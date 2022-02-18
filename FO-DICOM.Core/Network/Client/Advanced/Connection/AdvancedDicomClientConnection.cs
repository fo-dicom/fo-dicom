// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// Represents an open DICOM connection, which contains two things:<br/>
    /// 1. An open TCP socket to the port where the other AE is running<br/>
    /// 2. A DICOM listener that collects incoming DICOM messages over this socket<br/>
    /// </summary>
    public interface IAdvancedDicomClientConnection : IDicomClientConnection
    {
        /// <summary>
        /// Gets the DICOM service options. These configure low level details that change the behavior of the DICOM listener
        /// </summary>
        DicomServiceOptions Options { get; }
        
        /// <summary>
        /// Gets the callbacks that can be used to listen to incoming DICOM events
        /// </summary>
        IAdvancedDicomClientConnectionCallbacks Callbacks { get; }

        /// <summary>
        /// Tries to claim this connection to open a new association
        /// This will only work once.
        /// </summary>
        internal bool TryClaimForAssociation();
    }

    /// <inheritdoc cref="IAdvancedDicomClientConnection" />
    public class AdvancedDicomClientConnection : DicomService, IAdvancedDicomClientConnection
    {
        private int _claimed;
        
        public IAdvancedDicomClientConnectionCallbacks Callbacks { get; }
        
        public INetworkStream NetworkStream { get; }
        
        public Task Listener { get; private set; }
        
        public new bool IsSendNextMessageRequired => base.IsSendNextMessageRequired;

        public AdvancedDicomClientConnection(
                IAdvancedDicomClientConnectionCallbacks callbacks,
                INetworkStream networkStream,
                Encoding fallbackEncoding,
                DicomServiceOptions dicomServiceOptions,
                ILogger logger,
                DicomServiceDependencies dependencies) : base(networkStream, fallbackEncoding, logger, dependencies)
        {
            Options = dicomServiceOptions ?? throw new ArgumentNullException(nameof(dicomServiceOptions));
            Callbacks = callbacks ?? throw new ArgumentNullException(nameof(callbacks));
            NetworkStream = networkStream ?? throw new ArgumentNullException(nameof(networkStream));
        }

        public void StartListener()
        {
            if (Listener != null)
            {
                return;
            }

            Listener = Task.Run(RunAsync);
        }

        public new Task SendAssociationRequestAsync(DicomAssociation association) => base.SendAssociationRequestAsync(association);

        public new Task SendAssociationReleaseRequestAsync() => base.SendAssociationReleaseRequestAsync();

        public new Task SendAbortAsync(DicomAbortSource source, DicomAbortReason reason) => base.SendAbortAsync(source, reason);

        public new Task SendRequestAsync(DicomRequest request) => base.SendRequestAsync(request);

        public new Task SendNextMessageAsync() => base.SendNextMessageAsync();

        protected override Task OnSendQueueEmptyAsync() => Callbacks.OnSendQueueEmptyAsync();

        public Task OnReceiveAssociationAcceptAsync(DicomAssociation association) => Callbacks.OnReceiveAssociationAcceptAsync(association);

        public Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) => Callbacks.OnReceiveAssociationRejectAsync(result, source, reason);

        public Task OnReceiveAssociationReleaseResponseAsync() => Callbacks.OnReceiveAssociationReleaseResponseAsync();

        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason) => Callbacks.OnReceiveAbortAsync(source, reason);

        public Task OnConnectionClosedAsync(Exception exception) => Callbacks.OnConnectionClosedAsync(exception);

        public Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response) => Callbacks.OnRequestCompletedAsync(request, response);
        
        public Task OnRequestPendingAsync(DicomRequest request, DicomResponse response) => Callbacks.OnRequestPendingAsync(request, response);

        public Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout) => Callbacks.OnRequestTimedOutAsync(request, timeout);

        public Task<DicomResponse> OnCStoreRequestAsync(DicomCStoreRequest request) => Callbacks.OnCStoreRequestAsync(request);

        public Task<DicomResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request) => Callbacks.OnNEventReportRequestAsync(request);

        bool IAdvancedDicomClientConnection.TryClaimForAssociation() => Interlocked.CompareExchange(ref _claimed, 1, 0) == 0;
    }

    public class InterceptingAdvancedDicomClientConnection : IAdvancedDicomClientConnection
    {
        private readonly IAdvancedDicomClientConnection _inner;
        private readonly IAdvancedDicomClientConnectionInterceptor _interceptor;

        public InterceptingAdvancedDicomClientConnection(
            IAdvancedDicomClientConnection inner,
            IAdvancedDicomClientConnectionInterceptor interceptor
        )
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        }

        public INetworkStream NetworkStream => _inner.NetworkStream;

        public Task Listener => _inner.Listener;

        public bool IsSendNextMessageRequired => _inner.IsSendNextMessageRequired;

        public bool IsSendQueueEmpty => _inner.IsSendQueueEmpty;

        public DicomServiceOptions Options => _inner.Options;
        
        public IAdvancedDicomClientConnectionCallbacks Callbacks => _inner.Callbacks;
        
        public void StartListener()
            => _inner.StartListener();

        public Task SendAssociationRequestAsync(DicomAssociation association)
            => _interceptor.SendAssociationRequestAsync(_inner, association);

        public Task SendAssociationReleaseRequestAsync()
            => _interceptor.SendAssociationReleaseRequestAsync(_inner);

        public Task SendAbortAsync(DicomAbortSource source, DicomAbortReason reason)
            => _interceptor.SendAbortAsync(_inner, source, reason);

        public Task OnReceiveAssociationAcceptAsync(DicomAssociation association)
            => _interceptor.OnReceiveAssociationAcceptAsync(_inner, association);

        public Task OnReceiveAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
            => _interceptor.OnReceiveAssociationRejectAsync(_inner, result, source, reason);

        public Task OnReceiveAssociationReleaseResponseAsync()
            => _interceptor.OnReceiveAssociationReleaseResponseAsync(_inner);

        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
            => _interceptor.OnReceiveAbortAsync(_inner, source, reason);

        public Task OnConnectionClosedAsync(Exception exception)
            => _interceptor.OnConnectionClosedAsync(_inner, exception);

        public Task SendRequestAsync(DicomRequest request)
            => _interceptor.SendRequestAsync(_inner, request);

        public Task SendNextMessageAsync()
            => _inner.SendNextMessageAsync();

        public Task OnRequestCompletedAsync(DicomRequest request, DicomResponse response)
            => _interceptor.OnRequestCompletedAsync(_inner, request, response);

        public Task OnRequestPendingAsync(DicomRequest request, DicomResponse response)
            => _interceptor.OnRequestPendingAsync(_inner, request, response);

        public Task OnRequestTimedOutAsync(DicomRequest request, TimeSpan timeout)
            => _interceptor.OnRequestTimedOutAsync(_inner, request, timeout);

        public Task<DicomResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
            => _inner.OnCStoreRequestAsync(request);

        public Task<DicomResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request)
            => _inner.OnNEventReportRequestAsync(request);
        
        bool IAdvancedDicomClientConnection.TryClaimForAssociation() => _inner.TryClaimForAssociation();
        
        public void Dispose() => _inner.Dispose();
    }
}
