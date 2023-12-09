// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Printing
{
    /// <summary>
    /// Convenience representation of a Presentation LUT Information Object.
    /// 
    /// For more information, see http://dicom.nema.org/medical/dicom/current/output/chtml/part03/sect_B.18.html .
    /// </summary>
    public class PresentationLut : DicomDataset
    {
        #region FIELDS

        /// <summary>
        /// Gets the Presentation LUT SOP Class UID.
        /// </summary>
        public static readonly DicomUID SopClassUid = DicomUID.PresentationLUT;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationLut"/> class.
        /// </summary>
        /// <param name="sopInstance">SOP Instance UID associated with the Presentation LUT information object. If <code>null</code>,
        /// a UID will be automatically generated.</param>
        public PresentationLut(DicomUID sopInstance = null)
        {
            InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            SopInstanceUid = string.IsNullOrEmpty(sopInstance?.UID) ? DicomUID.Generate() : sopInstance;

            CreateLutSequence();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationLut"/> class.
        /// </summary>
        /// <param name="sopInstance">SOP Instance UID associated with the Presentation LUT information object. If <code>null</code>,
        /// a UID will be automatically generated.</param>
        /// <param name="dataset">Dataset presumed to contain Presentation LUT data.</param>
        public PresentationLut(DicomUID sopInstance, DicomDataset dataset)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException(nameof(dataset));
            }
            dataset.CopyTo(this);

            if (!dataset.Contains(DicomTag.PresentationLUTSequence)) CreateLutSequence();

            SopInstanceUid = string.IsNullOrEmpty(sopInstance?.UID) ? DicomUID.Generate() : sopInstance;
            AddOrUpdate(DicomTag.SOPInstanceUID, SopInstanceUid);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the SOP Instance UID of the Presentation LUT object.
        /// </summary>
        public DicomUID SopInstanceUid { get; }

        /// <summary>
        /// Gets the Presentation LUT Sequence dataset.
        /// 
        /// For more information, see http://dicom.nema.org/medical/dicom/current/output/chtml/part03/sect_C.11.4.html .
        /// </summary>
        public DicomDataset LutSequence
        {
            get
            {
                var lutSequence = GetSequence(DicomTag.PresentationLUTSequence);
                return lutSequence.Items[0];
            }
        }

        /// <summary>
        /// Gets or sets the LUT Descriptor, i.e. the format of the LUT data.
        /// 
        /// If defined, the LUT Descriptor contains three values; number of entries in the lookup table, first input value mapped (always 0),
        /// and number of bits for each entry in the <see cref="LutData">LUT Data</see> (between 10 and 16, inclusive).
        /// 
        /// For more details, see http://dicom.nema.org/medical/dicom/current/output/chtml/part03/sect_C.11.4.html#sect_C.11.4.1
        /// </summary>
        public ushort[] LutDescriptor
        {
            get => LutSequence.TryGetValues(DicomTag.LUTDescriptor, out ushort[] dummy) ? dummy : new ushort[0];
            set => LutSequence.AddOrUpdate(DicomTag.LUTDescriptor, value);
        }

        /// <summary>
        /// Gets or sets a free-form text explanation of the meaning of the LUT.
        /// </summary>
        public string LutExplanation
        {
            get => LutSequence.GetSingleValueOrDefault(DicomTag.LUTExplanation, string.Empty);
            set => LutSequence.AddOrUpdate(DicomTag.LUTExplanation, value);
        }

        /// <summary>
        /// Gets or sets the LUT entry values (P-Values).
        /// </summary>
        public ushort[] LutData
        {
            get => LutSequence.TryGetValues(DicomTag.LUTData, out ushort[] dummy) ? dummy : new ushort[0];
            set => LutSequence.AddOrUpdate(DicomTag.LUTData, value);
        }

        /// <summary>
        /// Gets or sets the shape of the Presentation LUT. Enumerated values 'IDENTITY' and 'LIN OD'.
        /// </summary>
        public string PresentationLutShape
        {
            get => GetSingleValueOrDefault(DicomTag.PresentationLUTShape, string.Empty);
            set => AddOrUpdate(DicomTag.PresentationLUTShape, value);
        }

        #endregion

        #region METHODS

        private void CreateLutSequence()
        {
            var lutSequence = new DicomSequence(DicomTag.PresentationLUTSequence, new DicomDataset());
            Add(lutSequence);
        }

        #endregion
    }
}
