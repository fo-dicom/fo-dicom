// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading;
using System.Threading.Tasks;

namespace Dicom.Network.Client.Tasks
{
    internal static class TaskCompletionSourceExtensions
    {
        public static void TrySetResultAsynchronously<T>(this TaskCompletionSource<T> @this, T result)
        {
#if NETSTANDARD
            /**
             * Our TaskCompletionSource should have been created with TaskCreationOptions.RunContinuationsAsynchronously, so we don't have to do anything special
             * Any continuations will run asynchronously by default.
             */
            @this.TrySetResult(result);
#else
            /**
             * In .NET versions older than 4.6, calling SetResult on TaskCompletionSource runs its continuations synchronously.
             * This is a recipe for deadlocks.
             */
            Task.Factory.StartNew(s => ((TaskCompletionSource<T>)s).TrySetResult(result), @this,
                CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
#endif
        }

        public static void TrySetCanceledAsynchronously<T>(this TaskCompletionSource<T> @this)
        {
#if NETSTANDARD
            /**
             * Our TaskCompletionSource should have been created with TaskCreationOptions.RunContinuationsAsynchronously, so we don't have to do anything special
             * Any continuations will run asynchronously by default.
             */
            @this.TrySetCanceled();
#else
            /**
             * In .NET versions older than 4.6, calling SetCanceled on TaskCompletionSource runs its continuations synchronously.
             * This is a recipe for deadlocks.
             */
            Task.Factory.StartNew(s => ((TaskCompletionSource<T>)s).TrySetCanceled(), @this,
                CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
#endif
        }
    }
}
