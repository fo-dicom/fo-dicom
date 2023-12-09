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
    [Collection(TestCollections.Network)]
    public class GH1281
    {
        private readonly XUnitDicomLogger _logger;

        public GH1281(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper);
        }

        [Fact]
        public async Task SendingDeflatedDicomFileViaCStore_ShouldWork()
        {
            using var server = DicomServerFactory.Create<AsyncDicomCStoreProvider>("127.0.0.1", Ports.GetNext());

            server.Options.LogDimseDatasets = true;
            server.Logger = _logger.IncludePrefix("Server");

            var dicomClient = DicomClientFactory.Create("127.0.0.1", server.Port, false, "AnySCU", "ANYSCP");
            dicomClient.ServiceOptions.LogDimseDatasets = true;
            dicomClient.Logger = _logger.IncludePrefix("Client");

            DicomCStoreResponse response = null;

            var file = "Test Data/Issue1097_FailToOpenDeflatedFileWithSQ.dcm";

            var dicomFile = DicomFile.Open(file);
            Assert.NotNull(dicomFile);

            var cStoreRequest = new DicomCStoreRequest(dicomFile)
            {
                OnResponseReceived = (req, res) =>
                {
                    response = res;
                }
            };

            await dicomClient.AddRequestAsync(cStoreRequest);

            await dicomClient.SendAsync(CancellationToken.None);

            Assert.Equal(DicomState.Success, response.Status.State);
        }
    }
}
