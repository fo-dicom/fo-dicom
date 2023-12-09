// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network.Client.Advanced
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class AdvancedDicomClientAssociationTests
    {
        #region Fields

        private readonly XUnitDicomLogger _logger;

        #endregion

        #region Constructors

        public AdvancedDicomClientAssociationTests(ITestOutputHelper testOutputHelper)
        {
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

        #endregion

        [Fact]
        public async Task OpenAssociation_RethrowsRejection()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "UNKNOWN-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<MockCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(cancellationToken);
                    association.Dispose();
                }
            }

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task OpenAssociation_OnDisposedConnection_ThrowsObjectDisposedException()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<MockCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            connection.Dispose();

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            // The connection is already disposed and should throw a ObjectDisposedException
            await Assert.ThrowsAsync<ObjectDisposedException>(() => connection.OpenAssociationAsync(openAssociationRequest, cancellationToken));
        }

        [Fact]
        public async Task OpenAssociation_AfterDisposingAssociationOnSameConnection_ThrowsInvalidOperationException()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<MockCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            using (var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken))
            {
                // Immediately release
                await association.ReleaseAsync(cancellationToken);
            }

            var exception = await Assert.ThrowsAsync<DicomNetworkException>(() => connection.OpenAssociationAsync(openAssociationRequest, cancellationToken));
            Assert.Equal("A connection can only be used once for one association. Create a new connection to open another association", exception.Message);
        }

        [Fact]
        public async Task OpenAssociation_AfterAlreadyOpeningAnAssociationOnSameConnection_ThrowsInvalidOperationException()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<MockCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            using (var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken))
            {
                // The connection is still usable, but cannot be used to open extra associations
                var exception = await Assert.ThrowsAsync<DicomNetworkException>(() => connection.OpenAssociationAsync(openAssociationRequest, cancellationToken));
                Assert.Equal("A connection can only be used once for one association. Create a new connection to open another association", exception.Message);

                // Immediately release
                await association.ReleaseAsync(cancellationToken);
            }
        }

        [Fact]
        public async Task Dispose_TryingToSendRequestsOnDisposedAssociation_ThrowsObjectDisposedException()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<MockCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };
            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);

            association.Dispose();

            await Assert.ThrowsAsync<ObjectDisposedException>(() => association.SendCEchoRequestAsync(cEchoRequest, CancellationToken.None));
        }

        [Fact]
        public async Task Dispose_PendingRequestOnDisposedAssociation_ThrowsObjectDisposedException()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<DicomClientTest.RecordingDicomCEchoProvider, DicomClientTest.RecordingDicomCEchoProviderServer>(port);

            server.OnCEchoRequest(request => Task.Delay(TimeSpan.FromMinutes(10), cancellationToken));

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };
            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);

            var responseTask = association.SendCEchoRequestAsync(cEchoRequest, cancellationToken);

            association.Dispose();

            await Assert.ThrowsAsync<ObjectDisposedException>(() => responseTask);
        }

        [Fact]
        public async Task C_ECHO_ReturnsResponse()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cEchoRequest = new DicomCEchoRequest();

            openAssociationRequest.PresentationContexts.AddFromRequest(cEchoRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cEchoRequest);

            DicomCEchoResponse cEchoResponse;

            using var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);
            try
            {
                cEchoResponse = await association.SendCEchoRequestAsync(cEchoRequest, cancellationToken);
            }
            finally
            {
                await association.ReleaseAsync(cancellationToken);
            }

            Assert.NotNull(cEchoResponse);
            Assert.Equal(DicomState.Success, cEchoResponse.Status.State);
        }

        [Fact]
        public async Task C_FIND_ReturnsResponse()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            var server = CreateServer<PendingAsyncDicomCFindProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cFindRequest = new DicomCFindRequest(DicomQueryRetrieveLevel.Study);

            openAssociationRequest.PresentationContexts.AddFromRequest(cFindRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cFindRequest);

            var responses = new List<DicomCFindResponse>();
            var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);

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

        [Fact]
        public async Task C_STORE_ReturnsResponse()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            var server = CreateServer<AsyncDicomCStoreProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cStoreRequest = new DicomCStoreRequest(TestData.Resolve("10200904.dcm"));

            openAssociationRequest.PresentationContexts.AddFromRequest(cStoreRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cStoreRequest);

            DicomCStoreResponse response;

            var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);

            try
            {
                response = await association.SendCStoreRequestAsync(cStoreRequest, cancellationToken);
            }
            finally
            {
                await association.ReleaseAsync(cancellationToken);
            }

            Assert.NotNull(response);
            Assert.Equal(DicomState.Success, response.Status.State);
        }

        [Fact]
        public async Task C_MOVE_ReturnsResponse()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            var server = CreateServer<AsyncDicomCMoveProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = callingAE,
                CalledAE = calledAE
            };

            var cMoveRequest = new DicomCMoveRequest("OTHER-SCP", "123");

            openAssociationRequest.PresentationContexts.AddFromRequest(cMoveRequest);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(cMoveRequest);

            var responses = new List<DicomCMoveResponse>();
            var association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);

            try
            {
                await foreach (var response in association.SendCMoveRequestAsync(cMoveRequest, cancellationToken))
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

        public class MockCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            public MockCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
                DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.AcceptTransferSyntaxes(DicomTransferSyntax.ImplicitVRLittleEndian);
                }

                foreach (var exNeg in association.ExtendedNegotiations)
                {
                    exNeg.AcceptApplicationInfo(exNeg.RequestedApplicationInfo);
                }

                if (association.CalledAE.Equals("ANY-SCP", StringComparison.OrdinalIgnoreCase))
                {
                    return SendAssociationAcceptAsync(association);
                }

                return SendAssociationRejectAsync(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.CalledAENotRecognized);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
                => SendAssociationReleaseResponseAsync();

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
                => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
        }
    }
}
