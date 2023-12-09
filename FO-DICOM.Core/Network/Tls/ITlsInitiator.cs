// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;

namespace FellowOakDicom.Network.Tls
{
    /// <summary>
    /// An interface for all handlers that initiate tls connections
    /// </summary>
    public interface ITlsInitiator
    {
        /// <summary>
        /// Initiate a tls connection
        /// </summary>
        /// <param name="plainStream">The cleartext stream over which unencrypted data will be sent and received</param>
        /// <param name="remoteAddress">Remote IP address or hostname</param>
        /// <param name="remotePort">Remote port</param>
        /// <returns></returns>
        Stream InitiateTls(Stream plainStream, string remoteAddress, int remotePort);
    }
}
