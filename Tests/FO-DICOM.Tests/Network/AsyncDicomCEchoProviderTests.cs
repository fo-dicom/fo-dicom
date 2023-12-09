// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class AsyncDicomCEchoProviderTests
    {
        private readonly XUnitDicomLogger _logger;

        public AsyncDicomCEchoProviderTests(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        [Fact]
        public async Task OnCEchoRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomCEchoResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomCEchoRequest
                {
                    OnResponseReceived = (req, res) => response = res,
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request);
                await client.SendAsync();

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }
    }

    #region helper classes

    public class AsyncDicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
    {
        public AsyncDicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
            DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(DicomPresentationContextResult.Accept);
            }

            await SendAssociationAcceptAsync(association);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationReleaseRequestAsync()
            => await SendAssociationReleaseResponseAsync().ConfigureAwait(false);

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // do nothing, ignore
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
            // do nothing here
        }

        public async Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
        {
            await Task.Yield();
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }


    #endregion
}
