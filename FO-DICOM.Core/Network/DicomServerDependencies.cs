using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Network
{
    public class DicomServerDependencies
    {
        public INetworkManager NetworkManager { get; }
        public ILoggerFactory LoggerFactory { get; }

        public DicomServerDependencies(INetworkManager networkManager, ILoggerFactory loggerFactory)
        {
            NetworkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
    }
}