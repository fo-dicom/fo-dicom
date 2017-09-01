// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35

using System;
using System.Text;
using System.Threading.Tasks;

using Dicom.Log;

namespace Dicom.Network
{
    /// <summary>
    /// Interface representing a DICOM server instance.
    /// </summary>
    public interface IDicomServer : IDisposable
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the IP address(es) the server listens to.
        /// </summary>
        string IPAddress { get; }

        /// <summary>
        /// Gets the port to which the server is listening.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Gets the logger used by <see cref="DicomServer{T}"/>
        /// </summary>
        Logger Logger { get; set; }

        /// <summary>
        /// Gets a value indicating whether the server is actively listening for client connections.
        /// </summary>
        bool IsListening { get; }

        /// <summary>
        /// Gets the exception that was thrown if the server failed to listen.
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Gets the <see cref="Task"/> managing the background listening and unused client removal processes.
        /// </summary>
        Task BackgroundWorker { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Starts the DICOM server.
        /// </summary>
        /// <param name="ipAddress">IP address(es) for the server to listen to.</param>
        /// <param name="port">Port to which the servier should be litening.</param>
        /// <param name="userState">User state to be shared with the connected services.</param>
        /// <param name="certificateName">Certificate name for secure connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="fallbackEncoding">Encoding to apply if no encoding is identified.</param>
        /// <returns>Await:able instance.</returns>
        Task StartAsync(string ipAddress, int port, object userState, string certificateName,
            DicomServiceOptions options, Encoding fallbackEncoding = null);

        /// <summary>
        /// Stop server from further listening.
        /// </summary>
        void Stop();

        #endregion
    }
}

#endif
