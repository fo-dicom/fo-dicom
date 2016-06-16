// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Dicom.Helpers;
    using Dicom.Log;

    using Xunit;
    using Xunit.Abstractions;

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
            this._testOutputHelper = testOutputHelper;
            remoteHost = null;
            remotePort = 0;
        }

        #endregion

        #region Unit tests

        [Fact]
        public void Send_SingleRequest_Recognized()
        {
            int port = Ports.GetNext();
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => ++counter };

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
            int port = Ports.GetNext();
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; ++i) client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => ++actual });

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(20)]
        [InlineData(200)]
        public void Send_MultipleTimes_AllRecognized(int expected)
        {
            int port = Ports.GetNext();
            var @lock = new object();

            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; ++i)
                {
                    client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => { lock (@lock) ++actual; } });
                    client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                }

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task SendAsync_SingleRequest_Recognized()
        {
            int port = Ports.GetNext();
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => ++counter };

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
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; ++i) client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => ++actual });

                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await Task.WhenAny(task, Task.Delay(10000));

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(20)]
        [InlineData(200)]
        public async Task SendAsync_MultipleTimes_AllRecognized(int expected)
        {
            int port = Ports.GetNext();

            using (
                var server = new DicomServer<DicomCEchoProvider>(
                    port,
                    options: new DicomServiceOptions(),
                    logger: new TestOutputHelperLogger(_testOutputHelper)))
            {
                await Task.Delay(500);
                Assert.True(server.IsListening, "Server is not listening");

                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; i++)
                {
                    client.AddRequest(
                        new DicomCEchoRequest
                            {
                                OnResponseReceived = (req, res) =>
                                    {
                                        _testOutputHelper.WriteLine("Response #{0}", i);
                                        Interlocked.Add(ref actual, 1);
                                    }
                            });
                    _testOutputHelper.WriteLine("Sending #{0}", i);
                    await Task.WhenAny(client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP"), Task.Delay(1000));
                    _testOutputHelper.WriteLine("Sent (or timed out) #{0}", i);
                }

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
                var server = new DicomServer<DicomCEchoProvider>(
                    port,
                    options: new DicomServiceOptions(),
                    logger: new TestOutputHelperLogger(_testOutputHelper)))
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
                                                Interlocked.Add(ref actual, 1);
                                            }
                                    });

                            _testOutputHelper.WriteLine("Sending #{0}", requestIndex);
                            var sendTask = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
                            await Task.WhenAny(sendTask, Task.Delay(1000));
                            _testOutputHelper.WriteLine("Sent (or timed out) #{0}", requestIndex);
                        }).ToList();
                await Task.WhenAll(requests);


                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void BeginSend_SingleRequest_Recognized()
        {
            int port = Ports.GetNext();
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => ++counter };

                var client = new DicomClient();
                client.AddRequest(request);

                client.EndSend(client.BeginSend("127.0.0.1", port, false, "SCU", "ANY-SCP", null, null));

                Assert.Equal(1, counter);
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(20)]
        [InlineData(100)]
        public void BeginSend_MultipleRequests_AllRecognized(int expected)
        {
            int port = Ports.GetNext();
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; ++i) client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => ++actual });

                client.EndSend(client.BeginSend("127.0.0.1", port, false, "SCU", "ANY-SCP", null, null));

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void WaitForAssociation_WithinTimeout_ReturnsTrue()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = client.WaitForAssociation(10000);
                task.Wait(10000);
                Assert.Equal(true, actual);
            }
        }

        [Fact]
        public void WaitForAssociation_TooShortTimeout_ReturnsFalse()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = client.WaitForAssociation(1);
                task.Wait(10000);
                Assert.Equal(false, actual);
            }
        }

        [Fact]
        public void WaitForAssociation_Aborted_ReturnsFalse()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.Abort();
                var actual = client.WaitForAssociation(500);

                Assert.Equal(false, actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_WithinTimeout_ReturnsTrue()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = await client.WaitForAssociationAsync(10000);
                task.Wait(10000);
                Assert.Equal(true, actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_TooShortTimeout_ReturnsFalse()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = await client.WaitForAssociationAsync(1);
                task.Wait(10000);
                Assert.Equal(false, actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_Aborted_ReturnsFalse()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Abort();

                var actual = await client.WaitForAssociationAsync(500);

                Assert.Equal(false, actual);
            }
        }

        [Fact]
        public void Release_AfterAssociation_SendIsCompleted()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
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
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.WaitForAssociation();

                await client.ReleaseAsync();
                Thread.Sleep(10);
                Assert.True(task.IsCompleted);
            }
        }

        [Fact]
        public void Send_RecordAssociationData_AssociationContainsHostAndPort()
        {
            int port = Ports.GetNext();
            using (new DicomServer<MockCEchoProvider>(port))
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
            using (new DicomServer<MockCEchoProvider>(port))
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
            using (new DicomServer<MockCEchoProvider>(port))
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
                Console.WriteLine(ex);
                awaiter.Set();
            }

            Assert.True(result);
        }

        #endregion

        #region Support classes

        public class MockCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            public MockCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
                : base(stream, fallbackEncoding, log)
            {
            }

            public void OnReceiveAssociationRequest(DicomAssociation association)
            {
                if (association.CalledAE.Equals("ANY-SCP", StringComparison.OrdinalIgnoreCase))
                {
                    Thread.Sleep(1000);
                    DicomClientTest.remoteHost = association.RemoteHost;
                    DicomClientTest.remotePort = association.RemotePort;
                    this.SendAssociationAccept(association);
                }
                else
                {
                    this.SendAssociationReject(
                        DicomRejectResult.Permanent,
                        DicomRejectSource.ServiceUser,
                        DicomRejectReason.CalledAENotRecognized);
                }
            }

            public void OnReceiveAssociationReleaseRequest()
            {
                this.SendAssociationReleaseResponse();
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

        #endregion
    }
}