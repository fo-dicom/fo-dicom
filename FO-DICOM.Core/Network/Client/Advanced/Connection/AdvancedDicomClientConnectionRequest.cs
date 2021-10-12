// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
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
        /// (Optional) an interceptor that will be given access to every request and callback
        /// Use at your own risk 
        /// </summary>
        public IAdvancedDicomClientConnectionInterceptor ConnectionInterceptor { get; set; }

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
        /// Somewhat confusingly, C-GET temporarily swaps the roles of "DicomClient" and "DicomServer". Consider using C-MOVE requests instead.
        /// </summary>
        public AdvancedDicomClientConnectionRequestHandlers RequestHandlers { get; set; }
    }
}