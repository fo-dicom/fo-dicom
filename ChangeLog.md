### 5.1.4 (TBD)

- Add support for saving new strings with multi-valued Specific Character Set (#1789)

### 5.1.3 (2024-06-27)

- Update to DICOM Standard 2024c
- **Breaking change**: Calculation of VOI LUT function LINEAR_EXACT changed as defined since DICOM Standard 2019d
- Add core support for HTJ2K-based transfer syntaxes (not actual codec) (#1687)
- Reduce the memory impact of the DicomDatasetComparer. By a static property DicomDataset.CompareInstancesByContent the usage of DicomDatasetComparer in DicomDataset.Equals can be disabled globally. (#1807)
- Add support for parsing DICOM files where the pixel data is not properly closed with a SequenceDelimitationItem (#1339)
- Update Dicom json converter to handle Infinity values for FL and FD VRs (#1725)
- Fix rendering of files with photometric interpretation YBR_RCT or YBR_ICT (#1734)
- Add support for rendering multiframe DICOM files where last fragment is 0 bytes (#1586)
- Fix rendering multiframe image when rendering several frames in parallel
- Use information from Functional Group Sequences when rendering EnhancedCT, EnhancedMR or BTO objects
- Fix rendering of compressed data where the photometric interpretation changed while decompressing data.
- DicomImage can cache decompressed pixel data, render-LUT or rendered image.
- New properties CacheMode and AutoAplyLUTToAllFrames in DicomImage.
- Fix performance regression in DicomServer (#1776)
- Fix bug where reading parallel from the a stream file returned wrong data (#1653)
- Fix rendering of images with 1 bit stored, where the image does not povide windowing values (#1432)
- Fix issue with applying FallbackEncoding when SpecificCharacterSet tag is missing (#1159)
- Apply FallbackEncoding of DicomServices to the DicomDatasets that are sent through this DicomService (#1642)
- Fix race condition in GenericGrayscalePipeline that could trigger a NullReferenceException (#1759)
- Add resiliency against WindowCenter or WindowWidth containing gibberish (#1756)
- Ignore overlay data that is too small (#1728)
- Allow leading zeros in DS values (#1793)
- Correctly handle gb2312 (ISO 2022 IR 58) character encoding (#1805)

### 5.1.2 (2023-12-21)

- Update to DICOM Standard 2023e
- **Breaking change**: Configuration of `MaxClientsAllowed` must now be done via the `configure` parameter of `IDicomServerFactory.Create(..)` instead of using the `Options` property of a `DicomServer`.
- **Breaking change**: `IServiceCollection.AddDicomServer(Action<DicomServiceOptions> configure)` was changed to `IServiceCollection.AddDicomServer(Action<DicomServerOptions> configure)`
- fo-dicom.Imaging.Desktop supports net6.0-windows and net7.0-windows targets (#1318)
- FO-DICOM.Tests target net6.0-windows and net7.0-windows and test WPF/WinForms images.
- Added private tags mentioned in RayStation 11A DICOM Conformance Statement (#1612)
- Fix issue where extracting a string from a DICOM dataset could return null if the tag was present but empty
- Extension methods `DicomDataset.WriteToLog` and `DicomFile.WriteToLog` now also accept a `Microsoft.Extensions.Logging.ILogger`
- Optimize the common case of adding a single `DicomItem` to a `DicomDataset` by adding an overload `DicomDataset.Add(DicomItem item)` (#1604)
- Immediately throw an exception if DICOM server synchronously fails to start (#1562)
- Fix issue where stopping a DICOM server left the unused services cleanup task running (#1562)
- Add the possibility to configure TCP buffer sizes (#1564)
- Fix incorrect values returned from `DicomEncoding.GetCharset()` (#1624)
- Tolerate `Specific Character Set` values misspelled as "ISO-IR ###" additionally to "ISO IR ###"
- Fix issue where reading a DICOM file with large pixel data (> 2 GB) did not work (#1453)
- Fix issue where a DICOM server could stop accepting incoming connections if MaxClientsAllowed is configured and one or more connections never close (#1468)
- Fix issue where a DICOM server could leak memory when one or more connections never close (#1594)
- Fix the issue of 'DicomAttribute not generated in XML when element is of type DicomFragmentSequence'
- Prevent adding duplicate presentation contexts to an association request (#1596)
- Fix issue with missing known DicomTransferSyntax from static DicomTransferSyntax.Entries dictionary (#1644)
- Improve robustness of DicomService when presented with HTTP requests. Bail early if the PDU type is not recognized (#1678)
- Enhancement: Added IEquatable implementation and equality operators for DicomDataset class
- Fix issue where a DICOM server could stop accepting incoming connections if MaxClientsAllowed is configured and one or more connections take longer than one minute to close (#1670)

#### 5.1.1 (2023-05-29)

- Fix issue where DicomClient did not send requests when Async Ops Invoked was zero (#1597)

#### 5.1.0 (2023-05-21)

- **Breaking change**: Switch to Microsoft.Extensions.Logging, replacing FellowOakDicom.Log.ILogger and FellowOakDicom.Log.ILogManager. These are old interfaces are still supported, but they are now marked as obsolete
- **Breaking change**: Updated DICOM Dictionary to 2023b. Several DicomTag constant names changed to singular name from plural form (#1469)
- Fix Truncating UIDs during Dimse and PDU logging (#1505)
- **Breaking change**: DicomServer factories methods take an instance of ITlsAcceptor instead of a certificate name in case of Tls connection.
- Add the possibility to use some certain client certificate for Tls connections.
- New interfaces ITlsAcceptor and ITlsInitiator give more freedom in handling Tls connections.
- Cache file length in FileByteSource to improve parse speed (#1493)
- Fix reading of DICOM files with extra tags in File Meta Information (#1376)
- Allow accessing person name components for empty items (#1405)
- Fix sending more DICOM requests over an existing association where a request previously timed out (#1396)
- Improve throughput of DicomClient when more requests are added mid-flight (#1396)
- Fix race-condition where Dicom clients could be accepted for connection before the server was fully configured (#1398)
- Fix overwriting of Lossy Compression ratio tag (#1400)
- Fix DicomClientFactory logger name (#1429)
- Fix DicomJsonConverter deserialization to handle invalid private creator item (#1445)
- Improve performance and reduce memory usage when opening DICOM files (#1414)
- Fix rendering of XA/XRF images that include a modality LUT sequence (#1442)
- Fix incorrect conversion of some decimal strings (#1454)
- Disabled dataset validation on `DicomFile.Clone()` (#1465)
- Fix reading of Confidentiality Profile Attributes from standard (led to missing Clean Graphics option) (#1212)
- Added support for DICOM supplement 225, Multi-Fragment video transfer syntax (#1469)
- Added support for rendering native icon image stored within encapsulated sop instance (#1483)
- Added property to omit adding the default Implicit VR Little Endian transfer syntax for CStoreRequest (#1475)
- Fix blanking of ValueElements in the anonymizer (#1491)
- Throw error when adding private dicom tag without explicit VR (#1462)
- Fix incorrect JSON conversion of inline binaries (#1487)
- Update VR=UI validation to reject empty component (#1509)
- Fix GetDateTimeOffset with default offset from date/time (#1511)
- Fix even length in pixel data by adding payload (#1019)
- Use CommunityToolkit.HighPerformance (#1473)
- Fix JsonDicomConverter number serialization mode 'PreferablyAsNumber' to handle integers greater than int.MaxValue or lesser than int.MinValue (#1521)
- Fixed missing logging of RemoteHost and RemoteIP in SCU (#1518)
- Added null check for EscapeXml in DicomXML (#1392)
- Added private tags from Varian official DICOM Conformance Statements (#1556)
- Fix handling of negative overlay origin (#1559)
- Add better logging for inbound connections (#1561)
- Added User Identity Negotiation support (#1110)

#### 5.0.3 (2022-05-23)

- **Breaking change**: subclasses of DicomService will have to pass an instance of DicomServiceDependencies along to the DicomService base constructor. This replaces the old LogManager / NetworkManager / TranscoderManager dependencies. (Implemented in the context of #1291)
- **Breaking change**: subclasses of DicomServer will have to pass an instance of DicomServerDependencies along to the DicomServer base constructor. This replaces the old NetworkManager / LogManager dependencies. (Implemented in the context of #1291)
- **Breaking change**: DicomClient no longer has a NetworkManager, LogManager or TranscoderManager, these are to be configured via dependency injection. (Implemented in the context of #1144)
- Update to DICOM Standard 2022b
- Added option `numberSerializationMode` to `JsonDicomConverter` that allows different modes for serializing DS/IS/UV/SV DICOM items, including handling of invalid values (#1354 & #1362)
- Added an extension to get a DateTimeOffset respecting the timezone info in the dataset (#1310)
- Fixed bug where anonymization threw an exception if a DicomTag of VR UI contained no value (#1308)
- Catch exception in logmessage, to avoid making the application crash because of logging (#1288)
- Fixed StreamByteBuffer to read an internally buffered stream completely (#1313)
- Optimize performance and reduce memory allocations in network layer (#1291)
- Fixed bug where disposal of DicomService could throw an exception if the buffered write stream still had content (#1319)
- Improve handling of dropped connections (#1332)
- Add new API for expert DICOM client connections/associations. This new API gives full control over the connection, association and requests when sending DICOM requests (#1144)
- Rewrite "DicomClient" based on the new advanced API. The state based implementation is gone. The public API has remained the same. (#1144)
- Ignore empty VOI LUT and Modality LUT Sequence (#1369)
- Validate calling AE and called AE length when creating a DicomClient (#1323)
- Improve handling of WSI creation: faster offset table calucation and a naming of temp files to allow more than 64.000.
- Change: DicomAnonymizer private fields and methods changed to protected so they can be used in subclasses, made instance methods virtual so they can be overridden in subclasses
- Fix VR's SV and UV VR Length field (#1386)

#### 5.0.2 (2022-01-11)

- Update to DICOM Standard 2021e
- Fix issue where opening a DICOM file from a stream writes too much data when saving it again (#1264)
- Add possibility to read from streams without `Seek` like `BrowserFileStream` (#1218)
- Add method to convert an array of DicomDatasets into a json string (#1271)
- Improved bilinear interpolation
- Fix issue where sending a deflated DICOM file via C-STORE was sent inflated, causing errors (#1283)
- Optimize performance and reduce memory allocations in network layer (#1267 and #1273)
- Enhance Association Request Timeout (#1284)
- Do not change target encoding for strings that are not encoded (#1301)

#### 5.0.1 (2021-11-11)

- Add generated API documentation for versions 4 and 5
- Fix IO Exception with >2GB images (#1148)
- Bug fix: Correct Source PDU Field in Association Abort Request (#984)
- Bug fix: Correct Person Name VR Json model (#1235)
- Vulnerability fix: Use secure version of `System.Text.Encodings.Web` package (#1223)
- Change: `DicomFile.Open` now throws a `DicomFileException` if the file size is less than 132 bytes (#641)
- Add XML documentation to nuget package
- Change: Trying to add a DICOM element with invalid group ID to DICOM meta information now throws `DicomDataException` (#750)
- Bug fix: Prevent DicomJsonConverter from consuming root end object token (#1251)
- Add missing handling of UV, SV and OV in DicomDatasetReaderObserver.OnElement
- Drastically reduce memory consumption when saving a DICOM file
- Fix rendering of single color image

#### 5.0.0 (2021-09-13)

- Update to DICOM Standard 2021b (#1189)
- New helper classes to build up a volume from a stack and calculate stacks/slices out of this volume in arbitrary orientation
- Optional parameter in DicomFile.Open methods to define the limit of large object size (#958)
- Add initial support for code extensions (#43)
- Add possibility to register additional encodings via `DicomEncoding.RegisterEncoding()`
- Do not validate VM for VRs OF, OL and OV (#1186)
- Add possibility to add values for the VRs UV, SV and OV
- Log warning messages on decoding errors (#1200)
- Bug fix: Anonymizer not parsing items in sequences (#1202)
- Fix anonymization of string values in private tags with VR UN

#### 5.0.0-alpha5 (2021-05-25)

- Add missing properties to IDicomClient interface (#1171)
- Fix unintended breaking change, where StringValue of tags with length 0 returned null, but should return string.empty.
- Be more tolerant to recognize Encoding by ignoring the difference of underscore and space.
- JsonDicom supports some VRs as number as well as as string. (#1161)
- Fix anonymizing private tags with explicit transfer syntax (#1181)
- Internal calculation of pixel values are done as double instead of int to avoid consequential errors when calculating (#1153)
- Fix: DicomDirectory did throw exception on calling constructor with no parameters (#1176)

#### 5.0.0-alpha4 (2021-03-01)

- Bug fix: No DICOM charset found for GB18030 in .NET Core (#1125)
- NLogManager constructor should be public (#1136)
- Update to DICOM Standard 2020e (#1132)
- Use Color32 instead of System.Drawing.Color (#1140)
- FrameGeometry is enhanced so that it also works for DX, CR or MG images. (#1138)
- DicomServerFactory missed the method overload to pass the userState object
- Private Creator UN tags are converted to LO (#1146)
- Bug fix: Ensure timeout detection can never stop prematurely
- Fix parsing of datasets with a final SequenceDelimiterItem at the end. (#1157)

#### 5.0.0-alpha3 and prior (2020-11-01)

##### Changes:

- There is only one library built in NetStandard 2.0 _fo-dicom.core_.\*
- Use `Microsoft.Extensions.DependencyInjection`. There is an extension method to `IServiceCollection.AddDefaultDicomServices()` to add all default implementations for the required interfaces.
  - _IFileReferenceFactory:_ creates a `IFileReference` instance. Is used internally whenever a Reference to a file is created.
  - _IImageManager:_ creates a `IImage` instance. Default is `RawImageManager`, that returns a byte array. call `IServiceCollection.UseImageManager<MyImageManager>()` to register an other implementation.
  - _ITranscoderManager:_ manages the codecs, used by `DicomTranscoder`. Call `IServiceCollection.useTranscoderManager<MyTranscoderManager>()` to register an other implementation.
  - _ILogManager:_ creates a concrete implementation of `ILogger`.
  - _INetworkManager:_ creates a listner, opens streams or checks connection status.
  - _IDicomClientFactory:_ is responsible to return an `IDicomClient` instance. This may be a new instance everytime or a reused instance per host or whateever.
  - _IDicomServerFactory:_ creates server instances, manages available ports, etc.
- If there is no DI container provided, fo-dicom creates its own internally. To configure it, call `new DicomSetupBuilder().RegisterServices(s => ...).Build();`
  There are extension methods for this DicomSetupBuilder like `.SkipValidation()` or `SetDicomServiceLogging(logDataPdus, log DimseDataset)`.
  The new interface `IServiceProviderHost` manages, if there is an internal ServiceProvider or if to use a given Asp.Net Service Provider.
- DicomServer uses DI to create the instances of your `DicomService`. You can use constructor injection there.
- Make server providers asynchronous
- A new class `DicomClientOptions` holds all the options of a DicomClient to be passed instead of the huge list of parameters.
- `DicomServerRegistration` manages the started servers per IP/Port.
- Some little memory consumption emprovements in IByteBuffer classes.
- new methods in `IByteBuffer` to directly manipulate/use the data instead of copying it around multiple times.
- Include Json serialization/deserialization directly into _fo-dicom.core_ based on `System.Text.Json`.
- Text encoding is now handled when a string is written into a network- or file-stream.
- Switch to IAsyncEnumerator using Microsoft.Bcl.AsyncInterfaces. LanguageVersion is set to 8.0.
- internal: SCU now sends two presentation context per CStoreRequest: one with the original TS and one with the additional and the mandatory ImplicitLittleEndian. So the chance is higher to send the file without conversion. (#1048)
- Optimize DicomTag.GetHashCode()
- Bug fix: Prevent special characters in association requests from crashing Fellow Oak DICOM (#1104)
- Make DicomService more memory efficient. Use existing streams in PDU and do not create new Memorystreams for every PDU. (#1091)

##### Breaking changes:

- namespace changed from `Dicom` to `FellowOakDicom`
- `IOManager` is removed. Instead of calling `IOManager.CreateFileReference(path)` now directly create a new instance `new Filereference(path)`. The same is true for `DirecotryReference`.
- In general: all \*Manager classes with their static `.SetImplementation` initializers have been replaced by Dependency Injection.
- By default there is only `RawImageManager` implementation for `IImageManager`. To have `WinFormsImageManager` or `WPFImageManager` you have to reference the package _fo-dicom.Imaging.Desktop_. To use `ImageSharpImageManager` you have to reference _fo-dicom.Imaging.ImageSharp_.
- There are only asynchronous server provider interfaces. All synchronous methods have been replaced by asynchronous.
- Instances of `DicomClient` and `DicomServer` are not created directly, but via a `DicomClientFactory` or a `DicomServerFactory`.
  If you are in a "DI-Environment" like Asp.Net, then inject a `IDicomClientFactory` instance and use this to create a DicomClient. otherwise call `DicomClientFactory.CreateDicomClient(...)`. This is a wrapper around accessing the internal DI container , getting the registered IDicomClientFactory and then calling this. So this is more overhead.
- DicomServiceOptions cannot be passed as parameter to DicomServer constructor/factory any more, but the values of options have to be set to the created instance of DicomServer.
- Classes `DicomFileReader`, `DicomReader`, `DicomReaderCallbackObserver` etc are now internal instead of public, because the resulting Datasets are wrong/inconsistent and need further changes. Therefore its usage is dangerous for users. (#823)
- Removed obsolete methods/classes/properties
  - `DicomValidation.AutoValidation`: Call `DicomSetupBuilder.SkipValidation()` instead.
  - `Dicom.Network.DicomClient`: use `FellowOakDicom.Network.Client.DicomClient` instead.
  - `Dicom.Network.IDicomServiceUser`: use `IDicomClientConnection` instead.
  - `ChangeTransferSyntax(..)` extension methods for `DicomFile` and `DicomDataset`: use the method `Clone(..)` instead.
  - `DicomDataset.Get<T>`: use `GetValue`, `GetValues`, `GetSingleValue` or `GetSequence` instead.
  - `DicomDataset.AddOrUpdatePixelData`: use `DicomPixelData.AddFrame(IByteBuffer)` to add pixel data to underlying dataset.
  - `DicomUID.Generate(name)`: use `DicomUID.Generate()` instead.
  - `DicomUID.IsValid(uid)`: use `DicomUID.IsValidUid(uid)` instead.
  - `DicomUIDGenerator.Generate()` and `DicomUIDGenerator.GenerateNew()`: use `DicomUIDGenerator.GenerateDerivedFromUUID()`
  - `DicomImage.Dataset`, `DicomImage.PixelData` and `DicomImage.PhotometricInterpretation`: do not load the DicomImage directly from filename if you also need access to the dataset, but load the DicomDataset from file first and then construct the DicomImage from this loaded DicomDataset. Then you can access both.
- DicomStringElement and derived classes do not have the "encoding" parameter in constructor, that takes a string-value
- DicomDataset.Add(OrUpdate) does not take an "encoding" parameter any more, instead the DicomDataset has a property `TextEncodings`, that is applied to all string-based tags.
- in update to DICOM2020e the keywords, that are provided by Nema, are used. therefore some DicomUID-Names changed.
