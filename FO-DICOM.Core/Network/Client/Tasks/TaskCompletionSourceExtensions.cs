// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Tasks
{

    internal static class TaskCompletionSourceExtensions
    {
        public static void TrySetResultAsynchronously<T>(this TaskCompletionSource<T> @this, T result)
        {
            /**
             * Our TaskCompletionSource should have been created with TaskCreationOptions.RunContinuationsAsynchronously, so we don't have to do anything special
             * Any continuations will run asynchronously by default.
             */
            @this.TrySetResult(result);
        }

        public static void TrySetCanceledAsynchronously<T>(this TaskCompletionSource<T> @this)
        {
            /**
             * Our TaskCompletionSource should have been created with TaskCreationOptions.RunContinuationsAsynchronously, so we don't have to do anything special
             * Any continuations will run asynchronously by default.
             */
            @this.TrySetCanceled();
        }
    }
}
