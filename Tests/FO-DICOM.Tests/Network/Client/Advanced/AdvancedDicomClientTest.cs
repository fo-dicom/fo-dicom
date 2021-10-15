// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client.Advanced;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network.Client.Advanced
{
    [Collection("Network"), Trait("Category", "Network")]
    public class AdvancedDicomClientTest
    {
        #region Fields

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly XUnitDicomLogger _logger;

        #endregion

        #region Constructors

        public AdvancedDicomClientTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        #endregion

        #region Helper functions

        private IDicomServer CreateServer<T>(int port) where T : DicomService, IDicomServiceProvider
        {
            var server = DicomServerFactory.Create<T>(port);
            server.Logger = _logger.IncludePrefix(nameof(IDicomServer));
            return server;
        }

        private TServer CreateServer<TProvider, TServer>(int port)
            where TProvider : DicomService, IDicomServiceProvider
            where TServer : class, IDicomServer<TProvider>
        {
            var logger = _logger.IncludePrefix(nameof(IDicomServer));
            var ipAddress = NetworkManager.IPv4Any;
            var server = DicomServerFactory.Create<TProvider, TServer>(ipAddress, port, logger: logger);
            server.Options.LogDimseDatasets = false;
            server.Options.LogDataPDUs = false;
            return server as TServer;
        }

        private IAdvancedDicomClient CreateClient()
        {
            var client = AdvancedDicomClientFactory.Create(new AdvancedDicomClientCreationRequest
            {
                Logger = _logger.IncludePrefix(nameof(AdvancedDicomClient))
            });
            return client;
        }

        #endregion

        [Fact]
        public async Task SendAsync_C_ECHO_ReturnsResponse()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var client = CreateClient();

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix(nameof(AdvancedDicomClient)),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await client.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            DicomCEchoResponse cEchoResponse = null;

            using var association = await client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken);
            try
            {
                cEchoResponse = await association.SendCEchoRequestAsync(cEchoRequest, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                await association.ReleaseAsync(cancellationToken);
            }

            Assert.NotNull(cEchoResponse);
            Assert.Equal(DicomState.Success, cEchoResponse.Status.State);
        }

        [Fact]
        public async Task SendAsync_C_FIND_ReturnsResponse()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            var server = CreateServer<PendingAsyncDicomCFindProvider>(port);
            var client = CreateClient();

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix(nameof(AdvancedDicomClient)),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            var connection = await client.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cFindRequest = new DicomCFindRequest(DicomQueryRetrieveLevel.Study);

            openAssociationRequest.PresentationContexts.AddFromRequest(cFindRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cFindRequest);

            var responses = new List<DicomCFindResponse>();
            var association = await client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken);

            try
            {
                await foreach (var response in association.SendCFindRequestAsync(cFindRequest, cancellationToken).ConfigureAwait(false))
                {
                    responses.Add(response);
                }
            }
            finally
            {
                await association.ReleaseAsync(cancellationToken);
            }

            Assert.NotEmpty(responses);
            Assert.Equal(3, responses.Count);
            Assert.Equal(DicomState.Pending, responses[0].Status.State);
            Assert.Equal(DicomState.Pending, responses[1].Status.State);
            Assert.Equal(DicomState.Success, responses[2].Status.State);
        }
    }
}
