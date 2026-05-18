## Introduction
*fo-dicom* relies on Microsoft.Extensions.Logging for its logging purposes.
Any logging configuration that is configured there, will automatically be used.

For example, when codecs are loaded, their names are written through the logger at a debug level.

## Default configuration
By default, *fo-dicom* will make use of `Microsoft.Extensions.Logging` for all of its logging.

More specifically, *fo-dicom* injects loggers of type `Microsoft.Extensions.Logging.ILogger` where necessary.

## Default logging libraries
To use NLog, Serilog or other logging libraries, simply configure them as you would for `Microsoft.Extensions.Logging` and they will be used automatically by *fo-dicom*.

## Logging Policy - Data Protection (PHI)

fo-dicom's intent is to avoid emitting PHI in library-generated logs.

* The library does not dump DICOM datasets or element values by default.
* Dataset-to-log helpers exist but are opt-in and not invoked by the library.
* PHI may still surface through application code, custom handlers, or exception payloads; therefore, fo-dicom cannot guarantee zero-PHI logging in all deployments.
* If you observe a library message that exposes PHI, please open an issue with a minimal, redacted reproduction so we can evaluate and adjust.
