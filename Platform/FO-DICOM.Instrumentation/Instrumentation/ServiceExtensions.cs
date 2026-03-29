// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log.Metrics;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System;

namespace FellowOakDicom.Instrumentation
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddFellowOakDicomInstrumentation(this IServiceCollection services, Action<MetricsOptions> configureMetrics = null)
        {
            services.AddSingleton<INetworkMetricsCollector, NetworkMetricCollector>();
            services.AddOptions<MetricsOptions>();
            if (configureMetrics != null)
            {
                services.PostConfigure<MetricsOptions>(configureMetrics);
            }
            return services;
        }


        public static MeterProviderBuilder AddFellowOakDicomInstrumentation(this MeterProviderBuilder providerBuilder)
        {
            return providerBuilder
                .AddMeter("fellowoakdicom.core");
        } 

        public static TracerProviderBuilder AddFellowOakDicomInstrumentation(this TracerProviderBuilder providerBuilder)
        {
            return providerBuilder
                .AddSource("fellowoakdicom.core");
        }

    }
}
