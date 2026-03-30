// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Common interface for DICOM service users and providers.
    /// </summary>
    public interface IDicomService
    {
        // TODO: In next major release remove this interface in favour of IAsyncDicomService

        /// <summary>
        /// Callback on recieving an abort message.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        void OnConnectionClosed(Exception exception);
    }

    /// <summary>
    /// Common interface for DICOM service users and providers.
    /// </summary>
    public interface IAsyncDicomService
    {
        /// <summary>
        /// Callback on recieving an abort message.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Detailed reason for abort.</param>
        Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Callback when connection is closed.
        /// </summary>
        /// <param name="exception">Exception, if any, that forced connection to close.</param>
        Task OnConnectionClosedAsync(Exception exception);
    }
}
