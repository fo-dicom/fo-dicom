// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class GH306
    {
        private readonly XUnitDicomLogger _logger;

        public GH306(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper).IncludeTimestamps().IncludeThreadId();
        }

        #region Unit tests

        [Fact]
        public async Task DicomClientSend_StoreNonPart10File_ShouldSucceed()
        {
            var port = Ports.GetNext();

            using var server = DicomServerFactory.Create<CStoreScp>(port);
            server.Logger = _logger.IncludePrefix("CStoreScp");

            var file = DicomFile.Open(TestData.Resolve("CR-MONO1-10-chest"));

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "SCP");
            client.Logger = _logger.IncludePrefix("DicomClient");
            await client.AddRequestAsync(new DicomCStoreRequest(file));

            var exception = await Record.ExceptionAsync(async () => await client.SendAsync());
            Assert.Null(exception);
        }

        [Fact]
        public async Task DicomClientSend_StorePart10File_ShouldSucceed()
        {
            var port = Ports.GetNext();

            using var server = DicomServerFactory.Create<CStoreScp>(port);
            server.Logger = _logger.IncludePrefix("CStoreScp");

            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "SCP");
            client.Logger = _logger.IncludePrefix("DicomClient");

            await client.AddRequestAsync(new DicomCStoreRequest(file));

            var exception = await Record.ExceptionAsync(async () => await client.SendAsync());
            Assert.Null(exception);
        }

        #endregion

        #region Support types

        private class CStoreScp : DicomService, IDicomCStoreProvider, IDicomServiceProvider
        {
            private DicomTransferSyntax[] _acceptedImageTransferSyntaxes =
            {
                DicomTransferSyntax.ExplicitVRLittleEndian,
                DicomTransferSyntax.ExplicitVRBigEndian,
                DicomTransferSyntax.ImplicitVRLittleEndian
            };

            public CStoreScp(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
                => Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success));

            public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
                => Task.CompletedTask;

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.AcceptTransferSyntaxes(_acceptedImageTransferSyntaxes);
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }
        }

        #endregion
    }
}
