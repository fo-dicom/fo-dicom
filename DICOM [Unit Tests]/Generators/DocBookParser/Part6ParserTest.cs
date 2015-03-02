using Dicom.Generators.DocBookParser;

namespace DICOM__Unit_Tests_.Generators.DocBookParser
{
	using System.IO;
	using System.Text;

	using Dicom;

	using Xunit;

	public class Part6ParserTest
	{
		[Fact]
		public void ParsePart6()
		{
			var dd = Part6Parser.ParseDataDictionaries("Generators/DocBookParser/part06.xml", "Generators/DocBookParser/part07.xml");
			var uids = Part6Parser.ParseUIDTables("Generators/DocBookParser/part06.xml", "Generators/DocBookParser/part16.xml");

			var dicomDict = new DicomDictionary();
			foreach (var ddentry in dd)
				dicomDict.Add(ddentry);

			var a = Dicom.Generators.DicomDictionaryGenerator.Generate("testns", "vclass", "vmethod", dicomDict);
			var b = Dicom.Generators.DicomTagGenerator.Generate("Dicom", "DicomTag", dicomDict);
			var c = Dicom.Generators.DicomUIDGenerator.Emit(uids);

			File.WriteAllText("DicomDictionaryGenerated.cs", a, Encoding.UTF8);
			File.WriteAllText("DicomTagGenerated.cs", b, Encoding.UTF8);
			File.WriteAllText("DicomUIDGenerated.cs", c, Encoding.UTF8);
		}
	}
}
