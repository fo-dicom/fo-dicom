// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network.Client.Advanced
{
    [Collection("Network"), Trait("Category", "Network")]
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

        private IAdvancedDicomClientConnectionFactory CreateConnectionFactory()
        {
            return Setup.ServiceProvider.GetRequiredService<IAdvancedDicomClientConnectionFactory>();
        }

        #endregion

        [Fact]
        public async Task OpenConnection_LoggerIsOptional()
        {
            var port = Ports.GetNext();
            var cancellationToken = CancellationToken.None;

            using var server = CreateServer<AsyncDicomCEchoProvider>(port);

            var connectionFactory = CreateConnectionFactory();

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
                connection = await connectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
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

            var connectionFactory = CreateConnectionFactory();

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
                connection = await connectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
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

            var connectionFactory = CreateConnectionFactory();

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
                connection = await connectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
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

            var connectionFactory = CreateConnectionFactory();

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
                connection = await connectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
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

            var connectionFactory = CreateConnectionFactory();

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
                connection1 = await connectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
                connection2 = await connectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
                connection3 = await connectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);
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
