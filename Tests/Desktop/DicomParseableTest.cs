﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Xunit;

    [Collection("General")]
    public class DicomParseableTest
    {
        [Fact]
        public void Parse_ImplementingClass_Succeeds()
        {
            var syntax = DicomParseable.Parse<DicomTransferSyntax>("1.2.840.10008.1.2.4.51");
            Assert.Equal(DicomTransferSyntax.JPEGProcess2_4, syntax);
        }

        [Fact]
        public void Parse_NonImplementingClass_Throws()
        {
            Assert.Throws<DicomDataException>(() => DicomParseable.Parse<DicomFile>(@".\Test Data\CT1_J2KI"));
        }
    }
}
