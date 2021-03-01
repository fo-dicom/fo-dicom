// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Text;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection("General")]
    public class DicomEncodingTest
    {
        [Fact]
        public void Default_Getter_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            var actual = DicomEncoding.Default.CodePage;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEncoding_NonMatchingCharset_ReturnsUSASCII()
        {
            var expected = Encoding.ASCII.CodePage;
            var actual = DicomEncoding.GetEncoding("GBK").CodePage;
            Assert.Equal(expected, actual);
        }
      
        [Fact]
        public void GetEncoding_GB18030() //https://github.com/fo-dicom/fo-dicom/issues/481
        {
            int codePage = 0;
            var exception = Record.Exception(() => { codePage = DicomEncoding.GetEncoding("GB18030").CodePage; });
            Assert.Null(exception);
            Assert.Equal(54936, codePage);
        }

        [Fact]
        public void GetCharset_GB18030()
        {
            var expected = "GB18030";
            var encoding = DicomEncoding.GetEncoding("GB18030");
            var actual = DicomEncoding.GetCharset(encoding);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FallbackEncoding()
        {
            var ds = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, "1.2.345.67890" }
            };
            ds.AddOrUpdate(new DicomPersonName(DicomTag.PatientName, DicomEncoding.Default, new FellowOakDicom.IO.Buffer.MemoryByteBuffer(DicomEncoding.GetEncoding("ISO_IR 100").GetBytes("Hölzl^Günther"))));
            var firstFile = new DicomFile(ds);
            var filestream = new MemoryStream();
            firstFile.Save(filestream);

            filestream.Flush();

            filestream.Position = 0;
            var secondFile = DicomFile.Open(filestream);
            // the dataset has be read with Default enconding ASCII and therefore "ö" and "ü" should not be recognized
            Assert.Equal("H?lzl^G?nther", secondFile.Dataset.GetString(DicomTag.PatientName));

            filestream.Position = 0;
            var thirdFile = DicomFile.Open(filestream, DicomEncoding.GetEncoding("ISO_IR 100"));
            // if reading with middle european encoding as fallback default the "ö" and "ü" should be recognized
            Assert.Equal("Hölzl^Günther", thirdFile.Dataset.GetString(DicomTag.PatientName));
        }
    }
}
