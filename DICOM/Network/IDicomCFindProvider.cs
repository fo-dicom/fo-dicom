using System;
using System.Collections.Generic;

namespace Dicom.Network {
	public interface IDicomCFindProvider {
		IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request);
	}
}
