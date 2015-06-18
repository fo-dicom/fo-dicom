// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Xunit;

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

        #endregion
    }
}