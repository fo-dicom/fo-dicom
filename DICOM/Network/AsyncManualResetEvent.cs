// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35

using System.Threading.Tasks;
using Dicom.Log;

namespace Dicom.Network
{
    /// <summary>
    /// Asynchronous manual reset event class, enabling the possibility to set a return value of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of value that the event can be set to.</typeparam>
    public class AsyncManualResetEvent<T>
    {
        #region FIELDS

        private TaskCompletionSource<T> _tcs;

        private readonly object _lock = new object();
        private readonly Logger _logger;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent{T}"/> class.
        /// </summary>
        /// <param name="isSet">Indicates whether event should be set from start.</param>
        /// <param name="value">Value of the set event.</param>
        internal AsyncManualResetEvent(bool isSet, T value, Logger logger)
        {
            _tcs = new TaskCompletionSource<T>();
            _logger = logger;
            if (isSet)
                _tcs.TrySetResult(value);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent{T}"/> class.
        /// </summary>
        /// <param name="isSet">Indicates whether event should be set from start. If set, value is set to <code>default(T)</code>.</param>
        internal AsyncManualResetEvent(bool isSet)
            : this(isSet, default(T), null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent{T}"/> class.
        /// Event is reset upon initialization.
        /// </summary>
        internal AsyncManualResetEvent(Logger logger)
            : this(false, default(T), logger)
        {
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets whether the event is set or not.
        /// </summary>
        internal bool IsSet
        {
            get
            {
                _logger?.Debug("[LOCK] Waiting for _lock in AsyncManualResetEvent.IsSet");
                bool isCompleted;
                lock (_lock)
                {
                    _logger?.Debug("[LOCK] Acquired _lock in AsyncManualResetEvent.IsSet");
                    isCompleted = _tcs.Task.IsCompleted;
                }
                _logger?.Debug("[LOCK] Released _lock in AsyncManualResetEvent.IsSet");
                return isCompleted;
            }
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Set event.
        /// </summary>
        /// <param name="value">Value to set for the event.</param>
        internal void Set(T value)
        {
            _logger?.Debug("[LOCK] Waiting for _lock in AsyncManualResetEvent.Set with value");
            lock (_lock)
            {
                _logger?.Debug("[LOCK] Acquired _lock in AsyncManualResetEvent.Set with value");
                if (_tcs.Task.IsCompleted)
                    _tcs = new TaskCompletionSource<T>();
                _tcs.TrySetResult(value);
            }
            _logger?.Debug("[LOCK] Released _lock in AsyncManualResetEvent.Set with value");
        }

        /// <summary>
        /// Set event to default value of type <typeparamref name="T"/>.
        /// </summary>
        internal void Set()
        {
            _logger?.Debug("[LOCK] Waiting for _lock in AsyncManualResetEvent.Set");
            lock (_lock)
            {
                _logger?.Debug("[LOCK] Acquired _lock in AsyncManualResetEvent.Set");
                if (_tcs.Task.IsCompleted)
                    _tcs = new TaskCompletionSource<T>();
                _tcs.TrySetResult(default(T));
            }
            _logger?.Debug("[LOCK] Released _lock in AsyncManualResetEvent.Set");
        }

        /// <summary>
        /// Reset event.
        /// </summary>
        internal void Reset()
        {
            lock (_lock)
            {
                _tcs = new TaskCompletionSource<T>();
            }
        }

        /// <summary>
        /// Asynchronously wait for event to be set.
        /// </summary>
        /// <returns>Awaitable <see cref="Task{T}"/>, where result is the set value.</returns>
        internal Task<T> WaitAsync()
        {
            lock (_lock)
            {
                return _tcs.Task;
            }
        }

        #endregion
    }

    /// <summary>
    /// Asynchronous parameterless manual reset event class.
    /// </summary>
    public sealed class AsyncManualResetEvent : AsyncManualResetEvent<object>
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent"/> class.
        /// </summary>
        /// <param name="isSet">Indicates whether event should be set from start.</param>
        internal AsyncManualResetEvent(bool isSet, Logger logger = null)
            : base(isSet, null, logger)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent"/> class.
        /// Event is reset upon initialization.
        /// </summary>
        internal AsyncManualResetEvent(Logger logger = null)
            : base(false, null, logger)
        {
        }

        #endregion
    }
}

#endif
