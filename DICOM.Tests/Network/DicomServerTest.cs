// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).


namespace Dicom.Network
{
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    using Xunit;

    [Collection("Network"), Trait("Category", "Network")]
    public class DicomServerTest
    {
        [Fact]
        public void Constructor_EstablishTwoWithSamePort_ShouldYieldAccessibleException()
        {
            var port = Ports.GetNext();

            var server1 = new DicomServer<DicomCEchoProvider>(port);
            while (!server1.IsListening) Thread.Sleep(10);
            var server2 = new DicomServer<DicomCEchoProvider>(port);
            Thread.Sleep(500);  // Allow for server2 to attempt listening

            Assert.True(server1.IsListening);
            Assert.Null(server1.Exception);

            Assert.False(server2.IsListening);
            Assert.IsType<SocketException>(server2.Exception);
        }

        [Fact]
        public void Stop_IsListening_TrueUntilStopRequested()
        {
            var port = Ports.GetNext();

            var server = new DicomServer<DicomCEchoProvider>(port);
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
    }
}