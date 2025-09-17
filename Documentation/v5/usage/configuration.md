There are 3 classes of various options that configure the behavior of fo-dicom.
In `DicomClientOptions` there are the properties to configure the `DicomClient`, with `DicomServerOptions` the behavior of the `DicomServer` is configured, and `DicomServiceOptions` is used in both for the behavior of the underlying network service.

*fo-dicom* uses the [Options pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-9.0). The configuration and the default values for them are registered as options in the DI-container. Whenever an instance of DicomClient or DicomServer is created, the current values of the options are loaded from DI and copied to the instance. The values for the instance are then not changed any more to avoid inconsistencies.
The current values for an instance can be accessed via the Options-properties.

The values of the options can be changed in several places

### Directly change the options of an instance

After an instance of a DicomClient or a DicomServer is created, the settings of these instances can be changed, before they start their network connections.
These settings only affect the instance where the values are changed.

```csharp
var client = clientFactory.Create("localhost", 104, false, "TEST", "SERVER");
// now change the values of the options as required
client.ClientOptions.MaximumNumberOfRequestsPerAssociation = 1;
client.ServiceOptions.MaxPDULength = 64000;

//...
await client.SendAsync();
```

```csharp
using var server = serverFactory.Create<DicomCEchoProvider>(port);
// change the options of the DicomServer
server.Options.UseRemoteAEForLogName = true;
```

### Passing configure-actions to factories or to setup

As described in the options pattern, fo-dicom accepts configuration functions as paraemters in factories or at startup of the application. These configuration actions are then always executed and change the default values of the registered options classes.

```csharp
using var server = serverFactory.Create<DicomCEchoProvider>(port, configure: o => o.MaxClientsAllowed = 1);
```

```csharp
new DicomSetupBuilder()
    .RegisterServices(s => s.AddFellowOakDicom(
        configureServiceOptions: o =>
        {
            o.LogDataPDUs = true;
            o.LogDimseDatasets = true;
        },
        configureClientOptions: o =>
        {
            o.AssociationRequestTimeoutInMs = 5000;
            o.AssociationReleaseTimeoutInMs = 1000;
        }))
    .Build();
```

### Binding to IConfiguration

Many applications already are using IConfiguration to handle configuration. The values then can be changed at runtime via appsettings.json files or environment variables, or any other registered provider.

On Startup, a section of the IConfiguration can be passed as argument. *fo-dicom* then tries to extract the values from that section. It is assumed, that all the values are located in sub-sections, where the name of the section has to match the class name and the name of the value has to match the property name.
The name of the section where *fo-dicom* searches for the value can be choosen freely. It is also possible to pass the whole Configuration as parameter, but it is not recommended.

Consider the following code snippet, where on application startup the section `FellowOakDicom` of the IConfiguration is bound to the options.

```csharp
public class Startup
{
    // .. configuration is initialized 

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddFellowOakDicom(Configuration.GetSecion("FellowOakDicom"));
        //...
    }

}
```

Then later at runtime, if you want to change some properties of DicomServiceOptions and DicomClientOptions, then add the following to your appsettings.json

```json
{
  FellowOakDicom: {
     DicomServiceOptions: {
       MaxPDULength: 512000
     },
     DicomClientOptions: {
       ConnectionTimeoutInMs: 2000
     }
  }
}
```

or add the following values to your environment variables:

```
FellowOakDicom__DicomServiceOptions__MaxPDULength=512000
FellowOakDicom__DicomClientOptions__ConnectionTimeoutInMs=2000
```

### Priority of evaluation

1. By default *fo-dicom* ships the options with some default values that are applied if no other configuration is set.
2. Then if some Configuration is bound to the options, then these values overrule the built-in default values.
3. Any configuration method that is passed as parameter in factories or at startup then overrides the values read from Configuration
4. Changing the properties of a created instance directly has the highest priority and will be used in any case and not changed by any other configuration mechanism.

