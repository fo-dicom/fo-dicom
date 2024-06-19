// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Threading;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Interface representing event handlers for DIMSE services applicable to C-MOVE SOP instances.
    /// </summary>
    public interface IDicomCMoveProvider
    {
        /// <summary>
        /// Handler of C-MOVE request.
        /// </summary>
        /// <param name="request">C-MOVE request subject to handling.</param>
        /// <param name="cancellationToken">A cancellation token that will trigger when the connection is lost</param>
        /// <returns>Collection of C-MOVE responses based on <paramref name="request"/>.</returns>
        IAsyncEnumerable<DicomCMoveResponse> OnCMoveRequestAsync(DicomCMoveRequest request, CancellationToken cancellationToken);
    }
}
