// Copyright (c) 2011-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.IO;

    using Xunit;

    public class PDUTest
    {
        [Fact]
        public void Write_AeWithNonAsciiCharacters_ShouldBeAsciified()
        {
            var notExpected = "GÖTEBORG";
            var request = new AAssociateRQ(new DicomAssociation("MALMÖ", notExpected));

            var writePdu = request.Write();

            RawPDU readPdu;
            using (var stream = new MemoryStream())
            {
                writePdu.WritePDU(stream);

                var length = (int)writePdu.Length;
                var buffer = new byte[length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(buffer, 0, length);
                readPdu = new RawPDU(buffer);
            }

            readPdu.Reset();
            readPdu.SkipBytes("Unknown", 10);
            var actual = readPdu.ReadString("Called AE", 16);

            Assert.NotEqual(notExpected, actual);
        }
    }
}