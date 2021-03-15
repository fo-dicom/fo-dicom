<img src="https://lh3.googleusercontent.com/-Fq3nigRUo7U/VfaIPuJMjfI/AAAAAAAAALo/7oaLrrTBhnw/s1600/Fellow%2BOak%2BSquare%2BTransp.png" alt="fo-dicom logo" height="80" />

# Fellow Oak DICOM

[![NuGet](https://img.shields.io/nuget/v/fo-dicom.svg)](https://www.nuget.org/packages/fo-dicom/)
![build development](https://github.com/fo-dicom/fo-dicom/workflows/build/badge.svg?branch=development)
[![Join the chat at https://gitter.im/fo-dicom/fo-dicom](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/fo-dicom/fo-dicom?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

### License
This library is licensed under the [Microsoft Public License (MS-PL)](http://opensource.org/licenses/MS-PL). See [License.txt](License.txt) for more information.

### Features
* Portable Class Library (PCL)
* Targets .NET 4.5 and higher, .NET Core (.NET Standard 1.3 and higher), Universal Windows Platform, Xamarin iOS, Xamarin Android, Mono and Unity
* DICOM dictionary version 2018b
* High-performance, fully asynchronous `async`/`await` API
* JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression (limited on .NET Core, Xamarin, Mono and Unity platforms)
* Supports very large datasets with content loading on demand
* Platform-specific image rendering
* JSON support
* XML export
* Anonymization

### Installation
Easiest is to obtain *fo-dicom* binaries from [NuGet](https://www.nuget.org/packages/fo-dicom/). This package reference the core *fo-dicom* assemblies for all Microsoft and Xamarin platforms.

### NuGet Packages
*Valid for version 3.1.0 (incl. pre-releases) and later*

Package | Description
------- | -----------
[fo-dicom](https://www.nuget.org/packages/fo-dicom/) | Dependencies package including core libraries for Microsoft and Xamarin platforms
[fo-dicom.Portable](https://www.nuget.org/packages/fo-dicom.Portable/) | Core library for PCL Profile 111
[fo-dicom.Desktop](https://www.nuget.org/packages/fo-dicom.Desktop/) | Core library and native codec libraries for .NET 4.5.2 and higher
[fo-dicom.NetCore](https://www.nuget.org/packages/fo-dicom.NetCore/) | Core library for .NET Core applications, Level 1.3 and higher
[fo-dicom.Universal](https://www.nuget.org/packages/fo-dicom.Universal/) | Core library and native codec libraries for Universal Windows Platform
[fo-dicom.Android](https://www.nuget.org/packages/fo-dicom.Android/) | Core library for Xamarin Android
[fo-dicom.iOS](https://www.nuget.org/packages/fo-dicom.iOS/) | Core library for Xamarin iOS (Unified)
[fo-dicom.Mono](https://www.nuget.org/packages/fo-dicom.Mono/) | Core library for Mono 4.5 and higher
[fo-dicom.log4net](https://www.nuget.org/packages/fo-dicom.log4net/) | .NET connector to enable *fo-dicom* logging with log4net
[fo-dicom.MetroLog](https://www.nuget.org/packages/fo-dicom.MetroLog/) | PCL Profile 111 connector to enable *fo-dicom* logging with MetroLog
[fo-dicom.NLog](https://www.nuget.org/packages/fo-dicom.NLog/) | .NET connector to enable *fo-dicom* logging with NLog
[fo-dicom.Serilog](https://www.nuget.org/packages/fo-dicom.Serilog/) | .NET connector to enable *fo-dicom* logging with Serilog
[fo-dicom.Json](https://www.nuget.org/packages/fo-dicom.Json/) | PCL profile 111 library for JSON I/O support
[fo-dicom.Drawing](https://www.nuget.org/packages/fo-dicom.Drawing/) | .NET Core library providing *System.Drawing* based image rendering and printing
[fo-dicom.ImageSharp](https://www.nuget.org/packages/fo-dicom.ImageSharp/) | .NET Standard library providing *SixLabors.ImageSharp* based image rendering

### API Documentation
The API documentation for the core library (represented by *fo-dicom.Desktop*) and the *log4net*, *NLog* and *Serilog* connectors is available [here](https://fo-dicom.github.io/).

### Usage Notes

#### Image rendering configuration
Out-of-the-box, *fo-dicom* for .NET defaults to *Windows Forms*-style image rendering. To switch to WPF-style image rendering, call:

    ImageManager.SetImplementation(WPFImageManager.Instance);

#### Logging configuration
By default, logging defaults to the no-op `NullLogerManager`. On .NET, several log managers are available and can be enabled like this:

    LogManager.SetImplementation(ConsoleLogManager.Instance);  // or ...
    LogManager.SetImplementation(NLogManager.Instance);        // or ...

On *Universal Windows Platform*, *Xamarin iOS*, *Xamarin Android* and *Mono* there is only one operational log manager available, namely `MetroLogManager.Instance`.

#### Cross-platform development

To facilitate cross-platform development, the core library is strong name signed and denoted *Dicom.Core.dll* on all platforms. From an assembly reference point-of-view this convention makes the core assemblies mutually replaceable. It is thus possible to develop a Portable Class Library that depends on the PCL *Dicom.Core* assembly, and when the developed Portable Class Library is used in a platform-specific application, the PCL *Dicom.Core* assembly can be replaced with the platform-specific *Dicom.Core* assembly without needing to re-build anything. *fo-dicom.Json* and *fo-dicom.MetroLog* are examples of portable class libraries that depend on the PCL *Dicom.Core.dll*.

The assembly naming convention is often referred to as the [bait-and-switch trick](http://log.paulbetts.org/the-bait-and-switch-pcl-trick/). The *fo-dicom* package supports the *bait-and-switch trick* by automatically selecting the best suited *Dicom.Core* assembly depending on the targeted platform of the development project upon download from NuGet.

### Sample applications
There are a number of simple sample applications that use *fo-dicom* available in separate repository [here](https://github.com/fo-dicom/fo-dicom-samples). These also include the samples
that were previously included in the *Examples* sub-folder of the VS solutions.

### Examples

#### File Operations
```csharp
var file = DicomFile.Open(@"test.dcm");             // Alt 1
var file = await DicomFile.OpenAsync(@"test.dcm");  // Alt 2

var patientid = file.Dataset.Get<string>(DicomTag.PatientID);

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
image.RenderImage().AsUIImage().AsJPEG().Save(@"test.jpg", true);     // iOS

```

#### C-Store SCU
```csharp
var client = new DicomClient("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
await client.AddRequestAsync(new DicomCStoreRequest(@"test.dcm"));
await client.SendAsync();
```

#### C-Echo SCU/SCP
```csharp
var server = new DicomServer<DicomCEchoProvider>(12345);

var client = new DicomClient("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
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

var client = new DicomClient("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");
await client.AddRequestAsync(cfind);
await client.SendAsync();
```

#### C-Move SCU
```csharp
var cmove = new DicomCMoveRequest("DEST-AE", studyInstanceUid);

var client = new DicomClient("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");
await client.AddRequestAsync(cmove);
await client.SendAsync(); 
```

#### N-Action SCU
```csharp
// It is better to increase 'associationLingerTimeoutInMs' default is 50 ms, which may not be
// be sufficient
var dicomClient = new Dicom.Network.Client.DicomClient("127.0.0.1", 12345, false, "SCU-AE", "SCP-AE",
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

### New to DICOM?

If you are new to DICOM, then take a look at the DICOM tutorial of Saravanan Subramanian:
https://saravanansubramanian.com/dicomtutorials/
The author is also using fo-dicom in some code samples.
