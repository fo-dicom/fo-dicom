using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection("Network")]
    public class GH1468
    {
        private readonly XUnitDicomLogger _logger;

        public GH1468(ITestOutputHelper output)
        {
            _logger = new XUnitDicomLogger(output).IncludeTimestamps().IncludeThreadId();
        }

        [Fact]
        public async Task WhenInitialConnectionIsNeverClosed_DicomServerShouldCorrectlyImplementMaxClientsAllowed()
        {
            // Arrange
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = DicomServerFactory.Create<DicomCEchoProvider>(
                port,
                logger: _logger.IncludePrefix("Server"),
                configure: o => o.MaxClientsAllowed = 2);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "ANY-SCP"
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(new DicomCEchoRequest());

            // Act
            // This connection only closes at the end of the test
            connectionRequest.Logger = _logger.IncludePrefix("Client1");
            using var connection1 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
            using var association1 = await connection1.OpenAssociationAsync(openAssociationRequest, cancellationToken);
            var response1 = await association1.SendCEchoRequestAsync(new DicomCEchoRequest(), cancellationToken);

            // This connection opens and closes
            DicomCEchoResponse response2;
            {
                connectionRequest.Logger = _logger.IncludePrefix("Client2");
                using var connection2 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
                using var association2 = await connection2.OpenAssociationAsync(openAssociationRequest, cancellationToken);
                response2 = await association1.SendCEchoRequestAsync(new DicomCEchoRequest(), cancellationToken);
                await association2.ReleaseAsync(cancellationToken);
            }

            // This connection opens and closes
            DicomCEchoResponse response3;
            {
                connectionRequest.Logger = _logger.IncludePrefix("Client3");
                using var connection3 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
                using var association3 = await connection3.OpenAssociationAsync(openAssociationRequest, cancellationToken);
                response3 = await association1.SendCEchoRequestAsync(new DicomCEchoRequest(), cancellationToken);
                await association3.ReleaseAsync(cancellationToken);
            }

            // Assert
            Assert.NotNull(response1);
            Assert.NotNull(response2);
            Assert.NotNull(response3);
            Assert.Equal(DicomState.Success, response1.Status.State);
            Assert.Equal(DicomState.Success, response2.Status.State);
            Assert.Equal(DicomState.Success, response3.Status.State);
        }
    }
}
