// Copyright (c) 2010-2015 Anders Gustafsson, Cureos AB.
// All rights reserved. Any unauthorised reproduction of this 
// material will constitute an infringement of copyright.

namespace Dicom.Network
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PDUTest
    {
        [TestMethod]
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

            Assert.AreNotEqual(notExpected, actual);
        }
    }
}