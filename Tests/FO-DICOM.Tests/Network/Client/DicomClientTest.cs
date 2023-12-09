// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Network.Client.States;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

// These tests cover some obsolete properties
#pragma warning disable CS0618

namespace FellowOakDicom.Tests.Network.Client
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomClientTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly XUnitDicomLogger _logger;

        #region Fields

        private static string _remoteHost;

        private static int _remotePort;

        #endregion

        #region Constructors

        public DicomClientTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
            _remoteHost = null;
            _remotePort = 0;
        }

        #endregion

        #region Helper functions

        private IDicomServer CreateServer<T>(int port, Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider
        {
            var server = DicomServerFactory.Create<T>(port, configure: configure);
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

        private IDicomClient CreateClient(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            var client = DicomClientFactory.Create(host, port, useTls, callingAe, calledAe);
            client.Logger = _logger.IncludePrefix(nameof(DicomClient));
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            return client;
        }

        #endregion

        [Fact]
        public async Task SendAsync_MultipleRequestsWhileAlreadySending_ReusesSameAssociation()
        {
            int port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);
            var counter = 0;

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);
            for (var i = 0; i < 5; i++)
            {
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };
                await client.AddRequestAsync(request);
            }

            bool moreRequestsSent = false;

            client.StateChanged += async (sender, args) =>
            {
                if (moreRequestsSent)
                {
                    return;
                }

                if (args.NewState is DicomClientSendingRequestsState)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };
                        await client.AddRequestAsync(request);
                    }

                    moreRequestsSent = true;
                }
            };

            await client.SendAsync();

            Assert.Equal(10, counter);
            Assert.Single(server.Providers.SelectMany(p => p.Associations));
        }

        [Fact]
        public async Task SendAsync_SingleRequest_Recognized()
        {
            int port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(request);

                var task = client.SendAsync();
                //await Task.WhenAny(task, Task.Delay(10000));
                await task;
                Assert.Equal(1, counter);
            }
        }

        [Fact]
        public async Task LogAssociationProperties()
        {
            var writer = new StringWriter();
            var logger = new TextWriterLogger(writer);

            int port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var request = new DicomCEchoRequest { };
                var client = CreateClient("127.0.0.1", port, false, "LOG-SCU", "ANY-SCP");
                client.Logger = logger;

                await client.AddRequestAsync(request);
                await client.SendAsync();
            }

            var logcontent = writer.ToString();
            Assert.Contains("\nRemote host:            127.0.0.1\n", logcontent);
            Assert.Contains($"\nRemote port:            {port}\n", logcontent);
            Assert.Contains("\nCalling AE Title:       LOG-SCU\n", logcontent);
            Assert.Contains("\nCalled AE Title:        ANY-SCP\n", logcontent);
        }

        [Fact]
        public async Task AutomaticallyFixTooLongAETitles()
        {
            // Arrange
            var port = Ports.GetNext();
            var counter = 0;
            using var server = CreateServer<ConfigurableDicomCEchoProvider, ConfigurableDicomCEchoProviderServer>(port);
            var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };
            DicomAssociation capturedAssociation = null;
            server.OnAssociationRequest(association =>
            {
                capturedAssociation = association;
                return Task.FromResult(true);
            });

            // DicomClientFactory cares about the length of AETitles,
            // but in case some developer registers a custom Factory or creates DicomClient directly for some other reason.
            var client = new DicomClient("localhost", port, null, "STORAGECOMMITTEST", "DE__257a276f6d47",
                new DicomClientOptions { }, new DicomServiceOptions { },
                Setup.ServiceProvider.GetRequiredService<ILoggerFactory>(),
                Setup.ServiceProvider.GetRequiredService<IAdvancedDicomClientConnectionFactory>());
            client.Logger = _logger.IncludePrefix(nameof(DicomClient));

            // Act
            await client.AddRequestAsync(request);
            var task = client.SendAsync();
            var winning = await Task.WhenAny(task, Task.Delay(TimeSpan.FromMinutes(1)));

            // Assert
            // Ensure that the request was handled
            if (winning != task)
            {
                throw new TimeoutException();
            }
            Assert.Equal(1, counter);
            // Ensure that the calling AE was automatically truncated to 16 characters
            Assert.NotNull(capturedAssociation);
            Assert.Equal("STORAGECOMMITTES", capturedAssociation.CallingAE);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task SendAsync_MultipleRequests_AllRecognized(int expected)
        {
            int port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(expected, 1);

                var requests = Enumerable.Range(0, expected)
                    .Select(i => new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref actual) });

                await client.AddRequestsAsync(requests);

                await client.SendAsync();

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(20)]
        [InlineData(100)]
        public async Task SendAsync_MultipleTimes_AllRecognized(int expected)
        {
            var port = Ports.GetNext();
            var flag = new ManualResetEventSlim();

            using var server = CreateServer<DicomCEchoProvider>(port);
            while (!server.IsListening)
            {
                await Task.Delay(50);
            }

            var actual = 0;

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            for (var i = 0; i < expected; i++)
            {
                await client.AddRequestAsync(
                    new DicomCEchoRequest
                    {
                        OnResponseReceived = (req, res) =>
                        {
                            Interlocked.Increment(ref actual);
                            if (actual == expected)
                            {
                                flag.Set();
                            }
                        }
                    });

                await client.SendAsync();
            }

            flag.Wait(10000);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(200)]
        public async Task SendAsync_MultipleTimesParallel_AllRecognized(int expected)
        {
            int port = Ports.GetNext();

            using var server = CreateServer<DicomCEchoProvider>(port);

            await Task.Delay(500);
            Assert.True(server.IsListening, "Server is not listening");

            var actual = 0;

            var requests = Enumerable.Range(0, expected).Select(
                async requestIndex =>
                {
                    var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                    client.Logger = _logger.IncludePrefix($"{nameof(DicomClient)} #{requestIndex}");
                    await client.AddRequestAsync(
                        new DicomCEchoRequest
                        {
                            OnResponseReceived = (req, res) =>
                            {
                                _testOutputHelper.WriteLine("Response #{0}", requestIndex);
                                Interlocked.Increment(ref actual);
                            }
                        });

                    _testOutputHelper.WriteLine("Sending #{0}", requestIndex);
                    await client.SendAsync();
                    _testOutputHelper.WriteLine("Sent (or timed out) #{0}", requestIndex);
                }).ToList();
            await Task.WhenAll(requests);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AssociationAccepted_SuccessfulSendAsync_IsInvoked()
        {
            var port = Ports.GetNext();
            using (CreateServer<MockCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var accepted = false;
                client.AssociationAccepted += (sender, args) => accepted = true;

                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();

                Assert.True(accepted);
            }
        }

        [Fact]
        public async Task AssociationRejected_AssociationNotAllowed_IsInvoked()
        {
            var port = Ports.GetNext();
            using (CreateServer<MockCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "NOTACCEPTEDSCP");

                var reason = DicomRejectReason.NoReasonGiven;
                client.AssociationRejected += (sender, args) => reason = args.Reason;

                await client.AddRequestAsync(new DicomCEchoRequest());
                var exception = await Record.ExceptionAsync(() => client.SendAsync());

                Assert.Equal(DicomRejectReason.CalledAENotRecognized, reason);
                Assert.NotNull(exception);
            }
        }

        [Fact]
        public async Task AssociationReleased_SuccessfulSendAsync_IsInvoked()
        {
            var port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var released = false;
                var handle = new ManualResetEventSlim();
                client.AssociationReleased += (sender, args) =>
                {
                    released = true;
                    handle.Set();
                };

                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();

                handle.Wait(1000);
                Assert.True(released);
            }
        }

        [Fact]
        public async Task SendAsync_RecordAssociationData_AssociationContainsHostAndPort()
        {
            int port = Ports.GetNext();
            using (CreateServer<MockCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();

                Assert.NotNull(_remoteHost);
                Assert.True(_remotePort > 0);
                Assert.NotEqual(port, _remotePort);
            }
        }

        [Fact]
        public async Task SendAsync_RecordAssociationData_AssociationContainsExtendedNegotiation()
        {
            int port = Ports.GetNext();
            using (CreateServer<MockCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                var requestedNegotiation = new DicomExtendedNegotiation(
                    DicomUID.Verification,
                    new DicomServiceApplicationInfo(new byte[] { 1, 1, 1 }));
                DicomExtendedNegotiationCollection acceptedNegotiations = null;

                client.AdditionalExtendedNegotiations.Add(requestedNegotiation);
                client.AssociationAccepted += (sender, args) => acceptedNegotiations = args.Association.ExtendedNegotiations;

                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();

                Assert.NotNull(acceptedNegotiations);
                Assert.NotEmpty(acceptedNegotiations);
                var acceptedNegotiation = acceptedNegotiations.First();
                Assert.Equal(requestedNegotiation.SopClassUid, acceptedNegotiation.SopClassUid);
                Assert.Equal(requestedNegotiation.RequestedApplicationInfo.GetValues(), acceptedNegotiation.AcceptedApplicationInfo.GetValues());
            }
        }

        [Fact]
        public async Task SendAsync_RejectedAssociation_ShouldYieldException()
        {
            var port = Ports.GetNext();
            using (CreateServer<MockCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "INVALID");
                await client.AddRequestAsync(new DicomCEchoRequest());
                var exception = await Record.ExceptionAsync(() => client.SendAsync());
                Assert.IsType<DicomAssociationRejectedException>(exception);
            }
        }

        [Fact(Skip = "Requires external C-ECHO SCP")]
        public async Task SendAsync_EchoRequestToExternalServer_ShouldSucceed()
        {
            var result = false;
            var awaiter = new ManualResetEventSlim();

            var client = CreateClient("127.0.0.1", 11112, false, "SCU", "ANY-SCP");
            var req = new DicomCEchoRequest();
            req.OnResponseReceived = (rq, rsp) =>
            {
                if (rsp.Status == DicomStatus.Success)
                {
                    result = true;
                }

                awaiter.Set();
            };
            await client.AddRequestAsync(req);

            try
            {
                await client.SendAsync();
                awaiter.Wait();
            }
            catch (Exception ex)
            {
                result = false;
                client.Logger.LogError("Send failed, exception: {0}", ex);
                awaiter.Set();
            }

            Assert.True(result);
        }

        [Fact]
        public async Task IsSendRequired_AddedRequestNotConnected_ReturnsTrue()
        {
            var port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(new DicomCEchoRequest());
                Assert.True(client.IsSendRequired);
                await client.SendAsync();
                await Task.Delay(100);

                await client.AddRequestAsync(new DicomCEchoRequest());

                Assert.True(client.IsSendRequired);
            }
        }

        [Fact]
        public async Task IsSendRequired_NoRequestNotConnected_ReturnsFalse()
        {
            var port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(new DicomCEchoRequest { OnResponseReceived = (req, res) => Thread.Sleep(100) });
                await client.SendAsync();

                Assert.False(client.IsSendRequired);
            }
        }

        [Fact]
        public async Task IsSendRequired_AddedRequestIsConnected_ReturnsFalse()
        {
            var port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var flag = new ManualResetEventSlim();

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.ClientOptions.AssociationLingerTimeoutInMs = 100;

                await client.AddRequestAsync(new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) });

                var sendTask = client.SendAsync();

                await client.AddRequestAsync(
                    new DicomCEchoRequest
                    {
                        OnResponseReceived = (req, res) =>
                        {
                            Interlocked.Increment(ref counter);
                            flag.Set();
                        }
                    });
                Assert.False(client.IsSendRequired);

                flag.Wait(1000);
                Assert.Equal(2, counter);
                await sendTask;
            }
        }

        [Fact]
        public async Task SendAsync_ToExplicitOnlyProvider_NotAccepted()
        {
            var port = Ports.GetNext();
            using (CreateServer<ExplicitLECStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(TestData.Resolve("CR-MONO1-10-chest"));

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(request);

                var exception = await Record.ExceptionAsync(() => client.SendAsync());

                Assert.IsType<DicomAssociationRejectedException>(exception);
            }
        }

        [Theory]
        [InlineData(200)]
        public async Task SendAsync_Plus128CStoreRequestsCompressedTransferSyntax_NoOverflowContextIdsAllRequestsRecognized(int expected)
        {
            var port = Ports.GetNext();
            using (CreateServer<SimpleCStoreProvider>(port))
            {
                var actual = 0;

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(expected, 1);

                var requests = Enumerable.Range(0, expected)
                    .Select(i => new DicomCStoreRequest(TestData.Resolve("CT1_J2KI"))
                    {
                        OnResponseReceived = (req, res) => Interlocked.Increment(ref actual)
                    });

                await client.AddRequestsAsync(requests);

                var exception = await Record.ExceptionAsync(() => client.SendAsync());

                Assert.Null(exception);
                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_BeforeSendAsync_ShouldNeverConnect(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);
            var cancellationTokenSource = new CancellationTokenSource();
            var numberOfRequestsSent = 5;
            var numberOfResponsesReceived = 0;

            var requests = Enumerable.Range(0, numberOfRequestsSent)
                .Select(i => new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => { Interlocked.Increment(ref numberOfResponsesReceived); }
                });

            await client.AddRequestsAsync(requests);

            bool connected = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                }
            };

            cancellationTokenSource.Cancel();

            await client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            cancellationTokenSource.Dispose();

            Assert.Equal(0, numberOfResponsesReceived);
            Assert.Empty(server.Providers);
            Assert.False(connected);
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_AfterConnect_BeforeAssociation_ShouldNeverAssociate(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);
            var cancellationTokenSource = new CancellationTokenSource();
            var numberOfRequestsSent = 5;
            var numberOfResponsesReceived = 0;
            for (var i = 0; i < numberOfRequestsSent; ++i)
            {
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => { Interlocked.Increment(ref numberOfResponsesReceived); }
                });
            }

            bool connected = false, associated = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                    cancellationTokenSource.Cancel();
                }

                if (args.NewState is DicomClientWithAssociationState)
                {
                    associated = true;
                }
            };

            await client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            cancellationTokenSource.Dispose();

            Assert.True(connected);
            Assert.False(associated);
            Assert.Empty(server.Providers.SelectMany(p => p.Associations));
            Assert.Empty(server.Providers.SelectMany(p => p.Requests));
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_AfterAssociation_BeforeSendAsync_ShouldNeverSend(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);
            var cancellationTokenSource = new CancellationTokenSource();

            var numberOfRequestsToSend = 5;
            var numberOfResponsesReceived = 0;
            for (var i = 0; i < numberOfRequestsToSend; ++i)
            {
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => { Interlocked.Increment(ref numberOfResponsesReceived); }
                });
            }

            bool connected = false, associated = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                }

                if (args.NewState is DicomClientWithAssociationState)
                {
                    associated = true;
                    cancellationTokenSource.Cancel();
                }
            };

            await client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            cancellationTokenSource.Dispose();

            Assert.True(connected);
            Assert.True(associated);
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
            Assert.Empty(server.Providers.SelectMany(p => p.Requests));
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_DuringSendAsync_ShouldStopSending(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);
            server.SetResponseTimeout(TimeSpan.FromSeconds(1));

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);

            var cancellationTokenSource = new CancellationTokenSource();

            var sentRequests = new ConcurrentBag<DicomCEchoRequest>();
            var numberOfResponsesReceived = 0;
            server.OnCEchoRequest(async request =>
            {
                sentRequests.Add(request);
                if (sentRequests.Count >= 2)
                {
                    cancellationTokenSource.Cancel();

                    await client.AddRequestAsync(new DicomCEchoRequest
                    {
                        OnResponseReceived = (req, res) => Interlocked.Increment(ref numberOfResponsesReceived)
                    });
                }
            });

            var numberOfRequestsToSend = 5;
            client.NegotiateAsyncOps(1, 1);
            for (var i = 0; i < numberOfRequestsToSend; ++i)
            {
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => Interlocked.Increment(ref numberOfResponsesReceived)
                });
            }

            bool connected = false, associated = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                }

                if (args.NewState is DicomClientWithAssociationState)
                {
                    associated = true;
                }
            };

            await client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            cancellationTokenSource.Dispose();

            Assert.True(connected);
            Assert.True(associated);
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
            Assert.InRange(sentRequests.Count, 0, 5);
        }

        [Fact]
        public async Task CancelImmediatelyRelease_DuringSendAsync_ShouldStopSendingAndImmediatelyRelease()
        {
            var cancellationMode = DicomClientCancellationMode.ImmediatelyReleaseAssociation;
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);
            server.SetResponseTimeout(TimeSpan.FromMilliseconds(100));

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);

            var cancellationTokenSource = new CancellationTokenSource();

            var sentRequests = new ConcurrentBag<DicomCEchoRequest>();
            server.OnCEchoRequest(request =>
            {
                sentRequests.Add(request);
                if (sentRequests.Count >= 2)
                {
                    cancellationTokenSource.Cancel();
                }

                return Task.FromResult(0);
            });

            var numberOfRequestsToSend = 5;
            var numberOfResponsesReceived = 0;
            client.NegotiateAsyncOps(1, 1);
            for (var i = 0; i < numberOfRequestsToSend; ++i)
            {
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => Interlocked.Increment(ref numberOfResponsesReceived)
                });
            }

            bool connected = false, associated = false, aborted = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                }

                if (args.NewState is DicomClientWithAssociationState)
                {
                    associated = true;
                }

                if (args.NewState is DicomClientAbortState)
                {
                    aborted = true;
                }
            };

            await client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            cancellationTokenSource.Dispose();

            Assert.True(connected);
            Assert.True(associated);
            Assert.False(aborted);
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
            Assert.True(numberOfResponsesReceived < numberOfRequestsToSend);
        }

        [Fact]
        public async Task CancelImmediatelyAbort_DuringSendAsync_ShouldStopSendingAndImmediatelyAbort()
        {
            var cancellationMode = DicomClientCancellationMode.ImmediatelyAbortAssociation;
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            server.SetResponseTimeout(TimeSpan.FromMilliseconds(100));

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);

            var cancellationTokenSource = new CancellationTokenSource();

            var sentRequests = new ConcurrentBag<DicomCEchoRequest>();
            server.OnCEchoRequest(request =>
            {
                sentRequests.Add(request);
                if (sentRequests.Count >= 2)
                {
                    cancellationTokenSource.Cancel();
                }

                return Task.FromResult(0);
            });

            var numberOfRequestsToSend = 5;
            var numberOfResponsesReceived = 0;
            for (var i = 0; i < numberOfRequestsToSend; ++i)
            {
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => Interlocked.Increment(ref numberOfResponsesReceived)
                });
            }

            bool connected = false, associated = false, aborted = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                }

                if (args.NewState is DicomClientWithAssociationState)
                {
                    associated = true;
                }

                if (args.NewState is DicomClientAbortState)
                {
                    aborted = true;
                }
            };

            await client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            cancellationTokenSource.Dispose();

            Assert.True(connected);
            Assert.True(associated);
            Assert.True(aborted);
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
            Assert.True(numberOfResponsesReceived < numberOfRequestsToSend);
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_DuringLinger_ShouldStopLingering(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ClientOptions.AssociationLingerTimeoutInMs = 10000;
            client.NegotiateAsyncOps(1, 1);
            var cancellationTokenSource = new CancellationTokenSource();

            var numberOfRequestsToSend = 5;
            var numberOfResponsesReceived = 0;
            client.NegotiateAsyncOps(1, 1);
            for (var i = 0; i < numberOfRequestsToSend; ++i)
            {
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => Interlocked.Increment(ref numberOfResponsesReceived)
                });
            }

            bool connected = false, associated = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                }

                if (args.NewState is DicomClientWithAssociationState)
                {
                    associated = true;
                }

                if (args.NewState is DicomClientLingeringState)
                {
                    cancellationTokenSource.Cancel();
                }
            };

            var timeoutCancellation = new CancellationTokenSource();
            var timeout = Task.Delay(5000, timeoutCancellation.Token);
            var sendTask = client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            var winner = await Task.WhenAny(sendTask, timeout);

            cancellationTokenSource.Dispose();
            timeoutCancellation.Dispose();

            Assert.Equal(winner, sendTask);
            Assert.True(connected);
            Assert.True(associated);
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_DuringAssociationRelease_ShouldNotLinger(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);
            var cancellationTokenSource = new CancellationTokenSource();
            var numberOfRequestsSent = 5;
            var numberOfResponsesReceived = 0;
            client.NegotiateAsyncOps(1, 1);
            for (var i = 0; i < numberOfRequestsSent; ++i)
            {
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => { Interlocked.Increment(ref numberOfResponsesReceived); }
                });
            }

            bool connected = false, associated = false;
            client.StateChanged += (sender, args) =>
            {
                if (args.NewState is DicomClientWithConnectionState)
                {
                    connected = true;
                }

                if (args.NewState is DicomClientWithAssociationState)
                {
                    associated = true;
                }

                if (args.NewState is DicomClientReleaseAssociationState)
                {
                    cancellationTokenSource.Cancel();
                }
            };

            await client.SendAsync(cancellationTokenSource.Token, cancellationMode);

            cancellationTokenSource.Dispose();

            Assert.True(connected);
            Assert.True(associated);
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
            Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
            Assert.Equal(5, numberOfResponsesReceived);
        }

        private void AllResponsesShouldHaveSucceeded(IEnumerable<DicomCEchoResponse> responses)
        {
            var logger = _logger.IncludePrefix("Responses");
            foreach (var r in responses)
            {
                logger.LogInformation($"{r.Type} [{r.RequestMessageID}]: " +
                            $"Status = {r.Status.State}, " +
                            $"Code = {r.Status.Code}, " +
                            $"ErrorComment = {r.Status.ErrorComment}, " +
                            $"Description = {r.Status.Description}");

                Assert.Equal(DicomState.Success, r.Status.State);
            }
        }

        [Theory(Skip = "These time based tests are troublesome in CI with varying degrees of host performance")]
        [InlineData( /*number of requests:*/ 6, /* seconds between each request: */ 1, /* linger: */ 5)]
        public async Task SendAsync_Linger_ShouldLingerLongEnoughToReuseAssociation(int numberOfRequests, int secondsBetweenEachRequest, int lingerTimeoutInSeconds)
        {
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();
            var expectedNumberOfAssociations = 1;

            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ClientOptions.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

            logger.LogInformation($"Beginning {numberOfRequests} parallel requests with {secondsBetweenEachRequest}s between each request");

            var responses = new ConcurrentBag<DicomCEchoResponse>();
            var sendTasks = new List<Task>();

            for (var i = 1; i <= numberOfRequests; i++)
            {
                var request = new DicomCEchoRequest
                {
                    OnResponseReceived = (req, res) => responses.Add(res)
                };
                await client.AddRequestAsync(request);

                if (client.IsSendRequired)
                {
                    // Do not await here, because this task will only complete after the client has completely processed the request
                    sendTasks.Add(client.SendAsync());
                }

                logger.LogInformation($"Waiting {secondsBetweenEachRequest} seconds between requests");
                await Task.Delay(TimeSpan.FromSeconds(secondsBetweenEachRequest));
            }

            await Task.WhenAll(sendTasks);

            AllResponsesShouldHaveSucceeded(responses);

            Assert.Equal(numberOfRequests, responses.Count);

            var associations = server.Providers.SelectMany(p => p.Associations).ToList();

            Assert.Equal(expectedNumberOfAssociations, associations.Count);

            var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

            Assert.Equal(numberOfRequests, receivedRequests.Count);
        }

        [Theory(Skip = "These time based tests are troublesome in CI with varying degrees of host performance")]
        [InlineData( /*number of requests:*/ 6, /* seconds between each request: */ 1, /* linger: */ 10)]
        public async Task SendAsync_Linger_ShouldKeepDelayingLingerAsLongAsRequestsAreComingIn(int numberOfRequests, int secondsBetweenEachRequest, int lingerTimeoutInSeconds)
        {
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();
            var expectedNumberOfAssociations = 1;
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ClientOptions.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

            logger.LogInformation($"Beginning {numberOfRequests} parallel requests with {secondsBetweenEachRequest}s between each request");

            var responses = new ConcurrentBag<DicomCEchoResponse>();

            var sendTasks = new List<Task>();

            for (var i = 1; i <= numberOfRequests; i++)
            {
                var request = new DicomCEchoRequest
                {
                    OnResponseReceived = (req, res) => responses.Add(res)
                };
                await client.AddRequestAsync(request);

                if (client.IsSendRequired)
                {
                    // Do not await here, because this task will only complete after the client has completely processed the request
                    sendTasks.Add(client.SendAsync());
                }

                logger.LogInformation($"Waiting {secondsBetweenEachRequest} seconds between requests");
                await Task.Delay(TimeSpan.FromSeconds(secondsBetweenEachRequest));
            }
            await Task.WhenAll(sendTasks);

            AllResponsesShouldHaveSucceeded(responses);

            Assert.Equal(numberOfRequests, responses.Count);

            var associations = server.Providers.SelectMany(p => p.Associations).ToList();

            Assert.Equal(expectedNumberOfAssociations, associations.Count);

            var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

            Assert.Equal(numberOfRequests, receivedRequests.Count);
        }

        [Theory(Skip = "These time based tests are troublesome in CI with varying degrees of host performance")]
        [InlineData( /*number of requests:*/ 2, /* seconds between each request: */ 2, /* linger: */ 1)]
        [InlineData( /*number of requests:*/ 2, /* seconds between each request: */ 3, /* linger: */ 2)]
        public async Task SendAsync_Linger_ShouldAutomaticallyOpenNewAssociationAfterLingerTime(int numberOfRequests, int secondsBetweenEachRequest, int lingerTimeoutInSeconds)
        {
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();
            // Each request should have its own association
            var expectedNumberOfAssociations = 2;
            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ClientOptions.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

            logger.LogInformation($"Beginning {numberOfRequests} parallel requests with {secondsBetweenEachRequest}s between each request");

            var responses = new ConcurrentBag<DicomCEchoResponse>();

            var sendTasks = new List<Task>();

            for (var i = 1; i <= numberOfRequests; i++)
            {
                var request = new DicomCEchoRequest
                {
                    OnResponseReceived = (req, res) => responses.Add(res)
                };
                await client.AddRequestAsync(request);

                if (client.IsSendRequired)
                {
                    // Do not await here, because this task will only complete after the client has completely processed the request
                    sendTasks.Add(client.SendAsync());
                }

                logger.LogInformation($"Waiting {secondsBetweenEachRequest} seconds between requests");
                await Task.Delay(TimeSpan.FromSeconds(secondsBetweenEachRequest));
            }
            await Task.WhenAll(sendTasks);

            AllResponsesShouldHaveSucceeded(responses);

            Assert.Equal(numberOfRequests, responses.Count);

            var associations = server.Providers.SelectMany(p => p.Associations).ToList();

            Assert.Equal(expectedNumberOfAssociations, associations.Count);

            var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

            Assert.Equal(numberOfRequests, receivedRequests.Count);
        }

        [Fact(Skip = "These time based tests are troublesome in CI with varying degrees of host performance")]
        public async Task SendAsync_Linger_ShouldAutomaticallyOpenNewAssociationAfterLingerTimeAfterLastRequest()
        {
            var numberOfRequests = 5;
            var lingerTimeoutInSeconds = 5;
            var secondsBetweenEachRequest = new[] { 1, 1, 1, 6, 1, 1 };
            var expectedNumberOfAssociations = 2;
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();

            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            server.SetResponseTimeout(TimeSpan.FromTicks(0));

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ClientOptions.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

            logger.LogInformation($"Beginning {numberOfRequests} parallel requests with variable wait times between each request");

            var responses = new ConcurrentBag<DicomCEchoResponse>();

            var sendTasks = new List<Task>();

            for (var i = 1; i <= numberOfRequests; i++)
            {
                var request = new DicomCEchoRequest
                {
                    OnResponseReceived = (req, res) => responses.Add(res)
                };
                logger.LogInformation($"Adding request {i}");
                await client.AddRequestAsync(request);

                if (client.IsSendRequired)
                {
                    // Do not await here, because this task will only complete after the client has completely processed the request
                    sendTasks.Add(Task.Run(() => client.SendAsync()));
                }

                var secondsToWait = secondsBetweenEachRequest[i];
                logger.LogInformation($"Waiting {secondsToWait} seconds between requests");
                await Task.Delay(TimeSpan.FromSeconds(secondsToWait));
            }
            await Task.WhenAll(sendTasks);

            AllResponsesShouldHaveSucceeded(responses);

            Assert.Equal(numberOfRequests, responses.Count);

            var associations = server.Providers.SelectMany(p => p.Associations).ToList();

            Assert.Equal(expectedNumberOfAssociations, associations.Count);

            var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

            Assert.Equal(numberOfRequests, receivedRequests.Count);
        }

        [Fact]
        public async Task OnCStoreRequest_AfterCGet_ShouldTrigger()
        {
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();

            using (CreateServer<RecordingDicomCGetProvider, RecordingDicomCGetProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                DicomCStoreRequest capturedCStoreRequest = null;

                client.OnCStoreRequest = async request =>
                {
                    logger.LogInformation("Handling C-Store request");
                    capturedCStoreRequest = request;
                    await Task.Delay(50);
                    return new DicomCStoreResponse(request, DicomStatus.Success);
                };

                var studyInstanceUID = "999.999.3859744";
                var seriesInstanceUID = "999.999.94827453";
                var sopInstanceUID = "999.999.133.1996.1.1800.1.6.21";

                logger.LogInformation("Sending C-Get request");
                await client.AddRequestAsync(new DicomCGetRequest(studyInstanceUID, seriesInstanceUID, sopInstanceUID));

                var pcs = DicomPresentationContext.GetScpRolePresentationContextsFromStorageUids(
                    DicomStorageCategory.Image,
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    DicomTransferSyntax.ImplicitVRLittleEndian,
                    DicomTransferSyntax.ImplicitVRBigEndian);
                client.AdditionalPresentationContexts.AddRange(pcs);

                await client.SendAsync();

                Assert.NotNull(capturedCStoreRequest);
                Assert.Equal(DicomUID.Parse(sopInstanceUID).ToString(), capturedCStoreRequest.SOPInstanceUID.ToString());
            }
        }

        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(10, 2, 5)]
        [InlineData(10, 10, 1)]
        [InlineData(100, 10, 10)]
        public async Task SendAsync_MaxRequestsPerAssoc_ShouldAlwaysCreateCorrectNumberOfAssociations(int numberOfRequests, int maxRequestsPerAssoc, int expectedNumberOfAssociations)
        {
            var port = Ports.GetNext();
            var logger = _logger.IncludePrefix("UnitTest");

            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ClientOptions.MaximumNumberOfRequestsPerAssociation = maxRequestsPerAssoc;
            client.NegotiateAsyncOps(10, 10);

            logger.LogInformation($"Beginning {numberOfRequests} requests with max {maxRequestsPerAssoc} requests / association");

            var responses = new ConcurrentBag<DicomCEchoResponse>();
            for (var i = 1; i <= numberOfRequests; i++)
            {
                var iLocal = i;
                var dicomCEchoRequest = new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) =>
                    {
                        logger.LogDebug($"Request completed: {iLocal}");
                        responses.Add(response);
                    }
                };
                await client.AddRequestAsync(dicomCEchoRequest);
            }

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
            {
                await client.SendAsync(cts.Token);
            }

            AllResponsesShouldHaveSucceeded(responses);

            Assert.Equal(numberOfRequests, responses.Count);

            Assert.Equal(expectedNumberOfAssociations, server.Providers.Count());

            foreach (var provider in server.Providers)
            {
                Assert.InRange(provider.Requests.Count(), 0, maxRequestsPerAssoc);
            }
        }

        [Fact]
        public async Task SendAsync_ToUnknownHost_ShouldNotLoopInfinitely()
        {
            var request = new DicomCEchoRequest();

            var client = CreateClient("www.google.com", 4333, false, "SCU", "ANY-SCP");
            await client.AddRequestAsync(request);

            Exception capturedException = null;
            try
            {
                await client.SendAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError("Error occurred during send {e}", exception);
                capturedException = exception;
            }
            Assert.NotNull(capturedException);
        }

        [Fact]
        public async Task SendAsync_ToDisposedDicomServer_ShouldNotLoopInfinitely()
        {
            var port = Ports.GetNext();
            var logger = _logger.IncludePrefix("UnitTest");

            RecordingDicomCEchoProviderServer server = null;
            DicomCEchoResponse echoResponse1 = null, echoResponse2 = null, echoResponse3 = null;
            try
            {
                server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port);

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                // Ensure requests are handled sequentially
                client.NegotiateAsyncOps(1, 1);

                var echoRequest1 = new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) =>
                    {
                        logger.LogInformation("Received echo response 1, disposing server");
                        echoResponse1 = response;
                        // ReSharper disable once AccessToDisposedClosure This is an edge case we are trying to test
                        server?.Dispose();
                        logger.LogInformation("Server is disposed");
                    }
                };
                var echoRequest2 = new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) =>
                    {
                        logger.LogInformation("Received echo response 2");
                        echoResponse2 = response;
                    }
                };
                var echoRequest3 = new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) =>
                    {
                        logger.LogInformation("Received echo response 3");
                        echoResponse3 = response;
                    }
                };

                await client.AddRequestsAsync(new[] { echoRequest1, echoRequest2, echoRequest3 });

                using var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(10));

                try
                {
                    await client.SendAsync(cancellation.Token, DicomClientCancellationMode.ImmediatelyAbortAssociation);
                }
                catch (Exception)
                {
                    /* Ignore */
                }

                Assert.False(cancellation.IsCancellationRequested);
            }
            finally
            {
                server?.Dispose();
            }

            Assert.NotNull(echoResponse1);
            Assert.Null(echoResponse2);
            Assert.Null(echoResponse3);
        }

        [Fact]
        public async Task UnlimitedAsyncOpsInvokedShouldBeSupported()
        {
            var port = Ports.GetNext();
            using var server = DicomServerFactory.Create<AsyncDicomCEchoProvider>(port, logger: _logger.IncludePrefix("Server"));
            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(0,0);
            client.Logger = _logger.IncludePrefix("Client");

            var numberOfRequests = 100;
            var counter = 0;
            for (var i = 0; i < numberOfRequests; i++)
            {
                var request = new DicomCEchoRequest
                    { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };
                await client.AddRequestAsync(request);
            }

            await client.SendAsync();

            Assert.Equal(numberOfRequests, counter);
        }


        [Fact]
        public async Task SendAsync_CustomTcpBufferSizes_Works()
        {
            /*
             * This test simply verifies that setting a custom TCP buffer size does not crash
             */
            var port = Ports.GetNext();
            var bufferSize = 4 * 1024 * 1024;
            using var server = CreateServer<DicomCEchoProvider>(port);
            server.Options.TcpReceiveBufferSize = bufferSize;
            server.Options.TcpSendBufferSize = bufferSize;

            var counter = 0;
            var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };

            var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.TcpReceiveBufferSize = bufferSize;
            client.ServiceOptions.TcpSendBufferSize = bufferSize;
            await client.AddRequestAsync(request);

            await client.SendAsync();
            Assert.Equal(1, counter);
        }

        #region Support classes

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
                    Thread.Sleep(1000);
                    _remoteHost = association.RemoteHost;
                    _remotePort = association.RemotePort;
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

        /// <summary>
        /// Artificial C-STORE provider, only supporting Explicit LE transfer syntax for the purpose of
        /// testing <see cref="SendAsync_ToExplicitOnlyProvider_NotAccepted"/>.
        /// </summary>
        private class ExplicitLECStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
        {
            private static readonly DicomTransferSyntax[] _acceptedTransferSyntaxes =
            {
                DicomTransferSyntax.ExplicitVRLittleEndian
            };

            public ExplicitLECStoreProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
                DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    if (!pc.AcceptTransferSyntaxes(_acceptedTransferSyntaxes))
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
                TimeSpan? responseTimeout, DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
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
            private readonly ConcurrentBag<RecordingDicomCEchoProvider> _providers;
            private Func<DicomCEchoRequest, Task> _onRequest;
            private TimeSpan? _responseTimeout;
            private readonly DicomServiceDependencies _dicomServiceDependencies;

            public IEnumerable<RecordingDicomCEchoProvider> Providers => _providers;

            public RecordingDicomCEchoProviderServer(DicomServerDependencies dicomServerDependencies,
                DicomServiceDependencies dicomServiceDependencies) : base(dicomServerDependencies)
            {
                _dicomServiceDependencies = dicomServiceDependencies ?? throw new ArgumentNullException(nameof(dicomServiceDependencies));
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
                var provider = new RecordingDicomCEchoProvider(stream, Encoding.UTF8, Logger, _onRequest, _responseTimeout, _dicomServiceDependencies);
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
                DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
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
                    Logger.LogError("Could not send file via C-Store request: {error}", e);
                    result = new DicomCGetResponse(request, DicomStatus.ProcessingFailure);
                }

                yield return result;
            }

        }


        public class RecordingDicomCGetProviderServer : DicomServer<RecordingDicomCGetProvider>
        {
            private readonly DicomServiceDependencies _dicomServiceDependencies;
            private readonly ConcurrentBag<RecordingDicomCGetProvider> _providers;

            public IEnumerable<RecordingDicomCGetProvider> Providers => _providers;

            public RecordingDicomCGetProviderServer(
                DicomServerDependencies dicomServerDependencies,
                DicomServiceDependencies dicomServiceDependencies) : base(dicomServerDependencies)
            {
                _dicomServiceDependencies = dicomServiceDependencies ?? throw new ArgumentNullException(nameof(dicomServiceDependencies));
                _providers = new ConcurrentBag<RecordingDicomCGetProvider>();
            }

            protected sealed override RecordingDicomCGetProvider CreateScp(INetworkStream stream)
            {
                var provider = new RecordingDicomCGetProvider(stream, Encoding.UTF8, Logger, _dicomServiceDependencies);
                _providers.Add(provider);
                return provider;
            }
        }

        #endregion

    }
}
