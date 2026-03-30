// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log.Metrics;
using FellowOakDicom.Network;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace FellowOakDicom.Instrumentation
{
    internal class NetworkMetricCollector : INetworkMetricsCollector
    {
        private readonly Counter<long> _bytesSent;
        private readonly Counter<long> _bytesReceived;
        private readonly Counter<int> _connections;
        private readonly Gauge<int> _concurrentConnections;

        private readonly IOptions<MetricsOptions> _options;
        private readonly IDicomServerRegistry _serverRegistry;

        public NetworkMetricCollector(IMeterFactory meterFactory, IOptions<MetricsOptions> options, IDicomServerRegistry serverRegistry)
        {
            var meter = meterFactory.Create("fellowoakdicom.core");
            Source = new ActivitySource("fellowoakdicom.core");
            _bytesSent = meter.CreateCounter<long>("fellowoakdicom.network.sent", "bytes");
            _bytesReceived = meter.CreateCounter<long>("fellowoakdicom.network.received", "bytes");
            _connections = meter.CreateCounter<int>("fellowoakdicom.network.connections", "# connections");
            _concurrentConnections = meter.CreateGauge<int>("fellowoakdicom.network.concurrentconnections", "# connections");
            _options = options;
            _serverRegistry = serverRegistry;
        }

        public ActivitySource Source { get; private set; }

        public void ConnectionClosed(DicomService service)
        {
            if (service.RunsAsServer && (_serverRegistry.Get(service.LocalPort) is DicomServerRegistration serverRegistry))
            {
                var clients = serverRegistry.DicomServer.GetNumberOfConnectedClients();
                // this connection here is about to be closed, but will stil be contained in the clients list, so subtract 1
                _concurrentConnections.Record(clients - 1, new KeyValuePair<string, object?>("LocalPort", service.LocalPort));
            }
        }

        public void ConnectionEstablished(DicomService service)
        {
            _connections.Add(1, GetTagsFromDicomService(service));
            if (service.RunsAsServer && (_serverRegistry.Get(service.LocalPort) is DicomServerRegistration serverRegistry))
            {
                var clients = serverRegistry.DicomServer.GetNumberOfConnectedClients();
                _concurrentConnections.Record(clients, new KeyValuePair<string, object?>("LocalPort", service.LocalPort));
            }
        }

        public void DataReceived(long numberOfBytes, DicomService service)
        {
            _bytesReceived.Add(numberOfBytes, GetTagsFromDicomService(service));
        }

        public void DataSent(long numberOfBytes, DicomService service)
        {
            _bytesSent.Add(numberOfBytes, GetTagsFromDicomService(service));
        }

        private KeyValuePair<string, object?>[] GetTagsFromDicomService(DicomService service)
        {
            var tags = new List<KeyValuePair<string, object?>>();
            if (_options.Value.RecordMetricsByLocalPort && service.RunsAsServer)
            {
                tags.Add(new KeyValuePair<string, object?>("LocalPort", service.LocalPort));
            }
            if (_options.Value.RecordMetricsByServiceClass)
            {
                tags.Add(new KeyValuePair<string, object?>("ServiceClass", service.RunsAsServer ? "SCP" : "SCU"));
            }
            return tags.ToArray();
        }
    }
}
