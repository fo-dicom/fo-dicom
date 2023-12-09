// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Log;
using FellowOakDicom.Tools;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Base class for DIMSE-C and DIMSE-N message items.
    /// </summary>
    public class DicomMessage
    {
        #region FIELDS

        private DicomDataset _dataset;
        private readonly TaskCompletionSource<bool> _allPDUsSentTCS = TaskCompletionSourceFactory.Create<bool>();

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
        internal DateTime? LastPDUSent { get; set; }

        /// <summary>
        /// Gets a task that will complete when all the PDUs of this DICOM message have been sent
        /// Important caveat: if this DICOM message is never picked up to be sent (e.g. because of connection issues) then this task never completes
        /// </summary>
        internal Task AllPDUsSent => _allPDUsSentTCS.Task;

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
            {
                return LastPendingResponseReceived.Value.Add(timeout) < DateTime.Now;
            }

            if (LastPDUSent != null)
            {
                return LastPDUSent.Value.Add(timeout) < DateTime.Now;
            }

            if(PendingSince != null)
            {
                return PendingSince.Value.Add(timeout) < DateTime.Now;
            }

            return false;
        }

        internal void AllPDUsWereSentSuccessfully() => _allPDUsSentTCS.TrySetResult(true);
        internal void NotAllPDUsWereSentSuccessfully() => _allPDUsSentTCS.TrySetResult(false);

        /// <summary>
        /// Formatted output of the DICOM message.
        /// </summary>
        /// <returns>Formatted output string of the DICOM message.</returns>
        public override string ToString()
            => $"{ToString(Type)} [{(IsRequest(Type) ? Command.GetSingleValue<ushort>(DicomTag.MessageID) : Command.GetSingleValue<ushort>(DicomTag.MessageIDBeingRespondedTo))}]";


        /// <summary>
        /// Formatted output of the DICOM message.
        /// </summary>
        /// <param name="printDatasets">Indicates whether datasets should be printed.</param>
        /// <returns>Formatted output string of the DICOM message.</returns>
        public string ToString(bool printDatasets)
        {
            try
            {
                var output = new StringBuilder(ToString());

                if (!printDatasets)
                {
                    return output.ToString();
                }

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
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Formatted output of the DICOM message.
        /// </summary>
        /// <param name="type">DICOM command field type.</param>
        /// <returns>Formatted output string of the DICOM message.</returns>
        public static string ToString(DicomCommandField type)
            => type switch
        {
            DicomCommandField.CCancelRequest => "C-Cancel request",
            DicomCommandField.CEchoRequest => "C-Echo request",
            DicomCommandField.CEchoResponse => "C-Echo response",
            DicomCommandField.CFindRequest => "C-Find request",
            DicomCommandField.CFindResponse => "C-Find response",
            DicomCommandField.CGetRequest => "C-Get request",
            DicomCommandField.CGetResponse => "C-Get response",
            DicomCommandField.CMoveRequest => "C-Move request",
            DicomCommandField.CMoveResponse => "C-Move response",
            DicomCommandField.CStoreRequest => "C-Store request",
            DicomCommandField.CStoreResponse => "C-Store response",
            DicomCommandField.NActionRequest => "N-Action request",
            DicomCommandField.NActionResponse => "N-Action response",
            DicomCommandField.NCreateRequest => "N-Create request",
            DicomCommandField.NCreateResponse => "N-Create response",
            DicomCommandField.NDeleteRequest => "N-Delete request",
            DicomCommandField.NDeleteResponse => "N-Delete response",
            DicomCommandField.NEventReportRequest => "N-EventReport request",
            DicomCommandField.NEventReportResponse => "N-EventReport response",
            DicomCommandField.NGetRequest => "N-Get request",
            DicomCommandField.NGetResponse => "N-Get response",
            DicomCommandField.NSetRequest => "N-Set request",
            DicomCommandField.NSetResponse => "N-Set response",
            _ => "DIMSE",
        };


        /// <summary>
        /// Evaluates whether a DICOM message is a request or a response.
        /// </summary>
        /// <param name="type">DICOM command field type.</param>
        /// <returns>True if message is a request, false otherwise.</returns>
        public static bool IsRequest(DicomCommandField type) => ((int)type & 0x8000) == 0;

    }
}
