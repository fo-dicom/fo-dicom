// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Network;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class GH538
    {

        #region Unit Tests

        [Fact]
        public async Task CStoreRequestSend_8And16BitJpegFiles_TransferSuccessful()
        {
            string file1 = TestData.Resolve("GH538-jpeg1.dcm");
            string file2 = TestData.Resolve("GH538-jpeg14sv1.dcm");
            var handle1 = new ManualResetEventSlim();
            var handle2 = new ManualResetEventSlim();
            var successes = 0;

            var port = Ports.GetNext();
            using (DicomServerFactory.Create<SimpleCStoreProvider>(port))
            {
                var request1 = new DicomCStoreRequest(file1);
                request1.OnResponseReceived = (req, rsp) =>
                {
                    if (req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.JPEGProcess1) &&
                        rsp.Status == DicomStatus.Success)
                    {
                        ++successes;
                    }

                    handle1.Set();
                };

                var request2 = new DicomCStoreRequest(file2);
                request2.OnResponseReceived = (req, rsp) =>
                {
                    if (req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.JPEGProcess14SV1) &&
                        rsp.Status == DicomStatus.Success)
                    {
                        ++successes;
                    }

                    handle2.Set();
                };

                var client = DicomClientFactory.Create("localhost", port, false, "STORESCU", "STORESCP");
                await client.AddRequestAsync(request1);
                await client.AddRequestAsync(request2);

                await client.SendAsync();
                handle1.Wait(10000);
                handle2.Wait(10000);

                Assert.Equal(2, successes);
            }
        }


        [Fact]
        public async Task CStoreRequestSend_16BitJpegFileToScpThatDoesNotSupportJpeg_TransferSuccessfulImplicitLENoPixelData()
        {
            string file = TestData.Resolve("GH538-jpeg14sv1.dcm");
            var handle = new ManualResetEventSlim();
            var success = false;

            var port = Ports.GetNext();
            using (DicomServerFactory.Create<VideoCStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(file)
                {
                    OnResponseReceived = (req, rsp) =>
                    {
                        if (req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.ImplicitVRLittleEndian) &&
                            req.Dataset.Contains(DicomTag.PixelData) && rsp.Status == DicomStatus.Success)
                        {
                            success = true;
                        }

                        handle.Set();
                    }
                };

                var client = DicomClientFactory.Create("localhost", port, false, "STORESCU", "STORESCP");
                await client.AddRequestAsync(request);

                await client.SendAsync();
                handle.Wait(10000);

                Assert.True(success);
            }
        }

        #endregion
    }
}
