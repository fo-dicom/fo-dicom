// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

using Dicom.Helpers;

using Xunit;

namespace Dicom.Bugs
{
    public class GH435
    {
        [Fact]
        public async Task DicomFileOpen_MissingSequenceDeclaration_CanReadBeyond()
        {
            var dicomFile = await ApplicationContent.OpenDicomFileAsync("Data/GH364.dcm");
            var actual = dicomFile.Dataset.Contains(DicomTag.StudyInstanceUID);
            Assert.True(actual);
        }
    }
}
