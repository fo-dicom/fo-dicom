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
			dimse.SOPClassUID = DicomUID.PatientRootQueryRetrieveInformationModelFIND;
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
			dimse.SOPClassUID = DicomUID.StudyRootQueryRetrieveInformationModelFIND;
			dimse.Dataset.Add(DicomTag.PatientID, patientId);
			dimse.Dataset.Add(DicomTag.PatientName, patientName);
			dimse.Dataset.Add(DicomTag.OtherPatientIDs, String.Empty);
			dimse.Dataset.Add(DicomTag.IssuerOfPatientID, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientSex, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientBirthDate, String.Empty);
			dimse.Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
			dimse.Dataset.Add(DicomTag.ModalitiesInStudy, modalitiesInStudy);
			dimse.Dataset.Add(DicomTag.Modality, modalitiesInStudy);
			dimse.Dataset.Add(DicomTag.StudyID, studyId);
			dimse.Dataset.Add(DicomTag.AccessionNumber, accession);
			dimse.Dataset.Add(DicomTag.StudyDate, studyDateTime);
			dimse.Dataset.Add(DicomTag.StudyTime, studyDateTime);
			dimse.Dataset.Add(DicomTag.StudyDescription, String.Empty);
			dimse.Dataset.Add(DicomTag.NumberOfStudyRelatedSeries, String.Empty);
			dimse.Dataset.Add(DicomTag.NumberOfStudyRelatedInstances, String.Empty);
			return dimse;
		}

		public static DicomCFindRequest CreateSeriesQuery(string studyInstanceUid, string modality = null) {
			var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Series);
			dimse.SOPClassUID = DicomUID.StudyRootQueryRetrieveInformationModelFIND;
			dimse.Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
			dimse.Dataset.Add(DicomTag.SeriesInstanceUID, String.Empty);
			dimse.Dataset.Add(DicomTag.SeriesNumber, String.Empty);
			dimse.Dataset.Add(DicomTag.SeriesDescription, String.Empty);
			dimse.Dataset.Add(DicomTag.Modality, modality);
			dimse.Dataset.Add(DicomTag.SeriesDate, String.Empty);
			dimse.Dataset.Add(DicomTag.SeriesTime, String.Empty);
			dimse.Dataset.Add(DicomTag.NumberOfSeriesRelatedInstances, String.Empty);
			return dimse;
		}

		public static DicomCFindRequest CreateWorklistQuery(string patientId = null, string patientName = null,
															string stationAE = null, string stationName = null, string modality = null,
															DicomDateRange scheduledDateTime = null)
		{
			var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Worklist);
			dimse.SOPClassUID = DicomUID.ModalityWorklistInformationModelFIND;
			dimse.Dataset.Add(DicomTag.PatientID, patientId);
			dimse.Dataset.Add(DicomTag.PatientName, patientName);
			dimse.Dataset.Add(DicomTag.OtherPatientIDs, String.Empty);
			dimse.Dataset.Add(DicomTag.IssuerOfPatientID, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientSex, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientWeight, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientBirthDate, String.Empty);
			dimse.Dataset.Add(DicomTag.MedicalAlerts, String.Empty);
			dimse.Dataset.Add(DicomTag.PregnancyStatus, new ushort[0]);
			dimse.Dataset.Add(DicomTag.Allergies, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientComments, String.Empty);
			dimse.Dataset.Add(DicomTag.SpecialNeeds, String.Empty);
			dimse.Dataset.Add(DicomTag.PatientState, String.Empty);
			dimse.Dataset.Add(DicomTag.CurrentPatientLocation, String.Empty);
			dimse.Dataset.Add(DicomTag.InstitutionName, String.Empty);
			dimse.Dataset.Add(DicomTag.AdmissionID, String.Empty);
			dimse.Dataset.Add(DicomTag.AccessionNumber, String.Empty);
			dimse.Dataset.Add(DicomTag.ReferringPhysicianName, String.Empty);
			dimse.Dataset.Add(DicomTag.AdmittingDiagnosesDescription, String.Empty);
			dimse.Dataset.Add(DicomTag.RequestingPhysician, String.Empty);
			dimse.Dataset.Add(DicomTag.StudyInstanceUID, String.Empty);
			dimse.Dataset.Add(DicomTag.StudyDescription, String.Empty);
			dimse.Dataset.Add(DicomTag.StudyID, String.Empty);
			dimse.Dataset.Add(DicomTag.ReasonForTheRequestedProcedure, String.Empty);
			dimse.Dataset.Add(DicomTag.StudyDate, String.Empty);
			dimse.Dataset.Add(DicomTag.StudyTime, String.Empty);

			dimse.Dataset.Add(DicomTag.RequestedProcedureID, String.Empty);
			dimse.Dataset.Add(DicomTag.RequestedProcedureDescription, String.Empty);
			dimse.Dataset.Add(DicomTag.RequestedProcedurePriority, String.Empty);
			dimse.Dataset.Add(new DicomSequence(DicomTag.RequestedProcedureCodeSequence));
			dimse.Dataset.Add(new DicomSequence(DicomTag.ReferencedStudySequence));

			dimse.Dataset.Add(new DicomSequence(DicomTag.ProcedureCodeSequence));

			var sps = new DicomDataset();
			sps.Add(DicomTag.ScheduledStationAETitle, stationAE);
			sps.Add(DicomTag.ScheduledStationName, stationName);
			sps.Add(DicomTag.ScheduledProcedureStepStartDate, scheduledDateTime);
			sps.Add(DicomTag.ScheduledProcedureStepStartTime, scheduledDateTime);
			sps.Add(DicomTag.Modality, modality);
			sps.Add(DicomTag.ScheduledPerformingPhysicianName, String.Empty);
			sps.Add(DicomTag.ScheduledProcedureStepDescription, String.Empty);
			sps.Add(new DicomSequence(DicomTag.ScheduledProtocolCodeSequence));
			sps.Add(DicomTag.ScheduledProcedureStepLocation, String.Empty);
			sps.Add(DicomTag.ScheduledProcedureStepID, String.Empty);
			sps.Add(DicomTag.RequestedContrastAgent, String.Empty);
			sps.Add(DicomTag.PreMedication, String.Empty);
			sps.Add(DicomTag.AnatomicalOrientationType, String.Empty);
			dimse.Dataset.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps));

			return dimse;
		}
	}
}
