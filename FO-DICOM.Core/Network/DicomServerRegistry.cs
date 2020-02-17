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
        /// <returns>True when a new DICOM server can be set up for that IP address and port</returns>
        bool IsAvailable(int port);

        /// <summary>
        /// Gets a running DICOM server listening on the provided port, or NULL if no such DICOM server exists.
        /// </summary>
        /// <param name="port">The port</param>
        /// <returns>A DICOM server registration or null</returns>
        DicomServerRegistration Get(int port);

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
    
    public class DicomServerRegistry : IDicomServerRegistry
    {
        private readonly ConcurrentDictionary<int, DicomServerRegistration> _servers;

        public DicomServerRegistry()
        {
            _servers  = new ConcurrentDictionary<int, DicomServerRegistration>();
        }

        public bool IsAvailable(int port) => !_servers.ContainsKey(port);

        public DicomServerRegistration Get(int port) => _servers.TryGetValue(port, out var registration) ? registration : null;

        public DicomServerRegistration Register(IDicomServer dicomServer, Task task)
        {
            var registration = new DicomServerRegistration(this, dicomServer, task);
            if (!_servers.TryAdd(dicomServer.Port, registration))
            {
                throw new DicomNetworkException(
                    "Could not register DICOM server on port {0}, probably because another server just registered to the same port.",
                    dicomServer.Port);
            }

            return registration;
        }

        public void Unregister(DicomServerRegistration registration) => _servers.TryRemove(registration.DicomServer.Port, out _);
    }
    
}