// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    /// <summary>
    /// Implementation of the root query retrieve info find sub-item.
    /// </summary>
    public class RootQueryRetrieveInfoFind : IExtendedNegotiationSubItem
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RootQueryRetrieveInfoFind"/> class.
        /// </summary>
        /// <param name="relationalQueries">Relational queries flag.</param>
        /// <param name="dateTimeMatching">Date time matching flag.</param>
        /// <param name="fuzzySemanticMatching">Fuzzy semantic matching flag.</param>
        /// <param name="timezoneQueryAdjustment">Time zone query adjustment flag.</param>
        /// <param name="enhancedMultiFrameImageConversion">Enhanced multi-frame image conversion flag.</param>
        public RootQueryRetrieveInfoFind(
            byte? relationalQueries,
            byte? dateTimeMatching,
            byte? fuzzySemanticMatching,
            byte? timezoneQueryAdjustment,
            byte? enhancedMultiFrameImageConversion)
        {
            RelationalQueries = relationalQueries;
            DateTimeMatching = dateTimeMatching;
            FuzzySemanticMatching = fuzzySemanticMatching;
            TimezoneQueryAdjustment = timezoneQueryAdjustment;
            EnhancedMultiFrameImageConversion = enhancedMultiFrameImageConversion;
        }

        /// <summary>
        /// Gets or sets the relational queries.
        /// </summary>
        public byte? RelationalQueries { get; set; }

        /// <summary>
        /// Gets or sets the date time matching.
        /// </summary>
        public byte? DateTimeMatching { get; set; }

        /// <summary>
        /// Gets or sets the fuzzy semantic matching.
        /// </summary>
        public byte? FuzzySemanticMatching { get; set; }

        /// <summary>
        /// Gets or sets the time zone query adjustment.
        /// </summary>
        public byte? TimezoneQueryAdjustment { get; set; }

        /// <summary>
        /// Gets or sets the enhanced multi frame image conversion.
        /// </summary>
        public byte? EnhancedMultiFrameImageConversion { get; set; }

        /// <summary>
        /// Factory method for creating a <see cref="RootQueryRetrieveInfoFind"/> instance.
        /// </summary>
        /// <param name="raw">Raw PDU.</param>
        /// <param name="itemSize">Item size.</param>
        /// <param name="bytesRead">Bytes read.</param>
        /// <returns>The created <see cref="RootQueryRetrieveInfoFind"/> instance.</returns>
        public static RootQueryRetrieveInfoFind Create(RawPDU raw, int itemSize, out int bytesRead)
        {
            byte? relationalQueries = null;
            byte? dateTimeMatching = null;
            byte? fuzzySemanticMatching = null;
            byte? timezoneQueryAdjustment = null;
            byte? enhancedMultiFrameImageConversion = null;

            bytesRead = 0;
            if (itemSize > 0)
            {
                relationalQueries = raw.ReadByte("Relational-queries");
                bytesRead++;

                if (itemSize > 1)
                {
                    dateTimeMatching = raw.ReadByte("Date-Time matching");
                    bytesRead++;

                    if (itemSize > 2)
                    {
                        fuzzySemanticMatching = raw.ReadByte("Fuzzy semantic matching");
                        bytesRead++;

                        if (itemSize > 3)
                        {
                            timezoneQueryAdjustment = raw.ReadByte("Timezone query adjustment");
                            bytesRead++;

                            if (itemSize > 4)
                            {
                                enhancedMultiFrameImageConversion = raw.ReadByte(
                                    "Enhanced Multi-Frame Image Conversion");
                                bytesRead++;
                            }
                        }
                    }
                }
            }

            return new RootQueryRetrieveInfoFind(
                relationalQueries,
                dateTimeMatching,
                fuzzySemanticMatching,
                timezoneQueryAdjustment,
                enhancedMultiFrameImageConversion);
        }

        /// <summary>
        /// Write PDU data.
        /// </summary>
        /// <param name="pdu">PDU data to write.</param>
        public void Write(RawPDU pdu)
        {
            if (RelationalQueries.HasValue)
            {
                pdu.Write("Relational-queries", RelationalQueries.Value);

                if (DateTimeMatching.HasValue)
                {
                    pdu.Write("Date-Time matching", DateTimeMatching.Value);
                    if (FuzzySemanticMatching.HasValue)
                    {
                        pdu.Write("Fuzzy semantic matching", FuzzySemanticMatching.Value);
                        if (TimezoneQueryAdjustment.HasValue)
                        {
                            pdu.Write("Timezone query adjustment", TimezoneQueryAdjustment.Value);

                            if (EnhancedMultiFrameImageConversion.HasValue)
                            {
                                pdu.Write(
                                    "Enhanced Multi-Frame Image Conversion",
                                    EnhancedMultiFrameImageConversion.Value);
                            }
                        }
                    }
                }
            }
        }
    }
}
