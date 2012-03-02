using System;
using System.Collections.Generic;

namespace Dicom.Network {
	public interface IDicomCMoveProvider {
		IEnumerable<DicomCMoveResponse> OnCMoveRequest(DicomCMoveRequest request);
	}
}
