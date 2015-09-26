// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Media
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public partial class DicomDirectory
    {
        #region Save/Load Methods

        /// <summary>
        /// Perform background DICOM directory open operation.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result handle to be managed by <see cref="EndOpen"/>.</returns>
        [Obsolete]
        public static new IAsyncResult BeginOpen(string fileName, AsyncCallback callback, object state)
        {
            return AsyncFactory.ToBegin(OpenAsync(fileName, DicomEncoding.Default), callback, state);
        }

        /// <summary>
        /// Perform background DICOM directory open operation.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="fallbackEncoding">Encoding to apply if encoding cannot be retrieved from file.</param>
        /// <param name="callback">Asynchronous callback.</param>
        /// <param name="state">Asynchronous state.</param>
        /// <returns>Asynchronous result handle to be managed by <see cref="EndOpen"/>.</returns>
        [Obsolete]
        public static new IAsyncResult BeginOpen(
            string fileName,
            Encoding fallbackEncoding,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(OpenAsync(fileName, fallbackEncoding), callback, state);
        }

        /// <summary>
        /// Complete background DICOM directory open operation.
        /// </summary>
        /// <param name="result">Asynchronous result from <code>BeginOpen</code> operation.</param>
        /// <returns>Resulting <see cref="DicomDirectory"/>.</returns>
        [Obsolete]
        public static new DicomDirectory EndOpen(IAsyncResult result)
        {
            return AsyncFactory.ToEnd<DicomDirectory>(result);
        }

        #endregion
    }
}