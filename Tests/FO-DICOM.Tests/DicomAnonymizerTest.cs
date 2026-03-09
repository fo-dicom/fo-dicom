// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Text;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomAnonymizerTest
    {
        #region Unit tests

        [Fact]
        public void AnonymizeInPlace_Dataset_PatientDataEmpty()
        {
            var dataset = DicomFile.Open(TestData.Resolve("CT1_J2KI")).Dataset;
            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            Assert.Empty(dataset.GetItem(DicomTag.PatientName).Value);
            Assert.Empty(dataset.GetItem(DicomTag.PatientID).Value);
            Assert.Empty(dataset.GetItem(DicomTag.PatientSex).Value);
        }

        [Fact]
        public void AnonymizeInPlace_File_SopInstanceUidTransferredToMetaInfo()
        {
            var file = DicomFile.Open(TestData.Resolve("CT1_J2KI"));
            var old = file.Dataset.GetItem(DicomTag.SOPInstanceUID).Value;
            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(file);

            var expected = file.Dataset.GetItem(DicomTag.SOPInstanceUID).Value;
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
            var expected = dataset.GetItem(DicomTag.StudyInstanceUID).Value;

            var anonymizer = new DicomAnonymizer();
            var newDataset = anonymizer.Anonymize(dataset);

            var actual = dataset.GetItem(DicomTag.StudyInstanceUID).Value;
            var actualNew = newDataset.GetItem(DicomTag.StudyInstanceUID).Value;

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

            var actualName = newDataset.GetItem(DicomTag.PatientName).Value;
            var actualId = newDataset.GetItem(DicomTag.PatientID).Value;

            Assert.Equal(expectedName, actualName);
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void AnonymizeInPlace_StudyDate_ShouldBeEmpty()
        {
            const string fileName = "CT1_J2KI";
            var tag = DicomTag.StudyDate;

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
            Assert.True(dataset.GetItem(tag).StringValues[0].Length > 0);

            var anonymizer = new DicomAnonymizer();
            anonymizer.AnonymizeInPlace(dataset);

            var expected = Array.Empty<string>();
            var actual = dataset.GetItem(tag).StringValues;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AnonymizeInPlace_SeriesDate_ShouldBeRemoved()
        {
            const string fileName = "CT1_J2KI";
            var tag = DicomTag.SeriesDate;

            var dataset = DicomFile.Open($"./Test Data/{fileName}").Dataset;
            Assert.True(dataset.GetItem(tag).StringValues[0].Length > 0);

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
            Assert.Empty(sequence.Items);
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
            Assert.NotEqual(sequence3.Items[0].GetItem(DicomTag.ReferencedSOPInstanceUID).Value, sequence3.Items[1].GetItem(DicomTag.ReferencedSOPInstanceUID).Value);
            Assert.NotEqual(generatedUid1, sequence3.Items[0].GetItem(DicomTag.ReferencedSOPInstanceUID).Value);
            Assert.NotEqual(generatedUid2, sequence3.Items[1].GetItem(DicomTag.ReferencedSOPInstanceUID).Value);
            Assert.Equal(1, sequence3.Items[0].GetItem(DicomTag.ReferencedFrameNumber).Value);
            Assert.Equal(2, sequence3.Items[1].GetItem(DicomTag.ReferencedFrameNumber).Value);
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

        [Fact]
        public void AnonymizeInPlace_DicomValueElement_ShouldBeDefault_WhenBlanking()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            var floatTag = new DicomTagFL(0x300f, 0x1010, new DicomPrivateCreator("TEST"));
            var doubleTag = new DicomTagFD(0x300f, 0x1011, new DicomPrivateCreator("TEST"));
            var longTag = new DicomTagSV(0x300f, 0x1012, new DicomPrivateCreator("TEST"));
            var unsignedLongTag = new DicomTagUV(0x300f, 0x1013, new DicomPrivateCreator("TEST"));
            var shortTag = new DicomTagSS(0x300f, 0x1014, new DicomPrivateCreator("TEST"));
            var unsignedShortTag = new DicomTagUS(0x300f, 0x1015, new DicomPrivateCreator("TEST"));
            var profile = new StringReader(@"300f,10[0-9A-F]{2};C;;;;;;;;;;");

            dataset.Add(new DicomFloatingPointSingle(floatTag, 12.5f));
            dataset.Add(new DicomFloatingPointDouble(doubleTag, 12.5d));
            dataset.Add(new DicomSignedVeryLong(longTag, -125));
            dataset.Add(new DicomUnsignedVeryLong(unsignedLongTag, 125U));
            dataset.Add(new DicomSignedShort(shortTag, -12));
            dataset.Add(new DicomUnsignedShort(unsignedShortTag, 12));

            var _anonymizer = new DicomAnonymizer(DicomAnonymizer.SecurityProfile.LoadProfile(profile, DicomAnonymizer.SecurityProfileOptions.BasicProfile));
            _anonymizer.AnonymizeInPlace(dataset);
            Assert.Equal(new float(), dataset.GetItem(floatTag).Value);
            Assert.Equal(new double(), dataset.GetItem(doubleTag).Value);
            Assert.Equal(new long(), dataset.GetItem(longTag).Value);
            Assert.Equal(new ulong(), dataset.GetItem(unsignedLongTag).Value);
            Assert.Equal(new short(), dataset.GetItem(shortTag).Value);
            Assert.Equal(new ushort(), dataset.GetItem(unsignedShortTag).Value);
        }

        [Fact]
        public void AnonymizeInPlace_OtherElement_ShouldBeEmpty_WhenBlanking()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            var testTag = new DicomTag(0x300f, 0x1010, new DicomPrivateCreator("TEST"));
            var profile = new StringReader(@"300f,1010;C;;;;;;;;;;");

            dataset.Add(new DicomOtherFloat(testTag, 12.5f));

            var _anonymizer = new DicomAnonymizer(DicomAnonymizer.SecurityProfile.LoadProfile(profile, DicomAnonymizer.SecurityProfileOptions.BasicProfile));
            _anonymizer.AnonymizeInPlace(dataset);
            Assert.Equal(0, dataset.GetValueCount(testTag));
        }
        #endregion
    }
}
