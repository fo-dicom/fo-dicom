using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicomJsonConverter
{
	using System.IO;

	using Dicom;
	using Dicom.IO;

	using Newtonsoft.Json;

	class Program
	{
		static void Main(string[] args)
		{
			if (args[0].EndsWith(".json"))
			{
				var json = File.ReadAllText(args[0]);
				var dataset = JsonConvert.DeserializeObject<DicomDataset>(json, new JsonDicomConverter());
				new DicomFile(dataset).Save(args[1]);
			}
			else if (args[0].EndsWith(".dcm"))
			{
				var ds = DicomFile.Open(args[0]).Dataset;
				var json = JsonConvert.SerializeObject(ds, new JsonDicomConverter(true));
				File.WriteAllText(args[1], json);
			}
		}
	}
}
