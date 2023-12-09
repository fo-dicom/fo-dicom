// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Implementation of the Storage Service Application Information field for the SOP Class Extended Negotiation
    /// Sub-item. See http://dicom.nema.org/medical/dicom/current/output/chtml/part04/sect_B.3.html#sect_B.3.1 for details
    /// on the Service Class Application Information field for C-STORE SOP Classes.
    /// </summary>
    public class DicomCStoreApplicationInfo : DicomServiceApplicationInfo
    {
        public DicomCStoreApplicationInfo()
        {
            LevelOfSupport = DicomLevelOfSupport.NotApplicable;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCStoreApplicationInfo"/> class.
        /// </summary>
        /// <param name="rawApplicationInfo">The raw application info byte array.</param>
        public DicomCStoreApplicationInfo(byte[] rawApplicationInfo) : base(rawApplicationInfo)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCStoreApplicationInfo"/> class.
        /// </summary>
        /// <param name="levelOfSupport">Level of support flag.</param>
        /// <param name="levelOfDigitalSignatureSupport">Level of Digital Signature support flag.</param>
        /// <param name="elementCoercion">Element Coercion flag.</param>
        public DicomCStoreApplicationInfo(DicomLevelOfSupport levelOfSupport,
            DicomLevelOfDigitalSignatureSupport levelOfDigitalSignatureSupport, DicomElementCoercion elementCoercion)
        {
            LevelOfSupport = levelOfSupport;
            LevelOfDigitalSignatureSupport = levelOfDigitalSignatureSupport;
            ElementCoercion = elementCoercion;
        }

        /// <summary>
        /// Gets or sets the Level of support flag.
        /// </summary>
        public DicomLevelOfSupport LevelOfSupport
        {
            get => (DicomLevelOfSupport) GetValueForEnum<DicomLevelOfSupport>(1,
                (byte) DicomLevelOfSupport.NotApplicable);
            set => AddOrUpdate(1, (byte) value);
        }

        /// <summary>
        /// Gets or sets the Level of Digital Signature support flag.
        /// </summary>
        public DicomLevelOfDigitalSignatureSupport LevelOfDigitalSignatureSupport
        {
            get => (DicomLevelOfDigitalSignatureSupport) GetValueForEnum<DicomLevelOfDigitalSignatureSupport>(3,
                (byte) DicomLevelOfDigitalSignatureSupport.Unspecified);
            set => AddOrUpdate(3, (byte) value);
        }

        /// <summary>
        /// Gets or sets the Element Coercion flag.
        /// </summary>
        public DicomElementCoercion ElementCoercion
        {
            get => (DicomElementCoercion) GetValueForEnum<DicomElementCoercion>(5,
                (byte) DicomElementCoercion.NotApplicable);
            set => AddOrUpdate(5, (byte) value);
        }
    }

    public enum DicomLevelOfSupport
    {
        /// <summary>0 - Level 0 SCP</summary>
        Level0 = 0,

        /// <summary>1 - Level 1 SCP</summary>
        Level1 = 1,

        /// <summary>2 - Level 2 SCP</summary>
        Level2 = 2,

        /// <summary>3 - N/A Association-requester is SCU only</summary>
        NotApplicable = 3
    }

    public enum DicomLevelOfDigitalSignatureSupport
    {
        /// <summary>0 - The signature level is unspecified, the AE is an SCU only, or the AE is not a level 2 SCP</summary>
        Unspecified = 0,

        /// <summary>1 - Signature level 1</summary>
        Level1 = 1,

        /// <summary>2 - Signature level 2</summary>
        Level2 = 2,

        /// <summary>3 - Signature level 3</summary>
        Level3 = 3
    }

    public enum DicomElementCoercion
    {
        /// <summary>0 - Does not coerce any Data Element</summary>
        NoCoercion = 0,

        /// <summary>1 - May coerce Data Elements</summary>
        AllowCoercion = 1,

        /// <summary>2 - N/A - Association-requester is SCU only</summary>
        NotApplicable = 2
    }
}
