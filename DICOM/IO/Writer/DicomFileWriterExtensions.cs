// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Writer
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension class for providing legacy methods from <see cref="DicomFileWriter"/> class.
    /// </summary>
    public static class DicomFileWriterExtensions
    {
        /// <summary>
        /// Perform background write operation.
        /// </summary>
        /// <param name="this"><see cref="DicomFileWriter"/> object performing the write operation.</param>
        /// <param name="target">Byte target subject to writing.</param>
        /// <param name="fileMetaInfo">DICOM file meta information.</param>
        /// <param name="dataset">DICOM dataset.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result handle to be managed by <see cref="EndWrite"/>.</returns>
        public static IAsyncResult BeginWrite(
            this DicomFileWriter @this,
            IByteTarget target,
            DicomFileMetaInformation fileMetaInfo,
            DicomDataset dataset,
            AsyncCallback callback,
            object state)
        {
            return APMHelper.ToBegin(@this.WriteAsync(target, fileMetaInfo, dataset), callback, state);
        }

        /// <summary>
        /// Complete background write operation.
        /// </summary>
        /// <param name="this"><see cref="DicomFileWriter"/> object performing the write operation.</param>
        /// <param name="result">Asynchronous result emanating from <see cref="BeginWrite"/>.</param>
        public static void EndWrite(this DicomFileWriter @this, IAsyncResult result)
        {
            APMHelper.ToEnd(result);
        }
    }
}