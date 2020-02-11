// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection("Network")]
    public class AsyncDicomCStoreProviderTests : IClassFixture<GlobalFixture>
    {
        private readonly XUnitDicomLogger _logger;
        private readonly IDicomServerFactory _serverFactory;
        private readonly IDicomClientFactory _clientFactory;

        public AsyncDicomCStoreProviderTests(ITestOutputHelper testOutputHelper, GlobalFixture globalFixture)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
            _serverFactory = globalFixture.GetRequiredService<IDicomServerFactory>();
            _clientFactory = globalFixture.GetRequiredService<IDicomClientFactory>();
        }

        [Fact]
        public async Task OnCStoreRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (_serverFactory.Create<AsyncDicomCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = _clientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name);

                DicomCStoreResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomCStoreRequest(TestData.Resolve("10200904.dcm"))
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

    public class AsyncDicomCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        public AsyncDicomCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log,
            ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
            : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
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

        public async Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
        {
            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
            => throw new NotImplementedException();
    }


    #endregion
}
