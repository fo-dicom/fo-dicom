// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

using Xunit;

namespace Dicom.Network
{
    public class DicomCFindRequestTest
    {
        #region Unit Tests

        [Theory, MemberData(nameof(AffectedSopClassUids))]
        public void Constructor_AffectedSopClassUid_ThrowWhenNotSupported(DicomUID affectedSopClassUid, bool throws)
        {
            var exception = Record.Exception(() => new DicomCFindRequest(affectedSopClassUid));
            Assert.Equal(throws, exception != null);
        }

        [Theory, MemberData(nameof(InstancesLevels))]
        public void Level_Getter_ReturnsCorrectQueryRetrieveLevel(DicomCFindRequest request, DicomQueryRetrieveLevel expected)
        {
            var actual = request.Level;
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Support Data

        public static readonly IEnumerable<object[]> AffectedSopClassUids = new[]
        {
            new object[] { DicomUID.PatientRootQueryRetrieveInformationModelFIND, true },
            new object[] { DicomUID.StudyRootQueryRetrieveInformationModelFIND, true },
            new object[] { DicomUID.ModalityWorklistInformationModelFIND, false },
            new object[] { DicomUID.UnifiedProcedureStepPullSOPClass, false },
            new object[] { DicomUID.UnifiedProcedureStepWatchSOPClass, false },
            new object[] { DicomUID.UnifiedProcedureStepPushSOPClass, true }
        };

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
