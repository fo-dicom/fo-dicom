
# WADO Sample


This is a sample of a partial WADO implementation using Web Api in an Asp.Net Project.
If needed, web api, can be hosted outside of IIS.


## Specification and what's missing

official specification: ftp://medical.nema.org/medical/dicom/2009/09_18pu.pdf

what is not implemented:

* Single Frame Objects (still conform to specification):   
The Server SHOULD also support the following MIME types:
  * image/gif
  * image/png
  * image/jp2
* Multi Frame Objects (still conform to specification):  
The Server SHOULD also support the following MIME types:
  * video/mpeg
  * image/gif
* nothing is implemented in 7.3 TEXT OBJECTS
* nothing is implemented in 7.4 OTHER OBJECTS
* charset is not implemented, but it's conform to specification
* anonymize is not implemented, but it's conform to specification
* many parameters in 8.2 are not implemented :
  * annotation
  * region
  * rows
  * columns
  * frame number
  * windwsCenter
  * windowsWidth
  * imageQuality
  * presentationUID
  * presentationSeriesUID



## How to test it

Launch the project and browse to :

http://localhost:{port}/wado?requestType=WADO&studyUID={studyUID}&seriesUID={serieUID}&objectUID={objectUID}&contentType=image/jpeg

replace {port} with your local port, you can replace {studyUID}, {serieUID} and {objectUID} by any value : it always return the same image in the sample.

## How to use it in your own application
Implement your own IDicomImageFinderService with your database.
replace this line :

```csharp
_dicomImageFinderService = new TestDicomImageFinderService();
```

by 

```csharp
_dicomImageFinderService = new \*your own implementation here\*;
```

in WadoUriController parameterless constructor