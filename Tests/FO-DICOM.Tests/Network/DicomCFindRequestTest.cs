// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

// These tests cover some obsolete methods or properties
#pragma warning disable CS0618

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class DicomCFindRequestTest
    {
        #region Unit Tests

        [Theory, ClassData(typeof(AffectedSopClassesGenerator))]
        public void Constructor_AffectedSopClassUid_ThrowWhenNotSupported(DicomUID affectedSopClassUid, bool throws)
        {
            var exception = Record.Exception(() => new DicomCFindRequest(affectedSopClassUid));
            Assert.Equal(throws, exception != null);
        }

        [Theory, ClassData(typeof(AffectedSopClassesGenerator))]
        public void Constructor_AffectedSopClassUid_Level_ThrowWhenNotSupported(DicomUID affectedSopClassUid, bool throws)
        {
            var exception = Record.Exception(() => new DicomCFindRequest(affectedSopClassUid, DicomPriority.High));
            Assert.Equal(throws, exception != null);
        }

        [Fact]
        public void Constructor_ParamatersAreSet()
        {
            var cfind = new DicomCFindRequest(DicomUID.UnifiedProcedureStepEvent, DicomQueryRetrieveLevel.NotApplicable, DicomPriority.High);
            Assert.Equal(DicomPriority.High, cfind.Priority);
            Assert.Equal(DicomQueryRetrieveLevel.NotApplicable, cfind.Level);
            Assert.Equal(DicomUID.UnifiedProcedureStepEvent, cfind.SOPClassUID);
        }

        [Theory, MemberData(nameof(InstancesLevels))]
        public void Level_Getter_ReturnsCorrectQueryRetrieveLevel(DicomCFindRequest request, DicomQueryRetrieveLevel expected)
        {
            var actual = request.Level;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_CreatesPatientRootQuery()
        {
            var query = new DicomCFindRequest(DicomUID.PatientRootQueryRetrieveInformationModelFind, DicomQueryRetrieveLevel.Patient);
            Assert.Equal(DicomQueryRetrieveLevel.Patient, query.Level);
            Assert.Equal(DicomUID.PatientRootQueryRetrieveInformationModelFind, query.SOPClassUID);
        }

        [Fact]
        public void Constructor_CreatesStudyRootQuery()
        {
            var query = new DicomCFindRequest(DicomUID.StudyRootQueryRetrieveInformationModelFind, DicomQueryRetrieveLevel.Study);
            Assert.Equal(DicomQueryRetrieveLevel.Study, query.Level);
            Assert.Equal(DicomUID.StudyRootQueryRetrieveInformationModelFind, query.SOPClassUID);
        }

        [Fact]
        public void CreateQueryWithInvalidUID()
        {
            var invalidStudyUID = "1.2.0004";
            var e = Record.Exception(() =>
            {
                var request = DicomCFindRequest.CreateSeriesQuery(invalidStudyUID);
                Assert.Equal(invalidStudyUID, request.Dataset.GetSingleValue<string>(DicomTag.StudyInstanceUID));
            });
            Assert.Null(e);
        }

        [Fact]
        public void AddInvalidUIDToQuery()
        {
            var invalidStudyUID = "1.2.0004";
            var e = Record.Exception(() =>
            {
                var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Study);
                request.Dataset.AddOrUpdate(DicomTag.StudyInstanceUID, invalidStudyUID);
                Assert.Equal(invalidStudyUID, request.Dataset.GetSingleValue<string>(DicomTag.StudyInstanceUID));
            });
            Assert.Null(e);
        }

        [Fact]
        public void AddSeveralUIDsToQuery()
        {
            var e = Record.Exception(() =>
            {
                var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Series);
                request.Dataset.Add(DicomTag.SeriesInstanceUID, "1.2.3\\3.4.5");
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);

            e = Record.Exception(() =>
            {
                var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Series);
                request.Dataset.Add(DicomTag.SeriesInstanceUID, "1.2.3", "2.3.4");
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);

            e = Record.Exception(() =>
            {
                var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Series);
                request.Dataset.Add(new DicomUniqueIdentifier(DicomTag.SeriesInstanceUID, "1.2.3", "3.4.5"));
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);
        }

        #endregion

        #region Support Data

        private class AffectedSopClassesGenerator : IEnumerable<object[]>
        {
            private readonly IEnumerable<object[]> _testData = new[]
            {
                new object[] { new DicomUID("1.2.3.4", "TestSopClass", DicomUidType.SOPClass, false), false },
                new object[] { new DicomUID("1.2.3.4", "TestSopClass", DicomUidType.SOPClass, true), false },
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                var uidTypes = Enum.GetValues(typeof(DicomUidType)).Cast<DicomUidType>();
                var testData = uidTypes
                    .Where(t => t != DicomUidType.SOPClass)
                    .Select(t => new object[] { new DicomUID("1.2.3.4", "NonSopClassUid", t, false), true });
                return _testData.Concat(testData).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public static readonly IEnumerable<object[]> InstancesLevels = new[]
        {
            new object[] { DicomCFindRequest.CreatePatientQuery("Doe^John"), DicomQueryRetrieveLevel.Patient },
            new object[] { DicomCFindRequest.CreateStudyQuery("Doe^John"), DicomQueryRetrieveLevel.Study },
            new object[] { DicomCFindRequest.CreateSeriesQuery("1.2.3"), DicomQueryRetrieveLevel.Series },
            new object[] { DicomCFindRequest.CreateImageQuery("1.2.3", "2.3.4"), DicomQueryRetrieveLevel.Image },
            new object[] { DicomCFindRequest.CreateWorklistQuery(), DicomQueryRetrieveLevel.NotApplicable },
            new object[] { new DicomCFindRequest(DicomQueryRetrieveLevel.Patient), DicomQueryRetrieveLevel.Patient },
            new object[] { new DicomCFindRequest(DicomQueryRetrieveLevel.Study), DicomQueryRetrieveLevel.Study },
            new object[] { new DicomCFindRequest(DicomQueryRetrieveLevel.Series), DicomQueryRetrieveLevel.Series },
            new object[] { new DicomCFindRequest(DicomQueryRetrieveLevel.Image), DicomQueryRetrieveLevel.Image },
            new object[] { new DicomCFindRequest(DicomQueryRetrieveLevel.Worklist), DicomQueryRetrieveLevel.NotApplicable },
            new object[] { new DicomCFindRequest(DicomUID.ModalityWorklistInformationModelFind), DicomQueryRetrieveLevel.NotApplicable },
            new object[] { new DicomCFindRequest(DicomUID.UnifiedProcedureStepPull), DicomQueryRetrieveLevel.NotApplicable },
            new object[] { new DicomCFindRequest(DicomUID.UnifiedProcedureStepWatch), DicomQueryRetrieveLevel.NotApplicable }
        };

        #endregion
    }
}
