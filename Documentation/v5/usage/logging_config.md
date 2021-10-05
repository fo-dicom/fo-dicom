## Introduction
*fo-dicom* has its own logging abstraction through which it can log information and warning messages.  For example, when codecs are loaded, their names are written through the logger at a debug level.

## Default configuration
By default, a null logger is selected, preventing all logger output. On all platforms except *Universal Windows Platform*, it is possible to steer logger output to a console window with the following code:
```
LogManager.SetImplementation(ConsoleLogManager.Instance);
```

The log manager can at any time be reset to the null logger using the following call:
```
LogManager.SetImplementation(null);
```

## Logging libraries
Currently there are four pre-built integrations with third-party logging libraries.

### NLog
To configure *NLog* integration with *fo-dicom*, simply insert the following code
```
LogManager.SetImplementation(NLogManager.Instance);
```

### Serilog
To configure *Serilog* integration with *fo-dicom*, simply insert the following code
```
LogManager.SetImplementation(new SerilogManager());
```
This will use the global `Serilog.Log` logger instance.  Alternatively, the `SerilogManager` class' constructor has an override that takes an instance of a *Serilog* `ILogger`.

An example project providing a sample *Serilog* configuration, demonstrating integration with *Seq*, writing to the console and some rolling output files is included in the source.

### log4net
To configure *log4net* integration with *fo-dicom*, simply insert the following code
```
LogManager.SetImplementation(Log4NetManager.Instance);
```

### MetroLog
To configure *MetroLog* integration with *fo-dicom*, simply insert the following code
```
LogManager.SetImplementation(MetroLogManager.Instance);
```
