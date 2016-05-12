// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Xunit;

    public class DicomFileMetaInformationTests
    {
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
    }
}
