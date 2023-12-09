// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of the N-CREATE request.
    /// </summary>
    public sealed class DicomNCreateRequest : DicomRequest
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNCreateRequest"/> class.
        /// </summary>
        /// <param name="command">N-CREATE request command.</param>
        public DicomNCreateRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNCreateRequest"/> class.
        /// </summary>
        /// <param name="affectedClassUid">Affected SOP class UID.</param>
        /// <param name="affectedInstanceUid">Affected SOP instance UID.</param>
        public DicomNCreateRequest(
            DicomUID affectedClassUid,
            DicomUID affectedInstanceUid)
            : base(DicomCommandField.NCreateRequest, affectedClassUid)
        {
            SOPInstanceUID = affectedInstanceUid;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the affected SOP instance UID.
        /// </summary>
        public DicomUID SOPInstanceUID
        {
            get => Command.GetSingleValueOrDefault<DicomUID>(DicomTag.AffectedSOPInstanceUID, null);
            private set => Command.AddOrUpdate(DicomTag.AffectedSOPInstanceUID, value);
        }

        #endregion

        #region DELEGATES AND EVENTS

        /// <summary>
        /// Delegate representing a N-CREATE RSP received event handler.
        /// </summary>
        /// <param name="request">N-CREATE RQ.</param>
        /// <param name="response">N-CREATE RSP.</param>
        public delegate void ResponseDelegate(DicomNCreateRequest request, DicomNCreateResponse response);

        /// <summary>
        /// Gets or sets the handler for the N-CREATE response received event.
        /// </summary>
        public ResponseDelegate OnResponseReceived;

        #endregion

        #region METHODS

        /// <summary>
        /// Invoke the event handler upon receiving a N-CREATE response.
        /// </summary>
        /// <param name="service">Associated DICOM service.</param>
        /// <param name="response">N-CREATE response.</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomNCreateResponse)response);
            }
            catch
            { /* ignore exception */ }
        }

        #endregion
    }
}
