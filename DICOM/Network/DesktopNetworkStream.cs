// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;

    public sealed class DesktopNetworkStream : INetworkStream
    {
        #region FIELDS

        private bool disposed = false;

        private readonly TcpClient tcpClient;

        private readonly Stream networkStream;

        #endregion

        #region CONSTRUCTORS

        internal DesktopNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors)
        {
            this.tcpClient = new TcpClient(host, port) { NoDelay = noDelay };

            Stream stream = this.tcpClient.GetStream();

            if (useTls)
            {
                var ssl = new SslStream(
                    stream,
                    false,
                    (sender, certificate, chain, errors) => errors == SslPolicyErrors.None || ignoreSslPolicyErrors);
                ssl.AuthenticateAsClient(host);
                stream = ssl;
            }

            this.networkStream = stream;
        }

        internal DesktopNetworkStream(TcpClient tcpClient, X509Certificate certificate)
        {
            this.tcpClient = tcpClient;

            Stream stream = this.tcpClient.GetStream();
            if (certificate != null)
            {
                var ssl = new SslStream(stream, false);
                ssl.AuthenticateAsServer(certificate, false, SslProtocols.Tls, false);
                stream = ssl;
            }
            this.networkStream = stream;
        }

        ~DesktopNetworkStream()
        {
            this.Dispose(false);
        }

        #endregion

        #region METHODS

        public Stream AsStream()
        {
            return this.networkStream;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed) return;

            this.networkStream.Dispose();
            this.tcpClient.Close();

            this.disposed = true;
        }

        #endregion
    }
}
