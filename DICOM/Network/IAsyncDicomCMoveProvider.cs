﻿// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dicom.Network
{
    /// <summary>
    /// Interface representing asynchronous event handlers for DIMSE services applicable to C-MOVE SOP instances.
    /// </summary>
    public interface IAsyncDicomCMoveProvider
    {
        /// <summary>
        /// Handler of C-MOVE request.
        /// </summary>
        /// <param name="request">C-MOVE request subject to handling.</param>
        /// <returns>Collection of C-MOVE responses based on <paramref name="request"/>.</returns>
        IEnumerable<Task<DicomCMoveResponse>> OnCMoveRequest(DicomCMoveRequest request);
    }
}
