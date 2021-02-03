// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Tasks
{
    internal static class TaskCompletionSourceFactory
    {
        /// <summary>
        /// Factory method to create a TaskCompletionSource. Its main purpose is so that you cannot forget to use RunContinuationsAsynchronously
        /// </summary>
        /// <typeparam name="T">The type of result</typeparam>
        /// <returns>A new TaskCompletionSource</returns>
        public static TaskCompletionSource<T> Create<T>() => new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
    }
}