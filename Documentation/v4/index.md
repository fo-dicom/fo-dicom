# Documentation for fo-dicom version 4

Fellow Oak DICOM, a DICOM toolkit in C# for .NET Framework, .NET Core, Universal Windows, Android, iOS, Mono and Unity.

_Note:_
Version 4 is the last version of `fo-dicom` that is based on the
[Portable Class Library (PCL)](https://docs.microsoft.com/en-us/xamarin/cross-platform/app-fundamentals/pcl).
As PCL is deprecated by Microsoft, newer versions of `fo-dicom` (starting with 5.0.0) use 
[.NET Standard](https://docs.microsoft.com/en-us/xamarin/cross-platform/app-fundamentals/net-standard) instead.
Bugfixes and minor improvements will still be added to `fo-dicom 4` for some time.

## Main features
- Portable Class Library (PCL)
- Targets .NET 4.5.2 and higher, .NET Core (.NET Standard 1.3 and higher), Universal Windows Platform, Xamarin iOS, Xamarin Android, Mono and Unity 
- DICOM dictionary version 2021b
- High-performance, fully asynchronous async/await API
- JPEG (including lossless), JPEG-LS, JPEG2000, and RLE image compression (limited on .NET Core, Xamarin, Mono and Unity platforms)
- Supports very large datasets with content loading on demand
- Platform-specific image rendering
- JSON import and export
- XML export
- Anonymization

## Installation
The newest `fo-dicom 4` release binaries can be obtained from [NuGet](https://www.nuget.org/packages/fo-dicom/4.0.8).
This package references the core *fo-dicom* assemblies for all Microsoft and Xamarin platforms.

## NuGet Packages

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
