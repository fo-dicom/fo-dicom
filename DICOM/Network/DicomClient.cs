using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

using Dicom.Log;

namespace Dicom.Network {
	public class DicomClient {
		private EventAsyncResult _async;
		private ManualResetEventSlim _assoc;
		private Exception _exception;
		private List<DicomRequest> _requests;
		private List<DicomPresentationContext> _contexts;
		private DicomServiceUser _service;
		private int _asyncInvoked;
		private int _asyncPerformed;
		private TcpClient _client;
		private bool _abort;

		public DicomClient() {
			_requests = new List<DicomRequest>();
			_contexts = new List<DicomPresentationContext>();
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

		/// <summary>
		/// Logger that is passed to the underlying DicomService implementation.
		/// </summary>
		public Logger Logger {
			get;
			set;
		}

		/// <summary>
		/// Options to control behavior of <see cref="DicomService"/> base class.
		/// </summary>
		public DicomServiceOptions Options {
			get;
			set;
		}

		/// <summary>
		/// Additional presentation contexts to negotiate with association.
		/// </summary>
		public List<DicomPresentationContext> AdditionalPresentationContexts {
			get { return _contexts; }
			set { _contexts = value; }
		}

		public object UserState {
			get;
			set;
		}

		public void AddRequest(DicomRequest request) {
			if (_service != null && _service.IsConnected) {
                //zssure:2015-04-14,try to conform whether AddRequest and Send uses the same one client
                LogManager.Default.GetLogger("Dicom.Network").Info("zssure debug at 20150414,the DicomRequest object is {0},DicomServiceUser is{1}", request.GetHashCode(), _service.GetHashCode());
                //zssure:2015-04-14,end
				_service.SendRequest(request);
				if (_service._timer != null)
					_service._timer.Change(Timeout.Infinite, Timeout.Infinite);
			} else
				_requests.Add(request);
		}

		public void Send(string host, int port, bool useTls, string callingAe, string calledAe) {
			EndSend(BeginSend(host, port, useTls, callingAe, calledAe, null, null));
		}

		public IAsyncResult BeginSend(string host, int port, bool useTls, string callingAe, string calledAe, AsyncCallback callback, object state) {
			_client = new TcpClient(host, port);
            //zssure:2015-04-14,try to conform whether AddRequest and Send uses the same one client
            LogManager.Default.GetLogger("Dicom.Network").Info("zssure debug at 20150414,the TcpClient object is {0},HashCode{1}", _client.ToString(),_client.GetHashCode()); 
            //zssure:2015-04-14,end

			if (Options != null)
				_client.NoDelay = Options.TcpNoDelay;
			else
				_client.NoDelay = DicomServiceOptions.Default.TcpNoDelay;

			Stream stream = _client.GetStream();

			if (useTls) {
				var ssl = new SslStream(stream, false, ValidateServerCertificate);
				ssl.AuthenticateAsClient(host);
				stream = ssl;
			}

			return BeginSend(stream, callingAe, calledAe, callback, state);
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
			foreach (var context in _contexts)
				assoc.PresentationContexts.Add(context.AbstractSyntax, context.GetTransferSyntaxes().ToArray());

			_service = new DicomServiceUser(this, stream, assoc, Logger);
            //zssure:2015-04-14,try to conform whether AddRequest and Send uses the same one client
            LogManager.Default.GetLogger("Dicom.Network").Info("zssure debug at 20150414,the DicomServiceUser object is {0}，HashCode{1}", _service.ToString(),_service.GetHashCode());
            //zssure:2015-04-14,end
			_assoc = new ManualResetEventSlim(false);

			_async = new EventAsyncResult(callback, state);
			return _async;
		}

		private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			if (sslPolicyErrors == SslPolicyErrors.None)
				return true;

			if (Options != null) {
				if (Options.IgnoreSslPolicyErrors)
					return true;
			} else if (DicomServiceOptions.Default.IgnoreSslPolicyErrors)
				return true;

			return false;
		}

		public void EndSend(IAsyncResult result) {
			if (_async != null)
				_async.AsyncWaitHandle.WaitOne();

			if (_assoc != null)
				_assoc.Set();

			if (_client != null) {
				try {
					_client.Close();
				} catch {
				}
			}

			_service = null;
			_client = null;
			_async = null;
			_assoc = null;

			if (_exception != null && !_abort)
				throw _exception;
		}

		public void WaitForAssociation(int millisecondsTimeout = 5000) {
			if (_assoc == null)
				return;

			_assoc.Wait(millisecondsTimeout);
		}

		public void Release() {
			try {
				_service._SendAssociationReleaseRequest();

				_async.AsyncWaitHandle.WaitOne(10000);
			} catch {
			} finally {
				Abort();
			}
		}

		public void Abort() {
			try {
				_abort = true;
				_client.Close();
			} catch {
			} finally {
				_client = null;
				try {
					_async.Set();
				} catch {
				}
				_async = null;
			}
		}

		private class DicomServiceUser : DicomService, IDicomServiceUser {
			public DicomClient _client;
			public Timer _timer;

			public DicomServiceUser(DicomClient client, Stream stream, DicomAssociation association, Logger log) : base(stream, log) {
				_client = client;
				if (_client.Options != null)
					Options = _client.Options;
				SendAssociationRequest(association);
			}

			public void _SendAssociationReleaseRequest() {
				try {
					SendAssociationReleaseRequest();
				} catch {
					// may have already disconnected
					_client._async.Set();
					return;
				}

				_timer = new Timer(OnReleaseTimeout);
				_timer.Change(2500, Timeout.Infinite);
			}

			public void OnReceiveAssociationAccept(DicomAssociation association) {
				_client._assoc.Set();
				_client._assoc = null;

                foreach (var request in _client._requests)
                {
                    //zssure:2015-04-14,try to conform whether AddRequest and Send uses the same one client
                    LogManager.Default.GetLogger("Dicom.Network").Info("zssure debug at 20150414,the DicomRequest object is {0},the DicomClient is{1},the DicomServiceUser is {2}", request.GetHashCode(), _client.GetHashCode(),this.GetHashCode());
                    //zssure:2015-04-14,end

                    SendRequest(request);
                }
				_client._requests.Clear();
			}

			protected override void OnSendQueueEmpty() {
				if (_client.Linger == Timeout.Infinite) {
					OnLingerTimeout(null);
				} else {
					_timer = new Timer(OnLingerTimeout);
					_timer.Change(_client.Linger, Timeout.Infinite);
				}
			}

			private void OnLingerTimeout(object state) {
				if (!IsSendQueueEmpty)
					return;

				if (IsConnected)
					_SendAssociationReleaseRequest();
			}

			private void OnReleaseTimeout(object state) {
				if (_timer != null)
					_timer.Change(Timeout.Infinite, Timeout.Infinite);

				try {
					if (_client._async != null)
						_client._async.Set();
				} catch {
					// event handler has already fired
				}
			}

			public void OnReceiveAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) {
				if (_timer != null)
					_timer.Change(Timeout.Infinite, Timeout.Infinite);

				_client._exception = new DicomAssociationRejectedException(result, source, reason);
				_client._async.Set();
			}

			public void OnReceiveAssociationReleaseResponse() {
				if (_timer != null)
					_timer.Change(Timeout.Infinite, Timeout.Infinite);

				_client._async.Set();
			}

			public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) {
				if (_timer != null)
					_timer.Change(Timeout.Infinite, Timeout.Infinite);

				_client._exception = new DicomAssociationAbortedException(source, reason);
				_client._async.Set();
			}

			public void OnConnectionClosed(int errorCode) {
				if (_timer != null)
					_timer.Change(Timeout.Infinite, Timeout.Infinite);

				if (errorCode != 0)
					_client._exception = new SocketException(errorCode);

				try {
					if (_client._async != null)
						_client._async.Set();
				} catch {
					// event handler has already fired
				}
			}
		}
	}
}
