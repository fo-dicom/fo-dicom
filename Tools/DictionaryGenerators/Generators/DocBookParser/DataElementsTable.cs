using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom;

namespace DictionaryGenerators.Generators.DocBookParser
{
	using System.Text.RegularExpressions;
	using System.Xml.Linq;

	public class DataElementsTable : DocBookTable<DicomDictionaryEntry>
	{
		public override DicomDictionaryEntry ParseRow(Dictionary<string, XElement> row)
		{
			if (row["Tag"].Value.ToLower() == "(no tag)") return null;
			var tag = DicomMaskedTag.Parse(row["Tag"].Value.ToLower());
			string name;
			string keyword;
			bool retired;

			if (row.ContainsKey("Message Field"))
			{
				name = row["Message Field"].Value;
				retired = Caption.Contains("Retired");
			}
			else
			{
				name = row["Name"].Value;
				retired = row[""].Value.Contains("RET");
			}

			keyword = row["Keyword"].Value.Replace("\u200b", string.Empty);

			if(keyword.EndsWith("Retired"))
				keyword = keyword.Substring(0, keyword.Length - "Retired".Length);
			var vrs = row["VR"].Value.Split(new[] { " or " }, StringSplitOptions.RemoveEmptyEntries).Where(vr => vr.ToLowerInvariant().Trim() != "see note").Select(DicomVR.Parse).ToArray();
			DicomVM vm = null;
			if (!string.IsNullOrEmpty(row["VM"].Value))
			{
				vm = DicomVM.Parse(row["VM"].Value.Replace(" or 1", string.Empty));
			}

			//var dicos = row[""].Value.Contains("DICOS");
			//var diconde = row[""].Value.Contains("DICONDE");

			return new DicomDictionaryEntry(tag, name, keyword, vm, retired, vrs);
		}

		public DataElementsTable(XElement tableElement)
			: base(tableElement)
		{
		}
	}
}
