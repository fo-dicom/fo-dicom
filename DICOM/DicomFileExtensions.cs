// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension class providing legacy method support from <see cref="DicomFile"/> class.
    /// </summary>
    public static class DicomFileExtensions
    {
        /// <summary>
        /// Deep-copy clone <see cref="DicomFile"/> object.
        /// </summary>
        /// <param name="original"><see cref="DicomFile"/> source.</param>
        /// <returns>Deep-copy clone of <paramref name="original"/>.</returns>
        public static DicomFile Clone(this DicomFile original)
        {
            var df = new DicomFile();
            df.FileMetaInfo.Add(original.FileMetaInfo);
            df.Dataset.Add(original.Dataset);
            df.Dataset.InternalTransferSyntax = original.Dataset.InternalTransferSyntax;
            return df;
        }

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
