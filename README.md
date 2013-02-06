# Fellow Oak DICOM for Windows Store apps

Last updated February 6, 2013.

Copyright (c) 2012-2013 Colby Dillion, adaptations to Windows Store platform (c) 2012-2013 Anders Gustafsson, Cureos AB

This is a fork of Colby Dillion's [fo-dicom](https://github.com/rcd/fo-dicom) toolkit, aimed at Windows Store applications. This repository provides a Windows Store class library project *Store.DICOM* that can be 
applied in any Windows Store application.

To sufficiently use the library in a Windows Store application, consider enabling the following capabilities and declarations in the application manifest file:
* Documents Library capability
* Internet (Client & Server) capability
* Private Networks (Client & Server) capability
* Removable Storage capability
* File Type Associations declaration e.g. for files with extensions *.dcm* and *.dic*

At this stage, the *Store.DICOM* class library exposes the same API as the regular .NET Framework *fo-dicom* class library. *Store.DICOM* currently does not publicly expose 
[Windows Runtime API](http://msdn.microsoft.com/en-us/library/windows/apps/br211377.aspx) asynchronous methods for file and network I/O etc. 

The library has dependencies to native (C/C++) codec classes and therefore has to be built separately for each platform. Supported platforms are *x86*, *x64* and *ARM*.

Compared to the regular *fo-dicom* class library, *Store.DICOM* exhibits the following known limitations:
* Only Secure Sockets Layer (SSL) client is supported, SSL server functionality is *not* enabled.
* Database query transforms are not supported, i.e. it is not possible to update a DICOM dataset based on a database query.

Issues directly related to using *fo-dicom* in Windows Store applications can be reported [here](https://github.com/cureos/fo-dicom/issues). 
General *fo-dicom* issues should be reported to the parent repository [Issues page](https://github.com/rcd/fo-dicom/issues).


# Fellow Oak DICOM for .NET

Please join the [Google group](http://groups.google.com/group/fo-dicom) for updates and support. Binaries are available from [Fellow Oak](http://www.fellowoak.com/binaries/) and [NuGet](http://www.nuget.org/packages/fo-dicom).

### Features
* High-performance, fully asynchronous, .NET 4.0 API
* JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression
* Supports very large datasets with content loading on demand
* Image rendering

### Examples

#### File Operations
```csharp
var file = DicomFile.Open(@"test.dcm");

var patientid = file.Dataset.Get<string>(DicomTag.PatientID);

file.Dataset.Add(DicomTag.PatientsName, "DOE^JOHN");

// creates a new instance of DicomFile
file = file.ChangeTransferSyntax(DicomTransferSyntax.JPEGProcess14SV1);

file.Save(@"output.dcm");
```

#### Render Image to JPEG
```csharp
var image = new DicomImage(@"test.dcm");
image.RenderImage().Save(@"test.jpg");
```

#### C-Store SCU
```csharp
var client = new DicomClient();
client.AddRequest(new DicomCStoreRequest(@"test.dcm"));
client.Send("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
```

#### C-Echo SCP
```csharp
var server = new DicomServer<DicomCEchoProvider>(12345);

var client = new DicomClient();
client.NegotiateAsyncOps();
for (int i = 0; i < 10; i++)
    client.AddRequest(new DicomCEchoRequest());
client.Send("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
```

### Contributors
* [Hesham Desouky](https://github.com/hdesouky) (Nebras Technology)
* [Mahesh Dubey](https://github.com/mdubey82)
* [Anders Gustafsson](https://github.com/cureos) (Cureos AB)
* [Justin Wake](https://github.com/jwake)

### Bug Reports
* [Chris Horn](https://github.com/GMZ)

### License
This library is licensed under the [Microsoft Public License (MS-PL)](http://opensource.org/licenses/MS-PL). See _License.txt_ for more information.