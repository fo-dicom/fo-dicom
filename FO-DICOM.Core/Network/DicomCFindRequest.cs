// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of a C-FIND request.
    /// </summary>
    public sealed class DicomCFindRequest : DicomPriorityRequest
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindRequest"/> class.
        /// </summary>
        /// <param name="command">C-FIND RQ command.</param>
        public DicomCFindRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindRequest"/> class.
        /// </summary>
        /// <param name="level">Query&#47;Retrieve level.</param>
        /// <param name="priority">Command priority.</param>
        public DicomCFindRequest(DicomQueryRetrieveLevel level, DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CFindRequest, GetAffectedSOPClassUID(level), priority)
        {
            // when creating requests, one may be forced to use invalid UIDs. So turn off validation
            Dataset = new DicomDataset().NotValidated();
            Level = level;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindRequest"/> class.
        /// </summary>
        /// <param name="affectedSopClassUid">Affected SOP Class UID.</param>
        /// <param name="priority">Command priority.</param>
        public DicomCFindRequest(DicomUID affectedSopClassUid, DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CFindRequest, affectedSopClassUid, priority)
        {
            if (affectedSopClassUid.Type != DicomUidType.SOPClass)
            {
                throw new DicomNetworkException($"Overloaded constructor does not support Affected SOP Class UID: {affectedSopClassUid.Name}");
            }

            // when creating requests, one may be forced to use invalid UIDs. So turn off validation
            Dataset = new DicomDataset().NotValidated();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindRequest"/> class.
        /// </summary>
        /// <param name="affectedSopClassUid">Affected SOP Class UID.</param>
        /// <param name="level">Query/Retrieve level.</param>
        /// <param name="priority">Command priority.</param>
        public DicomCFindRequest(DicomUID affectedSopClassUid, DicomQueryRetrieveLevel level, DicomPriority priority = DicomPriority.Medium)
            : this(affectedSopClassUid, priority)
        {
            Level = level;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the query&#47;Retrieve level.
        /// </summary>
        public DicomQueryRetrieveLevel Level
        {
            get => Dataset.GetSingleValueOrDefault(DicomTag.QueryRetrieveLevel, DicomQueryRetrieveLevel.NotApplicable);
            private set
            {
                switch (value)
                {
                    case DicomQueryRetrieveLevel.Patient:
                    case DicomQueryRetrieveLevel.Study:
                    case DicomQueryRetrieveLevel.Series:
                    case DicomQueryRetrieveLevel.Image:
                        Dataset.AddOrUpdate(DicomTag.QueryRetrieveLevel, value.ToString().ToUpperInvariant());
                        break;
                    default:
                        Dataset.Remove(DicomTag.QueryRetrieveLevel);
                        break;
                }
            }
        }

        #endregion

        #region DELEGATES AND EVENTS

        /// <summary>
        /// Delegate for response received event handling.
        /// </summary>
        /// <param name="request">C-FIND request.</param>
        /// <param name="response">C-FIND response.</param>
        public delegate void ResponseDelegate(DicomCFindRequest request, DicomCFindResponse response);

        /// <summary>
        /// Gets or sets the response received event handler.
        /// </summary>
        public ResponseDelegate OnResponseReceived;

        #endregion

        #region METHODS

        /// <summary>
        /// Event handler to perform when response has been received.
        /// </summary>
        /// <param name="service">Associated DICOM service.</param>
        /// <param name="response">C-FIND response.</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomCFindResponse)response);
            }
            catch
            { /* ignore exception */ }
        }

        /// <summary>
        /// Convenience method for creating a C-FIND patient query.
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="patientName">Patient name.</param>
        /// <returns>C-FIND patient query object.</returns>
        public static DicomCFindRequest CreatePatientQuery(string patientId = null, string patientName = null)
        {
            var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Patient);
            dimse.Dataset.Add(DicomTag.PatientID, patientId);
            dimse.Dataset.Add(DicomTag.PatientName, patientName);
            dimse.Dataset.Add(DicomTag.IssuerOfPatientID, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientSex, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientBirthDate, string.Empty);
            return dimse;
        }

        /// <summary>
        /// Convenience method for creating a C-FIND study query.
        /// </summary>
        /// <param name="patientId">Patient ID.</param>
        /// <param name="patientName">Patient name.</param>
        /// <param name="studyDateTime">Time range of studies.</param>
        /// <param name="accession">Accession number.</param>
        /// <param name="studyId">Study ID.</param>
        /// <param name="modalitiesInStudy">Modalities in study.</param>
        /// <param name="studyInstanceUid">Study instance UID.</param>
        /// <returns>C-FIND study query object.</returns>
        public static DicomCFindRequest CreateStudyQuery(
            string patientId = null,
            string patientName = null,
            DicomDateRange studyDateTime = null,
            string accession = null,
            string studyId = null,
            string modalitiesInStudy = null,
            string studyInstanceUid = null)
        {
            var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Study);
            dimse.Dataset.Add(DicomTag.PatientID, patientId);
            dimse.Dataset.Add(DicomTag.PatientName, patientName);
            dimse.Dataset.Add(DicomTag.IssuerOfPatientID, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientSex, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientBirthDate, string.Empty);
            dimse.Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
            dimse.Dataset.Add(DicomTag.ModalitiesInStudy, modalitiesInStudy);
            dimse.Dataset.Add(DicomTag.StudyID, studyId);
            dimse.Dataset.Add(DicomTag.AccessionNumber, accession);
            dimse.Dataset.Add(DicomTag.StudyDate, studyDateTime);
            dimse.Dataset.Add(DicomTag.StudyTime, studyDateTime);
            dimse.Dataset.Add(DicomTag.StudyDescription, string.Empty);
            dimse.Dataset.Add(DicomTag.NumberOfStudyRelatedSeries, string.Empty);
            dimse.Dataset.Add(DicomTag.NumberOfStudyRelatedInstances, string.Empty);
            return dimse;
        }

        /// <summary>
        /// Convenience method for creating a C-FIND series query.
        /// </summary>
        /// <param name="studyInstanceUid">Study instance UID.</param>
        /// <param name="modality">Modality.</param>
        /// <returns>C-FIND series query object.</returns>
        public static DicomCFindRequest CreateSeriesQuery(string studyInstanceUid, string modality = null)
        {
            var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Series);
            dimse.Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
            dimse.Dataset.Add(DicomTag.SeriesInstanceUID, string.Empty);
            dimse.Dataset.Add(DicomTag.SeriesNumber, string.Empty);
            dimse.Dataset.Add(DicomTag.SeriesDescription, string.Empty);
            dimse.Dataset.Add(DicomTag.Modality, modality);
            dimse.Dataset.Add(DicomTag.SeriesDate, string.Empty);
            dimse.Dataset.Add(DicomTag.SeriesTime, string.Empty);
            dimse.Dataset.Add(DicomTag.NumberOfSeriesRelatedInstances, string.Empty);
            return dimse;
        }

        /// <summary>
        /// Convenience method for creating a C-FIND image query.
        /// </summary>
        /// <param name="studyInstanceUid">Study instance UID.</param>
        /// <param name="seriesInstanceUid">Series instance UID.</param>
        /// <param name="modality">Modality.</param>
        /// <returns>C-FIND image query object.</returns>
        public static DicomCFindRequest CreateImageQuery(
            string studyInstanceUid,
            string seriesInstanceUid,
            string modality = null)
        {
            var dimse = new DicomCFindRequest(DicomQueryRetrieveLevel.Image);
            dimse.Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
            dimse.Dataset.Add(DicomTag.SeriesInstanceUID, seriesInstanceUid);
            dimse.Dataset.Add(DicomTag.SOPInstanceUID, string.Empty);
            dimse.Dataset.Add(DicomTag.InstanceNumber, string.Empty);
            dimse.Dataset.Add(DicomTag.Modality, modality);
            return dimse;
        }

        /// <summary>
        /// Convenience method for creating a C-FIND modality worklist query.
        /// </summary>
        /// <param name="patientId">Patient ID.</param>
        /// <param name="patientName">Patient name.</param>
        /// <param name="stationAE">Scheduled station Application Entity Title.</param>
        /// <param name="stationName">Scheduled station name.</param>
        /// <param name="modality">Modality.</param>
        /// <param name="scheduledDateTime">Scheduled procedure step start time.</param>
        /// <returns>C-FIND modality worklist query object.</returns>
        public static DicomCFindRequest CreateWorklistQuery(
            string patientId = null,
            string patientName = null,
            string stationAE = null,
            string stationName = null,
            string modality = null,
            DicomDateRange scheduledDateTime = null)
        {
            var dimse = new DicomCFindRequest(DicomUID.ModalityWorklistInformationModelFind);
            dimse.Dataset.Add(DicomTag.PatientID, patientId);
            dimse.Dataset.Add(DicomTag.PatientName, patientName);
            dimse.Dataset.Add(DicomTag.IssuerOfPatientID, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientSex, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientWeight, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientBirthDate, string.Empty);
            dimse.Dataset.Add(DicomTag.MedicalAlerts, string.Empty);
            dimse.Dataset.Add(DicomTag.PregnancyStatus, Array.Empty<ushort>());
            dimse.Dataset.Add(DicomTag.Allergies, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientComments, string.Empty);
            dimse.Dataset.Add(DicomTag.SpecialNeeds, string.Empty);
            dimse.Dataset.Add(DicomTag.PatientState, string.Empty);
            dimse.Dataset.Add(DicomTag.CurrentPatientLocation, string.Empty);
            dimse.Dataset.Add(DicomTag.InstitutionName, string.Empty);
            dimse.Dataset.Add(DicomTag.AdmissionID, string.Empty);
            dimse.Dataset.Add(DicomTag.AccessionNumber, string.Empty);
            dimse.Dataset.Add(DicomTag.ReferringPhysicianName, string.Empty);
            dimse.Dataset.Add(DicomTag.AdmittingDiagnosesDescription, string.Empty);
            dimse.Dataset.Add(DicomTag.RequestingPhysician, string.Empty);
            dimse.Dataset.Add(DicomTag.StudyInstanceUID, string.Empty);
            dimse.Dataset.Add(DicomTag.StudyDescription, string.Empty);
            dimse.Dataset.Add(DicomTag.StudyID, string.Empty);
            dimse.Dataset.Add(DicomTag.ReasonForTheRequestedProcedure, string.Empty);
            dimse.Dataset.Add(DicomTag.StudyDate, string.Empty);
            dimse.Dataset.Add(DicomTag.StudyTime, string.Empty);

            dimse.Dataset.Add(DicomTag.RequestedProcedureID, string.Empty);
            dimse.Dataset.Add(DicomTag.RequestedProcedureDescription, string.Empty);
            dimse.Dataset.Add(DicomTag.RequestedProcedurePriority, string.Empty);
            dimse.Dataset.Add(new DicomSequence(DicomTag.RequestedProcedureCodeSequence));
            dimse.Dataset.Add(new DicomSequence(DicomTag.ReferencedStudySequence));

            dimse.Dataset.Add(new DicomSequence(DicomTag.ProcedureCodeSequence));

            var sps = new DicomDataset
            {
                { DicomTag.ScheduledStationAETitle, stationAE },
                { DicomTag.ScheduledStationName, stationName },
                { DicomTag.ScheduledProcedureStepStartDate, scheduledDateTime },
                { DicomTag.ScheduledProcedureStepStartTime, scheduledDateTime },
                { DicomTag.Modality, modality },
                { DicomTag.ScheduledPerformingPhysicianName, string.Empty },
                { DicomTag.ScheduledProcedureStepDescription, string.Empty },
                new DicomSequence(DicomTag.ScheduledProtocolCodeSequence),
                { DicomTag.ScheduledProcedureStepLocation, string.Empty },
                { DicomTag.ScheduledProcedureStepID, string.Empty },
                { DicomTag.RequestedContrastAgent, string.Empty },
                { DicomTag.PreMedication, string.Empty },
                { DicomTag.AnatomicalOrientationType, string.Empty }
            };
            dimse.Dataset.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps));

            return dimse;
        }

        /// <summary>
        /// Gets affected SOP class UID corresponding to specified Query&#47;Retrieve <paramref name="level"/>.
        /// </summary>
        /// <param name="level">Query&#47;Retrieve level.</param>
        /// <returns>Affected SOP class UID corresponding to specified Query&#47;Retrieve <paramref name="level"/>.</returns>
        private static DicomUID GetAffectedSOPClassUID(DicomQueryRetrieveLevel level)
        {
            switch (level)
            {
                case DicomQueryRetrieveLevel.Patient:
                    return DicomUID.PatientRootQueryRetrieveInformationModelFind;
                case DicomQueryRetrieveLevel.Study:
                case DicomQueryRetrieveLevel.Series:
                case DicomQueryRetrieveLevel.Image:
                    return DicomUID.StudyRootQueryRetrieveInformationModelFind;
#pragma warning disable CS0618
                // While this QR level is obsolete, we must still support it
                case DicomQueryRetrieveLevel.Worklist:
#pragma warning restore CS0618
                case DicomQueryRetrieveLevel.NotApplicable:
                    return DicomUID.ModalityWorklistInformationModelFind;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        #endregion
    }
}
