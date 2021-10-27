// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// The factory that is responsible for creating new DICOM connections
    /// </summary>
    public interface IAdvancedDicomClientConnectionFactory
    {
        /// <inheritdoc cref="IAdvancedDicomClient.OpenConnectionAsync"/>
        Task<IAdvancedDicomClientConnection> ConnectAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken);
    }
    
    /// <inheritdoc cref="IAdvancedDicomClientConnectionFactory"/>
    public class AdvancedDicomClientConnectionFactory : IAdvancedDicomClientConnectionFactory
    {
        private readonly INetworkManager _networkManager;
        private readonly ILogManager _logManager;
        private readonly ITranscoderManager _transcoderManager;
        private readonly IOptions<DicomServiceOptions> _defaultDicomServiceOptions;

        public AdvancedDicomClientConnectionFactory(
            INetworkManager networkManager, 
            ILogManager logManager,
            ITranscoderManager transcoderManager,
            IOptions<DicomServiceOptions> defaultDicomServiceOptions)
        {
            _networkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            _transcoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
            _defaultDicomServiceOptions = defaultDicomServiceOptions;
        }
        
        public async Task<IAdvancedDicomClientConnection> ConnectAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Logger == null)
            {
                request.Logger = _logManager.GetLogger("Dicom.Network");
            }

            if (request.DicomServiceOptions == null)
            {
                request.DicomServiceOptions = _defaultDicomServiceOptions.Value;
            }

            cancellationToken.ThrowIfCancellationRequested();
            
            INetworkStream networkStream = null;

            try
            {
                networkStream = await Task.Run(() => _networkManager.CreateNetworkStream(request.NetworkStreamCreationOptions), cancellationToken).ConfigureAwait(false);

                var callbacks = new AdvancedDicomClientConnectionCallbacks(request.RequestHandlers);

                IAdvancedDicomClientConnection connection = new AdvancedDicomClientConnection(callbacks, networkStream, 
                    request.FallbackEncoding, request.DicomServiceOptions,
                    request.Logger, _logManager, _networkManager, _transcoderManager);

                if (request.ConnectionInterceptor != null)
                {
                    connection = new InterceptingAdvancedDicomClientConnection(connection, request.ConnectionInterceptor);
                }

                cancellationToken.ThrowIfCancellationRequested();
                
                connection.StartListener();

                return connection;
            }
            catch(OperationCanceledException)
            {
                networkStream?.Dispose();
                throw;
            }
        }
    }
}