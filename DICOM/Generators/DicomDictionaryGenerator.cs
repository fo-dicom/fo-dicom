using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Generators {
	public class DicomDictionaryGenerator {
		public static string Generate(string vnamespace, string vclass, string vmethod, DicomDictionary dict) {
			StringBuilder output = new StringBuilder();

			output.AppendFormat("namespace {0} {{", vnamespace).AppendLine();
			output.AppendFormat("\tpublic partial class {0} {{", vclass).AppendLine();
			output.AppendFormat("\t\tpublic static void {0}(DicomDictionary dict) {{", vmethod).AppendLine();

			foreach (DicomDictionaryEntry entry in dict) {
				string vm = null;
				switch (entry.ValueMultiplicity.ToString()) {
					case "1": vm = "DicomVM.VM_1"; break;
					case "1-2": vm = "DicomVM.VM_1_2"; break;
					case "1-3": vm = "DicomVM.VM_1_3"; break;
					case "1-8": vm = "DicomVM.VM_1_8"; break;
					case "1-32": vm = "DicomVM.VM_1_32"; break;
					case "1-99": vm = "DicomVM.VM_1_99"; break;
					case "1-n": vm = "DicomVM.VM_1_n"; break;
					case "2": vm = "DicomVM.VM_2"; break;
					case "2-n": vm = "DicomVM.VM_2_n"; break;
					case "2-2n": vm = "DicomVM.VM_2_2n"; break;
					case "3": vm = "DicomVM.VM_3"; break;
					case "3-n": vm = "DicomVM.VM_3_n"; break;
					case "3-3n": vm = "DicomVM.VM_3_3n"; break;
					case "4": vm = "DicomVM.VM_4"; break;
					case "6": vm = "DicomVM.VM_6"; break;
					case "16": vm = "DicomVM.VM_16"; break;
					default:
						vm = String.Format("DicomVM.Parse(\"{0}\")", entry.ValueMultiplicity);
						break;
				}

				if (entry.MaskTag == null) {
					output.AppendFormat("\t\t\tdict._entries.Add(DicomTag.{3}{6}, new DicomDictionaryEntry(DicomTag.{3}{6}, \"{2}\", \"{3}\", {4}, {5}",
						entry.Tag.ToString("g", null), entry.Tag.ToString("e", null), entry.Name, entry.Keyword, vm, entry.IsRetired ? "true" : "false", entry.IsRetired ? "RETIRED" : "");
				} else {
					output.AppendFormat("\t\t\tdict.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse(\"{0}\",\"{1}\"), \"{2}\", \"{3}\", {4}, {5}",
						entry.MaskTag.ToString("g", null), entry.MaskTag.ToString("e", null), entry.Name, entry.Keyword, vm, entry.IsRetired ? "true" : "false");
				}

				foreach (DicomVR vr in entry.ValueRepresentations)
					output.AppendFormat(", DicomVR.{0}", vr.Code);

				output.AppendLine("));");
			}

			output.AppendLine("\t\t}");
			output.AppendLine("\t}");
			output.AppendLine("}");

			return output.ToString();
		}
	}
}
