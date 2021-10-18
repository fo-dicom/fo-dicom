## Introduction
*fo-dicom* has its own logging abstraction through which it can log information and warning messages.  For example, when codecs are loaded, their names are written through the logger at a debug level.

## Default configuration
By default, a `ConsoleLogManager` logger is selected to steer all logger output to the console.

*fo-dicom* also is shipped with a `NullLoggerManager`, that skips any log output, and a `TextWriterLogManager`, that redirects all log entries into a file.

It is possible to change the log manager with the following code:
```
new DicomSetupBuilder()
    .RegisterServices(s => s.AddLogManager<NullLoggerManager>())
    .Build();
```


## Logging libraries
Currently there is one pre-built integration with third-party logging libraries available on nuget.

### NLog
To configure *NLog* integration with *fo-dicom*, simply reference the nuget package fo-dicom.nlog, and insert the following code
```
new DicomSetupBuilder()
    .RegisterServices(s => s.AddLogManager<NLogManager>())
    .Build();
```
