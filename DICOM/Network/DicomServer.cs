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

            try
            {
                using (var cancellationSource = new CancellationTokenSource())
                {
                    var token = cancellationSource.Token;
                    Task.Run(() => this.OnTimerTick(token), token);

                    var listener = NetworkManager.CreateNetworkListener(port);
                    listener.Start();
                    do
                    {
                        try
                        {
                            var networkStream = listener.AcceptNetworkStream(certificateName, noDelay);

                            var scp = this.CreateScp(networkStream.AsStream());
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

                    cancellationSource.Cancel();
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        /// <summary>
        /// Remove no longer used client connections.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        private async void OnTimerTick(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(1000, token).ConfigureAwait(false);
                    this.clients.RemoveAll(client => !client.IsConnected);
                }
                catch
                {
                }
            }
        }

        #endregion
    }
}
