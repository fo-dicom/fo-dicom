// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).


using Dicom.Network.Client.EventArguments;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dicom.Network
{
    [Collection("Network")]
    [Trait("Category", "Network")]
    public class DicomCStoreRequestTest
    {
        [Fact]
        public void Can_create_request_from_invalid_DicomFile()
        {
            var file = new DicomFile();
            file.Dataset.ValidateItems = false;
            file.Dataset.Add(DicomTag.SOPClassUID, DicomUID.CTImageStorage);
            file.Dataset.Add(new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3.04"));
            file.Dataset.ValidateItems = true;


            var request = new DicomCStoreRequest(file);
            Assert.NotNull(request);
        }

        [Fact]
        public async Task Retreive_transfer_syntaxes_prior_to_sending()
        {
            Client.DicomClient dicomClient = new Client.DicomClient("127.0.0.1", 104, false, "SCU", "ANY-SCP");
            dicomClient.AssociationRequest += (object o, AssociationRequestEventArgs s) => {
                var totalTs = s.Association.PresentationContexts.First().GetTransferSyntaxes();
                Assert.True(totalTs.Count() > 0);
                var implicitTs = from t in totalTs
                                 where t.UID == DicomTransferSyntax.ImplicitVRLittleEndian.UID
                                 select t;
                Assert.True(implicitTs.Count() == 1);

                s.Association.PresentationContexts.First().ClearTransferSyntaxes();
                s.Association.PresentationContexts.First().AddTransferSyntax(DicomTransferSyntax.JPEGProcess1);
            };
            var cStore = new DicomCStoreRequest(@"Test Data\testjpeglossy.dcm");
            await dicomClient.AddRequestAsync(cStore);
            await dicomClient.SendAsync();
        }
    }
}
