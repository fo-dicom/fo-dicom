## Usage notes

### Image rendering configuration

Out-of-the-box, fo-dicom for .NET defaults to Windows Forms-style image rendering. To switch to WPF-style image rendering, call: ImageManager.SetImplementation(WPFImageManager.Instance)

### Logging configuration

By default, logging defaults to the no-op NullLogerManager. Several log managers are available and can be enabled like this:

```cs
LogManager.SetImplementation(ConsoleLogManager.Instance); // or ...

LogManager.SetImplementation(NLogManager.Instance); // or ...
```
