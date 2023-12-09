// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Linq;
using System.Text;
using FellowOakDicom.Network;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    public class DicomPresentationContextCollectionTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DicomPresentationContextCollectionTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private void Print(DicomPresentationContextCollection presentationContexts)
        {
            var sb = new StringBuilder();
            foreach (var pc in presentationContexts)
            {
                sb.AppendFormat("Presentation Context:  {0} [{1}]\n", pc.ID, pc.Result);
                if (pc.AbstractSyntax.Name != "Unknown")
                {
                    sb.AppendFormat("     Abstract Syntax:  {0}\n", pc.AbstractSyntax.Name);
                }
                else
                {
                    sb.AppendFormat("     Abstract Syntax:  {0}\n", pc.AbstractSyntax);
                }

                foreach (var tx in pc.GetTransferSyntaxes())
                {
                    sb.AppendFormat("     Transfer Syntax:  {0}\n", tx.UID.Name);
                }
            }
            _testOutputHelper.WriteLine(sb.ToString());
        }

        [Fact]
        public void AddFromRequest_CStoreRequest_ShouldAddPresentationContextWithImplicitVRLittleEndian()
        {
            // Arrange
            var presentationContexts = new DicomPresentationContextCollection();
            var file = "Test Data/CR-ModalitySequenceLUT.dcm"; // JPEG Lossless
            var dicomFile = DicomFile.Open(file);
            var cStoreRequest = new DicomCStoreRequest(dicomFile);

            // Act
            presentationContexts.AddFromRequest(cStoreRequest);

            // Assert
            Print(presentationContexts);
            var presentationContext = presentationContexts.FirstOrDefault(pc =>
                pc.AbstractSyntax == cStoreRequest.SOPClassUID
                && pc.HasTransferSyntax(DicomTransferSyntax.ImplicitVRLittleEndian));

            Assert.NotNull(presentationContext);
        }

        [Fact]
        public void AddFromRequest_CStoreRequest_WithAdditionalTransferSyntaxes_ShouldAddPresentationContextWithAdditionalSyntaxes()
        {
            // Arrange
            var presentationContexts = new DicomPresentationContextCollection();
            var file = "Test Data/CR-ModalitySequenceLUT.dcm"; // JPEG Lossless
            var dicomFile = DicomFile.Open(file);
            var cStoreRequest = new DicomCStoreRequest(dicomFile)
            {
                AdditionalTransferSyntaxes = new []
                {
                    DicomTransferSyntax.ExplicitVRLittleEndian,
                    DicomTransferSyntax.JPEGProcess14SV1,
                }
            };

            // Act
            presentationContexts.AddFromRequest(cStoreRequest);

            // Assert
            Print(presentationContexts);

            Assert.NotNull(presentationContexts
                .FirstOrDefault(pc => pc.AbstractSyntax == cStoreRequest.SOPClassUID
                                      && pc.HasTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian)));
            Assert.NotNull(presentationContexts
                .FirstOrDefault(pc => pc.AbstractSyntax == cStoreRequest.SOPClassUID
                                      && pc.HasTransferSyntax(DicomTransferSyntax.JPEGProcess14SV1)));
        }

        [Fact]
        public void AddFromRequest_CStoreRequest_WhenDicomFileIsDeflated_ShouldIncludeExplicitVRLittleEndian()
        {
            // Arrange
            var presentationContexts = new DicomPresentationContextCollection();
            var file = "Test Data/Issue1097_FailToOpenDeflatedFileWithSQ.dcm"; // Deflated Explicit VR Little Endian
            var dicomFile = DicomFile.Open(file);
            var cStoreRequest = new DicomCStoreRequest(dicomFile);

            // Act
            presentationContexts.AddFromRequest(cStoreRequest);

            // Assert
            Print(presentationContexts);
            var presentationContext = presentationContexts.FirstOrDefault(pc =>
                pc.AbstractSyntax == cStoreRequest.SOPClassUID
                && pc.HasTransferSyntax(cStoreRequest.TransferSyntax));

            Assert.NotNull(presentationContext);
            Assert.True(presentationContext.HasTransferSyntax(DicomTransferSyntax.DeflatedExplicitVRLittleEndian));
            Assert.True(presentationContext.HasTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian));
        }

        [Fact]
        public void AddFromRequest_CStoreRequest_OmitImplicitVR_For_Lossless_Throws()
        {
            // Arrange
            var presentationContexts = new DicomPresentationContextCollection();
            var file = "Test Data/CR-ModalitySequenceLUT.dcm"; // JPEG Lossless
            var dicomFile = DicomFile.Open(file);
            var cStoreRequest = new DicomCStoreRequest(dicomFile);

            // Act
            cStoreRequest.OmitImplicitVrTransferSyntaxInAssociationRequest = true;

            // Assert
            Assert.Throws<InvalidOperationException>(() => presentationContexts.AddFromRequest(cStoreRequest));
        }

        [Fact]
        public void AddFromRequest_CStoreRequest_OmitImplicitVR_For_Lossy_ShouldNotIncludeExplicitVRLittleEndian()
        {
            // Arrange
            var presentationContexts = new DicomPresentationContextCollection();
            var file = "Test Data/GH538-jpeg1.dcm"; // JPEG Process 1
            var dicomFile = DicomFile.Open(file);
            var cStoreRequest = new DicomCStoreRequest(dicomFile);

            // Act
            cStoreRequest.OmitImplicitVrTransferSyntaxInAssociationRequest = true;
            presentationContexts.AddFromRequest(cStoreRequest);

            // Assert
            Print(presentationContexts);
            var presentationContext = presentationContexts.FirstOrDefault(pc =>
                pc.AbstractSyntax == cStoreRequest.SOPClassUID
                && pc.HasTransferSyntax(cStoreRequest.TransferSyntax));

            Assert.NotNull(presentationContext);
            Assert.True(presentationContext.HasTransferSyntax(DicomTransferSyntax.JPEGProcess1));
            Assert.False(presentationContext.HasTransferSyntax(DicomTransferSyntax.ImplicitVRLittleEndian));
        }

        [Fact]
        public void Add_SamePresentationContextTwice_ShouldSilentlyNotAddTheSecondOne()
        {
            // Arrange
            var presentationContexts = new DicomPresentationContextCollection();

            // Act
            // 1
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 2 (extra transfer syntax)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1, DicomTransferSyntax.JPEG2000Lossless }
            );

            // 3 (user role is true)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: true,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 4 (provider role is false)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: false,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 1b (this one is identical to 1 and should not be added)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // Assert
            Print(presentationContexts);
            Assert.Equal(4, presentationContexts.Count);

            var pc1 = presentationContexts.Single(pc => pc.UserRole == null && pc.ProviderRole == null && pc.GetTransferSyntaxes().Count == 1);
            var pc2 = presentationContexts.Single(pc => pc.GetTransferSyntaxes().Count == 2);
            var pc3 = presentationContexts.Single(pc => pc.UserRole == true);
            var pc4 = presentationContexts.Single(pc => pc.ProviderRole == false);

            Assert.NotNull(pc1);
            Assert.NotNull(pc2);
            Assert.NotNull(pc3);
            Assert.NotNull(pc4);
        }

        [Fact]
        public void Add_SamePresentationContextTwice_ShouldMaintainCorrectPresentationContextIds()
        {
            // Arrange
            var presentationContexts = new DicomPresentationContextCollection();

            // Act
            // 1
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 1b (this one is identical to 1 and should not be added)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 2 (extra transfer syntax)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1, DicomTransferSyntax.JPEG2000Lossless }
            );

            // 1c (this one is identical to 1 and should not be added)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 3 (user role is true)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: true,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 1d (this one is identical to 1 and should not be added)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: null,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // 4 (provider role is false)
            presentationContexts.Add(
                abstractSyntax: DicomUID.CTImageStorage,
                userRole: null,
                providerRole: false,
                transferSyntaxes: new[] { DicomTransferSyntax.JPEGProcess14SV1 }
            );

            // Assert
            Print(presentationContexts);
            Assert.Equal(4, presentationContexts.Count);

            var pc1 = presentationContexts.Single(pc => pc.UserRole == null && pc.ProviderRole == null && pc.GetTransferSyntaxes().Count == 1);
            var pc2 = presentationContexts.Single(pc => pc.GetTransferSyntaxes().Count == 2);
            var pc3 = presentationContexts.Single(pc => pc.UserRole == true);
            var pc4 = presentationContexts.Single(pc => pc.ProviderRole == false);

            Assert.NotNull(pc1);
            Assert.NotNull(pc2);
            Assert.NotNull(pc3);
            Assert.NotNull(pc4);
            Assert.Equal((byte) 1, pc1.ID);
            Assert.Equal((byte) 3, pc2.ID);
            Assert.Equal((byte) 5, pc3.ID);
            Assert.Equal((byte) 7, pc4.ID);
        }

    }
}
