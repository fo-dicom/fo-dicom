// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection("General")]
    public class DicomValidationTest
    {

        #region Unit tests

        [Fact]
        public void DicomValidation_AddValidData()
        {
            var ds = new DicomDataset();
            var validUid = "1.2.315.6666.0.8965.19187632.1";
            ds.Add(DicomTag.StudyInstanceUID, validUid);
            Assert.Equal(validUid, ds.GetSingleValue<string>(DicomTag.StudyInstanceUID));
        }

        [Fact]
        public void DicomValidation_AddInvalidData()
        {
            var ds = new DicomDataset();
            var invalidUid = "1.2.315.6666.008965..19187632.1";
            // trying to add this invalidUid should throw exception
            Assert.Throws<DicomValidationException>(() => ds.Add(DicomTag.StudyInstanceUID, invalidUid));

            ds.AutoValidate = false;
            // if AutoValidate is turned off, the invalidUid should be able to be added
            ds.Add(DicomTag.StudyInstanceUID, invalidUid);
            Assert.Equal(invalidUid, ds.GetSingleValue<string>(DicomTag.StudyInstanceUID));

            var tmpFile = Path.GetTempFileName();
            ds.Add(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage);
            ds.Add(DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateNew().UID);
            // save this invalid dicomdataset
            (new DicomFile(ds)).Save(tmpFile);

            // reading of this invalid dicomdataset should be possible
            var dsFile = DicomFile.Open(tmpFile);
            Assert.Equal(invalidUid, dsFile.Dataset.GetSingleValue<string>(DicomTag.StudyInstanceUID));

            // but the validation should still work
            Assert.Throws<DicomValidationException>(() => dsFile.Dataset.Validate());
        }

        [Fact]
        public void DicomValidation_ValidateUID()
        {
            var ds = new DicomDataset();
            var validUid = "1.2.315.6666.0.0.0.8965.19187632.1";
            ds.Add(DicomTag.StudyInstanceUID, validUid);
            Assert.Equal(validUid, ds.GetSingleValue<string>(DicomTag.StudyInstanceUID));

            var tooLongUid = validUid + "." + validUid;
            var ex = Assert.ThrowsAny<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.StudyInstanceUID, tooLongUid));
            Assert.Contains("length", ex.Message);

            var leadingZeroUid = validUid + ".03";
            var ex2 = Assert.ThrowsAny<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.SeriesInstanceUID, leadingZeroUid));
            Assert.Contains("leading zero", ex2.Message);
        }

        [Fact]
        public void DicomValidation_ValidateCodeString()
        {
            var ds = new DicomDataset();
            var validAETitle = "HUGO1";
            ds.Add(DicomTag.ReferencedFileID, validAETitle);

            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "Hugo1"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "HUGO-1"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "HUGOHUGOHUGOHUGO1"));
        }


        [Fact]
        public void DicomValidation_ValidateCodeStringWithGlobalSuppression()
        {
            DicomValidation.AutoValidation = false;
            var ds = new DicomDataset();
            var validAETitle = "HUGO1";
            ds.Add(DicomTag.ReferencedFileID, validAETitle);

            Assert.Null(Record.Exception(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "Hugo1")));
            Assert.Null(Record.Exception(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "HUGO-1")));
            Assert.Null(Record.Exception(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "HUGOHUGOHUGOHUGO1")));
            DicomValidation.AutoValidation = true;
        }


        [Fact]
        public void AddInvalidUIDMultiplicity()
        {
            Assert.Throws<DicomValidationException>(() =>
            {
                var ds = new DicomDataset();
                ds.Add(DicomTag.SeriesInstanceUID, "1.2.3\\3.4.5");
            });

            Assert.Throws<DicomValidationException>(() =>
            {
                var ds = new DicomDataset();
                ds.Add(DicomTag.SeriesInstanceUID, "1.2.3", "2.3.4");
            });

            Assert.Throws<DicomValidationException>(() =>
            {
                var ds = new DicomDataset();
                ds.Add(new DicomUniqueIdentifier(DicomTag.SeriesInstanceUID, "1.2.3", "3.4.5"));
            });
        }


        [Fact()]
        public void AddInvalidUIDMultiplicityWithGlobalSuppression()
        {
            DicomValidation.PerformValidation = false;
            Assert.Null(Record.Exception(() =>
            {
                var ds = new DicomDataset();
                ds.Add(DicomTag.SeriesInstanceUID, "1.2.3\\3.4.5");
            }));

            Assert.Null(Record.Exception(() =>
            {
                var ds = new DicomDataset();
                ds.Add(DicomTag.SeriesInstanceUID, "1.2.3", "2.3.4");
            }));

            Assert.Null(Record.Exception(() =>
            {
                var ds = new DicomDataset();
                ds.Add(new DicomUniqueIdentifier(DicomTag.SeriesInstanceUID, "1.2.3", "3.4.5"));
            }));
            DicomValidation.PerformValidation = true;
        }

        #endregion

    }
}
