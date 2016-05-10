// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom.Imaging.Codec
{
    public class DicomCodecExtensionsTest
    {
        [Fact]
        public void ChangeTransferSyntax_FromJ2KToJPEGWithParameters_DoesNotThrow()
        {
            var file = DicomFile.Open(@".\Test Data\CT1_J2KI");
            var exception =
                Record.Exception(
                    () =>
                    file.ChangeTransferSyntax(DicomTransferSyntax.JPEGProcess2_4, new DicomJpegParams { Quality = 50 }));
            Assert.Null(exception);
        }
    }
}
