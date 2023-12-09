// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Network.Client.EventArguments;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network.Client
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomClientTimeoutTest
    {
        #region Fields

        private readonly XUnitDicomLogger _logger;

        #endregion

        #region Constructors

        public DicomClientTimeoutTest(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        #endregion

        #region Unit tests

        private IDicomServer CreateServer<T>(int port) where T : DicomService, IDicomServiceProvider
        {
            var server = DicomServerFactory.Create<T>(port);
            server.Logger = _logger.IncludePrefix(typeof(T).Name).WithMinimumLevel(LogLevel.Debug);
            return server;
        }

        private TServer CreateServer<TServer, TProvider>(int port)
            where TServer: IDicomServer<TProvider>
            where TProvider : DicomService, IDicomServiceProvider
        {
            var server = DicomServerFactory.Create<TProvider, TServer>(NetworkManager.IPv4Any, port);
            server.Logger = _logger.IncludePrefix(typeof(TProvider).Name).WithMinimumLevel(LogLevel.Debug);
            return (TServer) server;
        }

        private IDicomClient CreateClient(int port)
        {
            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name).WithMinimumLevel(LogLevel.Debug);
            return client;
        }

        private IDicomClientFactory CreateClientFactory(INetworkManager networkManager)
        {
            var loggerFactory = Setup.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var dicomServiceDependencies = Setup.ServiceProvider.GetRequiredService<DicomServiceDependencies>();
            var defaultClientOptions = Setup.ServiceProvider.GetRequiredService<IOptions<DicomClientOptions>>();
            var defaultServiceOptions = Setup.ServiceProvider.GetRequiredService<IOptions<DicomServiceOptions>>();
            var advancedDicomClientConnectionFactory = new DefaultAdvancedDicomClientConnectionFactory(networkManager, loggerFactory, defaultServiceOptions, dicomServiceDependencies);
            return new DefaultDicomClientFactory(
                defaultClientOptions,
                defaultServiceOptions,
                loggerFactory,
                advancedDicomClientConnectionFactory,
                Setup.ServiceProvider);
        }

        [Fact]
        public async Task SendingFindRequestToServerThatNeverRespondsShouldTimeout()
        {
            var port = Ports.GetNext();
            using (CreateServer<NeverRespondingDicomServer>(port))
            {
                var client = CreateClient(port);

                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(2);

                var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Patient)
                {
                    Dataset = new DicomDataset
                    {
                        {DicomTag.PatientID, "PAT123"}
                    },
                    OnResponseReceived = (req, res) => throw new Exception("Did not expect a response"),
                };

                DicomRequest.OnTimeoutEventArgs eventArgsFromRequestTimeout = null;
                request.OnTimeout += (sender, args) => eventArgsFromRequestTimeout = args;
                RequestTimedOutEventArgs eventArgsFromDicomClientRequestTimedOut = null;
                client.RequestTimedOut += (sender, args) => eventArgsFromDicomClientRequestTimedOut = args;

                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(10), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Equal(winner, sendTask);
                Assert.NotNull(eventArgsFromRequestTimeout);
                Assert.NotNull(eventArgsFromDicomClientRequestTimedOut);
                Assert.Equal(request, eventArgsFromDicomClientRequestTimedOut.Request);
                Assert.Equal(client.ServiceOptions.RequestTimeout, eventArgsFromDicomClientRequestTimedOut.Timeout);
            }
        }

        [Fact]
        public async Task SendingMoveRequestToServerThatNeverRespondsShouldTimeout()
        {
            var port = Ports.GetNext();
            using (CreateServer<NeverRespondingDicomServer>(port))
            {
                var client = CreateClient(port);

                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(2);

                var request = new DicomCMoveRequest("another-AE", "study123")
                {
                    OnResponseReceived = (req, res) => throw new Exception("Did not expect a response")
                };

                DicomRequest.OnTimeoutEventArgs onTimeoutEventArgs = null;
                request.OnTimeout += (sender, args) => onTimeoutEventArgs = args;
                RequestTimedOutEventArgs eventArgsFromDicomClientRequestTimedOut = null;
                client.RequestTimedOut += (sender, args) => eventArgsFromDicomClientRequestTimedOut = args;

                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(10), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Equal(winner, sendTask);
                Assert.NotNull(onTimeoutEventArgs);
                Assert.NotNull(eventArgsFromDicomClientRequestTimedOut);
                Assert.Equal(request, eventArgsFromDicomClientRequestTimedOut.Request);
                Assert.Equal(client.ServiceOptions.RequestTimeout, eventArgsFromDicomClientRequestTimedOut.Timeout);
            }
        }

        [Fact]
        public async Task SendingFindRequestToServerThatSendsPendingResponsesWithinTimeoutShouldNotTimeout()
        {
            var port = Ports.GetNext();
            using (CreateServer<FastPendingResponsesDicomServer>(port))
            {
                var client = CreateClient(port);

                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(2);

                DicomCFindResponse lastResponse = null;
                var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Patient)
                {
                    Dataset = new DicomDataset
                    {
                        {DicomTag.PatientID, "PAT123"}
                    },
                    OnResponseReceived = (req, res) => lastResponse = res
                };

                DicomRequest.OnTimeoutEventArgs onTimeoutEventArgs = null;
                request.OnTimeout += (sender, args) => onTimeoutEventArgs = args;

                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(10), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Equal(winner, sendTask);
                Assert.NotNull(lastResponse);
                Assert.Equal(lastResponse.Status, DicomStatus.Success);
                Assert.Null(onTimeoutEventArgs);
            }
        }

        [Fact]
        public async Task SendingMoveRequestToServerThatSendsPendingResponsesWithinTimeoutShouldNotTimeout()
        {
            var port = Ports.GetNext();
            using (CreateServer<FastPendingResponsesDicomServer>(port))
            {
                var client = CreateClient(port);

                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(2);

                DicomCMoveResponse lastResponse = null;
                var request = new DicomCMoveRequest("another-AE", "study123")
                {
                    OnResponseReceived = (req, res) => lastResponse = res
                };

                DicomRequest.OnTimeoutEventArgs onTimeoutEventArgs = null;
                request.OnTimeout += (sender, args) => onTimeoutEventArgs = args;

                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(10), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Equal(winner, sendTask);
                Assert.NotNull(lastResponse);
                Assert.Equal(lastResponse.Status, DicomStatus.Success);
                Assert.Null(onTimeoutEventArgs);
            }
        }

        [Fact]
        public async Task SendingFindRequestToServerThatSendsPendingResponsesTooSlowlyShouldTimeout()
        {
            var port = Ports.GetNext();
            using (CreateServer<SlowPendingResponsesDicomServer>(port))
            {
                var client = CreateClient(port);

                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(2);

                var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Patient)
                {
                    Dataset = new DicomDataset
                    {
                        {DicomTag.PatientID, "PAT123"}
                    }
                };

                DicomRequest.OnTimeoutEventArgs onTimeoutEventArgs = null;
                request.OnTimeout += (sender, args) => onTimeoutEventArgs = args;
                RequestTimedOutEventArgs eventArgsFromDicomClientRequestTimedOut = null;
                client.RequestTimedOut += (sender, args) => eventArgsFromDicomClientRequestTimedOut = args;

                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(10), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Equal(winner, sendTask);
                Assert.NotNull(onTimeoutEventArgs);
                Assert.NotNull(eventArgsFromDicomClientRequestTimedOut);
                Assert.Equal(request, eventArgsFromDicomClientRequestTimedOut.Request);
                Assert.Equal(client.ServiceOptions.RequestTimeout, eventArgsFromDicomClientRequestTimedOut.Timeout);
            }
        }

        [Fact]
        public async Task SendingMoveRequestToServerThatSendsPendingResponsesTooSlowlyShouldTimeout()
        {
            var port = Ports.GetNext();
            using (CreateServer<SlowPendingResponsesDicomServer>(port))
            {
                var client = CreateClient(port);

                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(2);

                var request = new DicomCMoveRequest("another-AE", "study123");

                DicomRequest.OnTimeoutEventArgs onTimeoutEventArgs = null;
                request.OnTimeout += (sender, args) => onTimeoutEventArgs = args;
                RequestTimedOutEventArgs eventArgsFromDicomClientRequestTimedOut = null;
                client.RequestTimedOut += (sender, args) => eventArgsFromDicomClientRequestTimedOut = args;

                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(10), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Equal(winner, sendTask);
                Assert.NotNull(onTimeoutEventArgs);
                Assert.NotNull(eventArgsFromDicomClientRequestTimedOut);
                Assert.Equal(request, eventArgsFromDicomClientRequestTimedOut.Request);
                Assert.Equal(client.ServiceOptions.RequestTimeout, eventArgsFromDicomClientRequestTimedOut.Timeout);
            }
        }

        [Fact]
        public async Task SendingLargeFileUsingVeryShortResponseTimeoutShouldSucceed()
        {
            var port = Ports.GetNext();
            using (CreateServer<InMemoryDicomCStoreProvider>(port))
            {
                var streamWriteTimeout = TimeSpan.FromMilliseconds(10);
                var clientFactory = CreateClientFactory(new ConfigurableNetworkManager(() => Thread.Sleep(streamWriteTimeout)));
                var client = clientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name).WithMinimumLevel(LogLevel.Debug);
                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(2);
                client.ServiceOptions.MaxPDULength = 16 * 1024; // 16 KB

                DicomResponse response = null;

                // Size = 5 192 KB, one PDU = 16 KB, so this will result in 325 PDUs
                // If stream timeout = 50ms, then total time to send will be 3s 250ms
                var request = new DicomCStoreRequest(TestData.Resolve("10200904.dcm"))
                {
                    OnResponseReceived = (req, res) => response = res,
                };
                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(10), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Equal(winner, sendTask);

                Assert.NotNull(response);

                Assert.Equal(DicomStatus.Success, response.Status);
            }
        }

        [Fact]
        public async Task SendingLargeFileUsingVeryShortResponseTimeoutAndSendingTakesTooLongShouldFail()
        {
            var port = Ports.GetNext();
            using (CreateServer<InMemoryDicomCStoreProvider>(port))
            {
                var streamWriteTimeout = TimeSpan.FromMilliseconds(1500);
                var clientFactory = CreateClientFactory(new ConfigurableNetworkManager(() => Thread.Sleep(streamWriteTimeout)));
                var client = clientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name).WithMinimumLevel(LogLevel.Debug);
                client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(1);
                client.ServiceOptions.MaxPDULength = 16 * 1024;

                DicomResponse response = null;
                DicomRequest.OnTimeoutEventArgs onTimeoutEventArgs = null;

                // Size = 5 192 KB, one PDU = 16 KB, so this will result in 325 PDUs
                // If stream timeout = 1500ms, then total time to send will be 325 * 1500 = 487.5 seconds
                var request = new DicomCStoreRequest(TestData.Resolve("10200904.dcm"))
                {
                    OnResponseReceived = (req, res) => response = res,
                    OnTimeout = (sender, args) => onTimeoutEventArgs = args
                };

                RequestTimedOutEventArgs eventArgsFromDicomClientRequestTimedOut = null;
                client.RequestTimedOut += (sender, args) => eventArgsFromDicomClientRequestTimedOut = args;
                await client.AddRequestAsync(request);

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromSeconds(20), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                Assert.Same(winner, sendTask);
                Assert.Null(response);
                Assert.NotNull(onTimeoutEventArgs);
                Assert.NotNull(eventArgsFromDicomClientRequestTimedOut);
                Assert.Equal(request, eventArgsFromDicomClientRequestTimedOut.Request);
                Assert.Equal(client.ServiceOptions.RequestTimeout, eventArgsFromDicomClientRequestTimedOut.Timeout);
            }
        }

        [Theory]
        [InlineData(/* number of reqs: */ 6, /* max requests per assoc: */ 1, /* async ops invoked: */ 1)]
        [InlineData(/* number of reqs: */ 6, /* max requests per assoc: */ 6, /* async ops invoked: */ 1)]
        [InlineData(/* number of reqs: */ 6, /* max requests per assoc: */ 2, /* async ops invoked: */ 1)]
        [InlineData(/* number of reqs: */ 6, /* max requests per assoc: */ 1, /* async ops invoked: */ 2)]
        [InlineData(/* number of reqs: */ 6, /* max requests per assoc: */ 6, /* async ops invoked: */ 6)]
        public async Task ShouldSendAllRequestsEvenThoughTheyAllTimeOut(int numberOfRequests, int maximumRequestsPerAssociation, int asyncOpsInvoked)
        {
            var options = new
            {
                Requests = numberOfRequests,
                TimeBetweenRequests = TimeSpan.FromMilliseconds(100),
                MaxRequestsPerAssoc = maximumRequestsPerAssociation,
            };

            var port = Ports.GetNext();
            using (CreateServer<NeverRespondingDicomServer>(port))
            {
                var client = CreateClient(port);
                client.NegotiateAsyncOps(asyncOpsInvoked);

                // Ensure the client is quite impatient
                client.ServiceOptions.RequestTimeout = TimeSpan.FromMilliseconds(200);

                var testLogger = _logger.IncludePrefix("Test");
                testLogger.LogInformation($"Beginning {options.Requests} parallel requests with {options.MaxRequestsPerAssoc} requests / association");

                var requests = new List<DicomRequest>();
                for (var i = 1; i <= options.Requests; i++)
                {
                    var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Study);

                    requests.Add(request);
                    await client.AddRequestAsync(request);

                    if (i < options.Requests)
                    {
                        testLogger.LogInformation($"Waiting {options.TimeBetweenRequests.TotalMilliseconds}ms between requests");
                        await Task.Delay(options.TimeBetweenRequests);
                        testLogger.LogInformation($"Waited {options.TimeBetweenRequests.TotalMilliseconds}ms, moving on to next request");
                    }
                }

                var timedOutRequests = new ConcurrentStack<DicomRequest>();
                client.RequestTimedOut += (sender, args) => { timedOutRequests.Push(args.Request); };

                var sendTask = client.SendAsync();
                var sendTimeoutCancellationTokenSource = new CancellationTokenSource();
                var sendTimeout = Task.Delay(TimeSpan.FromMinutes(1), sendTimeoutCancellationTokenSource.Token);

                var winner = await Task.WhenAny(sendTask, sendTimeout);

                sendTimeoutCancellationTokenSource.Cancel();
                sendTimeoutCancellationTokenSource.Dispose();

                if (winner != sendTask)
                    throw new Exception("DicomClient.SendAsync timed out");

                Assert.Equal(requests.OrderBy(m => m.MessageID), timedOutRequests.OrderBy(m => m.MessageID));
            }
        }

        [Fact]
        public async Task SendAsync_WithSocketException_ShouldNotLoopInfinitely()
        {
            var port = Ports.GetNext();

            DicomCStoreResponse response1 = null, response2 = null, response3 = null;
            DicomRequest.OnTimeoutEventArgs timeout1 = null, timeout2 = null, timeout3 = null;
            using (CreateServer<InMemoryDicomCStoreProvider>(port))
            {

                var request1HasArrived = false;
                var clientFactory = CreateClientFactory(new ConfigurableNetworkManager(() =>
                {
                    if (request1HasArrived)
                    {
                        throw new IOException("Request 1 has arrived, we can no longer write to this stream!",
                            new SocketException());
                    }
                }));
                var client = clientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name).WithMinimumLevel(LogLevel.Debug);

                // Ensure requests are handled sequentially
                client.NegotiateAsyncOps(1, 1);

                var request1 = new DicomCStoreRequest(@"./Test Data/10200904.dcm")
                {
                    OnResponseReceived = (req, res) =>
                    {
                        request1HasArrived = true;
                        response1 = res;
                    },
                    OnTimeout = (sender, args) => timeout1 = args
                };
                var request2 = new DicomCStoreRequest(@"./Test Data/10200904.dcm")
                {
                    OnResponseReceived = (req, res) => response2 = res,
                    OnTimeout = (sender, args) => timeout2 = args
                };
                var request3 = new DicomCStoreRequest(@"./Test Data/10200904.dcm")
                {
                    OnResponseReceived = (req, res) => response3 = res,
                    OnTimeout = (sender, args) => timeout3 = args
                };

                await client.AddRequestsAsync(new[] { request1, request2, request3 });

                using var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(1));

                Exception exception = null;
                try
                {
                    await client.SendAsync(cancellation.Token, DicomClientCancellationMode.ImmediatelyAbortAssociation);
                }
                catch (Exception e)
                {
                    exception = e;
                }

                Assert.NotNull(exception);

                Assert.False(cancellation.IsCancellationRequested);
            }

            Assert.NotNull(response1);
            Assert.Null(response2);
            Assert.Null(response3);
            Assert.Null(timeout1);
            Assert.Null(timeout2);
            Assert.Null(timeout3);
        }

        [Fact(Skip = "Flaky test. Sometimes gets stuck in an indefinite loop.")]
        public async Task SendAsync_WithGenericStreamException_ShouldNotLoopInfinitely()
        {
            var port = Ports.GetNext();
            var logger = _logger.IncludePrefix("UnitTest");

            DicomCStoreResponse response1 = null, response2 = null, response3 = null;
            DicomRequest.OnTimeoutEventArgs timeout1 = null, timeout2 = null, timeout3 = null;
            using (CreateServer<InMemoryDicomCStoreProvider>(port))
            {
                var request1HasArrived = false;
                var clientFactory = CreateClientFactory(new ConfigurableNetworkManager(() =>
                {
                    if (request1HasArrived)
                    {
                        throw new Exception("Request 1 has arrived, we can no longer write to this stream!");
                    }
                }));
                var client = clientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name).WithMinimumLevel(LogLevel.Debug);

                // Ensure requests are handled sequentially
                client.NegotiateAsyncOps(1, 1);

                // Size = 5 192 KB, one PDU = 16 KB, so this will result in 325 PDUs
                // If stream timeout = 1500ms, then total time to send will be 325 * 1500 = 487.5 seconds
                var request1 = new DicomCStoreRequest(@"./Test Data/10200904.dcm")
                {
                    OnResponseReceived = (req, res) =>
                    {
                        request1HasArrived = true;
                        response1 = res;
                    },
                    OnTimeout = (sender, args) => timeout1 = args
                };
                var request2 = new DicomCStoreRequest(@"./Test Data/10200904.dcm")
                {
                    OnResponseReceived = (req, res) => response2 = res,
                    OnTimeout = (sender, args) => timeout2 = args
                };
                var request3 = new DicomCStoreRequest(@"./Test Data/10200904.dcm")
                {
                    OnResponseReceived = (req, res) => response3 = res,
                    OnTimeout = (sender, args) => timeout3 = args
                };

                await client.AddRequestsAsync(new[] { request1, request2, request3 });

                using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(30));

                Exception exception = null;
                try
                {
                    await client.SendAsync(cancellation.Token, DicomClientCancellationMode.ImmediatelyAbortAssociation);
                }
                catch (Exception e)
                {
                    exception = e;
                }

                Assert.NotNull(exception);
                Assert.False(cancellation.IsCancellationRequested, "The DicomClient had to be cancelled, this indicates it was stuck in an infinite loop");
            }

            Assert.NotNull(response1);
            Assert.Null(response2);
            Assert.Null(response3);
            Assert.Null(timeout1);
            Assert.Null(timeout2);
            Assert.Null(timeout3);
        }

        [Fact]
        public async Task AssociationRequestTimeOutExceptionShouldThrowAfterMaxRetry()
        {
            var port = Ports.GetNext();
            const int maxRetryCount = 2;
            const int assocReqTimeOutInMs = 2000;
            int eventFired = 0;

            using (var server = CreateServer<ConfigurableDicomCEchoProviderServer, ConfigurableDicomCEchoProvider>(port))
            {
                server.OnAssociationRequest(async association =>
                {
                    await Task.Delay(100_000);
                    return true;
                });
                var client = CreateClient(port);
                client.ClientOptions.AssociationRequestTimeoutInMs = assocReqTimeOutInMs;
                client.ClientOptions.MaximumNumberOfConsecutiveTimedOutAssociationRequests = maxRetryCount;
                client.AssociationRequestTimedOut += (sender, args) =>
                {
                    eventFired++;
                };
                await client.AddRequestAsync(new DicomCEchoRequest());

                Exception exception = null;
                try
                {
                    await client.SendAsync();
                }
                catch (DicomAssociationRequestTimedOutException e)
                {
                    exception = e;
                }

                // event will be fired total of initial association request + the maximimum retry count;
                Assert.Equal(maxRetryCount, eventFired);
                Assert.NotNull(exception);
            }
        }

        [Fact]
        public async Task AssociationRequestRetryCounterShouldResetWhenAssociationIsAccepted()
        {
            var port = Ports.GetNext();
            const int maxRetryCount = 2;
            const int assocReqTimeOutInMs = 2000;
            int eventFired = 0;

            using (var server = CreateServer<ConfigurableDicomCEchoProviderServer, ConfigurableDicomCEchoProvider>(port))
            {
                server.Logger = _logger.IncludePrefix("Server");
                var associationRequest = 0;
                server.OnAssociationRequest(async association =>
                {
                    var currentAssociationRequest = Interlocked.Increment(ref associationRequest);
                    // Ensure that it times out once (2 failed attempts) + once more on the second SendAsync
                    if (currentAssociationRequest <= maxRetryCount + 1)
                    {
                        await Task.Delay(5_000);
                    }

                    return true;
                });

                var client = CreateClient(port);
                client.Logger = _logger.IncludePrefix("Client");
                client.ClientOptions.AssociationRequestTimeoutInMs = assocReqTimeOutInMs;
                client.ClientOptions.MaximumNumberOfConsecutiveTimedOutAssociationRequests = maxRetryCount;
                client.AssociationRequestTimedOut += (sender, args) =>
                {
                    eventFired++;
                };

                Exception exception1 = null;
                try
                {
                    await client.AddRequestAsync(new DicomCEchoRequest());
                    await client.SendAsync();
                }
                catch (DicomAssociationRequestTimedOutException e)
                {
                    exception1 = e;
                }

                client.Logger.LogInformation("Second request + SendAsync");

                Exception exception2 = null;
                try
                {
                    await client.AddRequestAsync(new DicomCEchoRequest());
                    await client.SendAsync();
                }
                catch (DicomAssociationRequestTimedOutException e)
                {
                    exception2 = e;
                }

                Assert.Equal(maxRetryCount + 1, eventFired);
                Assert.NotNull(exception1);
                Assert.Null(exception2);
            }
        }

        [Fact]
        public async Task AssociationRequestRetryCounterShouldResetWhenAssociationIsRejected()
        {
            var port = Ports.GetNext();
            const int maxRetryCount = 2;
            const int assocReqTimeOutInMs = 2000;
            int eventFired = 0;

            using (var server = CreateServer<ConfigurableDicomCEchoProviderServer, ConfigurableDicomCEchoProvider>(port))
            {
                server.Logger = _logger.IncludePrefix("Server");
                var associationRequest = 0;
                server.OnAssociationRequest(async association =>
                {
                    var currentAssociationRequest = Interlocked.Increment(ref associationRequest);
                    // Ensure that it times out once (2 failed attempts) + once more on the second SendAsync
                    if (currentAssociationRequest <= maxRetryCount + 1)
                    {
                        await Task.Delay(5_000);
                    }

                    return false;
                });

                var client = CreateClient(port);
                client.Logger = _logger.IncludePrefix("Client");
                client.ClientOptions.AssociationRequestTimeoutInMs = assocReqTimeOutInMs;
                client.ClientOptions.MaximumNumberOfConsecutiveTimedOutAssociationRequests = maxRetryCount;
                client.AssociationRequestTimedOut += (sender, args) =>
                {
                    eventFired++;
                };

                Exception timeoutException1 = null;
                Exception rejectException1 = null;
                try
                {
                    await client.AddRequestAsync(new DicomCEchoRequest());
                    await client.SendAsync();
                }
                catch (DicomAssociationRejectedException e)
                {
                    rejectException1 = e;
                }
                catch (DicomAssociationRequestTimedOutException e)
                {
                    timeoutException1 = e;
                }

                client.Logger.LogInformation("Second request + SendAsync");

                Exception timeoutException2 = null;
                Exception rejectException2 = null;
                try
                {
                    await client.AddRequestAsync(new DicomCEchoRequest());
                    await client.SendAsync();
                }
                catch (DicomAssociationRejectedException e)
                {
                    rejectException2 = e;
                }
                catch (DicomAssociationRequestTimedOutException e)
                {
                    timeoutException2 = e;
                }

                Assert.Equal(maxRetryCount+1, eventFired);
                Assert.Null(rejectException1);
                Assert.NotNull(timeoutException1);
                Assert.NotNull(rejectException2);
                Assert.Null(timeoutException2);
            }
        }

        #endregion

        #region Support classes

        internal class ConfigurableNetworkManager : DesktopNetworkManager
        {
            private readonly Action _onStreamWrite;

            public ConfigurableNetworkManager(Action onStreamWrite)
            {
                _onStreamWrite = onStreamWrite ?? throw new ArgumentNullException(nameof(onStreamWrite));
            }

            protected internal override INetworkStream CreateNetworkStreamImpl(NetworkStreamCreationOptions options)
            {
                return new ConfigurableDesktopNetworkStreamDecorator(
                    _onStreamWrite,
                    new DesktopNetworkStream(options)
                );
            }
        }

        private class ConfigurableDesktopNetworkStreamDecorator : INetworkStream
        {
            private readonly Action _onStreamWrite;
            private readonly DesktopNetworkStream _desktopNetworkStream;

            public ConfigurableDesktopNetworkStreamDecorator(Action onStreamWrite, DesktopNetworkStream desktopNetworkStream)
            {
                _onStreamWrite = onStreamWrite ?? throw new ArgumentNullException(nameof(onStreamWrite));
                _desktopNetworkStream = desktopNetworkStream;
            }

            public void Dispose()
            {
                _desktopNetworkStream.Dispose();
            }

            public string RemoteHost => _desktopNetworkStream.RemoteHost;

            public string LocalHost => _desktopNetworkStream.LocalHost;

            public int RemotePort => _desktopNetworkStream.RemotePort;

            public int LocalPort => _desktopNetworkStream.LocalPort;

            public Stream AsStream()
            {
                return new ConfigurableStreamDecorator(_onStreamWrite, (NetworkStream)_desktopNetworkStream.AsStream());
            }
        }

        private class ConfigurableStreamDecorator : Stream
        {
            private readonly Action _onStreamWrite;
            private readonly NetworkStream _inner;

            public ConfigurableStreamDecorator(Action onStreamWrite, NetworkStream inner)
            {
                _onStreamWrite = onStreamWrite ?? throw new ArgumentNullException(nameof(onStreamWrite));
                _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            }

            public override void Flush() => _inner.Flush();

            public override long Seek(long offset, SeekOrigin origin) => _inner.Seek(offset, origin);

            public override void SetLength(long value) => _inner.SetLength(value);

            public override int Read(byte[] buffer, int offset, int count) => _inner.Read(buffer, offset, count);

            public override void Write(byte[] buffer, int offset, int count)
            {
                _onStreamWrite();

                _inner.Write(buffer, offset, count);
            }

            public override bool CanRead => _inner.CanRead;

            public override bool CanSeek => _inner.CanSeek;

            public override bool CanWrite => _inner.CanWrite;

            public override long Length => _inner.Length;

            public override long Position
            {
                get => _inner.Position;
                set => _inner.Position = value;
            }

            public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
            {
                return _inner.CopyToAsync(destination, bufferSize, cancellationToken);
            }

            public override void Close()
            {
                _inner.Close();
            }

            protected override void Dispose(bool disposing)
            {
                _inner.Dispose();
                base.Dispose(disposing);
            }

            public override Task FlushAsync(CancellationToken cancellationToken)
            {
                return _inner.FlushAsync(cancellationToken);
            }

            public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            {
                return _inner.BeginRead(buffer, offset, count, callback, state);
            }

            public override int EndRead(IAsyncResult asyncResult)
            {
                return _inner.EndRead(asyncResult);
            }

            public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                return _inner.ReadAsync(buffer, offset, count, cancellationToken);
            }

            public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            {
                return _inner.BeginWrite(buffer, offset, count, callback, state);
            }

            public override void EndWrite(IAsyncResult asyncResult)
            {
                _inner.EndWrite(asyncResult);
            }

            public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                _onStreamWrite();

                return _inner.WriteAsync(buffer, offset, count, cancellationToken);
            }

            public override int ReadByte()
            {
                return _inner.ReadByte();
            }

            public override void WriteByte(byte value)
            {
                _inner.WriteByte(value);
            }

            public override bool CanTimeout => _inner.CanTimeout;
            public override int ReadTimeout => _inner.ReadTimeout;

            public override int WriteTimeout
            {
                get => _inner.WriteTimeout;
                set => _inner.WriteTimeout = value;
            }

            public override string ToString()
            {
                return _inner.ToString();
            }

            public override bool Equals(object obj)
            {
                return _inner.Equals(obj);
            }

            public override int GetHashCode()
            {
                return _inner.GetHashCode();
            }
        }

        private class InMemoryDicomCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
        {
            public InMemoryDicomCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
                DicomServiceDependencies dependencies) : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var presentationContext in association.PresentationContexts)
                {
                    foreach (var ts in presentationContext.GetTransferSyntaxes())
                    {
                        presentationContext.SetResult(DicomPresentationContextResult.Accept, ts);
                        break;
                    }
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
                => Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success));

            public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
                => Task.CompletedTask;

        }

        private class NeverRespondingDicomServer : DicomService, IDicomServiceProvider, IDicomCFindProvider, IDicomCMoveProvider
        {
            private readonly ConcurrentBag<DicomRequest> _requests;

            public IEnumerable<DicomRequest> Requests => _requests;

            public NeverRespondingDicomServer(INetworkStream stream, Encoding fallbackEncoding,
                ILogger log, DicomServiceDependencies dependencies) : base(stream, fallbackEncoding, log, dependencies)
            {
                _requests = new ConcurrentBag<DicomRequest>();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var presentationContext in association.PresentationContexts)
                {
                    foreach (var ts in presentationContext.GetTransferSyntaxes())
                    {
                        presentationContext.SetResult(DicomPresentationContextResult.Accept, ts);
                        break;
                    }
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request)
            {
                await Task.Yield();
                _requests.Add(request);
                yield break;
            }

            public async IAsyncEnumerable<DicomCMoveResponse> OnCMoveRequestAsync(DicomCMoveRequest request)
            {
                await Task.Yield();
                _requests.Add(request);
                yield break;
            }

        }


        private class FastPendingResponsesDicomServer : DicomService, IDicomServiceProvider, IDicomCFindProvider, IDicomCMoveProvider
        {
            public FastPendingResponsesDicomServer(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies) : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var presentationContext in association.PresentationContexts)
                {
                    foreach (var ts in presentationContext.GetTransferSyntaxes())
                    {
                        presentationContext.SetResult(DicomPresentationContextResult.Accept, ts);
                        break;
                    }
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request)
            {
                await Task.Delay(400);
                yield return new DicomCFindResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCFindResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCFindResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCFindResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCFindResponse(request, DicomStatus.Success);
            }

            public async IAsyncEnumerable<DicomCMoveResponse> OnCMoveRequestAsync(DicomCMoveRequest request)
            {
                await Task.Delay(400);
                yield return new DicomCMoveResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCMoveResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCMoveResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCMoveResponse(request, DicomStatus.Pending);
                await Task.Delay(400);
                yield return new DicomCMoveResponse(request, DicomStatus.Success);
            }

        }


        private class SlowPendingResponsesDicomServer : DicomService, IDicomServiceProvider, IDicomCFindProvider, IDicomCMoveProvider
        {
            public SlowPendingResponsesDicomServer(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
                DicomServiceDependencies dependencies) : base(
                stream, fallbackEncoding, log, dependencies)
            {
            }

            private TimeSpan Delay => (UserState is TimeSpan span) ? span : TimeSpan.FromSeconds(4);

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var presentationContext in association.PresentationContexts)
                {
                    foreach (var ts in presentationContext.GetTransferSyntaxes())
                    {
                        presentationContext.SetResult(DicomPresentationContextResult.Accept, ts);
                        break;
                    }
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request)
            {
                yield return new DicomCFindResponse(request, DicomStatus.Pending);
                await Task.Delay(Delay);
                yield return new DicomCFindResponse(request, DicomStatus.Success);
            }

            public async IAsyncEnumerable<DicomCMoveResponse> OnCMoveRequestAsync(DicomCMoveRequest request)
            {
                yield return new DicomCMoveResponse(request, DicomStatus.Pending);
                await Task.Delay(Delay);
                yield return new DicomCMoveResponse(request, DicomStatus.Success);
            }
        }

        #endregion
    }
}

