// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using Xunit;

    public class DicomStatusTest
    {
        #region Unit tests

        private DicomStatus UPSIsAlreadyCompleted = new DicomStatus(
                                                      "B306",
                                                      DicomState.Warning,
                                                      "The UPS is already in the requested state of COMPLETED");

        private DicomStatus SOPInstanceDoesNotExist = new DicomStatus(
                                                        "C307",
                                                        DicomState.Failure,
                                                        "Specified SOP Instance UID does not exist or is not a UPS Instance managed by this SCP");

        [Fact]
        public void EqualsMethod_StatusWithDifferentCodes_AreNotEqual()
        {
            Assert.NotEqual(UPSIsAlreadyCompleted, SOPInstanceDoesNotExist);
        }

        [Fact]
        public void EqualsMethod_RandomFailure_EqualsStorageCannotUnderstand()
        {
            Assert.Equal(SOPInstanceDoesNotExist, DicomStatus.StorageCannotUnderstand);
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
            var statusTest = DicomStatus.Lookup(UPSIsAlreadyCompleted.Code);
            // Code B306 is not in the known list. So the DicomStatus.Lookup shall return
            // the original code, but a status and Description that matches best one of
            // the known states.
            Assert.Equal(UPSIsAlreadyCompleted.State, statusTest.State);
            Assert.Equal(UPSIsAlreadyCompleted.Code, statusTest.Code);
            Assert.NotEqual(UPSIsAlreadyCompleted.Description, statusTest.Description);
        }

        [Fact]
        public void AddKnownDicomStatuses_WithFailure_IsLookedupCorrectly()
        {
            try
            {
                DicomStatus.AddKnownDicomStatuses(new[] { UPSIsAlreadyCompleted, SOPInstanceDoesNotExist });
                var upsCompleteTest = DicomStatus.Lookup(UPSIsAlreadyCompleted.Code);
                Assert.Equal(UPSIsAlreadyCompleted, upsCompleteTest);
                Assert.Equal(UPSIsAlreadyCompleted.Code, upsCompleteTest.Code);
                Assert.Equal(UPSIsAlreadyCompleted.Description, upsCompleteTest.Description);
                var sopDoesNotExistTest = DicomStatus.Lookup(UPSIsAlreadyCompleted.Code);
                Assert.Equal(UPSIsAlreadyCompleted, sopDoesNotExistTest);
                Assert.Equal(UPSIsAlreadyCompleted.Code, sopDoesNotExistTest.Code);
                Assert.Equal(UPSIsAlreadyCompleted.Description, sopDoesNotExistTest.Description);
            }
            finally
            {
                DicomStatus.ResetEntries();
            }
        }
        #endregion
    }
}
