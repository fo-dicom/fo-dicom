// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System.Diagnostics;

namespace FellowOakDicom.Log.Metrics
{
    public interface INetworkMetricsCollector
    {

        public void DataSent(long numberOfBytes, DicomService service);

        public void DataReceived(long numberOfBytes, DicomService service);

        public void ConnectionEstablished(DicomService service);

        public void ConnectionClosed(DicomService service);

        public ActivitySource Source { get; }
    }
}
