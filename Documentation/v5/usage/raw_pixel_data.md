**Q:** I'm trying to read an RT Dose DICOM file, but I need to access to the image data because I need the array with the raw values, is it possible to get it with *fo-dicom*?

**A:**
You can for example obtain the pixel data from a `DicomDataset` called `dataset` for a specific frame with index `idx` like this:

```csharp
    var header = DicomPixelData.Create(dataset);
    var pixelData = PixelDataFactory.Create(header, idx);
```

The `PixelDataFactory.Create` creates different pixel data types depending on dataset properties such as bits allocated, pixel representation etc. Check the actual type of `pixelData` to obtain a pixel array that you can operate on:

```csharp
    if (pixelData is GrayscalePixelDataU16) {
      ushort[] pixels = ((GrayscalePixelDataU16)pixelData).Data;
      ...
    }
```

etc.

To obtain a full 3D grid from a multi-frame image, loop over all frames in the image, as defined by `header.NumberOfFrames`.