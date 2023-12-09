// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.General)]
    public class GH1146
    {
        [Fact]
        public void PrivateCreator_UN_tag()
        {
            var dicomFile = DicomFile.Open(TestData.Resolve("GH1146.dcm"));
            var element = dicomFile.Dataset.GetDicomItem<DicomElement>(new DicomTag(0x0029, 0x0001));
            var expected = DicomVR.UN;
            var actual = element.ValueRepresentation;
            Assert.Equal(expected, actual);
        }
    }
}
