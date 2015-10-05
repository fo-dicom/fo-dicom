// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Dicom.Log;

    using Xunit;

    [Collection("Network")]
    public class DicomServiceTest
    {
        #region Unit tests

        [Fact]
        public void Send_SingleRequest_DataSufficientlyTransported()
        {
            const int port = 11112;
            using (new DicomServer<MockCStoreProvider>(port))
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
            const int port = 11112;
            using (new DicomServer<MockCStoreProvider>(port))
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

        #region Support classes

        public class MockCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
        {
            public MockCStoreProvider(Stream stream, Logger log)
                : base(stream, log)
            {
            }

            public void OnReceiveAssociationRequest(DicomAssociation association)
            {
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