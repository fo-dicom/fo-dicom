// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests
{
    public class SetupTest
    {
        public SetupTest() { }


        [Fact()]
        public void DefaultSetupShouldIncludeDefaultDicomClientOptions()
        {
            var defaultClientSettings = new DicomClientOptions();
            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var clientFactory = serviceProvider.GetService<IDicomClientFactory>();
            Assert.NotNull(clientFactory);

            var clientOptions = serviceProvider.GetService<IOptions<DicomClientOptions>> ();
            Assert.NotNull(clientOptions?.Value);
            Assert.Equal(defaultClientSettings.ConnectionTimeoutInMs, clientOptions.Value.ConnectionTimeoutInMs);

            var client = clientFactory.Create("localhost", 104, false, "TEST", "SERVER");
            Assert.NotNull(client);
            Assert.Equal(defaultClientSettings.ConnectionTimeoutInMs, client.ClientOptions.ConnectionTimeoutInMs);
        }

        [Fact()]
        public void ConfigureDicomClientOptionsInSetup()
        {
            var defaultClientSettings = new DicomClientOptions();
            var configuredTimetoutInMs = defaultClientSettings.ConnectionTimeoutInMs + 1000;

            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom(configureClientOptions: (opt) => opt.ConnectionTimeoutInMs = configuredTimetoutInMs);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var clientFactory = serviceProvider.GetService<IDicomClientFactory>();
            Assert.NotNull(clientFactory);

            var clientOptions = serviceProvider.GetService<IOptions<DicomClientOptions>>();
            Assert.NotNull(clientOptions?.Value);
            Assert.Equal(configuredTimetoutInMs, clientOptions.Value.ConnectionTimeoutInMs);

            var client = clientFactory.Create("localhost", 104, false, "TEST", "SERVER");
            Assert.NotNull(client);
            Assert.Equal(configuredTimetoutInMs, client.ClientOptions.ConnectionTimeoutInMs);
        }

        [Fact()]
        public void ConfigureDicomClientOptionsInIConfiguration()
        {
            var defaultClientSettings = new DicomClientOptions();
            var configuredTimetoutInMs = defaultClientSettings.ConnectionTimeoutInMs + 1000;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {"FellowOakDicom:DicomClientOptions:ConnectionTimeoutInMs", configuredTimetoutInMs.ToString() }
                })
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom(configuration.GetSection("FellowOakDicom"));
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var clientFactory = serviceProvider.GetService<IDicomClientFactory>();
            Assert.NotNull(clientFactory);

            var clientOptions = serviceProvider.GetService<IOptions<DicomClientOptions>>();
            Assert.NotNull(clientOptions?.Value);
            Assert.Equal(configuredTimetoutInMs, clientOptions.Value.ConnectionTimeoutInMs);

            var client = clientFactory.Create("localhost", 104, false, "TEST", "SERVER");
            Assert.NotNull(client);
            Assert.Equal(configuredTimetoutInMs, client.ClientOptions.ConnectionTimeoutInMs);
        }

        [Fact()]
        public async Task ConfigureDicomServiceOptionsInIConfiguration()
        {
            var defaultServiceOptions = new DicomServiceOptions();
            var defaultPduLength = defaultServiceOptions.MaxPDULength;
            var configuredPduLength = defaultPduLength / 2;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {"FellowOakDicom:DicomServiceOptions:MaxPDULength", configuredPduLength.ToString() }
                })
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom(configuration.GetSection("FellowOakDicom"));
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var clientFactory = serviceProvider.GetService<IDicomClientFactory>();
            Assert.NotNull(clientFactory);

            var serverFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            using var server = serverFactory.Create<DicomCEchoProvider>(0);

            var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
            await client.AddRequestAsync(new DicomCEchoRequest());

            uint serverPduInAssociationAccepted = 0;
            client.AssociationAccepted += (sender, e) => serverPduInAssociationAccepted = e.Association.MaximumPDULength;

            await client.SendAsync();
            Assert.Equal(configuredPduLength, serverPduInAssociationAccepted);
        }

        [Fact()]
        public async Task ConfigureDicomServiceOptionsInIConfigurationAndInSetup()
        {
            var defaultServiceOptions = new DicomServiceOptions();
            var defaultPduLength = defaultServiceOptions.MaxPDULength;
            var configuredPduLength = defaultPduLength + 100 ;
            var configuredPduLengthInSetup = defaultPduLength + 200;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {"FellowOakDicom:DicomServiceOptions:MaxPDULength", configuredPduLength.ToString() }
                })
                .Build();

            var serviceCollection = new ServiceCollection()
                // both configurations are applied, the IConfiguration and the config-Action. In that case, the config-action should win.
                .AddFellowOakDicom(configuration.GetSection("FellowOakDicom"), configureServiceOptions: opt => opt.MaxPDULength = configuredPduLengthInSetup);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var clientFactory = serviceProvider.GetService<IDicomClientFactory>();
            Assert.NotNull(clientFactory);

            var serverFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            using var server = serverFactory.Create<DicomCEchoProvider>(0);

            var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
            await client.AddRequestAsync(new DicomCEchoRequest());

            uint serverPduInAssociationAccepted = 0;
            client.AssociationAccepted += (sender, e) => serverPduInAssociationAccepted = e.Association.MaximumPDULength;

            await client.SendAsync();
            Assert.Equal(configuredPduLengthInSetup, serverPduInAssociationAccepted);
        }

        [Fact()]
        public void ConfigureDicomClientOptionsInSetupAndIConfiguration()
        {
            var defaultClientSettings = new DicomClientOptions();
            var configuredTimetoutInMs = defaultClientSettings.ConnectionTimeoutInMs + 1000;
            var otherConfiguredTimeout = defaultClientSettings.ConnectionTimeoutInMs + 2000;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {"FellowOakDicom:DicomClientOptions:ConnectionTimeoutInMs", otherConfiguredTimeout.ToString() }
                })
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom(configuration.GetSection("FellowOakDicom"),
                configureClientOptions: (opt) => 
                opt.ConnectionTimeoutInMs = configuredTimetoutInMs
                );
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var clientFactory = serviceProvider.GetService<IDicomClientFactory>();
            Assert.NotNull(clientFactory);

            var clientOptions = serviceProvider.GetService<IOptions<DicomClientOptions>>();
            Assert.NotNull(clientOptions?.Value);
            Assert.Equal(configuredTimetoutInMs, clientOptions.Value.ConnectionTimeoutInMs);

            var client = clientFactory.Create("localhost", 104, false, "TEST", "SERVER");
            Assert.NotNull(client);
            Assert.Equal(configuredTimetoutInMs, client.ClientOptions.ConnectionTimeoutInMs);
        }

    }


}
