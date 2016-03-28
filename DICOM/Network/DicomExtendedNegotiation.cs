using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dicom.Network
{
	public interface IExtendedNegotiationSubItem
	{
		void Write(RawPDU pdu);
	}

	public delegate IExtendedNegotiationSubItem CreateIExtendedNegotiationSubItemDelegate(RawPDU raw, out int bytesRead);

	public class DicomExtendedNegotiation
	{
		private static Dictionary<DicomUID, CreateIExtendedNegotiationSubItemDelegate> subItemCreators = new Dictionary<DicomUID, CreateIExtendedNegotiationSubItemDelegate>();

		public DicomUID SopClassUid { get; private set; }

		public IExtendedNegotiationSubItem SubItem { get; private set; }

		static DicomExtendedNegotiation()
		{
			AddSubItemCreator(DicomUID.StudyRootQueryRetrieveInformationModelFIND, StudyRootQueryRetrieveInfo.Create);
		}

		public static void AddSubItemCreator(DicomUID uid, CreateIExtendedNegotiationSubItemDelegate creator)
		{
			if (false == subItemCreators.ContainsKey(uid))
			{
				subItemCreators.Add(uid, creator);
			}
		}

		public DicomExtendedNegotiation(DicomUID sopClassUid, IExtendedNegotiationSubItem subItem)
		{
			SopClassUid = sopClassUid;
			SubItem = subItem;
		}

		public void Write(RawPDU pdu)
		{
			pdu.Write("Item-Type", (byte)0x56);
			pdu.Write("Reserved", (byte)0x00);

			pdu.MarkLength16("Item-Length");

			pdu.Write("SOP Class UID Length", (ushort)(SopClassUid.UID.Length));
			pdu.Write("SOP Class UID", SopClassUid.UID);

			SubItem.Write(pdu);

			pdu.WriteLength16();
		}

		public static DicomExtendedNegotiation Create(RawPDU raw, ushort length)
		{
			ushort uidLen = raw.ReadUInt16("SOP Class UID Length");
			string uidStr = raw.ReadString("SOP Class UID", uidLen);

			DicomUID uid = DicomUID.Parse(uidStr);
			IExtendedNegotiationSubItem subItem = null;
			int subItemSize = 0;

			if (subItemCreators.ContainsKey(uid))
			{
				subItem = subItemCreators[uid](raw, out subItemSize);
			}

			int remaining = length - uidLen - subItemSize - 2;
			if (remaining > 0)
			{
				raw.SkipBytes("Unread bytes", remaining);
			}

			return new DicomExtendedNegotiation(uid, subItem);
		}
	}
}
