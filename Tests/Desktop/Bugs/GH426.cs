// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Network;

using Xunit;

namespace Dicom.Bugs
{
    [Collection("Network"), Trait("Category", "Network")]
    public class GH426
    {
        #region Unit tests

        [Fact]
        public void DicomClientSend_TooManyPresentationContexts_YieldsInformativeException()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomCEchoProvider>(port))
            {
                var client = new DicomClient();

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

        #endregion
    }
}
