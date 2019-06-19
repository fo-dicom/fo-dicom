// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace Dicom.Network.Client
{
    /// <summary>
    /// Delegate for client handling the C-STORE request immediately.
    /// </summary>
    /// <param name="request">C-STORE request subject to handling.</param>
    /// <returns>Response from handling the C-STORE <paramref name="request"/>.</returns>
    public delegate Task<DicomCStoreResponse> DicomClientCStoreRequestHandler(DicomCStoreRequest request);
}
