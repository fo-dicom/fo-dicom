// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Tls;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Network
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
        /// Gets a value indicating whether the server is actively listening for client connections.
        /// </summary>
        bool IsListening { get; }

        /// <summary>
        /// Gets the exception that was thrown if the server failed to listen.
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Gets the options to control behavior of <see cref="DicomService"/> base class.
        /// Gets the port to which the server is listening.
        /// </summary>
        DicomServiceOptions Options { get; }

        /// <summary>
        /// Gets the logger used by <see cref="DicomServer{T}"/>
        /// </summary>
        ILogger Logger { get; set; }
        
        /// <summary>
        /// Gets the service scope that will live as long as the DICOM server lives. Must be disposed alongside the DicomServer instance.
        /// </summary>
        IServiceScope ServiceScope { get; set; }
        
        /// <summary>
        /// Gets the DICOM server registration ticket with the central registry.
        /// The registry prevents multiple DICOM servers from being created for the same IP address and port.
        /// This registration must be disposed alongside the DICOM server itself.
        /// </summary>
        DicomServerRegistration Registration { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Starts the DICOM server listening for connections on the specified IP address(es) and port.
        /// </summary>
        /// <param name="ipAddress">IP address(es) for the server to listen to.</param>
        /// <param name="port">Port to which the server should be listening.</param>
        /// <param name="tlsAcceptor">Handler to accept secure connections.</param>
        /// <param name="fallbackEncoding">Encoding to apply if no encoding is identified.</param>
        /// <param name="serviceOptions">Service options</param>
        /// <param name="userState">User state to be shared with the connected services.</param>
        /// <param name="serverOptions">Server options</param>
        /// <returns>Awaitable <see cref="System.Threading.Tasks.Task"/>.</returns>
        Task StartAsync(string ipAddress, int port, ITlsAcceptor tlsAcceptor, Encoding fallbackEncoding,
            DicomServiceOptions serviceOptions, object userState, DicomServerOptions serverOptions);

        /// <summary>
        /// Stop server from further listening.
        /// </summary>
        void Stop();

        #endregion
    }

    /// <summary>
    /// Helper interface to ensure type safety when creating DICOM server objects via <code>DicomServer.Create</code> overloads.
    /// </summary>
    /// <typeparam name="TServiceProvider">DICOM service class consumed by the DICOM server object.</typeparam>
    public interface IDicomServer<TServiceProvider> : IDicomServer where TServiceProvider : DicomService, IDicomServiceProvider
    {
    }

}

