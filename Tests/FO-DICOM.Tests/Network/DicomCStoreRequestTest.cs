// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    [Trait(TestTraits.Category, TestCategories.Network)]
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
        public async Task FallbackEncoding_Should_Handle_Missing_SpecificCharacterSet()
        {
            var actualEncoding = Encoding.GetEncoding("iso-8859-1");

            // This test file from Olympus does not include Specific Charater Set,
            // but it is clearly containing Swedish characters
            var file = DicomFile.Open(TestData.Resolve("VL_Olympus1.dcm"), actualEncoding);
            Assert.Equal("Efternamn^Förnamn^Mellannamn^^", file.Dataset.GetSingleValue<string>(DicomTag.PatientName));

            var port = Ports.GetNext();

            // Specify fallback encoding
            using var server = DicomServerFactory.Create<CStoreScp>(port, null, actualEncoding);

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "SCP");
            await client.AddRequestAsync(new DicomCStoreRequest(file));
            await client.SendAsync();

            // Verify received instance correctly shows Swedish characters
            var patientName = CStoreScp.LastReceivedSopInstance.GetSingleValue<string>(DicomTag.PatientName);
            Assert.Equal("Efternamn^Förnamn^Mellannamn^^", patientName);
        }


        private class CStoreScp : DicomService, IDicomCStoreProvider, IDicomServiceProvider
        {
            public static DicomDataset LastReceivedSopInstance;

            private readonly DicomTransferSyntax[] _acceptedImageTransferSyntaxes =
            {
                DicomTransferSyntax.ExplicitVRLittleEndian,
                DicomTransferSyntax.ImplicitVRLittleEndian
            };

            public CStoreScp(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies)
                : base(stream, fallbackEncoding, log, dependencies)
            {
            }

            public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
            {
                LastReceivedSopInstance = request.Dataset;
                return Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success));
            }

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
    }
}
