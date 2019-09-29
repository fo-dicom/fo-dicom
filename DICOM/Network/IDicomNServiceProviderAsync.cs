﻿// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace Dicom.Network
{
    /// <summary>
    /// Interface representing event handlers for DIMSE services applicable to Normalized SOP instances.
    /// </summary>
    public interface IDicomNServiceProviderAsync
    {
        /// <summary>
        /// Handler of N-ACTION request.
        /// </summary>
        /// <param name="request">N-ACTION request subject to handling.</param>
        /// <returns>N-ACTION response based on <paramref name="request"/>.</returns>
        Task<DicomNActionResponse> OnNActionRequestAsync(DicomNActionRequest request);

        /// <summary>
        /// Handler of N-CREATE request.
        /// </summary>
        /// <param name="request">N-CREATE request subject to handling.</param>
        /// <returns>N-CREATE response based on <paramref name="request"/>.</returns>
        Task<DicomNCreateResponse> OnNCreateRequestAsync(DicomNCreateRequest request);

        /// <summary>
        /// Handler of N-DELETE request.
        /// </summary>
        /// <param name="request">N-DELETE request subject to handling.</param>
        /// <returns>N-DELETE response based on <paramref name="request"/>.</returns>
        Task<DicomNDeleteResponse> OnNDeleteRequestAsync(DicomNDeleteRequest request);

        /// <summary>
        /// Handler of N-EVENT-REPORT request.
        /// </summary>
        /// <param name="request">N-EVENT-REPORT request subject to handling.</param>
        /// <returns>N-EVENT-REPORT response based on <paramref name="request"/>.</returns>
        Task<DicomNEventReportResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request);

        /// <summary>
        /// Handler of N-GET request.
        /// </summary>
        /// <param name="request">N-GET request subject to handling.</param>
        /// <returns>N-GET response based on <paramref name="request"/>.</returns>
        Task<DicomNGetResponse> OnNGetRequestAsync(DicomNGetRequest request);

        /// <summary>
        /// Handler of N-SET request.
        /// </summary>
        /// <param name="request">N-SET request subject to handling.</param>
        /// <returns>N-SET response based on <paramref name="request"/>.</returns>
        Task<DicomNSetResponse> OnNSetRequestAsync(DicomNSetRequest request);
    }
}
