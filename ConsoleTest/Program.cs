using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

using NLog;
using NLog.Config;
using NLog.Targets;

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
                //DicomException.OnException += delegate(object sender, DicomExceptionEventArgs ea) {
                //    ConsoleColor old = Console.ForegroundColor;
                //    Console.ForegroundColor = ConsoleColor.Yellow;
                //    Console.WriteLine(ea.Exception);
                //    Console.ForegroundColor = old;
                //};

                //var config = new LoggingConfiguration();

                //var target = new ColoredConsoleTarget();
                //target.Layout = @"${date:format=HH\:mm\:ss}  ${message}";
                //config.AddTarget("Console", target);
                //config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, target));

                //NLog.LogManager.Configuration = config;

                //Dicom.Log.LogManager.Default = new Dicom.Log.NLogManager();

                ////var server = new DicomServer<DicomCEchoProvider>(12345);


                //var client = new DicomClient();
                ////client.NegotiateAsyncOps();
                ////for (int i = 0; i < 10; i++)
                ////    client.AddRequest(new DicomCEchoRequest());
                //client.AddRequest(new DicomCStoreRequest(@"Z:\test1.dcm"));
                //client.AddRequest(new DicomCStoreRequest(@"Z:\test2.dcm"));
                //client.Send("127.0.0.1", 104,false, "SCU", "ANY-SCP");

                //var samplesDir = Path.Combine(Path.GetPathRoot(Environment.CurrentDirectory), "Development", "fo-dicom-samples");
                //var testDir = Path.Combine(samplesDir, "Test");

                //if (!Directory.Exists(testDir))
                //    Directory.CreateDirectory(testDir);

                ////var img = new DicomImage(samplesDir + @"\ClearCanvas\CRStudy\1.3.51.5145.5142.20010109.1105627.1.0.1.dcm");
                ////img.RenderImage().Save(testDir + @"\test.jpg");

                ////var df = DicomFile.Open(samplesDir + @"\User Submitted\overlays.dcm");

                ////Console.WriteLine(df.FileMetaInfo.Get<DicomTransferSyntax>(DicomTag.TransferSyntaxUID).UID.Name);
                ////Console.WriteLine(df.Dataset.Get<PlanarConfiguration>(DicomTag.PlanarConfiguration));

                ////var img = new DicomImage(df.Dataset);
                ////img.RenderImage().Save(testDir + @"\test.jpg");

                ////df = df.ChangeTransferSyntax(DicomTransferSyntax.JPEGLSLossless);
                ////df.Save(testDir + @"\test-jls.dcm");

                ////df = df.ChangeTransferSyntax(DicomTransferSyntax.JPEG2000Lossless);
                ////df.Save(testDir + @"\test-j2k.dcm");

                ////df = df.ChangeTransferSyntax(DicomTransferSyntax.JPEGProcess14SV1);
                ////df.Save(testDir + @"\test-jll.dcm");

                ////df = df.ChangeTransferSyntax(DicomTransferSyntax.RLELossless);
                ////df.Save(testDir + @"\test-rle.dcm");

                ////df = df.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian);
                ////df.Save(testDir + @"\test-ele.dcm");

                ////df = df.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRBigEndian);
                ////df.Save(testDir + @"\test-ebe.dcm");

                ////df = df.ChangeTransferSyntax(DicomTransferSyntax.ImplicitVRLittleEndian);
                ////df.Save(testDir + @"\test-ile.dcm");

                ////Console.WriteLine("End...");
                ////Console.ReadLine();

                ////df.WriteToLog(LogManager.GetCurrentClassLogger(), LogLevel.Info);

                ////Console.WriteLine(DicomValueMultiplicity.Parse("1"));
                ////Console.WriteLine(DicomValueMultiplicity.Parse("3"));
                ////Console.WriteLine(DicomValueMultiplicity.Parse("1-3"));
                ////Console.WriteLine(DicomValueMultiplicity.Parse("1-n"));
                ////Console.WriteLine(DicomValueMultiplicity.Parse("2-2n"));

                ////Console.WriteLine(DicomTag.Parse("00200020"));
                ////Console.WriteLine(DicomTag.Parse("0008,0040"));
                ////Console.WriteLine(DicomTag.Parse("(3000,0012)"));
                ////Console.WriteLine(DicomTag.Parse("2000,2000:TEST CREATOR"));
                ////Console.WriteLine(DicomTag.Parse("(4000,4000:TEST_CREATOR:2)"));

                ////Console.WriteLine(DicomMaskedTag.Parse("(30xx,xx90)"));
                ////Console.WriteLine(DicomMaskedTag.Parse("(3000-3021,0016)"));

                ////DicomRange<DateTime> r = new DicomRange<DateTime>(DateTime.Now.AddSeconds(-5), DateTime.Now.AddSeconds(5));
                ////Console.WriteLine(r.Contains(DateTime.Now));
                ////Console.WriteLine(r.Contains(DateTime.Today));
                ////Console.WriteLine(r.Contains(DateTime.Now.AddSeconds(60)));

                ////DicomDictionary dict = new DicomDictionary();
                ////dict.Load(@"F:\Development\fo-dicom\DICOM\Dictionaries\dictionary.xml", DicomDictionaryFormat.XML);

                ////string output = Dicom.Generators.DicomTagGenerator.Generate("Dicom", "DicomTag", dict);
                ////File.WriteAllText(@"F:\Development\fo-dicom\DICOM\DicomTagGenerated.cs", output);

                ////output = Dicom.Generators.DicomDictionaryGenerator.Generate("Dicom", "DicomDictionary", "LoadInternalDictionary", dict);
                ////File.WriteAllText(@"F:\Development\fo-dicom\DICOM\DicomDictionaryGenerated.cs", output);

                ////string output = Dicom.Generators.DicomUIDGenerator.Process(@"F:\Development\fo-dicom\DICOM\Dictionaries\dictionary.xml");
                ////File.WriteAllText(@"F:\Development\fo-dicom\DICOM\DicomUIDGenerated.cs", output);
                var dicomFile = DicomFile.Open(@"D:\ZSData\not-standard-dicom\1.dcm");
                Console.WriteLine("SeriesUID={0}", dicomFile.Dataset.Get<string>(DicomTag.SeriesInstanceUID));
			} catch (Exception e) {
				if (!(e is DicomException))
					Console.WriteLine(e.ToString());
			}
		}
	}
}
