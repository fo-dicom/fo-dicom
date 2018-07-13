// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Threading;

using Xunit;

namespace Dicom.Network
{
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
