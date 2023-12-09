// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class DicomExtendedNegotiationTest
    {
        #region Unit tests

        [Fact]
        public void AcceptApplicationInfo_WhenNotRequested_ApplicationInfoIsNotSet()
        {
            var negotiation = new DicomExtendedNegotiation(DicomUID.Verification, (DicomServiceApplicationInfo)null);
            negotiation.AcceptApplicationInfo(new DicomCFindApplicationInfo(DicomCFindOption.DateTimeMatching | DicomCFindOption.FuzzySemanticMatching));
            Assert.Null(negotiation.RequestedApplicationInfo);
            Assert.Null(negotiation.AcceptedApplicationInfo);
        }

        [Theory]
        [MemberData(nameof(RawBytesTestData))]
        public void AcceptApplicationInfo_WhenRequested_ApplicationInfoIsSetAsExpected(byte[] requested, byte[] accepted, byte[] expected)
        {
            var requestedApplicationInfo = new DicomServiceApplicationInfo(requested);
            var acceptedApplicationInfo = new DicomServiceApplicationInfo(accepted);
            var expectedApplicationInfo = new DicomServiceApplicationInfo(expected);

            var negotiation = new DicomExtendedNegotiation(DicomUID.Verification, requestedApplicationInfo);
            negotiation.AcceptApplicationInfo(acceptedApplicationInfo);

            Assert.Equal(requestedApplicationInfo, negotiation.RequestedApplicationInfo);
            Assert.Equal(expectedApplicationInfo, negotiation.AcceptedApplicationInfo);
        }

        [Theory]
        [MemberData(nameof(OrderOfBytesTestData))]
        public void Constructor_WhenApplicationInfoIsSet_OrderOfBytesIsAsExpected(DicomServiceApplicationInfo appInfo, byte[] expectedByteOrder)
        {
            var sopClass = DicomUID.PatientRootQueryRetrieveInformationModelFind;
            var negotiation = new DicomExtendedNegotiation(sopClass, appInfo);

            Assert.Equal(expectedByteOrder, negotiation.RequestedApplicationInfo.GetValues());
            foreach (var neg in negotiation.RequestedApplicationInfo)
            {
                Assert.Equal(expectedByteOrder[neg.Key - 1], neg.Value);
            }
        }

        [Fact]
        public void Constructor_WithCreatedApplicationInfo_ApplicationInfoTypeIsAsExpected()
        {
            var sopClass = DicomUID.PatientRootQueryRetrieveInformationModelFind;
            var appInfoType = typeof(DicomCFindApplicationInfo);

            var appInfo = DicomServiceApplicationInfo.Create(sopClass, new byte[] { 1 });
            var negotiation = new DicomExtendedNegotiation(sopClass, appInfo);

            Assert.Equal(sopClass, negotiation.SopClassUid);
            Assert.Equal(appInfoType, negotiation.RequestedApplicationInfo.GetType());
        }

        [Fact]
        public void Constructor_WithDelegateCreatedApplicationInfo_ApplicationInfoTypeIsAsExpected()
        {
            var sopClass = DicomUID.BasicAnnotationBox;
            var appInfoType = typeof(DicomCFindApplicationInfo);

            DicomServiceApplicationInfo.OnCreateApplicationInfo = (sop, info) =>
            {
                if (sop == sopClass)
                    return Activator.CreateInstance(appInfoType, info) as DicomServiceApplicationInfo;

                return null;
            };

            var appInfo = DicomServiceApplicationInfo.Create(sopClass, new byte[] { 1 });
            var negotiation = new DicomExtendedNegotiation(sopClass, appInfo);

            Assert.Equal(sopClass, negotiation.SopClassUid);
            Assert.Equal(appInfoType, negotiation.RequestedApplicationInfo.GetType());
            DicomServiceApplicationInfo.OnCreateApplicationInfo = null;
        }

        #endregion

        #region Support Data

        public static readonly IEnumerable<object[]> OrderOfBytesTestData = new[]
        {
            new object[]
            {
                new DicomCFindApplicationInfo(DicomCFindOption.RelationalQueries | DicomCFindOption.FuzzySemanticMatching |DicomCFindOption.EnhancedMultiFrameImageConversion),
                new byte[] { 1, 0, 1, 0, 1 }
            },
            new object[]
            {
                new DicomCMoveApplicationInfo(DicomCMoveOption.RelationalRetrieval),
                new byte[] { 1, 0 }
            },
            new object[]
            {
                new DicomCGetApplicationInfo(DicomCGetOption.EnhancedMultiFrameImageConversion),
                new byte[] { 0, 1 }
            },
            new object[]
            {
                new DicomCStoreApplicationInfo(DicomLevelOfSupport.Level0, DicomLevelOfDigitalSignatureSupport.Level1, DicomElementCoercion.NotApplicable),
                new byte[] { 0, 0, 1, 0, 2 }
            },
            new object[]
            {
                new DicomServiceApplicationInfo(new byte[] { 50, 30, 40, 10, 20 }),
                new byte[] { 50, 30, 40, 10, 20 }
            },
        };

        public static readonly IEnumerable<object[]> RawBytesTestData = new[]
        {
            new object[]
            {
                new byte[]{ 0, 0, 1 },
                new byte[]{ 1, 0, 1 },
                new byte[]{ 1, 0, 1 }
            },
            new object[]
            {
                new byte[]{ 0, 0, 1 },
                new byte[]{ },
                new byte[]{ }
            },
            new object[]
            {
                new byte[]{ 1, 0, 1 },
                new byte[]{ 1 },
                new byte[]{ 1 }
            },
            new object[]
            {
                new byte[]{ 0, 0, 1, 0 },
                new byte[]{ 1, 0, 1, 1, 1, 0 },
                new byte[]{ 1, 0, 1, 1 }
            },
            new object[]
            {
                new byte[]{ 0, 0 },
                new byte[]{ 0, 0 },
                new byte[]{ 0, 0 }
            },
        };

        #endregion
    }
}
