// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <returns>Collection of C-FIND responses based on <paramref name="request"/>.</returns>
        Task<IEnumerable<Task<DicomCFindResponse>>> OnCFindRequestAsync(DicomCFindRequest request);
    }
}
