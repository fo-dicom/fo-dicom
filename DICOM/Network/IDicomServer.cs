// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35

using System;
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
        /// Gets the port to which the server is listening.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Gets the logger used by <see cref="DicomServer{T}"/>
        /// </summary>
        Logger Logger { get; }

        /// <summary>
        /// Gets the options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        DicomServiceOptions Options { get; }

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
        /// Stop server from further listening.
        /// </summary>
        void Stop();

        #endregion
    }
}

#endif