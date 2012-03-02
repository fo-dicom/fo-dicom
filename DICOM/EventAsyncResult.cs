using System;
using System.Threading;

namespace Dicom {
	internal class EventAsyncResult : IAsyncResult {
		private ManualResetEventSlim _event;
		private object _state;
		private AsyncCallback _callback;

		public EventAsyncResult(AsyncCallback callback=null, object state=null) {
			_state = state;
			_callback = callback;
			_event = new ManualResetEventSlim(false);
		}

		public void Set() {
			if (_event.IsSet)
				return;

			_event.Set();

			if (_callback != null)
				_callback.BeginInvoke(this, OnAsyncCallbackComplete, null);
		}
		private void OnAsyncCallbackComplete(IAsyncResult ar) {
			try {
				_callback.EndInvoke(ar);
			} catch {
			}
		}

		public object AsyncState {
			get { return _state; }
		}

		public object InternalState {
			get;
			set;
		}

		public WaitHandle AsyncWaitHandle {
			get { return _event.WaitHandle; }
		}

		public bool CompletedSynchronously {
			get { return false; }
		}

		public bool IsCompleted {
			get { return _event.IsSet; }
		}
	}
}
