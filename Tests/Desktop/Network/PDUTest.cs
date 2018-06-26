// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.IO;

using Xunit;

namespace Dicom.Network
{
    [Collection("Network")]
    public class PDUTest
    {
        #region Unit tests

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
                new DicomExtendedNegotiation(
                    DicomUID.StudyRootQueryRetrieveInformationModelFIND,
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
            Assert.True(
                testAssociation.ExtendedNegotiations[0].SopClassUid
                == DicomUID.StudyRootQueryRetrieveInformationModelFIND);

            RootQueryRetrieveInfoFind info =
                testAssociation.ExtendedNegotiations[0].SubItem as RootQueryRetrieveInfoFind;
            Assert.True(null != info);
            Assert.True(
                (1 == info.DateTimeMatching) && (1 == info.FuzzySemanticMatching) && (1 == info.RelationalQueries)
                && (1 == info.TimezoneQueryAdjustment) && (false == info.EnhancedMultiFrameImageConversion.HasValue));
        }

        [Theory]
        [MemberData(nameof(RawPDUTestData))]
        public void AssociateRJ_Read_ReasonGivenByContext(byte[] buffer, AAssociateRJ dummy, string expected)
        {
            using (var raw = new RawPDU(buffer))
            {
                var reject = new AAssociateRJ();
                reject.Read(raw);
                var actual = reject.Reason.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [MemberData(nameof(RawPDUTestData))]
        public void AssociateRJ_Write_BytesCorrectlyWritten(byte[] expected, AAssociateRJ reject, string dummy)
        {
            using (var raw = reject.Write())
            using (var stream = new MemoryStream())
            {
                raw.WritePDU(stream);
                var actual = stream.ToArray();

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [MemberData(nameof(AssociateACTestData))]
        public void AssociateAC_Read_TransferSyntaxIdentifiedIfAccept(byte[] buffer, byte contextId,
            DicomPresentationContextResult result, DicomTransferSyntax syntax)
        {
            var association = new DicomAssociation();
            association.PresentationContexts.Add(
                new DicomPresentationContext(contextId, DicomUID.Verification));

            using (var raw = new RawPDU(buffer))
            {
                var accept = new AAssociateAC(association);
                accept.Read(raw);

                var actual = association.PresentationContexts[contextId];

                Assert.Equal(result, actual.Result);
                Assert.Equal(syntax, actual.AcceptedTransferSyntax);
            }
        }

        #endregion

        #region Test data

        public static readonly IEnumerable<object[]> RawPDUTestData = new[]
        {
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x01},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.NoReasonGiven),
                nameof(DicomRejectReason.NoReasonGiven)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x02},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.ApplicationContextNotSupported),
                nameof(DicomRejectReason.ApplicationContextNotSupported)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x03},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.CallingAENotRecognized),
                nameof(DicomRejectReason.CallingAENotRecognized)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x07},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.CalledAENotRecognized),
                nameof(DicomRejectReason.CalledAENotRecognized)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x02, 0x01},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceProviderACSE,
                    DicomRejectReason.NoReasonGiven_),
                nameof(DicomRejectReason.NoReasonGiven_)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x02, 0x02},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceProviderACSE,
                    DicomRejectReason.ProtocolVersionNotSupported),
                nameof(DicomRejectReason.ProtocolVersionNotSupported)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x02, 0x03, 0x01},
                new AAssociateRJ(DicomRejectResult.Transient, DicomRejectSource.ServiceProviderPresentation,
                    DicomRejectReason.TemporaryCongestion),
                nameof(DicomRejectReason.TemporaryCongestion)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x02, 0x03, 0x02},
                new AAssociateRJ(DicomRejectResult.Transient, DicomRejectSource.ServiceProviderPresentation,
                    DicomRejectReason.LocalLimitExceeded),
                nameof(DicomRejectReason.LocalLimitExceeded)
            }
        };

        public static readonly IEnumerable<object[]> AssociateACTestData = new[]
        {
            new object[]
            {
                new byte[]
                {
                    0x03, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x21, 0x00, 0x00, 0x1e,
                    0x01, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x16, 0x31, 0x2e, 0x32, 0x2e, 0x38, 0x34, 0x30, 0x2e,
                    0x31, 0x30, 0x30, 0x30, 0x38, 0x2e, 0x31, 0x2e, 0x32, 0x2e, 0x34, 0x2e, 0x35, 0x30
                }, (byte)1, DicomPresentationContextResult.Accept, DicomTransferSyntax.JPEGProcess1
            },
            new object[]
            {
                new byte[]
                {
                    0x03, 0x00, 0x00, 0x00, 0x00, 0x54, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x21, 0x00, 0x00, 0x0c,
                    0x01, 0x00, 0x03, 0x00, 0x40, 0x00, 0x00, 0x04, 0x31, 0x32, 0x33, 0x34
                }, (byte)1, DicomPresentationContextResult.RejectAbstractSyntaxNotSupported, null
            }
        };

        #endregion
    }
}
