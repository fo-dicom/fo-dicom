// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dicom.Network
{
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
            var cfind = new DicomCFindRequest(DicomUID.UnifiedProcedureStepEventSOPClass, DicomQueryRetrieveLevel.NotApplicable, DicomPriority.High);
            Assert.Equal(cfind.Priority, DicomPriority.High);
            Assert.Equal(cfind.Level, DicomQueryRetrieveLevel.NotApplicable);
            Assert.Equal(cfind.SOPClassUID, DicomUID.UnifiedProcedureStepEventSOPClass);
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
            var query = new DicomCFindRequest(DicomUID.PatientRootQueryRetrieveInformationModelFIND, DicomQueryRetrieveLevel.Patient);
            Assert.Equal(DicomQueryRetrieveLevel.Patient, query.Level);
            Assert.Equal(DicomUID.PatientRootQueryRetrieveInformationModelFIND, query.SOPClassUID);
        }

        [Fact]
        public void Constructor_CreatesStudyRootQuery()
        {
            var query = new DicomCFindRequest(DicomUID.StudyRootQueryRetrieveInformationModelFIND, DicomQueryRetrieveLevel.Study);
            Assert.Equal(DicomQueryRetrieveLevel.Study, query.Level);
            Assert.Equal(DicomUID.StudyRootQueryRetrieveInformationModelFIND, query.SOPClassUID);
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
            new object[] { new DicomCFindRequest(DicomUID.ModalityWorklistInformationModelFIND), DicomQueryRetrieveLevel.NotApplicable },
            new object[] { new DicomCFindRequest(DicomUID.UnifiedProcedureStepPullSOPClass), DicomQueryRetrieveLevel.NotApplicable },
            new object[] { new DicomCFindRequest(DicomUID.UnifiedProcedureStepWatchSOPClass), DicomQueryRetrieveLevel.NotApplicable }
        };

        #endregion
    }
}
