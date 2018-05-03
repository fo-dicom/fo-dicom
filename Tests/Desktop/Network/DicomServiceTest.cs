// Copyright (c) 2012-2018 fo-dicom contributors.
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
        public void Send_PrivateTags_DataSufficientlyTransported()
        {
            var port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomDataset command = null, requestDataset = null, responseDataset = null;
                var request = new DicomCStoreRequest(new DicomDataset
                {
                    { DicomTag.CommandField, (ushort)DicomCommandField.CStoreRequest },
                    { DicomTag.AffectedSOPClassUID, DicomUID.CTImageStorage },
                    { DicomTag.MessageID, (ushort)1 },
                    { DicomTag.AffectedSOPInstanceUID, "1.2.3" },
                });

                var privateCreator = DicomDictionary.Default.GetPrivateCreator("Testing");
                var privTag1 = new DicomTag(4013, 0x008, privateCreator);
                var privTag2 = new DicomTag(4013, 0x009, privateCreator);
                DicomDictionary.Default.Add(new DicomDictionaryEntry(privTag1, "testTag1", "testKey1", DicomVM.VM_1, false, DicomVR.CS));
                DicomDictionary.Default.Add(new DicomDictionaryEntry(privTag2, "testTag2", "testKey2", DicomVM.VM_1, false, DicomVR.CS));

                request.Dataset = new DicomDataset();
                request.Dataset.Add(DicomTag.Modality, "CT");
                request.Dataset.Add(privTag1, "test1");
                request.Dataset.Add(new DicomCodeString(privTag2, "test2"));
                //{
                //    { DicomTag.Modality, "CT" },
                //    new DicomCodeString(privTag1, "test1"),
                //    { privTag2, "test2" },
                //};

                request.OnResponseReceived = (req, res) =>
                {
                    command = req.Command;
                    requestDataset = req.Dataset;
                    responseDataset = res.Dataset;
                };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                Assert.Equal((ushort)1, command.Get<ushort>(DicomTag.CommandField));

                Assert.Equal("CT", requestDataset.Get<string>(DicomTag.Modality));
                Assert.Equal("test2", requestDataset.Get<string>(privTag2, null));
                Assert.Equal("test1", requestDataset.Get<string>(privTag1, null));

                Assert.Equal("CT", responseDataset.Get<string>(DicomTag.Modality));
               // Assert.Equal("test1", responseDataset.Get<DicomCodeString>(privTag1).Get<string>());
                Assert.Equal("test2", responseDataset.Get<string>(privTag2,-1, null));
                Assert.Equal("test1", responseDataset.Get<string>(privTag1, null));
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
