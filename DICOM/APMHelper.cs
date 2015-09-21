// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Helper class for converting <see cref="Task"/>-based asynchronous pattern to Asynchronous Programming Model (APM) pattern.
    /// </summary>
    internal static class APMHelper
    {
        /// <summary>
        /// Convert <see cref="Task"/> to Begin... method
        /// </summary>
        /// <param name="task">Task to be converted.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result object.</returns>
        internal static Task ToBegin(Task task, AsyncCallback callback, object state)
        {
            if (task.AsyncState == state)
            {
                if (callback != null)
                {
                    task.ContinueWith(
                        delegate { callback(task); },
                        CancellationToken.None,
                        TaskContinuationOptions.None,
                        TaskScheduler.Default);
                }
                return task;
            }

            var tcs = new TaskCompletionSource<object>(state);
            task.ContinueWith(
                delegate
                    {
                        if (task.IsFaulted) tcs.TrySetException(task.Exception.InnerExceptions);
                        else if (task.IsCanceled) tcs.TrySetCanceled();
                        tcs.TrySetResult(null);

                        if (callback != null) callback(tcs.Task);

                    },
                CancellationToken.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>
        /// Mimic End... method assuming Begin... method mimics <see cref="Task"/>.
        /// </summary>
        /// <param name="asyncResult">Asynchronous result from <see cref="Task"/>-based Begin... method.</param>
        internal static void ToEnd(IAsyncResult asyncResult)
        {
            ((Task)asyncResult).GetAwaiter().GetResult();
        }
    }
}