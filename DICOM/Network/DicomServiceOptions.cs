// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
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
            public static readonly bool LogDataPDUs = false;

            public static readonly bool LogDimseDatasets = false;

            public static readonly bool UseRemoteAEForLogName = false;

            public static readonly uint MaxCommandBuffer = 1 * 1024; //1KB

            public static readonly uint MaxDataBuffer = 1 * 1024 * 1024; //1MB

            public static readonly bool IgnoreSslPolicyErrors = false;

            public static readonly bool TcpNoDelay = true;

            public static readonly int MaxPDVsPerPDU = 0;

            public static readonly int MaxClientsAllowed = 0;

            public static readonly bool IgnoreUnsupportedTransferSyntaxChange = false;
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
        /// </summary>
        public bool IgnoreUnsupportedTransferSyntaxChange { get; set; }

        #endregion
    }
}
