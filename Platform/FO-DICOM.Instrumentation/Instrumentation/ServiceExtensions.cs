using FellowOakDicom.Log.Metrics;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

namespace FellowOakDicom.Instrumentation
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddFellowOakDicomInstrumentation(this IServiceCollection services)
        {
            services.AddSingleton<INetworkMetricsCollector, NetworkMetricCollector>();
            return services;
        }


        public static MeterProviderBuilder AddFellowOakDicomInstrumentation(this MeterProviderBuilder providerBuilder)
        {
            return providerBuilder
                .AddMeter("fellowoakdicom.core");
        } 

    }
}
