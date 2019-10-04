// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Dicom.Network
{
    using Dicom.Helpers;
    using Dicom.Log;
    using Dicom.Network.Client;

    using Xunit;

    [Collection("Network")]
    [Trait("Category", "Network")]
    public class OldDicomCEchoProviderTest
    {
        private readonly ITestOutputHelper _output;

        public OldDicomCEchoProviderTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        [Fact]
        public void Send_FromDicomClient_DoesNotDeadlock()
        {
            var port = Ports.GetNext();
            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                server.Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomCEchoProvider");
                var client = new Network.DicomClient
                {
                    Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomClient")
                };
                for (var i = 0; i < 10; i++)
                {
                    client.AddRequest(new DicomCEchoRequest());
                }

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                Assert.False(client.IsSendRequired);
            }
        }
    }

    [Collection("Network")]
    [Trait("Category", "Network")]
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
            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                server.Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomCEchoProvider");
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId().IncludePrefix("DicomClient")
                };
                for (var i = 0; i < 10; i++)
                {
                    await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                }

                await client.SendAsync().ConfigureAwait(false);

                Assert.Empty(client.QueuedRequests);
            }
        }
    }
}
