// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System;

    /// <summary>
    /// Extension class for providing legacy methods from <see cref="DicomReader"/> class.
    /// </summary>
    [Obsolete]
    public static class DicomReaderExtensions
    {
        /// <summary>
        /// Perform background read operation.
        /// </summary>
        /// <param name="this"><see cref="DicomReader"/> object performing the read operation.</param>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Criterion at which to stop.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result handle to be managed by <see cref="EndRead"/>.</returns>
        [Obsolete]
        public static IAsyncResult BeginRead(
            this DicomReader @this,
            IByteSource source,
            IDicomReaderObserver observer,
            Func<ParseState, bool> stop,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(@this.ReadAsync(source, observer, stop), callback, state);
        }

        /// <summary>
        /// Complete background read operation.
        /// </summary>
        /// <param name="this"><see cref="DicomReader"/> object performing the write operation.</param>
        /// <param name="result">Asynchronous result emanating from <see cref="BeginRead"/>.</param>
        /// <returns>Result of the read operation.</returns>
        [Obsolete]
        public static DicomReaderResult EndRead(this DicomReader @this, IAsyncResult result)
        {
            return AsyncFactory.ToEnd<DicomReaderResult>(result);
        }
    }
}