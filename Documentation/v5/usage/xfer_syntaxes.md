# Transfer Syntaxes for images

Only for images with transfer syntaxes contained in the following list, extracting the pixel data is supported in fo-dicom.core.

Transfer Syntax UID | Description
----- | ------
1.2.840.10008.1.2 | Implicit VR Little Endian
1.2.840.10008.1.2.1 | Explicit VR Little EndianExplicit VR Little Endian
1.2.840.10008.1.2.1.99 | Deflated Explicit VR Little Endian
1.2.840.10008.1.2.2 | Explicit VR Big Endian

For other Transfer Syntaxes, an additional package is required. Efferent Health is maintaining a cross-platform codec package for fo-dicom. See [Fo-Dicom.Codec](https://github.com/Efferent-Health/fo-dicom.Codecs) to see details about supported Transfer Syntaxes.

# Custom Transfer Syntaxes

fo-dicom supports custom Transfer Syntaxes. If a DICOM image with a custom / unknown Transfer Syntax is received or opened then fo-dicom assumes that it is 
* Little Endian
* Explicit VR
* Encapsulated

When trying to render the pixel data or when trying to transcode the pixel data, fo-dicom throws an exception since it cannot know how to decode the pixel data. The application has to read and write the byte array directly.
If there is a custom Transfer Syntax that does not fit the default parameters, then this Transfer Syntax can be registered anytime before accessing the DicomDataset via the static method `DicomTransferSyntax.Register(DicomUID uid, Endian endian, bool isExplicitVR = true, bool isEncapsulated = true)`, and also Unregistered or Queried.