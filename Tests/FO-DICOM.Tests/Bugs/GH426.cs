// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Network;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("Network"), Trait("Category", "Network")]
    public class GH426
    {

        #region Unit tests

        [Fact]
        public async Task DicomClientSend_TooManyPresentationContexts_YieldsInformativeException()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<DicomCEchoProvider>(port))
            {
                var client = DicomClientFactory.Create("localhost", port, false, "SCU", "SCP");

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
