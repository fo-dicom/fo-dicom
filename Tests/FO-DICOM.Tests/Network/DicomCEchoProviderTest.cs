// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using FellowOakDicom.Tests.Helpers;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{

    [Collection("Network")]
    [Trait("Category", "Network")]
    public class DicomCEchoProviderTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IDicomServerFactory _serverFactory;
        private readonly IDicomClientFactory _clientFactory;

        public DicomCEchoProviderTest(ITestOutputHelper output, GlobalFixture globalFixture)
        {
            _output = output;
            _serverFactory = globalFixture.GetRequiredService<IDicomServerFactory>();
            _clientFactory = globalFixture.GetRequiredService<IDicomClientFactory>();
        }

        [Fact]
        public async Task Send_FromDicomClient_DoesNotDeadlock()
        {
            var port = Ports.GetNext();
            using (var server = _serverFactory.Create<DicomCEchoProvider>(port))
            {
                server.Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomCEchoProvider");
                var client = _clientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomClient");

                for (var i = 0; i < 10; i++)
                {
                    await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                }

                await client.SendAsync().ConfigureAwait(false);

                Assert.False(client.IsSendRequired);
            }
        }
    }
}
