#### v1.0.37 (2/19/2014)
* Add DICOM Compare tool to project
* Add support for disabling TCP Nagle algorithm
* Better handling of aborted connections
* Fix some UID values containing encoding characters
* Fix extraction of embedded overlays
* Utility methods for calculating window/level
* Use number keys to change W/L calculation mode in dump utility
* Use O key to show/hide overlays in dump utility
* Fix byte swapping for Implicit VR single byte images

#### v1.0.36 (8/6/2013)
* Fix bug sorting private tags
* Ability to add private tags to dataset
* Better handling of improperly encoded private sequences
* Fix bug adding presentation contexts from requests
* Fix typo in N-Set response handler

#### v1.0.35 (7/5/2013)
* Fix bug adding presentation contexts with Implicit Little Endian syntax
* Add option to use the remote AE Title as the logger name
* Miscellaneous improvements and fixes

#### v1.0.34 (6/27/2013)
* Fix bug returning default value for zero length elements
* Fix exception when disposing DicomDirectory
* Fix menu bug in DICOM Dump utility when changing syntax to JPEG Lossless
* Fix bug matching private DICOM tags
* Add classes for basic structured report creation
* Ability to browse multiframe images in DICOM Dump utility
* Ability to recover when parsing invalid sequences
* Ability to parse improperly encoded private sequences

#### v1.0.33 (4/28/2013)
* Add logging abstraction layer to remove runtime dependency on NLog
* Fix bug reading duplicate entries in DICOM dictionary
* Fix bug storing AT values
* Fix bug sorting private DICOM tags
* Fix bug using custom DicomWriteOptions

#### v1.0.32 (3/12/2013)
* DICOM Media example by Hesham Desouky
* Move DicomFileScanner to Dicom.Media namespace
* Add WriteToConsole logging extension method
* Increase line length of dataset logging extension methods
* Fix reading of sequences in DICOMDIR files (Hesham Desouky)
* Improve rendering performance by precalculating grayscale LUT
* Ability to render single-bit images
* Fix hash code implementation for private tags
* Miscellaneous improvements and fixes

#### v1.0.31 (1/13/2013)
* Improved portability and fewer compiler warnings (Anders Gustafsson)
* Ensure frame buffers created by codecs have even length
* Import DicomFileScanner from mDCM
* Ability to save datasets and images in DICOM Dump utility
* Ability to change tranfer syntax in DICOM Dump utility
* Fix loss of embedded overlay data during compression
* Fix rendering of big endian images
* Miscellaneous improvements and fixes

#### v1.0.30 (1/1/2013)
* Fix bug rescaling DicomImage (Mahesh Dubey)
* Add rules for matching and transforming DICOM datasets
* Miscellaneous improvements and fixes

#### v1.0.29 (12/22/2012)
* Minor modifications to facilitate library porting to Windows Store apps (Anders Gustafsson)
* Convert unit tests to use MS Unit Testing framework
* Move DICOMDIR classes back to Dicom.Media namespace
* Change DicomDirectory Open/Save methods to be consistent with DicomFile usage
* Fix bug decoding single frame from a multiframe image (Mahesh Dubey)

#### v1.0.28 (12/12/2012)
* Display exception message in DICOM Dump for image rendering errors
* Remove serialization members
* Fix bug where frame is not added to OtherWordPixelData (Mahesh Dubey)
* Fix bug where exception is not thrown for out of range frame numbers in GetFrame() (Mahesh Dubey)
* Fix bug where internal transfer syntax is not set when transcoding file (Mahesh Dubey)
* Add request/response classes for DICOM Normalized services
* Change DicomCStoreRequest constructor to use SOP Class/Instance UIDs from dataset instead of FMI
* Experimental support for DICOM directory files (Hesham Desouky)

#### v1.0.27 (10/30/2012)
* Ensure that file handles are closed after opening or saving DICOM file
* Add ability to move file pointed to by FileReference
* Fix window/level settings not being applied to rendered DicomImage
* Fix processing order of received DIMSE responses
* Process P-Data-TF PDUs on ThreadPool
* Temporary fix for rendering JPEG compressed YBR images
* Fix Async Ops window for associations where it is not negotiated
* Fix bug reading Palette Color LUT with implicit length
* Support decompression and rendering of JPEG Process 2 images
* Enable modification of Window/Level in DicomImage

#### v1.0.26 (10/19/2012)
* Advanced DIMSE logging options for DicomService base class
* Advanced configuration options for P-Data-TF PDU buffer lengths
* Fix bug where final PDV may not be written to P-Data-TF PDU
* Fix bug reading DIMSE datasets from network

#### v1.0.25 (10/18/2012)
* Fix min/max pixel value calculation for unsigned 32-bit pixel values
* Fix collection modified exception when calculating group lengths
* Better handling of null values when adding elements to dataset
* Fix default values when accessing SS/US element values (Anders Gustafsson, Cureos AB)
* Fix decoding of JPEG2000 images with signed pixel data (Mahesh Dubey)
* Ability to decompress single frame from dataset (Mahesh Dubey)
* Use ThreadPoolQueue to process related response messages in order

#### v1.0.24 (10/01/2012)
* Change the default presentation context transfer syntax acceptance behavior to prefer the SCU proposed order
* Reject all presentation contexts that have not already been accepted or rejected when sending association accept
* Add finalizers to temp file classes to catch files not deleted at application exit
* Remove Exists() method from DicomDataset (duplicates functionality of Contains())
* Extension methods for recalculating and removing group length elements
* Force calculation of group lengths when writing File Meta Info and Command datasets
* Fix exception when attempting to display ROI overlays
* Add ability to extract embedded overlays from pixel data
* Detect incorrect transfer syntax in file meta info
* Add support for reading and displaying GE Private Implicit VR Big Endian syntax

#### v1.0.23 (09/26/2012)
* Fix W/L calculation creating negative window width
* Round VOI LUT values instead of casting away fraction
* Fix bug reading signed pixel data from buffer
* Fix encoding of JPEG2000 images with signed pixel data
* Throttle queueing of PDUs to prevent out of memory errors for very large datasets
* Better management of PDU buffer memory
* Better handling for irregular specific character sets
* Support displaying images without specified photometric interpretation
* Ability to read files without preamble or file meta information (including ACR-NEMA)

#### v1.0.22 (09/25/2012)
* Add Offending Element tags to C-Move response output
* Add exception handling for C-Store requests with unparsable datasets
* Handle ObjectDisposedExceptions in network operations
* Fix transcoding between uncompressed transfer syntaxes
* Don't parse values for IS and DS elements if returning string types
* Add AMICAS private tags (AMICAS0) to private dictionary
* Add support for 32-bit pixel data (Anders Gustafsson, Cureos AB)
* Better handling of default item in Get<> method (Anders Gustafsson, Cureos AB)
* Support opening DicomFile from Stream (Anders Gustafsson, Cureos AB)
* Add support SIGMOID VOI LUT function
* Better handling of size and position of image display window in DICOM Dump
* Fix calculation of W/L from smallest/largest pixel value elements
* Fix viewing of images with bits allocated == 16 and bits stored == 8
* Add support for image scaling in DicomImage
* Use library to scale images before displaying in DICOM Dump
* Calculate W/L from pixel data values if no defaults are available
* Add ability to Get<> Int32 values from US/SS elements
* Add ability to Get<> DicomVR and IByteBuffer from elements
* Add ability to Get<> byte[] from elements
* Add ability to render basic PALETTE COLOR images
* Fix unnecessary byte swap for 8-bit pixel data stored in OW
* Add DicomFileException to allow better chance of recovery from parse errors
* Add maximum PDU length to association output
* Fix major bug in writing PDataTF PDUs; improvements in performance and memory usage
* Add ability to propose additional transfer syntaxes in C-Store request

#### v1.0.21 (09/14/2012)
* Add connection close event and socket error handlers to DicomService
* Fix C-Store SCP example's constructor not passing logger

#### v1.0.20 (09/13/2012)
* Fix exception in DicomClient when releasing association

#### v1.0.19 (09/13/2012)
* Force passing of Logger to DicomService constructor (may be null)

#### v1.0.18 (09/13/2012)
* Print Offending Element values when outputing request to log
* Add ability to pass custom logger to DicomService based classes

#### v1.0.17 (09/12/2012)
* Check overlay group before attempting to load overlay data
* Add ability to copy value column to clipboard in DICOM Dump
* Fix DicomClient linger timeout and add release timeout

#### v1.0.16 (09/11/2012)
* Fix decompression of JPEG Baseline Process 1 images
* Fix conversion of YBR to RGB in JPEG compressed images
* Add ability to handle encapsulated OW pixel data
* Better handling of grayscale images without Window/Level in dataset

#### v1.0.15 (09/06/2012)
* Add ability to store user state object in DicomService based classes
* Add ability to store user state object in DicomClient
* Fix handling of UIDs in DicomCFindRequest
* Fix comparison of private DicomTags
* Add shortcut constructor for private DicomTags
* Handle null DicomDateRange in DicomDataset.Add()
* Modality Worklist C-Find helper method

#### v1.0.14 (09/05/2012)
* Fix bug in DicomDatasetReaderObserver handling zero length Specific Character Set elements
* Fix bug in DICOM Dump when displaying zero length UIDs
* Load implementation version from assembly info

#### v1.0.13 (09/04/2012)
* Add ability for library to create and manage temp files
* User state object for DIMSE requests and responses
* Fix reading of elements with unknown dictionary VR (Justin Wake)
* Fix handling of UIDs in DicomCMoveRequest (Justin Wake)
* Add version to file meta information
* Add support for multiframe images in DicomImage

#### v1.0.12 (08/27/2012)
* Add private dictionary to assembly
* Fix parsing errors when reading private dictionaries
* Fix reading of private tags
* Miscellaneous enhancements

#### v1.0.11 (08/23/2012)
* Accept unknown transfer syntaxes
* Add ability to write DICOM dataset to string

#### v1.0.10 (08/13/2012)
* Fix bug preloading dictionary from another assembly (Mahesh Dubey)
* Add name of UID to DICOM Dump elements
* Better error handling in GetDateTime method
* Persistent temporary file remover

#### v1.0.9 (08/02/2012)
* Parsing of Attribute Tag element type
* Fix bug displaying compressed images
* Add codec libraries as references to DICOM Dump utility
* Fix bug writing private sequence lengths
* Fix stack overflow when reading datasets with lots of sequences
* Fix big endian pixel data being swapped twice

#### v1.0.7 (07/26/2012)
* Fix exception when accessing overlay data
* Fix parsing of multi-value string elements
* Add option to display image in DICOM Dump utility
* Fix C-Store request from Conquest causing exception

#### v1.0.6 (07/24/2012)
* Don't throw exception for invalid UID characters
* Allow casting of OB & UN elements to value types

#### v1.0.5 (07/22/2012)
* Bug fixes
* DICOM Dump example project

#### v1.0.4 (07/18/2012)
* Make logger instance protected in DicomService
* Lock DICOM dictionary while loading
* Throw exception if no DICOM dictionary entry is found while adding element to dataset
* Fix bug where status is not being set in DIMSE response
* Fix bug in C-Store SCP where file is inaccessible
* Add C-Store SCP example project

#### v1.0.3 (07/11/2012)
* Fix parsing of explicit length sequences

#### v1.0.2 (07/07/2012)
* Image rendering
* Don't create offset table for datasets over 4Gb
* Regenerate dictionary and tags to include grouped elements such as overlays
* Fix bug in ByteBufferEnumerator

#### v1.0.1 (06/22/2012)
* Initial public release
