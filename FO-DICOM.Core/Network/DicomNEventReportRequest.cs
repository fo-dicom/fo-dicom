// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Text;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Representation of an N-EVENTREPORT request.
    /// </summary>
    public sealed class DicomNEventReportRequest : DicomRequest
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNEventReportRequest"/> class.
        /// </summary>
        /// <param name="command">N-EVENTREPORT request command.</param>
        public DicomNEventReportRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomNEventReportRequest"/> class.
        /// </summary>
        /// <param name="affectedClassUid">Affected SOP class UID.</param>
        /// <param name="affectedInstanceUid">Affected SOP instance UID.</param>
        /// <param name="eventTypeId">Event type ID.</param>
        public DicomNEventReportRequest(
            DicomUID affectedClassUid,
            DicomUID affectedInstanceUid,
            ushort eventTypeId)
            : base(DicomCommandField.NEventReportRequest, affectedClassUid)
        {
            SOPInstanceUID = affectedInstanceUid;
            EventTypeID = eventTypeId;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the affected SOP instance UID.
        /// </summary>
        public DicomUID SOPInstanceUID
        {
            get => Command.GetSingleValue<DicomUID>(DicomTag.AffectedSOPInstanceUID);
            private set => Command.AddOrUpdate(DicomTag.AffectedSOPInstanceUID, value);
        }

        /// <summary>
        /// Gets the event type ID.
        /// </summary>
        public ushort EventTypeID
        {
            get => Command.GetSingleValue<ushort>(DicomTag.EventTypeID);
            private set => Command.AddOrUpdate(DicomTag.EventTypeID, value);
        }

        #endregion

        #region DELEGATES AND EVENTS

        /// <summary>
        /// Delegate representing a N-EVENTREPORT RSP received event handler.
        /// </summary>
        /// <param name="request">N-EVENTREPORT RQ.</param>
        /// <param name="response">N-EVENTREPORT RSP.</param>
        public delegate void ResponseDelegate(DicomNEventReportRequest request, DicomNEventReportResponse response);

        /// <summary>
        /// Gets or sets the handler for the N-EVENTREPORT response received event.
        /// </summary>
        public ResponseDelegate OnResponseReceived;

        #endregion

        #region METHODS

        /// <summary>
        /// Invoke the event handler upon receiving a N-EVENTREPORT response.
        /// </summary>
        /// <param name="service">Associated DICOM service.</param>
        /// <param name="response">N-EVENTREPORT response.</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomNEventReportResponse)response);
            }
            catch
            {
                // ignore exception
            }
        }

        /// <summary>
        /// Formatted output.
        /// </summary>
        /// <returns>Formatted output of the N-EVENTREPORT response message.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]", ToString(Type), MessageID);
            if (Command.Contains(DicomTag.EventTypeID))
            {
                sb.AppendFormat("\n\t\tEvent Type:\t{0:x4}", EventTypeID);
            }

            return sb.ToString();
        }

        #endregion
    }
}
