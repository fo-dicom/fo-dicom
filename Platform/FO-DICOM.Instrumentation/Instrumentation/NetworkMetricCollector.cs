using FellowOakDicom.Log.Metrics;
using System.Diagnostics.Metrics;

namespace FellowOakDicom.Instrumentation
{
    internal class NetworkMetricCollector : INetworkMetricsCollector
    {
        private readonly Counter<long> _bytesSent;
        private readonly Counter<long> _bytesReceived;
        private readonly UpDownCounter<int> _connections;

        public NetworkMetricCollector(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create("fellowoakdicom.core");
            _bytesSent = meter.CreateCounter<long>("fo-dicom.network.sent", "bytes");
            _bytesReceived = meter.CreateCounter<long>("fo-dicom.network.received", "bytes");
            _connections = meter.CreateUpDownCounter<int>("fo-dicom.network.connections", "# connections");
        }

        public void ConnectionClosed() => _connections.Add(-1);
        public void ConnectionEstablished() => _connections.Add(1);
        public void DataReceived(long numberOfBytes) => _bytesReceived.Add(numberOfBytes);
        public void DataSent(long numberOfBytes) => _bytesSent.Add(numberOfBytes);
    }
}
