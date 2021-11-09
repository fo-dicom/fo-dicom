using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;

namespace FellowOakDicom.Benchmark.Infrastructure
{
    /// <summary>
    /// Can be used to receive a port that should still be free
    /// </summary>
    public static class Ports
    {
        private static readonly object Lock = new object();
        private static int _lastPort = 20000;
        private static ISet<int> _occupiedPorts;

        public static int GetNext()
        {
            if (_occupiedPorts == null)
            {
                lock (Lock)
                {
                    if (_occupiedPorts == null)
                    {
                        var occupiedPorts = new HashSet<int>();

                        var properties = IPGlobalProperties.GetIPGlobalProperties();

                        foreach (var connection in properties.GetActiveTcpConnections())
                        {
                            occupiedPorts.Add(connection.LocalEndPoint.Port);
                        }

                        foreach (var listener in properties.GetActiveTcpListeners())
                        {
                            occupiedPorts.Add(listener.Port);
                        }

                        foreach (var listener in properties.GetActiveUdpListeners())
                        {
                            occupiedPorts.Add(listener.Port);
                        }

                        _occupiedPorts = occupiedPorts;
                    }
                }
            }

            var port = Interlocked.Increment(ref _lastPort);

            while (_occupiedPorts.Contains(port))
                port = Interlocked.Increment(ref _lastPort);

            return port;
        }
    }
}
