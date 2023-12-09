// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Implementation of the Retrieve Service Application Information field for the SOP Class Extended Negotiation
    /// Sub-item. See http://dicom.nema.org/medical/dicom/current/output/chtml/part04/sect_C.5.2.html
    /// for details on the Service Class Application Information field for C-MOVE SOP Classes.
    /// </summary>
    public class DicomCMoveApplicationInfo : DicomServiceApplicationInfo
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomCMoveApplicationInfo"/> class.
        /// </summary>
        public DicomCMoveApplicationInfo()
        {
            RelationalRetrieval = false;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCMoveApplicationInfo"/> class.
        /// </summary>
        /// <param name="rawApplicationInfo">The raw application info byte array.</param>
        public DicomCMoveApplicationInfo(byte[] rawApplicationInfo) : base(rawApplicationInfo)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCMoveApplicationInfo"/> class.
        /// </summary>
        /// <param name="options">The extended negotiation options for C-Move SOP classes.</param>
        public DicomCMoveApplicationInfo(DicomCMoveOption options)
        {
            RelationalRetrieval = options.HasFlag(DicomCMoveOption.RelationalRetrieval);
            EnhancedMultiFrameImageConversion = options.HasFlag(DicomCMoveOption.EnhancedMultiFrameImageConversion);
        }

        /// <summary>
        /// Gets or sets the Relational-retrieval flag.
        ///  true - relational-retrieval supported/requested
        ///  false - relational-retrieval not supported/requested
        /// </summary>
        public bool RelationalRetrieval
        {
            get => GetValueAsBoolean(1, false);
            set => AddOrUpdate(1, value);
        }

        /// <summary>
        /// Gets or sets the Enhanced Multi-Frame Image Conversion flag.
        ///  true - Query/Retrieve View supported/requested
        ///  false - Query/Retrieve View not supported/requested
        /// </summary>
        public bool EnhancedMultiFrameImageConversion
        {
            get => GetValueAsBoolean(2, false);
            set => AddOrUpdate(2, value);
        }
    }

    /// <summary>
    /// Specifies the options for C-Move SOP Class Extended Negotiation.
    /// </summary>
    [Flags]
    public enum DicomCMoveOption
    {
        /// <summary>
        /// None of the extended negotiation options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Request/indicate Relational-retrieval support.
        /// </summary>
        RelationalRetrieval = 2,

        /// <summary>
        /// Request/indicate Enhanced Multi-Frame Image Conversion support.
        /// </summary>
        EnhancedMultiFrameImageConversion = 4
    }
}
