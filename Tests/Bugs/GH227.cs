// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Threading.Tasks;

using Dicom.Imaging.Codec;

namespace Dicom.Bugs
{
    using Xunit;

    [Collection("General")]
    public class GH227
    {
        #region Unit tests

        [Fact]
        public void Open_DeflatedFile_DatasetReadilyAvailable()
        {
            var file = DicomFile.Open(@"Test Data\GH227.dcm");
            const int expected = 512;
            var actual = file.Dataset.Get<int>(DicomTag.Rows);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task OpenAsync_DeflatedFile_DatasetReadilyAvailable()
        {
            var file = await DicomFile.OpenAsync(@"Test Data\GH227.dcm").ConfigureAwait(false);
            const int expected = 512;
            var actual = file.Dataset.Get<int>(DicomTag.Columns);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Save_FileAsDeflated_CorrectlyCompressed()
        {
            var deflated =
                DicomFile.Open(@"Test Data\CT-MONO2-16-ankle")
                    .Clone(DicomTransferSyntax.DeflatedExplicitVRLittleEndian);

            using (var stream = new MemoryStream())
            {
                deflated.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var file = DicomFile.Open(stream);
                const int expected = 16;
                var actual = file.Dataset.Get<int>(DicomTag.BitsAllocated);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task SaveAsync_FileAsDeflated_CorrectlyCompressed()
        {
            var deflated =
                DicomFile.Open(@"Test Data\CT-MONO2-16-ankle")
                    .Clone(DicomTransferSyntax.DeflatedExplicitVRLittleEndian);

            using (var stream = new MemoryStream())
            {
                await deflated.SaveAsync(stream).ConfigureAwait(false);
                stream.Seek(0, SeekOrigin.Begin);

                var file = DicomFile.Open(stream);
                const int expected = 16;
                var actual = file.Dataset.Get<int>(DicomTag.BitsAllocated);
                Assert.Equal(expected, actual);
            }
        }

        #endregion
    }
}