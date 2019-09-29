// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace Dicom.Network
{
    /// <summary>
    /// Interface for async C-ECHO service class providers.
    /// </summary>
    public interface IDicomCEchoProviderAsync
    {
        /// <summary>
        /// Event handler for C-ECHO request.
        /// </summary>
        /// <param name="request">C-ECHO request.</param>
        /// <returns>C-ECHO response.</returns>
        Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request);
    }
}
