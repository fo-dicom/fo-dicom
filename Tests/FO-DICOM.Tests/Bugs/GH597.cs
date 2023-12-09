// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    public class GH597
    {
        [Fact]
        public void DicomFileSave_LargeData16BitLength_ConvertsVRToUN()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian)
            {
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, "1.2.3"),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.4")
            };

            const int expectedLength = 0x10000;
            var sb = new StringBuilder();
            for (int i = 0; i < expectedLength - 1; i++)
            {
                sb.Append("0\\");
            }
            sb.Append("0");
            var expectedValue = sb.ToString();
            dataset.Add(DicomTag.ContourData, expectedValue);

            using var stream = new MemoryStream();

            var file = new DicomFile(dataset);
            file.Save(stream);

            // Checking that UN value representation has been written to raw stream;
            // when DICOM file is re-read, the value representation is automatically set to the known tag's VR.
            stream.Seek(0, SeekOrigin.Begin);
            var streamString = Encoding.UTF8.GetString(stream.GetBuffer());
            Assert.True(streamString.Contains("UN"), "streamString.Contains('UN')");

            var newDataset = DicomFile.Open(stream).Dataset;
            var contourDataItem = (DicomElement)newDataset.ElementAt(2);

            var actualLength = contourDataItem.Count;
            var actualValue = contourDataItem.Get<string>(-1);

            Assert.Equal(expectedLength, actualLength);
            Assert.Equal(expectedValue, actualValue);
        }

    }
}
