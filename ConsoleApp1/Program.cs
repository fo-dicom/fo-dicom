using Dicom;
using Dicom.Log;
using Dicom.Network;
using System;
using System.Threading;

namespace ConsoleApp1
{
    internal static class Program
    {

        private static readonly string QRServerHost = "127.0.0.1";

        private static readonly int QRServerPort = 10400;

        private static readonly string QRServerAET = "DCMQRSCP";

        private static readonly string AET = "ANY-SCU";

        private static void Main(string[] args)
        {
            LogManager.SetImplementation(ConsoleLogManager.Instance);

            Console.WriteLine("VOR ERSTEM");
            QueryStudies();
            Console.WriteLine("DAZWISCHEN1");
            QueryStudies();
            Console.WriteLine("DAZWISCHEN2");
            QueryStudies();
            Console.WriteLine("DAZWISCHEN3");
            QueryStudies();
            Console.WriteLine("NACHHER");

            Console.WriteLine("FERTIG");
            Console.ReadLine();
        }

        /// <summary>
        ///
        /// </summary>
        private static void QueryStudies()
        {
            var request = CreateCFindRequest();
            request.OnResponseReceived += (DicomCFindRequest req, DicomCFindResponse resp) =>
            {
                if (resp.Status == DicomStatus.Pending && resp.HasDataset)
                {
                    Console.WriteLine(resp.Dataset.GetSingleValueOrDefault(DicomTag.StudyInstanceUID, ""));
                }
            };

            var client = new DicomClient();
            client.NegotiateAsyncOps();
            client.AddRequest(request);

            //var ev = new AutoResetEvent(false);
            //client.AssociationReleased +=
            //    (s, e) =>
            //    {
            //        ev.Set();
            //        };
            client.Send(QRServerHost, QRServerPort, false, AET, QRServerAET);
               //  ev.WaitOne();
               //client.Abort();
        }



        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private static DicomCFindRequest CreateCFindRequest()
        {
            var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Study);

            request.Dataset.AddOrUpdate(DicomTag.AccessionNumber, "");
            request.Dataset.AddOrUpdate(DicomTag.PatientBirthDate, "");
            request.Dataset.AddOrUpdate(DicomTag.PatientID, "");
            request.Dataset.AddOrUpdate(DicomTag.PatientName, "");
            request.Dataset.AddOrUpdate(DicomTag.PatientSex, "");
            request.Dataset.AddOrUpdate(DicomTag.SpecificCharacterSet, @"\ISO 2022 IR 13\ISO 2022 IR 87");
            request.Dataset.AddOrUpdate(DicomTag.StudyDate, "20170404");
            request.Dataset.AddOrUpdate(DicomTag.StudyInstanceUID, "");

            return request;
        }
    }
}
