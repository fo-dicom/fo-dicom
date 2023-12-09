// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using Xunit;

namespace FellowOakDicom.Tests
{

    /// <summary>
    /// unit test for DicomTransferSyntax
    /// note that Register may leave extra item into internal static dictionary
    /// which may cause unit test to fail.
    /// </summary>
    [Collection(TestCollections.General)]
    public class DicomTransferSyntaxTest
    {
        #region Unit tests

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void LookupReturnsKnownTransferSyntax()
        {
            var ts = DicomTransferSyntax.Lookup(DicomUID.ImplicitVRLittleEndian);
            Assert.NotNull(ts);
            Assert.Equal(DicomUID.ImplicitVRLittleEndian, ts.UID);
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void LookupReturnsAdHocTransferSyntax()
        {
            var uid = new DicomUID("0", "testing", DicomUidType.TransferSyntax);

            var ts = DicomTransferSyntax.Lookup(uid);
            Assert.NotNull(ts);
            Assert.Equal(uid, ts.UID);

            //  Lookup must not auto-register, as it is invoked from DicomServer.
            //  auto-registration may cause DoS by sending crafted transfer syntaxes repeatedly,
            //  which causes internal static dictionary to hold all the transfer syntaxes.
            Assert.Null(DicomTransferSyntax.Query(uid));
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void LookupThrowsOnInvalidUidType()
        {
            var uid = DicomUID.ComputedRadiographyImageStorage;
            Assert.Throws<DicomDataException>(
                () => DicomTransferSyntax.Lookup(uid)
            );
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void QueryReturnsKnownTransferSyntax()
        {
            var uid = DicomUID.ImplicitVRLittleEndian;
            var ts = DicomTransferSyntax.Query(uid);
            Assert.NotNull(ts);
            Assert.Equal(uid, ts.UID);
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void QueryReturnsRegisteredTransferSyntax()
        {
            var uid = new DicomUID("0", "testing", DicomUidType.TransferSyntax);
            DicomTransferSyntax.Register(uid);

            var ts = DicomTransferSyntax.Query(uid);
            Assert.NotNull(ts);
            Assert.Equal(uid, ts.UID);

            DicomTransferSyntax.Unregister(uid);
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void QueryReturnsNullIfNotRegistered()
        {
            var uid = new DicomUID("0", "testing", DicomUidType.TransferSyntax);

            var ts = DicomTransferSyntax.Query(uid);
            Assert.Null(ts);
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void RegisterRegistersTransferSyntax()
        {
            var uid = new DicomUID("0", "testing", DicomUidType.TransferSyntax);

            Assert.Null(DicomTransferSyntax.Query(uid));

            var ts1 = DicomTransferSyntax.Register(uid);
            Assert.NotNull(ts1);

            var ts2 = DicomTransferSyntax.Query(uid);
            Assert.Equal(uid, ts2.UID);

            DicomTransferSyntax.Unregister(uid);
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void RegisterHandlesMultipleRegistrations()
        {
            var uid = new DicomUID("0", "testing", DicomUidType.TransferSyntax);

            //  should not matter if 2 instances are same.
            Assert.NotNull(DicomTransferSyntax.Register(uid));
            Assert.NotNull(DicomTransferSyntax.Register(uid));

            //  just to be sure
            DicomTransferSyntax.Unregister(uid);
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void UnregisterUnregistersTransferSyntax()
        {
            var uid = new DicomUID("0", "testing", DicomUidType.TransferSyntax);

            DicomTransferSyntax.Register(uid);
            Assert.NotNull(DicomTransferSyntax.Query(uid));

            DicomTransferSyntax.Unregister(uid);
            Assert.Null(DicomTransferSyntax.Query(uid));
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void UnregisterHandleMultipleUnregistrations()
        {
            var uid = new DicomUID("0", "testing", DicomUidType.TransferSyntax);

            DicomTransferSyntax.Register(uid);
            Assert.NotNull(DicomTransferSyntax.Query(uid));

            Assert.True(DicomTransferSyntax.Unregister(uid));
            Assert.Null(DicomTransferSyntax.Query(uid));

            Assert.False(DicomTransferSyntax.Unregister(uid));
            Assert.Null(DicomTransferSyntax.Query(uid));
        }

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
