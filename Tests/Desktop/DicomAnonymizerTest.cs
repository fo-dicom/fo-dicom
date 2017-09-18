// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom
{
    [Collection("General")]
    public class DicomAnonymizerTest
    {
        #region Unit tests

        [Fact]
        public void AnonymizeInPlace_Dataset_PatientDataEmpty()
        {
#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync(@"Data/CT1_J2KI").Result.Dataset;
#else
            var dataset = DicomFile.Open(@"./Test Data/CT1_J2KI").Dataset;
#endif
            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            Assert.Equal(0, dataset.Get<string[]>(DicomTag.PatientName).Length);
            Assert.Equal(0, dataset.Get<string[]>(DicomTag.PatientID).Length);
            Assert.Equal(0, dataset.Get<string[]>(DicomTag.PatientSex).Length);
        }

        [Fact]
        public void AnonymizeInPlace_File_SopInstanceUidTransferredToMetaInfo()
        {
#if NETFX_CORE
            var file = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync(@"Data/CT1_J2KI").Result;
#else
            var file = DicomFile.Open(@"./Test Data/CT1_J2KI");
#endif
            var old = file.Dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(file);

            var expected = file.Dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
            var actual = file.FileMetaInfo.MediaStorageSOPInstanceUID;
            Assert.NotEqual(expected, old);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AnonymizeInPlace_File_ImplementationVersionNameMaintained()
        {
#if NETFX_CORE
            var file = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync(@"Data/CT1_J2KI").Result;
#else
            var file = DicomFile.Open(@"./Test Data/CT1_J2KI");
#endif
            var expected = file.FileMetaInfo.ImplementationVersionName;
            Assert.False(string.IsNullOrEmpty(expected));

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(file);

            var actual = file.FileMetaInfo.ImplementationVersionName;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Anonymize_Dataset_OriginalDatasetNotModified()
        {
#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync(@"Data/CT-MONO2-16-ankle").Result.Dataset;
#else
            var dataset = DicomFile.Open(@"./Test Data/CT-MONO2-16-ankle").Dataset;
#endif
            var expected = dataset.Get<DicomUID>(DicomTag.StudyInstanceUID);

            var anonymizer = new DicomAnonymizer();
            var newDataset = anonymizer.Anonymize(dataset);

            var actual = dataset.Get<DicomUID>(DicomTag.StudyInstanceUID);
            var actualNew = newDataset.Get<DicomUID>(DicomTag.StudyInstanceUID);

            Assert.Equal(expected, actual);
            Assert.NotEqual(expected, actualNew);
        }

        [Fact]
        public void Anonymize_UsePredefinedPatientNameAndId_ShouldBeSetInAnonymizedDataset()
        {
            const string fileName = "CT1_J2KI";
#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result.Dataset;
#else
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
#endif
            const string expectedName = "fo-dicom";
            const string expectedId = "GH-575";

            var anonymizer = new DicomAnonymizer();
            anonymizer.Profile.PatientName = expectedName;
            anonymizer.Profile.PatientID = expectedId;

            var newDataset = anonymizer.Anonymize(dataset);

            var actualName = newDataset.Get<string>(DicomTag.PatientName);
            var actualId = newDataset.Get<string>(DicomTag.PatientID);

            Assert.Equal(expectedName, actualName);
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void AnonymizeInPlace_StudyDate_ShouldBeEmpty()
        {
            const string fileName = "CT1_J2KI";
            var tag = DicomTag.StudyDate;

#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result.Dataset;
#else
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
#endif
            Assert.True(dataset.Get<string>(tag).Length > 0);

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            var expected = new string[0];
            var actual = dataset.Get<string[]>(tag);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AnonymizeInPlace_SeriesDate_ShouldBeRemoved()
        {
            const string fileName = "CT1_J2KI";
            var tag = DicomTag.SeriesDate;

#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result.Dataset;
#else
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
#endif
            Assert.True(dataset.Get<string>(tag).Length > 0);

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            var contains = dataset.Contains(tag);
            Assert.False(contains);
        }

        [Fact]
        public void AnonymizeInPlace_RemovableSequence_ShouldBeRemoved()
        {
            const string fileName = "GH610.dcm";
            var tag = DicomTag.OriginalAttributesSequence;

#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result.Dataset;
#else
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
#endif
            Assert.True(dataset.Contains(tag));

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            Assert.False(dataset.Contains(tag));
        }

        #endregion
    }
}
