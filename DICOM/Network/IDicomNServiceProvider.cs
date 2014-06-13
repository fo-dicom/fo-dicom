using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public interface IDicomNServiceProvider {
		DicomNActionResponse OnNActionRequest(DicomNActionRequest request);
		DicomNCreateResponse OnNCreateRequest(DicomNCreateRequest request);
		DicomNDeleteResponse OnNDeleteRequest(DicomNDeleteRequest request);
		DicomNEventReportResponse OnNEventReportRequest(DicomNEventReportRequest request);
		DicomNGetResponse OnNGetRequest(DicomNGetRequest request);
		DicomNSetResponse OnNSetRequest(DicomNSetRequest request);
	}
}
