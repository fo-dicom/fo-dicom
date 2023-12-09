// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Memory;
using FellowOakDicom.Network;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Network)]
    public class GH1678
    {
        [Fact]
        public async Task WhenReceivingAnHttpRequest_ShouldCloseConnectionAndNotAllocateLargeBuffer()
        {
            // Arrange
            var port = Ports.GetNext();
            var recordingMemoryProvider = new RecordingMemoryProvider(new ArrayPoolMemoryProvider());
            var serviceProvider = new ServiceCollection()
                .AddFellowOakDicom()
                .Replace(ServiceDescriptor.Singleton<IMemoryProvider>(recordingMemoryProvider))
                .BuildServiceProvider();
            var dicomServerFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            using var server = dicomServerFactory.Create<DicomCEchoProvider>(port);
            using var httpClient = new HttpClient();
            HttpRequestException capturedHttpRequestException = null;
            OperationCanceledException capturedOperationCanceledException = null;

            // Act
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
                await httpClient.GetAsync($"http://localhost:{port}/", cts.Token);
            }
            catch (HttpRequestException e)
            {
                capturedHttpRequestException = e;
            }
            catch (OperationCanceledException e)
            {
                capturedOperationCanceledException = e;
            }

            // Assert
            const int oneGigaByte = 1024 * 1024 * 1024;
            Assert.All(recordingMemoryProvider.RequestedLengths, length => Assert.True(length < oneGigaByte));
            Assert.NotNull(capturedHttpRequestException);
            Assert.Null(capturedOperationCanceledException);
        }

        private class RecordingMemoryProvider : IMemoryProvider
        {
            private readonly IMemoryProvider _inner;

            public RecordingMemoryProvider(IMemoryProvider inner)
            {
                _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            }

            public List<int> RequestedLengths { get; } = new List<int>();

            public IMemory Provide(int length)
            {
                RequestedLengths.Add(length);
                return _inner.Provide(length);
            }
        }

    }
}
