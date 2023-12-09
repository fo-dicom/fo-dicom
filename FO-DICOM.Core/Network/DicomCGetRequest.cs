// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of a C-GET request.
    /// </summary>
    public sealed class DicomCGetRequest : DicomPriorityRequest
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomCGetRequest"/> class.
        /// </summary>
        /// <param name="command">
        /// The command associated with a C-GET request.
        /// </param>
        public DicomCGetRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomCGetRequest"/> class.
        /// </summary>
        /// <param name="studyInstanceUid">
        /// The Study Instance UID confining the C-GET operation.
        /// </param>
        /// <param name="priority">
        /// The priority of the C-GET operation.
        /// </param>
        public DicomCGetRequest(
            string studyInstanceUid,
            DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CGetRequest, DicomUID.StudyRootQueryRetrieveInformationModelGet, priority)
        {
            // when creating requests, one may be forced to use invalid UIDs. So turn off validation
            Dataset = new DicomDataset().NotValidated();
            Level = DicomQueryRetrieveLevel.Study;
            Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomCGetRequest"/> class.
        /// </summary>
        /// <param name="studyInstanceUid">
        /// The Study Instance UID confining the C-GET operation.
        /// </param>
        /// <param name="seriesInstanceUid">
        /// The Series Instance UID confining the C-GET operation.
        /// </param>
        /// <param name="priority">
        /// The priority of the C-GET operation.
        /// </param>
        public DicomCGetRequest(
            string studyInstanceUid,
            string seriesInstanceUid,
            DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CGetRequest, DicomUID.StudyRootQueryRetrieveInformationModelGet, priority)
        {
            // when creating requests, one may be forced to use invalid UIDs. So turn off validation
            Dataset = new DicomDataset().NotValidated();
            Level = DicomQueryRetrieveLevel.Series;
            Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
            Dataset.Add(DicomTag.SeriesInstanceUID, seriesInstanceUid);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomCGetRequest"/> class.
        /// </summary>
        /// <param name="studyInstanceUid">
        /// The Study Instance UID confining the C-GET operation.
        /// </param>
        /// <param name="seriesInstanceUid">
        /// The Series Instance UID confining the C-GET operation.
        /// </param>
        /// <param name="sopInstanceUid">
        /// The SOP Instance UID confining the C-GET operation.
        /// </param>
        /// <param name="priority">
        /// The priority of the C-GET operation.
        /// </param>
        public DicomCGetRequest(
            string studyInstanceUid,
            string seriesInstanceUid,
            string sopInstanceUid,
            DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CGetRequest, DicomUID.StudyRootQueryRetrieveInformationModelGet, priority)
        {
            // when creating requests, one may be forced to use invalid UIDs. So turn off validation
            Dataset = new DicomDataset().NotValidated();
            Level = DicomQueryRetrieveLevel.Image;
            Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
            Dataset.Add(DicomTag.SeriesInstanceUID, seriesInstanceUid);
            Dataset.Add(DicomTag.SOPInstanceUID, sopInstanceUid);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the Query/Retrieve level of the C-GET operation.
        /// </summary>
        public DicomQueryRetrieveLevel Level
        {
            get => Dataset.GetSingleValue<DicomQueryRetrieveLevel>(DicomTag.QueryRetrieveLevel);
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

        #region DELEGATES

        /// <summary>
        /// Represents a callback method to be executed when the response for the DIMSE C-GET request is received.
        /// </summary>
        /// <param name="request">Sent DIMSE C-GET request.</param>
        /// <param name="response">Received DIMSE C-GET response.</param>
        public delegate void ResponseDelegate(DicomCGetRequest request, DicomCGetResponse response);

        #endregion

        #region PUBLIC FIELDS

        /// <summary>
        /// Delegate to be executed when the response for the DIMSE C-GET request is received.
        /// </summary>
        public ResponseDelegate OnResponseReceived;

        #endregion

        #region METHODS

        /// <summary>
        /// Internal. Executes the DICOM C-GET response callback.
        /// </summary>
        /// <param name="service">DICOM SCP implementation</param>
        /// <param name="response">Received DIMSE (C-GET) response</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomCGetResponse)response);
            }
            catch
            {
                // ignore exception
            }
        }

        #endregion
    }
}
