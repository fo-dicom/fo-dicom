In case you find runtime exception like

    Decoding dataset with transfer syntax: JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression is not supported

then check those things:

### Is the Transfer Syntax supported on your platform

Check this wiki page [Supported-Transfer-Syntaxes](https://github.com/fo-dicom/fo-dicom/wiki/Supported-Transfer-Syntaxes) if fo-dicom provides an encoder/decoder for your platform. 

if you are building netcore and running on linux, then you can also take a look at https://github.com/Efferent-Health/Dicom-native. They shared their codec that runs on every netcore platform.


### Is the build architecture set

When installing *fo-dicom* via *NuGet*, a build script *fo-dicom.targets* is inserted into the Visual Studio project. This 
build script is responsible for fetching the _Dicom.Native*.dll_ associated with the current build architecture to the build output (*/bin*) folder.

The fetch procedure only works if the build architecture of the **project** is set to either *x86* (fetches *Dicom.Native.dll*) or *x64* (fetches *Dicom.Native64.dll*), otherwise you will be prompted in the build log to select an appropriate build architecture.

This means that, when installing via *NuGet*, you will be able to sufficiently build your *fo-dicom* referencing application or class library even in the *Any CPU* architecture setting, but you will not be able to access the native codecs since these libraries will not be copied to the build output folder. To sufficiently access the native codecs via your application, please make sure that the project build architecture is set to *x86* or *x64*.

