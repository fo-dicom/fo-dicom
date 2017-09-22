// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine.Networking;

namespace Dicom.Network
{
    /// <summary>
    /// Unity implementation of the <see cref="INetworkListener"/>.
    /// </summary>
    public class UnityNetworkListener : INetworkListener
    {
        #region FIELDS

        private readonly ListenerImpl _listener;

        private readonly string _ipAddress;

        private readonly int _port;

        private X509Certificate _certificate = null;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityNetworkListener"/> class. 
        /// </summary>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">TCP/IP port to listen to.</param>
        internal UnityNetworkListener(string ipAddress, int port)
        {
            _listener = new ListenerImpl();
            _ipAddress = ipAddress;
            _port = port;
        }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public Task StartAsync()
        {
            return Task.FromResult(_listener.Listen(_ipAddress, _port));
        }

        /// <inheritdoc />
        public void Stop()
        {
            _listener.Stop();
        }

        /// <inheritdoc />
        public async Task<INetworkStream> AcceptNetworkStreamAsync(string certificateName, bool noDelay, CancellationToken token)
        {
            INetworkStream stream = null;
            try
            {
                var connection = await _listener.AcceptNetworkConnectionAsync().ConfigureAwait(false);
                if (noDelay) connection.SetMaxDelay(0);

                if (!string.IsNullOrEmpty(certificateName) && _certificate == null)
                {
                    _certificate = GetX509Certificate(certificateName);
                }

                stream = new UnityNetworkStream(connection, _certificate);
            }
            catch
            {
            }

            return stream;
        }

        /// <summary>
        /// Get X509 certificate from the certificate store.
        /// </summary>
        /// <param name="certificateName">Certificate name.</param>
        /// <returns>Certificate with the specified name.</returns>
        private static X509Certificate GetX509Certificate(string certificateName)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindBySubjectName, certificateName, false);
            store.Dispose();

            if (certs.Count == 0)
            {
                throw new DicomNetworkException("Unable to find certificate for " + certificateName);
            }

            return certs[0];
        }

        #endregion

        #region INNER TYPES

        private class ListenerImpl : NetworkServerSimple
        {
            private readonly AsyncManualResetEvent<NetworkConnection> _awaiter =
                new AsyncManualResetEvent<NetworkConnection>();

            internal Task<NetworkConnection> AcceptNetworkConnectionAsync()
            {
                return _awaiter.WaitAsync();
            }

            public override void OnConnected(NetworkConnection conn)
            {
                base.OnConnected(conn);

                _awaiter.Set(conn);
                _awaiter.Reset();
            }
        }

        #endregion
    }
}
