// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// Represents an advanced DICOM client that exposes the highest amount of manual control over the underlying DICOM communication when sending DICOM requests<br/>
    /// Using an advanced DICOM client, it is possible to manage the lifetime of a DICOM connection + association in a fine-grained manner and send any number of DICOM requests over it<br/>
    /// <br/>
    /// The regular DICOM client is completely built on top of this advanced DICOM client.<br/>
    /// Be aware that consumers of the advanced DICOM client are left to their own devices to handle the fine details of interacting with other PACS software.<br/>
    /// Here is an incomplete sample of the things the regular DICOM client will do for you:<br/>
    /// - Enforce a maximum amount of requests per association<br/>
    /// - Keep an association alive for a certain amount of time to allow more requests to be sent<br/>
    /// - Automatically open more associations while more requests are enqueued<br/>
    /// - Automatically negotiate presentation contexts based on the requests that are enqueued<br/>
    /// <br/>
    /// This advanced DICOM client is offered to expert users of the Fellow Oak DICOM library.<br/>
    /// If you do not consider yourself such an expert, please reconsider the compatibility of the regular DicomClient with your use case.
    /// </summary>
    public interface IAdvancedDicomClientConnectionFactory
    {
        /// <summary>
        /// Opens a new TCP connection to another AE using the parameters provided in the connection <paramref name="request"/><br/>
        /// WARNING: you cannot reuse a single connection for multiple associations
        /// </summary>
        /// <param name="request">The connection request that specifies the details of the connection that should be opened</param>
        /// <param name="cancellationToken">The token that will cancel the opening of the connection</param>
        /// <returns>A new instance of <see cref="IAdvancedDicomClientConnection"/></returns>
        Task<IAdvancedDicomClientConnection> OpenConnectionAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken);
    }
    
    public static class AdvancedDicomClientConnectionFactory
    {
        /// <inheritdoc cref="IAdvancedDicomClientConnectionFactory.OpenConnectionAsync"/>
        public static Task<IAdvancedDicomClientConnection> OpenConnectionAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken)
            => Setup.ServiceProvider.GetRequiredService<IAdvancedDicomClientConnectionFactory>().OpenConnectionAsync(request, cancellationToken);
    }
    
    /// <inheritdoc cref="IAdvancedDicomClientConnectionFactory"/>
    public class DefaultAdvancedDicomClientConnectionFactory : IAdvancedDicomClientConnectionFactory
    {
        private readonly INetworkManager _networkManager;
        private readonly ILoggerFactory _loggerFactory;
        private readonly DicomServiceDependencies _dicomServiceDependencies;
        private readonly IOptions<DicomServiceOptions> _defaultDicomServiceOptions;

        public DefaultAdvancedDicomClientConnectionFactory(
            INetworkManager networkManager, 
            ILoggerFactory loggerFactory,
            IOptions<DicomServiceOptions> defaultDicomServiceOptions,
            DicomServiceDependencies dicomServiceDependencies)
        {
            _networkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _defaultDicomServiceOptions = defaultDicomServiceOptions;
            _dicomServiceDependencies = dicomServiceDependencies ?? throw new ArgumentNullException(nameof(dicomServiceDependencies));
        }
        
        public async Task<IAdvancedDicomClientConnection> OpenConnectionAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            cancellationToken.ThrowIfCancellationRequested();

            request.Logger ??= _loggerFactory.CreateLogger(LogCategories.Network);

            request.DicomServiceOptions ??= _defaultDicomServiceOptions.Value;

            if (request.NetworkStreamCreationOptions == null)
            {
                throw new ArgumentException(nameof(AdvancedDicomClientConnectionRequest.NetworkStreamCreationOptions) + " cannot be null");
            }

            cancellationToken.ThrowIfCancellationRequested();
            
            INetworkStream networkStream = null;

            try
            {
                networkStream = await Task.Run(() => _networkManager.CreateNetworkStream(request.NetworkStreamCreationOptions), cancellationToken).ConfigureAwait(false);

                var eventCollector = new AdvancedDicomClientConnectionEventCollector(request.RequestHandlers);

                IAdvancedDicomClientConnection connection = new AdvancedDicomClientConnection(eventCollector, networkStream, 
                    request.FallbackEncoding, request.DicomServiceOptions,
                    request.Logger, _dicomServiceDependencies);

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