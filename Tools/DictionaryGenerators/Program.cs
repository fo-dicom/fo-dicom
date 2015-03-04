namespace DictionaryGenerators
{
	using System.IO;
	using System.Text;

	using Dicom;
	using Generators.DocBookParser;

	class Program
	{
		static void Main(string[] args)
		{

			if (args.Length != 1 ||
				!File.Exists(Path.Combine(args[0], "part06.xml")) ||
				!File.Exists(Path.Combine(args[0], "part07.xml")) ||
				!File.Exists(Path.Combine(args[0], "part16.xml")))
			{
				System.Console.WriteLine("Syntax: {0} <xml-base-path>", System.AppDomain.CurrentDomain.FriendlyName);
				System.Console.WriteLine();
				System.Console.WriteLine("e.g: {0} C:\\temp", System.AppDomain.CurrentDomain.FriendlyName);
				System.Console.WriteLine();
				System.Console.WriteLine("Cannot find required XML files (part06.xml, part07.xml and part16.xml). Try downloading them using the attached script \"GetDocBooks.ps1\".");
				System.Environment.Exit(1);
			}

			string basepath = args[0];

			var dd = Part6Parser.ParseDataDictionaries(Path.Combine(basepath, "part06.xml"), Path.Combine(basepath, "part07.xml"));
			var uids = Part6Parser.ParseUIDTables(Path.Combine(basepath, "part06.xml"), Path.Combine(basepath, "part16.xml"));

			var dicomDict = new DicomDictionary();
			foreach (var ddentry in dd)
			{
				dicomDict.Add(ddentry);
			}

			var a = Generators.DicomDictionaryGenerator.Generate("Dicom", "DicomDictionary", "LoadGeneratedDictionary_", dicomDict);
			var b = Generators.DicomTagGenerator.Generate("Dicom", "DicomTag", dicomDict);
			var c = Generators.DicomUIDGenerator.Emit(uids);

			File.WriteAllText("DicomDictionaryGenerated.cs", a, Encoding.UTF8);
			File.WriteAllText("DicomTagGenerated.cs", b, Encoding.UTF8);
			File.WriteAllText("DicomUIDGenerated.cs", c, Encoding.UTF8);
		}
	}
}
