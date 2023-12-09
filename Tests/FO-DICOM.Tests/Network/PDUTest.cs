// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using FellowOakDicom.IO;
using Xunit;
using System.Threading.Tasks;
using FellowOakDicom.Memory;

namespace FellowOakDicom.Tests.Network
{
    [Collection(TestCollections.Network)]
    public class PDUTest
    {
        private readonly ArrayPoolMemoryProvider _memoryProvider;

        public PDUTest()
        {
            _memoryProvider = new ArrayPoolMemoryProvider();
        }

        #region Unit tests

        [Fact]
        public async Task Write_AeWithNonAsciiCharacters_ShouldBeAsciified()
        {
            var notExpected = "GÖTEBORG";
            var request = new AAssociateRQ(new DicomAssociation("MALMÖ", notExpected), _memoryProvider);

            using var ms = new MemoryStream();
            await request.WriteAsync(ms, CancellationToken.None);
            var readPdu = new RawPDU(ms, _memoryProvider);

            readPdu.Reset();
            readPdu.SkipBytes("Unknown", 10);
            var actual = readPdu.ReadString("Called AE", 16);

            Assert.NotEqual(notExpected, actual);
        }

        [Fact]
        public void Save_ToNonExistingDirectory_Succeeds()
        {
            var path = TestData.Resolve("PDU Test");
            var name = Path.Combine(path, "assoc.pdu");
            if (Directory.Exists(path)) Directory.Delete(path, true);

            var pdu = new RawPDU(RawPduType.A_ASSOCIATE_RQ, _memoryProvider);
            pdu.Save(new FileReference(name));

            Assert.True(File.Exists(name));
        }

        [Theory]
        [MemberData(nameof(ExtendedNegotiationTestData))]
        public async Task AssociateRQ_WriteRead_ExpectedExtendedNegotiation(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo, DicomUID commonServiceClass, DicomUID[] relatedSopClasses)
        {
            var association = new DicomAssociation("testCalling", "testCalled");
            association.ExtendedNegotiations.Add(sopClassUid, applicationInfo, commonServiceClass, relatedSopClasses);

            var rq = new AAssociateRQ(association, _memoryProvider);
            using var ms = new MemoryStream();
            await rq.WriteAsync(ms, CancellationToken.None);
            var readPdu = new RawPDU(ms, _memoryProvider);

            var testAssociation = new DicomAssociation();
            var rq2 = new AAssociateRQ(testAssociation, _memoryProvider);
            rq2.Read(readPdu);

            Assert.Single(testAssociation.ExtendedNegotiations);
            var negotiation = testAssociation.ExtendedNegotiations.First();
            Assert.Equal(sopClassUid, negotiation.SopClassUid);
            Assert.Equal(applicationInfo, negotiation.RequestedApplicationInfo);
            Assert.Equal(commonServiceClass, negotiation.ServiceClassUid);
            Assert.Equal(relatedSopClasses, negotiation.RelatedGeneralSopClasses);
            Assert.Null(negotiation.AcceptedApplicationInfo);
        }


        [Theory]
        [MemberData(nameof(ExtendedNegotiationTestData))]
        public async Task AssociateAC_WriteRead_ExpectedExtendedNegotiation(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo, DicomUID commonServiceClass, DicomUID[] relatedSopClasses)
        {
            var inAssociation = new DicomAssociation("testCalling", "testCalled");
            inAssociation.ExtendedNegotiations.Add(sopClassUid, applicationInfo, commonServiceClass, relatedSopClasses);
            var acceptedApplicationInfo = new DicomServiceApplicationInfo(applicationInfo.GetValues());
            acceptedApplicationInfo.AddOrUpdate(1, 10);
            inAssociation.ExtendedNegotiations.AcceptApplicationInfo(sopClassUid, acceptedApplicationInfo);

            var ac = new AAssociateAC(inAssociation, _memoryProvider);
            using var ms = new MemoryStream();
            await ac.WriteAsync(ms, CancellationToken.None);

            var readPdu = new RawPDU(ms, _memoryProvider);

            var outAssociation = new DicomAssociation();
            outAssociation.ExtendedNegotiations.Add(sopClassUid, applicationInfo);
            var ac2 = new AAssociateAC(outAssociation, _memoryProvider);
            ac2.Read(readPdu);

            Assert.Single(outAssociation.ExtendedNegotiations);
            var negotiation = outAssociation.ExtendedNegotiations.First();
            Assert.Equal(sopClassUid, negotiation.SopClassUid);
            Assert.Equal(applicationInfo, negotiation.RequestedApplicationInfo);
            Assert.Equal(acceptedApplicationInfo, negotiation.AcceptedApplicationInfo);
            Assert.Null(negotiation.ServiceClassUid);
            Assert.Empty(negotiation.RelatedGeneralSopClasses);
        }

        [Theory]
        [MemberData(nameof(RawPDUTestData))]
        public void AssociateRJ_Read_ReasonGivenByContext(byte[] buffer, AAssociateRJ _, string expected)
        {
            using var raw = new RawPDU(buffer, _memoryProvider);
            var reject = new AAssociateRJ(_memoryProvider);
            reject.Read(raw);
            var actual = reject.Reason.ToString();

            Assert.Equal(expected, actual);
            Assert.NotNull(_);
        }

        [Theory]
        [MemberData(nameof(RawPDUTestData))]
        public async Task AssociateRJ_Write_BytesCorrectlyWritten(byte[] expected, AAssociateRJ reject, string _)
        {
            using var rawMs = new MemoryStream();
            await reject.WriteAsync(rawMs, CancellationToken.None);
            var actual = rawMs.ToArray();

            Assert.Equal(expected, actual);
            Assert.NotNull(_);
        }

        [Theory]
        [MemberData(nameof(AssociateACTestData))]
        public void AssociateAC_Read_TransferSyntaxIdentifiedIfAccept(byte[] buffer, byte contextId,
            DicomPresentationContextResult result, DicomTransferSyntax syntax)
        {
            var association = new DicomAssociation();
            association.PresentationContexts.Add(
                new DicomPresentationContext(contextId, DicomUID.Verification));

            using var raw = new RawPDU(buffer, _memoryProvider);
            var accept = new AAssociateAC(association, _memoryProvider);
            accept.Read(raw);

            var actual = association.PresentationContexts[contextId];

            Assert.Equal(result, actual.Result);
            Assert.Equal(syntax, actual.AcceptedTransferSyntax);
        }

        [Theory]
        [MemberData(nameof(RawPDUTestData))]
        public async Task AssociateRJ_WriteAsync_BytesCorrectlyWritten(byte[] expected, AAssociateRJ reject, string _)
        {
            using var stream = new MemoryStream();
            await reject.WriteAsync(stream, CancellationToken.None);
            var actual = stream.ToArray();

            Assert.Equal(expected, actual);
            Assert.NotNull(_);
        }

        #endregion

        #region Test data

        public static readonly IEnumerable<object[]> ExtendedNegotiationTestData = new[]
        {
            new object[]
            {
                DicomUID.ProcedureLogStorage,
                new DicomCStoreApplicationInfo(DicomLevelOfSupport.Level2, DicomLevelOfDigitalSignatureSupport.Level3,
                    DicomElementCoercion.AllowCoercion),
                DicomUID.Storage,
                new[] {DicomUID.EnhancedSRStorage}
            },
            new object[]
            {
                DicomUID.StudyRootQueryRetrieveInformationModelFind,
                new DicomCFindApplicationInfo(DicomCFindOption.RelationalQueries | DicomCFindOption.DateTimeMatching),
                null,
                new DicomUID[] {}
            },
        };

        public static readonly IEnumerable<object[]> RawPDUTestData = new[]
        {
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x01},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.NoReasonGiven, new ArrayPoolMemoryProvider()),
                nameof(DicomRejectReason.NoReasonGiven)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x02},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.ApplicationContextNotSupported, new ArrayPoolMemoryProvider()),
                nameof(DicomRejectReason.ApplicationContextNotSupported)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x03},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.CallingAENotRecognized, new ArrayPoolMemoryProvider()),
                nameof(DicomRejectReason.CallingAENotRecognized)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x01, 0x07},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser,
                    DicomRejectReason.CalledAENotRecognized, new ArrayPoolMemoryProvider()),
                nameof(DicomRejectReason.CalledAENotRecognized)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x02, 0x01},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceProviderACSE,
                    DicomRejectReason.NoReasonGiven_, new ArrayPoolMemoryProvider()),
                nameof(DicomRejectReason.NoReasonGiven_)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x01, 0x02, 0x02},
                new AAssociateRJ(DicomRejectResult.Permanent, DicomRejectSource.ServiceProviderACSE,
                    DicomRejectReason.ProtocolVersionNotSupported, new ArrayPoolMemoryProvider()),
                nameof(DicomRejectReason.ProtocolVersionNotSupported)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x02, 0x03, 0x01},
                new AAssociateRJ(DicomRejectResult.Transient, DicomRejectSource.ServiceProviderPresentation,
                    DicomRejectReason.TemporaryCongestion, new ArrayPoolMemoryProvider()),
                nameof(DicomRejectReason.TemporaryCongestion)
            },
            new object[]
            {
                new byte[] {0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x02, 0x03, 0x02},
                new AAssociateRJ(DicomRejectResult.Transient, DicomRejectSource.ServiceProviderPresentation,
                    DicomRejectReason.LocalLimitExceeded, new ArrayPoolMemoryProvider()),
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

        #region Helper functions

        private RawPDU ConvertWriteToReadPdu(RawPDU writePdu)
        {
            using var stream = new MemoryStream();
            writePdu.WritePDU(stream);

            int length = (int)stream.Length;
            byte[] buffer = new byte[length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(buffer, 0, length);
            return new RawPDU(buffer, _memoryProvider);
        }

        #endregion
    }
}
