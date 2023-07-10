using System.IO;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    public class GH1341
    {
        [Fact]
        public void DicomDataSetGetValueOrDefault_WhenTagIsPresentButEmpty_ShouldReturnEmptyString()
        {
            // Arrange
            var originalDicomFile = new DicomFile
            {
                FileMetaInfo =
                {
                    TransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian
                }
            };
            originalDicomFile.Dataset.Add(DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID());
            originalDicomFile.Dataset.Add(DicomTag.ImageComments, string.Empty);

            // Roundtrip over a stream to simulate I/O
            using var ms = new MemoryStream();
            originalDicomFile.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var roundTrippedDicomFile = DicomFile.Open(ms);
            var dicomDataSet = roundTrippedDicomFile.Dataset;

            // Act
            var imageComments = dicomDataSet.GetSingleValue<string>(DicomTag.ImageComments);

            // Assert
            Assert.Equal(string.Empty, imageComments);
        }
    }
}
