namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    public class AdvancedDicomClientConnectionRequestHandlers
    {
        public DicomClientCStoreRequestHandler OnCStoreRequest { get; set; }
        
        public DicomClientNEventReportRequestHandler OnNEventReportRequest { get; set; }
    }
}