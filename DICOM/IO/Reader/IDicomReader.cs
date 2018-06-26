﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System;

#if !NET35
    using System.Threading.Tasks;
#endif

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
        /// Reader was stopped at specific tag.
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

        bool IsDeflated { get; set; }

        /// <summary>
        /// Gets or sets the DICOM dictionary to be used by the reader.
        /// </summary>
        DicomDictionary Dictionary { get; set; }

        /// <summary>
        /// Perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Criterion at which to stop.</param>
        /// <returns>Reader resulting status.</returns>
        DicomReaderResult Read(IByteSource source, IDicomReaderObserver observer, Func<ParseState, bool> stop = null);

#if !NET35
        /// <summary>
        /// Asynchronously perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Criterion at which to stop.</param>
        /// <returns>Awaitable reader resulting status.</returns>
        Task<DicomReaderResult> ReadAsync(IByteSource source, IDicomReaderObserver observer, Func<ParseState, bool> stop = null);
#endif
    }
}
