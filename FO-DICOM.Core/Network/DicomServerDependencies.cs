using FellowOakDicom.Log;
using System;

namespace FellowOakDicom.Network
{
    public class DicomServerDependencies
    {
        public INetworkManager NetworkManager { get; }
        public ILogManager LogManager { get; }

        public DicomServerDependencies(INetworkManager networkManager, ILogManager logManager)
        {
            NetworkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            LogManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }
    }
}