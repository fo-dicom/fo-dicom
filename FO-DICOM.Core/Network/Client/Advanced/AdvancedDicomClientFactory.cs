// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Connection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FellowOakDicom.Network.Client.Advanced
{
    public interface IAdvancedDicomClientFactory
    {
        /// <summary>
        /// Initializes an instance of <see cref="IAdvancedDicomClient"/>.
        /// </summary>
        IAdvancedDicomClient Create(AdvancedDicomClientCreationRequest request);
    }
    
    /// <inheritdoc cref="IAdvancedDicomClientFactory"/>
    public class AdvancedDicomClientFactory : IAdvancedDicomClientFactory
    {
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;
        private readonly ILogManager _logManager;

        public AdvancedDicomClientFactory(
            IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory,
            ILogManager logManager)
        {
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }

        IAdvancedDicomClient IAdvancedDicomClientFactory.Create(AdvancedDicomClientCreationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var logger = request.Logger ?? _logManager.GetLogger("Dicom.Network");
            
            return new AdvancedDicomClient(_advancedDicomClientConnectionFactory, logger);
        }
        
        /// <summary>
        /// Initializes an instance of <see cref="IAdvancedDicomClient"/> outside of the DI-container.
        /// </summary>
        public static IAdvancedDicomClient Create(AdvancedDicomClientCreationRequest request) => Setup.ServiceProvider
            .GetRequiredService<IAdvancedDicomClientFactory>()
            .Create(request);
    }
}