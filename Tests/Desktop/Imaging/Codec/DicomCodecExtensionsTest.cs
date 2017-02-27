// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Imaging.Codec
{
    public class DicomCodecExtensionsTest
    {
        #region Unit tests

        [Fact]
        public void ChangeTransferSyntax_FileFromJ2KToJPEGWithParameters_DoesNotThrow()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var exception =
                Record.Exception(
                    () =>
                    file.Clone(DicomTransferSyntax.JPEGProcess14, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }

        [Fact]
        public void ChangeTransferSyntax_DatasetFromJ2KToJPEGWithParameters_DoesNotThrow()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var exception =
                Record.Exception(
                    () =>
                    file.Dataset.Clone(DicomTransferSyntax.JPEGProcess14, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }

        #endregion
    }
}
