// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
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
    }
}