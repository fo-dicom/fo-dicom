// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dicom.Helpers;
using Dicom.Log;
using Dicom.Network.Client.States;
using Xunit;
using Xunit.Abstractions;

namespace Dicom.Network.Client
{
    [Collection("Network"), Trait("Category", "Network")]
    public class DicomClientTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly XUnitDicomLogger _logger;

        #region Fields

        private static string remoteHost;

        private static int remotePort;

        #endregion

        #region Constructors

        public DicomClientTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
            remoteHost = null;
            remotePort = 0;
        }

        #endregion

        #region Helper functions

        private IDicomServer CreateServer<T>(int port) where T : DicomService, IDicomServiceProvider
        {
            var server = DicomServer.Create<T>(port);
            server.Logger = _logger.IncludePrefix(nameof(IDicomServer));
            return server;
        }

        private TServer CreateServer<TProvider, TServer>(int port)
            where TProvider : DicomService, IDicomServiceProvider
            where TServer : class, IDicomServer<TProvider>, new()
        {
            var logger = _logger.IncludePrefix(nameof(IDicomServer));
            var options = new DicomServiceOptions
            {
                LogDimseDatasets = false,
                LogDataPDUs = false,
            };
            var ipAddress = NetworkManager.IPv4Any;
            var server = DicomServer.Create<TProvider, TServer>(ipAddress, port, logger: logger, options: options);
            return server as TServer;
        }

        private DicomClient CreateClient(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            var client = new DicomClient(host, port, useTls, callingAe, calledAe)
            {
                Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
            };
            return client;
        }

        #endregion

        [Fact]
        public async Task SendAsync_MultipleRequestsWhileAlreadySending_ReusesSameAssociation()
        {
            int port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var counter = 0;

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(1, 1);
                for (var i = 0; i < 5; i++)
                {
                    var request = new DicomCEchoRequest {OnResponseReceived = (req, res) => Interlocked.Increment(ref counter)};
                    await client.AddRequestAsync(request).ConfigureAwait(false);
                }

                bool moreRequestsSent = false;

                client.StateChanged += async (sender, args) =>
                {
                    if (moreRequestsSent)
                        return;
                    if (args.NewState is DicomClientSendingRequestsState)
                    {
                        while (!client.QueuedRequests.IsEmpty)
                        {
                            await Task.Delay(10).ConfigureAwait(false);
                        }
                    }

                    for (var i = 0; i < 5; i++)
                    {
                        var request = new DicomCEchoRequest {OnResponseReceived = (req, res) => Interlocked.Increment(ref counter)};
                        await client.AddRequestAsync(request).ConfigureAwait(false);
                    }

                    moreRequestsSent = true;
                };

                await client.SendAsync().ConfigureAwait(false);

                Assert.Equal(10, counter);
                Assert.Single(server.Providers.SelectMany(p => p.Associations));
            }
        }

        [Fact]
        public async Task SendAsync_SingleRequest_Recognized()
        {
            int port = Ports.GetNext();
            using (CreateServer<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest {OnResponseReceived = (req, res) => Interlocked.Increment(ref counter)};

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(request).ConfigureAwait(false);

                var task = client.SendAsync();
                //await Task.WhenAny(task, Task.Delay(10000));
                await task.ConfigureAwait(false);
                Assert.Equal(1, counter);
            }
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
                    .Select(i => new DicomCEchoRequest {OnResponseReceived = (req, res) => Interlocked.Increment(ref actual)});

                await client.AddRequestsAsync(requests).ConfigureAwait(false);

                await client.SendAsync().ConfigureAwait(false);

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

            using (var server = CreateServer<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) await Task.Delay(50);

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
                                if (actual == expected) flag.Set();
                            }
                        }).ConfigureAwait(false);
                    await client.SendAsync().ConfigureAwait(false);
                }

                flag.Wait(10000);
                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(20)]
        [InlineData(200)]
        public async Task SendAsync_MultipleTimesParallel_AllRecognized(int expected)
        {
            int port = Ports.GetNext();

            using (
                var server = CreateServer<DicomCEchoProvider>(port))
            {
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
                            }).ConfigureAwait(false);

                        _testOutputHelper.WriteLine("Sending #{0}", requestIndex);
                        await client.SendAsync().ConfigureAwait(false);
                        _testOutputHelper.WriteLine("Sent (or timed out) #{0}", requestIndex);
                    }).ToList();
                await Task.WhenAll(requests).ConfigureAwait(false);

                Assert.Equal(expected, actual);
            }
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

                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

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

                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
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

                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

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
                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(remoteHost);
                Assert.True(remotePort > 0);
                Assert.NotEqual(port, remotePort);
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
                    new DicomServiceApplicationInfo(new byte[] {1, 1, 1}));
                DicomExtendedNegotiationCollection acceptedNegotiations = null;

                client.AdditionalExtendedNegotiations.Add(requestedNegotiation);
                client.AssociationAccepted += (sender, args) => acceptedNegotiations = args.Association.ExtendedNegotiations;

                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

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
                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                var exception =
                    await
                        Record.ExceptionAsync(() => client.SendAsync())
                            .ConfigureAwait(false);
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
            await client.AddRequestAsync(req).ConfigureAwait(false);

            try
            {
                await client.SendAsync().ConfigureAwait(false);
                awaiter.Wait();
            }
            catch (Exception ex)
            {
                result = false;
                client.Logger.Error("Send failed, exception: {0}", ex);
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
                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);
                Assert.True(client.IsSendRequired);
                await client.SendAsync();
                Thread.Sleep(100);

                await client.AddRequestAsync(new DicomCEchoRequest()).ConfigureAwait(false);

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
                await client.AddRequestAsync(new DicomCEchoRequest {OnResponseReceived = (req, res) => Thread.Sleep(100)}).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

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
                client.AssociationLingerTimeoutInMs = 100;
                await client.AddRequestAsync(new DicomCEchoRequest {OnResponseReceived = (req, res) => Interlocked.Increment(ref counter)})
                    .ConfigureAwait(false);

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
                await sendTask.ConfigureAwait(false);
            }
        }

        [Fact]
        public async Task SendAsync_ToExplicitOnlyProvider_NotAccepted()
        {
            var port = Ports.GetNext();
            using (CreateServer<ExplicitLECStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(@"./Test Data/CR-MONO1-10-chest");

                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(request).ConfigureAwait(false);

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
                    .Select(i => new DicomCStoreRequest(@"./Test Data/CT1_J2KI")
                    {
                        OnResponseReceived = (req, res) => Interlocked.Increment(ref actual)
                    });

                await client.AddRequestsAsync(requests).ConfigureAwait(false);

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
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
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

                await client.AddRequestsAsync(requests).ConfigureAwait(false);

                bool connected = false;
                client.StateChanged += (sender, args) =>
                {
                    if (args.NewState is DicomClientWithConnectionState)
                    {
                        connected = true;
                    }
                };

                cancellationTokenSource.Cancel();

                await client.SendAsync(cancellationTokenSource.Token, cancellationMode).ConfigureAwait(false);

                cancellationTokenSource.Dispose();

                Assert.Equal(0, numberOfResponsesReceived);
                Assert.Empty(server.Providers);
                Assert.False(connected);
            }
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_AfterConnect_BeforeAssociation_ShouldNeverAssociate(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
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
                    }).ConfigureAwait(false);
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

                await client.SendAsync(cancellationTokenSource.Token, cancellationMode).ConfigureAwait(false);

                cancellationTokenSource.Dispose();

                Assert.True(connected);
                Assert.False(associated);
                Assert.Empty(server.Providers.SelectMany(p => p.Associations));
                Assert.Empty(server.Providers.SelectMany(p => p.Requests));
            }
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_AfterAssociation_BeforeSendAsync_ShouldNeverSend(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
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
                    }).ConfigureAwait(false);
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

                await client.SendAsync(cancellationTokenSource.Token, cancellationMode).ConfigureAwait(false);

                cancellationTokenSource.Dispose();

                Assert.True(connected);
                Assert.True(associated);
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
                Assert.Empty(server.Providers.SelectMany(p => p.Requests));
            }
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_DuringSendAsync_ShouldStopSending(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
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
                            OnResponseReceived = (req, res) =>
                            {
                                Interlocked.Increment(ref numberOfResponsesReceived);
                            }
                        }).ConfigureAwait(false);
                    }
                });

                var numberOfRequestsToSend = 5;
                client.NegotiateAsyncOps(1, 1);
                for (var i = 0; i < numberOfRequestsToSend; ++i)
                {
                    await client.AddRequestAsync(new DicomCEchoRequest
                    {
                        OnResponseReceived = (request, response) =>
                        {
                            Interlocked.Increment(ref numberOfResponsesReceived);
                        }
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

                await client.SendAsync(cancellationTokenSource.Token, cancellationMode).ConfigureAwait(false);

                cancellationTokenSource.Dispose();

                Assert.True(connected);
                Assert.True(associated);
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
                Assert.InRange(sentRequests.Count, 0, 5);
            }
        }

        [Fact]
        public async Task CancelImmediatelyRelease_DuringSendAsync_ShouldStopSendingAndImmediatelyRelease()
        {
            var cancellationMode = DicomClientCancellationMode.ImmediatelyReleaseAssociation;
            var port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
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
                        OnResponseReceived = (request, response) =>
                        {
                            Interlocked.Increment(ref numberOfResponsesReceived);
                        }
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

                await client.SendAsync(cancellationTokenSource.Token, cancellationMode).ConfigureAwait(false);

                cancellationTokenSource.Dispose();

                Assert.True(connected);
                Assert.True(associated);
                Assert.False(aborted);
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
                Assert.True(numberOfResponsesReceived <= 2);
            }
        }

        [Fact]
        public async Task CancelImmediatelyAbort_DuringSendAsync_ShouldStopSendingAndImmediatelyAbort()
        {
            var cancellationMode = DicomClientCancellationMode.ImmediatelyAbortAssociation;
            var port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
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
                        OnResponseReceived = (request, response) =>
                        {
                            Interlocked.Increment(ref numberOfResponsesReceived);
                        }
                    }).ConfigureAwait(false);
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

                await client.SendAsync(cancellationTokenSource.Token, cancellationMode).ConfigureAwait(false);

                cancellationTokenSource.Dispose();

                Assert.True(connected);
                Assert.True(associated);
                Assert.True(aborted);
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
                Assert.True(numberOfResponsesReceived <= 2);
            }
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_DuringLinger_ShouldStopLingering(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(1, 1);
                client.AssociationLingerTimeoutInMs = 10000;
                var cancellationTokenSource = new CancellationTokenSource();

                var numberOfRequestsToSend = 5;
                var numberOfResponsesReceived = 0;
                client.NegotiateAsyncOps(1, 1);
                for (var i = 0; i < numberOfRequestsToSend; ++i)
                {
                    await client.AddRequestAsync(new DicomCEchoRequest
                    {
                        OnResponseReceived = (request, response) =>
                        {
                            Interlocked.Increment(ref numberOfResponsesReceived);
                        }
                    }).ConfigureAwait(false);
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
                var sendTask = client.SendAsync(cancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, timeout).ConfigureAwait(false);

                cancellationTokenSource.Dispose();
                timeoutCancellation.Dispose();

                Assert.Equal(winner, sendTask);
                Assert.True(connected);
                Assert.True(associated);
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
            }
        }

        [Theory]
        [InlineData(DicomClientCancellationMode.ImmediatelyReleaseAssociation)]
        [InlineData(DicomClientCancellationMode.ImmediatelyAbortAssociation)]
        public async Task Cancel_DuringAssociationRelease_ShouldNotLinger(DicomClientCancellationMode cancellationMode)
        {
            var port = Ports.GetNext();
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(1, 1);
                var cancellationTokenSource = new CancellationTokenSource();
                var numberOfRequestsSent = 5;
                var numberOfResponsesReceived = 0;
                client.NegotiateAsyncOps(1, 1);
                for (var i = 0; i < numberOfRequestsSent; ++i)
                {
                    var i1 = i;
                    await client.AddRequestAsync(new DicomCEchoRequest
                    {
                        OnResponseReceived = (request, response) => { Interlocked.Increment(ref numberOfResponsesReceived); }
                    }).ConfigureAwait(false);
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

                await client.SendAsync(cancellationTokenSource.Token, cancellationMode).ConfigureAwait(false);

                cancellationTokenSource.Dispose();

                Assert.True(connected);
                Assert.True(associated);
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Associations));
                Assert.NotEmpty(server.Providers.SelectMany(p => p.Requests));
                Assert.Equal(5, numberOfResponsesReceived);
            }
        }

        private async Task<DicomCEchoResponse> SendEchoRequestWithTimeout(DicomClient dicomClient, int timeoutInMilliseconds = 3000)
        {
            var request = new DicomCEchoRequest();
            var logger = _logger.IncludePrefix("C-Echo request");

            var responseCompletionSource = new TaskCompletionSource<DicomCEchoResponse>();
            var responseCancellationSource = new CancellationTokenSource(timeoutInMilliseconds);

            var cancellationRegistration = responseCancellationSource.Token.Register(() =>
            {
                logger.Error($"Request [{request.MessageID}] timed out!");
                responseCompletionSource.SetCanceled();
            });

            request.OnResponseReceived += (req, res) =>
            {
                logger.Info($"Response [{request.MessageID}] received!");
                responseCompletionSource.SetResult(res);
            };

            await dicomClient.AddRequestAsync(request).ConfigureAwait(false);

            // ReSharper disable once MethodSupportsCancellation Let's not cancel the cancellation, ha ha!
            responseCompletionSource.Task.ContinueWith(_ => cancellationRegistration.Dispose());

            return await responseCompletionSource.Task;
        }

        private void AllResponsesShouldHaveSucceeded(IEnumerable<DicomCEchoResponse> responses)
        {
            var logger = _logger.IncludePrefix("Responses");
            foreach (var r in responses)
            {
                logger.Info($"{r.Type} [{r.RequestMessageID}]: " +
                            $"Status = {r.Status.State}, " +
                            $"Code = {r.Status.Code}, " +
                            $"ErrorComment = {r.Status.ErrorComment}, " +
                            $"Description = {r.Status.Description}");

                Assert.Equal(DicomState.Success, r.Status.State);
            }
        }

        [Theory]
        [InlineData( /*number of requests:*/ 6, /* seconds between each request: */ 1, /* linger: */ 5)]
        [InlineData( /*number of requests:*/ 3, /* seconds between each request: */ 2, /* linger: */ 5)]
        public async Task SendAsync_Linger_ShouldLingerLongEnoughToReuseAssociation(int numberOfRequests, int secondsBetweenEachRequest, int lingerTimeoutInSeconds)
        {
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();
            var expectedNumberOfAssociations = 1;

            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

                logger.Info($"Beginning {numberOfRequests} parallel requests with {secondsBetweenEachRequest}s between each request");

                var requests = new List<Task<DicomCEchoResponse>>();

                var sendTasks = new List<Task>();

                for (var i = 1; i <= numberOfRequests; i++)
                {
                    var task = SendEchoRequestWithTimeout(client);
                    requests.Add(task);

                    if (client.IsSendRequired)
                    {
                        // Do not await here, because this task will only complete after the client has completely processed the request
                        sendTasks.Add(client.SendAsync());
                    }

                    if (i < numberOfRequests)
                    {
                        logger.Info($"Waiting {secondsBetweenEachRequest} seconds between requests");
                        await Task.Delay(TimeSpan.FromSeconds(secondsBetweenEachRequest)).ConfigureAwait(false);
                        logger.Info($"Waited {secondsBetweenEachRequest} seconds, moving on to next request");
                    }
                }

                var responses = await Task.WhenAll(requests).ConfigureAwait(false);

                AllResponsesShouldHaveSucceeded(responses);

                Assert.Equal(numberOfRequests, responses.Length);

                var associations = server.Providers.SelectMany(p => p.Associations).ToList();

                Assert.Equal(expectedNumberOfAssociations, associations.Count);

                var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

                Assert.Equal(numberOfRequests, receivedRequests.Count);

                // now let the DicomClient complete gracefully
                await Task.WhenAll(sendTasks).ConfigureAwait(false);
            }
        }

        [Theory]
        [InlineData( /*number of requests:*/ 6, /* seconds between each request: */ 1, /* linger: */ 5)]
        [InlineData( /*number of requests:*/ 2, /* seconds between each request: */ 4, /* linger: */ 5)]
        public async Task SendAsync_Linger_ShouldKeepDelayingLingerAsLongAsRequestsAreComingIn(int numberOfRequests, int secondsBetweenEachRequest, int lingerTimeoutInSeconds)
        {
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();
            var expectedNumberOfAssociations = 1;
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

                logger.Info($"Beginning {numberOfRequests} parallel requests with {secondsBetweenEachRequest}s between each request");

                var requests = new List<Task<DicomCEchoResponse>>();

                var sendTasks = new List<Task>();

                for (var i = 1; i <= numberOfRequests; i++)
                {
                    var task = SendEchoRequestWithTimeout(client);
                    requests.Add(task);

                    if (client.IsSendRequired)
                    {
                        // Do not await here, because this task will only complete after the client has completely processed the request
                        sendTasks.Add(client.SendAsync());
                    }

                    if (i < numberOfRequests)
                    {
                        logger.Info($"Waiting {secondsBetweenEachRequest} seconds between requests");
                        await Task.Delay(TimeSpan.FromSeconds(secondsBetweenEachRequest)).ConfigureAwait(false);
                        logger.Info($"Waited {secondsBetweenEachRequest} seconds, moving on to next request");
                    }
                }

                var responses = await Task.WhenAll(requests).ConfigureAwait(false);

                AllResponsesShouldHaveSucceeded(responses);

                Assert.Equal(numberOfRequests, responses.Length);

                var associations = server.Providers.SelectMany(p => p.Associations).ToList();

                Assert.Equal(expectedNumberOfAssociations, associations.Count);

                var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

                Assert.Equal(numberOfRequests, receivedRequests.Count);

                // now let the DicomClient complete gracefully
                await Task.WhenAll(sendTasks).ConfigureAwait(false);
            }
        }

        [Theory]
        [InlineData( /*number of requests:*/ 2, /* seconds between each request: */ 2, /* linger: */ 1)]
        [InlineData( /*number of requests:*/ 2, /* seconds between each request: */ 3, /* linger: */ 2)]
        public async Task SendAsync_Linger_ShouldAutomaticallyOpenNewAssociationAfterLingerTime(int numberOfRequests, int secondsBetweenEachRequest, int lingerTimeoutInSeconds)
        {
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();
            // Each request should have its own association
            var expectedNumberOfAssociations = 2;
            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

                logger.Info($"Beginning {numberOfRequests} parallel requests with {secondsBetweenEachRequest}s between each request");

                var requests = new List<Task<DicomCEchoResponse>>();

                var sendTasks = new List<Task>();

                for (var i = 1; i <= numberOfRequests; i++)
                {
                    var task = SendEchoRequestWithTimeout(client);
                    requests.Add(task);

                    if (client.IsSendRequired)
                    {
                        // Do not await here, because this task will only complete after the client has completely processed the request
                        sendTasks.Add(client.SendAsync());
                    }

                    if (i < numberOfRequests)
                    {
                        logger.Info($"Waiting {secondsBetweenEachRequest} seconds between requests");
                        await Task.Delay(TimeSpan.FromSeconds(secondsBetweenEachRequest)).ConfigureAwait(false);
                        logger.Info($"Waited {secondsBetweenEachRequest} seconds, moving on to next request");
                    }
                }

                var responses = await Task.WhenAll(requests).ConfigureAwait(false);

                AllResponsesShouldHaveSucceeded(responses);

                Assert.Equal(numberOfRequests, responses.Length);

                var associations = server.Providers.SelectMany(p => p.Associations).ToList();

                Assert.Equal(expectedNumberOfAssociations, associations.Count);

                var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

                Assert.Equal(numberOfRequests, receivedRequests.Count);

                // now let the DicomClient complete gracefully
                await Task.WhenAll(sendTasks).ConfigureAwait(false);
            }
        }

        [Fact]
        public async Task SendAsync_Linger_ShouldAutomaticallyOpenNewAssociationAfterLingerTimeAfterLastRequest()
        {
            var numberOfRequests = 5;
            var lingerTimeoutInSeconds = 5;
            var secondsBetweenEachRequest = new[] {1, 1, 1, 6, 1};
            var expectedNumberOfAssociations = 2;
            var logger = _logger.IncludePrefix("UnitTest");
            var port = Ports.GetNext();

            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.AssociationLingerTimeoutInMs = lingerTimeoutInSeconds * 1000;

                logger.Info($"Beginning {numberOfRequests} parallel requests with variable wait times between each request");

                var requests = new List<Task<DicomCEchoResponse>>();

                var sendTasks = new List<Task>();

                for (var i = 1; i <= numberOfRequests; i++)
                {
                    var task = SendEchoRequestWithTimeout(client, 10000);
                    requests.Add(task);

                    if (client.IsSendRequired)
                    {
                        // Do not await here, because this task will only complete after the client has completely processed the request
                        sendTasks.Add(client.SendAsync());
                    }

                    if (i < numberOfRequests)
                    {
                        var secondsToWait = secondsBetweenEachRequest[i];
                        logger.Info($"Waiting {secondsBetweenEachRequest} seconds between requests");
                        await Task.Delay(TimeSpan.FromSeconds(secondsToWait)).ConfigureAwait(false);
                        logger.Info($"Waited {secondsBetweenEachRequest} seconds, moving on to next request");
                    }

                }

                var responses = await Task.WhenAll(requests).ConfigureAwait(false);

                AllResponsesShouldHaveSucceeded(responses);

                Assert.Equal(numberOfRequests, responses.Length);

                var associations = server.Providers.SelectMany(p => p.Associations).ToList();

                Assert.Equal(expectedNumberOfAssociations, associations.Count);

                var receivedRequests = server.Providers.SelectMany(p => p.Requests).ToList();

                Assert.Equal(numberOfRequests, receivedRequests.Count);

                // now let the DicomClient complete gracefully
                await Task.WhenAll(sendTasks).ConfigureAwait(false);
            }
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
                    logger.Info("Handling C-Store request");
                    capturedCStoreRequest = request;
                    await Task.Delay(50).ConfigureAwait(false);
                    return new DicomCStoreResponse(request, DicomStatus.Success);
                };

                var studyInstanceUID = "999.999.3859744";
                var seriesInstanceUID = "999.999.94827453";
                var sopInstanceUID = "999.999.133.1996.1.1800.1.6.21";

                logger.Info("Sending C-Get request");
                await client.AddRequestAsync(new DicomCGetRequest(studyInstanceUID, seriesInstanceUID, sopInstanceUID)).ConfigureAwait(false);

                var pcs = DicomPresentationContext.GetScpRolePresentationContextsFromStorageUids(
                    DicomStorageCategory.Image,
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    DicomTransferSyntax.ImplicitVRLittleEndian,
                    DicomTransferSyntax.ImplicitVRBigEndian);
                client.AdditionalPresentationContexts.AddRange(pcs);

                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(capturedCStoreRequest);
                Assert.Equal(DicomUID.Parse(sopInstanceUID).ToString(), capturedCStoreRequest.SOPInstanceUID.ToString());
            }
        }

        [Theory]
        [InlineData(2,1,2 )]
        [InlineData(10,2,5)]
        [InlineData(10,10,1)]
        [InlineData(100,10,10 )]
        public async Task SendAsync_MaxRequestsPerAssoc_ShouldAlwaysCreateCorrectNumberOfAssociations(int numberOfRequests, int maxRequestsPerAssoc, int expectedNumberOfAssociations)
        {
            var port = Ports.GetNext();
            var logger = _logger.IncludePrefix("UnitTest");

            using (var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>(port))
            {
                var client = CreateClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(10, 10);
                client.MaximumNumberOfRequestsPerAssociation = maxRequestsPerAssoc;

                logger.Info($"Beginning {numberOfRequests} requests with max {maxRequestsPerAssoc} requests / association");

                var responses = new ConcurrentBag<DicomCEchoResponse>();
                for (var i = 1; i <= numberOfRequests; i++)
                {
                    var iLocal = i;
                    var dicomCEchoRequest = new DicomCEchoRequest
                    {
                        OnResponseReceived = (request, response) =>
                        {
                            logger.Debug($"Request completed: {iLocal}");
                            responses.Add(response);
                        }
                    };
                    await client.AddRequestAsync(dicomCEchoRequest).ConfigureAwait(false);
                }

                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
                {
                    await client.SendAsync(cts.Token).ConfigureAwait(false);
                }

                AllResponsesShouldHaveSucceeded(responses);

                Assert.Equal(numberOfRequests, responses.Count());

                Assert.Equal(expectedNumberOfAssociations, server.Providers.Count());

                foreach (var provider in server.Providers)
                {
                    Assert.InRange(provider.Requests.Count(), 0, maxRequestsPerAssoc);
                }
            }
        }

        #region Support classes

        public class MockCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            public MockCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
                : base(stream, fallbackEncoding, log)
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
                    remoteHost = association.RemoteHost;
                    remotePort = association.RemotePort;
                    return SendAssociationAcceptAsync(association);
                }

                return SendAssociationRejectAsync(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.CalledAENotRecognized);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
            {
                return new DicomCEchoResponse(request, DicomStatus.Success);
            }
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

            public ExplicitLECStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
                : base(stream, fallbackEncoding, log)
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
            {
                return SendAssociationReleaseResponseAsync();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
            {
                return new DicomCStoreResponse(request, DicomStatus.Success);
            }

            public void OnCStoreRequestException(string tempFileName, Exception e)
            {
            }
        }

        public class RecordingDicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            private readonly ConcurrentBag<DicomAssociation> _associations;
            private readonly ConcurrentBag<DicomCEchoRequest> _requests;
            private readonly Func<DicomCEchoRequest, Task> _onRequest;
            private readonly TimeSpan? _responseTimeout;

            public IEnumerable<DicomCEchoRequest> Requests => _requests;
            public IEnumerable<DicomAssociation> Associations => _associations;

            public RecordingDicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log, Func<DicomCEchoRequest, Task> onRequest,
                TimeSpan? responseTimeout)
                : base(stream, fallbackEncoding, log)
            {
                _onRequest = onRequest ?? throw new ArgumentNullException(nameof(onRequest));
                _responseTimeout = responseTimeout;
                _requests = new ConcurrentBag<DicomCEchoRequest>();
                _associations = new ConcurrentBag<DicomAssociation>();
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

                _associations.Add(association);

                await SendAssociationAcceptAsync(association);
            }

            /// <inheritdoc />
            public async Task OnReceiveAssociationReleaseRequestAsync()
            {
                await WaitForALittleBit().ConfigureAwait(false);
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

            public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
            {
                _requests.Add(request);

                _onRequest(request).GetAwaiter().GetResult();

                WaitForALittleBit().GetAwaiter().GetResult();

                if (_responseTimeout.HasValue)
                {
                    Task.Delay(_responseTimeout.Value).GetAwaiter().GetResult();
                }

                return new DicomCEchoResponse(request, DicomStatus.Success);
            }
        }

        public class RecordingDicomCEchoProviderServer : DicomServer<RecordingDicomCEchoProvider>
        {
            private readonly ConcurrentBag<RecordingDicomCEchoProvider> _providers;
            private Func<DicomCEchoRequest, Task> _onRequest;
            private TimeSpan? _responseTimeout;

            public IEnumerable<RecordingDicomCEchoProvider> Providers => _providers;

            public RecordingDicomCEchoProviderServer()
            {
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
                var provider = new RecordingDicomCEchoProvider(stream, Encoding.UTF8, Logger, _onRequest, _responseTimeout);
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

            public RecordingDicomCGetProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
                : base(stream, fallbackEncoding, log)
            {
                _requests = new ConcurrentBag<DicomCGetRequest>();
                _associations = new ConcurrentBag<DicomAssociation>();
            }

            async Task WaitForALittleBit()
            {
                var ms = new Random().Next(10);
                await Task.Delay(ms);
            }

            /// <inheritdoc />
            public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                await WaitForALittleBit().ConfigureAwait(false);
                foreach (var pc in association.PresentationContexts)
                {
                    pc.SetResult(DicomPresentationContextResult.Accept);
                }

                _associations.Add(association);

                await SendAssociationAcceptAsync(association);
            }

            /// <inheritdoc />
            public async Task OnReceiveAssociationReleaseRequestAsync()
            {
                await WaitForALittleBit().ConfigureAwait(false);
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

            public IEnumerable<DicomCGetResponse> OnCGetRequest(DicomCGetRequest request)
            {
                _requests.Add(request);

                WaitForALittleBit().GetAwaiter().GetResult();

                yield return new DicomCGetResponse(request, DicomStatus.Pending);

                DicomCGetResponse nextResponse;

                try
                {
                    var file = DicomFile.Open(@".\Test Data\10200904.dcm");

                    var cStoreRequest = new DicomCStoreRequest(file);

                    SendRequestAsync(cStoreRequest).Wait();

                    nextResponse = new DicomCGetResponse(request, DicomStatus.Success);
                }
                catch (Exception e)
                {
                    Logger.Error("Could not send file via C-Store request: {error}", e);
                    nextResponse = new DicomCGetResponse(request, DicomStatus.ProcessingFailure);
                }

                yield return nextResponse;
            }
        }

        public class RecordingDicomCGetProviderServer : DicomServer<RecordingDicomCGetProvider>
        {
            private readonly ConcurrentBag<RecordingDicomCGetProvider> _providers;

            public IEnumerable<RecordingDicomCGetProvider> Providers => _providers;

            public RecordingDicomCGetProviderServer()
            {
                _providers = new ConcurrentBag<RecordingDicomCGetProvider>();
            }

            protected sealed override RecordingDicomCGetProvider CreateScp(INetworkStream stream)
            {
                var provider = new RecordingDicomCGetProvider(stream, Encoding.UTF8, Logger);
                _providers.Add(provider);
                return provider;
            }
        }

        #endregion
    }
}
