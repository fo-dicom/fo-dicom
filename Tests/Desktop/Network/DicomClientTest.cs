﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Dicom.Log;

    using Xunit;
    using Xunit.Abstractions;

namespace Dicom.Network
{
    [Collection("Network"), Trait("Category", "Network")]
    public class DicomClientTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        #region Fields

        private static string remoteHost;

        private static int remotePort;

        #endregion

        #region Constructors

        public DicomClientTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            remoteHost = null;
            remotePort = 0;
        }

        #endregion

        #region Unit tests

        [Fact]
        public void Send_SingleRequest_Recognized()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal(1, counter);
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(100)]
        [InlineData(1000)]
        public void Send_MultipleRequests_AllRecognized(int expected)
        {
            var port = Ports.GetNext();
            var flag = new ManualResetEventSlim();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var actual = 0;
                DicomCEchoRequest.ResponseDelegate callback = (req, res) =>
                {
                    Interlocked.Increment(ref actual);
                    if (actual == expected) flag.Set();
                };

                var client = new DicomClient();
                client.NegotiateAsyncOps(expected, 1);

                for (var i = 0; i < expected; ++i)
                    client.AddRequest(new DicomCEchoRequest { OnResponseReceived = callback });

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                flag.Wait(10000);

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(20)]
        [InlineData(100)]
        public void Send_MultipleTimes_AllRecognized(int expected)
        {
            var port = Ports.GetNext();
            var flag = new ManualResetEventSlim();

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) Thread.Sleep(50);

                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; ++i)
                {
                    client.AddRequest(
                        new DicomCEchoRequest
                            {
                                OnResponseReceived = (req, res) =>
                                    {
                                        _testOutputHelper.WriteLine($"{i}");
                                        Interlocked.Increment(ref actual);
                                        if (actual == expected) flag.Set();
                                    }
                            });
                    client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                }

                flag.Wait(10000);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task SendAsync_SingleRequest_Recognized()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) };

                var client = new DicomClient();
                client.AddRequest(request);

                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await Task.WhenAny(task, Task.Delay(10000));

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
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                client.NegotiateAsyncOps(expected, 1);

                for (var i = 0; i < expected; ++i) client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref actual) });

                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await Task.WhenAny(task, Task.Delay(30000));

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

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) await Task.Delay(50);

                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; i++)
                {
                    client.AddRequest(
                        new DicomCEchoRequest
                            {
                                OnResponseReceived = (req, res) =>
                                    {
                                        Interlocked.Increment(ref actual);
                                        if (actual == expected) flag.Set();
                                    }
                            });
                    await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
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
                var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                await Task.Delay(500);
                Assert.True(server.IsListening, "Server is not listening");

                var actual = 0;

                var requests = Enumerable.Range(0, expected).Select(
                    async requestIndex =>
                        {
                            var client = new DicomClient();
                            client.AddRequest(
                                new DicomCEchoRequest
                                    {
                                        OnResponseReceived = (req, res) =>
                                            {
                                                _testOutputHelper.WriteLine("Response #{0}", requestIndex);
                                                Interlocked.Increment(ref actual);
                                            }
                                    });

                            _testOutputHelper.WriteLine("Sending #{0}", requestIndex);
                            await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
                            _testOutputHelper.WriteLine("Sent (or timed out) #{0}", requestIndex);
                        }).ToList();
                await Task.WhenAll(requests);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void WaitForAssociation_WithinTimeout_ReturnsTrue()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = client.WaitForAssociation(10000);
                task.Wait(10000);
                Assert.True(actual);
            }
        }

        [Fact]
        public void WaitForAssociation_TooShortTimeout_ReturnsFalse()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (rq, rsp) => Thread.Sleep(100) });
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = client.WaitForAssociation(1);
                task.Wait(1000);
                Assert.False(actual);
            }
        }

        [Fact]
        public void WaitForAssociation_Aborted_ReturnsFalse()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.Abort();
                var actual = client.WaitForAssociation(1000);

                Assert.False(actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_WithinTimeout_ReturnsTrue()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = await client.WaitForAssociationAsync(10000);
                task.Wait(10000);
                Assert.True(actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_TooShortTimeout_ReturnsFalse()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (rq, rsp) => Thread.Sleep(100) });
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = await client.WaitForAssociationAsync(1);
                task.Wait(1000);
                Assert.False(actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_Aborted_ReturnsFalse()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Abort();

                var actual = await client.WaitForAssociationAsync(500);

                Assert.False(actual);
            }
        }

        [Fact]
        public void AssociationAccepted_SuccessfulSend_IsInvoked()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();

                var accepted = false;
                client.AssociationAccepted += (sender, args) => accepted = true;

                client.AddRequest(new DicomCEchoRequest());
                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.True(accepted);
            }
        }

        [Fact]
        public void AssociationRejected_AssociationNotAllowed_IsInvoked()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();

                var reason = DicomRejectReason.NoReasonGiven;
                client.AssociationRejected += (sender, args) => reason = args.Reason;

                client.AddRequest(new DicomCEchoRequest());
                var exception = Record.Exception(() => client.Send("127.0.0.1", port, false, "SCU", "NOTACCEPTEDSCP"));

                Assert.Equal(DicomRejectReason.CalledAENotRecognized, reason);
                Assert.NotNull(exception);
            }
        }

        [Fact]
        public void AssociationReleased_SuccessfulSend_IsInvoked()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new DicomClient();

                var released = false;
                var handle = new ManualResetEventSlim();
                client.AssociationReleased += (sender, args) => { released = true; handle.Set(); };

                client.AddRequest(new DicomCEchoRequest());
                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                handle.Wait(1000);
                Assert.True(released);
            }
        }

        [Fact]
        public void Release_AfterAssociation_SendIsCompleted()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.WaitForAssociation();

                client.Release();
                Thread.Sleep(10);
                Assert.True(task.IsCompleted);
            }
        }

        [Fact]
        public async Task ReleaseAsync_AfterAssociation_SendIsCompleted()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                Task task = null;
                var client = new DicomClient();
                client.AssociationAccepted += HandleAssociationAccepted;
                client.AddRequest(new DicomCEchoRequest());
                task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                void HandleAssociationAccepted(object sender, AssociationAcceptedEventArgs e)
                {
                    (sender as DicomClient).ReleaseAsync().Wait();
                    Thread.Sleep(10);
                    Assert.True(task.IsCompleted);
                }

            }
        }

        [Fact]
        public void Send_RecordAssociationData_AssociationContainsHostAndPort()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.NotNull(remoteHost);
                Assert.True(remotePort > 0);
                Assert.NotEqual(port, remotePort);
            }
        }

        [Fact]
        public void Send_RejectedAssociation_ShouldYieldException()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var exception = Record.Exception(() => client.Send("127.0.0.1", port, false, "SCU", "INVALID"));
                Assert.IsType<DicomAssociationRejectedException>(exception);
            }
        }

        [Fact]
        public async Task SendAsync_RejectedAssociation_ShouldYieldException()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var exception =
                    await
                    Record.ExceptionAsync(() => client.SendAsync("127.0.0.1", port, false, "SCU", "INVALID"))
                        .ConfigureAwait(false);
                Assert.IsType<DicomAssociationRejectedException>(exception);
            }
        }

        [Fact(Skip = "Requires external C-ECHO SCP")]
        public void Send_EchoRequestToExternalServer_ShouldSucceed()
        {
            var result = false;
            var awaiter = new ManualResetEventSlim();

            var client = new DicomClient();
            var req = new DicomCEchoRequest();
            req.OnResponseReceived = (rq, rsp) =>
                {
                    if (rsp.Status == DicomStatus.Success)
                    {
                        result = true;
                    }
                    awaiter.Set();
                };
            client.AddRequest(req);

            try
            {
                client.Send("localhost", 11112, false, "SCU", "COMMON");
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
        public void IsSendRequired_AddedRequestNotConnected_ReturnsTrue()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                Assert.True(client.IsSendRequired);
                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                Thread.Sleep(100);

                client.AddRequest(new DicomCEchoRequest());

                Assert.True(client.IsSendRequired);
            }
        }

        [Fact]
        public void IsSendRequired_NoRequestNotConnected_ReturnsFalse()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => Thread.Sleep(100) });
                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.False(client.IsSendRequired);
            }
        }

        [Fact]
        public void IsSendRequired_AddedRequestIsConnected_ReturnsFalse()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var flag = new ManualResetEventSlim();

                var client = new DicomClient { Linger = 100 };
                client.AddRequest(
                    new DicomCEchoRequest { OnResponseReceived = (req, res) => Interlocked.Increment(ref counter) });
                client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.AddRequest(
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
            }
        }

        [Fact]
        public void Send_ToExplicitOnlyProvider_NotAccepted()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<ExplicitLECStoreProvider>(port))
            {
                var request = new DicomCStoreRequest(@"./Test Data/CR-MONO1-10-chest");

                var client = new DicomClient();
                client.AddRequest(request);

                var exception = Record.Exception(() => client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP"));

                Assert.IsType<DicomAssociationRejectedException>(exception);
            }
        }

        [Theory]
        [InlineData(200)]
        public void Send_Plus128CStoreRequestsCompressedTransferSyntax_NoOverflowContextIdsAllRequestsRecognized(int expected)
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                client.NegotiateAsyncOps(expected, 1);

                for (var i = 0; i < expected; ++i)
                    client.AddRequest(new DicomCStoreRequest(@"./Test Data/CT1_J2KI")
                    {
                        OnResponseReceived = (req, res) => Interlocked.Increment(ref actual)
                    });

                var exception = Record.Exception(() => client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP"));

                Assert.Null(exception);
                Assert.Equal(expected, actual);
            }
        }

        #endregion

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
        /// testing <see cref="Send_ToExplicitOnlyProvider_NotAccepted"/>.
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

        #endregion
    }
}
