// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dicom.Network
{
    /// <summary>
    /// Interface for methods related to a C-GET SCP.
    /// </summary>
    public interface IAsyncDicomCGetProvider
    {
        /// <summary>
        /// Generate collection of <see cref="DicomCGetResponse">C-GET responses</see> from a <see cref="DicomCGetRequest">C-GET request</see>.
        /// </summary>
        /// <param name="request">C-GET request.</param>
        /// <returns>Collection of C-GET responses resulting from the <paramref name="request"/>.</returns>
        Task<IEnumerable<Task<DicomCGetResponse>>> OnCGetRequestAsync(DicomCGetRequest request);
    }
}
