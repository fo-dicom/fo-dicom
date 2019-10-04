// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).


using System;
#if !NET35
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Asynchronous manual reset event class, enabling the possibility to set a return value of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of value that the event can be set to.</typeparam>
    [Obsolete("Use the AsyncEx library instead")] // Or Dicom.Network.Client.Tasks.AsyncManualResetEvent if you're a Fellow Oak DICOM contributor
    public class AsyncManualResetEvent<T>
    {
        #region FIELDS

        private TaskCompletionSource<T> _tcs;

        private readonly object _lock = new object();

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent{T}"/> class.
        /// </summary>
        /// <param name="isSet">Indicates whether event should be set from start.</param>
        /// <param name="value">Value of the set event.</param>
        internal AsyncManualResetEvent(bool isSet, T value)
        {
            _tcs = new TaskCompletionSource<T>();
            if (isSet)
                _tcs.TrySetResult(value);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent{T}"/> class.
        /// </summary>
        /// <param name="isSet">Indicates whether event should be set from start. If set, value is set to <code>default(T)</code>.</param>
        internal AsyncManualResetEvent(bool isSet)
            : this(isSet, default(T))
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent{T}"/> class.
        /// Event is reset upon initialization.
        /// </summary>
        internal AsyncManualResetEvent()
            : this(false, default(T))
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
                bool isCompleted;
                lock (_lock)
                {
                    isCompleted = _tcs.Task.IsCompleted;
                }
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
            lock (_lock)
            {
                if (_tcs.Task.IsCompleted)
                    _tcs = new TaskCompletionSource<T>();
                _tcs.TrySetResult(value);
            }
        }

        /// <summary>
        /// Set event to default value of type <typeparamref name="T"/>.
        /// </summary>
        internal void Set()
        {
            lock (_lock)
            {
                if (_tcs.Task.IsCompleted)
                    _tcs = new TaskCompletionSource<T>();
                _tcs.TrySetResult(default(T));
            }
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
            Task<T> task;
            lock (_lock)
            {
                task = _tcs.Task;
            }
            return task;
        }

        #endregion
    }

    /// <summary>
    /// Asynchronous parameterless manual reset event class.
    /// </summary>
    [Obsolete("Use the AsyncEx library instead")] // Or Dicom.Network.Client.Tasks.AsyncManualResetEvent if you're a Fellow Oak DICOM contributor
    public sealed class AsyncManualResetEvent : AsyncManualResetEvent<object>
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent"/> class.
        /// </summary>
        /// <param name="isSet">Indicates whether event should be set from start.</param>
        internal AsyncManualResetEvent(bool isSet)
            : base(isSet, null)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="AsyncManualResetEvent"/> class.
        /// Event is reset upon initialization.
        /// </summary>
        internal AsyncManualResetEvent()
            : base(false, null)
        {
        }

        #endregion
    }
}

#endif
