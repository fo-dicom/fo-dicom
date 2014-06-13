using System;
using System.Collections.Generic;
using System.Threading;

namespace Dicom.Threading {
	/// <summary>
	/// Executes a queue of actions on a single thread.
	/// </summary>
	public class ActionQueue : IDisposable {
		private Thread _thread;
		private ManualResetEventSlim _event;
		private volatile bool _stop;
		private object _lock;
		private Queue<Action> _actions;

		public ActionQueue() {
		}

		~ActionQueue() {
			Stop();
		}

		public int Count {
			get {
				if (_actions == null)
					return 0;

				lock (_lock)
					return _actions.Count;
			}
		}

		public void Enqueue(Action action) {
			if (_thread == null)
				Start();

			lock (_lock) {
				_actions.Enqueue(action);
				_event.Set();
			}
		}

		public void Enqueue<T1>(Action<T1> action, T1 arg0) {
			Enqueue(new ActionCallback<T1>(action, arg0));
		}

		public void Enqueue<T1, T2>(Action<T1, T2> action, T1 arg0, T2 arg1) {
			Enqueue(new ActionCallback<T1, T2>(action, arg0, arg1));
		}

		public void Enqueue<T1, T2, T3, T4>(Action<T1, T2, T3> action, T1 arg0, T2 arg1, T3 arg2) {
			Enqueue(new ActionCallback<T1, T2, T3>(action, arg0, arg1, arg2));
		}

		public void Enqueue<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg0, T2 arg1, T3 arg2, T4 arg3) {
			Enqueue(new ActionCallback<T1, T2, T3, T4>(action, arg0, arg1, arg2, arg3));
		}

		private void Start() {
			if (_thread != null)
				return;

			_event = new ManualResetEventSlim(false);
			_stop = false;
			_lock = new object();
			_actions = new Queue<Action>();
			_thread = new Thread(Proc);
			_thread.IsBackground = true;
			_thread.Start();
		}

		private void Stop() {
			if (_thread != null) {
				_stop = true;
				_event.Set();
				_thread = null;
			}
		}

		private void Proc() {
			while (true) {
				_event.Wait(1000);

				while (!_stop) {
					Action action;

					lock (_lock) {
						if (_actions.Count == 0) {
							_event.Reset();
							break;
						}

						action = _actions.Dequeue();

						if (_actions.Count == 0)
							_event.Reset();
					}

					try {
						action.Invoke();
					} catch {
						// log this somewhere?
					}

					// force dereference
					action = null;
				}

				if (_stop)
					return;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
			Stop();
		}
	}
}
