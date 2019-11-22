// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Options to control the behavior of the <see cref="DicomService"/> base class.
    /// </summary>
    public class DicomServiceOptions
    {
        #region INNER TYPES

        /// <summary>Default options for use with the <see cref="DicomService"/> base class.</summary>
        public static class Default
        {
            public static bool LogDataPDUs { get; internal set; } = false;
            public static bool LogDimseDatasets { get; internal set; } = false;
            public static bool UseRemoteAEForLogName { get; internal set; } = false;
            public static uint MaxCommandBuffer { get; internal set; } = 1 * 1024; //1KB
            public static uint MaxDataBuffer { get; internal set; } = 1 * 1024 * 1024; //1MB
            public static bool IgnoreSslPolicyErrors { get; internal set; } = false;
            public static bool TcpNoDelay { get; internal set; } = true;
            public static int MaxPDVsPerPDU { get; internal set; } = 0;
            public static int MaxClientsAllowed { get; internal set; } = 0;
            public static bool IgnoreUnsupportedTransferSyntaxChange { get; internal set; } = false;
            public static TimeSpan? RequestTimeout { get; internal set; } = null;
            public static uint MaxPDULength { get; internal set; } = 262144; // 256 Kb
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>Constructor</summary>
        public DicomServiceOptions()
        {
            LogDataPDUs = Default.LogDataPDUs;
            LogDimseDatasets = Default.LogDimseDatasets;
            UseRemoteAEForLogName = Default.UseRemoteAEForLogName;
            MaxCommandBuffer = Default.MaxCommandBuffer;
            MaxDataBuffer = Default.MaxDataBuffer;
            IgnoreSslPolicyErrors = Default.IgnoreSslPolicyErrors;
            TcpNoDelay = Default.TcpNoDelay;
            MaxPDVsPerPDU = Default.MaxPDVsPerPDU;
            MaxClientsAllowed = Default.MaxClientsAllowed;
            IgnoreUnsupportedTransferSyntaxChange = Default.IgnoreUnsupportedTransferSyntaxChange;
            MaxPDULength = Default.MaxPDULength;
            RequestTimeout = Default.RequestTimeout;
        }

        #endregion

        #region PROPERTIES

        /// <summary>Gets or sets whether or not to write message to log for each P-Data-TF PDU sent or received.</summary>
        public bool LogDataPDUs { get; set; }

        /// <summary>Gets or sets whether or not to write command and data datasets to log.</summary>
        public bool LogDimseDatasets { get; set; }

        /// <summary>Gets or sets whether or not to use the AE Title of the remote host as the log name.</summary>
        public bool UseRemoteAEForLogName { get; set; }

        /// <summary>Gets or sets maximum buffer length for command PDVs when generating P-Data-TF PDUs.</summary>
        public uint MaxCommandBuffer { get; set; }

        /// <summary>Gets or sets maximum buffer length for data PDVs when generating P-Data-TF PDUs.</summary>
        public uint MaxDataBuffer { get; set; }

        /// <summary>Gets or sets whether DICOM client should ignore SSL certificate errors.</summary>
        public bool IgnoreSslPolicyErrors { get; set; }

        /// <summary>Gets or sets whether to enable (true) or disable (false) TCP Nagle algorithm.</summary>
        public bool TcpNoDelay { get; set; }

        /// <summary>Gets or sets the maximum number of PDVs per PDU, or unlimited if set to zero.
        /// Setting this to 1 can work around common bugs in other implementations.</summary>
        public int MaxPDVsPerPDU { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of clients allowed for a specific server. Unlimited if set to zero.
        /// </summary>
        public int MaxClientsAllowed { get; set; }

        /// <summary>
        /// Gets or sets whether to ignore transfer syntax change when DICOM dataset cannot be transcoded from
        /// its own transfer syntax to the negotiated Accepted Transfer Syntax.
        /// If set to true then the transcoding is ignored and the DICOM dataset is sent as is, if set to false
        /// then the pixeldata is removed from the DICOM dataset.
        /// </summary>
        public bool IgnoreUnsupportedTransferSyntaxChange { get; set; }

        /// <summary>
        /// Gets or sets the amount of time the client will wait for responses from the PACS
        /// </summary>
        public TimeSpan? RequestTimeout { get; set; }

        /// <summary>
        /// Gets the maximum PDU length, increasing this may speed up the sending of C-Store requests, but beware too high values in spotty networks.
        /// </summary>
        public uint MaxPDULength { get; set; }

        #endregion
    }

    public static class DicomServiceOptionsBuilderExtensions
    {

        public static DicomSetupBuilder SetDicomServiceLogging(this DicomSetupBuilder builder, bool logDataPDUs, bool logDimseDataset)
        {
            DicomServiceOptions.Default.LogDataPDUs = logDataPDUs;
            DicomServiceOptions.Default.LogDimseDatasets = logDimseDataset;
            return builder;
        }

    }
}
