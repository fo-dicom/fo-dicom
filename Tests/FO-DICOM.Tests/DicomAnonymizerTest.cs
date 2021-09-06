﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Xunit;
using System.Text;
using System;

namespace FellowOakDicom.Tests
{

    [Collection("General")]
    public class DicomAnonymizerTest
    {
        #region Unit tests

        [Fact]
        public void AnonymizeInPlace_Dataset_PatientDataEmpty()
        {
            var dataset = DicomFile.Open(TestData.Resolve("CT1_J2KI")).Dataset;
            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            Assert.Empty(dataset.GetValues<string>(DicomTag.PatientName));
            Assert.Empty(dataset.GetValues<string>(DicomTag.PatientID));
            Assert.Empty(dataset.GetValues<string>(DicomTag.PatientSex));
        }

        [Fact]
        public void AnonymizeInPlace_File_SopInstanceUidTransferredToMetaInfo()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
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
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
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
            var dataset = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle")).Dataset;
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
            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
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

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
            Assert.True(dataset.GetSingleValue<string>(tag).Length > 0);

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            var expected = Array.Empty<string>();
            var actual = dataset.GetValues<string>(tag);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AnonymizeInPlace_SeriesDate_ShouldBeRemoved()
        {
            const string fileName = "CT1_J2KI";
            var tag = DicomTag.SeriesDate;

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
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

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
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

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
            dataset.Add(new DicomSequence(tag,
                new DicomDataset(new DicomLongString(DicomTag.CodeMeaning, "SOME MEANING"))));

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            Assert.True(dataset.Contains(tag));

            var sequence = dataset.GetSequence(tag);
            Assert.Equal(0, sequence.Items.Count);
        }

        [Fact]
        public void AnonymizeInPlace_SequenceToKeep_NestedDatasetsShouldBeParsed()
        {
            const string fileName = "GH610.dcm";
            var tagRoiContourSeq = DicomTag.ROIContourSequence;
            var tagContourSeq = DicomTag.ContourSequence;
            var tagContourImgSeq = DicomTag.ContourImageSequence;
            var generatedUid1 = DicomUIDGenerator.GenerateDerivedFromUUID();
            var generatedUid2 = DicomUIDGenerator.GenerateDerivedFromUUID();

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;

            dataset.Add(new DicomSequence(tagRoiContourSeq, new DicomDataset(
                new DicomSequence(tagContourSeq, new DicomDataset(
                    new DicomSequence(tagContourImgSeq,
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

            Assert.True(dataset.Contains(tagRoiContourSeq));

            var sequence1 = dataset.GetSequence(tagRoiContourSeq);
            var sequence2 = sequence1.Items[0].GetSequence(tagContourSeq);
            var sequence3 = sequence2.Items[0].GetSequence(tagContourImgSeq);
            Assert.NotEqual(sequence3.Items[0].GetSingleValue<DicomUID>(DicomTag.ReferencedSOPInstanceUID), sequence3.Items[1].GetSingleValue<DicomUID>(DicomTag.ReferencedSOPInstanceUID));
            Assert.NotEqual(generatedUid1, sequence3.Items[0].GetSingleValue<DicomUID>(DicomTag.ReferencedSOPInstanceUID));
            Assert.NotEqual(generatedUid2, sequence3.Items[1].GetSingleValue<DicomUID>(DicomTag.ReferencedSOPInstanceUID));
            Assert.Equal(1, sequence3.Items[0].GetSingleValue<int>(DicomTag.ReferencedFrameNumber));
            Assert.Equal(2, sequence3.Items[1].GetSingleValue<int>(DicomTag.ReferencedFrameNumber));
        }

        [Fact]
        public void AnonymizeInPlace_BasicProfile()
        {
            const string fileName = "CT1_J2KI";

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;

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

            var originalDicom = DicomFile.Open($"./Test Data/{fileName}");

            var securityProfile = DicomAnonymizer.SecurityProfile.LoadProfile(null, DicomAnonymizer.SecurityProfileOptions.BasicProfile);
            securityProfile.PatientName = "kökö";

            var dicomAnonymizer = new DicomAnonymizer(securityProfile);
            var anonymizedDicom = dicomAnonymizer.Anonymize(originalDicom);

            // Ensure that we are using valid input data for test.
            Assert.Equal(Encoding.ASCII, DicomEncoding.Default);
            Assert.NotEqual(DicomEncoding.GetEncoding(originalDicom.Dataset.GetString(DicomTag.SpecificCharacterSet)), DicomEncoding.Default);

            // Ensure DICOM encoding same as original.
            Assert.Equal(originalDicom.Dataset.GetString(DicomTag.SpecificCharacterSet), anonymizedDicom.Dataset.GetString(DicomTag.SpecificCharacterSet));
            Assert.Equal("kökö", anonymizedDicom.Dataset.GetString(DicomTag.PatientName));
        }

        [Fact]
        public void AnonymizeWithoutException()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(new DicomDecimalString(new DicomTag(0x01F1, 0x1033, new DicomPrivateCreator("ELSCINT1")), "0.8"));

            var _anonymizer = new DicomAnonymizer(DicomAnonymizer.SecurityProfile.LoadProfile(null, (DicomAnonymizer.SecurityProfileOptions)15));

            var ex = Record.Exception(
                () =>
                    _anonymizer.AnonymizeInPlace(dataset)
                    );

            Assert.Null(ex);
        }

        #endregion
    }
}
