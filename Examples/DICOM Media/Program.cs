using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DICOM_Media
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    PrintUsage();   
                    return;
                }
                foreach (var arg in args)
                {
                    if (arg == "read")
                    {
                        ReadMedia(args);
                        return;
                    }
                    else if (arg == "write")
                    {
                        WriteMedia(args);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private static void WriteMedia(string[] args)
        {
            string dicomDirPath = string.Empty;
            string imagesFolder = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-f:"))
                {
                    dicomDirPath = args[i].Substring(3);
                }
                else if (args[i].StartsWith("-i:"))
                {
                    imagesFolder = args[i].Substring(3);
                }
            }
            if (string.IsNullOrEmpty(dicomDirPath) || string.IsNullOrEmpty(imagesFolder))
            {
                PrintUsage();
                return;
            }

            var dirInfo = new DirectoryInfo(imagesFolder);

            var dicomDir = new Dicom.Media.DicomDirectory();
            foreach (var file in dirInfo.GetFiles("*.*",SearchOption.AllDirectories))
            {
                var dicomFile = Dicom.DicomFile.Open(file.FullName);

                dicomDir.AddFile(dicomFile, string.Format(@"000001\{0}",file.Name));
            }

            dicomDir.Save(dicomDirPath);
        }

        private static void ReadMedia(string[] args)
        {
            string dicomDirPath = string.Empty;
            string imagesFolder = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-f:"))
                {
                    dicomDirPath = args[i].Substring(3);
                }
                else if (args[i].StartsWith("-i:"))
                {
                    imagesFolder = args[i].Substring(3);
                }
            }

            Dicom.Media.DicomDirectory.OpenMedia(dicomDirPath);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("usage: create|read -f:filePath -i:imagesFolder");
        }
    }
}
