// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class GH526
    {
        private readonly XUnitDicomLogger _logger;

        public GH526(ITestOutputHelper output)
        {
            _logger = new XUnitDicomLogger(output).IncludeTimestamps().IncludeThreadId();
        }

        #region Unit Tests

        [Fact]
        public async Task CStoreRequestSend_VideoFileServerSupportsMPEG2_TransferSuccessful()
        {
            string fileName = TestData.Resolve("ETIAM_video_002.dcm");

            var success = false;
            var handle = new ManualResetEventSlim();

            var port = Ports.GetNext();
            using (DicomServerFactory.Create<VideoCStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(fileName);
                request.OnResponseReceived = (req, rsp) =>
                {
                    success = req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.MPEG2) &&
                              rsp.Status == DicomStatus.Success;
                    handle.Set();
                };

                var client = DicomClientFactory.Create("localhost", port, false, "STORESCU", "STORESCP");
                await client.AddRequestAsync(request);
                await client.SendAsync();
                handle.Wait(10000);

                Assert.True(success);
            }
        }

        [Fact]
        public async Task CStoreRequestSend_VideoFileServerSupportsMPEG4_TransferSuccessful()
        {
            string fileName = TestData.Resolve("test_720.dcm");
            var success = false;
            var handle = new ManualResetEventSlim();

            var port = Ports.GetNext();
            using var server = DicomServerFactory.Create<VideoCStoreProvider>(port);
            server.Logger = _logger.IncludePrefix("VideoCStoreProvider");

            var request = new DicomCStoreRequest(fileName)
            {
                OnResponseReceived = (req, rsp) =>
                {
                    success = req.Dataset.InternalTransferSyntax.Equals(
                                  DicomTransferSyntax.Lookup(DicomUID.MPEG4HP41)) &&
                              rsp.Status == DicomStatus.Success;
                    handle.Set();
                }
            };

            var client = DicomClientFactory.Create("localhost", port, false, "STORESCU", "STORESCP");
            client.Logger = _logger.IncludePrefix("DicomClient");

            await client.AddRequestAsync(request);
            await client.SendAsync();
            handle.Wait(10000);

            Assert.True(success);
        }

        #endregion
    }
}
