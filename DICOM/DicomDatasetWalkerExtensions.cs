// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;

    /// <summary>
    /// Extensions class for legacy methods from <see cref="DicomDatasetWalker"/> class.
    /// </summary>
    public static class DicomDatasetWalkerExtensions
    {
        /// <summary>
        /// Perform a background "walk" across the DICOM dataset provided in the <see cref="DicomDatasetWalker"/> constructor.
        /// </summary>
        /// <param name="this"><see cref="DicomDatasetWalker"/> object used to perform walk.</param>
        /// <param name="walker">Dataset walker implementation to be used for dataset traversal.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result handle to be managed by <see cref="EndWalk"/>.</returns>
        [Obsolete]
        public static IAsyncResult BeginWalk(
            this DicomDatasetWalker @this,
            IDicomDatasetWalker walker,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(@this.WalkAsync(walker), callback, state);
        }

        /// <summary>
        /// Complete background walk operation.
        /// </summary>
        /// <param name="this"><see cref="DicomDatasetWalker"/> object used to perform walk.</param>
        /// <param name="result">Asynchronous result emanating from <see cref="BeginWalk"/>.</param>
        [Obsolete]
        public static void EndWalk(this DicomDatasetWalker @this, IAsyncResult result)
        {
            AsyncFactory.ToEnd(result);
        }
    }
}