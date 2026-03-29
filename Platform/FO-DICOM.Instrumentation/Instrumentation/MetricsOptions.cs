// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Instrumentation
{
    public class MetricsOptions
    {

        /// <summary>
        /// Configures if there should be separate metricsseries for SCU and SCP or if there should be one combined series
        /// </summary>
        public bool RecordMetricsByServiceClass { get; set; } = false;

        /// <summary>
        /// Configures if there should be separate mtricsseries per local listening ports for SCP services
        /// </summary>
        public bool RecordMetricsByLocalPort { get; set; } = false;

    }
}
