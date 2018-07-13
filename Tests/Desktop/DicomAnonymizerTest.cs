// Copyright (c) 2012-2018 fo-dicom contributors.
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

            Assert.Empty(dataset.GetValues<string>(DicomTag.PatientName));
            Assert.Empty(dataset.GetValues<string>(DicomTag.PatientID));
            Assert.Empty(dataset.GetValues<string>(DicomTag.PatientSex));
        }

        [Fact]
        public void AnonymizeInPlace_File_SopInstanceUidTransferredToMetaInfo()
        {
#if NETFX_CORE
            var file = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync(@"Data/CT1_J2KI").Result;
#else
            var file = DicomFile.Open(@"./Test Data/CT1_J2KI");
#endif
            var old = file.Dataset.GetSingleValue<DicomUID>(DicomTag.SOPInstanceUID);
            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(file);

            var expected = file.Dataset.GetSingleValue<DicomUID>(DicomTag.SOPInstanceUID);
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
            var expected = dataset.GetSingleValue<DicomUID>(DicomTag.StudyInstanceUID);

            var anonymizer = new DicomAnonymizer();
            var newDataset = anonymizer.Anonymize(dataset);

            var actual = dataset.GetSingleValue<DicomUID>(DicomTag.StudyInstanceUID);
            var actualNew = newDataset.GetSingleValue<DicomUID>(DicomTag.StudyInstanceUID);

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

            var actualName = newDataset.GetSingleValue<string>(DicomTag.PatientName);
            var actualId = newDataset.GetSingleValue<string>(DicomTag.PatientID);

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
            Assert.True(dataset.GetSingleValue<string>(tag).Length > 0);

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            var expected = new string[0];
            var actual = dataset.GetValues<string>(tag);
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
            Assert.True(dataset.GetSingleValue<string>(tag).Length > 0);

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

        [Fact]
        public void AnonymizeInPlace_ClearableSequence_ShouldBeCleared()
        {
            const string fileName = "GH610.dcm";
            var tag = DicomTag.PersonIdentificationCodeSequence;

#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result.Dataset;
#else
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
#endif
            dataset.Add(new DicomSequence(tag,
                new DicomDataset(new DicomLongString(DicomTag.CodeMeaning, "SOME MEANING"))));

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            Assert.True(dataset.Contains(tag));

            var sequence = dataset.Get<DicomSequence>(tag);
            Assert.Equal(0, sequence.Items.Count);
        }

        #endregion
    }
}
