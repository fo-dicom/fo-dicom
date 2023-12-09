// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class DicomStatusTest
    {
        #region Unit tests

        private readonly DicomStatus _stateUPSIsAlreadyCompleted = new DicomStatus(
                                                      "B306",
                                                      DicomState.Warning,
                                                      "The UPS is already in the requested state of COMPLETED");

        private readonly DicomStatus _stateSOPInstanceDoesNotExist = new DicomStatus(
                                                        "C307",
                                                        DicomState.Failure,
                                                        "Specified SOP Instance UID does not exist or is not a UPS Instance managed by this SCP");

        [Fact]
        public void EqualsMethod_StatusWithDifferentCodes_AreNotEqual()
        {
            Assert.NotEqual(_stateUPSIsAlreadyCompleted, _stateSOPInstanceDoesNotExist);
        }

        [Fact]
        public void EqualsMethod_RandomFailure_EqualsStorageCannotUnderstand()
        {
            Assert.Equal(_stateSOPInstanceDoesNotExist, DicomStatus.StorageCannotUnderstand);
        }

        [Fact]
        public void Lookup_MaskedEntryExists_LooksUpCorrectValue()
        {
            var testStatus = DicomStatus.PrintManagementFilmBoxEmptyPage;
            var status = DicomStatus.Lookup(testStatus.Code);
            Assert.True(status.Code == testStatus.Code);
        }

        [Fact]
        public void Lookup_WithWarning_ReturnsCorrectStatusClass()
        {
            var statusTest = DicomStatus.Lookup(_stateUPSIsAlreadyCompleted.Code);
            // Code B306 is not in the known list. So the DicomStatus.Lookup shall return
            // the original code, but a status and Description that matches best one of
            // the known states.
            Assert.Equal(_stateUPSIsAlreadyCompleted.State, statusTest.State);
            Assert.Equal(_stateUPSIsAlreadyCompleted.Code, statusTest.Code);
            Assert.NotEqual(_stateUPSIsAlreadyCompleted.Description, statusTest.Description);
        }

        [Fact]
        public void AddKnownDicomStatuses_WithFailure_IsLookedupCorrectly()
        {
            try
            {
                DicomStatus.AddKnownDicomStatuses(new[] { _stateUPSIsAlreadyCompleted, _stateSOPInstanceDoesNotExist });
                var upsCompleteTest = DicomStatus.Lookup(_stateUPSIsAlreadyCompleted.Code);
                Assert.Equal(_stateUPSIsAlreadyCompleted, upsCompleteTest);
                Assert.Equal(_stateUPSIsAlreadyCompleted.Code, upsCompleteTest.Code);
                Assert.Equal(_stateUPSIsAlreadyCompleted.Description, upsCompleteTest.Description);
                var sopDoesNotExistTest = DicomStatus.Lookup(_stateUPSIsAlreadyCompleted.Code);
                Assert.Equal(_stateUPSIsAlreadyCompleted, sopDoesNotExistTest);
                Assert.Equal(_stateUPSIsAlreadyCompleted.Code, sopDoesNotExistTest.Code);
                Assert.Equal(_stateUPSIsAlreadyCompleted.Description, sopDoesNotExistTest.Description);
            }
            finally
            {
                DicomStatus.ResetEntries();
            }
        }

        [Fact]
        public void NullComparison()
        {
            Assert.True(DicomStatus.Success != null);
            Assert.True(((DicomStatus)null) == null);
            Assert.False(DicomStatus.Success == null);
            Assert.False(((DicomStatus)null) != null);
        }

        [Fact]
        public void HashCodes()
        {
            Assert.Equal(DicomStatus.Success.GetHashCode(), DicomStatus.Success.GetHashCode());
            Assert.Equal(
                new DicomStatus( "DDDD", DicomState.Success, "DESC1").GetHashCode(),
                new DicomStatus( "DDDD", DicomState.Success, "DESC1").GetHashCode()
            );
            Assert.Equal(
                new DicomStatus("DDDD", DicomState.Success, "DESC1", "COMMENT1").GetHashCode(),
                new DicomStatus("DDDD", DicomState.Success, "DESC1", "COMMENT1").GetHashCode()
            );
            Assert.Equal(
                new DicomStatus( DicomStatus.Success, "COMMENT1").GetHashCode(),
                new DicomStatus( DicomStatus.Success, "COMMENT1").GetHashCode()
            );
            Assert.Equal(
                new DicomStatus(1, DicomStatus.Success).GetHashCode(),
                new DicomStatus(1, DicomStatus.Success).GetHashCode()
            );

            Assert.NotEqual(DicomStatus.Success.GetHashCode(), DicomStatus.StorageStorageOutOfResources.GetHashCode());
            Assert.NotEqual(
                new DicomStatus("DDDD", DicomState.Success, "DESC1", "COMMENT1").GetHashCode(),
                new DicomStatus("DDDE", DicomState.Success, "DESC1", "COMMENT1").GetHashCode()
            );
            Assert.NotEqual(
                new DicomStatus("DDDD", DicomState.Success, "DESC1", "COMMENT1").GetHashCode(),
                new DicomStatus("DDDD", DicomState.Cancel, "DESC1", "COMMENT1").GetHashCode()
            );
            Assert.NotEqual(
                new DicomStatus("DDDD", DicomState.Success, "DESC1", "COMMENT1").GetHashCode(),
                new DicomStatus("DDDD", DicomState.Success, "DESC2", "COMMENT1").GetHashCode()
            );
            Assert.NotEqual(
                new DicomStatus("DDDD", DicomState.Success, "DESC1", "COMMENT1").GetHashCode(),
                new DicomStatus("DDDD", DicomState.Success, "DESC2", "COMMENT2").GetHashCode()
            );
            Assert.NotEqual(
                new DicomStatus( DicomStatus.Success, "COMMENT1").GetHashCode(),
                new DicomStatus( DicomStatus.Success, "COMMENT2").GetHashCode()
            );
            Assert.NotEqual(
                new DicomStatus( DicomStatus.Success, "COMMENT1").GetHashCode(),
                new DicomStatus( DicomStatus.Cancel, "COMMENT1").GetHashCode()
            );
            Assert.NotEqual(
                new DicomStatus(0xDDDD, DicomStatus.Success).GetHashCode(),
                new DicomStatus(0xDDDE, DicomStatus.Success).GetHashCode()
            );
            Assert.NotEqual(
                new DicomStatus(0xDDDD, DicomStatus.Success).GetHashCode(),
                new DicomStatus(0xDDDD, DicomStatus.Cancel).GetHashCode()
            );
        }

        #endregion
    }
}
