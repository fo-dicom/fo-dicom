using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Memory;
using System;

namespace FellowOakDicom.Network
{
    public class DicomServiceDependencies
    {
        public ILogManager LogManager { get; }
        public INetworkManager NetworkManager { get; }
        public ITranscoderManager TranscoderManager { get; }
        public IMemoryProvider MemoryProvider { get; }

        public DicomServiceDependencies(ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager, IMemoryProvider memoryProvider)
        {
            LogManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            NetworkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            TranscoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
            MemoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
        }
    }
}