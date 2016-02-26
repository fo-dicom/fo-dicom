// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;

    public static class _DicomFileExtensions
    {
        /// <summary>
        /// Perform background save operation.
        /// </summary>
        /// <param name="this"><see cref="DicomFile"/> object to perform the save operation.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result handle to be managed by <see cref="EndSave"/>.</returns>
        [Obsolete]
        public static IAsyncResult BeginSave(
            this DicomFile @this,
            string fileName,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(@this.SaveAsync(fileName), callback, state);
        }

        /// <summary>
        /// Complete background save operation.
        /// </summary>
        /// <param name="this"><see cref="DicomFile"/> object performing the save operation.</param>
        /// <param name="result">Asynchronous result emanating from <see cref="BeginSave"/>.</param>
        [Obsolete]
        public static void EndSave(this DicomFile @this, IAsyncResult result)
        {
            AsyncFactory.ToEnd(result);
        }
    }
}
