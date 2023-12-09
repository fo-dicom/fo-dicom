// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Tls;
using System;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Contains the necessary parameters to open a new network stream to another DICOM SCP
    /// </summary>
    public class NetworkStreamCreationOptions
    {
        /// <summary>
        /// The IP address or host name of the SCP
        /// </summary>
        public string Host { get; set; }
        
        /// <summary>
        /// The port on which the SCP is listening
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        /// Gets or sets the handler to use TLS (SSLStream) when opening the TCP connection. If this handler is null then no TLS will be used
        /// </summary>
        public ITlsInitiator TlsInitiator { get; set; }
        
        /// <summary>
        /// Whether or not to disable the delay when buffers are not full
        /// </summary>
        /// <seealso cref="System.Net.Sockets.TcpClient.NoDelay"/>
        public bool NoDelay { get; set; }
        
        /// <summary>
        /// After how much time a write or read operation over the TCP connection must time out
        /// </summary>
        /// <seealso cref="System.Net.Security.SslStream.ReadTimeout"/>
        /// <seealso cref="System.Net.Security.SslStream.WriteTimeout"/>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Gets or sets the size of the receive buffer of the underlying TCP connection
        /// If not configured, the default value of 8192 bytes will be used
        /// </summary>
        /// <seealso cref="System.Net.Sockets.TcpClient.ReceiveBufferSize"/>
        public int? ReceiveBufferSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the send buffer of the underlying TCP connection
        /// If not configured, the default value of 8192 bytes will be used
        /// </summary>
        /// <seealso cref="System.Net.Sockets.TcpClient.SendBufferSize"/>
        public int? SendBufferSize { get; set; }
    }
}
