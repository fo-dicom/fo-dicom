using System.Threading.Tasks;

namespace Dicom.Network.Client
{
    /// <summary>
    /// Delegate for client handling the N-EVENT-REPORT-RQ request immediately.
    /// </summary>
    /// <param name="request">N-EVENT-REPORT-RQ request subject to handling.</param>
    /// <returns>Response from handling the N-EVENT-REPORT-RQ <paramref name="request"/>.</returns>
    public delegate Task<DicomNEventReportResponse> DicomClientNEventReportRequestHandler(DicomNEventReportRequest request);
}
