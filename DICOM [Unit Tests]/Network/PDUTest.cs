// Copyright (c) 2012-2015 fo-dicom contributors.
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

        [Fact]
        public void Save_ToNonExistingDirectory_Succeeds()
        {
            var path = @".\Test Data\PDU Test";
            var name = Path.Combine(path, "assoc.pdu");
            if (Directory.Exists(path)) Directory.Delete(path, true);

            var pdu = new RawPDU(0x01);
            pdu.Save(name);

            Assert.True(File.Exists(name));
        }
    }
}