using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCMoveRequest : DicomRequest {
		public DicomCMoveRequest(DicomDataset command) : base(command) {
		}

		public DicomCMoveRequest(string studyInstanceUid, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CMoveRequest, DicomUID.VerificationSOPClass, priority) {
			AffectedSOPClassUID = DicomUID.StudyRootQueryRetrieveInformationModelMOVE;
			Dataset = new DicomDataset();
			Level = DicomQueryRetrieveLevel.Study;
			Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
		}

		public DicomCMoveRequest(string studyInstanceUid, string seriesInstanceUid, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CMoveRequest, DicomUID.VerificationSOPClass, priority) {
			AffectedSOPClassUID = DicomUID.StudyRootQueryRetrieveInformationModelMOVE;
			Dataset = new DicomDataset();
			Level = DicomQueryRetrieveLevel.Series;
			Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
			Dataset.Add(DicomTag.SeriesInstanceUID, seriesInstanceUid);
		}

		public DicomQueryRetrieveLevel Level {
			get { return Dataset.Get<DicomQueryRetrieveLevel>(DicomTag.QueryRetrieveLevel); }
			set {
				Dataset.Remove(DicomTag.QueryRetrieveLevel);
				if (value != DicomQueryRetrieveLevel.Worklist)
					Dataset.Add(DicomTag.QueryRetrieveLevel, value.ToString().ToUpper());
			}
		}

		public delegate void ResponseDelegate(DicomCMoveRequest request, DicomCMoveResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomCMoveResponse)response);
			} catch {
			}
		}
	}
}
