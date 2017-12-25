// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    /// <summary>
    /// Options to control the behavior of the <see cref="DicomService"/> base class.
    /// </summary>
    public class DicomServiceOptions
    {
        /// <summary>Default options for use with the <see cref="DicomService"/> base class.</summary>
        public static readonly DicomServiceOptions Default = new DicomServiceOptions();

        /// <summary>Constructor</summary>
        public DicomServiceOptions()
        {
            LogDataPDUs = false;
            LogDimseDatasets = false;
            UseRemoteAEForLogName = false;
            MaxCommandBuffer = 1 * 1024; //1KB
            MaxDataBuffer = 1 * 1024 * 1024; //1MB
            ThreadPoolLinger = 200;
            IgnoreSslPolicyErrors = false;
            TcpNoDelay = true;
            OnePDVPerPDU = false;
        }

        /// <summary>Write message to log for each P-Data-TF PDU sent or received.</summary>
        public bool LogDataPDUs { get; set; }

        /// <summary>Write command and data datasets to log.</summary>
        public bool LogDimseDatasets { get; set; }

        /// <summary>Use the AE Title of the remote host as the log name.</summary>
        public bool UseRemoteAEForLogName { get; set; }

        /// <summary>Maximum buffer length for command PDVs when generating P-Data-TF PDUs.</summary>
        public uint MaxCommandBuffer { get; set; }

        /// <summary>Maximum buffer length for data PDVs when generating P-Data-TF PDUs.</summary>
        public uint MaxDataBuffer { get; set; }

        /// <summary>Amount of time in milliseconds to retain Thread Pool thread to process additional requests.</summary>
        public int ThreadPoolLinger { get; set; }

        /// <summary>DICOM client should ignore SSL certificate errors.</summary>
        public bool IgnoreSslPolicyErrors { get; set; }

        /// <summary>Enable or disable TCP Nagle algorithm.</summary>
        public bool TcpNoDelay { get; set; }

        /// <summary>Write one PDV per PDU regardless of whether another PDV would fit in the PDU. Works around common bugs in other implementations.</summary>
        public bool OnePDVPerPDU { get; set; }
    }
}
