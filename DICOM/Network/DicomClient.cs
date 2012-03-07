using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Dicom.Network {
	public class DicomClient {
		private EventAsyncResult _async;
		private Exception _exception;
		private List<DicomRequest> _requests;
		private DicomServiceUser _service;
		private int _asyncInvoked;
		private int _asyncPerformed;

		public DicomClient() {
			_requests = new List<DicomRequest>();
			_asyncInvoked = 1;
			_asyncPerformed = 1;
			Linger = 50;
		}

		public void NegotiateAsyncOps(int invoked = 0, int performed = 0) {
			_asyncInvoked = invoked;
			_asyncPerformed = performed;
		}

		/// <summary>
		/// Time in milliseconds to keep connection alive for additional requests.
		/// </summary>
		public int Linger {
			get;
			set;
		}

		public void AddRequest(DicomRequest request) {
			if (_service != null && _service.IsConnected)
				_service.SendRequest(request);
			else
				_requests.Add(request);
		}

		public void Send(Stream stream, string callingAe, string calledAe) {
			EndSend(BeginSend(stream, callingAe, calledAe, null, null));
		}

		public IAsyncResult BeginSend(Stream stream, string callingAe, string calledAe, AsyncCallback callback, object state) {
			var assoc = new DicomAssociation(callingAe, calledAe);
			assoc.MaxAsyncOpsInvoked = _asyncInvoked;
			assoc.MaxAsyncOpsPerformed = _asyncPerformed;
			foreach (var request in _requests)
				assoc.PresentationContexts.AddFromRequest(request);

			_service = new DicomServiceUser(this, stream, assoc);

			_async = new EventAsyncResult(callback, state);
			return _async;
		}

		public void EndSend(IAsyncResult result) {
			_async.AsyncWaitHandle.WaitOne();

			_service = null;
			_async = null;

			if (_exception != null)
				throw _exception;
		}

		private class DicomServiceUser : DicomService, IDicomServiceUser {
			private DicomClient _client;
			private Timer _timer;

			public DicomServiceUser(DicomClient client, Stream stream, DicomAssociation association) : base(stream) {
				_client = client;
				SendAssociationRequest(association);
			}

			public void OnReceiveAssociationAccept(DicomAssociation association) {
				foreach (var request in _client._requests)
					SendRequest(request);
			}

			protected override void OnSendQueueEmpty() {
				if (_timer == null)
					_timer = new Timer(OnLingerTimeout);
				if (_client.Linger == Timeout.Infinite)
					SendAssociationReleaseRequest();
				else
					_timer.Change(_client.Linger, Timeout.Infinite);
			}

			private void OnLingerTimeout(object state) {
				SendAssociationReleaseRequest();
			}

			public void OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) {
				_client._exception = new DicomAssociationRejectedException(result, source, reason);
				_client._async.Set();
			}

			public void OnReceiveAssociationReleaseResponse() {
				_client._async.Set();
			}

			public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) {
				_client._exception = new DicomAssociationAbortedException(source, reason);
				_client._async.Set();
			}
		}
	}
}
