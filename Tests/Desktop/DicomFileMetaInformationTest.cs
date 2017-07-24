// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.IO;

namespace Dicom
{
    using Xunit;

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
        public void SourceApplicationEntityTitle_GetterWhenAttributeIncluded_ReturnsValue()
        {
            var metaInfo =
                new DicomFileMetaInformation(
                    new DicomDataset(
                        new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                        new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3")));

            var exception = Record.Exception(() => { Assert.NotNull(metaInfo.SourceApplicationEntityTitle); });
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
        public void Constructor_FromFileMetaInformation_ShouldNotThrow()
        {
            var metaInfo = new DicomFileMetaInformation();
            var exception = Record.Exception(() => new DicomFileMetaInformation(metaInfo));
            Assert.Null(exception);
        }

        [Theory]
        [MemberData(nameof(NewOptionalAttributes))]
        public void Save_NewOptionalAttributes_SavedWhenExisting(DicomItem item)
        {
            var inFile = DicomFile.Open(@"Test Data\CT-MONO2-16-ankle");
            inFile.FileMetaInfo.Add(item);

            using (var saveStream = new MemoryStream())
            {
                inFile.Save(saveStream);
                saveStream.Seek(0, SeekOrigin.Begin);

                var file = DicomFile.Open(saveStream);
                Assert.True(file.FileMetaInfo.Contains(item.Tag));
            }
        }

        [Fact]
        public void PrivateInformationCreatorUID_SetterGetter_DataIsMaintained()
        {
            var inFile = DicomFile.Open(@"Test Data\CT-MONO2-16-ankle");
            var expected = "1.2.3";
            inFile.FileMetaInfo.PrivateInformationCreatorUID = DicomUID.Parse(expected);

            using (var saveStream = new MemoryStream())
            {
                inFile.Save(saveStream);
                saveStream.Seek(0, SeekOrigin.Begin);

                var file = DicomFile.Open(saveStream);
                Assert.Equal(expected, file.FileMetaInfo.PrivateInformationCreatorUID.UID);
            }
        }

        [Fact]
        public void PrivateInformation_SetterGetter_DataIsMaintained()
        {
            var inFile = DicomFile.Open(@"Test Data\CT-MONO2-16-ankle");
            var expected = new byte[] { 0x00, 0x01, 0x02, 0x03 };
            inFile.FileMetaInfo.PrivateInformation = expected;

            using (var saveStream = new MemoryStream())
            {
                inFile.Save(saveStream);
                saveStream.Seek(0, SeekOrigin.Begin);

                var file = DicomFile.Open(saveStream);
                Assert.Equal(expected, file.FileMetaInfo.PrivateInformation);
            }
        }

        [Fact]
        public void NewProperties_Getters_ReturnsNullIfNonExisting()
        {
            var file = DicomFile.Open(@"Test Data\CT-MONO2-16-ankle");

            Assert.Null(file.FileMetaInfo.SendingApplicationEntityTitle);
            Assert.Null(file.FileMetaInfo.ReceivingApplicationEntityTitle);
            Assert.Null(file.FileMetaInfo.PrivateInformationCreatorUID);
            Assert.Null(file.FileMetaInfo.PrivateInformation);
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
