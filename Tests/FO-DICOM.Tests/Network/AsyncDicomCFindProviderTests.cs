// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class AsyncDicomCFindProviderTests
    {
        private readonly XUnitDicomLogger _logger;

        public AsyncDicomCFindProviderTests(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        [Fact]
        public async Task OnCFindRequestAsync_ImmediateSuccess_ShouldRespond()
        {
            using var server = DicomServerFactory.Create<ImmediateSuccessAsyncDicomCFindProvider>(0, logger: _logger.IncludePrefix("DicomServer"));
            var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
            client.Logger = _logger.IncludePrefix(nameof(DicomClient));
            client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

            DicomCFindResponse response = null;
            DicomRequest.OnTimeoutEventArgs timeout = null;
            var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Study)
            {
                OnResponseReceived = (req, res) => response = res,
                OnTimeout = (sender, args) => timeout = args
            };

            await client.AddRequestAsync(request);
            await client.SendAsync();

            Assert.NotNull(response);
            Assert.Equal(DicomStatus.Success, response.Status);
            Assert.Null(timeout);
        }

        [Fact]
        public async Task OnCFindRequestAsync_Pending_ShouldRespond()
        {
            var counter = new InvokeCounter();
            using var server = DicomServerFactory.Create<PendingAsyncDicomCFindProvider>(0, logger: _logger.IncludePrefix("DicomServer"), userState: counter);

            var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
            client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name);
            client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;

            var responses = new ConcurrentQueue<DicomCFindResponse>();
            DicomRequest.OnTimeoutEventArgs timeout = null;
            var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Study)
            {
                OnResponseReceived = (req, res) => responses.Enqueue(res),
                OnTimeout = (sender, args) => timeout = args
            };

            await client.AddRequestAsync(request);
            await client.SendAsync();

            Assert.Collection(
                responses,
                response1 => Assert.Equal(DicomStatus.Pending, response1.Status),
                response2 => Assert.Equal(DicomStatus.Pending, response2.Status),
                response3 => Assert.Equal(DicomStatus.Success, response3.Status)
            );
            Assert.Null(timeout);

            await Task.Delay(1000);
            Assert.Equal(0, counter.AbortCounter);
            Assert.Equal(1, counter.ConnectionClosedCounter);
            Assert.Equal(0, counter.AbortAsyncCounter);
            Assert.Equal(0, counter.ConnectionClosedAsyncCounter);
        }

        [Fact]
        public async Task OnCFindRequestAsync_Pending_WithAsyncService_ShouldRespond()
        {
            var counter = new InvokeCounter();
            using var server = DicomServerFactory.Create<PendingAsyncDicomCFindProviderWithAsyncService>(0, logger: _logger.IncludePrefix("DicomServer"), userState: counter);

            var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
            client.Logger = _logger.IncludePrefix(typeof(DicomClient).Name);
            client.ClientOptions.AssociationRequestTimeoutInMs = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;

            var responses = new ConcurrentQueue<DicomCFindResponse>();
            DicomRequest.OnTimeoutEventArgs timeout = null;
            var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Study)
            {
                OnResponseReceived = (req, res) => responses.Enqueue(res),
                OnTimeout = (sender, args) => timeout = args
            };

            await client.AddRequestAsync(request);
            await client.SendAsync();

            Assert.Collection(
                responses,
                response1 => Assert.Equal(DicomStatus.Pending, response1.Status),
                response2 => Assert.Equal(DicomStatus.Pending, response2.Status),
                response3 => Assert.Equal(DicomStatus.Success, response3.Status)
            );
            Assert.Null(timeout);

            await Task.Delay(1000);
            Assert.Equal(0, counter.AbortCounter);
            Assert.Equal(0, counter.ConnectionClosedCounter);
            Assert.Equal(0, counter.AbortAsyncCounter);
            Assert.Equal(1, counter.ConnectionClosedAsyncCounter);
        }

        [Fact]
        public async Task OnCFindRequestAsync_Pending_WithAsyncService_ShouldCallAbortAsync()
        {
            var cancellationToken = CancellationToken.None;
            var counter = new InvokeCounter();
            using var server = DicomServerFactory.Create<PendingAsyncDicomCFindProviderWithAsyncService>(0, logger: _logger.IncludePrefix("DicomServer"), userState: counter);

            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port
                },
                Logger = _logger.IncludePrefix("client"),
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };

            using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

            var openAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "ANY-SCP"
            };

            DicomRequest.OnTimeoutEventArgs timeout = null;
            var request = new DicomCFindRequest(DicomQueryRetrieveLevel.Study)
            {
                OnTimeout = (sender, args) => timeout = args
            };

            openAssociationRequest.PresentationContexts.AddFromRequest(request);
            openAssociationRequest.ExtendedNegotiations.AddFromRequest(request);

            IAdvancedDicomClientAssociation association = null;
            DicomAssociationRejectedException exception = null;
            try
            {
                association = await connection.OpenAssociationAsync(openAssociationRequest, cancellationToken);
                await association.AbortAsync(cancellationToken);
            }
            catch (DicomAssociationRejectedException e)
            {
                exception = e;
            }
            finally
            {
                if (association != null)
                {
                    await association.ReleaseAsync(cancellationToken);
                    association.Dispose();
                }
            }

            Assert.Null(timeout);

            await Task.Delay(1000);
            Assert.Equal(0, counter.AbortCounter);
            Assert.Equal(0, counter.ConnectionClosedCounter);
            Assert.Equal(1, counter.AbortAsyncCounter);
            Assert.Equal(1, counter.ConnectionClosedAsyncCounter);
        }
    }

    #region helper classes

    public class InvokeCounter
    {
        public int AbortCounter { get; set; }
        public int AbortAsyncCounter { get; set; }
        public int ConnectionClosedCounter { get; set; }
        public int ConnectionClosedAsyncCounter { get; set; }
    }

    public class ImmediateSuccessAsyncDicomCFindProvider : DicomService, IDicomServiceProvider, IDicomCFindProvider
    {
        public ImmediateSuccessAsyncDicomCFindProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
            DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(DicomPresentationContextResult.Accept);
            }

            await SendAssociationAcceptAsync(association);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationReleaseRequestAsync()
            => await SendAssociationReleaseResponseAsync().ConfigureAwait(false);

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            // do nothing here
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
            // do nothing here
        }

        public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request)
        {
            await Task.Yield();
            yield return new DicomCFindResponse(request, DicomStatus.Success);
        }

    }


    public class PendingAsyncDicomCFindProvider : DicomService, IDicomServiceProvider, IDicomCFindProvider
    {
        public PendingAsyncDicomCFindProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
            DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(DicomPresentationContextResult.Accept);
            }

            await SendAssociationAcceptAsync(association);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationReleaseRequestAsync()
            => await SendAssociationReleaseResponseAsync().ConfigureAwait(false);

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            if (UserState is InvokeCounter counter)
            {
                counter.AbortCounter++;
            }
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
            if (UserState is InvokeCounter counter)
            {
                counter.ConnectionClosedCounter++;
            }
        }

        public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request)
        {
            await Task.Yield();
            yield return new DicomCFindResponse(request, DicomStatus.Pending);
            await Task.Yield();
            yield return new DicomCFindResponse(request, DicomStatus.Pending);
            await Task.Yield();
            yield return new DicomCFindResponse(request, DicomStatus.Success);
        }

    }

    public class PendingAsyncDicomCFindProviderWithAsyncService : PendingAsyncDicomCFindProvider, IAsyncDicomService
    {
        public PendingAsyncDicomCFindProviderWithAsyncService(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies) : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        public Task OnConnectionClosedAsync(Exception exception)
        {
            if (UserState is InvokeCounter counter)
            {
                counter.ConnectionClosedAsyncCounter++;
            }
            return Task.CompletedTask;
        }

        public Task OnReceiveAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            if (UserState is InvokeCounter counter)
            {
                counter.AbortAsyncCounter++;
            }
            return Task.CompletedTask;
        }
    }

    #endregion
}
