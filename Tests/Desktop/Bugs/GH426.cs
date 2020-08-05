// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;
using Dicom.Network;

using Dicom.Network.Client;
using Xunit;

namespace Dicom.Bugs
{
    [Collection("Network"), Trait("Category", "Network")]
    public class GH426
    {
        #region Unit tests

        [Fact]
        public void OldDicomClientSend_TooManyPresentationContexts_YieldsInformativeException()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new Network.DicomClient();

                // this just illustrates the issue of too many presentation contexts, not real world application.
                var pcs =
                    DicomPresentationContext.GetScpRolePresentationContextsFromStorageUids(
                        DicomStorageCategory.None,
                        DicomTransferSyntax.ImplicitVRLittleEndian);

                client.AdditionalPresentationContexts.AddRange(pcs);

                var exception = Record.Exception(() => client.Send("localhost", port, false, "SCU", "SCP"));
                Assert.IsType<DicomNetworkException>(exception);
            }
        }

        [Fact]
        public async Task DicomClientSend_TooManyPresentationContexts_YieldsInformativeException()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new Network.Client.DicomClient("localhost", port, false, "SCU", "SCP");

                // this just illustrates the issue of too many presentation contexts, not real world application.
                var pcs =
                    DicomPresentationContext.GetScpRolePresentationContextsFromStorageUids(
                        DicomStorageCategory.None,
                        DicomTransferSyntax.ImplicitVRLittleEndian);

                client.AdditionalPresentationContexts.AddRange(pcs);

                var exception = await Record.ExceptionAsync(() => client.SendAsync());
                Assert.IsType<DicomNetworkException>(exception);
            }
        }

        #endregion
    }
}
