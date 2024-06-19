﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="cancellationToken">A cancellation token that will trigger when the connection is lost</param>
        /// <returns>C-ECHO response.</returns>
        Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request, CancellationToken cancellationToken);
    }
}
