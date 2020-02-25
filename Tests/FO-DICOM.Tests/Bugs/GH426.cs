// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using FellowOakDicom.Tests.Network;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

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

            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port))
            {
                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create("localhost", port, false, "SCU", "SCP");

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
