// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection(TestCollections.Network)]
    [Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomCEchoProviderTest
    {
        private readonly ITestOutputHelper _output;

        public DicomCEchoProviderTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Send_FromDicomClient_DoesNotDeadlock()
        {
            var port = Ports.GetNext();
            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port);

            server.Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomCEchoProvider");
            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomClient");

            for (var i = 0; i < 10; i++)
            {
                await client.AddRequestAsync(new DicomCEchoRequest());
            }

            await client.SendAsync();

            Assert.False(client.IsSendRequired);
        }

    }
}
