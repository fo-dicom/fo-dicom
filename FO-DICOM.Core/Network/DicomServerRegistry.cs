// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Register where we keep the running DICOM servers
    /// </summary>
    public interface IDicomServerRegistry
    {
        /// <summary>
        /// Checks whether listening to the provided port at the provided IP address is still possible
        /// </summary>
        /// <param name="port">The port</param>
        /// <param name="ipAddress">The IP address</param>
        /// <returns>True when a new DICOM server can be set up for that IP address and port</returns>
        bool IsAvailable(int port, string ipAddress = NetworkManager.IPv4Any);

        /// <summary>
        /// Gets a running DICOM server listening on the provided port, or NULL if no such DICOM server exists.
        /// </summary>
        /// <param name="port">The port</param>
        /// <param name="ipAddress">The IP address</param>
        /// <returns>A DICOM server registration or null</returns>
        DicomServerRegistration Get(int port, string ipAddress = NetworkManager.IPv4Any);

        /// <summary>
        /// Register a new DICOM server
        /// </summary>
        /// <param name="dicomServer">The DICOM server that is now running</param>
        /// <param name="task">The task that represents the running of the DICOM server</param>
        DicomServerRegistration Register(IDicomServer dicomServer, Task task);

        /// <summary>
        /// Unregisters a DICOM server. This needs to happen when the DICOM server is stopped.
        /// </summary>
        /// <param name="registration"></param>
        void Unregister(DicomServerRegistration registration);
    }

    /// <summary>
    /// Register where we keep the running DICOM servers
    /// </summary>
    public static class DicomServerRegistry
    {
        /// <summary>
        /// Checks whether listening to the provided port at the provided IP address is still possible
        /// </summary>
        /// <param name="port">The port</param>
        /// <param name="ipAddress">The IP address</param>
        /// <returns>True when a new DICOM server can be set up for that IP address and port</returns>
        public static bool IsAvailable(int port, string ipAddress = NetworkManager.IPv4Any)
            => Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().IsAvailable(port, ipAddress);

        /// <summary>
        /// Gets a running DICOM server listening on the provided port, or NULL if no such DICOM server exists.
        /// </summary>
        /// <param name="port">The port</param>
        /// <param name="ipAddress">The IP address</param>
        /// <returns>A DICOM server registration or null</returns>
        public static DicomServerRegistration Get(int port, string ipAddress = NetworkManager.IPv4Any)
            => Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port, ipAddress);

        /// <summary>
        /// Register a new DICOM server
        /// </summary>
        /// <param name="dicomServer">The DICOM server that is now running</param>
        /// <param name="task">The task that represents the running of the DICOM server</param>
        public static DicomServerRegistration Register(IDicomServer dicomServer, Task task)
            => Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Register(dicomServer, task);

        /// <summary>
        /// Unregisters a DICOM server. This needs to happen when the DICOM server is stopped.
        /// </summary>
        /// <param name="registration"></param>
        public static void Unregister(DicomServerRegistration registration)
            => Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Unregister(registration);
    }

    public class DefaultDicomServerRegistry : IDicomServerRegistry
    {
        private readonly ConcurrentDictionary<(int, string), DicomServerRegistration> _servers;

        public DefaultDicomServerRegistry()
        {
            _servers  = new ConcurrentDictionary<(int, string), DicomServerRegistration>();
        }

        public bool IsAvailable(int port, string ipAddress = NetworkManager.IPv4Any)
            => !_servers.ContainsKey((port, ipAddress));

        public DicomServerRegistration Get(int port, string ipAddress = NetworkManager.IPv4Any)
            => _servers.TryGetValue((port, ipAddress), out var registration) ? registration : null;

        public DicomServerRegistration Register(IDicomServer dicomServer, Task task)
        {
            var registration = new DicomServerRegistration(this, dicomServer, task);
            if (!_servers.TryAdd((dicomServer.Port, dicomServer.IPAddress), registration))
            {
                throw new DicomNetworkException($"Could not register DICOM server on port {dicomServer.Port}, probably because another server just registered to the same port.");
            }

            return registration;
        }

        public void Unregister(DicomServerRegistration registration)
            => _servers.TryRemove((registration.DicomServer.Port, registration.DicomServer.IPAddress), out _);

    }
    
}