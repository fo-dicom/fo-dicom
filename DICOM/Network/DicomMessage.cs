// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    using System.Text;
    using Dicom.Log;

    /// <summary>
    /// Base class for DIMSE-C and DIMSE-N message items.
    /// </summary>
    public class DicomMessage
    {
        #region FIELDS

        private DicomDataset _dataset;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomMessage"/> class.
        /// </summary>
        public DicomMessage()
        {
            Command = new DicomDataset();
            Dataset = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomMessage"/> class.
        /// </summary>
        /// <param name="command">
        /// The DICOM dataset representing the message command.
        /// </param>
        public DicomMessage(DicomDataset command)
        {
            Command = command;
        }

        /// <summary>
        /// Gets or sets the command field type.
        /// </summary>
        public DicomCommandField Type
        {
            get => Command.GetSingleValue<DicomCommandField>(DicomTag.CommandField);
            protected set => Command.AddOrUpdate(DicomTag.CommandField, (ushort) value);
        }

        /// <summary>
        /// Gets or sets the affected or requested SOP Class UID
        /// </summary>
        public DicomUID SOPClassUID
        {
            get
            {
                switch (Type)
                {
                    case DicomCommandField.NGetRequest:
                    case DicomCommandField.NSetRequest:
                    case DicomCommandField.NActionRequest:
                    case DicomCommandField.NDeleteRequest:
                        return Command.GetSingleValue<DicomUID>(DicomTag.RequestedSOPClassUID);
                    case DicomCommandField.CStoreRequest:
                    case DicomCommandField.CFindRequest:
                    case DicomCommandField.CGetRequest:
                    case DicomCommandField.CMoveRequest:
                    case DicomCommandField.CEchoRequest:
                    case DicomCommandField.NEventReportRequest:
                    case DicomCommandField.NCreateRequest:
                        return Command.GetSingleValue<DicomUID>(DicomTag.AffectedSOPClassUID);
                    default:
                        return Command.GetSingleValueOrDefault<DicomUID>(DicomTag.AffectedSOPClassUID, null);
                }
            }
            protected set
            {
                switch (Type)
                {
                    case DicomCommandField.NGetRequest:
                    case DicomCommandField.NSetRequest:
                    case DicomCommandField.NActionRequest:
                    case DicomCommandField.NDeleteRequest:
                        Command.AddOrUpdate(DicomTag.RequestedSOPClassUID, value);
                        break;
                    default:
                        Command.AddOrUpdate(DicomTag.AffectedSOPClassUID, value);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the message contains a dataset.
        /// </summary>
        public bool HasDataset => Command.GetSingleValueOrDefault(DicomTag.CommandDataSetType, (ushort) 0x0101) != 0x0101;

        /// <summary>
        /// Gets or sets the presentation Context.
        /// </summary>
        public DicomPresentationContext PresentationContext { get; set; }

        /// <summary>
        /// Gets or sets the SOP Class Extended Negotiation Service Class Application Information.
        /// </summary>
        public DicomServiceApplicationInfo ApplicationInfo { get; set; }

        /// <summary>
        /// Gets or sets the associated DICOM command.
        /// </summary>
        public DicomDataset Command { get; protected set; }

        /// <summary>
        /// Gets or sets the dataset potentially included in the message..
        /// </summary>
        public DicomDataset Dataset
        {
            get => _dataset;
            set
            {
                _dataset = value;
                Command.AddOrUpdate(DicomTag.CommandDataSetType, _dataset != null ? (ushort) 0x0202 : (ushort) 0x0101);
            }
        }

        /// <summary>
        /// Gets or sets the state object that will be passed from request to response objects.
        /// </summary>
        public object UserState { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the message was taken from the message queue and added to the pending list (i.e. the DICOM request is being sent or already waiting for a response)
        /// </summary>
        public DateTime? PendingSince { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the last PDU was sent
        /// </summary>
        public DateTime? LastPDUSent { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the last response with status 'Pending' was received
        /// </summary>
        public DateTime? LastPendingResponseReceived { get; set; }

        /// <summary>
        /// Given a timeout duration, returns whether this DICOM message is considered timed out or not.
        /// </summary>
        /// <param name="timeout">The timeout duration that should be taken into account</param>
        /// <returns>Whether this DICOM message is considered timed out or not.</returns>
        public bool IsTimedOut(TimeSpan timeout)
        {
            if (LastPendingResponseReceived != null)
                return LastPendingResponseReceived.Value.Add(timeout) < DateTime.Now;

            if (LastPDUSent != null)
                return LastPDUSent.Value.Add(timeout) < DateTime.Now;

            if(PendingSince != null)
                return PendingSince.Value.Add(timeout) < DateTime.Now;

            return false;
        }

        /// <summary>
        /// Formatted output of the DICOM message.
        /// </summary>
        /// <returns>Formatted output string of the DICOM message.</returns>
        public override string ToString()
        {
            return
                $"{ToString(Type)} [{(IsRequest(Type) ? Command.GetSingleValue<ushort>(DicomTag.MessageID) : Command.GetSingleValue<ushort>(DicomTag.MessageIDBeingRespondedTo))}]";
        }

        /// <summary>
        /// Formatted output of the DICOM message.
        /// </summary>
        /// <param name="printDatasets">Indicates whether datasets should be printed.</param>
        /// <returns>Formatted output string of the DICOM message.</returns>
        public string ToString(bool printDatasets)
        {
            var output = new StringBuilder(ToString());

            if (!printDatasets) return output.ToString();

            output.AppendLine();
            output.AppendLine("--------------------------------------------------------------------------------");
            output.AppendLine(" DIMSE Command:");
            output.AppendLine("--------------------------------------------------------------------------------");
            output.AppendLine(Command.WriteToString());

            if (HasDataset)
            {
                output.AppendLine("--------------------------------------------------------------------------------");
                output.AppendLine(" DIMSE Dataset:");
                output.AppendLine("--------------------------------------------------------------------------------");
                output.AppendLine(Dataset.WriteToString());
            }

            output.AppendLine("--------------------------------------------------------------------------------");

            return output.ToString();
        }

        /// <summary>
        /// Formatted output of the DICOM message.
        /// </summary>
        /// <param name="type">DICOM command field type.</param>
        /// <returns>Formatted output string of the DICOM message.</returns>
        public static string ToString(DicomCommandField type)
        {
            switch (type)
            {
                case DicomCommandField.CCancelRequest:
                    return "C-Cancel request";
                case DicomCommandField.CEchoRequest:
                    return "C-Echo request";
                case DicomCommandField.CEchoResponse:
                    return "C-Echo response";
                case DicomCommandField.CFindRequest:
                    return "C-Find request";
                case DicomCommandField.CFindResponse:
                    return "C-Find response";
                case DicomCommandField.CGetRequest:
                    return "C-Get request";
                case DicomCommandField.CGetResponse:
                    return "C-Get response";
                case DicomCommandField.CMoveRequest:
                    return "C-Move request";
                case DicomCommandField.CMoveResponse:
                    return "C-Move response";
                case DicomCommandField.CStoreRequest:
                    return "C-Store request";
                case DicomCommandField.CStoreResponse:
                    return "C-Store response";
                case DicomCommandField.NActionRequest:
                    return "N-Action request";
                case DicomCommandField.NActionResponse:
                    return "N-Action response";
                case DicomCommandField.NCreateRequest:
                    return "N-Create request";
                case DicomCommandField.NCreateResponse:
                    return "N-Create response";
                case DicomCommandField.NDeleteRequest:
                    return "N-Delete request";
                case DicomCommandField.NDeleteResponse:
                    return "N-Delete response";
                case DicomCommandField.NEventReportRequest:
                    return "N-EventReport request";
                case DicomCommandField.NEventReportResponse:
                    return "N-EventReport response";
                case DicomCommandField.NGetRequest:
                    return "N-Get request";
                case DicomCommandField.NGetResponse:
                    return "N-Get response";
                case DicomCommandField.NSetRequest:
                    return "N-Set request";
                case DicomCommandField.NSetResponse:
                    return "N-Set response";
                default:
                    return "DIMSE";
            }
        }

        /// <summary>
        /// Evaluates whether a DICOM message is a request or a response.
        /// </summary>
        /// <param name="type">DICOM command field type.</param>
        /// <returns>True if message is a request, false otherwise.</returns>
        public static bool IsRequest(DicomCommandField type)
        {
            return ((int) type & 0x8000) == 0;
        }
    }
}
