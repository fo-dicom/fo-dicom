// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Implementation of the Query Service Application Information field for the SOP Class Extended Negotiation
    /// Sub-item. See http://dicom.nema.org/medical/dicom/current/output/chtml/part04/sect_C.5.html#sect_C.5.1.1
    /// for details on the Service Class Application Information field for C-FIND SOP Classes.
    /// </summary>
    public class DicomCFindApplicationInfo : DicomServiceApplicationInfo
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindApplicationInfo"/> class.
        /// </summary>
        public DicomCFindApplicationInfo()
        {
            RelationalQueries = false;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindApplicationInfo"/> class.
        /// </summary>
        /// <param name="rawApplicationInfo">The raw application info byte array.</param>
        public DicomCFindApplicationInfo(byte[] rawApplicationInfo) : base(rawApplicationInfo)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindApplicationInfo"/> class.
        /// </summary>
        /// <param name="options">The extended negotiation options for the C-Find SOP classes.</param>
        public DicomCFindApplicationInfo(DicomCFindOption options)
        {
            RelationalQueries = options.HasFlag(DicomCFindOption.RelationalQueries);
            DateTimeMatching = options.HasFlag(DicomCFindOption.DateTimeMatching);
            FuzzySemanticMatching = options.HasFlag(DicomCFindOption.FuzzySemanticMatching);
            TimezoneQueryAdjustment = options.HasFlag(DicomCFindOption.TimezoneQueryAdjustment);
            EnhancedMultiFrameImageConversion = options.HasFlag(DicomCFindOption.EnhancedMultiFrameImageConversion);
        }

        /// <summary>
        /// Gets or sets the Relational-Queries flag.
        ///  true - relational queries supported/requested
        ///  false - relational queries not supported/requested
        /// </summary>
        public bool RelationalQueries
        {
            get => GetValueAsBoolean(1, false);
            set => AddOrUpdate(1, value);
        }

        /// <summary>
        /// Gets or sets the Date-time Matching flag.
        ///  true - combined matching supported/requested
        ///  false - combined matching not supported/requested
        /// </summary>
        public bool DateTimeMatching
        {
            get => GetValueAsBoolean(2, false);
            set => AddOrUpdate(2, value);
        }

        /// <summary>
        /// Gets or sets the Fuzzy Semantic Matching Of Person Names flag.
        ///  true - fuzzy semantic matching supported/requested
        ///  false - fuzzy semantic matching not supported/requested
        /// </summary>
        public bool FuzzySemanticMatching
        {
            get => GetValueAsBoolean(3, false);
            set => AddOrUpdate(3, value);
        }

        /// <summary>
        /// Gets or sets the Timezone Query Adjustment flag.
        ///  true - Timezone query adjustment supported/requested
        ///  false - Timezone query adjustment not supported/requested
        /// </summary>
        public bool TimezoneQueryAdjustment
        {
            get => GetValueAsBoolean(4, false);
            set => AddOrUpdate(4, value);
        }

        /// <summary>
        /// Gets or sets the Enhanced Multi-Frame Image Conversion flag.
        ///  true - Query/Retrieve View supported/requested
        ///  false - Query/Retrieve View not supported/requested
        /// </summary>
        public bool EnhancedMultiFrameImageConversion
        {
            get => GetValueAsBoolean(5, false);
            set => AddOrUpdate(5, value);
        }

        public static DicomCFindApplicationInfo CreateForWorklistQuery(bool fuzzySemanticMatching,
            bool timezoneQueryAdjustment)
        {
            return new DicomCFindApplicationInfo
            {
                RelationalQueries = true,
                DateTimeMatching = true,
                FuzzySemanticMatching = fuzzySemanticMatching,
                TimezoneQueryAdjustment = timezoneQueryAdjustment
            };
        }
    }

    /// <summary>
    /// Specifies the options for C-Find SOP Class Extended Negotiation.
    /// </summary>
    [Flags]
    public enum DicomCFindOption
    {
        /// <summary>
        /// None of the extended negotiation options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Request/indicate Relational-Queries support.
        /// </summary>
        RelationalQueries = 2,

        /// <summary>
        /// Request/indicate support for Date-Time combined matching.
        /// </summary>
        DateTimeMatching = 4,

        /// <summary>
        /// Request/indicate support for Fuzzy Semantic Matching Of Person Names.
        /// </summary>
        FuzzySemanticMatching = 8,

        /// <summary>
        /// Request/indicate Timezone Query Adjustment support.
        /// </summary>
        TimezoneQueryAdjustment = 16,

        /// <summary>
        /// Request/indicate Enhanced Multi-Frame Image Conversion support.
        /// </summary>
        EnhancedMultiFrameImageConversion = 32
    }
}
