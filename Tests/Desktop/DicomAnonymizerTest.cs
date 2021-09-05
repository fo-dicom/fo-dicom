﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;

namespace Dicom
{
    using System.Text;

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

        [Fact]
        public void AnonymizeInPlace_SequenceToKeep_NestedDatasetsShouldBeParsed()
        {
            const string fileName = "GH610.dcm";
            var tag1 = DicomTag.ROIContourSequence;
            var tag2 = DicomTag.ContourSequence;
            var tag3 = DicomTag.ContourImageSequence;
            var generatedUid1 = DicomUIDGenerator.GenerateNew();
            var generatedUid2 = DicomUIDGenerator.GenerateNew();

#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result.Dataset;
#else
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
#endif      
            dataset.Add(new DicomSequence(tag1, new DicomDataset(
                new DicomSequence(tag2, new DicomDataset(
                    new DicomSequence(tag3,
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.ReferencedSOPInstanceUID, generatedUid1.UID),
                        new DicomIntegerString(DicomTag.ReferencedFrameNumber, 1)
                        ),
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.ReferencedSOPInstanceUID, generatedUid2.UID),
                        new DicomIntegerString(DicomTag.ReferencedFrameNumber, 2)
                        )
                    ))
                ))
            ));

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            Assert.True(dataset.Contains(tag1));

            var sequence1 = dataset.Get<DicomSequence>(tag1);
            var sequence2 = sequence1.Items[0].Get<DicomSequence>(tag2);
            var sequence3 = sequence2.Items[0].Get<DicomSequence>(tag3);
            Assert.NotEqual(sequence3.Items[0].Get<DicomUID>(DicomTag.ReferencedSOPInstanceUID), sequence3.Items[1].Get<DicomUID>(DicomTag.ReferencedSOPInstanceUID));
            Assert.NotEqual(generatedUid1, sequence3.Items[0].Get<DicomUID>(DicomTag.ReferencedSOPInstanceUID));
            Assert.NotEqual(generatedUid2, sequence3.Items[1].Get<DicomUID>(DicomTag.ReferencedSOPInstanceUID));
            Assert.Equal(1, sequence3.Items[0].Get<int>(DicomTag.ReferencedFrameNumber));
            Assert.Equal(2, sequence3.Items[1].Get<int>(DicomTag.ReferencedFrameNumber));
        }

        [Fact]
        public void AnonymizeInPlace_BasicProfile()
        {
            const string fileName = "CT1_J2KI";

#if NETFX_CORE
            var dataset = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result.Dataset;
#else
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
#endif

            var profile = DicomAnonymizer.SecurityProfile.LoadProfile(null, DicomAnonymizer.SecurityProfileOptions.BasicProfile);
            var anony = new DicomAnonymizer(profile);

            var anonymized = anony.Anonymize(dataset);

            Assert.NotEqual(dataset.GetString(DicomTag.PatientName), anonymized.GetString(DicomTag.PatientName));
            Assert.NotEqual(dataset.GetString(DicomTag.PatientAge), anonymized.GetSingleValueOrDefault(DicomTag.PatientAge, string.Empty));
            Assert.NotEqual(dataset.GetString(DicomTag.PatientID), anonymized.GetSingleValueOrDefault(DicomTag.PatientID, string.Empty));
        }

        [Fact]
        public void Anonymize_PatientName_ShouldUseOriginalDicomEncoding()
        {
            const string fileName = "GH064.dcm";

#if NETFX_CORE
            var originalDicom = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result;
#else
            var originalDicom = DicomFile.Open($"./Test Data/{fileName}");
#endif

            var securityProfile = Dicom.DicomAnonymizer.SecurityProfile.LoadProfile(null, Dicom.DicomAnonymizer.SecurityProfileOptions.BasicProfile);
            securityProfile.PatientName = "kökö";

            var dicomAnonymizer = new Dicom.DicomAnonymizer(securityProfile);
            var anonymizedDicom = dicomAnonymizer.Anonymize(originalDicom);

            // Ensure that we are using valid input data for test.
            Assert.Equal(Encoding.ASCII, DicomEncoding.Default);
            Assert.NotEqual(DicomEncoding.GetEncoding(originalDicom.Dataset.GetString(DicomTag.SpecificCharacterSet)), DicomEncoding.Default);

            // Ensure DICOM encoding same as original.
            Assert.Equal(originalDicom.Dataset.GetString(DicomTag.SpecificCharacterSet), anonymizedDicom.Dataset.GetString(DicomTag.SpecificCharacterSet));
            Assert.Equal("kökö", anonymizedDicom.Dataset.GetString(DicomTag.PatientName));
        }

        [Fact]
        public void Anonymize_AvoidValidationOnAnonymization()
        {
            const string fileName = "GH064.dcm";
#if NETFX_CORE
            var originalDicom = Dicom.Helpers.ApplicationContent.OpenDicomFileAsync($"Data/{fileName}").Result;
#else
            var originalDicom = DicomFile.Open($"./Test Data/{fileName}");
#endif

            var ds = new DicomDataset(originalDicom.Dataset).NotValidated();
            var invalidUid = "1.2.315.6666.008965..19187632.1";
            ds.AddOrUpdate(DicomTag.StudyInstanceUID, invalidUid);
            ds = ds.Validated();

            var anonymizer = new DicomAnonymizer();
            var anonymizedDs = anonymizer.Anonymize(ds);
            Assert.NotNull(anonymizedDs);

            var df = new DicomFile(ds);
            var anonymizedDf = anonymizer.Anonymize(df);
            Assert.NotNull(anonymizedDf);
        }

        #endregion
    }
}
