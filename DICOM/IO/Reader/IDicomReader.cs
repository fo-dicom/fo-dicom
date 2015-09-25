// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Reader
{
    /// <summary>
    /// Possible DICOM reader results.
    /// </summary>
    public enum DicomReaderResult
    {
        /// <summary>
        /// Reader is processing.
        /// </summary>
        Processing,

        /// <summary>
        /// Reader completed successfully.
        /// </summary>
        Success,

        /// <summary>
        /// Reader completed with error.
        /// </summary>
        Error,

        /// <summary>
        /// Reader was stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// Reader was suspended.
        /// </summary>
        Suspended
    }

    /// <summary>
    /// Interface representing a DICOM reader.
    /// </summary>
    public interface IDicomReader
    {
        /// <summary>
        /// Gets or sets whether value representation is explicit or not.
        /// </summary>
        bool IsExplicitVR { get; set; }

        /// <summary>
        /// Gets the current reader status.
        /// </summary>
        DicomReaderResult Status { get; }

        /// <summary>
        /// Perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Tag at which to stop.</param>
        /// <returns>Reader resulting status.</returns>
        DicomReaderResult Read(IByteSource source, IDicomReaderObserver observer, DicomTag stop = null);

        IAsyncResult BeginRead(
            IByteSource source,
            IDicomReaderObserver observer,
            DicomTag stop,
            AsyncCallback callback,
            object state);

        DicomReaderResult EndRead(IAsyncResult result);
    }
}
