using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
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

        public DicomServiceDependencies(ILoggerFactory loggerFactory, INetworkManager networkManager, ITranscoderManager transcoderManager, IMemoryProvider memoryProvider)
        {
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            NetworkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            TranscoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
            MemoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
        }
    }
}