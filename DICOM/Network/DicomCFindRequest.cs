using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomCFindRequest : DicomRequest {
		public DicomCFindRequest(DicomDataset command) : base(command) {
		}

		public DicomCFindRequest(DicomQueryRetrieveLevel level, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CFindRequest, DicomUID.StudyRootQueryRetrieveInformationModelFIND, priority) {
			Dataset = new DicomDataset();
			Level = level;
		}

		public DicomQueryRetrieveLevel Level {
			get { return Dataset.Get<DicomQueryRetrieveLevel>(DicomTag.QueryRetrieveLevel); }
			set {
				Dataset.Remove(DicomTag.QueryRetrieveLevel);
				if (value != DicomQueryRetrieveLevel.Worklist)
					Dataset.Add(DicomTag.QueryRetrieveLevel, value.ToString().ToUpper());
			}
		}

		public delegate void ResponseDelegate(DicomCFindRequest request, DicomCFindResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomCFindResponse)response);
			} catch {
			}
		}

		public static DicomCFindRequest CreatePatientQuery(string patientId = null, string patientName = null) {
			var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Patient);
			dimse.AffectedSOPClassUID = DicomUID.PatientRootQueryRetrieveInformationModelFIND;
			dimse.Dataset.Add(DicomTag.PatientID, patientId);
			dimse.Dataset.Add(DicomTag.PatientName, patientName);
			dimse.Dataset.Add(DicomTag.OtherPatientIDs, String.Empty);
			dimse.Dataset.Add(DicomTag.IssuerOfPatientID, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientSex, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientBirthDate, String.Empty);
			return dimse;
		}

		public static DicomCFindRequest CreateStudyQuery(string patientId = null, string patientName = null, 
			DicomDateRange studyDateTime = null, string accession = null, string studyId = null, 
			string modalitiesInStudy = null, string studyInstanceUid = null)
		{
			var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Study);
			dimse.AffectedSOPClassUID = DicomUID.StudyRootQueryRetrieveInformationModelFIND;
			dimse.Dataset.Add(DicomTag.PatientID, patientId);
			dimse.Dataset.Add(DicomTag.PatientName, patientName);
			dimse.Dataset.Add(DicomTag.OtherPatientIDs, String.Empty);
			dimse.Dataset.Add(DicomTag.IssuerOfPatientID, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientSex, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientBirthDate, String.Empty);
			dimse.Dataset.Add(DicomTag.StudyInstanceUID, DicomUID.Parse(studyInstanceUid));
			dimse.Dataset.Add(DicomTag.ModalitiesInStudy, modalitiesInStudy);
			dimse.Dataset.Add(DicomTag.StudyID, studyId);
			dimse.Dataset.Add(DicomTag.AccessionNumber, accession);
			dimse.Dataset.Add(DicomTag.StudyDate, studyDateTime);
			dimse.Dataset.Add(DicomTag.StudyTime, studyDateTime);
			dimse.Dataset.Add(DicomTag.NumberOfStudyRelatedSeries, String.Empty);
			dimse.Dataset.Add(DicomTag.NumberOfStudyRelatedSeries, String.Empty);
			return dimse;
		}

		public static DicomCFindRequest CreateSeriesQuery(string studyInstanceUid) {
			var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Series);
			dimse.AffectedSOPClassUID = DicomUID.StudyRootQueryRetrieveInformationModelFIND;
			dimse.Dataset.Add(DicomTag.StudyInstanceUID, DicomUID.Parse(studyInstanceUid));
			dimse.Dataset.Add(DicomTag.SeriesInstanceUID, DicomUID.Parse(String.Empty));
			dimse.Dataset.Add(DicomTag.SeriesNumber, String.Empty);
			dimse.Dataset.Add(DicomTag.SeriesDescription, String.Empty);
			dimse.Dataset.Add(DicomTag.Modality, String.Empty);
			dimse.Dataset.Add(DicomTag.SeriesDate, String.Empty);
			dimse.Dataset.Add(DicomTag.SeriesTime, String.Empty);
			dimse.Dataset.Add(DicomTag.NumberOfSeriesRelatedInstances, String.Empty);
			return dimse;
		}
	}
}
