// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dicom.Log;
using Dicom.Network;

using Xunit;

namespace Dicom.Bugs
{
    public class GH526
    {
        #region Unit Tests

        [Fact]
        public void CStoreRequestSend_VideoFileServerSupportsMPEG2_TransferSuccessful()
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

                var client = new DicomClient();
                client.AddRequest(request);
                client.Send("localhost", port, false, "STORESCU", "STORESCP");
                handle.Wait(10000);

                Assert.True(success);
            }
        }

        [Fact]
        public void CStoreRequestSend_VideoFileServerSupportsMPEG4_TransferSuccessful()
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

                var client = new DicomClient();
                client.AddRequest(request);
                client.Send("localhost", port, false, "STORESCU", "STORESCP");
                handle.Wait(10000);

                Assert.True(success);
            }
        }

        #endregion

        #region Support classes

        private class VideoCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
        {
            private static readonly DicomTransferSyntax[] AcceptedVideoTransferSyntaxes =
                {
                    DicomTransferSyntax.MPEG2,
                    DicomTransferSyntax.Lookup(DicomUID.MPEG4AVCH264HighProfileLevel41) 
                };

            public VideoCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
                : base(stream, fallbackEncoding, log)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.AcceptTransferSyntaxes(AcceptedVideoTransferSyntaxes);
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
            {
                var tempName = Path.GetTempFileName();
                Logger.Info(tempName);
                request.File.Save(tempName);

                return new DicomCStoreResponse(request, DicomStatus.Success);
            }

            public void OnCStoreRequestException(string tempFileName, Exception e)
            {
            }
        }

        #endregion
    }
}
