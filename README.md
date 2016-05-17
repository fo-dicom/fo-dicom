<img src="https://lh3.googleusercontent.com/-Fq3nigRUo7U/VfaIPuJMjfI/AAAAAAAAALo/7oaLrrTBhnw/s1600/Fellow%2BOak%2BSquare%2BTransp.png" alt="fo-dicom logo" height="80" />

# Fellow Oak DICOM

[//]: # ( [![NuGet Pre Release](https://img.shields.io/nuget/vpre/fo-dicom.svg)](https://www.nuget.org/packages/fo-dicom/) )
[![NuGet](https://img.shields.io/nuget/v/fo-dicom.svg)](https://www.nuget.org/packages/fo-dicom/)
[![Build status](https://ci.appveyor.com/api/projects/status/r3yptmhufh3dl1xc?svg=true)](https://ci.appveyor.com/project/anders9ustafsson/fo-dicom)
[![Stories in Ready](https://badge.waffle.io/fo-dicom/fo-dicom.svg?label=ready&title=Ready)](http://waffle.io/fo-dicom/fo-dicom)
[![Join the chat at https://gitter.im/fo-dicom/fo-dicom](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/fo-dicom/fo-dicom?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

### Features
* Core functionality in Portable Class Library (PCL)
* Targets .NET 4.5.2 and higher, Universal Windows Platform, Xamarin iOS, Xamarin Android, Mono and Unity
* DICOM dictionary version 2016b
* High-performance, fully asynchronous `async`/`await` API
* JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression (limited on Xamarin, Mono and Unity platforms)
* Supports very large datasets with content loading on demand
* Image rendering
* JSON support

### Installation
Easiest is to obtain *fo-dicom* binaries from [NuGet](https://www.nuget.org/packages/fo-dicom/). This package contains all assemblies required to consume *fo-dicom* in a .NET application.

Starting with *fo-dicom* version 2.0, there will also be separate *NuGet* packages available for [Dicom.Core](https://www.nuget.org/packages/fo-dicom.Core/), [Dicom.Legacy](https://www.nuget.org/packages/fo-dicom.Legacy/) and
[Dicom.Platform](https://www.nuget.org/packages/fo-dicom.Platform/). *Dicom.Core* is the PCL library with core functionality, *Dicom.Legacy* is a PCL library with obsolete asynchronous API methods, and *Dicom.Platform* contains
the support libraries required to run *fo-dicom* on specific target platforms. As of now, .NET 4.5 and higher, *Universal Windows Platform*, *Xamarin iOS* and *Xamarin Android* are the available platforms in the *Dicom.Platform* *NuGet* package.
Starting with *fo-dicom* version 2.1.0, there are also separate platform packages for *Windows Desktop*, *Universal Windows Platform*, Xamarin iOS* and *Xamarin Android*.

*fo-dicom* can use a wide variety of logging frameworks. These connectors come in separate *NuGet* packages for [NLog](https://www.nuget.org/packages/fo-dicom.NLog/), [Serilog](https://www.nuget.org/packages/fo-dicom.Serilog/),
[log4net](https://www.nuget.org/packages/fo-dicom.log4net/) and [MetroLog](https://www.nuget.org/packages/fo-dicom.MetroLog/). The *MetroLog* connector is a Portable Class Library, whereas the other logging connectors are .NET dedicated libraries.

### v2.0 Breaking Changes
Out-of-the-box, *fo-dicom* for .NET defaults to *Windows Forms*-style image rendering. To switch to WPF-style image rendering, call:

    ImageManager.SetImplementation(WPFImageManager.Instance);

By default, logging defaults to the no-op `NullLogerManager`. On .NET, several log managers are available and can be enabled like this:

    LogManager.SetImplementation(ConsoleLogManager.Instance);  // or ...
    LogManager.SetImplementation(NLogManager.Instance);        // or ...

On *Universal Windows Platform*, *Xamarin iOS*, *Xamarin Android* and *Mono* there is only one operational log manager available, namely `MetroLogManager.Instance`.

### Examples

#### File Operations
```csharp
var file = DicomFile.Open(@"test.dcm");             // Alt 1
var file = await DicomFile.OpenAsync(@"test.dcm");  // Alt 2

var patientid = file.Dataset.Get<string>(DicomTag.PatientID);

file.Dataset.Add(DicomTag.PatientsName, "DOE^JOHN");

// creates a new instance of DicomFile
file = file.ChangeTransferSyntax(DicomTransferSyntax.JPEGProcess14SV1);

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
var client = new DicomClient();
client.AddRequest(new DicomCStoreRequest(@"test.dcm"));
client.Send("127.0.0.1", 12345, false, "SCU", "ANY-SCP");             // Alt 1
await client.SendAsync("127.0.0.1", 12345, false, "SCU", "ANY-SCP");  // Alt 2
```

#### C-Echo SCU/SCP
```csharp
var server = new DicomServer<DicomCEchoProvider>(12345);

var client = new DicomClient();
client.NegotiateAsyncOps();
for (int i = 0; i < 10; i++)
    client.AddRequest(new DicomCEchoRequest());
client.Send("127.0.0.1", 12345, false, "SCU", "ANY-SCP");             // Alt 1
await client.SendAsync("127.0.0.1", 12345, false, "SCU", "ANY-SCP");  // Alt 2
```

#### C-Find SCU
```csharp
var cfind = DicomCFindRequest.CreateStudyQuery(patientId: "12345");
cfind.OnResponseReceived = (DicomCFindRequest rq, DicomCFindResponse rp) => {
	Console.WriteLine("Study UID: {0}", rp.Dataset.Get<string>(DicomTag.StudyInstanceUID));
};

var client = new DicomClient();
client.AddRequest(cfind);
client.Send("127.0.0.1", 104, false, "SCU-AE", "SCP-AE");             // Alt 1
await client.SendAsync("127.0.0.1", 104, false, "SCU-AE", "SCP-AE");  // Alt 2
```

#### C-Move SCU
```csharp
var cmove = new DicomCMoveRequest("DEST-AE", studyInstanceUid);

var client = new DicomClient();
client.AddRequest(cmove);
client.Send("127.0.0.1", 104, false, "SCU-AE", "SCP-AE");             // Alt 1
await client.SendAsync("127.0.0.1", 104, false, "SCU-AE", "SCP-AE");  // Alt 2
```

### Sample applications
There are a number of simple sample applications that use *fo-dicom* available in separate repository [here](https://github.com/fo-dicom/fo-dicom-samples). These also include the samples
that were previously included in the *Examples* sub-folder of the VS solutions.

### Contributors
* [Anders Gustafsson](https://github.com/anders9ustafsson), Cureos AB
* [Colby Dillion](https://github.com/rcd)
* [Ian Yates](http://github.com/IanYates)
* [Hesham Desouky](https://github.com/hdesouky), Nebras Technology
* [Chris Horn](https://github.com/GMZ)
* [Mahesh Dubey](https://github.com/mdubey82)
* [Alexander Saratow](https://github.com/swalex)
* [Justin Wake](https://github.com/jwake)
* [Ryan Melena](https://github.com/RyanMelenaNoesis)
* [Rickard Holmberg](https://github.com/rickardraysearch)
* [Chris Hafey](https://github.com/chafey)
* [Michael Pavlovsky](https://github.com/michaelp)
* [Johannes Liegert](https://github.com/0xLigety)
* [Håkan MacLean](https://github.com/MacL3an)
* [captainstark](https://github.com/captainstark)
* [lste](https://github.com/lste)
* [Thunderstriker](https://github.com/Thunderstriker)
* [Ed55](https://github.com/Ed55)
* [zcr01](https://github.com/zcr01)

### License
This library is licensed under the [Microsoft Public License (MS-PL)](http://opensource.org/licenses/MS-PL). See [License.txt](License.txt) for more information.
