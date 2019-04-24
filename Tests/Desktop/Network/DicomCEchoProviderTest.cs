// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using Dicom.Helpers;
    using Dicom.Log;
    using Dicom.Network.Client;

    using Xunit;

    [Collection("Network")]
    [Trait("Category", "Network")]
    public class OldDicomCEchoProviderTest
    {
        [Fact]
        public void Send_FromDicomClient_DoesNotDeadlock()
        {
            LogManager.SetImplementation(NLogManager.Instance);
            var target = NLogHelper.AssignMemoryTarget(
                "Dicom.Network",
                @"${message}",
                NLog.LogLevel.Trace);

            var port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new Network.DicomClient();
                for (var i = 0; i < 10; i++)
                {
                    client.AddRequest(new DicomCEchoRequest());
                }

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");
                Assert.True(target.Logs.Count > 0);
            }
        }
    }

    [Collection("Network")]
    [Trait("Category", "Network")]
    public class DicomCEchoProviderTest
    {
        [Fact]
        public void Send_FromDicomClient_DoesNotDeadlock()
        {
            LogManager.SetImplementation(NLogManager.Instance);
            var target = NLogHelper.AssignMemoryTarget(
                "Dicom.Network",
                @"${message}",
                NLog.LogLevel.Trace);

            var port = Ports.GetNext();
            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                for (var i = 0; i < 10; i++)
                {
                    client.AddRequest(new DicomCEchoRequest());
                }

                client.Send();
                Assert.True(target.Logs.Count > 0);
            }
        }
    }
}
