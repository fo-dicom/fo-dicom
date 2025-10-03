// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom;

namespace FellowOakDicom.Log.Metrics
{
    public interface INetworkMetricsCollector
    {

        public void DataSent(long numberOfBytes);

        public void DataReceived(long numberOfBytes);

        public void ConnectionEstablished();

        public void ConnectionClosed();
    }
}
