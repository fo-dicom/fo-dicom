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

    }
}
