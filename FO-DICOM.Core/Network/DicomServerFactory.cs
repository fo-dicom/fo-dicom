using System;
using System.Text;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;

namespace FellowOakDicom.Network
{
    public interface IDicomServerFactory
    {
        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <typeparam name="TServer">Concrete DICOM server type to be returned.</typeparam>
        /// <param name="ipAddress">IP address(es) to listen to. Value <code>null</code> applies default, IPv4Any.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="userState">Optional optional parameters.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <returns>An instance of <typeparamref name="TServer"/>, that starts listening for connections in the background.</returns>
        IDicomServer Create<T, TServer>(
            string ipAddress,
            int port,
            object userState = null,
            string certificateName = null,
            DicomServiceOptions options = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null) where T : DicomService, IDicomServiceProvider where TServer : IDicomServer<T>, new();
    }
    
    public class DicomServerFactory : IDicomServerFactory
    {
        private readonly INetworkManager _networkManager;
        private readonly ILogManager _logManager;
        private readonly ITranscoderManager _transcoderManager;

        public DicomServerFactory(
            INetworkManager networkManager,
            ILogManager logManager,
            ITranscoderManager transcoderManager)
        {
            _networkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            _transcoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
        }

        public IDicomServer Create<T, TServer>(string ipAddress, int port, object userState = null, string certificateName = null, DicomServiceOptions options = null,
            Encoding fallbackEncoding = null, ILogger logger = null) where T : DicomService, IDicomServiceProvider where TServer : IDicomServer<T>, new()
        {
            // TODO continue this
            throw new NotImplementedException();            
        }
    }
}