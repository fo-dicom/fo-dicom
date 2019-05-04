// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Helpers;
using Dicom.Network;
using Dicom.Network.Client;

using Xunit;
using Xunit.Abstractions;

namespace Dicom.Bugs
{
    public class GH526
    {
        private readonly XUnitDicomLogger _logger;

        public GH526(ITestOutputHelper output)
        {
            _logger = new XUnitDicomLogger(output).IncludeTimestamps().IncludeThreadId();
        }


        #region Unit Tests

        [Fact]
        public void OldCStoreRequestSend_VideoFileServerSupportsMPEG2_TransferSuccessful()
        {
            const string fileName = "GH526.dcm";
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(
                    @"https://www.creatis.insa-lyon.fr/~jpr/PUBLIC/gdcm/gdcmSampleData/DICOM_MPEG_from_ETIAM/ETIAM_video_002.dcm",
                    fileName);
            }

            var success = false;
            var handle = new ManualResetEventSlim();

            var port = Ports.GetNext();
            using (DicomServer.Create<VideoCStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(fileName);
                request.OnResponseReceived = (req, rsp) =>
                {
                    success = req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.MPEG2) &&
                              rsp.Status == DicomStatus.Success;
                    handle.Set();
                };

                var client = new Network.DicomClient();
                client.AddRequest(request);
                client.Send("localhost", port, false, "STORESCU", "STORESCP");
                handle.Wait(10000);

                Assert.True(success);
            }
        }

        [Fact]
        public async Task CStoreRequestSend_VideoFileServerSupportsMPEG2_TransferSuccessful()
        {
            const string fileName = "GH526.dcm";
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(
                    @"https://www.creatis.insa-lyon.fr/~jpr/PUBLIC/gdcm/gdcmSampleData/DICOM_MPEG_from_ETIAM/ETIAM_video_002.dcm",
                    fileName);
            }

            var success = false;
            var handle = new ManualResetEventSlim();

            var port = Ports.GetNext();
            using (DicomServer.Create<VideoCStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(fileName);
                request.OnResponseReceived = (req, rsp) =>
                {
                    success = req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.MPEG2) &&
                              rsp.Status == DicomStatus.Success;
                    handle.Set();
                };

                var client = new Network.Client.DicomClient("localhost", port, false, "STORESCU", "STORESCP");
                client.AddRequest(request);
                await client.SendAsync();
                handle.Wait(10000);

                Assert.True(success);
            }
        }

        [Fact]
        public void OldCStoreRequestSend_VideoFileServerSupportsMPEG4_TransferSuccessful()
        {
            const string fileName = @"Test Data/test_720.dcm";
            var success = false;
            var handle = new ManualResetEventSlim();

            var port = Ports.GetNext();
            using (DicomServer.Create<VideoCStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(fileName);
                request.OnResponseReceived = (req, rsp) =>
                {
                    success = req.Dataset.InternalTransferSyntax.Equals(
                                  DicomTransferSyntax.Lookup(DicomUID.MPEG4AVCH264HighProfileLevel41)) &&
                              rsp.Status == DicomStatus.Success;
                    handle.Set();
                };

                var client = new Network.DicomClient();
                client.AddRequest(request);
                client.Send("localhost", port, false, "STORESCU", "STORESCP");
                handle.Wait(10000);

                Assert.True(success);
            }
        }

        [Fact]
        public async Task CStoreRequestSend_VideoFileServerSupportsMPEG4_TransferSuccessful()
        {
            const string fileName = @"Test Data/test_720.dcm";
            var success = false;
            var handle = new ManualResetEventSlim();

            var port = Ports.GetNext();
            using (var server = DicomServer.Create<VideoCStoreProvider>(port))
            {
                server.Logger = _logger.IncludePrefix("VideoCStoreProvider");
                var request = new DicomCStoreRequest(fileName);
                request.OnResponseReceived = (req, rsp) =>
                {
                    success = req.Dataset.InternalTransferSyntax.Equals(
                                  DicomTransferSyntax.Lookup(DicomUID.MPEG4AVCH264HighProfileLevel41)) &&
                              rsp.Status == DicomStatus.Success;
                    handle.Set();
                };

                var client = new Network.Client.DicomClient("localhost", port, false, "STORESCU", "STORESCP")
                {
                    Logger = _logger.IncludePrefix("DicomClient")
                };
                client.AddRequest(request);
                await client.SendAsync();
                handle.Wait(10000);

                Assert.True(success);
            }
        }

#endregion
    }
}
