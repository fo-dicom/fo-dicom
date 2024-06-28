
![Fellow Oak DICOM Logo](https://raw.githubusercontent.com/fo-dicom/fo-dicom/6089255b6af35a04259b1219351fc40119ac5295/FellowOakSquareTransp.png)

# Fellow Oak DICOM

Fellow Oak DICOM is a DICOM toolkit in C# for all .NET Standard 2.0 compatible frameworks.

### License
This library is licensed under the [Microsoft Public License (MS-PL)](http://opensource.org/licenses/MS-PL). See [License.txt](License.txt) for more information.

### Features
* Targets .NET Standard 2.0
* DICOM dictionary version 2024c
* High-performance, fully asynchronous `async`/`await` API
* JPEG (including lossless), JPEG-LS, JPEG2000, HTJPEG2000, and RLE image compression (via additional package)
* Supports very large datasets with content loading on demand
* Image rendering to System.Drawing.Bitmap or SixLabors.ImageSharp
* JSON and XML export/import
* Anonymization
* DICOM services
* Customize components via DI container 

### Supported Runtimes

Fellow Oak DICOM officially supports the following runtimes:

* .NET Core 7.0
* .NET Core 6.0
* .NET Framework 4.6.2

Other runtimes that implement .NET Standard 2.0 may work, but be aware that our CI pipeline only tests these platforms (and only on Windows)

### Installation
Easiest is to obtain *fo-dicom* binaries from [NuGet](https://www.nuget.org/packages/fo-dicom/). This package reference the core *fo-dicom* assemblies for all Microsoft and Xamarin platforms.

### NuGet Packages
*Valid for version 5.0.0 and later*

Package | Description
------- | -----------
[fo-dicom](https://www.nuget.org/packages/fo-dicom/) | Core package containing parser, services and tools.
[fo-dicom.Imaging.Desktop](https://www.nuget.org/packages/fo-dicom.Imaging.Desktop/) | Library with referencte to System.Drawing, required for rendering into Bitmaps
[fo-dicom.Imaging.ImageSharp](https://www.nuget.org/packages/fo-dicom.Desktop/) | Library with reference to ImageSharp, can be used for platform independent rendering
[fo-dicom.Codecs](https://www.nuget.org/packages/fo-dicom.Codecs/) | Cross-platform Dicom codecs for fo-dicom, developed by Efferent Health (https://github.com/Efferent-Health/fo-dicom.Codecs)


### Documentation
Documentation, including API documentation, is available via GitHub pages:
- documentation for the latest release for [fo-dicom 4](https://fo-dicom.github.io/stable/v4/index.html) and
  [fo-dicom 5](https://fo-dicom.github.io/stable/v5/index.html)
- documentation for the development version for [fo-dicom 4](https://fo-dicom.github.io/dev/v4/index.html) and
  [fo-dicom 5](https://fo-dicom.github.io/dev/v5/index.html)

### Usage Notes

#### Getting started in modern .NET

If you are using the web application builder:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFellowOakDicom();
var app = builder.Build();
// This is still necessary for now until fo-dicom has first-class AspNetCore integration
DicomSetupBuilder.UseServiceProvider(app.Services);
```

If you are using the host builder:

```csharp
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddFellowOakDicom();
    })
    .Build();

// This is still necessary for now until fo-dicom has first-class AspNetCore integration
DicomSetupBuilder.UseServiceProvider(host.Services);
```

If you are not using the host builder, you'll need to make your own service collection:

```csharp
var services = new ServiceCollection();
services.AddFellowOakDicom();
var serviceProvider = services.BuildServiceProvider();
DicomSetupBuilder.UseServiceProvider(serviceProvider);
```

#### Getting started in .NET Framework

Use `DicomSetupBuilder` to configure the internals of Fellow Oak DICOM:

```csharp
new DicomSetupBuilder()
    .RegisterServices(s => s.AddFellowOakDicom())
.Build();
```

#### Dependency injection support

Whenever you use APIs of Fellow Oak DICOM such as `DicomFile.Open`, `DicomServerFactory.Create`, the global statically registered service provider (`DicomSetupBuilder.UseServiceProvider`) will be used to resolve dependencies.  
Please note that using dependency injection is generally preferred over the static APIs, if it is available.

| Use case                                      | Static API                                                 | Can use dependency injection ?                   |
| --------------------------------------------- |------------------------------------------------------------| ------------------------------------------------ |
| Creating a DICOM server                       | `DicomServerFactory.Create`                                | Yes, use `IDicomServerFactory`                   |
| Creating a DICOM client                       | `DicomClientFactory.Create`                                | Yes, use `IDicomClientFactory`                   |
| Creating an advanced DICOM client connection  | `AdvancedDicomClientConnectionFactory.OpenConnectionAsync` | Yes, use `IAdvancedDicomClientConnectionFactory` |
| Opening a DICOM file                          | `DicomFile.OpenAsync(..)`                                  | No                                               |
| Rendering a DICOM file                        | `new DicomImage(..).RenderImage(..)`                       | No                                               |

#### Injecting custom dependencies into DICOM services

It is possible to inject custom dependencies into your DICOM services, but with one important requirement: **you must have these exact three constructor parameters: `INetworkStream stream, Encoding fallbackEncoding, ILogger logger`**. 
The names and their order don't matter, but the types do.
Yes, `ILogger` is a non-generic typed logger. If you want a `Logger<T>`, you can **add** an extra constructor parameter and use it however you see fit.

Here is a full standalone example:

```csharp
using System.Text;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Information);
    })
    .ConfigureServices(services =>
    {
        services.AddFellowOakDicom();
        services.AddHostedService<Worker>();
        // This will be injected into the DICOM service
        services.AddSingleton<CustomDependency>();
    })
    .Build();

DicomSetupBuilder.UseServiceProvider(host.Services);

host.Run();

public class CustomDependency
{
    
}

public class Worker : IHostedService
{
    private readonly ILogger<Worker> _logger;
    private readonly IDicomServerFactory _dicomServerFactory;
    private readonly IDicomClientFactory _dicomClientFactory;
    private IDicomServer? _server;

    public Worker(ILogger<Worker> logger, IDicomServerFactory dicomServerFactory, IDicomClientFactory dicomClientFactory)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dicomServerFactory = dicomServerFactory ?? throw new ArgumentNullException(nameof(dicomServerFactory));
        _dicomClientFactory = dicomClientFactory ?? throw new ArgumentNullException(nameof(dicomClientFactory));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting DICOM server");
        _server = _dicomServerFactory.Create<EchoService>(104);
        _logger.LogInformation("DICOM server is running");

        var client = _dicomClientFactory.Create("127.0.0.1", 104, false, "AnySCU", "AnySCP");

        _logger.LogInformation("Sending C-ECHO request");
        DicomCEchoResponse? response = null;
        await client.AddRequestAsync(new DicomCEchoRequest { OnResponseReceived = (_, r) => response = r});
        await client.SendAsync(cancellationToken);
        if (response != null)
        {
            _logger.LogInformation("C-ECHO response received");
        }
        else
        {
            _logger.LogError("No C-ECHO response received");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (_server != null)
        {
            _server.Stop();
            _server.Dispose();
            _server = null;
        }
        return Task.CompletedTask;
    }
}

public class EchoService : DicomService, IDicomServiceProvider, IDicomCEchoProvider
{
    private readonly ILogger _logger;
    private readonly CustomDependency _customDependency;

    public EchoService(INetworkStream stream,
        Encoding fallbackEncoding, 
        ILogger logger,
        DicomServiceDependencies dependencies,
        CustomDependency customDependency) : base(stream, fallbackEncoding, logger, dependencies)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customDependency = customDependency ?? throw new ArgumentNullException(nameof(customDependency));
    }

    public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) => _logger.LogInformation("Received abort");
    public void OnConnectionClosed(Exception exception) => _logger.LogInformation("Connection closed");

    public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
    {
        foreach (DicomPresentationContext presentationContext in association.PresentationContexts)
            presentationContext.SetResult(DicomPresentationContextResult.Accept);
        return SendAssociationAcceptAsync(association);
    }

    public Task OnReceiveAssociationReleaseRequestAsync()
    {
        _logger.LogInformation("Received association release");
        return Task.CompletedTask;
    }

    public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request) => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
}
```


#### Image rendering configuration
Out-of-the-box, *fo-dicom* defaults to an internal class *FellowOakDicom.Imaging.IImage*-style image rendering. To switch to Desktop-style or ImageSharp-style image rendering, you first have to add the nuget package you desire and then call:

```csharp
new DicomSetupBuilder()
    .RegisterServices(s => s.AddFellowOakDicom().AddImageManager<WinFormsImageManager>())
.Build();
```

or

```csharp
new DicomSetupBuilder()
    .RegisterServices(s => s.AddFellowOakDicom().AddImageManager<ImageSharpImageManager>())
.Build();
```

Then when rendering you can cast the IImage to the type by

```csharp
var image = new DicomImage("filename.dcm");
var bitmap = image.RenderImage().As<Bitmap>();
```

or

```csharp
var image = new DicomImage("filename.dcm");
var sharpimage = image.RenderImage().AsSharpImage();
```
    
#### Logging configuration
Fellow Oak DICOM uses `Microsoft.Extensions.Logging`, so if you are already using that, Fellow Oak DICOM logging will show up automatically.

In the past, Fellow Oak DICOM had a custom abstraction for logging: ILogger and ILogManager.
For backwards compatibility purposes, this is still supported but not recommended for new applications.

```csharp
services.AddLogManager<MyLogManager>();
```

where MyLogManager looks like this:

```
using FellowOakDicom.Log;

public class MyLogManager: ILogManager {
    public ILogger GetLogger(string name) {
        ...
    }
}
```


### Sample applications
There are a number of simple sample applications that use *fo-dicom* available in separate repository [here](https://github.com/fo-dicom/fo-dicom-samples). These also include the samples
that were previously included in the *Examples* sub-folder of the VS solutions.

### Examples

#### File Operations
```csharp
var file = DicomFile.Open(@"test.dcm");             // Alt 1
var file = await DicomFile.OpenAsync(@"test.dcm");  // Alt 2

var patientid = file.Dataset.GetString(DicomTag.PatientID);

file.Dataset.AddOrUpdate(DicomTag.PatientName, "DOE^JOHN");

// creates a new instance of DicomFile
var newFile = file.Clone(DicomTransferSyntax.JPEGProcess14SV1);

file.Save(@"output.dcm");             // Alt 1
await file.SaveAsync(@"output.dcm");  // Alt 2
```

#### Render Image to JPEG
```csharp
var image = new DicomImage(@"test.dcm");
image.RenderImage().AsBitmap().Save(@"test.jpg");                     // Windows Forms

```

#### C-Store SCU
```csharp
var client = DicomClientFactory.Create("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
await client.AddRequestAsync(new DicomCStoreRequest(@"test.dcm"));
await client.SendAsync();
```

#### C-Echo SCU/SCP
```csharp
var server = DicomServerFactory.Create<DicomCEchoProvider>(12345);

var client = DicomClientFactory.Create("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
client.NegotiateAsyncOps();
// Optionally negotiate user identity
client.NegotiateUserIdentity(new DicomUserIdentityNegotiation
{
    UserIdentityType = DicomUserIdentityType.Jwt,
    PositiveResponseRequested = true,
    PrimaryField = "JWT_TOKEN"
});
for (int i = 0; i < 10; i++)
    await client.AddRequestAsync(new DicomCEchoRequest());
await client.SendAsync();
```

#### C-Find SCU
```csharp
var cfind = DicomCFindRequest.CreateStudyQuery(patientId: "12345");
cfind.OnResponseReceived = (DicomCFindRequest rq, DicomCFindResponse rp) => {
	Console.WriteLine("Study UID: {0}", rp.Dataset.GetString(DicomTag.StudyInstanceUID));
};

var client = DicomClientFactory.Create("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");
await client.AddRequestAsync(cfind);
await client.SendAsync();
```

#### C-Move SCU
```csharp
var cmove = new DicomCMoveRequest("DEST-AE", studyInstanceUid);

var client = DicomClientFactory.Create("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");
await client.AddRequestAsync(cmove);
await client.SendAsync(); 
```

#### N-Action SCU
```csharp
// It is better to increase 'associationLingerTimeoutInMs' default is 50 ms, which may not be
// be sufficient
var dicomClient = DicomClientFactory.Create("127.0.0.1", 12345, false, "SCU-AE", "SCP-AE",
DicomClientDefaults.DefaultAssociationRequestTimeoutInMs, DicomClientDefaults.DefaultAssociationReleaseTimeoutInMs,5000);
var txnUid = DicomUIDGenerator.GenerateDerivedFromUUID().UID;
var nActionDicomDataSet = new DicomDataset
{
    { DicomTag.TransactionUID,  txnUid }
};
var dicomRefSopSequence = new DicomSequence(DicomTag.ReferencedSOPSequence);
var seqItem = new DicomDataset()
{
    { DicomTag.ReferencedSOPClassUID, "1.2.840.10008.5.1.4.1.1.1" },
    { DicomTag.ReferencedSOPInstanceUID, "1.3.46.670589.30.2273540226.4.54" }
};
dicomRefSopSequence.Items.Add(seqItem);
nActionDicomDataSet.Add(dicomRefSopSequence);
var nActionRequest = new DicomNActionRequest(DicomUID.StorageCommitmentPushModelSOPClass,
                DicomUID.StorageCommitmentPushModelSOPInstance, 1)
{
    Dataset = nActionDicomDataSet,
    OnResponseReceived = (DicomNActionRequest request, DicomNActionResponse response) => 
    {
        Console.WriteLine("NActionResponseHandler, response status:{0}", response.Status);
    },
};
await dicomClient.AddRequestAsync(nActionRequest);
dicomClient.OnNEventReportRequest = OnNEventReportRequest;
await dicomClient.SendAsync();

private static Task<DicomNEventReportResponse> OnNEventReportRequest(DicomNEventReportRequest request)
{
    var refSopSequence = request.Dataset.GetSequence(DicomTag.ReferencedSOPSequence);
    foreach(var item in refSopSequence.Items)
    {
        Console.WriteLine("SOP Class UID: {0}", item.GetString(DicomTag.ReferencedSOPClassUID));
        Console.WriteLine("SOP Instance UID: {0}", item.GetString(DicomTag.ReferencedSOPInstanceUID));
    }
    return Task.FromResult(new DicomNEventReportResponse(request, DicomStatus.Success));
}
```

#### C-ECHO with advanced DICOM client connection: manual control over TCP connection and DICOM association
```csharp
var cancellationToken = CancellationToken.None;
// Alternatively, inject IDicomServerFactory via dependency injection instead of using this static method
using var server = DicomServerFactory.Create<DicomCEchoProvider>(12345); 

var connectionRequest = new AdvancedDicomClientConnectionRequest
{
    NetworkStreamCreationOptions = new NetworkStreamCreationOptions
    {
        Host = "127.0.0.1",
        Port = server.Port,
    }
};

// Alternatively, inject IAdvancedDicomClientConnectionFactory via dependency injection instead of using this static method
using var connection = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cancellationToken);

var associationRequest = new AdvancedDicomClientAssociationRequest
{
    CallingAE = "EchoSCU",
    CalledAE = "EchoSCP",
    // Optionally negotiate user identity
    UserIdentityNegotiation = new DicomUserIdentityNegotiation
    {
        UserIdentityType = DicomUserIdentityType.UsernameAndPasscode,
        PositiveResponseRequested = true,
        PrimaryField = "USERNAME",
        SecondaryField = "PASSCODE",
    }
};

var cEchoRequest = new DicomCEchoRequest();

using var association = await connection.OpenAssociationAsync(associationRequest, cancellationToken);
try
{
    DicomCEchoResponse cEchoResponse = await association.SendCEchoRequestAsync(cEchoRequest, cancellationToken).ConfigureAwait(false);
    
    Console.WriteLine(cEchoResponse.Status);
}
finally
{
    await association.ReleaseAsync(cancellationToken);
}
```
