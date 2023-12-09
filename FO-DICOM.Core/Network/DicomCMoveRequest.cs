// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of a C-MOVE request.
    /// </summary>
    public sealed class DicomCMoveRequest : DicomPriorityRequest
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Inititalizes an instance of the <see cref="DicomCMoveRequest"/> class.
        /// </summary>
        /// <param name="command">Request command.</param>
        public DicomCMoveRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCMoveRequest"/> class for a specific study.
        /// </summary>
        /// <param name="destinationAe">Move destination Application Entity Title.</param>
        /// <param name="studyInstanceUid">Study instance UID.</param>
        /// <param name="priority">Request priority.</param>
        public DicomCMoveRequest(
            string destinationAe,
            string studyInstanceUid,
            DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CMoveRequest, DicomUID.StudyRootQueryRetrieveInformationModelMove, priority)
        {
            DestinationAE = destinationAe;
            // when creating requests, one may be forced to use invalid UIDs. So turn off validation
            Dataset = new DicomDataset().NotValidated();
            Level = DicomQueryRetrieveLevel.Study;
            Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCMoveRequest"/> class for a specific series.
        /// </summary>
        /// <param name="destinationAe">Move destination Application Entity Title.</param>
        /// <param name="studyInstanceUid">Study instance UID.</param>
        /// <param name="seriesInstanceUid">Series instance UID.</param>
        /// <param name="priority">Request priority.</param>
        public DicomCMoveRequest(
            string destinationAe,
            string studyInstanceUid,
            string seriesInstanceUid,
            DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CMoveRequest, DicomUID.StudyRootQueryRetrieveInformationModelMove, priority)
        {
            DestinationAE = destinationAe;
            // when creating requests, one may be forced to use invalid UIDs. So turn off validation
            Dataset = new DicomDataset().NotValidated();

            Level = DicomQueryRetrieveLevel.Series;
            Dataset.Add(DicomTag.StudyInstanceUID, studyInstanceUid);
            Dataset.Add(DicomTag.SeriesInstanceUID, seriesInstanceUid);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCMoveRequest"/> class for a specific image.
        /// </summary>
        /// <param name="destinationAe">Move destination Application Entity Title.</param>
        /// <param name="studyInstanceUid">Study instance UID.</param>
        /// <param name="seriesInstanceUid">Series instance UID.</param>
        /// <param name="sopInstanceUid">SOP instance UID.</param>
        /// <param name="priority">Request priority.</param>
        public DicomCMoveRequest(
            string destinationAe,
            string studyInstanceUid,
            string seriesInstanceUid,
            string sopInstanceUid,
            DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CMoveRequest, DicomUID.StudyRootQueryRetrieveInformationModelMove, priority)
        {
            DestinationAE = destinationAe;
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
        /// Gets the Query&#47;Retrieve level.
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

        /// <summary>
        /// Gets the move destination Application Entity Title.
        /// </summary>
        public string DestinationAE
        {
            get => Command.GetSingleValue<string>(DicomTag.MoveDestination);
            private set => Command.AddOrUpdate(DicomTag.MoveDestination, value);
        }

        #endregion

        #region DELEGATES AND EVENTS

        /// <summary>
        /// Delegate representing a C-MOVE RSP received event handler.
        /// </summary>
        /// <param name="request">C-MOVE RQ.</param>
        /// <param name="response">C-MOVE RSP.</param>
        public delegate void ResponseDelegate(DicomCMoveRequest request, DicomCMoveResponse response);

        /// <summary>
        /// Gets or sets the handler for the C-MOVE response received event.
        /// </summary>
        public ResponseDelegate OnResponseReceived;

        #endregion

        #region METHODS

        /// <summary>
        /// Invoke the event handler upon receiving a C-MOVE response.
        /// </summary>
        /// <param name="service">Associated DICOM service.</param>
        /// <param name="response">C-MOVE response.</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomCMoveResponse)response);
            }
            catch
            {
                // ignore exception
            }
        }

        #endregion
    }
}
