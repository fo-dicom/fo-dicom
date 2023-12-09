// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
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
    public class AsyncDicomCMoveProviderTests
    {
        private readonly XUnitDicomLogger _logger;

        public AsyncDicomCMoveProviderTests(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        [Fact]
        public async Task OnCMoveRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomCMoveProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));

                var responses = new List<DicomCMoveResponse>();
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomCMoveRequest("OTHER-SCP", "123")
                {
                    OnResponseReceived = (req, res) => responses.Add(res),
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request);
                await client.SendAsync();

                Assert.NotEmpty(responses);
                Assert.Equal(DicomState.Pending, responses[0].Status.State);
                Assert.Equal(DicomState.Pending, responses[1].Status.State);
                Assert.Equal(DicomState.Success, responses[2].Status.State);
                Assert.Null(timeout);
            }
        }
    }

    #region helper classes

    public class AsyncDicomCMoveProvider : DicomService, IDicomServiceProvider, IDicomCMoveProvider
    {
        public AsyncDicomCMoveProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
            DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        /// <inheritdoc />
        public virtual async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
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
            // do nothing here
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
            // do nothing here
        }

        public async IAsyncEnumerable<DicomCMoveResponse> OnCMoveRequestAsync(DicomCMoveRequest request)
        {
            await Task.Yield();
            yield return new DicomCMoveResponse(request, DicomStatus.Pending);
            await Task.Yield();
            yield return new DicomCMoveResponse(request, DicomStatus.Pending);
            await Task.Yield();
            yield return new DicomCMoveResponse(request, DicomStatus.Success);
        }
    }


    #endregion
}
