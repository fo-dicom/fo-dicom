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
    [Collection("Network")]
    public class GH1597
    {
        private readonly ITestOutputHelper _output;

        public GH1597(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task UnlimitedAsyncOpsInvokedShouldBeSupported()
        {
            var port = Ports.GetNext();
            var logger = new XUnitDicomLogger(_output).IncludeTimestamps().IncludeThreadId();
            using var server = DicomServerFactory.Create<AsyncDicomCEchoProvider>(port, logger: logger.IncludePrefix("Server"));
            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(0,0);
            client.Logger = logger.IncludePrefix("Client");

            var numberOfRequests = 100;
            var counter = 0;
            for (var i = 0; i < numberOfRequests; i++)
            {
                var request = new DicomCEchoRequest
                    { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };
                await client.AddRequestAsync(request).ConfigureAwait(false);
            }

            await client.SendAsync().ConfigureAwait(false);

            Assert.Equal(numberOfRequests, counter);
        }
    }
}
