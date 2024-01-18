*This documentation may be outdated and shall be reworked for fo-dicom 5*

Name | UID | Desktop<sup>1</sup> | Universal<sup>2</sup> | .NET&nbsp;Core | Android | &nbsp;iOS&nbsp; | Mono | Unity | Name
--- | --- | :---: | :---: | :---: | :---: | :---: | :---: | :---: | ---
Implicit VR Little Endian<sup>3</sup> | 1.2.840.10008.1.2 | ✔️  | ✔️  | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | Implicit VR Little Endian
Explicit VR Little Endian | 1.2.840.10008.1.2.1 | ✔️  | ✔️  | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | Explicit VR Little Endian
Deflated Explicit VR Little Endian | 1.2.840.10008.1.2.1.99 | ✔️  | ✔️  | ✔️ | ✔️ | ✔️ | ✔️ |   | Deflated Explicit VR Little Endian
Explicit VR Big Endian | 1.2.840.10008.1.2.2 |  ✔️  | ✔️  | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | Explicit VR Big Endian
JPEG Baseline (Process 1)<sup>4</sup> | 1.2.840.10008.1.2.4.50 | ✔️  | ✔️  | ✔️ | ✔️ | ✔️ | ✔️ |   | JPEG Baseline (Process 1)
JPEG Baseline (Processes 2&nbsp;&&nbsp;4)<sup>5</sup> | 1.2.840.10008.1.2.4.51 | ✔️  | ✔️ | 8&#8209;bit | 8&#8209;bit | 8&#8209;bit | 8&#8209;bit |   | JPEG Baseline (Processes 2&nbsp;&&nbsp;4)
JPEG Lossless, Nonhierarchical (Process 14) | 1.2.840.10008.1.2.4.57 | ✔️  | ✔️  |   |   |   |   |   | JPEG Lossless, Nonhierarchical (Process 14)
JPEG Lossless, Nonhierarchical, First-Order Prediction (Process 14 [SV1])<sup>6</sup> | 1.2.840.10008.1.2.4.70 | ✔️  | ✔️  |   |   |   |   |   | JPEG Lossless, Nonhierarchical, First-Order Prediction (Process 14 [SV1])
JPEG-LS Lossless Image Compression | 1.2.840.10008.1.2.4.80 | ✔️  | ✔️  |   |   |   |   |   | JPEG-LS Lossless Image Compression
JPEG-LS Lossy (Near-Lossless) Image Compression | 1.2.840.10008.1.2.4.81 | ✔️  | ✔️  |   |   |   |   |   | JPEG-LS Lossy (Near-Lossless) Image Compression
JPEG 2000 Image Compression (Lossless Only) | 1.2.840.10008.1.2.4.90 | ✔️  | ✔️ | 8&#8209;bit | 8&#8209;bit | 8&#8209;bit | 8&#8209;bit |   | JPEG 2000 Image Compression (Lossless Only)
JPEG 2000 Image Compression | 1.2.840.10008.1.2.4.91 | ✔️  | ✔️ | 8&#8209;bit | 8&#8209;bit | 8&#8209;bit | 8&#8209;bit |   | JPEG 2000 Image Compression
High-Throughput JPEG 2000 Image Compression (Lossless Only)<sup>7</sup> | 1.2.840.10008.1.2.4.201 |   |   |   |   |   |   |   | High-Throughput JPEG 2000 Image Compression (Lossless Only)
High-Throughput JPEG 2000 with RPCL Options Image Compression (Lossless Only)<sup>7</sup> | 1.2.840.10008.1.2.4.202 |   |   |    |   |   |   |   | High-Throughput JPEG 2000 with RPCL Options Image Compression (Lossless Only)
High-Throughput JPEG 2000 Image Compression<sup>7</sup> | 1.2.840.10008.1.2.4.203 |   |   |    |   |   |   |   | High-Throughput JPEG 2000 Image Compression
RLE Lossless | 1.2.840.10008.1.2.5 | ✔️  | ✔️  | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | RLE Lossless

<sup>1</sup>*.NET Framework* package  
<sup>2</sup>*Universal Windows Platform* (UWP), Windows 10 package  
<sup>3</sup>Default Transfer Syntax for DICOM  
<sup>4</sup>Default Transfer Syntax for Lossy JPEG 8-bit Image Compression  
<sup>5</sup>Default Transfer Syntax for Lossy JPEG 12-bit Image Compression (Process 4 only)  
<sup>6</sup>SV1 = Selection Value 1; Default Transfer Syntax for Lossless JPEG Image Compression  
<sup>7</sup>Actual codec support to be implemented in the fo-dicom.Codecs package.

# Custom Transfer Syntaxes

Since **version 4.0.1** and higher fo-dicom supports custom Transfer Syntaxes. If a DICOM image with a custom / unknown Transfer Syntax is received or opened then fo-dicom assumes that it is 
* Little Endian
* Explicit VR
* Encapsulated

When trying to render the pixel data or when trying to transcode the pixel data, fo-dicom throws an exception since it cannot know how to decode the pixel data. The application has to read and write the byte array directly.
If there is a custom Transfer Syntax that does not fit the default parameters, then this Transfer Syntax can be registered anytime before accessing the DicomDataset via the static method `DicomTransferSyntax.Register(DicomUID uid, Endian endian, bool isExplicitVR = true, bool isEncapsulated = true)`, and also Unregistered or Queried.