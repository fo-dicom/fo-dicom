﻿// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Text;

    /// <summary>
    /// Representation of an N-ACTION response message.
    /// </summary>
    public sealed class DicomNActionResponse : DicomResponse
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNActionResponse"/> class.
        /// </summary>
        /// <param name="command">
        /// A dataset representing the response command.
        /// </param>
        public DicomNActionResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNActionResponse"/> class.
        /// </summary>
        /// <param name="request">
        /// The associated N-ACTION request.
        /// </param>
        /// <param name="status">
        /// The response status.
        /// </param>
        public DicomNActionResponse(DicomNActionRequest request, DicomStatus status)
            : base(request, status)
        {
            if (request.HasSOPInstanceUID)
            {
                this.SOPInstanceUID = request.SOPInstanceUID;
            }

            this.ActionTypeID = request.ActionTypeID;
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
        /// Gets the action type ID.
        /// </summary>
        public ushort ActionTypeID
        {
            get
            {
                return Command.Get<ushort>(DicomTag.ActionTypeID);
            }
            private set
            {
                Command.Add(DicomTag.ActionTypeID, value);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Formatted output.
        /// </summary>
        /// <returns>Formatted output of the N-ACTION response.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
            if (Command.Contains(DicomTag.ActionTypeID)) sb.AppendFormat("\n\t\tAction Type:	{0:x4}", ActionTypeID);
            if (Status.State != DicomState.Pending && Status.State != DicomState.Success)
            {
                if (!String.IsNullOrEmpty(Status.ErrorComment)) sb.AppendFormat("\n\t\tError:		{0}", Status.ErrorComment);
            }
            return sb.ToString();
        }

        #endregion
    }
}
