// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Dicom.Log;

    using Xunit;

    [Collection("Network"), Trait("Category", "Network")]
    public class DicomServiceTest
    {
        #region Unit tests

        [Fact]
        public void Send_SingleRequest_DataSufficientlyTransported()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomDataset command = null, dataset = null;
                var request = new DicomCStoreRequest(@".\Test Data\CT1_J2KI");
                request.OnResponseReceived = (req, res) =>
                    {
                        command = request.Command;
                        dataset = request.Dataset;
                    };

                var client = new DicomClient();
                client.AddRequest(request);

                client.Send("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var commandField = command.Get<ushort>(DicomTag.CommandField);
                Assert.Equal((ushort)1, commandField);

                var modality = dataset.Get<string>(DicomTag.Modality);
                Assert.Equal("CT", modality);
            }
        }

        [Fact]
        public async Task SendAsync_SingleRequest_DataSufficientlyTransported()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<SimpleCStoreProvider>(port))
            {
                DicomDataset command = null, dataset = null;
                var request = new DicomCStoreRequest(@".\Test Data\CT1_J2KI");
                request.OnResponseReceived = (req, res) =>
                {
                    command = request.Command;
                    dataset = request.Dataset;
                };

                var client = new DicomClient();
                client.AddRequest(request);

                await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var commandField = command.Get<ushort>(DicomTag.CommandField);
                Assert.Equal((ushort)1, commandField);

                var modality = dataset.Get<string>(DicomTag.Modality);
                Assert.Equal("CT", modality);
            }
        }

        #endregion
    }

    #region Support classes

    internal class SimpleCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        private static readonly DicomTransferSyntax[] AcceptedTransferSyntaxes =
            {
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    DicomTransferSyntax.ExplicitVRBigEndian,
                    DicomTransferSyntax.ImplicitVRLittleEndian
                };

        private static readonly DicomTransferSyntax[] AcceptedImageTransferSyntaxes =
            {
                    // Lossless
                    DicomTransferSyntax.JPEGLSLossless,
                    DicomTransferSyntax.JPEG2000Lossless,
                    DicomTransferSyntax.JPEGProcess14SV1,
                    DicomTransferSyntax.JPEGProcess14,
                    DicomTransferSyntax.RLELossless,

                    // Lossy
                    DicomTransferSyntax.JPEGLSNearLossless,
                    DicomTransferSyntax.JPEG2000Lossy,
                    DicomTransferSyntax.JPEGProcess1,
                    DicomTransferSyntax.JPEGProcess2_4,

                    // Uncompressed
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    DicomTransferSyntax.ExplicitVRBigEndian,
                    DicomTransferSyntax.ImplicitVRLittleEndian
                };

        public SimpleCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.Verification) pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
                else if (pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None) pc.AcceptTransferSyntaxes(AcceptedImageTransferSyntaxes);
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

        public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
        {
            var tempName = Path.GetTempFileName();
            Logger.Info(tempName);
            request.File.Save(tempName);

            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
        {
        }
    }

    #endregion
}