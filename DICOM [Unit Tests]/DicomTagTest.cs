// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;

    using Xunit;

    /// <summary>
    ///     This is a test class for DicomTagTest and is intended
    ///     to contain all DicomTagTest Unit Tests
    /// </summary>
    [Collection("General")]
    public class DicomTagTest
    {
        /// <summary>
        ///     A test for ToString
        /// </summary>
        [Fact]
        public void ToJsonStringTest()
        {
            const ushort @group = 0x7FE0;
            const ushort element = 0x00FF;
            var target = new DicomTag(group, element);
            const string format = "J";
            IFormatProvider formatProvider = null;
            const string expected = "7FE000FF";
            string actual = string.Empty;
            actual = target.ToString(format, formatProvider);
            Assert.Equal(expected, actual);
        }
    }
}