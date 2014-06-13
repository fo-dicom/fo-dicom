using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dicom.Media {
	class Program {
		static void Main(string[] args) {
			try {
				if (args.Length < 2) {
					PrintUsage();
					return;
				}

				var action = args[0];
				var path = args[1];

				if (action == "read") {
					path = Path.Combine(path, "DICOMDIR");

					if (!File.Exists(path)) {
						Console.WriteLine("DICOMDIR file not found: {0}", path);
						return;
					}

					ReadMedia(path);
					return;
				}

				WriteMedia(path);
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			} finally {

			}
		}

		private static void WriteMedia(string path) {
			var dicomDirPath = Path.Combine(path, "DICOMDIR");

			var dirInfo = new DirectoryInfo(path);

			var dicomDir = new DicomDirectory();
			foreach (var file in dirInfo.GetFiles("*.*", SearchOption.AllDirectories)) {
				var dicomFile = Dicom.DicomFile.Open(file.FullName);

				dicomDir.AddFile(dicomFile, String.Format(@"000001\{0}", file.Name));
			}

			dicomDir.Save(dicomDirPath);
		}

		private static void ReadMedia(string fileName) {
			var dicomDirectory = DicomDirectory.Open(fileName);

			foreach (var patientRecord in dicomDirectory.RootDirectoryRecordCollection) {
				Console.WriteLine("Patient: {0} ({1})", patientRecord.Get<string>(DicomTag.PatientName), patientRecord.Get<string>(DicomTag.PatientID));

				foreach (var studyRecord in patientRecord.LowerLevelDirectoryRecordCollection) {
					Console.WriteLine("\tStudy: {0}", studyRecord.Get<string>(DicomTag.StudyInstanceUID));

					foreach (var seriesRecord in studyRecord.LowerLevelDirectoryRecordCollection) {
						Console.WriteLine("\t\tSeries: {0}", seriesRecord.Get<string>(DicomTag.SeriesInstanceUID));

						foreach (var imageRecord in seriesRecord.LowerLevelDirectoryRecordCollection) {
							Console.WriteLine("\t\t\tImage: {0} [{1}]", imageRecord.Get<string>(DicomTag.ReferencedSOPInstanceUIDInFile), imageRecord.Get<string>(Dicom.DicomTag.ReferencedFileID));
						}
					}
				}
			}
		}

		private static void PrintUsage() {
			Console.WriteLine("Usage: Dicom.Media.exe read|write <directory>");
		}
	}
}
