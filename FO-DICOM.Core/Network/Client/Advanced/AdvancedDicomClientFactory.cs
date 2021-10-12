// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Connection;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom.Network.Client.Advanced
{
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
            var logger = request.Logger ?? _logManager.GetLogger("Dicom.Network");
            
            return new AdvancedDicomClient(_advancedDicomClientConnectionFactory, logger);
        }
        
        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/> out of DI-container.
        /// </summary>
        public static IAdvancedDicomClient Create(AdvancedDicomClientCreationRequest request) => Setup.ServiceProvider
            .GetRequiredService<IAdvancedDicomClientFactory>()
            .Create(request);
    }
}