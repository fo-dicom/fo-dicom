// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomServiceTest
    {

        #region Unit tests

        [Fact]
        public async Task Send_PrivateTags_DataSufficientlyTransported()
        {
            var port = Ports.GetNext();
            using var _ = DicomServerFactory.Create<SimpleCStoreProvider>(port);

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

            request.Dataset = new DicomDataset
                {
                    { DicomTag.Modality, "CT" },
                    { DicomVR.LO, privTag1, "TESTA" },
                    new DicomCodeString(privTag2, "TESTB")
                };

            request.OnResponseReceived = (req, res) =>
            {
                command = req.Command;
                requestDataset = req.Dataset;
                responseDataset = res.Dataset;
            };

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            await client.AddRequestAsync(request);

            await client.SendAsync();

            Assert.Equal((ushort)1, command.GetSingleValue<ushort>(DicomTag.CommandField));

            Assert.Equal("CT", requestDataset.GetString(DicomTag.Modality));
            Assert.Equal("TESTB", requestDataset.GetSingleValueOrDefault<string>(privTag2, null));
            Assert.Equal("TESTA", requestDataset.GetSingleValueOrDefault<string>(privTag1, null));

            Assert.Equal("CT", responseDataset.GetSingleValue<string>(DicomTag.Modality));
            Assert.Equal("TESTB", responseDataset.GetValueOrDefault<string>(privTag2, 0, null));
            Assert.Equal("TESTA", responseDataset.GetSingleValueOrDefault<string>(privTag1, null));
        }


        [Fact]
        public async Task SendAsync_SingleRequest_DataSufficientlyTransported()
        {
            int port = Ports.GetNext();
            using var _ = DicomServerFactory.Create<SimpleCStoreProvider>(port);

            DicomDataset command = null, dataset = null;
            var request = new DicomCStoreRequest(TestData.Resolve("CT1_J2KI"));
            request.OnResponseReceived = (req, res) =>
            {
                command = request.Command;
                dataset = request.Dataset;
            };

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            await client.AddRequestAsync(request);

            await client.SendAsync();

            var commandField = command.GetSingleValue<ushort>(DicomTag.CommandField);
            Assert.Equal((ushort)1, commandField);

            var modality = dataset.GetSingleValue<string>(DicomTag.Modality);
            Assert.Equal("CT", modality);
        }

        #endregion
    }
}
