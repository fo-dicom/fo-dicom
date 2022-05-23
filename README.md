<img src="https://lh3.googleusercontent.com/-Fq3nigRUo7U/VfaIPuJMjfI/AAAAAAAAALo/7oaLrrTBhnw/s1600/Fellow%2BOak%2BSquare%2BTransp.png" alt="fo-dicom logo" height="80" />

# Fellow Oak DICOM

[![NuGet](https://img.shields.io/nuget/v/fo-dicom.svg)](https://www.nuget.org/packages/fo-dicom/)
![build development](https://github.com/fo-dicom/fo-dicom/workflows/build/badge.svg?branch=development)
[![codecov](https://codecov.io/gh/fo-dicom/fo-dicom/branch/development/graph/badge.svg)](https://codecov.io/gh/fo-dicom/fo-dicom)
[![Join the chat at https://gitter.im/fo-dicom/fo-dicom](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/fo-dicom/fo-dicom?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

### License
This library is licensed under the [Microsoft Public License (MS-PL)](http://opensource.org/licenses/MS-PL). See [License.txt](License.txt) for more information.

### Features
* Targets .NET Standard 2.0
* DICOM dictionary version 2022b
* High-performance, fully asynchronous `async`/`await` API
* JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression (via additional package)
* Supports very large datasets with content loading on demand
* Image rendering to System.Drawing.Bitmap or SixLabors.ImageSharp
* JSON and XML export/import
* Anonymization
* DICOM services
* Customize components via DI container 

### Installation
Easiest is to obtain *fo-dicom* binaries from [NuGet](https://www.nuget.org/packages/fo-dicom/). This package reference the core *fo-dicom* assemblies for all Microsoft and Xamarin platforms.

### NuGet Packages
*Valid for version 5.0.0 and later*

Package | Description
------- | -----------
[fo-dicom](https://www.nuget.org/packages/fo-dicom/) | Core package containing parser, services and tools.
[fo-dicom.Imaging.Desktop](https://www.nuget.org/packages/fo-dicom.Imaging.Desktop/) | Library with referencte to System.Drawing, required for rendering into Bitmaps
[fo-dicom.Imaging.ImageSharp](https://www.nuget.org/packages/fo-dicom.Desktop/) | Library with reference to ImageSharp, can be used for platform independent rendering
[fo-dicom.NLog](https://www.nuget.org/packages/fo-dicom.NLog/) | .NET connector to enable *fo-dicom* logging with NLog
[fo-dicom.Codecs](https://www.nuget.org/packages/fo-dicom.Codecs/) | Cross-platform Dicom codecs for fo-dicom, developed by Efferent Health (https://github.com/Efferent-Health/fo-dicom.Codecs)


### Documentation
Documentation, including API documentation, is available via GitHub pages:
- documentation for the latest release for [fo-dicom 4](https://fo-dicom.github.io/stable/v4/index.html) and
  [fo-dicom 5](https://fo-dicom.github.io/stable/v5/index.html)
- documentation for the development version for [fo-dicom 4](https://fo-dicom.github.io/dev/v4/index.html) and
  [fo-dicom 5](https://fo-dicom.github.io/dev/v5/index.html)


### Usage Notes

#### Image rendering configuration
Out-of-the-box, *fo-dicom* defaults to an internal class *FellowOakDicom.Imaging.IImage*-style image rendering. To switch to Desktop-style or ImageSharp-style image rendering, you first have to add the nuget package you desire and then call:

    new DicomSetupBuilder()
        .RegisterServices(s => s.AddFellowOakDicom().AddImageManager<WinFormsImageManager>())
	.Build();
	
or

    new DicomSetupBuilder()
        .RegisterServices(s => s.AddFellowOakDicom().AddImageManager<ImageSharpImageManager>())
	.Build();

Then when rendering you can cast the IImage to the type by

    var image = new DicomImage("filename.dcm");
    var bitmap = image.RenderImage().As<Bitmap>();

or

    var image = new DicomImage("filename.dcm");
    var shartimage = image.RenderImage().AsSharpImage();

#### Logging configuration
By default, logging defaults to the no-op `NullLogerManager`. There are several logmanagers configurable within `DicomSetupBuilder` like

    s.AddLogManager<ConsoleLogManager>()  // or ...
    s.AddLogManager<NLogManager>()   // or ...
    

    LogManager.SetImplementation(ConsoleLogManager.Instance);  // or ...
    LogManager.SetImplementation(NLogManager.Instance);        // or ...

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
var server = new DicomServer<DicomCEchoProvider>(12345);

var client = DicomClientFactory.Create("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
client.NegotiateAsyncOps();
for (int i = 0; i < 10; i++)
    await client.AddRequestAsync(new DicomCEchoRequest());
await client.SendAsync();
```

#### C-Find SCU
```csharp
var cfind = DicomCFindRequest.CreateStudyQuery(patientId: "12345");
cfind.OnResponseReceived = (DicomCFindRequest rq, DicomCFindResponse rp) => {
	Console.WriteLine("Study UID: {0}", rp.Dataset.Get<string>(DicomTag.StudyInstanceUID));
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
    CalledAE = "EchoSCP"
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

### New to DICOM?

If you are new to DICOM, then take a look at the DICOM tutorial of Saravanan Subramanian:
https://saravanansubramanian.com/dicomtutorials/
The author is also using fo-dicom in some code samples.
