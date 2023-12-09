// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// Contains request handlers for requests that have been sent from a DICOM server to a DICOM client
    /// </summary>
    public class AdvancedDicomClientConnectionRequestHandlers
    {
        /// <summary>
        /// The handler that will be invoked to produce a C-STORE response for an incoming C-STORE request, typically following a C-GET request
        /// </summary>
        public DicomClientCStoreRequestHandler OnCStoreRequest { get; set; }
        
        /// <summary>
        /// The handler that will be invoked to produce a N-EVENT-REPORT response for an incoming N-EVENT-REPORT request, typically following a N-EVENT-REPORT-GET request
        /// </summary>
        public DicomClientNEventReportRequestHandler OnNEventReportRequest { get; set; }
    }
}