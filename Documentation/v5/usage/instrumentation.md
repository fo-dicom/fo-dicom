## How to use instrumentation in an asp.net core application

### in Startup
After FellowOadkDicom was configured in services, additionally call

```csharp
   services.AddFellowOakDicomInstrumentation(opt => 
      // optionally do some configuration
      opt.RecordMetricsByServiceClass = true);
```

Then initialize OpenTelemetry and add metrics instrumentation and tracing instrumentation.

This is a sample configuration:

```csharp
    services.AddOpenTelemetry()
        .ConfigureResource(res =>
            res.AddService("MyApplicationName")
                .AddAttributes([
                    new KeyValuePair<string, object>("Hostname", Environment.MachineName)
                ])
        )
        .WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                // Add this in order to recored fo-dicom metrics
                .AddFellowOakDicomInstrumentation()
                .AddRuntimeInstrumentation();
        })
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                // Add this in order to recored fo-dicom traces
                .AddFellowOakDicomInstrumentation();
        })
        .UseOtlpExporter(OpenTelemetry.Exporter.OtlpExportProtocol.Grpc, new Uri("http://localhost:4317"));
```

In order that the metrics of DicomServer and DicomClient are recoreded, they have to be created from DI.

So do not call `DicomServerFactory.Create` or `DicomClientFactory.Create` direclty, but resolve `IDicomServerFactory` and `IDicomClientFactory` from the serice provider and use these factories to create the DicomServer or DicomClient.

