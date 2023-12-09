// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of an N-GET request.
    /// </summary>
    public sealed class DicomNGetRequest : DicomRequest
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNGetRequest"/> class.
        /// </summary>
        /// <param name="command">N-GET request command dataset.</param>
        public DicomNGetRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNGetRequest"/> class.
        /// </summary>
        /// <param name="requestedClassUid">Requested SOP class UID.</param>
        /// <param name="requestedInstanceUid">Requested SOP instance UID.</param>
        public DicomNGetRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid)
            : base(DicomCommandField.NGetRequest, requestedClassUid)
        {
            SOPInstanceUID = requestedInstanceUid;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the requested SOP instance UID.
        /// </summary>
        public DicomUID SOPInstanceUID
        {
            get => Command.GetSingleValue<DicomUID>(DicomTag.RequestedSOPInstanceUID);
            private set => Command.AddOrUpdate(DicomTag.RequestedSOPInstanceUID, value);
        }

        /// <summary>
        /// Gets the list of attribute identifiers.
        /// </summary>
        public DicomTag[] Attributes
        {
            get => Command.TryGetValues<DicomTag>(DicomTag.AttributeIdentifierList, out DicomTag[] dummy) ? dummy : null;
            private set => Command.AddOrUpdate(DicomTag.AttributeIdentifierList, value);
        }

        #endregion

        #region DELEGATES AND EVENTS

        /// <summary>
        /// Delegate representing a N-GET RSP received event handler.
        /// </summary>
        /// <param name="request">N-GET RQ.</param>
        /// <param name="response">N-GET RSP.</param>
        public delegate void ResponseDelegate(DicomNGetRequest request, DicomNGetResponse response);

        /// <summary>
        /// Gets or sets the handler for the N-GET response received event.
        /// </summary>
        public ResponseDelegate OnResponseReceived;

        #endregion

        #region METHODS

        /// <summary>
        /// Invoke the event handler upon receiving a N-GET response.
        /// </summary>
        /// <param name="service">Associated DICOM service.</param>
        /// <param name="response">N-GET response.</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomNGetResponse)response);
            }
            catch
            {
                // ignore exception
            }
        }

        #endregion
    }
}
