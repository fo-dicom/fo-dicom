We have implemented an image abstraction layer that facilitates the adoption of *fo-dicom* image rendering functionality to various platforms.

We have introduced an interface `IImage` to represent an image object. 

This interface has a default implementation `RawImage`. But you can add packages with implementations based on `System.Drawing.Image` (`WinFormsImage`) and `System.Windows.Media.Imaging.WriteableBitmap` (`WPFImage`) on .NET as well as for `SixLabors.ImageSharp` on .net core to be used on many different platforms.

The `IImage` interface is minimal; its only operational method is `DrawGraphics`, which is used to draw image layers when rendering a `CompositeGraphic`. 

For consumers of `IImage` on different platforms, the most important interface method is however

    T As<T>();

which allows you to cast the `IImage` object to the real image type on whatever platform you are targeting!

Which `IImage` implementation to use is determined in the `ImageManager` class (compare with `LogManager` and `TranscoderManager`). In core library *RawImage* based imaging is selected by default. To select an other imaging, call:

    new DicomSetupBuilder()
        .RegisterServices(s => s.AddImageManager<ImageSharpImageManager>())
        .Build();

The public API for imaging is as follows:

```DicomImage {```<br />
```    IImage RenderImage(int frame = 0);```<br />
``` } ```<br />

The direct (and only practical) consequence of this API is that to get the real image object, it will be necessary to use the `As<T>()` "cast operator".

For example, in a *Net Core* application (provided `ImageManager` is using the `ImageSharpImageManager` implementation):

    Image<Bgra32> renderedImage = dicomImage.RenderImage().AsSharpImage();

And in a *Desktop* application (provided `ImageManager` is using the `WinFormsImageManager` implementation):

    Bitmap renderedImage = dicomImage.RenderImage().AsClonedBitmap();


### Platform specifics

#### .NET
There are convenience extension methods for obtaining `Bitmap` and `WriteableBitmap` objects, that circumvent the use of `IImage.As<>`:

    IImage.AsClonedBitmap();
    IImage.AsSharedBitmap();
    IImage.AsWriteableBitmap();

The difference between the two methods `AsClonedBitmap()` and `AsSharedBitmap()` is the internal memory management. When calling `AsSharedBitmap()` and by default, to reduce momory consumption, the Bitmap encapsulates the the memory used by the `IImage` implementation. Therefore the Bitmap gets invalid if the IImage instance geets disposed. To avoid this, you have to call `AsClonedBitmap()` to get a Bitmap instance with its own memory where the pixel data will be copied to.

