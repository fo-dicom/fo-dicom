// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for methods related to a C-GET SCP.
    /// </summary>
    public interface IDicomCGetProvider
    {
        /// <summary>
        /// Generate collection of <see cref="DicomCGetResponse">C-GET responses</see> from a <see cref="DicomCGetRequest">C-GET request</see>.
        /// </summary>
        /// <param name="request">C-GET request.</param>
        /// <returns>Collection of C-GET responses resulting from the <paramref name="request"/>.</returns>
        IEnumerable<DicomCGetResponse> OnCGetRequest(DicomCGetRequest request);
    }
}
