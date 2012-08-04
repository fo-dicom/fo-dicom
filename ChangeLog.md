#### v1.0.10
* Fix bug preloading dictionary from another assembly

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