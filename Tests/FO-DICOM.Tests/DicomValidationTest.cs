// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Tests.Helpers;
using System.IO;
using Xunit;

// These tests cover some obsolete properties such as AutoValidate
#pragma warning disable CS0618

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.Validation)]
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
            ds.Add(DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID().UID);
            // save this invalid dicomdataset
            (new DicomFile(ds)).Save(tmpFile);

            // reading of this invalid dicomdataset should be possible
            var dsFile = DicomFile.Open(tmpFile);
            Assert.Equal(invalidUid, dsFile.Dataset.GetSingleValue<string>(DicomTag.StudyInstanceUID));

            // but the validation should still work
            Assert.Throws<DicomValidationException>(() => dsFile.Dataset.Validate());
            IOHelper.DeleteIfExists(tmpFile);
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

            var emptyComponentUid = validUid + ".";
            var ex3 = Assert.ThrowsAny<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.SeriesInstanceUID, emptyComponentUid));
            Assert.Contains("not be empty", ex3.Message);

            var emptyComponent2Uid = validUid + ".2..3459.123";
            var ex4 = Assert.ThrowsAny<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.SeriesInstanceUID, emptyComponent2Uid));
            Assert.Contains("not be empty", ex4.Message);

            var emptyComponent3Uid = "." + validUid;
            var ex5 = Assert.ThrowsAny<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.SeriesInstanceUID, emptyComponent3Uid));
            Assert.Contains("not be empty", ex5.Message);
        }

        [Fact]
        public void DicomValidation_NoVMValidationForOX()
        {
            var ds = new DicomDataset
            {
                { DicomTag.Rows, (ushort)1 },
                { DicomTag.Columns, (ushort)2 },
            };

            // shall not throw (regression test for #1186)
            ds.AddOrUpdate(DicomTag.FloatPixelData, new float[] { 1.0f, 2.0f });
            ds.AddOrUpdate(DicomTag.TrackPointIndexList, new uint[] { 1, 2 });
            ds.AddOrUpdate(DicomTag.ExtendedOffsetTable, new ulong[] { 1, 2 });
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

        [Theory]
        [InlineData("0.333333333333  ")] // 16 chars
        [InlineData("0001024.0  ")] // leading zeros
        [InlineData(".0123")] // leading dot
        [InlineData("12345.")] // trailing dot
        [InlineData("71e-43")] // scientific notation
        [InlineData("-71e-43")] // scientific notation, negative value
        [InlineData("+71.123e+21")] // leading plus
        public void DicomValidation_ValidateValidDS(string value)
        {
            var ds = new DicomDataset { { DicomTag.RescaleSlope, value } };
            Assert.Equal(value, ds.GetSingleValue<string>(DicomTag.RescaleSlope));
        }
        
        [Theory]
        [InlineData("0.333333333333   ")] // 17 chars
        [InlineData(".")] // single dot
        [InlineData(".e25")] // scientific notation, single dot
        [InlineData("-323.456e-4.5")] // floating value exponent
        [InlineData("54e34e2")] // double exponent
        [InlineData("-43e")] // missing exponent
        [InlineData("--323")] // double minus
        public void DicomValidation_ValidateInvalidDS(string value)
        {
            var ds = new DicomDataset();
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.RescaleSlope, value));
        }
        
        [Fact]
        public void AddInvalidUIDMultiplicity()
        {
            Assert.Throws<DicomValidationException>(() => _ = new DicomDataset
                  {
                    { DicomTag.SeriesInstanceUID, "1.2.3\\3.4.5" }
                  });

            Assert.Throws<DicomValidationException>(() => _ = new DicomDataset
                {
                    { DicomTag.SeriesInstanceUID, "1.2.3", "2.3.4" }
                });

            Assert.Throws<DicomValidationException>(() => _ = new DicomDataset
                {
                    new DicomUniqueIdentifier(DicomTag.SeriesInstanceUID, "1.2.3", "3.4.5")
                });
        }


        [Fact()]
        public void ValidationAllowESCInSeriesDescriptionTag()
        {
            var ex = Record.Exception(() => _ = new DicomDataset
                {
                    new DicomLongString(DicomTag.SeriesDescription, "A ESC value: \u001b")
                });
            Assert.Null(ex);
        }


        [Fact]
        public void DicomValidation_ValidateDA()
        {
            var ds =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

            ds.Add(DicomTag.ScheduledProcedureStepStartDate, "20191031");
            ds.Add(DicomTag.ScheduledProcedureStepEndDate, "20191030-");
            ds.Add(DicomTag.PerformedProcedureStepStartDate, "-20191228");
            ds.Add(DicomTag.PerformedProcedureStepEndDate, "20190101-20200101");

            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDate, "19970101-");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepEndDate, "-19701231");
            ds.AddOrUpdate(DicomTag.PerformedProcedureStepStartDate, "20190101-20200101");
            ds.AddOrUpdate(DicomTag.PerformedProcedureStepEndDate, "20190101-20200101");

            var currentDate = System.DateTime.Now.ToString("yyyyMMdd");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDate, currentDate);

            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDate, "20191031--"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepEndDate, "-20191031-"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.PerformedProcedureStepStartDate, "201911"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.PerformedProcedureStepStartDate, "20193101"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.PerformedProcedureStepStartDate, "20191232"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.PerformedProcedureStepEndDate, "31122008"));
        }


        [Fact]
        public void DicomValidation_ValidateDT()
        {
            var ds =
                new DicomDataset(
                    new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage),
                    new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"));

            var currentDate = System.DateTime.Now;
            var zone = currentDate.ToString("yyyyMMddHHmmsszzz").Substring(14).Replace(":", string.Empty);

            // Basic valid tests
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyy"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMM"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMdd"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHH"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmm"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmmss"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmmss.f"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmmss.ff"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmmss.fff"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmmss.ffff"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmmss.fffff"));
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, currentDate.ToString("yyyyMMddHHmmss.ffffff"));

            // Basic valid UTC offset tests
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204230259.165432+0000");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204230259+0530");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "200812042302+0530");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2008120423+0530");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204-0100");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "200812+1400");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2007-0500");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2007+0500");

            // Random valid range tests
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "-2008");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2008-");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "-20081204230259");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"-20081204230259.165432{zone}");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2008-200912");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"200812-200812{zone}");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"20081204{zone}-20081204");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"2008120422{zone}-2008120423{zone}");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"2008120422-0200-2008120423");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "200812042202-200812042302");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"20081204220259.1{zone}-20081204230259.1");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204220259.12-20081204230259.12");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204220259.132-20081204230259.132");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204220259-20081204230259.1432");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"20081204220259.15432{zone}-20081204230259.15432{zone}");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204220259.165432-20081204230259.165432");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081204220259+0000-20081204230259.165432+0100");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"20081204220259.165432+0200-20081204230259.165432{zone}");
            ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"2019-1200-2020+1400");

            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2019-1300-2020+1300"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "-0200"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20191000"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20190013"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "201912-0000"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "201912+1500"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "- "));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "201"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2019-2020-2021"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20191021120000.000000-1300-20191022120000.000000-1300"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20191031+"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20081304230259"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "-20191031+2400"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20191031121200+02"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "20193010-20193110"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, "2019-0100-2020-0100-202002-0100"));
            Assert.Throws<DicomValidationException>(() => ds.AddOrUpdate(DicomTag.ScheduledProcedureStepStartDateTime, $"20081-200812{zone}"));
        }

        [Fact]
        public void DicomValidation_ValidatePN()
        {
            var ds = new DicomDataset { { DicomTag.PatientName, "Doe^John=Doe^John=Doe^John" } };
            // PatientName has VM 1
            Assert.Throws<DicomValidationException>(() => 
                ds.AddOrUpdate(DicomTag.PatientName, "Doe^John", "Doe^John"));
            // OtherPatientNames has VM 1-n
            ds.AddOrUpdate(DicomTag.OtherPatientNames, "Doe^John", "Doe^John");
            // more than 3 component groups
            Assert.Throws<DicomValidationException>(() => 
                ds.AddOrUpdate(DicomTag.ReferringPhysicianName, "Doe^Jane=Doe^Jane=Doe^Jane=Doe^Jane"));
            // more than 5 components
            Assert.Throws<DicomValidationException>(() => 
                ds.AddOrUpdate(DicomTag.ReferringPhysicianName, "Doe^John^^^Ph.D.^Junior"));
        }

        #endregion

    }

    [Collection("none")]
    public class DicomValidationTestWithoutFixture
    {

        [Fact(Skip = "Do not use global Validation option in unittest")] // TODO: enable again
        public void DicomValidation_ValidateCodeStringWithGlobalSuppression()
        {
            new DicomSetupBuilder()
                .RegisterServices(s => s.AddFellowOakDicom())
                .SkipValidation()
                .Build();

            var ds = new DicomDataset();
            var validAETitle = "HUGO1";
            ds.Add(DicomTag.ReferencedFileID, validAETitle);

            Assert.Null(Record.Exception(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "Hugo1")));
            Assert.Null(Record.Exception(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "HUGO-1")));
            Assert.Null(Record.Exception(() => ds.AddOrUpdate(DicomTag.ReferencedFileID, "HUGOHUGOHUGOHUGO1")));
        }
    }

}
