*fo-dicom* supports asynchronous operations via the `async`/`await` pattern (or [Task-based Asynchronous Pattern, TAP](https://msdn.microsoft.com/en-us/library/hh873175.aspx)).

### Asynchronous  API
The public API has been extended with the following methods for asynchronous operation:

```csharp
    DicomFile
    {
      Task SaveAsync(string fileName)
      static Task<DicomFile> OpenAsync(string fileName)
      static Task<DicomFile> OpenAsync(string fileName, Encoding fallbackEncoding)
      static Task<DicomFile> OpenAsync(Stream stream)
      static Task<DicomFile> OpenAsync(Stream stream, Encoding fallbackEncoding)
    }

    DicomDirectory
    {
      static Task<DicomDirectory> OpenAsync(string fileName)
      static Task<DicomDirectory> OpenAsync(string fileName, Encoding fallbackEncoding)
    }

    DicomClient
    {
      Task SendAsync(DicomClientCancellationMode cancellationMode, CancellationToken cancellationToken);
    }
```

### Usage
To read a DICOM file asynchronously using this API, you can make the call:

```csharp
    var dicomFile = await DicomFile.OpenAsync("some file name");
```

To save an existing file:

```csharp
    await dicomFile.SaveAsync("some other file name");
```

To asynchronously send a service request to some DICOM server:

```csharp
    var client = new DicomClient("DICOMSERVER", 11112, false, "SCU", "ANY-SCP");
    await client.AddRequestAsync(new DicomCEchoRequest());
    await client.SendAsync();
```
