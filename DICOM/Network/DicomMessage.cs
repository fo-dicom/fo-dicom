using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.Log;

namespace Dicom.Network {
	public class DicomMessage {
		public DicomMessage() {
			Command = new DicomDataset();
			Dataset = null;
		}

		public DicomMessage(DicomDataset command) {
			Command = command;
		}

		public DicomCommandField Type {
			get { return Command.Get<DicomCommandField>(DicomTag.CommandField); }
			set { Command.Add(DicomTag.CommandField, (ushort)value); }
		}

		/// <summary>Affected or requested SOP Class UID</summary>
		public DicomUID SOPClassUID {
			get {
				switch (Type) {
				case DicomCommandField.NGetRequest:
				case DicomCommandField.NSetRequest:
				case DicomCommandField.NActionRequest:
				case DicomCommandField.NDeleteRequest:
					return Command.Get<DicomUID>(DicomTag.RequestedSOPClassUID);
				default:
					return Command.Get<DicomUID>(DicomTag.AffectedSOPClassUID);
				}
			}
			set {
				switch (Type) {
				case DicomCommandField.NGetRequest:
				case DicomCommandField.NSetRequest:
				case DicomCommandField.NActionRequest:
				case DicomCommandField.NDeleteRequest:
					Command.Add(DicomTag.RequestedSOPClassUID, value);
					break;
				default:
					Command.Add(DicomTag.AffectedSOPClassUID, value);
					break;
				}
			}
		}

		public ushort MessageID {
			get { return Command.Get<ushort>(DicomTag.MessageID); }
			set { Command.Add(DicomTag.MessageID, value); }
		}

		public bool HasDataset {
			get { return Command.Get<ushort>(DicomTag.CommandDataSetType, 0, 0x0101) != 0x0101; }
		}

		public DicomDataset Command {
			get;
			set;
		}

		private DicomDataset _dataset;
		public DicomDataset Dataset {
			get { return _dataset; }
			set {
				_dataset = value;
				Command.Add(DicomTag.CommandDataSetType,
					(_dataset != null) ? (ushort)0x0202 : (ushort)0x0101);
			}
		}

		/// <summary>State object that will be passed from request to response objects.</summary>
		public object UserState {
			get;
			set;
		}

		public override string ToString() {
			return String.Format("{0} [{1}]", ToString(Type), IsRequest(Type) ? MessageID : Command.Get<ushort>(DicomTag.MessageIDBeingRespondedTo));
		}

		public string ToString(bool printDatasets) {
			var output = new StringBuilder(ToString());

			if (!printDatasets)
				return output.ToString();

			output.AppendLine();
			output.AppendLine("--------------------------------------------------------------------------------");
			output.AppendLine(" DIMSE Command:");
			output.AppendLine("--------------------------------------------------------------------------------");
			output.AppendLine(Command.WriteToString());

			if (HasDataset) {
				output.AppendLine("--------------------------------------------------------------------------------");
				output.AppendLine(" DIMSE Dataset:");
				output.AppendLine("--------------------------------------------------------------------------------");
				output.AppendLine(Dataset.WriteToString());
			}

			output.AppendLine("--------------------------------------------------------------------------------");

			return output.ToString();
		}

		public static string ToString(DicomCommandField type) {
			switch (type) {
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

		public static bool IsRequest(DicomCommandField type) {
			return ((int)type & 0x8000) == 0;
		}
	}
}
