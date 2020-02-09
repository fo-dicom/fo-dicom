using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Register where we keep the running DICOM servers
    /// </summary>
    public interface IDicomServerRegistry
    {
        bool IsAvailable(string ipAddress, int port);

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
        private readonly ConcurrentDictionary<string, DicomServerRegistration> _servers;

        public DicomServerRegistry()
        {
            _servers  = new ConcurrentDictionary<string, DicomServerRegistration>();
        }

        private static string ToKey(string ipAddress, int port) => $"IP:{ipAddress},PORT:{port}";

        public bool IsAvailable(string ipAddress, int port) => !_servers.ContainsKey(ToKey(ipAddress, port));

        public DicomServerRegistration Register(IDicomServer dicomServer, Task task)
        {
            var registration = new DicomServerRegistration(this, dicomServer, task);
            if (!_servers.TryAdd(ToKey(dicomServer.IPAddress, dicomServer.Port), registration))
            {
                throw new DicomNetworkException(
                    "Could not register DICOM server on port {0}, probably because another server just registered to the same port.",
                    dicomServer.Port);
            }

            return registration;
        }

        public void Unregister(DicomServerRegistration registration)
        {
            string key = ToKey(registration.DicomServer.IPAddress, registration.DicomServer.Port);
            
            _servers.TryRemove(key, out _);
        } 
    }
    
}