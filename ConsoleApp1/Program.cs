using Dicom;
using Dicom.Log;
using Dicom.Network;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class Program
    {
        private static readonly string QRServerHost = "127.0.0.1";

        private static readonly int QRServerPort = 10400;

        private static readonly string QRServerAET = "DCMQRSCP";

        private static readonly string AET = "ANY-SCU";

        private static int CEchoPort = 12345;

        private static async Task Main(string[] args)
        {
            LogManager.SetImplementation(ConsoleLogManager.Instance);

            CFindSerial();

            await CEchoSerialAsync(1000);

            await CEchoParallelAsync(200);

            Console.ReadLine();
        }

        private static void CFindSerial()
        {
            for (int i = 0; i < 10; ++i)
            {
                QueryStudies();
            }
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="expected"></param>
        private static async Task CEchoSerialAsync(int expected)
        {
            var port = CEchoPort++;

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) await Task.Delay(50);

                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; i++)
                {
                    client.AddRequest(
                        new DicomCEchoRequest
                        {
                            OnResponseReceived = (req, res) =>
                            {
                                Interlocked.Increment(ref actual);
                            }
                        }
                    );
                    await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP", 600 * 1000);
                }

                Debug.Assert(expected == actual);
            }
        }

        /// </summary>
        /// <param name="expected"></param>
        private static async Task CEchoParallelAsync(int expected)
        {
            int port = CEchoPort++;

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) await Task.Delay(50);

                var actual = 0;

                var requests = Enumerable.Range(0, expected).Select(
                    async requestIndex =>
                    {
                        var client = new DicomClient();
                        client.AddRequest(
                            new DicomCEchoRequest
                            {
                                OnResponseReceived = (req, res) =>
                                {
                                    Debug.WriteLine("Response #{0}", requestIndex);
                                    Interlocked.Increment(ref actual);
                                }
                            }
                        );

                        Debug.WriteLine("Sending #{0}", requestIndex);
                        await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP", 600 * 1000);
                        Debug.WriteLine("Sent (or timed out) #{0}", requestIndex);
                    }
                ).ToArray();

                await Task.WhenAll(requests);

                Debug.Assert(expected == actual);
            }
        }
    }
}
