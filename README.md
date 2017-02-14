<img src="https://lh3.googleusercontent.com/-Fq3nigRUo7U/VfaIPuJMjfI/AAAAAAAAALo/7oaLrrTBhnw/s1600/Fellow%2BOak%2BSquare%2BTransp.png" alt="fo-dicom logo" height="80" />

# Fellow Oak DICOM

[//]: # ( [![NuGet Pre Release](https://img.shields.io/nuget/vpre/fo-dicom.svg)](https://www.nuget.org/packages/fo-dicom/) )
[![NuGet](https://img.shields.io/nuget/v/fo-dicom.svg)](https://www.nuget.org/packages/fo-dicom/)
[![Build status](https://ci.appveyor.com/api/projects/status/r3yptmhufh3dl1xc?svg=true)](https://ci.appveyor.com/project/anders9ustafsson/fo-dicom)
[![Stories in Ready](https://badge.waffle.io/fo-dicom/fo-dicom.svg?label=ready&title=Ready)](http://waffle.io/fo-dicom/fo-dicom)
[![Join the chat at https://gitter.im/fo-dicom/fo-dicom](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/fo-dicom/fo-dicom?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

### License
This library is licensed under the [Microsoft Public License (MS-PL)](http://opensource.org/licenses/MS-PL). See [License.txt](License.txt) for more information.

### Features
* Portable Class Library (PCL)
* Targets .NET 4.5.2 and higher, .NET Core (.NET Standard 1.3 and higher), Universal Windows Platform, Xamarin iOS, Xamarin Android, Mono and Unity
* DICOM dictionary version 2016e
* High-performance, fully asynchronous `async`/`await` API
* JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression (limited on .NET Core, Xamarin, Mono and Unity platforms)
* Supports very large datasets with content loading on demand
* Platform-specific image rendering
* JSON support
* XML export (preview)
* Anonymization (preview)

### Installation
Easiest is to obtain *fo-dicom* binaries from [NuGet](https://www.nuget.org/packages/fo-dicom/). This package reference the core *fo-dicom* assemblies for all Microsoft and Xamarin platforms.

### NuGet Packages
*Valid for version 3.0.0 (incl. pre-releases) and later*

Package | Description
------- | -----------
[fo-dicom](https://www.nuget.org/packages/fo-dicom/) | Dependencies package including core libraries for Microsoft and Xamarin platforms
[fo-dicom.Portable](https://www.nuget.org/packages/fo-dicom.Portable/) | Core library for PCL Profile 111
[fo-dicom.Desktop](https://www.nuget.org/packages/fo-dicom.Desktop/) | Core library and native codec libraries for .NET 4.5.2 and higher
[fo-dicom.NetCore](https://www.nuget.org/packages/fo-dicom.NetCore/) | Core library for .NET Core applications, Level 1.3 and higher
[fo-dicom.Universal](https://www.nuget.org/packages/fo-dicom.Universal/) | Core library and native codec libraries for Universal Windows Platform
[fo-dicom.Android](https://www.nuget.org/packages/fo-dicom.Android/) | Core library for Xamarin Android
[fo-dicom.iOS](https://www.nuget.org/packages/fo-dicom.iOS/) | Core library for Xamarin iOS (Unified)
[fo-dicom.log4net](https://www.nuget.org/packages/fo-dicom.log4net/) | .NET connector to enable *fo-dicom* logging with log4net
[fo-dicom.MetroLog](https://www.nuget.org/packages/fo-dicom.MetroLog/) | PCL Profile 111 connector to enable *fo-dicom* logging with MetroLog
[fo-dicom.NLog](https://www.nuget.org/packages/fo-dicom.NLog/) | .NET connector to enable *fo-dicom* logging with NLog
[fo-dicom.Serilog](https://www.nuget.org/packages/fo-dicom.Serilog/) | .NET connector to enable *fo-dicom* logging with Serilog
[fo-dicom.Json](https://www.nuget.org/packages/fo-dicom.Json/) | PCL profile 111 library for JSON I/O support
[fo-dicom.Legacy](https://www.nuget.org/packages/fo-dicom.Legacy/) | PCL profile 111 library with obsolete asynchronous methods (to be deprecated)

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

#### Important notice for Universal Windows Platform applications
The *fo-dicom* class library is not fully Windows Runtime compatible, and therefore cannot be built with the .NET Native tool chain enabled in *Universal Windows Platform* applications.  
Typically, the *Compile with .NET Native tool chain* checkbox is checked in Release mode for UWP applications. To enable building the application in Release mode, uncheck this checkbox.

### Sample applications
There are a number of simple sample applications that use *fo-dicom* available in separate repository [here](https://github.com/fo-dicom/fo-dicom-samples). These also include the samples
that were previously included in the *Examples* sub-folder of the VS solutions.

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
client.Send("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");             // Alt 1
await client.SendAsync("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");  // Alt 2
```

#### C-Move SCU
```csharp
var cmove = new DicomCMoveRequest("DEST-AE", studyInstanceUid);

var client = new DicomClient();
client.AddRequest(cmove);
client.Send("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");             // Alt 1
await client.SendAsync("127.0.0.1", 11112, false, "SCU-AE", "SCP-AE");  // Alt 2
```

### Contributors
* [Anders Gustafsson](https://github.com/anders9ustafsson), Cureos AB
* [Colby Dillion](https://github.com/rcd)
* [Ian Yates](http://github.com/IanYates)
* [Hesham Desouky](https://github.com/hdesouky), Nebras Technology
* [Chris Horn](https://github.com/GMZ)
* [Mahesh Dubey](https://github.com/mdubey82)
* [Rickard Holmberg](https://github.com/rickardraysearch)
* [Reinhard Gruber](https://github.com/gofal)
* [Jaime Olivares](https://github.com/jaime-olivares)
* [Zaid Safadi](https://github.com/Zaid-Safadi)
* [Alexander Saratow](https://github.com/swalex)
* [Håkan MacLean](https://github.com/MacL3an)
* [Sam Gorman](https://github.com/HSGorman)
* [Justin Wake](https://github.com/jwake)
* [Ryan Melena](https://github.com/RyanMelenaNoesis)
* [Alexander Pyzynia](https://github.com/werwolfby)
* [Chris Hafey](https://github.com/chafey)
* [Johannes Liegert](https://github.com/0xLigety)
* [Aerik Sylvan](https://github.com/aerik)
* [Michael Pavlovsky](https://github.com/michaelp)
* [lste](https://github.com/lste)
* [captainstark](https://github.com/captainstark)
* [do0om](https://github.com/do0om)
* [Thunderstriker](https://github.com/Thunderstriker)
* [Ed55](https://github.com/Ed55)
* [zcr01](https://github.com/zcr01)

