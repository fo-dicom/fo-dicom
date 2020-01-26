// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection("Network")]
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
                var client = new FellowOakDicom.Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(FellowOakDicom.Network.Client.DicomClient).Name)
                };

                DicomNActionResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNActionRequest(
                    DicomUID.BasicFilmSessionSOPClass,
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
                var client = new FellowOakDicom.Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(FellowOakDicom.Network.Client.DicomClient).Name)
                };

                DicomNCreateResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNCreateRequest(
                    DicomUID.BasicFilmSessionSOPClass,
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
                var client = new FellowOakDicom.Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(FellowOakDicom.Network.Client.DicomClient).Name)
                };

                DicomNDeleteResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNDeleteRequest(
                    DicomUID.BasicFilmSessionSOPClass,
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
                var client = new FellowOakDicom.Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(FellowOakDicom.Network.Client.DicomClient).Name)
                };

                DicomNEventReportResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNEventReportRequest(
                    DicomUID.BasicFilmSessionSOPClass,
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
                var client = new FellowOakDicom.Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(FellowOakDicom.Network.Client.DicomClient).Name)
                };

                DicomNGetResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNGetRequest(
                    DicomUID.BasicFilmSessionSOPClass,
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
                var client = new FellowOakDicom.Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(FellowOakDicom.Network.Client.DicomClient).Name)
                };

                DicomNSetResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomNSetRequest(
                    DicomUID.BasicFilmSessionSOPClass,
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

    public class AsyncDicomNServiceProvider : DicomService, IDicomServiceProvider, IDicomNServiceProvider
    {
        public AsyncDicomNServiceProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        async Task WaitForALittleBit()
        {
            var ms = new Random().Next(10);
            await Task.Delay(ms).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            await WaitForALittleBit().ConfigureAwait(false);

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
    }
    #endregion
}
