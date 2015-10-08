// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Net.Security;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;

    using Dicom.Log;

    /// <summary>
    /// DICOM server class.
    /// </summary>
    /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
    public class DicomServer<T> : IDisposable
        where T : DicomService, IDicomServiceProvider
    {
        #region FIELDS

        private X509Certificate cert;

        private bool disposed = false;

        private readonly List<T> clients = new List<T>();

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.
        /// </summary>
        /// <param name="port">Port to listen to.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="logger">Logger.</param>
        public DicomServer(int port, string certificateName = null, DicomServiceOptions options = null, Logger logger = null)
        {
            this.Options = options;
            this.Logger = logger ?? LogManager.GetLogger("Dicom.Network");

            Task.Factory.StartNew(
                () => this.Listen(port, certificateName),
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the logger used by <see cref="DicomServer{T}"/>
        /// </summary>
        public Logger Logger { get; private set; }

        /// <summary>
        /// Gets the options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        public DicomServiceOptions Options { get; private set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Execute the disposal.
        /// </summary>
        /// <param name="disposing">True if called from <see cref="Dispose"/>, false otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.disposed = true;
        }

        /// <summary>
        /// Create an instance of the DICOM service class.
        /// </summary>
        /// <param name="stream">Network stream.</param>
        /// <returns>An instance of the DICOM service class.</returns>
        protected virtual T CreateScp(Stream stream)
        {
            return (T)Activator.CreateInstance(typeof(T), stream, this.Logger);
        }

        /// <summary>
        /// Listen indefinitely for network connections on the specified <paramref name="port"/>.
        /// </summary>
        /// <param name="port">Port to listen to.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        private void Listen(int port, string certificateName)
        {
            var noDelay = this.Options != null ? this.Options.TcpNoDelay : DicomServiceOptions.Default.TcpNoDelay;

            using (new Timer(this.OnTimerTick, false, 1000, 1000))
            {
                TcpListener listener = null;
                do
                {
                    try
                    {
                        var stream = ListenForNetworkStream(out listener, port, noDelay, certificateName, ref this.cert);

                        var scp = this.CreateScp(stream);
                        if (this.Options != null) scp.Options = this.Options;

                        this.clients.Add(scp);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error("Exception accepting client {@error}", e);
                    }
                }
                while (!this.disposed);

                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }

        /// <summary>
        /// Start listening for a new network stream, and return stream when obtained.
        /// </summary>
        /// <param name="listener">Network listener.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="noDelay">True if no delay in connection, false otherwise.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="certificate">X509 certificate, if <paramref name="certificateName"/> exists.</param>
        /// <returns>Network stream.</returns>
        private static Stream ListenForNetworkStream(
            out TcpListener listener,
            int port,
            bool noDelay,
            string certificateName,
            ref X509Certificate certificate)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            var client = listener.AcceptTcpClient();

            client.NoDelay = noDelay;

            Stream stream = client.GetStream();
            if (string.IsNullOrEmpty(certificateName)) return stream;

            var ssl = new SslStream(stream, false);
            ssl.AuthenticateAsServer(
                certificate ?? (certificate = GetX509Certificate(certificateName)),
                false,
                SslProtocols.Tls,
                false);
            stream = ssl;

            return stream;
        }

        /// <summary>
        /// Remove no longer used client connections.
        /// </summary>
        /// <param name="state">Object state.</param>
        private void OnTimerTick(object state)
        {
            try
            {
                this.clients.RemoveAll(client => !client.IsConnected);
            }
            catch
            {
            }
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
            store.Close();

            if (certs.Count == 0)
            {
                throw new DicomNetworkException("Unable to find certificate for " + certificateName);
            }

            return certs[0];
        }

        #endregion
    }
}
