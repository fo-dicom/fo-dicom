// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Network
{
    public class DicomServiceDependencies
    {
        public ILoggerFactory LoggerFactory { get; }
        public INetworkManager NetworkManager { get; }
        public ITranscoderManager TranscoderManager { get; }
        public IMemoryProvider MemoryProvider { get; }

        public IServiceProvider ServiceProvider { get; }

        public DicomServiceDependencies(ILoggerFactory loggerFactory, INetworkManager networkManager, ITranscoderManager transcoderManager, IMemoryProvider memoryProvider, IServiceProvider serviceProvider)
        {
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            NetworkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            TranscoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
            MemoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException($"{nameof(serviceProvider)}");
        }
    }
}