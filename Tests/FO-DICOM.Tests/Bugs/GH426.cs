// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class GH426
    {
        private readonly XUnitDicomLogger _logger;

        public GH426(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper).IncludeTimestamps().IncludeThreadId().IncludePrefix("Client");
        }

        #region Unit tests

        [Fact]
        public async Task DicomClientSend_TooManyPresentationContexts_YieldsInformativeException()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<DicomCEchoProvider>(port))
            {
                var client = DicomClientFactory.Create("localhost", port, false, "SCU", "SCP");

                client.Logger = _logger;

                // this just illustrates the issue of too many presentation contexts, not real world application.
                var pcs =
                    DicomPresentationContext.GetScpRolePresentationContextsFromStorageUids(
                        DicomStorageCategory.None,
                        DicomTransferSyntax.ImplicitVRLittleEndian);

                client.AdditionalPresentationContexts.AddRange(pcs);

                DicomCGetRequest request = new DicomCGetRequest("1.2.840.113619.2.1.1.322987881.621.736170080");

                await client.AddRequestAsync(request);

                var exception = await Record.ExceptionAsync(() => client.SendAsync());
                Assert.IsType<DicomNetworkException>(exception);
                Assert.Equal("Too many presentation contexts configured for this association!", exception.Message);
            }
        }

        #endregion
    }
}
