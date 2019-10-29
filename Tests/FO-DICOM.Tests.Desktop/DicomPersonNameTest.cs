// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text;
using Xunit;

namespace FellowOakDicom.Tests
{

    /// <summary>
    ///This is a test class for DicomPersonNameTest and is intended
    ///to contain all DicomPersonNameTest Unit Tests
    ///</summary>
    [Collection("General")]
    public class DicomPersonNameTest
    {
        /// <summary>
        ///A test for DicomPersonName Constructor
        ///</summary>
        [Fact]
        public void DicomPersonNameConstructorTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.Equal("Last^First^Middle^Prefix^Suffix", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "", "");
            Assert.Equal("Last^First^Middle", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "Last", "First", null, "");
            Assert.Equal("Last^First", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "", null, "Suffix");
            Assert.Equal("Last^First^^^Suffix", target.Get<string>());
            target = new DicomPersonName(DicomTag.PatientName, "", "", "", null, null);
            Assert.Equal("", target.Get<string>());
        }

        /// <summary>
        ///A test for DicomPersonName Constructor
        ///</summary>
        [Fact]
        public void DicomPersonNameConstructorTest1()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, DicomEncoding.GetEncoding("ISO IR 144"), "Тарковский", "Андрей", "Арсеньевич");
            byte[] b = target.Buffer.GetByteRange(0, (int)target.Buffer.Size);
            byte[] c = Encoding.GetEncoding("iso-8859-5").GetBytes("Тарковский^Андрей^Арсеньевич");
            Assert.Equal(c, b);
            // foloowing test checks also padding with space!
            target = new DicomPersonName(DicomTag.PatientName, DicomEncoding.GetEncoding("ISO IR 144"), "Тарковский", "Андрей");
            b = target.Buffer.GetByteRange(0, (int)target.Buffer.Size);
            c = Encoding.GetEncoding("iso-8859-5").GetBytes("Тарковский^Андрей ");
            Assert.Equal(c, b);
        }

        /// <summary>
        ///A test for Last
        ///</summary>
        [Fact]
        public void LastTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.Equal("Last", target.Last);
            target = new DicomPersonName(DicomTag.PatientName, "");
            Assert.Equal("", target.Last);
            target = new DicomPersonName(DicomTag.PatientName, "=Doe^John");
            Assert.Equal("", target.Last);
        }

        /// <summary>
        ///A test for First
        ///</summary>
        [Fact]
        public void FirstTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.Equal("First", target.First);
            target = new DicomPersonName(DicomTag.PatientName, "Last");
            Assert.Equal("", target.First);
            target = new DicomPersonName(DicomTag.PatientName, "Last=Doe^John");
            Assert.Equal("", target.First);
        }

        /// <summary>
        ///A test for Middle
        ///</summary>
        [Fact]
        public void MiddleTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.Equal("Middle", target.Middle);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First");
            Assert.Equal("", target.Middle);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First=Doe^John^Peter");
            Assert.Equal("", target.Middle);
        }

        /// <summary>
        ///A test for Prefix
        ///</summary>
        [Fact]
        public void PrefixTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.Equal("Prefix", target.Prefix);
            target = new DicomPersonName(DicomTag.PatientName, "Last");
            Assert.Equal("", target.Prefix);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First^Middle=Doe^John^Peter^MD^xx");
            Assert.Equal("", target.Prefix);
        }

        /// <summary>
        ///A test for Suffix
        ///</summary>
        [Fact]
        public void SuffixTest()
        {
            DicomPersonName target = new DicomPersonName(DicomTag.PatientName, "Last", "First", "Middle", "Prefix", "Suffix");
            Assert.Equal("Suffix", target.Suffix);
            target = new DicomPersonName(DicomTag.PatientName, "Last");
            Assert.Equal("", target.Suffix);
            target = new DicomPersonName(DicomTag.PatientName, "Last^First^Middle^Prefix=Doe^John^Peter^MD");
            Assert.Equal("", target.Suffix);
        }
    }
}
