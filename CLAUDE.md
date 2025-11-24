# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

**Fellow Oak DICOM (fo-dicom)** is a .NET DICOM library supporting medical imaging applications. It provides comprehensive DICOM parsing, networking, imaging, and manipulation capabilities.

**Main Branch**: `development` (all PRs should target this branch)

## Key Build Commands

### Building the Project
```bash
# Build entire solution
dotnet build FO-DICOM.Full.sln

# Build in Release mode
dotnet build FO-DICOM.Full.sln --configuration Release

# Build specific project
dotnet build FO-DICOM.Core/FO-DICOM.Core.csproj

# Create NuGet packages (automatically generated on build for core project)
dotnet pack FO-DICOM.Core/FO-DICOM.Core.csproj -c Release
```

### Running Tests
```bash
# Run all cross-platform tests
dotnet test Tests/FO-DICOM.Tests/FO-DICOM.Tests.csproj --configuration Release --framework net8.0

# Run specific framework tests
dotnet test Tests/FO-DICOM.Tests/FO-DICOM.Tests.csproj --framework net9.0
dotnet test Tests/FO-DICOM.Tests/FO-DICOM.Tests.csproj --framework net462  # Windows only

# Run Windows-specific tests (WinForms/GDI+ imaging)
dotnet test Tests/FO-DICOM.Tests.Windows/FO-DICOM.Tests.Windows.csproj --framework net8.0-windows

# Run with code coverage (net8.0 only per CI configuration)
dotnet test Tests/FO-DICOM.Tests/FO-DICOM.Tests.csproj --framework net8.0 --collect:"XPlat Code Coverage" --settings coverlet.runsettings
```

### Running Benchmarks
```bash
# Build benchmarks
dotnet build Tests/FO-DICOM.Benchmark/FO-DICOM.Benchmark.csproj --configuration Release --framework net8.0

# Run benchmarks
./Tests/FO-DICOM.Benchmark/bin/Release/net8.0/fo-dicom.Benchmark.exe
```

## Project Structure

### Core Library
- **FO-DICOM.Core/** - Main DICOM library (targets netstandard2.0)
  - `Network/` - DICOM networking (C-STORE, C-FIND, C-MOVE, C-ECHO, N-ACTION, etc.)
  - `Network/Client/` - DICOM client implementations
  - `Network/Tls/` - TLS/SSL support for secure DICOM connections
  - `Imaging/` - Image rendering and manipulation
  - `Imaging/Codec/` - Compression codec infrastructure (JPEG, JPEG-LS, JPEG2000, RLE, HTJ2K)
  - `Imaging/Render/` - Image rendering pipeline with VOI LUT, modality LUT
  - `IO/` - File I/O, buffering, and DICOM stream reading/writing
  - `Serialization/` - JSON and XML export/import
  - `Dictionaries/` - Embedded DICOM data dictionaries (compressed)
  - `Log/` - Logging infrastructure (Microsoft.Extensions.Logging)
  - `Log/Metrics/` - Network metrics collection (INetworkMetricsCollector)
  - `Memory/` - Memory management utilities
  - `Printing/` - DICOM print support
  - `StructuredReport/` - Structured reporting support
  - `Tools/` - Utility classes (UID generation, anonymization)
  - `T4/` - T4 text templates for code generation

### Platform-Specific Packages
- **Platform/FO-DICOM.Imaging.Desktop/** - System.Drawing/GDI+ rendering (Windows)
- **Platform/FO-DICOM.Imaging.ImageSharp/** - ImageSharp cross-platform rendering
- **Platform/FO-DICOM.Imaging.ImageSharp.NetStandard/** - ImageSharp for .NET Standard
- **Platform/FO-DICOM.Imaging.SkiaSharp/** - SkiaSharp rendering
- **Platform/FO-DICOM.AspNetCore/** - ASP.NET Core integration
- **Platform/FO-DICOM.Instrumentation/** - Telemetry and instrumentation

### Additional Packages
- **Serialization/FO-DICOM.Json/** - JSON serialization support
- **Tools/FO-DICOM.Dump/** - Command-line DICOM file dumping utility

### Tests
- **Tests/FO-DICOM.Tests/** - Main cross-platform test suite (net8.0, net9.0, net462)
- **Tests/FO-DICOM.Tests.Windows/** - Windows-specific tests for GDI+ imaging (net8.0-windows)
- **Tests/FO-DICOM.Benchmark/** - BenchmarkDotNet performance tests
- **Tests/FO-DICOM.AspNetCoreTest/** - ASP.NET Core integration tests

## Architecture Notes

### Multi-Framework Support
The library targets multiple frameworks to support various .NET ecosystems:
- **netstandard2.0** - Core library for broad compatibility (.NET Framework 4.6.2+, .NET Core, Xamarin)
- **net8.0/net9.0** - Modern .NET runtimes with latest performance optimizations
- **net462** - .NET Framework support (Windows only)
- **net8.0-windows/net9.0-windows** - Windows-specific features (WinForms, GDI+)

### Dependency Injection Architecture
fo-dicom v5+ uses Microsoft.Extensions.DependencyInjection throughout:
- **Modern .NET setup**: Use `services.AddFellowOakDicom()` with WebApplication or Host builders
- **Legacy .NET setup**: Use `DicomSetupBuilder` for .NET Framework applications
- **Service resolution**: Static APIs (DicomFile.Open, DicomServerFactory.Create) use global service provider
- **DI-aware APIs**: IDicomServerFactory, IDicomClientFactory, IAdvancedDicomClientConnectionFactory available for injection

### Image Rendering
Out-of-the-box, fo-dicom uses internal IImage rendering. To use platform-specific rendering:
- **WinForms/Desktop**: Add FO-DICOM.Imaging.Desktop package and register `WinFormsImageManager`
- **Cross-platform**: Add FO-DICOM.Imaging.ImageSharp package and register `ImageSharpImageManager`
- **SkiaSharp**: Add FO-DICOM.Imaging.SkiaSharp package and register `SkiaSharpImageManager`

### Code Generation
Several files are auto-generated using T4 templates:
- `DicomTagGenerated.cs` - DICOM tag definitions (generated from DICOM dictionary)
- `DicomUIDGenerated.cs` - DICOM UID definitions
- `DicomAnonymizerGenerated.cs` - Anonymization rules

**Do not manually edit these generated files.** Modify the corresponding .tt template files instead.

### DICOM Networking Services
When implementing custom DICOM services (SCP providers), **constructors must include these exact three types** (names and order don't matter):
1. `INetworkStream stream`
2. `Encoding fallbackEncoding`
3. `ILogger logger` (non-generic)

Additional custom dependencies can be injected beyond these required parameters.

### Async Architecture
The library uses fully async/await patterns throughout:
- All I/O operations are async (DicomFile.OpenAsync, DicomFile.SaveAsync)
- Network operations are async (client.SendAsync, connection.OpenAssociationAsync)
- Use cancellation tokens for operation cancellation

### Version Information
- Current version: **5.2.4** (as of Directory.Build.props)
- DICOM standard: **2025d** (latest)
- C# language version: **8.0**
- Breaking changes between v4 and v5: Migration guide at https://github.com/fo-dicom/fo-dicom/wiki/Upgrade-from-version-4-to-version-5

## CI/CD Pipeline

### GitHub Actions Workflows
- **.github/workflows/build.yml** - Main build and test workflow
  - Runs on push/PR to `development` branch
  - Tests on multiple OS (Windows x64/ARM, Ubuntu x64/ARM, macOS x64/ARM)
  - Tests multiple frameworks (net8.0, net9.0, net462)
  - Code coverage on net8.0 only (uploaded to codecov)
  - Runs benchmarks on Windows

- **.github/workflows/builddocs.yml** - Documentation generation (DocFx)

### Test Matrix
The CI runs tests across:
- **6 OS/architecture combinations**: Windows-x64, Windows-arm, Ubuntu-x64, Ubuntu-arm, macOS-x64, macOS-arm
- **3 framework targets**: net8.0, net9.0, net462 (Windows only)
- Cross-platform tests run on all OS, Windows-specific tests run only on Windows

## NuGet Packages

The solution produces multiple NuGet packages:
- **fo-dicom** (core) - Main DICOM library
- **fo-dicom.Imaging.Desktop** - Windows GDI+ rendering
- **fo-dicom.Imaging.ImageSharp** - Cross-platform ImageSharp rendering
- **fo-dicom.Imaging.SkiaSharp** - SkiaSharp rendering
- **fo-dicom.Codecs** - External codec package (Efferent Health, not in this repo)

All packages use MS-PL license.

## Common Development Patterns

### Opening and Manipulating DICOM Files
```csharp
// Async is preferred
var file = await DicomFile.OpenAsync(@"test.dcm");
var patientId = file.Dataset.GetString(DicomTag.PatientID);
file.Dataset.AddOrUpdate(DicomTag.PatientName, "DOE^JOHN");
await file.SaveAsync(@"output.dcm");
```

### Creating DICOM Clients and Servers
Use factory interfaces via DI or static factory methods:
```csharp
// Via DI (preferred)
public MyService(IDicomClientFactory clientFactory, IDicomServerFactory serverFactory)

// Via static API (requires DicomSetupBuilder.UseServiceProvider setup)
var client = DicomClientFactory.Create("127.0.0.1", 104, false, "SCU", "SCP");
var server = DicomServerFactory.Create<DicomCEchoProvider>(104);
```

### Implementing Custom DICOM Services
```csharp
public class MyService : DicomService, IDicomServiceProvider, IDicomCEchoProvider
{
    // Required constructor parameters (exact types required, names/order flexible)
    public MyService(INetworkStream stream, Encoding fallbackEncoding, ILogger logger,
                     DicomServiceDependencies dependencies,
                     MyCustomDependency customDep) // Additional custom dependencies OK
        : base(stream, fallbackEncoding, logger, dependencies)
    {
        // Implementation
    }
}
```

### Logging
fo-dicom uses Microsoft.Extensions.Logging. Configure logging via standard .NET patterns:
```csharp
builder.ConfigureLogging(logging => {
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Information);
});
```

## Important Notes

- **Breaking changes**: v5 has significant breaking changes from v4. Consult migration guide before upgrading legacy code.
- **TLS handshake blocking**: v5.1.4+ includes fixes for frozen TLS handshakes blocking the TCP listener loop
- **Memory management**: v5.2.3+ fixes memory leaks in DicomServer
- **Character encoding**: Use FallbackEncoding when SpecificCharacterSet is missing or invalid
- **Multi-frame rendering**: v5.1.3+ supports parallel multi-frame rendering with proper synchronization
- **HTJ2K support**: Core support added in v5.1.3, but codec implementation requires external package
- **Functional groups**: Enhanced MR/CT rendering uses functional group sequences when present
- **Warnings as errors**: TreatWarningsAsErrors is set to false in Directory.Build.props
- **Nullable reference types**: Enabled project-wide
