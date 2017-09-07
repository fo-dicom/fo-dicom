// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Linq;
using System.Text;

using Xunit;

namespace Dicom.Bugs
{
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

            const int length = 0x10000;
            var sb = new StringBuilder();
            sb.Append('0', length);
            dataset.Add(DicomTag.ContourData, sb.ToString());

            using (var stream = new MemoryStream())
            {
                var file = new DicomFile(dataset);
                file.Save(stream);

                stream.Seek(0, SeekOrigin.Begin);
                var newDataset = DicomFile.Open(stream).Dataset;
                var contourDataItem = (DicomElement)newDataset.ElementAt(2);

                Assert.Equal((uint)length, contourDataItem.Length);

                var expected = DicomVR.UN;
                var actual = contourDataItem.ValueRepresentation;
                Assert.Equal(expected, actual);
            }
        }
    }
}
