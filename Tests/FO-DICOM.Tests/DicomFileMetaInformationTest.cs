// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.IO;
using Xunit;

// These tests cover some obsolete properties such as AutoValidate
#pragma warning disable CS0618

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.Validation)]
    public class DicomFileMetaInformationTest
    {
        #region Unit tests

        [Fact]
        public void ImplementationVersionName_GetterWhenAttributeIncluded_ReturnsValue()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));

            var exception = Record.Exception(() => { Assert.NotNull(metaInfo.ImplementationVersionName); });
            Assert.Null(exception);
        }

        [Fact]
        public void ImplementationVersionName_GetterWhenAttributeMissing_ReturnsNull()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));
            metaInfo.Remove(DicomTag.ImplementationVersionName);

            var exception = Record.Exception(() => { Assert.Null(metaInfo.ImplementationVersionName); });
            Assert.Null(exception);
        }

        [Fact]
        public void SourceApplicationEntityTitle_GetterWhenAttributeMissing_ReturnsNull()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));
            metaInfo.Remove(DicomTag.SourceApplicationEntityTitle);

            var exception = Record.Exception(() => { Assert.Null(metaInfo.SourceApplicationEntityTitle); });
            Assert.Null(exception);
        }

        [Fact]
        public void SourceApplicationEntityTitle_GetterWhenAttributeAlreadyExists_ReturnsValue()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"))
                        .Add(DicomTag.SourceApplicationEntityTitle, "ABCDEFG"));

            var exception = Record.Exception(() => { Assert.Equal("ABCDEFG", metaInfo.SourceApplicationEntityTitle); });
            Assert.Null(exception);
        }

        [Fact]
        public void Constructor_FromFileMetaInformation_ShouldNotThrow()
        {
            var metaInfo = new DicomFileMetaInformation();
            var exception = Record.Exception(() => new DicomFileMetaInformation(metaInfo));
            Assert.Null(exception);
        }

        [Fact]
        public void AddingRegularTag_ToMetaInformation_ShouldThrow()
        {
            var metaInfo = new DicomFileMetaInformation();
            var exception = Record.Exception(() => metaInfo.AddOrUpdate(DicomTag.PatientName, "Doe^John"));
            Assert.IsType<DicomDataException>(exception);
        }

        [Theory]
        [MemberData(nameof(NewOptionalAttributes))]
        public void Save_NewOptionalAttributes_SavedWhenExisting(DicomItem item)
        {
            var inFile = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));
            inFile.FileMetaInfo.Add(item);

            using var saveStream = new MemoryStream();
            inFile.Save(saveStream);
            saveStream.Seek(0, SeekOrigin.Begin);

            var file = DicomFile.Open(saveStream);
            Assert.True(file.FileMetaInfo.Contains(item.Tag));
        }

        [Fact]
        public void PrivateInformationCreatorUID_SetterGetter_DataIsMaintained()
        {
            var inFile = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));
            var expected = "1.2.3";
            inFile.FileMetaInfo.PrivateInformationCreatorUID = DicomUID.Parse(expected);

            using var saveStream = new MemoryStream();
            inFile.Save(saveStream);
            saveStream.Seek(0, SeekOrigin.Begin);

            var file = DicomFile.Open(saveStream);
            Assert.Equal(expected, file.FileMetaInfo.PrivateInformationCreatorUID.UID);
        }

        [Fact]
        public void PrivateInformation_SetterGetter_DataIsMaintained()
        {
            var inFile = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));
            var expected = new byte[] { 0x00, 0x01, 0x02, 0x03 };
            inFile.FileMetaInfo.PrivateInformation = expected;

            using var saveStream = new MemoryStream();
            inFile.Save(saveStream);
            saveStream.Seek(0, SeekOrigin.Begin);

            var file = DicomFile.Open(saveStream);
            Assert.Equal(expected, file.FileMetaInfo.PrivateInformation);
        }

        [Fact]
        public void NewProperties_Getters_ReturnsNullIfNonExisting()
        {
            var file = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle"));

            Assert.Null(file.FileMetaInfo.SendingApplicationEntityTitle);
            Assert.Null(file.FileMetaInfo.ReceivingApplicationEntityTitle);
            Assert.Null(file.FileMetaInfo.PrivateInformationCreatorUID);
            Assert.Null(file.FileMetaInfo.PrivateInformation);
        }

        [Fact]
        public void Construction_sets_validation_to_false_default_constructor()
        {
            var metaInfo = new DicomFileMetaInformation();
            Assert.False(metaInfo.ValidateItems);
        }

        [Theory]
        [InlineData(true, "1.2.3.0456", true)]
        [InlineData(true, "1.2.3.456.00789", true)]
        [InlineData(true, "1.2.3.456.00.789", true)]
        [InlineData(false, "1.2.3.0456", false)]
        [InlineData(false, "1.2.3.456.00789", false)]
        [InlineData(false, "1.2.3.456.00.789", false)]
        [InlineData(true, "1.2.3.456", false)]
        [InlineData(true, "1.2.3.456.789", false)]
        [InlineData(true, "1.2.3.456.0.789", false)]
        public void Construction_sets_validation_to_false_copy_constructor(bool validate, string sopInstanceUid, bool expectedError)
        {
            DicomFileMetaInformation existing, metaInfo;
            var dataset = new DicomDataset { ValidateItems = validate };

            if (expectedError)
            {
                // we test the dataset as well here
                Assert.Throws<DicomValidationException>(() => dataset.Add(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, sopInstanceUid)));

                // such that we can construct it
                dataset.ValidateItems = false;
                dataset.AddOrUpdate(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, sopInstanceUid));
                dataset.ValidateItems = validate;

                Assert.Throws<DicomValidationException>(() => new DicomFileMetaInformation(dataset));

                return;
            }
            else
            {
                dataset.Add(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, sopInstanceUid));
                existing = new DicomFileMetaInformation(dataset);
            }

            metaInfo = new DicomFileMetaInformation(existing);

            Assert.Equal(validate, metaInfo.ValidateItems);
            Assert.Equal(validate, metaInfo.AutoValidate);
            Assert.Equal(sopInstanceUid, metaInfo.MediaStorageSOPInstanceUID.UID);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Construction_sets_validation_to_dataset_value_using_dataset_constructor(bool validate)
        {
            var dataset = new DicomDataset { ValidateItems = validate };
            dataset.Add(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3.456"));
            var metaInfo = new DicomFileMetaInformation(dataset);

            Assert.Equal(validate, metaInfo.ValidateItems);
            Assert.Equal("1.2.3.456", metaInfo.MediaStorageSOPInstanceUID.UID);
        }

        [Theory]
        [InlineData("1.2.3.0456")]
        [InlineData("1.2.3.456.00789")]
        [InlineData("1.2.3.456.00.789")]
        public void Invalid_UI_values_for_meta_information_should_not_throw_on_creation(string sopInstanceUid)
        {
            var dataset = new DicomDataset
            {
                AutoValidate = false
            };
            dataset.Add(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, sopInstanceUid));
            var metaInfo = new DicomFileMetaInformation(dataset);

            Assert.Equal(sopInstanceUid, metaInfo.MediaStorageSOPInstanceUID.UID);
        }

        [Theory]
        [InlineData("1.2.3.0456")]
        [InlineData("1.2.3.456.00789")]
        [InlineData("1.2.3.456.00.789")]
        public void Invalid_UI_values_for_meta_information_should_throw_on_set_when_validation_is_enabled(string sopInstanceUid)
        {
            var metaInfo = new DicomFileMetaInformation() { ValidateItems = true };
            Assert.Throws<DicomValidationException>(() => metaInfo.MediaStorageSOPInstanceUID = new DicomUID(sopInstanceUid, "test", DicomUidType.SOPInstance));
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> NewOptionalAttributes
        {
            get
            {
                yield return new object[] { new DicomApplicationEntity(DicomTag.SendingApplicationEntityTitle, "SENDING") };
                yield return new object[] { new DicomApplicationEntity(DicomTag.ReceivingApplicationEntityTitle, "RECEIVING") };
                yield return new object[] { new DicomUniqueIdentifier(DicomTag.PrivateInformationCreatorUID, "1.2.3") };
                yield return new object[] { new DicomOtherByte(DicomTag.PrivateInformation, 0x00, 0x01, 0x02, 0x03) };
            }
        }

        #endregion
    }
}
