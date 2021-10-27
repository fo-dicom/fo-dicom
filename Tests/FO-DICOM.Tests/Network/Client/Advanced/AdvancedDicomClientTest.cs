// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
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

        private readonly XUnitDicomLogger _logger;

        #endregion

        #region Constructors

        public AdvancedDicomClientTest(ITestOutputHelper testOutputHelper)
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
        public async Task OpenConnection_LoggerIsOptional()
        {
            var port = Ports.GetNext();
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
                Logger = null,
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await client.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task OpenConnection_DicomServiceOptionsIsOptional()
        {
            var port = Ports.GetNext();
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
                DicomServiceOptions = null
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await client.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task OpenConnection_FallbackEncodingIsOptional()
        {
            var port = Ports.GetNext();
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
                FallbackEncoding = null,
                DicomServiceOptions = new DicomServiceOptions()
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await client.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task OpenConnection_NetworkStreamCreationOptionsIsRequired()
        {
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var client = CreateClient();

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = null,
                Logger = _logger.IncludePrefix(nameof(AdvancedDicomClient)),
                FallbackEncoding = null,
                DicomServiceOptions = new DicomServiceOptions()
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await client.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task OpenConnection_CanBeCalledMultipleTimes()
        {
            var port = Ports.GetNext();
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

            IAdvancedDicomClientConnection connection1 = null;
            IAdvancedDicomClientConnection connection2 = null;
            IAdvancedDicomClientConnection connection3 = null;
            Exception exception = null;
            try
            {
                connection1 = await client.OpenConnectionAsync(connectionRequest, cancellationToken);
                connection2 = await client.OpenConnectionAsync(connectionRequest, cancellationToken);
                connection3 = await client.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection1?.Dispose();
                connection2?.Dispose();
                connection3?.Dispose();
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task OpenAssociation_RethrowsRejection()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "UNKNOWN-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<MockCEchoProvider>(port);

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


            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken);
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
            await Assert.ThrowsAsync<ObjectDisposedException>(() => client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken));
        }

        [Fact]
        public async Task OpenAssociation_AfterDisposingAssociationOnSameConnection_ThrowsInvalidOperationException()
        {
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<MockCEchoProvider>(port);

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

            using (var association = await client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken))
            {
                // Immediately release
                await association.ReleaseAsync(cancellationToken);
            }

            var exception = await Assert.ThrowsAsync<DicomNetworkException>(() => client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken));
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

            using (var association = await client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken))
            {
                // The connection is still usable, but cannot be used to open extra associations
                var exception = await Assert.ThrowsAsync<DicomNetworkException>(() => client.OpenAssociationAsync(connection, openAssociationRequest, cancellationToken));
                Assert.Equal("A connection can only be used once for one association. Create a new connection to open another association", exception.Message);

                // Immediately release
                await association.ReleaseAsync(cancellationToken);
            }
        }

        [Fact]
        public async Task C_ECHO_ReturnsResponse()
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
        public async Task C_FIND_ReturnsResponse()
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

        public class MockCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            public MockCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log,
                ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
                : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
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
