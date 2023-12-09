// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Base class for DIMSE-C and DIMSE-N response items.
    /// </summary>
    public abstract class DicomResponse : DicomMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DicomResponse"/> class.
        /// </summary>
        /// <param name="command">
        /// The command dataset.
        /// </param>
        protected DicomResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomResponse"/> class.
        /// </summary>
        /// <param name="request">
        /// The request initiating the response.
        /// </param>
        /// <param name="status">
        /// Response status.
        /// </param>
        protected DicomResponse(DicomRequest request, DicomStatus status)
        {
            PresentationContext = request.PresentationContext;

            Type = (DicomCommandField)(0x8000 | (int)request.Type);
            SOPClassUID = request.SOPClassUID;
            RequestMessageID = request.MessageID;
            Status = status;
        }

        /// <summary>
        /// Gets or sets the message ID of the request being responded to.
        /// </summary>
        public ushort RequestMessageID
        {
            get => Command.GetSingleValue<ushort>(DicomTag.MessageIDBeingRespondedTo);
            set => Command.AddOrUpdate(DicomTag.MessageIDBeingRespondedTo, value);
        }

        /// <summary>
        /// Gets or sets the response status.
        /// </summary>
        public DicomStatus Status
        {
            get
            {
                var status = DicomStatus.Lookup(Command.GetSingleValue<ushort>(DicomTag.Status));
                if ( Command.TryGetSingleValue(DicomTag.ErrorComment, out string comment))
                {
                    return new DicomStatus(status, comment);
                }
                return status;
            }
            set
            {
                Command.AddOrUpdate(DicomTag.Status, value.Code);
                if (string.IsNullOrEmpty(value.ErrorComment?.Trim()))
                {
                    Command.Remove(DicomTag.ErrorComment);
                }
                else
                {
                    Command.AddOrUpdate(DicomTag.ErrorComment, value.ErrorComment);
                }
            }
        }

        /// <summary>
        /// Formatted output of the DICOM response.
        /// </summary>
        /// <returns>
        /// The formatted output of the DICOM response.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
            if (Status.State != DicomState.Pending && Status.State != DicomState.Success)
            {
                if (!string.IsNullOrEmpty(Status.ErrorComment))
                {
                    sb.AppendFormat("\n\t\tError:\t\t{0}", Status.ErrorComment);
                }

                if (Command.Contains(DicomTag.OffendingElement))
                {
                    string[] tags = Command.GetValues<string>(DicomTag.OffendingElement);
                    if (tags.Length > 0)
                    {
                        sb.Append("\n\t\tTags:\t\t");
                        foreach (var tag in tags)
                        {
                            sb.AppendFormat(" {0}", tag);
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}
