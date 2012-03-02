using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.Codec;
using Dicom.IO;
using Dicom.IO.Buffer;
using Dicom.IO.Reader;
using Dicom.IO.Writer;
using Dicom.Log;
using Dicom.Network;

namespace ConsoleTest {
	class Program {
		static void Main(string[] args) {
			try {
				DicomLog.OnDicomException += delegate(object sender, DicomExceptionEventArgs ea) {
					ConsoleColor old = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(ea.Exception);
					Console.ForegroundColor = old;
				};

				DicomLog.DefaultLogger = ConsoleLogger.Instance;
				DicomTranscoder.LoadCodecs();

				var elem = new DicomDate(DicomTag.StudyDate, DateTime.Today);
				var range = elem.Get<DicomDateRange>();
				Console.WriteLine(range);

				//DicomClient client = new DicomClient();
				//client.AddRequest(new DicomCEchoRequest());
				//client.AddRequest(new DicomCStoreRequest(@"C:\Development\test.dcm"));

				//TcpClient tcp = new TcpClient("127.0.0.1", 12345);
				//client.Send(tcp.GetStream(), "SCU", "ANY-SCP");
				//tcp.Close();

				//DicomFile df = DicomFile.Open(@"Z:\test6.dcm");

				//Console.WriteLine(df.FileMetaInfo.Get<DicomTransferSyntax>(DicomTag.TransferSyntaxUID).UID.Name);
				//Console.WriteLine(df.Dataset.Get<PlanarConfiguration>(DicomTag.PlanarConfiguration));

				//df = df.ChangeTransferSyntax(DicomTransferSyntax.JPEGLSLossless, new DicomJpegLsParams { AllowedError = 3 });
				//df.Save(@"Z:\test_comp.dcm");
				//df = df.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRBigEndian);
				//df.Save(@"Z:\test_out.dcm");

				//Console.WriteLine("End...");
				//Console.ReadLine();

				//df.WriteToLog(ConsoleLogger.Instance);

				//Console.WriteLine(DicomValueMultiplicity.Parse("1"));
				//Console.WriteLine(DicomValueMultiplicity.Parse("3"));
				//Console.WriteLine(DicomValueMultiplicity.Parse("1-3"));
				//Console.WriteLine(DicomValueMultiplicity.Parse("1-n"));
				//Console.WriteLine(DicomValueMultiplicity.Parse("2-2n"));

				//Console.WriteLine(DicomTag.Parse("00200020"));
				//Console.WriteLine(DicomTag.Parse("0008,0040"));
				//Console.WriteLine(DicomTag.Parse("(3000,0012)"));
				//Console.WriteLine(DicomTag.Parse("2000,2000:TEST CREATOR"));
				//Console.WriteLine(DicomTag.Parse("(4000,4000:TEST_CREATOR:2)"));

				//Console.WriteLine(DicomMaskedTag.Parse("(30xx,xx90)"));
				//Console.WriteLine(DicomMaskedTag.Parse("(3000-3021,0016)"));

				//DicomRange<DateTime> r = new DicomRange<DateTime>(DateTime.Now.AddSeconds(-5), DateTime.Now.AddSeconds(5));
				//Console.WriteLine(r.Contains(DateTime.Now));
				//Console.WriteLine(r.Contains(DateTime.Today));
				//Console.WriteLine(r.Contains(DateTime.Now.AddSeconds(60)));

				//DicomDictionary dict = new DicomDictionary();
				//dict.Load(@"Z:\DICOM\DICOM for .NET 4.0\Dictionaries\GDCM.DICOMV3.xml", DicomDictionaryFormat.GDCM);

				//Console.WriteLine(DicomUID.JPEGLosslessProcess14SV1.Name);

				//string output = Dicom.Generators.DicomTagGenerator.Generate("Dicom", "DicomTag", dict);
				//File.WriteAllText(@"Z:\DICOM\DICOM for .NET 4.0\DicomTagGenerated.cs", output);

				//string output = Dicom.Generators.DicomDictionaryGenerator.Generate("Dicom", "DicomDictionary", "LoadInternalDictionary", dict);
				//File.WriteAllText(@"Z:\DICOM\DICOM for .NET 4.0\DicomDictionaryGenerated.cs", output);
				//File.WriteAllText(@"Z:\DicomDictionaryGenerated.cs", output);
			} catch (Exception e) {
				if (!(e is DicomException))
					Console.WriteLine(e.ToString());
			}
		}
	}
}
