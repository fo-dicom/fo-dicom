// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension class for providing legacy methods from <see cref="DicomFileReader"/> class.
    /// </summary>
    public static class DicomFileReaderExtensions
    {
        /// <summary>
        /// Perform background read operation.
        /// </summary>
        /// <param name="this"><see cref="DicomFileReader"/> object performing the read operation.</param>
        /// <param name="source">Byte source subject to reading.</param>
        /// <param name="fileMetaInfo">DICOM file meta information reader observer.</param>
        /// <param name="dataset">DICOM dataset reader observer.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result handle to be managed by <see cref="EndRead"/>.</returns>
        [Obsolete]
        public static IAsyncResult BeginRead(
            this DicomFileReader @this,
            IByteSource source,
            IDicomReaderObserver fileMetaInfo,
            IDicomReaderObserver dataset,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(Task.Run(() => @this.Read(source, fileMetaInfo, dataset)), callback, state);
        }

        /// <summary>
        /// Complete background read operation.
        /// </summary>
        /// <param name="this"><see cref="DicomFileReader"/> object performing the read operation.</param>
        /// <param name="result">Asynchronous result emanating from <see cref="BeginRead"/>.</param>
        [Obsolete]
        public static DicomReaderResult EndRead(this DicomFileReader @this, IAsyncResult result)
        {
            return AsyncFactory.ToEnd<DicomReaderResult>(result);
        }

    }
}