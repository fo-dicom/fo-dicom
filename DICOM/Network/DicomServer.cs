// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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

        private bool disposed = false;

        private readonly Task listenTask;

        private readonly CancellationTokenSource cancellationSource;

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

            this.cancellationSource = new CancellationTokenSource();

            Task.Factory.StartNew(
                this.OnTimerTick,
                this.cancellationSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            this.listenTask = Task.Factory.StartNew(
                () => this.Listen(port, certificateName),
                this.cancellationSource.Token,
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

        /// <summary>
        /// Gets whether the server is actively listening for client connections.
        /// </summary>
        public bool IsListening
        {
            get
            {
                return !this.listenTask.IsCompleted;
            }
        }

        /// <summary>
        /// Gets the exception that was thrown if the server failed to listen.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return this.listenTask.IsFaulted ? this.listenTask.Exception.InnerException : null;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Stop server from further listening.
        /// </summary>
        public void Stop()
        {
            if (!this.cancellationSource.IsCancellationRequested)
            {
                this.cancellationSource.Cancel(true);
            }
        }

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
            if (this.disposed) return;

            if (disposing)
            {
                this.Stop();
                this.cancellationSource.Dispose();
                this.clients.Clear();
            }

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
            try
            {
                var noDelay = this.Options != null ? this.Options.TcpNoDelay : DicomServiceOptions.Default.TcpNoDelay;

                var listener = NetworkManager.CreateNetworkListener(port);
                listener.Start();

                while (!this.cancellationSource.IsCancellationRequested)
                {
                    var networkStream = listener.AcceptNetworkStream(certificateName, noDelay);

                    var scp = this.CreateScp(networkStream.AsStream());
                    if (this.Options != null) scp.Options = this.Options;

                    this.clients.Add(scp);
                }

                if (listener != null)
                {
                    listener.Stop();
                }
            }
            catch (Exception e)
            {
                this.Logger.Error("Exception listening for clients, {@error}", e);
                this.Stop();
                throw;
            }
        }

        /// <summary>
        /// Remove no longer used client connections.
        /// </summary>
        private async void OnTimerTick()
        {
            while (!this.cancellationSource.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(1000, this.cancellationSource.Token).ConfigureAwait(false);
                    this.clients.RemoveAll(client => !client.IsConnected);
                }
                catch (Exception e)
                {
                    this.Logger.Warn("Exception removing disconnected clients, {@error}", e);
                }
            }
        }

        #endregion
    }
}
