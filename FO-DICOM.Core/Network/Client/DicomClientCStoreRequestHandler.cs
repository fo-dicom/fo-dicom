// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client
{
    /// <summary>
    /// Delegate for client handling the C-STORE request immediately.
    /// </summary>
    /// <param name="request">C-STORE request subject to handling.</param>
    /// <returns>Response from handling the C-STORE <paramref name="request"/>.</returns>
    public delegate Task<DicomCStoreResponse> DicomClientCStoreRequestHandler(DicomCStoreRequest request);
}
