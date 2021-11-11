#### 5.0.2 (TBD)


#### 5.0.1 (2021-11-11)

* Add generated API documentation for versions 4 and 5
* Fix IO Exception with >2GB images (#1148)
* Bug fix: Correct Source PDU Field in Association Abort Request (#984)
* Bug fix: Correct Person Name VR Json model (#1235)
* Vulnerability fix: Use secure version of `System.Text.Encodings.Web` package (#1223) 
* Change: `DicomFile.Open` now throws a `DicomFileException` if the file size is less than 132 bytes (#641)
* Add XML documentation to nuget package
* Change: Trying to add a DICOM element with invalid group ID to DICOM meta information now throws `DicomDataException` (#750)
* Bug fix: Prevent DicomJsonConverter from consuming root end object token (#1251)
* Add missing handling of UV, SV and OV in DicomDatasetReaderObserver.OnElement
* Drastically reduce memory consumption when saving a DICOM file
* Fix rendering of single color image

#### 5.0.0 (2021-09-13)

* Update to DICOM Standard 2021b (#1189)
* New helper classes to build up a volume from a stack and calculate stacks/slices out of this volume in arbitrary orientation
* Optional parameter in DicomFile.Open methods to define the limit of large object size (#958)
* Add initial support for code extensions (#43)
* Add possibility to register additional encodings via `DicomEncoding.RegisterEncoding()` 
* Do not validate VM for VRs OF, OL and OV (#1186)
* Add possibility to add values for the VRs UV, SV and OV
* Log warning messages on decoding errors (#1200)
* Bug fix: Anonymizer not parsing items in sequences (#1202)
* Fix anonymization of string values in private tags with VR UN

#### 5.0.0-alpha5 (2021-05-25)

* Add missing properties to IDicomClient interface (#1171)
* Fix unintended breaking change, where StringValue of tags with length 0 returned null, but should return string.empty.
* Be more tolerant to recognize Encoding by ignoring the difference of underscore and space.
* JsonDicom supports some VRs as number as well as as string. (#1161)
* Fix anonymizing private tags with explicit transfer syntax (#1181)
* Internal calculation of pixel values are done as double instead of int to avoid consequential errors when calculating (#1153)
* Fix: DicomDirectory did throw exception on calling constructor with no parameters (#1176)

#### 5.0.0-alpha4 (2021-03-01)

* Bug fix: No DICOM charset found for GB18030 in .NET Core (#1125)
* NLogManager constructor should be public (#1136)
* Update to DICOM Standard 2020e (#1132)
* Use Color32 instead of System.Drawing.Color (#1140)
* FrameGeometry is enhanced so that it also works for DX, CR or MG images. (#1138)
* DicomServerFactory missed the method overload to pass the userState object
* Private Creator UN tags are converted to LO (#1146)
* Bug fix: Ensure timeout detection can never stop prematurely
* Fix parsing of datasets with a final SequenceDelimiterItem at the end. (#1157)


#### 5.0.0-alpha3 and prior (2020-11-01)

##### Changes:

* There is only one library built in NetStandard 2.0 *fo-dicom.core*.*
* Use `Microsoft.Extensions.DependencyInjection`. There is an extension method to `IServiceCollection.AddDefaultDicomServices()` to add all default implementations for the required interfaces.
  * *IFileReferenceFactory:* creates a `IFileReference` instance. Is used internally whenever a Reference to a file is created.
  * *IImageManager:* creates a `IImage` instance. Default is `RawImageManager`, that returns a byte array. call  `IServiceCollection.UseImageManager<MyImageManager>()` to register an other implementation.
  * *ITranscoderManager:* manages the codecs, used by `DicomTranscoder`. Call `IServiceCollection.useTranscoderManager<MyTranscoderManager>()` to register an other implementation.
  * *ILogManager:* creates a concrete implementation of `ILogger`.
  * *INetworkManager:* creates a listner, opens streams or checks connection status.
  * *IDicomClientFactory:* is responsible to return an `IDicomClient` instance. This may be a new instance everytime or a reused instance per host or whateever.
  * *IDicomServerFactory:* creates server instances, manages available ports, etc.
* If there is no DI container provided, fo-dicom creates its own internally. To configure it, call `new DicomSetupBuilder().RegisterServices(s => ...).Build();`
  There are extension methods for this DicomSetupBuilder like `.SkipValidation()` or `SetDicomServiceLogging(logDataPdus, log DimseDataset)`.
  The new interface `IServiceProviderHost` manages, if there is an internal ServiceProvider or if to use a given Asp.Net Service Provider.
* DicomServer uses DI to create the instances of your `DicomService`. You can use constructor injection there.
* Make server providers asynchronous
* A new class `DicomClientOptions` holds all the options of a DicomClient to be passed instead of the huge list of parameters.
* `DicomServerRegistration` manages the started servers per IP/Port.
* Some little memory consumption emprovements in IByteBuffer classes.
* new methods in `IByteBuffer` to directly manipulate/use the data instead of copying it around multiple times.
* Include Json serialization/deserialization directly into *fo-dicom.core* based on `System.Text.Json`.
* Text encoding is now handled when a string is written into a network- or file-stream.
* Switch to IAsyncEnumerator using Microsoft.Bcl.AsyncInterfaces. LanguageVersion is set to 8.0. 
* internal: SCU now sends two presentation context per CStoreRequest: one with the original TS and one with the additional and the mandatory ImplicitLittleEndian. So the chance is higher to send the file without conversion. (#1048)
* Optimize DicomTag.GetHashCode()
* Bug fix: Prevent special characters in association requests from crashing Fellow Oak DICOM (#1104)
* Make DicomService more memory efficient. Use existing streams in PDU and do not create new Memorystreams for every PDU. (#1091)

##### Breaking changes:

* namespace changed from `Dicom` to `FellowOakDicom`
* `IOManager` is removed. Instead of calling `IOManager.CreateFileReference(path)` now directly create a new instance `new Filereference(path)`. The same is true for `DirecotryReference`.
* In general: all *Manager classes with their static `.SetImplementation` initializers have been replaced by Dependency Injection.
* By default there is only `RawImageManager` implementation for `IImageManager`. To have `WinFormsImageManager` or `WPFImageManager` you have to reference the package *fo-dicom.Imaging.Desktop*. To use `ImageSharpImageManager` you have to reference *fo-dicom.Imaging.ImageSharp*.
* There are only asynchronous server provider interfaces. All synchronous methods have been replaced by asynchronous.
* Instances of `DicomClient` and `DicomServer` are not created directly, but via a `DicomClientFactory` or a `DicomServerFactory`.
  If you are in a "DI-Environment" like Asp.Net, then inject a `IDicomClientFactory` instance and use this to create a DicomClient. otherwise call `DicomClientFactory.CreateDicomClient(...)`.  This is a wrapper around accessing the internal DI container , getting the registered IDicomClientFactory and then calling this. So this is more overhead.
* DicomServiceOptions cannot be passed as parameter to DicomServer constructor/factory any more, but the values of options have to be set to the created instance of DicomServer.
* Classes `DicomFileReader`, `DicomReader`, `DicomReaderCallbackObserver` etc are now internal instead of public, because the resulting Datasets are wrong/inconsistent and need further changes. Therefore its usage is dangerous for users. (#823)
* Removed obsolete methods/classes/properties
  * `DicomValidation.AutoValidation`: Call `DicomSetupBuilder.SkipValidation()` instead.
  * `Dicom.Network.DicomClient`: use `FellowOakDicom.Network.Client.DicomClient` instead.
  * `Dicom.Network.IDicomServiceUser`: use `IDicomClientConnection` instead.
  * `ChangeTransferSyntax(..)` extension methods for `DicomFile` and `DicomDataset`: use the method `Clone(..)` instead.
  * `DicomDataset.Get<T>`: use `GetValue`, `GetValues`, `GetSingleValue` or `GetSequence` instead.
  * `DicomDataset.AddOrUpdatePixelData`:  use `DicomPixelData.AddFrame(IByteBuffer)` to add pixel data to underlying dataset.
  * `DicomUID.Generate(name)`: use `DicomUID.Generate()` instead.
  * `DicomUID.IsValid(uid)`: use `DicomUID.IsValidUid(uid)` instead.
  * `DicomUIDGenerator.Generate()` and `DicomUIDGenerator.GenerateNew()`: use `DicomUIDGenerator.GenerateDerivedFromUUID()`
  * `DicomImage.Dataset`, `DicomImage.PixelData` and `DicomImage.PhotometricInterpretation`: do not load the DicomImage directly from filename if you also need access to the dataset, but load the DicomDataset from file first and then construct the DicomImage from this loaded DicomDataset. Then you can access both.
* DicomStringElement and derived classes do not have the "encoding" parameter in constructor, that takes a string-value
* DicomDataset.Add(OrUpdate) does not take an "encoding" parameter any more, instead the DicomDataset has a property `TextEncodings`, that is applied to all string-based tags.
* in update to DICOM2020e the keywords, that are provided by Nema, are used. therefore some DicomUID-Names changed.

   
