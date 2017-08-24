// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace Dicom.Bugs
{
    using System;
    using System.Text;
    using Dicom.Log;
    using Dicom.Network;

    using Xunit;

    public class GH306
    {
        #region Unit tests

        [Fact]
        public void DicomClientSend_StoreNonPart10File_ShouldSucceed()
        {
            var port = Ports.GetNext();

            using (var server = DicomServer.Create<CStoreScp>(port))
            {
                var file = DicomFile.Open(@".\Test Data\CR-MONO1-10-chest");

                var client = new DicomClient();
                client.AddRequest(new DicomCStoreRequest(file));

                var exception = Record.Exception(() => client.Send("127.0.0.1", port, false, "SCU", "SCP"));
                Assert.Null(exception);
            }
        }

        [Fact]
        public void DicomClientSend_StorePart10File_ShouldSucceed()
        {
            var port = Ports.GetNext();

            using (var server = DicomServer.Create<CStoreScp>(port))
            {
                var file = DicomFile.Open(@".\Test Data\CT-MONO2-16-ankle");

                var client = new DicomClient();
                client.AddRequest(new DicomCStoreRequest(file));

                var exception = Record.Exception(() => client.Send("127.0.0.1", port, false, "SCU", "SCP"));
                Assert.Null(exception);
            }
        }

        #endregion

        #region Support types

        private class CStoreScp : DicomService, IDicomCStoreProvider, IDicomServiceProvider
        {
            private DicomTransferSyntax[] AcceptedImageTransferSyntaxes =
                {
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    DicomTransferSyntax.ExplicitVRBigEndian,
                    DicomTransferSyntax.ImplicitVRLittleEndian
                };

            public CStoreScp(INetworkStream stream, Encoding fallbackEncoding, Logger log)
                : base(stream, fallbackEncoding, log)
            {
            }

            public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
            {
                return new DicomCStoreResponse(request, DicomStatus.Success);
            }

            public void OnCStoreRequestException(string tempFileName, Exception e)
            {
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.AcceptTransferSyntaxes(AcceptedImageTransferSyntaxes);
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