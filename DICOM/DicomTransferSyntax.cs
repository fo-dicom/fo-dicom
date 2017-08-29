// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;

using Dicom.IO;

namespace Dicom
{
    /// <summary>
    /// Representation of a DICOM transfer syntax.
    /// </summary>
    public class DicomTransferSyntax : DicomParseable
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomTransferSyntax"/> class.
        /// </summary>
        /// <param name="uid">UID of the transfer syntax.</param>
        private DicomTransferSyntax(DicomUID uid)
        {
            UID = uid;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the unique identifier of the transfer syntax.
        /// </summary>
        public DicomUID UID { get; }

        /// <summary>
        /// Gets whether or not the transfer syntax is declared retired.
        /// </summary>
        public bool IsRetired { get; private set; }

        /// <summary>
        /// Gets whether or not the Value Representation of the transfer syntax is explicit.
        /// </summary>
        public bool IsExplicitVR { get; private set; }

        /// <summary>
        /// Gets whether or not the transfer syntax data representation is encapsulated.
        /// </summary>
        public bool IsEncapsulated { get; private set; }

        /// <summary>
        /// Gets whether or not the transfer syntax data representation is lossy.
        /// </summary>
        public bool IsLossy { get; private set; }

        /// <summary>
        /// Gets the lossy compression method identifier.
        /// </summary>
        public string LossyCompressionMethod { get; private set; }

        /// <summary>
        /// Gets whether or not the transfer syntax represents deflatable objects.
        /// </summary>
        public bool IsDeflate { get; private set; }

        /// <summary>
        /// Gets the endianness of the transfer syntax.
        /// </summary>
        public Endian Endian { get; private set; }

        /// <summary>
        /// Gets whether or not the pixel data requires swapping.
        /// </summary>
        public bool SwapPixelData { get; private set; }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public override string ToString()
        {
            return UID.Name;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is DicomTransferSyntax) return ((DicomTransferSyntax)obj).UID.Equals(UID);
            if (obj is DicomUID) return ((DicomUID)obj).Equals(UID);
            if (obj is string) return UID.UID.Equals((string)obj);
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return UID.GetHashCode();
        }

        #endregion

        #region OPERATORS

        /// <summary>
        /// Equivalence operator for <see cref="DicomTransferSyntax"/> objects.
        /// </summary>
        /// <param name="a">Left-hand side <see cref="DicomTransferSyntax"/> to check for equivalence.</param>
        /// <param name="b">Right-hand side <see cref="DicomTransferSyntax"/> to check for equivalence.</param>
        /// <returns>true if <see cref="UID"/> of <see cref="DicomTransferSyntax"/> objects are equivalent or if both objects are <code>null</code>, 
        /// false otherwise.</returns>
        public static bool operator ==(DicomTransferSyntax a, DicomTransferSyntax b)
        {
            if ((object)a == null && (object)b == null) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.UID == b.UID;
        }

        /// <summary>
        /// Non-equivalence operator for <see cref="DicomTransferSyntax"/> objects.
        /// </summary>
        /// <param name="a">Left-hand side <see cref="DicomTransferSyntax"/> to check for non-eequivalence.</param>
        /// <param name="b">Right-hand side <see cref="DicomTransferSyntax"/> to check for non-equivalence.</param>
        /// <returns>true if <see cref="UID"/> of <see cref="DicomTransferSyntax"/> objects are non-equivalent or exactly one of
        /// the objects are <code>null</code>, false otherwise.</returns>
        public static bool operator !=(DicomTransferSyntax a, DicomTransferSyntax b)
        {
            return !(a == b);
        }

        #endregion

        #region Dicom Transfer Syntax

        /// <summary>Virtual transfer syntax for reading datasets improperly encoded in Big Endian format with implicit VR.</summary>
        public static DicomTransferSyntax ImplicitVRBigEndian =
            new DicomTransferSyntax(new DicomUID(DicomUID.ExplicitVRBigEndianRETIRED.UID + ".123456",
                "Implicit VR Big Endian", DicomUidType.TransferSyntax))
            {
                IsExplicitVR = false,
                Endian = Endian.Big
            };

        /// <summary>GE Private Implicit VR Big Endian</summary>
        /// <remarks>Same as Implicit VR Little Endian except for big endian pixel data.</remarks>
        public static DicomTransferSyntax GEPrivateImplicitVRBigEndian =
            new DicomTransferSyntax(DicomUID.GEPrivateImplicitVRBigEndian)
            {
                IsExplicitVR = false,
                Endian = Endian.Little,
                SwapPixelData = true
            };

        /// <summary>Implicit VR Little Endian</summary>
        public static DicomTransferSyntax ImplicitVRLittleEndian =
            new DicomTransferSyntax(DicomUID.ImplicitVRLittleEndian)
            {
                Endian = Endian.Little
            };

        /// <summary>Explicit VR Little Endian</summary>
        public static DicomTransferSyntax ExplicitVRLittleEndian =
            new DicomTransferSyntax(DicomUID.ExplicitVRLittleEndian)
            {
                IsExplicitVR = true,
                Endian = Endian.Little
            };

        /// <summary>Explicit VR Big Endian</summary>
        public static DicomTransferSyntax ExplicitVRBigEndian =
            new DicomTransferSyntax(DicomUID.ExplicitVRBigEndianRETIRED)
            {
                IsExplicitVR = true,
                Endian = Endian.Big
            };

        /// <summary>Deflated Explicit VR Little Endian</summary>
        public static DicomTransferSyntax DeflatedExplicitVRLittleEndian =
            new DicomTransferSyntax(DicomUID.DeflatedExplicitVRLittleEndian)
            {
                IsExplicitVR = true,
                IsDeflate = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Baseline (Process 1)</summary>
        public static DicomTransferSyntax JPEGProcess1 = new DicomTransferSyntax(DicomUID.JPEGBaseline1)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            IsLossy = true,
            LossyCompressionMethod = "ISO_10918_1",
            Endian = Endian.Little
        };

        /// <summary>JPEG Extended (Process 2 &amp; 4)</summary>
        public static DicomTransferSyntax JPEGProcess2_4 = new DicomTransferSyntax(DicomUID.JPEGExtended24)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            IsLossy = true,
            LossyCompressionMethod = "ISO_10918_1",
            Endian = Endian.Little
        };

        /// <summary>JPEG Extended (Process 3 &amp; 5) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess3_5Retired =
            new DicomTransferSyntax(DicomUID.JPEGExtended35RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod =
                    "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Spectral Selection, Non-Hierarchical (Process 6 &amp; 8) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess6_8Retired =
            new DicomTransferSyntax(DicomUID.JPEGSpectralSelectionNonHierarchical68RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Spectral Selection, Non-Hierarchical (Process 7 &amp; 9) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess7_9Retired =
            new DicomTransferSyntax(DicomUID.JPEGSpectralSelectionNonHierarchical79RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Full Progression, Non-Hierarchical (Process 10 &amp; 12) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess10_12Retired =
            new DicomTransferSyntax(DicomUID.JPEGFullProgressionNonHierarchical1012RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Full Progression, Non-Hierarchical (Process 11 &amp; 13) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess11_13Retired =
            new DicomTransferSyntax(DicomUID.JPEGFullProgressionNonHierarchical1113RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Non-Hierarchical (Process 14)</summary>
        public static DicomTransferSyntax JPEGProcess14 =
            new DicomTransferSyntax(DicomUID.JPEGLosslessNonHierarchical14)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Non-Hierarchical (Process 15) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess15Retired =
            new DicomTransferSyntax(DicomUID.JPEGLosslessNonHierarchical15RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Extended, Hierarchical (Process 16 &amp; 18) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess16_18Retired =
            new DicomTransferSyntax(DicomUID.JPEGExtendedHierarchical1618RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Extended, Hierarchical (Process 17 &amp; 19) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess17_19Retired =
            new DicomTransferSyntax(DicomUID.JPEGExtendedHierarchical1719RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Spectral Selection, Hierarchical (Process 20 &amp; 22) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess20_22Retired =
            new DicomTransferSyntax(DicomUID.JPEGSpectralSelectionHierarchical2022RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Spectral Selection, Hierarchical (Process 21 &amp; 23) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess21_23Retired =
            new DicomTransferSyntax(DicomUID.JPEGSpectralSelectionHierarchical2123RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Full Progression, Hierarchical (Process 24 &amp; 26) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess24_26Retired =
            new DicomTransferSyntax(DicomUID.JPEGFullProgressionHierarchical2426RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Full Progression, Hierarchical (Process 25 &amp; 27) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess25_27Retired =
            new DicomTransferSyntax(DicomUID.JPEGFullProgressionHierarchical2527RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Hierarchical (Process 28) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess28Retired =
            new DicomTransferSyntax(DicomUID.JPEGLosslessHierarchical28RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Hierarchical (Process 29) (Retired)</summary>
        public static DicomTransferSyntax JPEGProcess29Retired =
            new DicomTransferSyntax(DicomUID.JPEGLosslessHierarchical29RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1])</summary>
        public static DicomTransferSyntax JPEGProcess14SV1 = new DicomTransferSyntax(DicomUID.JPEGLossless)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            Endian = Endian.Little
        };

        /// <summary>JPEG-LS Lossless Image Compression</summary>
        public static DicomTransferSyntax JPEGLSLossless = new DicomTransferSyntax(DicomUID.JPEGLSLossless)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            Endian = Endian.Little
        };

        /// <summary>JPEG-LS Lossy (Near-Lossless) Image Compression</summary>
        public static DicomTransferSyntax JPEGLSNearLossless = new DicomTransferSyntax(DicomUID.JPEGLSLossyNearLossless)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            IsLossy = true,
            LossyCompressionMethod = "ISO_14495_1",
            Endian = Endian.Little
        };

        /// <summary>JPEG 2000 Lossless Image Compression</summary>
        public static DicomTransferSyntax JPEG2000Lossless = new DicomTransferSyntax(DicomUID.JPEG2000LosslessOnly)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            Endian = Endian.Little
        };

        /// <summary>JPEG 2000 Lossy Image Compression</summary>
        public static DicomTransferSyntax JPEG2000Lossy = new DicomTransferSyntax(DicomUID.JPEG2000)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            IsLossy = true,
            LossyCompressionMethod = "ISO_15444_1",
            Endian = Endian.Little
        };

        /// <summary>MPEG2 Main Profile @ Main Level</summary>
        public static DicomTransferSyntax MPEG2 = new DicomTransferSyntax(DicomUID.MPEG2)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            IsLossy = true,
            LossyCompressionMethod = "ISO_13818_2",
            Endian = Endian.Little
        };

        /// <summary>RLE Lossless</summary>
        public static DicomTransferSyntax RLELossless = new DicomTransferSyntax(DicomUID.RLELossless)
        {
            IsExplicitVR = true,
            IsEncapsulated = true,
            Endian = Endian.Little
        };

        #endregion

        #region Static Methods

        private static readonly IDictionary<DicomUID, DicomTransferSyntax> Entries =
            new Dictionary<DicomUID, DicomTransferSyntax>();

        static DicomTransferSyntax()
        {
            #region Load Transfer Syntax List

            Entries.Add(GEPrivateImplicitVRBigEndian.UID, GEPrivateImplicitVRBigEndian);
            Entries.Add(ImplicitVRLittleEndian.UID, ImplicitVRLittleEndian);
            Entries.Add(ExplicitVRLittleEndian.UID, ExplicitVRLittleEndian);
            Entries.Add(ExplicitVRBigEndian.UID, ExplicitVRBigEndian);
            Entries.Add(DeflatedExplicitVRLittleEndian.UID, DeflatedExplicitVRLittleEndian);
            Entries.Add(JPEGProcess1.UID, JPEGProcess1);
            Entries.Add(JPEGProcess2_4.UID, JPEGProcess2_4);
            Entries.Add(JPEGProcess3_5Retired.UID, JPEGProcess3_5Retired);
            Entries.Add(JPEGProcess6_8Retired.UID, JPEGProcess6_8Retired);
            Entries.Add(JPEGProcess7_9Retired.UID, JPEGProcess7_9Retired);
            Entries.Add(JPEGProcess10_12Retired.UID, JPEGProcess10_12Retired);
            Entries.Add(JPEGProcess11_13Retired.UID, JPEGProcess11_13Retired);
            Entries.Add(JPEGProcess14.UID, JPEGProcess14);
            Entries.Add(JPEGProcess15Retired.UID, JPEGProcess15Retired);
            Entries.Add(JPEGProcess16_18Retired.UID, JPEGProcess16_18Retired);
            Entries.Add(JPEGProcess17_19Retired.UID, JPEGProcess17_19Retired);
            Entries.Add(JPEGProcess20_22Retired.UID, JPEGProcess20_22Retired);
            Entries.Add(JPEGProcess21_23Retired.UID, JPEGProcess21_23Retired);
            Entries.Add(JPEGProcess24_26Retired.UID, JPEGProcess24_26Retired);
            Entries.Add(JPEGProcess25_27Retired.UID, JPEGProcess25_27Retired);
            Entries.Add(JPEGProcess28Retired.UID, JPEGProcess28Retired);
            Entries.Add(JPEGProcess29Retired.UID, JPEGProcess29Retired);
            Entries.Add(JPEGProcess14SV1.UID, JPEGProcess14SV1);
            Entries.Add(JPEGLSLossless.UID, JPEGLSLossless);
            Entries.Add(JPEGLSNearLossless.UID, JPEGLSNearLossless);
            Entries.Add(JPEG2000Lossless.UID, JPEG2000Lossless);
            Entries.Add(JPEG2000Lossy.UID, JPEG2000Lossy);
            Entries.Add(MPEG2.UID, MPEG2);
            Entries.Add(RLELossless.UID, RLELossless);

            #endregion
        }

        /// <summary>
        /// Get the transfer syntax from the specified <paramref name="uid"/> string.
        /// </summary>
        /// <param name="uid">String representing transfer syntax UID.</param>
        /// <returns><see cref="DicomTransferSyntax"/> object corresponding to <paramref name="uid"/>.</returns>
        /// <remarks><see cref="Parse"/> is a wrapper around <see cref="Lookup"/> for string based <paramref name="uid"/>.</remarks>
        public static DicomTransferSyntax Parse(string uid)
        {
            if (uid == null) throw new ArgumentNullException(nameof(uid));
            return Lookup(DicomUID.Parse(uid));
        }

        /// <summary>
        /// Get transfer syntax (pre-defined or built on-the-fly) for the specified <paramref name="uid"/>.
        /// </summary>
        /// <param name="uid">Transfer syntax UID.</param>
        /// <returns>Transfer syntax object, either pre-defined or built on-the-fly.</returns>
        /// <exception cref="DicomDataException">Thrown in the specified UID is not a transfer syntax type.</exception>
        /// <remarks>If transfer syntax object is built on-the-fly, value representation is set to Explicit, encapsulation is set to <code>true</code>
        /// and endianness is set to <see cref="IO.Endian.Little">Little Endian</see>.</remarks>
        public static DicomTransferSyntax Lookup(DicomUID uid)
        {
            if (uid == null) throw new ArgumentNullException(nameof(uid));

            DicomTransferSyntax tx;
            if (Entries.TryGetValue(uid, out tx)) return tx;

            if (uid.Type == DicomUidType.TransferSyntax)
            {
                return new DicomTransferSyntax(uid)
                {
                    IsRetired = uid.IsRetired,
                    IsExplicitVR = true,
                    IsEncapsulated = true,
                    Endian = Endian.Little
                };
            }

            throw new DicomDataException("UID: {0} is not a transfer syntax type.", uid);
        }

        #endregion
    }
}
