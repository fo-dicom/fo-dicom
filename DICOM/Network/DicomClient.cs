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

		public DicomClient() {
			_requests = new List<DicomRequest>();
		}

		public void AddRequest(DicomRequest request) {
			_requests.Add(request);
		}

		public void Send(Stream stream, string callingAe, string calledAe) {
			EndSend(BeginSend(stream, callingAe, calledAe, null, null));
		}

		public IAsyncResult BeginSend(Stream stream, string callingAe, string calledAe, AsyncCallback callback, object state) {
			var assoc = new DicomAssociation(callingAe, calledAe);
			foreach (var request in _requests)
				assoc.PresentationContexts.AddFromRequest(request);

			_service = new DicomServiceUser(this, stream, assoc);

			_async = new EventAsyncResult(callback, state);
			return _async;
		}

		public void EndSend(IAsyncResult result) {
			_async.AsyncWaitHandle.WaitOne();
			if (_exception != null)
				throw _exception;
		}

		private class DicomServiceUser : DicomService, IDicomServiceUser {
			private DicomClient _client;

			public DicomServiceUser(DicomClient client, Stream stream, DicomAssociation association) : base(stream) {
				_client = client;
				SendAssociationRequest(association);
			}

			public void OnReceiveAssociationAccept(DicomAssociation association) {
				foreach (var request in _client._requests)
					SendRequest(request);
			}

			protected override void OnSendQueueEmpty() {
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
