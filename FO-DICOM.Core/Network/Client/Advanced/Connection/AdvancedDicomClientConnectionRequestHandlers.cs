// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    public class AdvancedDicomClientConnectionRequestHandlers
    {
        public DicomClientCStoreRequestHandler OnCStoreRequest { get; set; }
        
        public DicomClientNEventReportRequestHandler OnNEventReportRequest { get; set; }
    }
}