using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    public class AdvancedDicomClientConnectionFactory : IAdvancedDicomClientConnectionFactory
    {
        private readonly INetworkManager _networkManager;
        private readonly ILogManager _logManager;
        private readonly ITranscoderManager _transcoderManager;

        public AdvancedDicomClientConnectionFactory(
            INetworkManager networkManager, 
            ILogManager logManager,
            ITranscoderManager transcoderManager)
        {
            _networkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            _transcoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
        }
        
        public async Task<IAdvancedDicomClientConnection> ConnectAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var networkStream = await Task.Run(() => _networkManager.CreateNetworkStream(request.NetworkStreamCreationOptions), cancellationToken).ConfigureAwait(false);

            var callbacks = new AdvancedDicomClientConnectionCallbacks(request.RequestHandlers);

            IAdvancedDicomClientConnection connection = new AdvancedDicomClientConnection(callbacks, networkStream, request.FallbackEncoding, request.DicomServiceOptions, request.Logger, _logManager, _networkManager, _transcoderManager);

            if (request.ConnectionInterceptor != null)
            {
                connection = new InterceptingAdvancedDicomClientConnection(connection, request.ConnectionInterceptor);
            }

            connection.StartListener();

            return connection;
        }
    }
}