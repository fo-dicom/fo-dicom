// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FellowOakDicom.Network.Client.Advanced
{
    public interface IAdvancedDicomClientFactory
    {
        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        IAdvancedDicomClient Create();
    }

    public static class AdvancedDicomClientFactory
    {
        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/> out of DI-container.
        /// </summary>
        public static IAdvancedDicomClient Create() => Setup.ServiceProvider
            .GetRequiredService<IAdvancedDicomClientFactory>().Create();
    }

    public class DefaultAdvancedDicomClientFactory : IAdvancedDicomClientFactory
    {
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;
        private readonly IOptions<AdvancedDicomClientOptions> _defaultAdvancedClientOptions;

        public DefaultAdvancedDicomClientFactory(
            IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory,
            IOptions<AdvancedDicomClientOptions> defaultAdvancedClientOptions
        )
        {
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _defaultAdvancedClientOptions = defaultAdvancedClientOptions ?? throw new ArgumentNullException(nameof(defaultAdvancedClientOptions));
        }

        public virtual IAdvancedDicomClient Create()
        {
            var advancedClientOptions = _defaultAdvancedClientOptions.Value.Clone();

            return new AdvancedDicomClient(_advancedDicomClientConnectionFactory, advancedClientOptions);
        }
    }
}