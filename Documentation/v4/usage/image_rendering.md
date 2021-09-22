**NOTE! This image rendering API applies from version 2.0.0 and to version 4.0.x of _fo-dicom_.**

We have implemented an image abstraction layer that facilitates the adoption of *fo-dicom* image rendering functionality to various platforms.

We have introduced an interface `IImage` to represent an image object. This interface has been implemented for `System.Drawing.Image` (`WinFormsImage`) and `System.Windows.Media.Imaging.WriteableBitmap` (`WPFImage`) on .NET as well as for the relevant image types on the other platforms that *fo-dicom* target (`WriteableBitmap` on the *Universal Windows Platform*, `CGImage` on *iOS*, `Bitmap` on *Android* etc.).

The `IImage` interface is minimal; its only operational method is `DrawGraphics`, which is used to draw image layers when rendering a `CompositeGraphic`. 

For consumers of `IImage` on different platforms, the most important interface method is however

    T As<T>();

which allows you to cast the `IImage` object to the real image type on whatever platform you are targeting!

Which `IImage` implementation to use is determined in the new `ImageManager` class (compare with `LogManager` and the recently added `IOManager`). On :NET, with two image manager implementations, *Windows Forms* based imaging is selected by default. To select *WPF* based imaging, call:

    ImageManager.SetImplementation(WPFImageManager.Instance);

To encompass the introduction of `IImage`, the public API has been changed as follows:

```IGraphic {```<br />
```IImage RenderImage(ILUT lut);```<br />
~~```System.Drawing.Image RenderImage(ILUT lut);```~~<br />
~~```System.Windows.Media.Imaging.BitmapSource RenderImageSource(ILUT lut);```~~<br />
``` } ```<br />

```DicomImage {```<br />
```IImage RenderImage(int frame = 0);```<br />
~~```System.Drawing.Image RenderImage(int frame = 0);```~~<br />
~~```System.Windows.Media.ImageSource RenderImageSource(int frame = 0);```~~<br />
``` } ```<br />

The direct (and only practical) consequence of these changes to the public API is that to get the real image object, it will be necessary to use the `As<T>()` "cast operator".

For example, in a *Windows Forms* application (provided `ImageManager` is using the `WinFormsImageManager` implementation):

    Bitmap renderedImage = dicomImage.RenderImage().As<Bitmap>();

And in a *WPF* application (provided `ImageManager` is using the `WPFImageManager` implementation):

    WriteableBitmap renderedImage = dicomImage.RenderImage().As<WriteableBitmap>();

With these changes, it is now completely possible to maintain for example the `DicomImage` implementation in a platform-agnostic core library, whereas the `WinFormsImage` and `WPFImage` implementations can be moved to a *Windows desktop* specific support library. What's more, on the *Mono* platform where WPF is not available, the `WinFormsImage` implementation can still be included in a *Mono* specific support library (see #13).

### Platform specifics

#### .NET
There are convenience extension methods for obtaining `Bitmap` and `WriteableBitmap` objects, that circumvent the use of `IImage.As<>`:

    IImage.AsBitmap();
    IImage.AsWriteableBitmap();

#### iOS
On *iOS*, an `IImage` object can be cast, using the `As<>()` method, to `CGImage`, `UIImage` and `CIImage`.

There are convenience extension methods for obtaining different iOS image objects, that circumvent the use of `IImage.As<>`:

    IImage.AsCGImage();
    IImage.AsCIImage();
    IImage.AsUIImage();

#### Android
On *Android*, an `IImage` object can be cast, using the `As<>()` method, to `Android.Graphics.Bitmap`.

There is a convenience extension method for obtaining `Android.Graphics.Bitmap` objects, that circumvent the use of `IImage.As<>`:

    IImage.AsBitmap();

#### Universal Windows Platform
On the *Universal Windows Platform*, an `IImage` object can be cast, using the `As<>()` method, to `WriteableBitmap` or any of its superclasses, i.e. `BitmapSource` or `ImageSource`.

There is a convenience extension method for obtaining `WriteableBitmap` objects, that circumvent the use of `IImage.As<>`:

    IImage.AsWriteableBitmap();

#### Mono
On *Mono*, the `WinFormsImageManager` is re-used, meaning that the `IImage` object can be cast to `Bitmap` or its superclass `Image`.

#### Unity
On *Unity*, an `IImage` object can be cast, using the `As<>()` method, to `Texture2D`.

There is a convenience extension method for obtaining `Texture2D` objects, that circumvent the use of `IImage.As<>`:

    IImage.AsTexture2D();