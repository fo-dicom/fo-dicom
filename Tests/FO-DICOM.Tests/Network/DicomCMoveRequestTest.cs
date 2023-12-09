// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class DicomCMoveRequestTest
    {
        #region Unit Tests

        [Fact]
        public void Level_GetterOnRequestCreatedFromCommand_Throws()
        {
            var request = new DicomCMoveRequest(new DicomDataset());
            var exception = Record.Exception(() => request.Level);
            Assert.NotNull(exception);
        }

        [Theory, MemberData(nameof(InstancesLevels))]
        public void Level_Getter_ReturnsCorrectQueryRetrieveLevel(DicomCMoveRequest request, DicomQueryRetrieveLevel expected)
        {
            var actual = request.Level;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateQueryWithInvalidUID()
        {
            var invalidStudyUID = "1.2.0004";
            var e = Record.Exception(() =>
            {
                var request = new DicomCMoveRequest("DestinationAE", invalidStudyUID);
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
                var request = new DicomCMoveRequest("DestinationAE", invalidStudyUID);
                request.Dataset.AddOrUpdate(DicomTag.SeriesInstanceUID, invalidStudyUID);
                Assert.Equal(invalidStudyUID, request.Dataset.GetSingleValue<string>(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);
        }


        [Fact]
        public void AddSeveralUIDsToQuery()
        {
            var e = Record.Exception(() =>
            {
                var request = new DicomCMoveRequest("DestinationAE", "1.2.3.456");
                request.Dataset.Add(DicomTag.SeriesInstanceUID, "1.2.3\\3.4.5");
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);

            e = Record.Exception(() =>
            {
                var request = new DicomCMoveRequest("DestinationAE", "1.2.3.456");
                request.Dataset.Add(DicomTag.SeriesInstanceUID, "1.2.3", "2.3.4");
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);

            e = Record.Exception(() =>
            {
                var request = new DicomCMoveRequest("DestinationAE", "1.2.3.456");
                request.Dataset.Add(new DicomUniqueIdentifier(DicomTag.SeriesInstanceUID, "1.2.3", "3.4.5"));
                Assert.Equal(2, request.Dataset.GetValueCount(DicomTag.SeriesInstanceUID));
            });
            Assert.Null(e);
        }

        #endregion

        #region Support Data

        public static readonly IEnumerable<object[]> InstancesLevels = new[]
        {
            new object[] { new DicomCMoveRequest("SOMESCP", "2.3.4"), DicomQueryRetrieveLevel.Study },
            new object[] { new DicomCMoveRequest("SOMESCP", "2.3.4", "3.4.5"), DicomQueryRetrieveLevel.Series },
            new object[] { new DicomCMoveRequest("SOMESCP", "2.3.4", "3.4.5", "4.5.6"), DicomQueryRetrieveLevel.Image },
        };

        #endregion
    }
}
