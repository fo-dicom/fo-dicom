using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Generators {
	public class DicomTagGenerator {
		public static string Generate(string vnamespace, string vclass, DicomDictionary dict) {
			StringBuilder output = new StringBuilder();

			output.AppendFormat("namespace {0} {{", vnamespace).AppendLine();
			output.AppendFormat("\tpublic partial class {0} {{", vclass).AppendLine();

			foreach (DicomDictionaryEntry entry in dict) {
				//if (entry.MaskTag != null)
				//	continue;

				string vrs = String.Join("/", entry.ValueRepresentations.Select(x => x.ToString()));
				string variable = "_" + Char.ToLower(entry.Keyword[0]) + entry.Keyword.Substring(1);

				output.AppendFormat("\t\t///<summary>{0} VR={1} VM={2} {3}{4}</summary>",
					entry.Tag, vrs, entry.ValueMultiplicity, entry.Name, entry.IsRetired ? " (RETIRED)" : "").AppendLine();
				output.AppendFormat("\t\tpublic readonly static DicomTag {0}{1} = new DicomTag(0x{2:x4}, 0x{3:x4});", 
					entry.Keyword, entry.IsRetired ? "RETIRED" : "", entry.Tag.Group, entry.Tag.Element).AppendLine();
				output.AppendLine();

				//output.AppendFormat("\t\t///<summary>{0} VR={1} VM={2} {3}{4}</summary>",
				//    entry.Tag, vrs, entry.ValueMultiplicity, entry.Name, entry.IsRetired ? " (RETIRED)" : "").AppendLine();
				//output.AppendFormat("\t\tpublic static DicomTag {0}{1} {{", entry.Keyword, entry.IsRetired ? "RETIRED" : "").AppendLine();
				//output.AppendLine("\t\t\tget {");
				//output.AppendFormat("\t\t\t\tif ({0} == null)", variable).AppendLine();
				//output.AppendFormat("\t\t\t\t\t{0} = new DicomTag(0x{1:x4}, 0x{2:x4});", variable, entry.Tag.Group, entry.Tag.Element).AppendLine();
				//output.AppendFormat("\t\t\t\treturn {0};", variable).AppendLine();
				//output.AppendLine("\t\t\t}");
				//output.AppendLine("\t\t}");
				//output.AppendFormat("\t\tprivate static DicomTag {0};", variable).AppendLine();
				//output.AppendLine();
			}

			output.AppendLine("\t}");
			output.AppendLine("}");

			return output.ToString();
		}
	}
}
