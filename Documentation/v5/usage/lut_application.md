For grayscale images, *fo-dicom* by default applies the rendering options available in the DICOM dataset when rendering the actual image with `DicomImage.RenderImage`. By default, the rendering options identifies whether the colors should be displayed using the `Monochrome1` or `Monochrome2` color tables. To override this color scheme, the following workarounds are available.

The `Color32[]` property `GrayscaleColorMap` has been added to the `DicomImage` class in *fo-dicom* version 2.0. By default, the color map is selected based on the Photometric Interpretation of the associated dataset (Monochrome 1 or 2), but it can be set to any 256 item `Color32` array prior to rendering the image.

Example of applying arbitrary color map and then render image:

```csharp
    var di = new DicomImage(dicomFileName);
    di.GrayscaleColorMap = ColorTable.LoadLUT(lutFileName);
    var image = di.RenderImage();
```

By default, grayscale images are rendered via the `Monochrome1` or `Monochrome2` color tables: 

![monochrome](https://cloud.githubusercontent.com/assets/6515030/9720999/a3172874-5594-11e5-8c06-5b7ae246ff8c.png)

To override this selection upon rendering, for example using a color table loaded from file via `ColorTable.LoadLUT` or by composing a 256 item `Color32` array, the following helper method can be applied to create an LUT that simultaneously accounts for the image properties specified in the dataset:

```csharp
    public static ILUT CreateFromDataset(DicomDataset dataset, Color32[] colorTable)
    {
      var options = GrayscaleRenderOptions.FromDataset(dataset);

      var composite = new CompositeLUT();
      OutputLUT output;

      if (options.RescaleIntercept != 0.0 || options.RescaleSlope != 1.0)
      {
        composite.Add(new ModalityLUT(options));
      }

      composite.Add(VOILUT.Create(options));
      composite.Add(
        output =
        new OutputLUT(colorTable ?? (options.Monochrome1 ? ColorTable.Monochrome1 : ColorTable.Monochrome2)));

      if (options.Invert)
      {
        composite.Add(new InvertLUT(output.MinimumOutputValue, output.MaximumOutputValue));
      }

      return new PrecalculatedLUT(composite, options.BitDepth.MinimumValue, options.BitDepth.MaximumValue);
    }
```

Creating a composite LUT using a non-monochrome color table such as the "green-fire-blue" color scheme and rendering the same image via the `ImageGraphic.RenderImage(ILUT)` method would then yield the following result:

![greenfireblue](https://cloud.githubusercontent.com/assets/6515030/9714016/7e9a4ae2-5555-11e5-9b46-7fcf75c315c1.jpg)