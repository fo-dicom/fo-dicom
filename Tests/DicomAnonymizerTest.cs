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

#endregion
    }
}
