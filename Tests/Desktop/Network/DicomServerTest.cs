// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Helpers;

namespace Dicom.Network
{
    using System.Linq;
    using System.Threading;

    using Xunit;

    [Collection("Network"), Trait("Category", "Network"), TestCaseOrderer("Dicom.Helpers.PriorityOrderer", "DICOM [Unit Tests]")]
    public class DicomServerTest
    {
        [Fact]
        public void Constructor_EstablishTwoWithSamePort_ShouldYieldAccessibleException()
        {
            var port = Ports.GetNext();

            var server1 = DicomServer.Create<DicomCEchoProvider>(port);
            while (!server1.IsListening) Thread.Sleep(10);

            var exception = Record.Exception(() => DicomServer.Create<DicomCEchoProvider>(port));
            Assert.IsType<DicomNetworkException>(exception);

            Assert.True(server1.IsListening);
            Assert.Null(server1.Exception);
        }

        [Fact]
        public void Stop_IsListening_TrueUntilStopRequested()
        {
            var port = Ports.GetNext();

            var server = DicomServer.Create<DicomCEchoProvider>(port);
            while (!server.IsListening) Thread.Sleep(10);

            for (var i = 0; i < 10; ++i)
            {
                Thread.Sleep(500);
                Assert.True(server.IsListening);
            }

            server.Stop();
            Thread.Sleep(500);

            Assert.False(server.IsListening);
        }

        [Fact]
        public void Create_GetInstanceSamePort_ReturnsInstance()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var server = DicomServer.GetInstance(port);
                Assert.Equal(port, server.Port);
            }
        }

        [Fact]
        public void Create_GetInstanceSamePortAfterDisposal_ReturnsNull()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port)) { }

            var server = DicomServer.GetInstance(port);
            Assert.Null(server);
        }

        [Fact]
        public void Create_TwiceOnSamePortWithDisposalInBetween_DoesNotThrow()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
            }

            var e = Record.Exception(
                () =>
                    {
                        using (DicomServer.Create<DicomCEchoProvider>(port))
                        {
                            Assert.NotNull(DicomServer.GetInstance(port));
                        }
                    });
            Assert.Null(e);
        }

        [Fact]
        public void Create_GetInstanceDifferentPort_ReturnsNull()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var server = DicomServer.GetInstance(Ports.GetNext());
                Assert.Null(server);
            }
        }

        [Fact]
        public void Create_MultipleInstancesSamePort_Throws()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var e = Record.Exception(() => DicomServer.Create<DicomCEchoProvider>(port));
                Assert.IsType<DicomNetworkException>(e);
            }
        }

        [Fact]
        public void Create_MultipleInstancesDifferentPorts_AllRegistered()
        {
            var ports = new int[20].Select(i => Ports.GetNext()).ToArray();

            foreach (var port in ports)
            {
                var server = DicomServer.Create<DicomCEchoProvider>(port);
                while (!server.IsListening) Thread.Sleep(10);
            }

            foreach (var port in ports)
            {
                Assert.Equal(port, DicomServer.GetInstance(port).Port);
            }

            foreach (var port in ports)
            {
                DicomServer.GetInstance(port).Dispose();
            }
        }

        [Fact]
        public void IsListening_DicomServerRunningOnPort_ReturnsTrue()
        {
            var port = Ports.GetNext();

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) Thread.Sleep(10);
                Assert.True(DicomServer.IsListening(port));
            }
        }

        [Fact]
        public void IsListening_DicomServerStoppedOnPort_ReturnsFalse()
        {
            var port = Ports.GetNext();

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                server.Stop();
                Thread.Sleep(500);

                Assert.NotNull(DicomServer.GetInstance(port));
                Assert.False(DicomServer.IsListening(port));
            }
        }

        [Fact]
        public void IsListening_DicomServerNotInitializedOnPort_ReturnsFalse()
        {
            var port = Ports.GetNext();

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                Assert.False(DicomServer.IsListening(Ports.GetNext()));
            }
        }

        [Fact]
        public void Send_KnownSOPClass_SendSucceeds()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(@".\Test Data\CT-MONO2-16-ankle");
                request.OnResponseReceived = (req, res) =>
                    { status = res.Status; };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal(DicomStatus.Success, status);
            }
        }

        [Fact, TestPriority(1)]
        public void Send_PrivateNotRegisteredSOPClass_SendFails()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(@".\Test Data\GH355.dcm");
                request.OnResponseReceived = (req, res) =>
                    { status = res.Status; };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal(DicomStatus.SOPClassNotSupported, status);
            }
        }

        [Fact, TestPriority(2)]
        public void Send_PrivateRegisteredSOPClass_SendSucceeds()
        {
            var uid = new DicomUID("1.3.46.670589.11.0.0.12.1", "Private MR Spectrum Storage", DicomUidType.SOPClass);
            DicomUID.Register(uid);

            var port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(@".\Test Data\GH355.dcm");
                request.OnResponseReceived = (req, res) =>
                    { status = res.Status; };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal(DicomStatus.Success, status);
            }
        }

        [Fact]
        public void Stop_DisconnectedClientsCount_ShouldBeZeroAfterShortDelay()
        {
            var port = Ports.GetNext();

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) Thread.Sleep(10);

                var client = new DicomClient();
                client.AddRequest(new DicomCEchoRequest());
                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Release(0);
                Thread.Sleep(100);

                server.Stop();
                Thread.Sleep(100);

                var actual = ((DicomServer<DicomCEchoProvider>) server).CompletedServicesCount;
                Assert.Equal(0, actual);
            }
        }
    }
}
