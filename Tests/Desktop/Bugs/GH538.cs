// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading;

using Dicom.Network;

using Xunit;

namespace Dicom.Bugs
{
    public class GH538
    {
        #region Unit Tests

        [Fact]
        public void CStoreRequestSend_8And16BitJpegFiles_TransferSuccessful()
        {
            const string file1 = @"Test Data/GH538-jpeg1.dcm";
            const string file2 = @"Test Data/GH538-jpeg14sv1.dcm";
            var handle1 = new ManualResetEventSlim();
            var handle2 = new ManualResetEventSlim();
            var successes = 0;

            var port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                var request1 = new DicomCStoreRequest(file1);
                request1.OnResponseReceived = (req, rsp) =>
                {
                    if (req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.JPEGProcess1) &&
                        rsp.Status == DicomStatus.Success) ++successes;
                    handle1.Set();
                };

                var request2 = new DicomCStoreRequest(file2);
                request2.OnResponseReceived = (req, rsp) =>
                {
                    if (req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.JPEGProcess14SV1) &&
                        rsp.Status == DicomStatus.Success) ++successes;
                    handle2.Set();
                };

                var client = new DicomClient();
                client.AddRequest(request1);
                client.AddRequest(request2);

                client.Send("localhost", port, false, "STORESCU", "STORESCP");
                handle1.Wait(10000);
                handle2.Wait(10000);

                Assert.Equal(2, successes);
            }
        }

#if NETSTANDARD
        [Fact]
        public void CStoreRequestSend_16BitJpegFileToScpThatDoesNotSupportJpeg_TransferSuccessfulImplicitLENoPixelData()
        {
            const string file = @"Test Data/GH538-jpeg14sv1.dcm";
            var handle = new ManualResetEventSlim();
            var success = false;

            var port = Ports.GetNext();
            using (DicomServer.Create<VideoCStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(file);
                request.OnResponseReceived = (req, rsp) =>
                {
                    if (req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.ImplicitVRLittleEndian) &&
                        !req.Dataset.Contains(DicomTag.PixelData) && rsp.Status == DicomStatus.Success) success = true;
                    handle.Set();
                };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("localhost", port, false, "STORESCU", "STORESCP");
                handle.Wait(10000);

                Assert.True(success);
            }
        }
#endif

        #endregion
    }
}
