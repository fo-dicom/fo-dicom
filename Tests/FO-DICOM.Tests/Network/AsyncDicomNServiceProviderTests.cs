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
    public class AsyncDicomNServiceProviderTests
    {
        private readonly XUnitDicomLogger _logger;

        public AsyncDicomNServiceProviderTests(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        [Fact]
        public async Task OnNActionRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomNActionResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNActionRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                    1)
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
        public async Task OnNCreateRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomNCreateResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNCreateRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
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
        public async Task OnNDeleteRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomNDeleteResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNDeleteRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
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
        public async Task OnNEventReportRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(nameof(DicomClient));
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomNEventReportResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNEventReportRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                    1)
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
        public async Task OnNGetRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name);
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomNGetResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNGetRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
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
        public async Task OnNSetRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name);
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

                DicomNSetResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNSetRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
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

    public class AsyncDicomNServiceProvider : DicomService, IDicomServiceProvider, IDicomNServiceProvider
    {
        public AsyncDicomNServiceProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
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

        public Task<DicomNActionResponse> OnNActionRequestAsync(DicomNActionRequest request)
            => Task.FromResult(new DicomNActionResponse(request, DicomStatus.Success));

        public Task<DicomNCreateResponse> OnNCreateRequestAsync(DicomNCreateRequest request)
            => Task.FromResult(new DicomNCreateResponse(request, DicomStatus.Success));

        public Task<DicomNDeleteResponse> OnNDeleteRequestAsync(DicomNDeleteRequest request)
            => Task.FromResult(new DicomNDeleteResponse(request, DicomStatus.Success));

        public Task<DicomNEventReportResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request)
            => Task.FromResult(new DicomNEventReportResponse(request, DicomStatus.Success));

        public Task<DicomNGetResponse> OnNGetRequestAsync(DicomNGetRequest request)
            => Task.FromResult(new DicomNGetResponse(request, DicomStatus.Success));

        public Task<DicomNSetResponse> OnNSetRequestAsync(DicomNSetRequest request)
            => Task.FromResult(new DicomNSetResponse(request, DicomStatus.Success));

        public Task OnSendNEventReportRequestAsync(DicomNActionRequest request)
            => SendRequestAsync(new DicomNEventReportRequest(DicomUID.StorageCommitmentPushModel, DicomUID.StorageCommitmentPushModel, 2)
            {
                Dataset = request.Dataset
            });
    }
    #endregion
}
