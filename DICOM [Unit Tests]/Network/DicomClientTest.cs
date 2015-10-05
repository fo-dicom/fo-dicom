// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Dicom.Log;

    using Xunit;

    [Collection("Network")]
    public class DicomClientTest
    {
        #region Unit tests

        [Fact]
        public void Send_SingleRequest_Recognized()
        {
            const int port = 11112;
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
        [InlineData(5)]
        [InlineData(20)]
        [InlineData(100)]
        public void Send_MultipleRequests_AllRecognized(int expected)
        {
            const int port = 11112;
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
        public void Send_MultipleTimes_AllRecognized(int expected)
        {
            const int port = 11112;
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; ++i)
                {
                    client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => ++actual });
                    client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                }

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task SendAsync_SingleRequest_Recognized()
        {
            const int port = 11112;
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var counter = 0;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) => ++counter };

                var client = new DicomClient();
                client.AddRequest(request);

                await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal(1, counter);
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(20)]
        [InlineData(100)]
        public async Task SendAsync_MultipleRequests_AllRecognized(int expected)
        {
            const int port = 11112;
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; ++i) client.AddRequest(new DicomCEchoRequest { OnResponseReceived = (req, res) => ++actual });

                await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void BeginSend_SingleRequest_Recognized()
        {
            const int port = 11112;
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
            const int port = 11112;
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
            const int port = 11112;
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = client.WaitForAssociation(10000);
                task.Wait();
                Assert.Equal(true, actual);
            }
        }

        [Fact]
        public void WaitForAssociation_TooShortTimeout_ReturnsFalse()
        {
            const int port = 11112;
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = client.WaitForAssociation(1);
                task.Wait();
                Assert.Equal(false, actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_WithinTimeout_ReturnsTrue()
        {
            const int port = 11112;
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = await client.WaitForAssociationAsync(10000);
                task.Wait();
                Assert.Equal(true, actual);
            }
        }

        [Fact]
        public async Task WaitForAssociationAsync_TooShortTimeout_ReturnsFalse()
        {
            const int port = 11112;
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var actual = await client.WaitForAssociationAsync(1);
                task.Wait();
                Assert.Equal(false, actual);
            }
        }

        [Fact]
        public void Release_AfterAssociation_SendIsCompleted()
        {
            const int port = 11112;
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.WaitForAssociation();
                Thread.Sleep(10);
                Assert.False(task.IsCompleted);

                client.Release();
                Thread.Sleep(10);
                Assert.True(task.IsCompleted);
            }
        }

        [Fact]
        public async Task ReleaseAsync_AfterAssociation_SendIsCompleted()
        {
            const int port = 11112;
            using (new DicomServer<MockCEchoProvider>(port))
            {
                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                var task = client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                client.WaitForAssociation();
                Thread.Sleep(10);
                Assert.False(task.IsCompleted);

                await client.ReleaseAsync();
                Thread.Sleep(10);
                Assert.True(task.IsCompleted);
            }
        }

        #endregion

        #region Support classes

        public class MockCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            public MockCEchoProvider(Stream stream, Logger log)
                : base(stream, log)
            {
            }

            public void OnReceiveAssociationRequest(DicomAssociation association)
            {
                Thread.Sleep(1000);
                this.SendAssociationAccept(association);
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