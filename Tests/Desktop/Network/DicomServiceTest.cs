// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

using Xunit;

namespace Dicom.Network
{
    [Collection("Network"), Trait("Category", "Network")]
    public class DicomServiceTest
    {
        #region Unit tests

        [Fact]
        public void Send_SingleRequest_DataSufficientlyTransported()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomDataset command = null, dataset = null;
                var request = new DicomCStoreRequest(@".\Test Data\CT1_J2KI");
                request.OnResponseReceived = (req, res) =>
                    {
                        command = request.Command;
                        dataset = request.Dataset;
                    };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var commandField = command.Get<ushort>(DicomTag.CommandField);
                Assert.Equal((ushort)1, commandField);

                var modality = dataset.Get<string>(DicomTag.Modality);
                Assert.Equal("CT", modality);
            }
        }

        [Fact]
        public async Task SendAsync_SingleRequest_DataSufficientlyTransported()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomDataset command = null, dataset = null;
                var request = new DicomCStoreRequest(@".\Test Data\CT1_J2KI");
                request.OnResponseReceived = (req, res) =>
                {
                    command = request.Command;
                    dataset = request.Dataset;
                };

                var client = new DicomClient();
                client.AddRequest(request);

                await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var commandField = command.Get<ushort>(DicomTag.CommandField);
                Assert.Equal((ushort)1, commandField);

                var modality = dataset.Get<string>(DicomTag.Modality);
                Assert.Equal("CT", modality);
            }
        }

        #endregion
    }
}
