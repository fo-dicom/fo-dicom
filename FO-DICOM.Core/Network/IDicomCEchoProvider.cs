// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Interface for C-ECHO service class providers.
    /// </summary>
    public interface IDicomCEchoProvider
    {
        /// <summary>
        /// Event handler for C-ECHO request.
        /// </summary>
        /// <param name="request">C-ECHO request.</param>
        /// <returns>C-ECHO response.</returns>
        DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request);
    }
}
