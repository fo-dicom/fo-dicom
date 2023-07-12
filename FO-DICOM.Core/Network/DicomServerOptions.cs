﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Configures the DICOM server startup process
    /// </summary>
    public class DicomServerOptions
    {
        /// <summary>
        /// Gets or sets whether to enable (true) or disable (false) TCP Nagle algorithm on incoming TCP connections
        /// </summary>
        public bool TcpNoDelay { get; set; } = true;
        
        /// <summary>
        /// Gets or sets the maximum number of clients allowed for a DICOM server. Unlimited if set to zero.
        /// </summary>
        public int MaxClientsAllowed { get; set; } = 0;

        public DicomServerOptions Clone() =>
            new DicomServerOptions
            {
                MaxClientsAllowed = MaxClientsAllowed,
            };
    }
}
