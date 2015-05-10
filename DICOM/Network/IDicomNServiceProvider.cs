using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public interface IDicomNServiceProvider {
		DicomNActionResponse OnNActionRequest(DicomNActionRequest request, DicomPresentationContext presentationContext);
		DicomNCreateResponse OnNCreateRequest(DicomNCreateRequest request, DicomPresentationContext presentationContext);
		DicomNDeleteResponse OnNDeleteRequest(DicomNDeleteRequest request, DicomPresentationContext presentationContext);
		DicomNEventReportResponse OnNEventReportRequest(DicomNEventReportRequest request, DicomPresentationContext presentationContext);
		DicomNGetResponse OnNGetRequest(DicomNGetRequest request, DicomPresentationContext presentationContext);
		DicomNSetResponse OnNSetRequest(DicomNSetRequest request, DicomPresentationContext presentationContext);
	}
}
