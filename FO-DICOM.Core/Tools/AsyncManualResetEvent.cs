// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Tools
{
    /// <summary>
    /// An async-compatible manual-reset event.
    /// Original idea by Stephen Toub: http://blogs.msdn.com/b/pfxteam/archive/2012/02/11/10266920.aspx
    /// This implementation was originally written by Stephen Cleary https://github.com/StephenCleary/AsyncEx
    /// and adapted for use in this library
    /// </summary>
    internal sealed class AsyncManualResetEvent : IDisposable
    {
        /// <summary>
        /// The object used for synchronization.
        /// </summary>
        private readonly object _mutex;

        /// <summary>
        /// The current state of the event.
        /// </summary>
        private TaskCompletionSource<object> _tcs;

        /// <summary>
        /// Creates an async-compatible manual-reset event.
        /// </summary>
        /// <param name="set">Whether the manual-reset event is initially set or unset.</param>
        public AsyncManualResetEvent(bool set)
        {
            _mutex = new object();
            _tcs = TaskCompletionSourceFactory.Create<object>();
            if (set)
            {
                _tcs.TrySetResult(null);
            }
        }

        /// <summary>
        /// Creates an async-compatible manual-reset event that is initially unset.
        /// </summary>
        public AsyncManualResetEvent()
            : this(false)
        {
        }

        /// <summary>
        /// Asynchronously waits for this event to be set.
        /// </summary>
        public Task WaitAsync()
        {
            lock (_mutex)
            {
                return _tcs.Task;
            }
        }

        /// <summary>
        /// Sets the event, atomically completing every task returned by <see cref="WaitAsync"/>.
        /// If the event is already set, this method does nothing.
        /// </summary>
        public void Set()
        {
            lock (_mutex)
            {
                _tcs.TrySetResult(null);
            }
        }

        /// <summary>
        /// Resets the event. If the event is already reset, this method does nothing.
        /// </summary>
        public void Reset()
        {
            lock (_mutex)
            {
                if (_tcs.Task.IsCompleted)
                {
                    _tcs = TaskCompletionSourceFactory.Create<object>();
                }
            }
        }

        public void Dispose() => _tcs.TrySetCanceled();
    }
}
