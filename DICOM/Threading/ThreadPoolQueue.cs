using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Dicom.Threading {
	public class ThreadPoolQueue<T> {
		private class WorkItem {
			public T Group;
			public Action Action;
			public WaitCallback Callback;
			public object State;
		}

		private class WorkGroup {
			public T Key;
			public object Lock = new object();
			public volatile bool Executing = false;
			public Queue<WorkItem> Items = new Queue<WorkItem>();

			public WorkGroup(T key) {
				Key = key;
			}
		}

		private object _lock = new object();
		private volatile bool _stopped = false;
		private Dictionary<T, WorkGroup> _groups;

		public ThreadPoolQueue() {
			_groups = new Dictionary<T, WorkGroup>();
			Linger = 200;
			DefaultGroup = default(T);
		}

		/// <summary>Time in milliseconds (MS) to keep the WorkGroup alive after processing last item.</summary>
		public int Linger {
			get;
			set;
		}

		public bool IsRunning {
			get { return !_stopped; }
		}

		/// <summary>Value of key for default group.</summary>
		public T DefaultGroup {
			get;
			set;
		}

		public void Start() {
			_stopped = false;

			foreach (var group in _groups.Keys.ToArray())
				Execute(group);
		}

		public void Stop() {
			_stopped = true;
		}

		public void Queue(Action action) {
			Queue(new WorkItem {
				Group = DefaultGroup,
				Action = action
			});
		}

		public void Queue(WaitCallback callback) {
			Queue(new WorkItem {
				Group = DefaultGroup,
				Callback = callback
			});
		}

		public void Queue(WaitCallback callback, object state) {
			Queue(new WorkItem {
				Group = DefaultGroup,
				Callback = callback,
				State = state
			});
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

				lock (group.Lock)
					group.Items.Enqueue(item);

				Execute(item.Group);
			}
		}

		private void Execute(T groupKey) {
			if (_stopped)
				return;

			WorkGroup group = null;
			lock (_lock) {
				if (!_groups.TryGetValue(groupKey, out group))
					return;
			}
			lock (group.Lock) {
				if (group.Executing)
					return;

				if (group.Items.Count == 0 && !group.Key.Equals(DefaultGroup)) {
					_groups.Remove(groupKey);
                    System.Console.WriteLine("Remove WorkGroup Key is {0}", group.Key);
					return;
				}

				group.Executing = true;

				ThreadPool.QueueUserWorkItem(ExecuteProc, group);
			}
		}

		private void ExecuteProc(object state) {
			var group = (WorkGroup)state;

			do {
				if (_stopped)
					return;

				WorkItem item = null;

				bool empty;
				lock (group.Lock) {
					empty = group.Items.Count == 0;

					if (!empty)
						item = group.Items.Dequeue();
                    System.Console.WriteLine("WorkGroup Key is {0}", group.Key);
				}

				if (empty) {
					var linger = DateTime.Now.AddMilliseconds(Linger);
					while (empty && DateTime.Now < linger) {
						Thread.Sleep(0);
						lock (group.Lock) {
							empty = group.Items.Count == 0;

							if (!empty)
								item = group.Items.Dequeue();
						}
					}

					if (empty) {
						lock (group.Lock) {
							group.Executing = false;

							lock (_lock) {
                                if (!group.Key.Equals(DefaultGroup))
                                {
                                    _groups.Remove(group.Key);
                                    System.Console.WriteLine("Remove WorkGroup Key is {0}", group.Key);
                                }

								return;
							}
						}
					}
				}

				try {
					if (item.Action != null)
						item.Action();
					else if (item.Callback != null)
						item.Callback(item.State);
				} catch {
					// log this somewhere?
				}
			} while (true);
		}
	}
}
