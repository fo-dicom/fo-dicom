using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCMoveRequest : DicomRequest {
		public DicomCMoveRequest(DicomDataset command) : base(command) {
		}

		public DicomCMoveRequest(string destinationAe, string studyInstanceUid, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CMoveRequest, DicomUID.StudyRootQueryRetrieveInformationModelMOVE, priority) {
			DestinationAE = destinationAe;
			Dataset = new DicomDataset();
			Level = DicomQueryRetrieveLevel.Study;
			Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
		}

		public DicomCMoveRequest(string destinationAe, string studyInstanceUid, string seriesInstanceUid, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CMoveRequest, DicomUID.StudyRootQueryRetrieveInformationModelMOVE, priority) {
			DestinationAE = destinationAe;
			Dataset = new DicomDataset();
			Level = DicomQueryRetrieveLevel.Series;
			Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
			Dataset.Add(DicomTag.SeriesInstanceUID, seriesInstanceUid);
		}

		public DicomCMoveRequest(string destinationAe, string studyInstanceUid, string seriesInstanceUid, string sopInstanceUid, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CMoveRequest, DicomUID.StudyRootQueryRetrieveInformationModelMOVE, priority) {
			DestinationAE = destinationAe;
			Dataset = new DicomDataset();
			Level = DicomQueryRetrieveLevel.Image;
			Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
			Dataset.Add(DicomTag.SeriesInstanceUID, seriesInstanceUid);
			Dataset.Add(DicomTag.SOPInstanceUID, sopInstanceUid);
		}

		public DicomQueryRetrieveLevel Level {
			get { return Dataset.Get<DicomQueryRetrieveLevel>(DicomTag.QueryRetrieveLevel); }
			set {
				Dataset.Remove(DicomTag.QueryRetrieveLevel);
				if (value != DicomQueryRetrieveLevel.Worklist)
					Dataset.Add(DicomTag.QueryRetrieveLevel, value.ToString().ToUpper());
			}
		}

		public string DestinationAE {
			get { return Command.Get<string>(DicomTag.MoveDestination); }
			set { Command.Add(DicomTag.MoveDestination, value); }
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
