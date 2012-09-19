#### v1.0.22
* Add Offending Element tags to C-Move response output
* Add exception handling for C-Store requests with unparsable datasets
* Handle ObjectDisposedExceptions in network operations
* Fix transcoding between uncompressed transfer syntaxes
* Don't parse values for IS and DS elements if returning string types
* Add AMICAS private tags (AMICAS0) to private dictionary
* Add support for 32-bit pixel data (Anders Gustafsson, Cureos AB)
* Better handling of default item in Get<> method (Anders Gustafsson, Cureos AB)
* Support opening DicomFile from Stream (Anders Gustafsson, Cureos AB)

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