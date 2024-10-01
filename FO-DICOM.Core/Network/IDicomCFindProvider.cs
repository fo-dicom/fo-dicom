// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Threading;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Interface representing event handlers for DIMSE services applicable to C-FIND SOP instances.
    /// </summary>
    public interface IDicomCFindProvider
    {
        /// <summary>
        /// Handler of C-FIND request.
        /// </summary>
        /// <param name="request">C-FIND request subject to handling.</param>
        /// <param name="cancellationToken">A cancellation token that will trigger when the connection is lost</param>
        /// <returns>Collection of C-FIND responses based on <paramref name="request"/>.</returns>
        IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request, CancellationToken cancellationToken);
    }
}

