using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicom.Generators.DocBookParser
{
	using System.Xml.Linq;

	public class UidTable : DocBookTable<DicomUID>
	{
		private static DicomUidType ParseType(string type)
		{
			switch (type)
			{
				case "Transfer Syntax":
				case "Transfer":
					return DicomUidType.TransferSyntax;
				case "SOP Class":
				case "Query/Retrieve":
					return DicomUidType.SOPClass;
				case "Meta SOP Class":
					return DicomUidType.MetaSOPClass;
				case "Service Class":
					return DicomUidType.ServiceClass;
				case "Well-known Printer SOP Instance":
				case "Well-known Print Queue SOP Instance":
				case "Well-known SOP Instance":
					return DicomUidType.SOPInstance;
				case "DICOM Application Context Name":
				case "Application Context Name":
					return DicomUidType.ApplicationContextName;
				case "Application Hosting Model":
					return DicomUidType.ApplicationHostingModel;
				case "Coding Scheme Designator":
				case "DICOM UIDs as a Coding Scheme":
				case "Coding Scheme":
					return DicomUidType.CodingScheme;
				case "Well-known frame of reference":
				case "Synchronization Frame of Reference":
					return DicomUidType.FrameOfReference;
				case "LDAP OID":
					return DicomUidType.LDAP;
				case "Context Group Name":
					return DicomUidType.ContextGroupName;
				case "Mapping Resource":
					// TODO: Add to fo-dicom
					return DicomUidType.Unknown;
				default:
					throw new NotImplementedException();
					return DicomUidType.Unknown;
			}

		}

		public override DicomUID ParseRow(Dictionary<string, XElement> row)
		{
			string name;
			DicomUidType type;
			string uid;
			bool retired;

			if (Caption == "Context Group UID Values")
			{
				var olink = row["Context Group Name"].Descendants(DocBookNS + "olink").SingleOrDefault();
				if (olink == null)
					return null;
				var targetdoc = olink.Attribute("targetdoc").Value;
				var targetptr = olink.Attribute("targetptr").Value;
				var targetobj = Part16.Descendants().Single(section => (string)section.Attribute(XmlNS + "id") == targetptr);
				var targetsel = targetobj.Descendants(DocBookNS + "title").SingleOrDefault();
				if (targetsel == null)
					targetsel = targetobj.Descendants(DocBookNS + "caption").SingleOrDefault();
				name = targetsel.Value;
				type = DicomUidType.ContextGroupName;
				uid = row["Context UID"].Value;
				retired = false;
			}
			else if (Caption == "Standard Color Palettes")
			{
				name = row["Content Description (0070,0081)"].Value;
				type = DicomUidType.SOPInstance;
				retired = false;
				uid = row["Well-known SOP Instance UID"].Value.Replace("\u200b", string.Empty);
			}
			else
			{
				name = row["UID Name"].Value;
				if (Caption == "Well-known Frames of Reference") type = DicomUidType.FrameOfReference;
				else type = ParseType(row["UID Type"].Value.Replace("\u200b", string.Empty));
				uid = row["UID Value"].Value.Replace("\u200b", string.Empty);
				retired = name.EndsWith("(Retired)");
			}
			return new DicomUID(uid, name, type, retired);
		}

		public UidTable(XElement tableElement, XDocument part16Document)
			: base(tableElement)
		{
			this.Part16 = part16Document;
		}

		public UidTable(XElement tablElement) : base(tablElement)
		{
			throw new ArgumentException();
		}

		public XDocument Part16 { get; set; }
	}
}
