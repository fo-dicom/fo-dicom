﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Xunit;

    [Collection("General")]
    public class DicomVRTest
    {
        #region Unit tests

        [Fact]
        public void Parse_UC_ReturnsVRObject()
        {
            var actual = DicomVR.Parse("UC");
            Assert.IsType<DicomVR>(actual);
        }

        [Fact]
        public void Parse_UR_ReturnsVRObject()
        {
            var actual = DicomVR.Parse("UR");
            Assert.IsType<DicomVR>(actual);
        }

        [Fact]
        public void Parse_OD_ReturnsVRObject()
        {
            var actual = DicomVR.Parse("OD");
            Assert.IsType<DicomVR>(actual);
        }

        [Fact]
        public void Parse_OF_ReturnsVRObject()
        {
            var actual = DicomVR.Parse("OF");
            Assert.IsType<DicomVR>(actual);
        }

        #endregion
    }
}
