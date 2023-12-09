// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;

namespace FellowOakDicom.Network.Tls
{
    /// <summary>
    /// This is an interface for all handlers that accept tls connections
    /// </summary>
    public interface ITlsAcceptor
    {
        /// <summary>
        /// Accepts an incomming Tls connection
        /// </summary>
        /// <param name="encryptedStream">The encrypted stream over which unencrypted data will be sent and received</param>
        /// <param name="remoteAddress">Remote IP address or hostname</param>
        /// <param name="localPort">The local port to which the remote entity has connected</param>
        /// <returns></returns>
        Stream AcceptTls(Stream encryptedStream, string remoteAddress, int localPort);
    }
}
