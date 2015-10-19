// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for handling queue of categorized work items.
    /// </summary>
    /// <typeparam name="T">Group key type.</typeparam>
    public class TaskQueue<T>
    {
        /// <summary>
        /// Work item.
        /// </summary>
        private class WorkItem
        {
            public T Group;

            public Action Action;

            public Action<object> Callback;

            public object State;
        }

        /// <summary>
        /// Group of work items.
        /// </summary>
        private class WorkGroup
        {
            public readonly T Key;

            public readonly object Lock = new object();

            public volatile Task Task;

            public readonly Queue<WorkItem> Items = new Queue<WorkItem>();

            public WorkGroup(T key)
            {
                Key = key;
            }
        }

        private readonly object _lock = new object();

        private volatile bool _stopped = false;

        private readonly Dictionary<T, WorkGroup> _groups;

        /// <summary>
        /// Initializes an instance of <see cref="TaskQueue{T}"/>.
        /// </summary>
        public TaskQueue(T defaultGroup = default(T))
        {
            _groups = new Dictionary<T, WorkGroup>();
            Linger = 200;
            DefaultGroup = defaultGroup;
        }

        /// <summary>Gets or sets the time in milliseconds (ms) to keep the WorkGroup alive after processing last item.</summary>
        public int Linger { get; set; }

        /// <summary>
        /// Gets whether the thread pool queue is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return !_stopped;
            }
        }

        /// <summary>Gets or sets the value of key for default group.</summary>
        public T DefaultGroup { get; private set; }

        /// <summary>
        /// Starts the queue.
        /// </summary>
        public void Start()
        {
            _stopped = false;

            foreach (var group in _groups.Keys) Execute(group);
        }

        /// <summary>
        /// Stops the queue.
        /// </summary>
        /// <remarks>Executing work item will run to completion.</remarks>
        public void Stop()
        {
            _stopped = true;
        }

        /// <summary>
        /// Queue an <see cref="Action"/> to the <see cref="DefaultGroup"/>.
        /// </summary>
        /// <param name="action">Action to queue.</param>
        public void Queue(Action action)
        {
            Queue(new WorkItem { Group = DefaultGroup, Action = action });
        }

        /// <summary>
        /// Queue a <see cref="Action{T}"/> to the <see cref="DefaultGroup"/>.
        /// </summary>
        /// <param name="callback">Callback to queue.</param>
        public void Queue(Action<object> callback)
        {
            Queue(new WorkItem { Group = DefaultGroup, Callback = callback });
        }

        /// <summary>
        /// Queue a <see cref="Action{T}"/> to the <see cref="DefaultGroup"/>.
        /// </summary>
        /// <param name="callback">Callback to queue.</param>
        /// <param name="state">Callback state.</param>
        public void Queue(Action<object> callback, object state)
        {
            Queue(new WorkItem { Group = DefaultGroup, Callback = callback, State = state });
        }

        /// <summary>
        /// Queue an <see cref="Action"/>.
        /// </summary>
        /// <param name="group">Group within which to execute work item.</param>
        /// <param name="action">Action to queue.</param>
        public void Queue(T group, Action action)
        {
            Queue(new WorkItem { Group = group, Action = action });
        }

        /// <summary>
        /// Queue a <see cref="Action{T}"/>.
        /// </summary>
        /// <param name="group">Group within which to execute work item.</param>
        /// <param name="callback">Callback to queue.</param>
        public void Queue(T group, Action<object> callback)
        {
            Queue(new WorkItem { Group = group, Callback = callback });
        }

        /// <summary>
        /// Queue a <see cref="Action{T}"/>.
        /// </summary>
        /// <param name="group">Group within which to execute work item.</param>
        /// <param name="callback">Callback to queue.</param>
        /// <param name="state">Callback state.</param>
        public void Queue(T group, Action<object> callback, object state)
        {
            Queue(new WorkItem { Group = group, Callback = callback, State = state });
        }

        /// <summary>
        /// Queue work item.
        /// </summary>
        /// <param name="item">Work item to queue.</param>
        private void Queue(WorkItem item)
        {
            lock (_lock)
            {
                WorkGroup group;
                if (!_groups.TryGetValue(item.Group, out group))
                {
                    group = new WorkGroup(item.Group);
                    _groups.Add(item.Group, group);
                }

                lock (group.Lock) group.Items.Enqueue(item);

                Execute(item.Group);
            }
        }

        /// <summary>
        /// Execute group of work items.
        /// </summary>
        /// <param name="groupKey">Group key.</param>
        private void Execute(T groupKey)
        {
            if (_stopped) return;

            WorkGroup group;
            lock (_lock)
            {
                if (!_groups.TryGetValue(groupKey, out group)) return;
            }

            lock (group.Lock)
            {
                if (group.Task != null && !group.Task.IsCompleted) return;
                group.Task = Task.Run(() => this.ExecuteProc(group));
            }
        }

        /// <summary>
        /// The delegate invocation for executing a group of work items. 
        /// </summary>
        /// <param name="group">Group to execute.</param>
        private async void ExecuteProc(WorkGroup group)
        {
            while (!_stopped)
            {
                WorkItem item = null;

                bool empty;
                lock (group.Lock)
                {
                    empty = group.Items.Count == 0;
                    if (!empty) item = group.Items.Dequeue();
                }

                if (empty)
                {
                    // Linger specified time in case new tasks are added.
                    try
                    {
                        using (var cancellationSource = new CancellationTokenSource(this.Linger))
                        {
                            do
                            {
                                lock (group.Lock)
                                {
                                    if (group.Items.Count > 0)
                                    {
                                        empty = false;
                                        item = group.Items.Dequeue();
                                        cancellationSource.Cancel();
                                    }
                                }
                                await Task.Delay(1, cancellationSource.Token).ConfigureAwait(false);
                            }
                            while (!cancellationSource.IsCancellationRequested);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                    }

                    if (empty)
                    {
                        lock (group.Lock)
                        {
                            group.Task = null;

                            lock (_lock)
                            {
                                if (!group.Key.Equals(DefaultGroup)) _groups.Remove(group.Key);
                                return;
                            }
                        }
                    }
                }

                try
                {
                    if (item.Action != null) item.Action();
                    else if (item.Callback != null) item.Callback(item.State);
                }
                catch
                {
                    // log this somewhere?
                }
            }
        }
    }
}
