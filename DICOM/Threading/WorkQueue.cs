using System;
using System.Collections.Generic;
using System.Threading;

namespace Dicom.Threading {
	public class WorkQueue<T> : IDisposable {
		public delegate void WorkItemProcessor(T workItem);

		#region Private Members
		private WorkItemProcessor _processor;

		private object _queueLock;
		private Queue<T> _queue;

		private volatile bool _pause;
		private volatile int _threadCount;

		private volatile int _processed;
		private volatile int _active;
		#endregion

		#region Public Constructors
		public WorkQueue(WorkItemProcessor processor) : this(processor, Environment.ProcessorCount) {
		}

		public WorkQueue(WorkItemProcessor processor, int threads) {
			_threadCount = threads;

			_processor = processor;

			_queueLock = new object();
			_queue = new Queue<T>();
		}
		#endregion

		#region Public Properties
		public int PendingWorkItems {
			get {
				lock (_queueLock) {
					return _queue.Count;
				}
			}
		}

		public int ProcessedWorkItems {
			get { return _processed; }
		}

		public int ActiveThreads {
			get { return _active; }
		}

		public int ThreadCount {
			get { return _threadCount; }
		}

		public bool Pause {
			get { return _pause; }
			set {
				lock (_queueLock) {
					_pause = value;
					ProcessNext();
				}
			}
		}
		#endregion

		#region Public Methods
		public void QueueWorkItem(T workItem) {
			lock (_queueLock)
				_queue.Enqueue(workItem);
			ProcessNext();
		}
		#endregion

		#region Private Members
		private void ProcessNext() {
			lock (_queueLock) {
				if (_queue.Count > 0 && !_pause && _active < _threadCount) {
					T item = _queue.Dequeue();
					_processor.BeginInvoke(item, WorkerProcComplete, null);
					_active++;
				}
			}
		}

		private void WorkerProcComplete(IAsyncResult result) {
			try {
				_processor.EndInvoke(result);
			} catch {
			} finally {
				lock (_queueLock) {
					_processed++;
					_active--;
				}
				ProcessNext();
			}
		}
		#endregion

		#region IDisposable Members

		public void Dispose() {
			lock (_queueLock) {
				_queue.Clear();
			}
			while (true) {
				lock (_queueLock) {
					if (_active == 0)
						break;
				}
				Thread.Sleep(0);
			}
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
