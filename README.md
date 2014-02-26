# Fellow Oak DICOM for .NET

Please join the [Google group](http://groups.google.com/group/fo-dicom) for updates and support. Binaries are available from [GitHub](https://github.com/rcd/fo-dicom/releases) and [NuGet](http://www.nuget.org/packages/fo-dicom).

### Features
* High-performance, fully asynchronous, .NET 4.0 API
* JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression
* Supports very large datasets with content loading on demand
* Image rendering

### Notes
* Support for compressed images requires the Visual Studio 2010 SP1 Redistributable Package to be installed. ([x86](http://www.microsoft.com/en-us/download/details.aspx?id=8328) or [x64](http://www.microsoft.com/en-us/download/details.aspx?id=14632)) 

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

#### C-Echo SCU/SCP
```csharp
var server = new DicomServer<DicomCEchoProvider>(12345);

var client = new DicomClient();
client.NegotiateAsyncOps();
for (int i = 0; i < 10; i++)
    client.AddRequest(new DicomCEchoRequest());
client.Send("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
```

#### C-Find SCU
```csharp
var cfind = DicomCFindRequest.CreateStudyQuery(patientId: "12345");
cfind.OnResponseReceived = (DicomCFindRequest rq, DicomCFindResponse rp) => {
	Console.WriteLine("Study UID: {0}", rp.Dataset.Get<string>(DicomTag.StudyInstanceUID));
};

var client = new DicomClient();
client.AddRequest(cfind);
client.Send("127.0.0.1", 104, false, "SCU-AE", "SCP-AE");
```

#### C-Move SCU
```csharp
var cmove = new DicomCMoveRequest("DEST-AE", studyInstanceUid);

var client = new DicomClient();
client.AddRequest(cmove);
client.Send("127.0.0.1", 104, false, "SCU-AE", "SCP-AE");
```

### Contributors
* [Hesham Desouky](https://github.com/hdesouky) (Nebras Technology)
* [Mahesh Dubey](https://github.com/mdubey82)
* [Anders Gustafsson](https://github.com/cureos) (Cureos AB)
* [Justin Wake](https://github.com/jwake)
* [Chris Horn](https://github.com/GMZ)
* [captainstark](https://github.com/captainstark)

### License
This library is licensed under the [Microsoft Public License (MS-PL)](http://opensource.org/licenses/MS-PL). See _License.txt_ for more information.