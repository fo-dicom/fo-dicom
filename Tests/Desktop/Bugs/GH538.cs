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
    public class GH538
    {
        #region Unit Tests

        [Fact]
        public void CStoreRequestSend_VideoFileServerSupportsMPEG4_TransferSuccessful()
        {
            const string file1 = @"Test Data/GH538-jpeg1.dcm";
            const string file2 = @"Test Data/GH538-jpeg14.dcm";
            var handle1 = new ManualResetEventSlim();
            var handle2 = new ManualResetEventSlim();
            var success = true;

            var port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                var request1 = new DicomCStoreRequest(file1);
                request1.OnResponseReceived = (req, rsp) =>
                {
                    success &= req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.JPEGProcess1) &&
                              rsp.Status == DicomStatus.Success;
                    handle1.Set();
                };

                var request2 = new DicomCStoreRequest(file2);
                request2.OnResponseReceived = (req, rsp) =>
                {
                    success &= req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.JPEGProcess14SV1) &&
                              rsp.Status == DicomStatus.Success;
                    handle2.Set();
                };

                var client = new DicomClient();
                client.AddRequest(request1);
                client.AddRequest(request2);

                client.Send("localhost", port, false, "STORESCU", "STORESCP");
                handle1.Wait(10000);
                handle2.Wait(10000);

                Assert.True(success);
            }
        }

        #endregion
    }
}
