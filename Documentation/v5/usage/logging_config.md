## Introduction
*fo-dicom* relies on Microsoft.Extensions.Logging for its logging purposes.
Any logging configuration that is configured there, will automatically be used.

For example, when codecs are loaded, their names are written through the logger at a debug level.

## Default configuration
By default, *fo-dicom* will make use of `Microsoft.Extensions.Logging` for all of its logging.

More specifically, *fo-dicom* injects loggers of type `Microsoft.Extensions.Logging.ILogger` where necessary.

## Default logging libraries
To use NLog, Serilog or other logging libraries, simply configure them as you would for `Microsoft.Extensions.Logging` and they will be used automatically by *fo-dicom*.

## Backwards compatibility configuration
In the past, *fo-dicom* used custom abstractions for logging called `FellowOakDicom.Log.ILogManager` and `FellowOakDicom.Log.ILogger`
Using these historical abstractions is still supported, but they have moved to the `FellowOakDicom.Log.Obsolete` namespace and are marked obsolete.

It is possible to change the log manager with the following code:
```
new DicomSetupBuilder()
    .RegisterServices(s => s.AddLogManager<MyLogManager>())
    .Build();
```

__Warning__: internally, this `MyLogManager` will be registered as an `ILoggerProvider` (through an adapter) in `Microsoft.Extensions.Logging`
If you use `Microsoft.Extensions.Logging` elsewhere in the same application, your `MyLogManager` will also appear there.

## Backwards compatibility logging libraries
NLog is officially supported via a pre-built library available on nuget: https://www.nuget.org/packages/fo-dicom.NLog/

To configure *NLog* integration with *fo-dicom*, simply reference the nuget package, and insert the following code
```
new DicomSetupBuilder()
    .RegisterServices(s => s.AddLogManager<NLogManager>())
    .Build();
```

For all other logging libraries, you will have to provide your own implementation of `ILogManager` and `ILogger`.
