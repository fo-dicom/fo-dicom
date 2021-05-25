// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
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
        public async Task SendAsync_SingleRequest_Recognized()
        {
            var server = "127.0.0.1";
            var port = Ports.GetNext();
            var callingAE = "SCU";
            var calledAE = "ANY-SCP";
            var cancellationToken = CancellationToken.None;

            using (CreateServer<DicomCEchoProvider>(port))
            {
                var client = CreateClient();

                // TODO Alex perhaps a fluent API here?
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
                    cEchoResponse = await association.SendEchoRequestAsync(cEchoRequest, CancellationToken.None).ConfigureAwait(false);
                }
                finally
                {
                    await association.ReleaseAsync(CancellationToken.None);
                }

                Assert.NotNull(cEchoResponse);
                Assert.Equal(DicomState.Success, cEchoResponse.Status.State);
            }
        }

        #region Support classes

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

        /// <summary>
        /// Artificial C-STORE provider, only supporting Explicit LE transfer syntax for the purpose of
        /// testing <see cref="SendAsync_ToExplicitOnlyProvider_NotAccepted"/>.
        /// </summary>
        private class ExplicitLECStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
        {
            private static readonly DicomTransferSyntax[] AcceptedTransferSyntaxes =
            {
                DicomTransferSyntax.ExplicitVRLittleEndian
            };

            public ExplicitLECStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log,
                ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
                : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    if (!pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes))
                    {
                        return SendAssociationRejectAsync(DicomRejectResult.Permanent,
                            DicomRejectSource.ServiceProviderACSE, DicomRejectReason.ApplicationContextNotSupported);
                    }
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
                => SendAssociationReleaseResponseAsync();

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
                => Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success));

            public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e) => Task.CompletedTask;
        }

        public class RecordingDicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            private readonly ConcurrentBag<DicomAssociation> _associations;
            private readonly ConcurrentBag<DicomCEchoRequest> _requests;
            private readonly Func<DicomCEchoRequest, Task> _onRequest;
            private readonly TimeSpan? _responseTimeout;

            public IEnumerable<DicomCEchoRequest> Requests => _requests;
            public IEnumerable<DicomAssociation> Associations => _associations;

            public RecordingDicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log, Func<DicomCEchoRequest, Task> onRequest,
                TimeSpan? responseTimeout, ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
                : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
            {
                _onRequest = onRequest ?? throw new ArgumentNullException(nameof(onRequest));
                _responseTimeout = responseTimeout;
                _requests = new ConcurrentBag<DicomCEchoRequest>();
                _associations = new ConcurrentBag<DicomAssociation>();
            }

            /// <inheritdoc />
            public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.SetResult(DicomPresentationContextResult.Accept);
                }

                _associations.Add(association);

                await SendAssociationAcceptAsync(association).ConfigureAwait(false);
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

            public async Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
            {
                _requests.Add(request);

                await _onRequest(request);

                if (_responseTimeout.HasValue)
                {
                    await Task.Delay(_responseTimeout.Value);
                }

                return new DicomCEchoResponse(request, DicomStatus.Success);
            }
        }

        public class RecordingDicomCEchoProviderServer : DicomServer<RecordingDicomCEchoProvider>
        {
            private readonly INetworkManager _networkManager;
            private readonly ILogManager _logManager;
            private readonly ITranscoderManager _transcoderManager;
            private readonly ConcurrentBag<RecordingDicomCEchoProvider> _providers;
            private Func<DicomCEchoRequest, Task> _onRequest;
            private TimeSpan? _responseTimeout;

            public IEnumerable<RecordingDicomCEchoProvider> Providers => _providers;

            public RecordingDicomCEchoProviderServer(INetworkManager networkManager, ILogManager logManager,
                ITranscoderManager transcoderManager) : base(networkManager, logManager)
            {
                _networkManager = networkManager;
                _logManager = logManager;
                _transcoderManager = transcoderManager;
                _providers = new ConcurrentBag<RecordingDicomCEchoProvider>();
                _onRequest = _ => Task.FromResult(0);
            }

            public void OnCEchoRequest(Func<DicomCEchoRequest, Task> onRequest)
            {
                _onRequest = onRequest;
            }

            public void SetResponseTimeout(TimeSpan responseTimeout)
            {
                _responseTimeout = responseTimeout;
            }

            protected sealed override RecordingDicomCEchoProvider CreateScp(INetworkStream stream)
            {
                var provider = new RecordingDicomCEchoProvider(stream, Encoding.UTF8, Logger, _onRequest, _responseTimeout,
                    _logManager, _networkManager, _transcoderManager);
                _providers.Add(provider);
                return provider;
            }
        }


        public class RecordingDicomCGetProvider : DicomService, IDicomServiceProvider, IDicomCGetProvider
        {
            private readonly ConcurrentBag<DicomAssociation> _associations;
            private readonly ConcurrentBag<DicomCGetRequest> _requests;

            public IEnumerable<DicomCGetRequest> Requests => _requests;
            public IEnumerable<DicomAssociation> Associations => _associations;

            public RecordingDicomCGetProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
                ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
                : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
            {
                _requests = new ConcurrentBag<DicomCGetRequest>();
                _associations = new ConcurrentBag<DicomAssociation>();
            }

            /// <inheritdoc />
            public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.SetResult(DicomPresentationContextResult.Accept);
                }

                _associations.Add(association);

                await SendAssociationAcceptAsync(association).ConfigureAwait(false);
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

            public async IAsyncEnumerable<DicomCGetResponse> OnCGetRequestAsync(DicomCGetRequest request)
            {
                _requests.Add(request);
                yield return new DicomCGetResponse(request, DicomStatus.Pending);

                DicomCGetResponse result;
                try
                {
                    var file = await DicomFile.OpenAsync(TestData.Resolve("10200904.dcm")).ConfigureAwait(false);

                    var cStoreRequest = new DicomCStoreRequest(file);

                    await SendRequestAsync(cStoreRequest).ConfigureAwait(false);

                    result = new DicomCGetResponse(request, DicomStatus.Success);
                }
                catch (Exception e)
                {
                    Logger.Error("Could not send file via C-Store request: {error}", e);
                    result = new DicomCGetResponse(request, DicomStatus.ProcessingFailure);
                }

                yield return result;
            }

        }


        public class RecordingDicomCGetProviderServer : DicomServer<RecordingDicomCGetProvider>
        {
            private readonly INetworkManager _networkManager;
            private readonly ILogManager _logManager;
            private readonly ConcurrentBag<RecordingDicomCGetProvider> _providers;
            private readonly ITranscoderManager _transcoderManager;

            public IEnumerable<RecordingDicomCGetProvider> Providers => _providers;

            public RecordingDicomCGetProviderServer(INetworkManager networkManager, ILogManager logManager, ITranscoderManager transcoderManager) : base(networkManager, logManager)
            {
                _networkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
                _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
                _transcoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
                _providers = new ConcurrentBag<RecordingDicomCGetProvider>();
            }

            protected sealed override RecordingDicomCGetProvider CreateScp(INetworkStream stream)
            {
                var provider = new RecordingDicomCGetProvider(stream, Encoding.UTF8, Logger, _logManager, _networkManager, _transcoderManager);
                _providers.Add(provider);
                return provider;
            }
        }

        #endregion

    }
}
