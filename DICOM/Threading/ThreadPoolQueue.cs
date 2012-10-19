using System;
using System.Collections.Generic;
using System.Threading;

namespace Dicom.Threading {
	class ThreadPoolQueue<T> {
		private class WorkItem {
			public T Group;
			public Action Action;
			public WaitCallback Callback;
			public object State;
		}

		private class WorkGroup {
			public T Key;
			public volatile bool Executing = false;
			public Queue<WorkItem> Items = new Queue<WorkItem>();

			public WorkGroup(T key) {
				Key = key;
			}
		}

		private object _lock = new object();
		private Dictionary<T, WorkGroup> _groups;

		public ThreadPoolQueue() {
			_groups = new Dictionary<T, WorkGroup>();
		}

		public void Queue(T group, Action action) {
			Queue(new WorkItem {
				Group = group,
				Action = action
			});
		}

		public void Queue(T group, WaitCallback callback) {
			Queue(new WorkItem {
				Group = group,
				Callback = callback
			});
		}

		public void Queue(T group, WaitCallback callback, object state) {
			Queue(new WorkItem {
				Group = group,
				Callback = callback,
				State = state
			});
		}

		private void Queue(WorkItem item) {
			lock (_lock) {
				WorkGroup group = null;
				if (!_groups.TryGetValue(item.Group, out group)) {
					group = new WorkGroup(item.Group);
					_groups.Add(item.Group, group);
				}

				group.Items.Enqueue(item);
			}

			ExecuteNext(item.Group);
		}

		private void ExecuteNext(T groupKey) {
			lock (_lock) {
				WorkGroup group = null;
				if (!_groups.TryGetValue(groupKey, out group))
					return;

				if (group.Executing)
					return;

				if (group.Items.Count == 0) {
					_groups.Remove(groupKey);
					return;
				}

				var item = group.Items.Dequeue();

				ThreadPool.QueueUserWorkItem(ExecuteProc, item);
			}
		}

		private void ExecuteProc(object state) {
			var item = (WorkItem)state;

			try {
				if (item.Action != null)
					item.Action();
				else if (item.Callback != null)
					item.Callback(item.State);
			} catch {
				// log this somewhere?
			}

			lock (_lock) {
				WorkGroup group = null;
				if (!_groups.TryGetValue(item.Group, out group))
					return;

				group.Executing = false;

				ExecuteNext(item.Group);
			}
		}
	}
}
