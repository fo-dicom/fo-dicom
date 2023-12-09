// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Text;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Representation of an N-ACTION request.
    /// </summary>
    public sealed class DicomNActionRequest : DicomRequest
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNActionRequest"/> class.
        /// </summary>
        /// <param name="command">N-ACTION request command.</param>
        public DicomNActionRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNActionRequest"/> class.
        /// </summary>
        /// <param name="requestedClassUid">Requested SOP class UID.</param>
        /// <param name="requestedInstanceUid">Requested SOP instance UID.</param>
        /// <param name="actionTypeId">Action type ID.</param>
        public DicomNActionRequest(
            DicomUID requestedClassUid,
            DicomUID requestedInstanceUid,
            ushort actionTypeId)
            : base(DicomCommandField.NActionRequest, requestedClassUid)
        {
            SOPInstanceUID = requestedInstanceUid;
            ActionTypeID = actionTypeId;
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
        /// Gets the action type ID.
        /// </summary>
        public ushort ActionTypeID
        {
            get => Command.GetSingleValue<ushort>(DicomTag.ActionTypeID);
            private set => Command.AddOrUpdate(DicomTag.ActionTypeID, value);
        }

        #endregion

        #region DELEGATES AND EVENTS

        /// <summary>
        /// Delegate representing a N-ACTION RSP received event handler.
        /// </summary>
        /// <param name="request">N-ACTION RQ.</param>
        /// <param name="response">N-ACTION RSP.</param>
        public delegate void ResponseDelegate(DicomNActionRequest request, DicomNActionResponse response);

        /// <summary>
        /// Gets or sets the handler for the N-ACTION response received event.
        /// </summary>
        public ResponseDelegate OnResponseReceived;

        #endregion

        #region METHODS

        /// <summary>
        /// Invoke the event handler upon receiving a N-ACTION response.
        /// </summary>
        /// <param name="service">Associated DICOM service.</param>
        /// <param name="response">N-ACTION response.</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomNActionResponse)response);
            }
            catch
            {
                // ignore exception
            }
        }

        /// <summary>
        /// Formatted output.
        /// </summary>
        /// <returns>Formatted output of the N-ACTION request.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]", ToString(Type), MessageID);
            if (Command.Contains(DicomTag.ActionTypeID))
            {
                sb.AppendFormat("\n\t\tAction Type:\t{0:x4}", ActionTypeID);
            }

            return sb.ToString();
        }

        #endregion
    }
}
