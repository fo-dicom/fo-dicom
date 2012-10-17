using System;
using System.Collections.Generic;
using System.Threading;

namespace Dicom.Threading {
	class ThreadPoolQueue<T> {
		private class WorkItem<T> {
			public T Group;
			public Action Action;
			public WaitCallback Callback;
			public object State;
		}

		private class WorkGroup<T> {
			public T Key;
			public volatile bool Executing = false;
			public Queue<WorkItem<T>> Items = new Queue<WorkItem<T>>();

			public WorkGroup(T key) {
				Key = key;
			}
		}

		private object _lock = new object();
		private Dictionary<T, WorkGroup<T>> _groups;

		public ThreadPoolQueue() {
			_groups = new Dictionary<T, WorkGroup<T>>();
		}

		public void Queue(T group, Action action) {
			Queue(new WorkItem<T> {
				Group = group,
				Action = action
			});
		}

		public void Queue(T group, WaitCallback callback) {
			Queue(new WorkItem<T> {
				Group = group,
				Callback = callback
			});
		}

		public void Queue(T group, WaitCallback callback, object state) {
			Queue(new WorkItem<T> {
				Group = group,
				Callback = callback,
				State = state
			});
		}

		private void Queue(WorkItem<T> item) {
			lock (_lock) {
				WorkGroup<T> group = null;
				if (!_groups.TryGetValue(item.Group, out group)) {
					group = new WorkGroup<T>(item.Group);
					_groups.Add(item.Group, group);
				}

				group.Items.Enqueue(item);
			}

			ExecuteNext(item.Group);
		}

		private void ExecuteNext(T groupKey) {
			lock (_lock) {
				WorkGroup<T> group = null;
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
			var item = (WorkItem<T>)state;

			try {
				if (item.Action != null)
					item.Action();
				else if (item.Callback != null)
					item.Callback(item.State);
			} catch {
				// log this somewhere?
			}

			WorkGroup<T> group = null;
			if (!_groups.TryGetValue(item.Group, out group))
				return;

			group.Executing = false;

			ExecuteNext(item.Group);
		}
	}
}
