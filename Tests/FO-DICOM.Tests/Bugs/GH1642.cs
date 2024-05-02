using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network), TestCaseOrderer("FellowOakDicom.Tests.Helpers.PriorityOrderer", "fo-dicom.Tests")]
    public class GH1642
    {
        private readonly XUnitDicomLogger _logger;

        public GH1642(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper).IncludeTimestamps().IncludeThreadId();
        }

        [Fact]
        public async Task DicomClientFallbackEncoding()
        {
            var port = Ports.GetNext();
            var serverLogger = _logger.IncludePrefix("Server").WithMinimumLevel(LogLevel.Information);
            var clientLogger = _logger.IncludePrefix("Client").WithMinimumLevel(LogLevel.Information);

            using (var server = DicomServerFactory.Create<CFindProvider>("127.0.0.1", port, logger: serverLogger, fallbackEncoding: DicomEncoding.GetEncoding("ISO_IR 100")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "CLIENT", "SERVER");
                client.FallbackEncoding = DicomEncoding.GetEncoding("ISO_IR 100");
                var request = DicomCFindRequest.CreateWorklistQuery();
                var responses = new List<DicomDataset>();
                request.OnResponseReceived += (DicomCFindRequest req, DicomCFindResponse resp) =>
                {
                    if (resp.Status == DicomStatus.Pending)
                    {
                        responses.Add(resp.Dataset);
                    }
                };

                await client.AddRequestAsync(request);
                await client.SendAsync();

                Assert.Equal(2, responses.Count);
                Assert.Equal("Διονυσιος", responses[1].GetString(DicomTag.PatientName));
                Assert.Equal("Müller^Günther", responses[0].GetString(DicomTag.PatientName));
            }
        }
    }


    public class CFindProvider : DicomService, IDicomServiceProvider, IDicomCFindProvider
    {
        public CFindProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger logger, DicomServiceDependencies dependencies) : base(stream, fallbackEncoding, logger, dependencies)
        {
        }

        public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request)
        {
            yield return new DicomCFindResponse(request, DicomStatus.Pending)
            {
                Dataset = new DicomDataset(
                    new DicomPersonName(DicomTag.PatientName, "Müller^Günther")
                    )
            };
            yield return new DicomCFindResponse(request, DicomStatus.Pending)
            {
                Dataset = new DicomDataset(
                    new DicomCodeString(DicomTag.SpecificCharacterSet, "ISO 2022 IR 126"),
                    new DicomPersonName(DicomTag.PatientName, "Διονυσιος")
                    )
            };
            yield return new DicomCFindResponse(request, DicomStatus.Success);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(DicomPresentationContextResult.Accept);
            }

            await SendAssociationAcceptAsync(association).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationReleaseRequestAsync()
        {
            await SendAssociationReleaseResponseAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
        }
    }


}
