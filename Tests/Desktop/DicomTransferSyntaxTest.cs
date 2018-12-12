// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Dicom.IO;
    using Xunit;

    [Collection("General")]
    public class DicomTransferSyntaxTest
    {
        #region Unit tests

        /// <summary>
        /// Parse can parse string representation of known UID.
        /// </summary>
        [Fact]
        public void CanParseKnownTransferSyntax()
        {
            var ts = DicomTransferSyntax.Parse("1.2.840.10008.1.2");
            Assert.Same(DicomTransferSyntax.ImplicitVRLittleEndian, ts);
        }

        /// <summary>
        /// Parse can parse string representation of unknown UID.
        /// </summary>
        [Fact]
        public void CanParseUnkownTransferSyntax()
        {
            var ts = DicomTransferSyntax.Parse("1.2.3.4.5.6.7.8.9.0");
            Assert.Equal(Endian.Little, ts.Endian);
            Assert.False(ts.IsRetired);
            Assert.True(ts.IsExplicitVR);
            Assert.True(ts.IsEncapsulated);
            Assert.Equal("Unknown", ts.UID.Name);
            Assert.Equal(DicomUidType.TransferSyntax, ts.UID.Type);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", ts.UID.UID);
        }

        #endregion Unit tests
    }
}
