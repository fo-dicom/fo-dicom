// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using Xunit;

    public class DicomServiceTest
    {
        #region Unit tests

        [Fact]
        public void Send_SingleRequest_Recognized()
        {
            const int port = 11112;
            using (new DicomServer<DicomCEchoProvider>(port))
            {
                DicomDataset command, dataset;
                var request = new DicomCEchoRequest { OnResponseReceived = (req, res) =>
                    {
                        command = req.Command;
                        dataset = req.Dataset;
                    } };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                //TODO
                Assert.Equal(1, 2);
            }
        }

        #endregion
    }
}