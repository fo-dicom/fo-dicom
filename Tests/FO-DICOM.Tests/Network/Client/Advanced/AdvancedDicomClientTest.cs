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

        public class Echo : AdvancedDicomClientTest
        {
            public Echo(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

            [Fact]
            public async Task SendAsync_SingleRequest_Recognized()
            {
                var server = "127.0.0.1";
                var port = Ports.GetNext();
                var callingAE = "SCU";
                var calledAE = "ANY-SCP";
                var cancellationToken = CancellationToken.None;

                using (CreateServer<AsyncDicomCEchoProvider>(port))
                {
                    var client = CreateClient();

                    var connectionRequest = new AdvancedDicomClientConnectionRequest
                    {
                        NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                        {
                            Host = server,
                            Port = port,
                        },
                        Logger = _logger.IncludePrefix(nameof(IAdvancedDicomClient)),
                        FallbackEncoding = DicomEncoding.Default,
                        DicomServiceOptions = new DicomServiceOptions()
                    };

                    var connection = await client.OpenConnectionAsync(connectionRequest, cancellationToken);

                    var openAssociationRequest = new AdvancedDicomClientAssociationRequest
                    {
                        CallingAE = callingAE,
                        CalledAE = calledAE
                    };

                    var cEchoRequest = new DicomCEchoRequest();

                    openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
                    openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

                    DicomCEchoResponse cEchoResponse = null;
                    var association = await client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken);

                    try
                    {
                        cEchoResponse = await association.SendCEchoRequestAsync(cEchoRequest, CancellationToken.None).ConfigureAwait(false);
                    }
                    finally
                    {
                        await association.ReleaseAsync(CancellationToken.None);
                    }

                    Assert.NotNull(cEchoResponse);
                    Assert.Equal(DicomState.Success, cEchoResponse.Status.State);
                }
            }

        }

        public class Find : AdvancedDicomClientTest
        {
            public Find(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

            [Fact]
            public async Task SendAsync_SingleRequest_Recognized()
            {
                var server = "127.0.0.1";
                var port = Ports.GetNext();
                var callingAE = "SCU";
                var calledAE = "ANY-SCP";
                var cancellationToken = CancellationToken.None;

                using (CreateServer<PendingAsyncDicomCFindProvider>(port))
                {
                    var client = CreateClient();

                    var connectionRequest = new AdvancedDicomClientConnectionRequest
                    {
                        NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                        {
                            Host = server,
                            Port = port,
                        },
                        Logger = _logger.IncludePrefix(nameof(IAdvancedDicomClient)),
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
                        await foreach (var response in association.SendCFindRequestAsync(cFindRequest, CancellationToken.None).ConfigureAwait(false))
                        {
                            responses.Add(response);
                        }
                    }
                    finally
                    {
                        await association.ReleaseAsync(CancellationToken.None);
                    }

                    Assert.NotEmpty(responses);
                    Assert.Equal(3, responses.Count);
                    Assert.Equal(DicomState.Pending, responses[0].Status.State);
                    Assert.Equal(DicomState.Pending, responses[1].Status.State);
                    Assert.Equal(DicomState.Success, responses[2].Status.State);
                }
            }

        }
    }
}
