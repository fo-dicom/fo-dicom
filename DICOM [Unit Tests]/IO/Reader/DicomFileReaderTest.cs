// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System.IO;

    using Xunit;

    public class DicomFileReaderTest
    {
        #region Unit tests

        [Fact]
        public void EndRead_ValidSource_ReturnsSuccess()
        {
            using (var stream = File.OpenRead(@".\Test Data\CT1_J2KI"))
            {
                var source = new StreamByteSource(stream);
                var reader = new DicomFileReader();

                const DicomReaderResult expected = DicomReaderResult.Success;
                var actual = reader.EndRead(
                    reader.BeginRead(source, new MockObserver(), new MockObserver(), null, null));

                Assert.Equal(expected, actual);
            }
        }

        #endregion
    }
}