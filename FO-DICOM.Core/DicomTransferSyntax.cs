// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using System;
using System.Collections.Generic;

namespace FellowOakDicom
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

        public override string ToString() => UID.Name;

        public override bool Equals(object obj)
        {
            return obj switch
            {
                DicomTransferSyntax ts => ts.UID.Equals(UID),
                DicomUID uid => uid.Equals(UID),
                string str => UID.UID.Equals(str),
                _ => false,
            };
        }

        public override int GetHashCode() => UID.GetHashCode();

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
        public static readonly DicomTransferSyntax ImplicitVRBigEndian =
            new DicomTransferSyntax(new DicomUID(DicomUID.ExplicitVRBigEndianRETIRED.UID + ".123456",
                "Implicit VR Big Endian", DicomUidType.TransferSyntax))
            {
                IsExplicitVR = false,
                Endian = Endian.Big
            };

        /// <summary>GE Private Implicit VR Big Endian</summary>
        /// <remarks>Same as Implicit VR Little Endian except for big endian pixel data.</remarks>
        public static readonly DicomTransferSyntax GEPrivateImplicitVRBigEndian =
            new DicomTransferSyntax(DicomUID.GEPrivateImplicitVRBigEndian)
            {
                IsExplicitVR = false,
                Endian = Endian.Little,
                SwapPixelData = true
            };

        /// <summary>Implicit VR Little Endian</summary>
        public static readonly DicomTransferSyntax ImplicitVRLittleEndian =
            new DicomTransferSyntax(DicomUID.ImplicitVRLittleEndian)
            {
                Endian = Endian.Little
            };

        /// <summary>Explicit VR Little Endian</summary>
        public static readonly DicomTransferSyntax ExplicitVRLittleEndian =
            new DicomTransferSyntax(DicomUID.ExplicitVRLittleEndian)
            {
                IsExplicitVR = true,
                Endian = Endian.Little
            };

        /// <summary>Explicit VR Big Endian</summary>
        public static readonly DicomTransferSyntax ExplicitVRBigEndian =
            new DicomTransferSyntax(DicomUID.ExplicitVRBigEndianRETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                Endian = Endian.Big
            };

        /// <summary>Deflated Explicit VR Little Endian</summary>
        public static readonly DicomTransferSyntax DeflatedExplicitVRLittleEndian =
            new DicomTransferSyntax(DicomUID.DeflatedExplicitVRLittleEndian)
            {
                IsExplicitVR = true,
                IsDeflate = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Baseline (Process 1)</summary>
        public static readonly DicomTransferSyntax JPEGProcess1 =
            new DicomTransferSyntax(DicomUID.JPEGBaseline8Bit)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Extended (Process 2 &amp; 4)</summary>
        public static readonly DicomTransferSyntax JPEGProcess2_4 =
            new DicomTransferSyntax(DicomUID.JPEGExtended12Bit)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_10918_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG Extended (Process 3 &amp; 5) (Retired)</summary>
        public static readonly DicomTransferSyntax JPEGProcess3_5Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess6_8Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess7_9Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess10_12Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess11_13Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess14 =
            new DicomTransferSyntax(DicomUID.JPEGLossless)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Non-Hierarchical (Process 15) (Retired)</summary>
        public static readonly DicomTransferSyntax JPEGProcess15Retired =
            new DicomTransferSyntax(DicomUID.JPEGLosslessNonHierarchical15RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Extended, Hierarchical (Process 16 &amp; 18) (Retired)</summary>
        public static readonly DicomTransferSyntax JPEGProcess16_18Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess17_19Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess20_22Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess21_23Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess24_26Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess25_27Retired =
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
        public static readonly DicomTransferSyntax JPEGProcess28Retired =
            new DicomTransferSyntax(DicomUID.JPEGLosslessHierarchical28RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Hierarchical (Process 29) (Retired)</summary>
        public static readonly DicomTransferSyntax JPEGProcess29Retired =
            new DicomTransferSyntax(DicomUID.JPEGLosslessHierarchical29RETIRED)
            {
                IsRetired = true,
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1])</summary>
        public static readonly DicomTransferSyntax JPEGProcess14SV1 =
            new DicomTransferSyntax(DicomUID.JPEGLosslessSV1)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG-LS Lossless Image Compression</summary>
        public static readonly DicomTransferSyntax JPEGLSLossless =
            new DicomTransferSyntax(DicomUID.JPEGLSLossless)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG-LS Lossy (Near-Lossless) Image Compression</summary>
        public static readonly DicomTransferSyntax JPEGLSNearLossless =
            new DicomTransferSyntax(DicomUID.JPEGLSNearLossless)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14495_1",
                Endian = Endian.Little
            };

        /// <summary>JPEG 2000 Lossless Image Compression</summary>
        public static readonly DicomTransferSyntax JPEG2000Lossless =
            new DicomTransferSyntax(DicomUID.JPEG2000Lossless)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>JPEG 2000 Lossy Image Compression</summary>
        public static readonly DicomTransferSyntax JPEG2000Lossy =
            new DicomTransferSyntax(DicomUID.JPEG2000)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_15444_1",
                Endian = Endian.Little
            };

        ///<summary>JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)</summary>
        public static readonly DicomTransferSyntax JPEG2000Part2MultiComponentLosslessOnly =
            new DicomTransferSyntax(DicomUID.JPEG2000MCLossless)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        ///<summary>JPEG 2000 Part 2 Multi-component Image Compression</summary>
        public static readonly DicomTransferSyntax JPEG2000Part2MultiComponent =
            new DicomTransferSyntax(DicomUID.JPEG2000MC)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_15444_2",
                Endian = Endian.Little
            };

        ///<summary>JPIP Referenced</summary>
        public static readonly DicomTransferSyntax JPIPReferenced =
            new DicomTransferSyntax(DicomUID.JPIPReferenced)
            {
                IsExplicitVR = true,
                Endian = Endian.Little
            };

        ///<summary>JPIP Referenced Deflate</summary>
        public static readonly DicomTransferSyntax JPIPReferencedDeflate =
            new DicomTransferSyntax(DicomUID.JPIPReferencedDeflate)
            {
                IsExplicitVR = true,
                IsDeflate = true,
                Endian = Endian.Little
            };

        /// <summary>MPEG2 Main Profile @ Main Level</summary>
        public static readonly DicomTransferSyntax MPEG2 =
            new DicomTransferSyntax(DicomUID.MPEG2MPML)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_13818_2",
                Endian = Endian.Little
            };


        /// <summary>Fragmentable MPEG2 Main Profile @ Main Level</summary>
        public static readonly DicomTransferSyntax FragmentableMPEG2 =
            new DicomTransferSyntax(DicomUID.MPEG2MPMLF)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_13818_2",
                Endian = Endian.Little
            };

        ///<summary>MPEG2 Main Profile / High Level</summary>
        public static readonly DicomTransferSyntax MPEG2MainProfileHighLevel =
            new DicomTransferSyntax(DicomUID.MPEG2MPHL)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_13818_2",
                Endian = Endian.Little
            };

        ///<summary>Fragmentable MPEG2 Main Profile / High Level</summary>
        public static readonly DicomTransferSyntax FragmentableMPEG2MainProfileHighLevel =
            new DicomTransferSyntax(DicomUID.MPEG2MPHLF)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_13818_2",
                Endian = Endian.Little
            };

        ///<summary>MPEG-4 AVC/H.264 High Profile / Level 4.1</summary>
        public static readonly DicomTransferSyntax MPEG4AVCH264HighProfileLevel41 =
            new DicomTransferSyntax(DicomUID.MPEG4HP41)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.1</summary>
        public static readonly DicomTransferSyntax FragmentableMPEG4AVCH264HighProfileLevel41 =
            new DicomTransferSyntax(DicomUID.MPEG4HP41F)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1</summary>
        public static readonly DicomTransferSyntax MPEG4AVCH264BDCompatibleHighProfileLevel41 =
            new DicomTransferSyntax(DicomUID.MPEG4HP41BD)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>Fragmentable MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1</summary>
        public static readonly DicomTransferSyntax FragmentableMPEG4AVCH264BDCompatibleHighProfileLevel41 =
            new DicomTransferSyntax(DicomUID.MPEG4HP41BDF)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video</summary>
        public static readonly DicomTransferSyntax MPEG4AVCH264HighProfileLevel42For2DVideo =
            new DicomTransferSyntax(DicomUID.MPEG4HP422D)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video</summary>
        public static readonly DicomTransferSyntax FragmentableMPEG4AVCH264HighProfileLevel42For2DVideo =
            new DicomTransferSyntax(DicomUID.MPEG4HP422DF)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video</summary>
        public static readonly DicomTransferSyntax MPEG4AVCH264HighProfileLevel42For3DVideo =
            new DicomTransferSyntax(DicomUID.MPEG4HP423D)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video</summary>
        public static readonly DicomTransferSyntax FragmentableMPEG4AVCH264HighProfileLevel42For3DVideo =
            new DicomTransferSyntax(DicomUID.MPEG4HP423DF)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2</summary>
        public static readonly DicomTransferSyntax MPEG4AVCH264StereoHighProfileLevel42 =
            new DicomTransferSyntax(DicomUID.MPEG4HP42STEREO)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>Fragmentable MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2</summary>
        public static readonly DicomTransferSyntax FragmentableMPEG4AVCH264StereoHighProfileLevel42 =
            new DicomTransferSyntax(DicomUID.MPEG4HP42STEREOF)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_14496_10",
                Endian = Endian.Little
            };

        ///<summary>HEVC/H.265 Main Profile / Level 5.1</summary>
        public static readonly DicomTransferSyntax HEVCH265MainProfileLevel51 =
            new DicomTransferSyntax(DicomUID.HEVCMP51)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_23008_2",
                Endian = Endian.Little
            };

        ///<summary>HEVC/H.265 Main 10 Profile / Level 5.1</summary>
        public static readonly DicomTransferSyntax HEVCH265Main10ProfileLevel51 =
            new DicomTransferSyntax(DicomUID.HEVCM10P51)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_23008_2",
                Endian = Endian.Little
            };

        /// <summary>High-Throughput JPEG 2000 Image Compression (Lossless Only)</summary>
        public static readonly DicomTransferSyntax HTJ2KLossless =
            new DicomTransferSyntax(DicomUID.HTJ2KLossless)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        /// <summary>High-Throughput JPEG 2000 with RPCL Options Image Compression (Lossless Only)</summary>
        public static readonly DicomTransferSyntax HTJ2KLosslessRPCL =
            new DicomTransferSyntax(DicomUID.HTJ2KLosslessRPCL)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        ///<summary>High-Throughput JPEG 2000 Image Compression</summary>
        public static readonly DicomTransferSyntax HTJ2K =
            new DicomTransferSyntax(DicomUID.HTJ2K)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                IsLossy = true,
                LossyCompressionMethod = "ISO_15444_15",
                Endian = Endian.Little
            };

        ///<summary>JPIP HTJ2K Referenced</summary>
        public static readonly DicomTransferSyntax JPIPHTJ2KReferenced =
            new DicomTransferSyntax(DicomUID.JPIPHTJ2KReferenced)
            {
                IsExplicitVR = true,
                Endian = Endian.Little
            };

        ///<summary>JPIP HTJ2K Referenced Deflate</summary>
        public static readonly DicomTransferSyntax JPIPHTJ2KReferencedDeflate =
            new DicomTransferSyntax(DicomUID.JPIPHTJ2KReferencedDeflate)
            {
                IsExplicitVR = true,
                IsDeflate = true,
                Endian = Endian.Little
            };


        /// <summary>RLE Lossless</summary>
        public static readonly DicomTransferSyntax RLELossless =
            new DicomTransferSyntax(DicomUID.RLELossless)
            {
                IsExplicitVR = true,
                IsEncapsulated = true,
                Endian = Endian.Little
            };

        ///<summary>RFC 2557 MIME encapsulation</summary>
        public static readonly DicomTransferSyntax RFC2557MIMEEncapsulation =
            new DicomTransferSyntax(DicomUID.RFC2557MIMEEncapsulationRETIRED)
            {
                IsExplicitVR = true,
                Endian = Endian.Little
            };

        ///<summary>XML Encoding</summary>
        public static readonly DicomTransferSyntax XMLEncoding =
            new DicomTransferSyntax(DicomUID.XMLEncodingRETIRED)
            {
                IsExplicitVR = true,
                Endian = Endian.Little
            };

        ///<summary>Papyrus 3 Implicit VR Little Endian (Retired)</summary>
        public static readonly DicomTransferSyntax Papyrus3ImplicitVRLittleEndianRetired =
            new DicomTransferSyntax(DicomUID.Papyrus3ImplicitVRLittleEndianRETIRED)
            {
                IsExplicitVR = false,
                IsRetired = true,
                Endian = Endian.Little
            };

        #endregion

        #region Static Methods

        private static readonly IDictionary<DicomUID, DicomTransferSyntax> Entries = new Dictionary<DicomUID, DicomTransferSyntax>();

        public static IEnumerable<DicomTransferSyntax> KnownEntries => Entries.Values;

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
            Entries.Add(JPEG2000Part2MultiComponentLosslessOnly.UID, JPEG2000Part2MultiComponentLosslessOnly);
            Entries.Add(JPEG2000Part2MultiComponent.UID, JPEG2000Part2MultiComponent);
            Entries.Add(JPIPReferenced.UID, JPIPReferenced);
            Entries.Add(JPIPReferencedDeflate.UID, JPIPReferencedDeflate);
            Entries.Add(MPEG2.UID, MPEG2);
            Entries.Add(FragmentableMPEG2.UID, FragmentableMPEG2);
            Entries.Add(MPEG2MainProfileHighLevel.UID, MPEG2MainProfileHighLevel);
            Entries.Add(FragmentableMPEG2MainProfileHighLevel.UID, FragmentableMPEG2MainProfileHighLevel);
            Entries.Add(MPEG4AVCH264HighProfileLevel41.UID, MPEG4AVCH264HighProfileLevel41);
            Entries.Add(FragmentableMPEG4AVCH264HighProfileLevel41.UID, FragmentableMPEG4AVCH264HighProfileLevel41);
            Entries.Add(MPEG4AVCH264BDCompatibleHighProfileLevel41.UID, MPEG4AVCH264BDCompatibleHighProfileLevel41);
            Entries.Add(FragmentableMPEG4AVCH264BDCompatibleHighProfileLevel41.UID, FragmentableMPEG4AVCH264BDCompatibleHighProfileLevel41);
            Entries.Add(MPEG4AVCH264HighProfileLevel42For2DVideo.UID, MPEG4AVCH264HighProfileLevel42For2DVideo);
            Entries.Add(FragmentableMPEG4AVCH264HighProfileLevel42For2DVideo.UID, FragmentableMPEG4AVCH264HighProfileLevel42For2DVideo);
            Entries.Add(MPEG4AVCH264HighProfileLevel42For3DVideo.UID, MPEG4AVCH264HighProfileLevel42For3DVideo);
            Entries.Add(FragmentableMPEG4AVCH264HighProfileLevel42For3DVideo.UID, FragmentableMPEG4AVCH264HighProfileLevel42For3DVideo);
            Entries.Add(MPEG4AVCH264StereoHighProfileLevel42.UID, MPEG4AVCH264StereoHighProfileLevel42);
            Entries.Add(FragmentableMPEG4AVCH264StereoHighProfileLevel42.UID, FragmentableMPEG4AVCH264StereoHighProfileLevel42);
            Entries.Add(HEVCH265MainProfileLevel51.UID, HEVCH265MainProfileLevel51);
            Entries.Add(HEVCH265Main10ProfileLevel51.UID, HEVCH265Main10ProfileLevel51);
            Entries.Add(HTJ2KLossless.UID, HTJ2KLossless);
            Entries.Add(HTJ2KLosslessRPCL.UID, HTJ2KLosslessRPCL);
            Entries.Add(HTJ2K.UID, HTJ2K);
            Entries.Add(JPIPHTJ2KReferenced.UID, JPIPHTJ2KReferenced);
            Entries.Add(JPIPHTJ2KReferencedDeflate.UID, JPIPHTJ2KReferencedDeflate);
            Entries.Add(RLELossless.UID, RLELossless);
            Entries.Add(RFC2557MIMEEncapsulation.UID, RFC2557MIMEEncapsulation);
            Entries.Add(XMLEncoding.UID, XMLEncoding);
            Entries.Add(Papyrus3ImplicitVRLittleEndianRetired.UID, Papyrus3ImplicitVRLittleEndianRetired);

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
            return Lookup(DicomUID.Parse(s: uid, type: DicomUidType.TransferSyntax));
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

            if (Entries.TryGetValue(uid, out DicomTransferSyntax tx)) return tx;

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

            throw new DicomDataException($"UID: {uid} is not a transfer syntax type.");
        }

        /// <summary>
        /// register transfer syntax into internal dictionary, assuming Little Endian and Explicit VR.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DicomTransferSyntax Register(DicomUID uid)
        {
            return Register(uid, Endian.Little, true, true);
        }

        /// <summary>
        /// register transfer syntax into internal dictionary.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="endian"></param>
        /// <param name="isExplicitVR"></param>
        /// <returns></returns>
        public static DicomTransferSyntax Register(DicomUID uid, Endian endian, bool isExplicitVR = true, bool isEncapsulated = true)
        {
            lock (Entries)
            {
                //  return cached transfer syntax from internal dictionary.
                if (Entries.TryGetValue(uid, out DicomTransferSyntax tx))
                {
                    return tx;
                }

                if (uid == null)
                {
                    throw new ArgumentNullException(nameof(uid));
                }

                if (uid.Type != DicomUidType.TransferSyntax)
                {
                    throw new DicomDataException($"UID: {uid} is not a transfer syntax type.");
                }

                //  cache transfer syntax into internal dictionary.
                tx = new DicomTransferSyntax(uid)
                {
                    IsRetired = uid.IsRetired,
                    IsExplicitVR = isExplicitVR,
                    IsEncapsulated = isEncapsulated,
                    Endian = endian
                };
                Entries.Add(uid, tx);

                return tx;
            }
        }

        /// <summary>
        /// unregister transfer syntax from internal dictionary.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool Unregister(DicomUID uid)
        {
            lock (Entries)
            {
                return Entries.Remove(uid);
            }
        }

        /// <summary>
        /// unregister transfer syntax from internal dictionary.
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool Unregister(DicomTransferSyntax ts)
        {
            return Unregister(ts.UID);
        }

        /// <summary>
        /// Query DicomTransferSyntax by UID. returns null if not found.
        /// </summary>
        public static DicomTransferSyntax Query(DicomUID uid)
        {
            lock (Entries)
            {
                Entries.TryGetValue(uid, out DicomTransferSyntax ts);
                return ts;
            }
        }

        #endregion
    }
}
