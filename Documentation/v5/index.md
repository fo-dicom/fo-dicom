# Documentation for fo-dicom version 5

Fellow Oak DICOM, a DICOM toolkit in C# for all .NET Standard 2.0 compatible frameworks.

_Note_:
`fo-dicom 5` is the first version based on .NET Standard 2.0, earlier versions are available targeting Portable Class Library (PCL), NetFramework, Xamarin, Mono, Unity or NetCore instead.
If you still need to rely on PCL or on one of the other listed frameworks, check the latest `fo-dicom 4.x` release and the respective documentation.

## Main features
- Targets .NET Standard 2.0 and will work on all platform supporting that standard
- DICOM dictionary version 2021b
- High-performance, fully asynchronous async/await API
- JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression (via additional package)
- Supports very large datasets with content loading on demand
- Image rendering to `System.Drawing.Bitmap` or `SixLabors.ImageSharp`
- JSON and XML import and export
- Anonymization
- DICOM services
- Customize components via DI container

## Installation
The newest `fo-dicom` release binaries can be obtained from [NuGet](https://www.nuget.org/packages/fo-dicom).
This package references the core *fo-dicom* assemblies for all Microsoft and Xamarin platforms.

## NuGet Packages
Package | Description
------- | -----------
[fo-dicom](https://www.nuget.org/packages/fo-dicom/) | Core package containing parser, services and tools
[fo-dicom.Imaging.Desktop](https://www.nuget.org/packages/fo-dicom.Imaging.Desktop/) | Library with referencte to System.Drawing, required for rendering into Bitmaps
[fo-dicom.Imaging.ImageSharp](https://www.nuget.org/packages/fo-dicom.Imaging.ImageSharp/) | Library with reference to ImageSharp, can be used for platform independent rendering
[fo-dicom.NLog](https://www.nuget.org/packages/fo-dicom.NLog/) | .NET connector to enable `fo-dicom` logging with NLog
[fo-dicom.Codecs](https://www.nuget.org/packages/fo-dicom.Codecs/) | Cross-platform DICOM codecs for `fo-dicom`, developed by [Efferent Health](https://github.com/Efferent-Health/fo-dicom.Codecs)
