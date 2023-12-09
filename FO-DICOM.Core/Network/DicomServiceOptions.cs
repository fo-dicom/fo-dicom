// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Configures a DICOM service (an instance of an active DICOM connection)
    /// </summary>
    public class DicomServiceOptions
    {
        /// <summary>
        /// Singleton instance that contains the default values
        /// Never modify this instance!
        /// </summary>
        internal static readonly DicomServiceOptions Default = new DicomServiceOptions(); 
        
        /// <summary>Gets or sets whether or not to write message to log for each P-Data-TF PDU sent or received.</summary>
        public bool LogDataPDUs { get; set; } = false;

        /// <summary>Gets or sets whether or not to write command and data datasets to log.</summary>
        public bool LogDimseDatasets { get; set; } = false;

        /// <summary>Gets or sets whether or not to use the AE Title of the remote host as the log name.</summary>
        public bool UseRemoteAEForLogName { get; set; } = false;

        /// <summary>Gets or sets maximum buffer length for command PDVs when generating P-Data-TF PDUs.</summary>
        public uint MaxCommandBuffer { get; set; } = 1 * 1024; //1KB

        /// <summary>Gets or sets maximum buffer length for data PDVs when generating P-Data-TF PDUs.</summary>
        public uint MaxDataBuffer { get; set; } = 1 * 1024 * 1024; //1MB
        
        /// <summary>
        /// Gets or sets whether to enable (true) or disable (false) TCP Nagle algorithm on incoming TCP connections
        /// </summary>
        public bool TcpNoDelay { get; set; } = true;

        /// <summary>
        /// Gets or sets the size of the receive buffer of the underlying TCP connection
        /// If not configured, the default value of 8192 bytes will be used
        /// </summary>
        /// <seealso cref="System.Net.Sockets.TcpClient.ReceiveBufferSize"/>
        public int? TcpReceiveBufferSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the send buffer of the underlying TCP connection
        /// If not configured, the default value of 8192 bytes will be used
        /// </summary>
        /// <seealso cref="System.Net.Sockets.TcpClient.SendBufferSize"/>
        public int? TcpSendBufferSize { get; set; }

        /// <summary>Gets or sets the maximum number of PDVs per PDU, or unlimited if set to zero.
        /// Setting this to 1 can work around common bugs in other implementations.</summary>
        public int MaxPDVsPerPDU { get; set; } = 0;

        /// <summary>
        /// Gets or sets whether to ignore transfer syntax change when DICOM dataset cannot be transcoded from
        /// its own transfer syntax to the negotiated Accepted Transfer Syntax.
        /// If set to true then the transcoding is ignored and the DICOM dataset is sent as is, if set to false
        /// then the pixeldata is removed from the DICOM dataset.
        /// </summary>
        public bool IgnoreUnsupportedTransferSyntaxChange { get; set; } = false;

        /// <summary>
        /// Gets or sets the amount of time the client will wait for responses from the PACS
        /// </summary>
        public TimeSpan? RequestTimeout { get; set; } = null;

        /// <summary>
        /// Gets the maximum PDU length, increasing this may speed up the sending of C-Store requests, but beware too high values in spotty networks.
        /// </summary>
        public uint MaxPDULength { get; set; } = 262144; // 256 Kb

        public DicomServiceOptions Clone() =>
            new DicomServiceOptions
            {
                LogDataPDUs = LogDataPDUs,
                LogDimseDatasets = LogDimseDatasets,
                UseRemoteAEForLogName = UseRemoteAEForLogName,
                MaxCommandBuffer = MaxCommandBuffer,
                MaxDataBuffer = MaxDataBuffer,
                RequestTimeout = RequestTimeout,
                IgnoreUnsupportedTransferSyntaxChange = IgnoreUnsupportedTransferSyntaxChange,
                MaxPDULength = MaxPDULength,
                MaxPDVsPerPDU = MaxPDVsPerPDU,
                TcpNoDelay = TcpNoDelay,
                TcpReceiveBufferSize = TcpReceiveBufferSize,
                TcpSendBufferSize = TcpSendBufferSize
            };
    }
}
