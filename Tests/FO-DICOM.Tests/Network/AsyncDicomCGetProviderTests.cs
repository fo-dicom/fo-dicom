// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Concurrent;
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
    public class AsyncDicomCGetProviderTests
    {
        private readonly XUnitDicomLogger _logger;

        public AsyncDicomCGetProviderTests(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        [Fact]
        public async Task OnCGetRequestAsync_ImmediateSuccess_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<ImmediateSuccessAsyncDicomCGetProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomCGetResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var studyInstanceUID = DicomUIDGenerator.GenerateDerivedFromUUID().ToString();
                var request = new DicomCGetRequest(studyInstanceUID)
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

        [Fact]
        public async Task OnCGetRequestAsync_Pending_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<PendingAsyncDicomCGetProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                var responses = new ConcurrentQueue<DicomCGetResponse>();
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var studyInstanceUID = DicomUIDGenerator.GenerateDerivedFromUUID().ToString();
                var request = new DicomCGetRequest(studyInstanceUID)
                {
                    OnResponseReceived = (req, res) => responses.Enqueue(res),
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request);
                await client.SendAsync();

                Assert.Collection(
                    responses,
                    response1 => Assert.Equal(DicomStatus.Pending, response1.Status),
                    response2 => Assert.Equal(DicomStatus.Pending, response2.Status),
                    response3 => Assert.Equal(DicomStatus.Success, response3.Status)
                );
                Assert.Null(timeout);
            }
        }
    }

    #region helper classes

    public class ImmediateSuccessAsyncDicomCGetProvider : DicomService, IDicomServiceProvider, IDicomCGetProvider
    {
        public ImmediateSuccessAsyncDicomCGetProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
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
            // do nothing here
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
            // do nothing here
        }

        public async IAsyncEnumerable<DicomCGetResponse> OnCGetRequestAsync(DicomCGetRequest request)
        {
            await Task.Yield();
            yield return new DicomCGetResponse(request, DicomStatus.Success);
        }

    }


    public class PendingAsyncDicomCGetProvider : DicomService, IDicomServiceProvider, IDicomCGetProvider
    {
        public PendingAsyncDicomCGetProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
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
            // do nothing here
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
            // do nothing here
        }

        public async IAsyncEnumerable<DicomCGetResponse> OnCGetRequestAsync(DicomCGetRequest request)
        {
            await Task.Yield();
            yield return new DicomCGetResponse(request, DicomStatus.Pending);
            yield return new DicomCGetResponse(request, DicomStatus.Pending);
            yield return new DicomCGetResponse(request, DicomStatus.Success);
        }

    }


    #endregion
}
