// Copyright (c) 2012-2025 fo-dicom contributors.
﻿using FellowOakDicom.Log.Metrics;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

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

    }
}
