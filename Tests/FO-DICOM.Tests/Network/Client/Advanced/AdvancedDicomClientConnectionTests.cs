// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network.Client.Advanced
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class AdvancedDicomClientConnectionTests
    {
        #region Fields

        private readonly XUnitDicomLogger _logger;

        #endregion

        #region Constructors

        public AdvancedDicomClientConnectionTests(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        #endregion

        #region Helper functions

        private IDicomServer CreateServer<T>(int port) where T : DicomService, IDicomServiceProvider
        {
            var server = DicomServerFactory.Create<T>(port);
            server.Logger = _logger.IncludePrefix(nameof(IDicomServer));
            return server;
        }

        #endregion

        [Fact]
        public async Task OpenConnection_LoggerIsOptional()
        {
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger,
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task OpenConnection_DicomServiceOptionsIsOptional()
        {
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger,
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = null
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task OpenConnection_FallbackEncodingIsOptional()
        {
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger,
                FallbackEncoding = null,
                DicomServiceOptions = new DicomServiceOptions()
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.Null(exception);
        }

        [Fact]
        public async Task OpenConnection_NetworkStreamCreationOptionsIsRequired()
        {
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = null,
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = null,
                DicomServiceOptions = new DicomServiceOptions()
            };

            IAdvancedDicomClientConnection connection = null;
            Exception exception = null;
            try
            {
                connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection?.Dispose();
            }

            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task OpenConnection_CanBeCalledMultipleTimes()
        {
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = _logger.IncludePrefix("Client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            IAdvancedDicomClientConnection connection1 = null;
            IAdvancedDicomClientConnection connection2 = null;
            IAdvancedDicomClientConnection connection3 = null;
            Exception exception = null;
            try
            {
                connection1 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
                connection2 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
                connection3 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                connection1?.Dispose();
                connection2?.Dispose();
                connection3?.Dispose();
            }

            Assert.Null(exception);
        }
    }
}
