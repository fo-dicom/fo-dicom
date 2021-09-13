The version 5 of *Fellow Oak DICOM* includes a complete restructuring of the code base.
It has always been a important requirement for fo-dicom to be usable on as many platforms and for as many systema as possible. While up to version 4 this was done by building several platform specific assemblies and a PCL assemly, the version 5 has only one assembly built against .net standard 2.0. It is intended not to build .netstandard 2.1 or .netcore because fo-dicom has to be able to be used from netframework, which is still a very wide used platform.

Take a look [here](#breaking-changes) at the **breaking changes** that you will have to handle if you upgrade an existing application based on fo-dicom 4 to use version 5.

### Changes

* <u>There is only one library built in NetStandard 2.0 *fo-dicom.core*</u>. So there are no dependency problems any more when having referenced fo-dicom in assembly and also in application. 

* <u>Usage of `Microsoft.Extensions.DependencyInjection`</u>. Instead of setting concrete implementations for interfaces in the various Manager*-classes now version 5 uses DI, which is more state of the art today. There is an extension method to `IServiceCollection.AddDefaultDicomServices()` to add all default implementations for the required interfaces.
If there is no DI container provided, fo-dicom creates its own internally. To configure it, call `new DicomSetupBuilder().RegisterServices(s => ...).Build();`
There are extension methods for this DicomSetupBuilder like `.SkipValidation()` or `SetDicomServiceLogging(logDataPdus, log DimseDataset)`.
The new interface `IServiceProviderHost` manages, if there is an internal ServiceProvider or if to use a given Asp.Net Service Provider.
  * *IFileReferenceFactory:* used to create `IFileReference` instances. Is used internally whenever a Reference to a file is created. By default it creates wrapper around System.IO.File, but you can use it to also create wrapper around other types of streams like for web or ftp etc.
  * *IImageManager:* creates a `IImage` instance. Default is `RawImageManager`, that returns a byte array. call  `IServiceCollection.UseImageManager<MyImageManager>()` to register an other implementation. Fo-dicom also delivers a `ImageSharpManager` via nuget.
  * *ITranscoderManager:* manages the codecs, used by `DicomTranscoder`. Call `IServiceCollection.useTranscoderManager<MyTranscoderManager>()` to register an other implementation.
  * *ILogManager:* creates a concrete implementation of `ILogger`.
  * *INetworkManager:* creates a listner, opens streams or checks connection status.
  * *IDicomClientFactory:* is responsible to return an `IDicomClient` instance. This may be a new instance everytime or a reused instance per host or whateever.
  * *IDicomServerFactory:* creates server instances, manages available ports, etc.
  

* <u>DicomServer uses DI to create the instances of your `DicomService`</>. That means that you can use constructor injection there. So if you registered some instances in DI container, then you can utilize those within your DicomService.

* <u>More asnychronous methods</u>. More methods and interfaces are now asynchronous. So now also IAsyncEnumerator using Microsoft.Bcl.AsyncInterfaces are used in some interfaces. This requires to update C# language version to at least 8.0

* <u>A new class `DicomClientOptions`</u> holds all the options of a DicomClient to be passed instead of the huge list of parameters.

* <u>`DicomServerRegistration`</u> is a new class, that manages the started servers per IP/Port. So now you can check and query all running DICOM servers and manage them.

* <u>Memory consumption emprovements</u> in IByteBuffer classes. For example by new methods in `IByteBuffer` to directly manipulate/use the data instead of copying it around multiple times.

* <u>Faster and more efficient json serialization/deserialization</u> based on `System.Text.Json` is now included in *fo-dicom.core* without the need to add a extra nuget-package. But you can still include the nuget-package to serialize/deserialize based on `Newtonsoft.Json`.

* <u>Text encoding in DICOM tags</u> is now handled completely different. While you had to add the encoding as parameter when adding a string to a DicomDataset, this is now not necessary any more. When the DicomDataset is serialized into a network- or file-stream, then the encoding is detected from the DicomDataset and this encoding is then used to serialize the strings.

* <u>Code Extensions</u> are now implemented when decoding datasets with multiple encodings.

* <u>Pixel manipulation pileline</u> internally now uses `System.Double` instead of `System.Int32`. So no data loss because of rounding errors is avoided.

* <u>Volume calcualation and MPR</u> is basically supported. Some helper classes support building volume data and calculate slices from that volume.

* <u>Various bug fixes ... </u>

### Breaking Changes

* The namespace changed from `Dicom` to `FellowOakDicom`, because "Dicom" was too wide as root namespace. Guideline say that the root namespace should contain companyname (or product name). 

* `IOManager` is removed. Instead of calling `IOManager.CreateFileReference(path)` now directly create a new instance `new Filereference(path)` or `FileReferenceFactory.Create(path)`. The same is true for `DirecotryReference`.

* In general: all *Manager classes with their static `.SetImplementation` initializers have been replaced by Dependency Injection.

* By default there is only `RawImageManager` implementation for `IImageManager`. To have `WinFormsImageManager` or `WPFImageManager` you have to reference the package *fo-dicom.Imaging.Desktop*. To use `ImageSharpImageManager` you have to reference *fo-dicom.Imaging.ImageSharp*.

* There are now only asynchronous server provider interfaces. All synchronous methods have been replaced by asynchronous.

* Instances of `DicomClient` and `DicomServer` are not created directly, but via a `DicomClientFactory` or a `DicomServerFactory`.
  If you are in a "DI-Environment" like Asp.Net, then inject a `IDicomClientFactory` instance and use this to create a DicomClient. Otherwise call `DicomClientFactory.CreateDicomClient(...)`. This is a wrapper around accessing the internal DI container, getting the registered IDicomClientFactory and then calling this. So this is more overhead.

* DicomServiceOptions cannot be passed as parameter to DicomServer constructor/factory any more, but the values of options have to be set to the created instance of DicomServer.

* Classes `DicomFileReader`, `DicomReader`, `DicomReaderCallbackObserver` etc are now internal instead of public, because the resulting Datasets are wrong/inconsistent and need further changes. Therefore its usage is dangerous for users.

* obsolte methods/classes/properties are removed:
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

* `DicomStringElement` and derived classes do not have the "encoding" parameter in constructor, that takes a string-value. And `DicomDataset.Add(OrUpdate)` does not take an "encoding" parameter any more, instead the DicomDataset has a property `TextEncoding`, that is applied to all string-based tags within that DicomDataset and sub-datasets in sequences.

* in update to DICOM2021a the keywords, that are provided by Nema, are used. therefore some DicomUID-Names changed.

* Instead of System.Drawing.Color now Color32 is used, to make the code more platform independent (#1140)
