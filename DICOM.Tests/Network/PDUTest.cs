// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.IO;

    using Xunit;

    [Collection("Network")]
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

		[Fact]
		public void WriteReadAAssociateRQExtendedNegotiation()
		{
			DicomAssociation association = new DicomAssociation("testCalling", "testCalled");
			association.ExtendedNegotiations.Add(
				new DicomExtendedNegotiation(DicomUID.StudyRootQueryRetrieveInformationModelFIND,
				new RootQueryRetrieveInfoFind(1, 1, 1, 1, null)));

			AAssociateRQ rq = new AAssociateRQ(association);

			RawPDU writePdu = rq.Write();

			RawPDU readPdu;
			using (MemoryStream stream = new MemoryStream())
			{
				writePdu.WritePDU(stream);

				int length = (int)stream.Length;
				byte[] buffer = new byte[length];
				stream.Seek(0, SeekOrigin.Begin);
				stream.Read(buffer, 0, length);
				readPdu = new RawPDU(buffer);
			}

			DicomAssociation testAssociation = new DicomAssociation();
			AAssociateRQ rq2 = new AAssociateRQ(testAssociation);
			rq2.Read(readPdu);

			Assert.True(testAssociation.ExtendedNegotiations.Count == 1);
			Assert.True(testAssociation.ExtendedNegotiations[0].SopClassUid == DicomUID.StudyRootQueryRetrieveInformationModelFIND);

			RootQueryRetrieveInfoFind info = testAssociation.ExtendedNegotiations[0].SubItem as RootQueryRetrieveInfoFind;
			Assert.True(null != info);
			Assert.True((1 == info.DateTimeMatching)
				&& (1 == info.FuzzySemanticMatching)
				&& (1 == info.RelationalQueries)
				&& (1 == info.TimezoneQueryAdjustment)
				&& (false == info.EnhancedMultiFrameImageConversion.HasValue));
		}
	}
}