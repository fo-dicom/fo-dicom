// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Text;

    /// <summary>
    /// Representation of an N-EVENTREPORT response message.
    /// </summary>
    public sealed class DicomNEventReportResponse : DicomResponse
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNEventReportResponse"/> class.
        /// </summary>
        /// <param name="command">
        /// The dataset representing the N-EVENTREPORT response command.
        /// </param>
        public DicomNEventReportResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNEventReportResponse"/> class.
        /// </summary>
        /// <param name="request">
        /// The request associated with the N-EVENTREPORT response.
        /// </param>
        /// <param name="status">
        /// The response status.
        /// </param>
        public DicomNEventReportResponse(DicomNEventReportRequest request, DicomStatus status)
            : base(request, status)
        {
            if (request.HasSOPInstanceUID)
            {
                this.SOPInstanceUID = request.SOPInstanceUID;
            }

            this.EventTypeID = request.EventTypeID;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the affected SOP instance UID.
        /// </summary>
        public DicomUID SOPInstanceUID
        {
            get
            {
                return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID);
            }
            private set
            {
                Command.Add(DicomTag.AffectedSOPInstanceUID, value);
            }
        }

        /// <summary>
        /// Gets the event type ID.
        /// </summary>
        public ushort EventTypeID
        {
            get
            {
                return Command.Get<ushort>(DicomTag.EventTypeID);
            }
            private set
            {
                Command.Add(DicomTag.EventTypeID, value);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Formatted output of the N-EVENTREPORT response message.
        /// </summary>
        /// <returns>Formatted output of the N-EVENTREPORT response message.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
            sb.AppendFormat("\n\t\tEvent Type:	{0:x4}", EventTypeID);
            if (Status.State != DicomState.Pending && Status.State != DicomState.Success)
            {
                if (!String.IsNullOrEmpty(Status.ErrorComment)) sb.AppendFormat("\n\t\tError:		{0}", Status.ErrorComment);
            }
            return sb.ToString();
        }

        #endregion
    }
}
