using System;
using FellowOakDicom.Log;

namespace FellowOakDicom.Network.Client
{
    public interface IDicomClientFactory
    {
        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        /// <param name="options">An optional set of parameters that further modify the behavior of this DICOM client</param>
        IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe, DicomClientOptions options = null);
    }

    public class DicomClientFactory : IDicomClientFactory
    {
        private readonly ILogManager _logManager;
        private readonly INetworkManager _networkManager;

        public DicomClientFactory(ILogManager logManager, INetworkManager networkManager)
        {
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            _networkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
        }

        public IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe, DicomClientOptions options = null)
        {
            return new DicomClient(host, port, useTls, callingAe, calledAe, options ?? new DicomClientOptions(), _networkManager, _logManager);
        }
    }
}