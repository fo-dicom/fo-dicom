To add private tags to the default DICOM dictionary in *fo-dicom* so that tag descriptions and value representations show up sufficiently e.g. in *fo-dicom* based viewers, a private tags file can be loaded programmatically.

The private tags file should have the same layout as the [Private Dictionary.xml](https://github.com/fo-dicom/fo-dicom/blob/development/DICOM/Dictionaries/Private%20Dictionary.xml) file in the repository:

    <?xml version="1.0" encoding="UTF-8"?>
    <dictionaries>
      <dictionary creator="PRIVATE CREATOR #1">
        <tag group="3009" element="xx00" vr="UL" vm="1">Private tag #1 description</tag>
        <tag group="3009" element="xx01" vr="US" vm="2">Private tag #1 description</tag>
        ...
        <tag group="3009" element="xx02" vr="DS" vm="1-n">Private tag #N description</tag>
      </dictionary>
      <dictionary creator="PRIVATE CREATOR #2">
        ...
      </dictionary>
      ...
    </dictionaries>

Then, in your code, add the following line to add the private tags to the collection of existing standard and private tags:

    DicomDictionary.Default.Load("path_to_your_private_tags.xml", DicomDictionaryFormat.XML);