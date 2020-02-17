// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    public class GH526 : IClassFixture<GlobalFixture>
    {
        private readonly XUnitDicomLogger _logger;
        private readonly IDicomServerFactory _serverFactory;
        private readonly IDicomClientFactory _clientFactory;

        public GH526(ITestOutputHelper output, GlobalFixture globalFixture)
        {
            _logger = new XUnitDicomLogger(output).IncludeTimestamps().IncludeThreadId();
            _serverFactory = globalFixture.GetRequiredService<IDicomServerFactory>();
            _clientFactory = globalFixture.GetRequiredService<IDicomClientFactory>();
        }

        #region Unit Tests

        [Fact]
        public async Task CStoreRequestSend_VideoFileServerSupportsMPEG2_TransferSuccessful()
        {
            string fileName = TestData.Resolve("ETIAM_video_002.dcm");

            var success = false;
            var handle = new ManualResetEventSlim();

            var port = Ports.GetNext();
            using (_serverFactory.Create<VideoCStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(fileName);
                request.OnResponseReceived = (req, rsp) =>
                {
                    success = req.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.MPEG2) &&
                              rsp.Status == DicomStatus.Success;
                    handle.Set();
                };

                var client = _clientFactory.Create("localhost", port, false, "STORESCU", "STORESCP");
                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);
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
            using (var server = _serverFactory.Create<VideoCStoreProvider>(port))
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

                var client = _clientFactory.Create("localhost", port, false, "STORESCU", "STORESCP");
                client.Logger = _logger.IncludePrefix("DicomClient");

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);
                handle.Wait(10000);

                Assert.True(success);
            }
        }

#endregion
    }
}
