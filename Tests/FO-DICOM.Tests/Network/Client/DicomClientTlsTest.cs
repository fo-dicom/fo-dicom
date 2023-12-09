// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Tls;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static FellowOakDicom.Tests.Network.Client.DicomClientTest;

namespace FellowOakDicom.Tests.Network.Client
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class DicomClientTlsTest
    {
        #region Fields

        private readonly XUnitDicomLogger _logger;

        #endregion

        #region Constructors

        public DicomClientTlsTest(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        #endregion

        #region Helper functions

        private TServer CreateServer<TProvider, TServer>(string ipAddress, int port, ITlsAcceptor tlsAcceptor = null)
            where TProvider : DicomService, IDicomServiceProvider
            where TServer : class, IDicomServer<TProvider>
        {
            var logger = _logger.IncludePrefix(nameof(IDicomServer));
            var server = DicomServerFactory.Create<TProvider, TServer>(ipAddress, port, logger: logger, tlsAcceptor: tlsAcceptor);
            server.Options.LogDimseDatasets = false;
            server.Options.LogDataPDUs = false;
            return server as TServer;
        }

        private IDicomClient CreateClient(string host, int port, ITlsInitiator tlsInitiator, string callingAe, string calledAe)
        {
            var client = DicomClientFactory.Create(host, port, tlsInitiator, callingAe, calledAe);
            client.Logger = _logger.IncludePrefix(nameof(DicomClient));
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            return client;
        }

        private void AllResponsesShouldHaveSucceeded(IEnumerable<DicomCEchoResponse> responses)
        {
            var logger = _logger.IncludePrefix("Responses");
            foreach (var r in responses)
            {
                logger.LogInformation($"{r.Type} [{r.RequestMessageID}]: " +
                            $"Status = {r.Status.State}, " +
                            $"Code = {r.Status.Code}, " +
                            $"ErrorComment = {r.Status.ErrorComment}, " +
                            $"Description = {r.Status.Description}");

                Assert.Equal(DicomState.Success, r.Status.State);
            }
        }

        #endregion

        [Theory]
#if !NET462
        [InlineData(true)]
#endif
        [InlineData(false)]
        public async Task SendAsync_WithClientCertificate_ShouldAuthenticate(bool requireMutualAuthentication)
        {
            // Arrange
            var port = Ports.GetNext();
            var serverLogger = _logger.IncludePrefix(nameof(IDicomServer));

            var tlsAcceptor = new DefaultTlsAcceptor("./Test Data/FellowOakDicom.pfx", "FellowOakDicom")
            {
                RequireMutualAuthentication = requireMutualAuthentication,
                CertificateValidationCallback = (sender, x509Certificate, chain, errors) =>
                {
                    if (errors != SslPolicyErrors.None)
                    {
                        switch (errors)
                        {
                            case SslPolicyErrors.RemoteCertificateNotAvailable:
                                serverLogger.LogDebug("SSL policy errors: client certificate is missing");
                                if (!requireMutualAuthentication)
                                {
                                    // No remote certificate needed if mutual authentication is disabled
                                    return true;
                                }
                                break;
                            case SslPolicyErrors.RemoteCertificateNameMismatch:
                                serverLogger.LogDebug("SSL policy errors: client certificate name mismatch");
                                break;
                            case SslPolicyErrors.RemoteCertificateChainErrors:
                                serverLogger.LogDebug("SSL policy errors: validation error somewhere in the chain validation of the certificate");
                                break;
                        }

                        if (chain != null)
                        {
                            for (var index = 0; index < chain.ChainStatus.Length; index++)
                            {
                                var chainStatus = chain.ChainStatus[index];
                                serverLogger.LogDebug($"SSL Chain status [{index}]: {chainStatus.Status} {chainStatus.StatusInformation}");

                                // Since we're using a self signed certificate, it's obvious the root will be untrusted. That's okay for this test
                                if (chainStatus.Status.HasFlag(X509ChainStatusFlags.UntrustedRoot))
                                    return true;
                                // since we're using a generated client from an unknown CA, the chain will not be checked successfully.
                                if (chainStatus.Status.HasFlag(X509ChainStatusFlags.PartialChain))
                                    return true;
                            }
                        }

                        return false;
                    }

                    return true;
                }
            };

            using var server = CreateServer<RecordingDicomCEchoProvider, RecordingDicomCEchoProviderServer>("127.0.0.1", port, tlsAcceptor: tlsAcceptor);

            var tlsInitiator = new DefaultTlsInitiator();
            var client = CreateClient("127.0.0.1", port, tlsInitiator, "SCU", "ANY-SCP");

            tlsInitiator.CertificateValidationCallback = (sender, x509Certificate, chain, errors) =>
                {
                    if (errors != SslPolicyErrors.None)
                    {
                        switch (errors)
                        {
                            case SslPolicyErrors.RemoteCertificateNotAvailable:
                                client.Logger.LogDebug("SSL policy errors: server certificate is missing");
                                break;
                            case SslPolicyErrors.RemoteCertificateNameMismatch:
                                client.Logger.LogDebug("SSL policy errors: server certificate name mismatch");
                                break;
                            case SslPolicyErrors.RemoteCertificateChainErrors:
                                client.Logger.LogDebug("SSL policy errors: validation error somewhere in the chain validation of the server certificate");
                                break;
                        }

                        for (var index = 0; index < chain.ChainStatus.Length; index++)
                        {
                            var chainStatus = chain.ChainStatus[index];
                            client.Logger.LogDebug($"SSL Chain status [{index}]: {chainStatus.Status} {chainStatus.StatusInformation}");

                            // Since we're using a self signed certificate, it's obvious the root will be untrusted. That's okay for this test
                            if (chainStatus.Status.HasFlag(X509ChainStatusFlags.UntrustedRoot))
                                return true;
                        }

                        return false;
                    }

                    return true;
                };
            if (requireMutualAuthentication)
            {
                tlsInitiator.Certificates = new X509CertificateCollection { new X509Certificate(TestData.Resolve("testclienteku.contoso.com.pfx"), "PLACEHOLDER") };
            }


            DicomCEchoResponse actualResponse = null;
            var dicomCEchoRequest = new DicomCEchoRequest
            {
                OnResponseReceived = (request, response) =>
                {
                    actualResponse = response;
                }
            };
            await client.AddRequestAsync(dicomCEchoRequest);

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
            {
                await client.SendAsync(cts.Token);
            }

            AllResponsesShouldHaveSucceeded(new[] { actualResponse });
        }

    }
}
