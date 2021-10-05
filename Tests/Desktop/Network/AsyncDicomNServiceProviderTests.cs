// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
using System.Threading.Tasks;
using Dicom.Helpers;
using Dicom.Log;
using Xunit;
using Xunit.Abstractions;

namespace Dicom.Network
{
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

            using (DicomServer.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

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

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }

        [Fact]
        public async Task OnNCreateRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

                DicomNCreateResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNCreateRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
                {
                    OnResponseReceived = (req, res) => response = res,
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }

        [Fact]
        public async Task OnNDeleteRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

                DicomNDeleteResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNDeleteRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
                {
                    OnResponseReceived = (req, res) => response = res,
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }

        [Fact]
        public async Task OnNEventReportRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

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

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }

        [Fact]
        public async Task OnNGetRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

                DicomNGetResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNGetRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
                {
                    OnResponseReceived = (req, res) => response = res,
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }

        [Fact]
        public async Task OnNSetRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<AsyncDicomNServiceProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

                DicomNSetResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNSetRequest(
                    DicomUID.BasicFilmSession,
                    new DicomUID("1.2.3", null, DicomUidType.SOPInstance))
                {
                    OnResponseReceived = (req, res) => response = res,
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }
    }

    #region helper classes

    public class AsyncDicomNServiceProvider : DicomService, IDicomServiceProvider, IAsyncDicomNServiceProvider
    {
        public AsyncDicomNServiceProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
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

        public Task<DicomNActionResponse> OnNActionRequestAsync(DicomNActionRequest request)
        {
            return Task.FromResult(new DicomNActionResponse(request, DicomStatus.Success));
        }

        public Task<DicomNCreateResponse> OnNCreateRequestAsync(DicomNCreateRequest request)
        {
            return Task.FromResult(new DicomNCreateResponse(request, DicomStatus.Success));
        }

        public Task<DicomNDeleteResponse> OnNDeleteRequestAsync(DicomNDeleteRequest request)
        {
            return Task.FromResult(new DicomNDeleteResponse(request, DicomStatus.Success));
        }

        public Task<DicomNEventReportResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request)
        {
            return Task.FromResult(new DicomNEventReportResponse(request, DicomStatus.Success));
        }

        public Task<DicomNGetResponse> OnNGetRequestAsync(DicomNGetRequest request)
        {
            return Task.FromResult(new DicomNGetResponse(request, DicomStatus.Success));
        }

        public Task<DicomNSetResponse> OnNSetRequestAsync(DicomNSetRequest request)
        {
            return Task.FromResult(new DicomNSetResponse(request, DicomStatus.Success));
        }
        public Task OnSendNEventReportRequestAsync(DicomNActionRequest request)
        {
            return SendRequestAsync(new DicomNEventReportRequest(DicomUID.StorageCommitmentPushModel, DicomUID.StorageCommitmentPushModel, 2)
            {
                Dataset = request.Dataset
            });
        }
    }
    #endregion
}
