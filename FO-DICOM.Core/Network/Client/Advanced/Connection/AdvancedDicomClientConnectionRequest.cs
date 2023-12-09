// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System.Text;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// Contains all the knobs and bells needed to open a new connection to a DICOM AE
    /// </summary>
    public class AdvancedDicomClientConnectionRequest
    {
        /// <summary>
        /// Configures how to open the TCP connection
        /// </summary>
        public NetworkStreamCreationOptions NetworkStreamCreationOptions { get; set; }

        /// <summary>
        /// Configures the DICOM listener
        /// </summary>
        public DicomServiceOptions DicomServiceOptions { get; set; }

        /// <summary>
        /// The logger that will be used by the DICOM listener
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// The encoding that will be used as a fallback if no encoding can be derived from the DICOM dataset
        /// </summary>
        public Encoding FallbackEncoding { get; set; } = DicomEncoding.Default;

        /// <summary>
        /// The request handlers that will handle reverse requests.
        /// For example, issuing a C-GET request will cause the other AE to send a C-STORE request back over the same association.
        /// The C-STORE request handler configured here will be responsible for producing a C-STORE response 
        /// </summary>
        public AdvancedDicomClientConnectionRequestHandlers RequestHandlers { get; set; }
    }
}