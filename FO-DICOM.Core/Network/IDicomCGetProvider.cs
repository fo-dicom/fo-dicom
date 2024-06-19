﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Threading;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Interface for methods related to a C-GET SCP.
    /// </summary>
    public interface IDicomCGetProvider
    {
        /// <summary>
        /// Generate collection of <see cref="DicomCGetResponse">C-GET responses</see> from a <see cref="DicomCGetRequest">C-GET request</see>.
        /// </summary>
        /// <param name="request">C-GET request.</param>
        /// <param name="cancellationToken">A cancellation token that will trigger when the connection is lost</param>
        /// <returns>Collection of C-GET responses resulting from the <paramref name="request"/>.</returns>
        IAsyncEnumerable<DicomCGetResponse> OnCGetRequestAsync(DicomCGetRequest request, CancellationToken cancellationToken);
    }
}
