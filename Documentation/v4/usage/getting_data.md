## new GetValue-Methods in DicomDataset

Since **fo-dicom version 4.0.0** there is a new API to get data from DicomDataset

<dl>

<dt>
public T GetDicomItem<T>(DicomTag tag) where T:DicomItem
</dt>
<dd>
Gets the DicomItem of the specified tag.   

*param T:* Type of the return value. Must inherit from DicomItem.   
*returns:* Item corresponding to tag or `null` if the tag is not contained in the instance.
</dd>


<dt>
public DicomSequence GetSequence(DicomTag tag)
</dt>
<dd>
Gets the sequence of the specified tag.

*param tag:* Requested DICOM tag.   
*returns:* Sequence of datasets corresponding to tag   
*exception DicomDataException:* If the dataset does not contain tag or this is not a sequence
</dd>
        
<dt>
public bool TryGetSequence(DicomTag tag, out DicomSequence sequence)
</dt>
<dd>
Gets the sequence of the specifiedtag.

*param tag:* Requested DICOM tag.
*param sequence:* Sequence of datasets corresponding to tag.    
*returns:* Returns true if the tag could be returned as sequence, false otherwise.
</dd>

<dt>
public int GetValueCount(DicomTag tag)
</dt>
<dd>
Returns the number of values in the specified tag.

*param tag:* Requested DICOM tag.    
*exception DicomDataException:* If the dataset does not contain tag.    
</dd>


<dt>
public T GetValue<T>(DicomTag tag, int index)
</dt>
<dd>
Gets the index-th element value of the specified tag.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*param index:* Item index (for multi-valued elements).    
*returns:* Element value corresponding to tag.    
*exception DicomDataException:* If the dataset does not contain tag or if the specified
        /// <paramref name="index">item index</paramref> is out-of-range.    
</dd>


<dt>
public bool TryGetValue<T>(DicomTag tag, int index, out T elementValue)
</dt>
<dd>
Tries to get the index-th element value of the specified tag.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*param index:* Item index (for multi-valued elements).    
*param elementValue:* Element value corresponding to tag.    
*returns:* Returns `true` if the element value could be exctracted, otherwise `false`.    
</dd>


<dt>
public T GetValueOrDefault<T>(DicomTag tag, int index, T defaultValue)
</dt>
<dd>
Gets the index-th element value of the specified tag or the provided defaultValue if the requested value is not contained in the dataset.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*param index:* Item index (for multi-valued elements).    
*param defaultValue:* Value that is returned if the requested element value does not exist.    
</dd>


<dt>
public T[] GetValues<T>(DicomTag tag)
</dt>
<dd>
Gets the array of element values of the specified tag.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*returns:* Element values corresponding to tag.    
*exception DicomDataException:* If the dataset does not contain tag.    
</dd>


<dt>
public bool TryGetValues<T>(DicomTag tag, out T[] values)
</dt>
<dd>
Tries to get the array of element values of the specified tag.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*param elementValue">Element values corresponding to tag.    
*returns:* Returns `true` if the element values could be exctracted, otherwise `false`.    
</dd>


<dt>
public T GetSingleValue<T>(DicomTag tag)
</dt>
<dd>
Gets the element value of the specified tag, whose value multiplicity has to be 1.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*returns:* Element values corresponding to tag.    
*exception DicomDataException:* If the dataset does not contain tag, is empty or is multi-valued.    
</dd>


<dt>
public bool TryGetSingleValue<T>(DicomTag tag, out T value)
</dt>
<dd>
Tries to get the element value of the specified tag, whose value multiplicity has to be 1.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*param elementValue:* Element value corresponding to tag.    
*returns:* Returns `true` if the element values could be exctracted, otherwise `false`.    
</dd>


<dt>
public T GetSingleValueOrDefault<T>(DicomTag tag, T defaultValue)
</dt>
<dd>
Gets the element value of the specified tag, whose value multiplicity has to be 1, or the provided defaultValue if the element value does not exist.

*type T:* Type of the return value. This cannot be an array type.    
*param tag:* Requested DICOM tag.    
*param defaultValue:* Value that is returned if the requested element value does not exist.    
</dd>


<dt>
public string GetString(DicomTag tag)
</dt>
<dd>
Gets a string representation of the value of the specified tag.

*param tag:* Requested DICOM tag.    
*returns:* String representing the element value corresponding to tag.    
*exception DicomDataException:* If the dataset does not contain tag.    
</dd>


<dt>
public bool TryGetString(DicomTag tag, out string stringValue)
</dt>
<dd>
Tries to get a string representation of the value of the specified tag.

*param tag:* Requested DICOM tag.    
*param stringValue:* String representing the element value corresponding to tag.    
*returns:* Returns `false` if the dataset does not contain the tag.    
</dd>

</dl>

## old Get-Methods in DicomDataset

For previous versions up to **fo-dicom 3.0.2** use the following methods:

    // Given these prerequisites, DicomDataset.Get<T> yields results as follows:
    using static Dicom.DicomTag;
    var ds = DicomFile.Open(fileName).Dataset;

**CS, Code String**

Call <= 3.0.2 | Call >= 4.0.0 | Result 
--- | --- | ---
`ds.Get<string>(ImageType, -1)` | `ds.GetString(ImageType)` | "ORIGINAL\PRIMARY\AXIAL"
`ds.Get<string>(ImageType)`|   | "ORIGINAL"
`ds.Get<string>(ImageType, 0)`| `ds.GetValue<string>(ImageType, 0)` | "ORIGINAL"
`ds.Get<string>(ImageType, 2)`| `ds.GetValue<string>(ImageType, 2)` | "AXIAL"
`ds.Get<string[]>(ImageType)`| `ds.GetValues<string>(ImageType)` | { "ORIGINAL", "PRIMARY", "AXIAL" }
`ds.Get<DicomCodeString>(ImageType)`| `ds.GetDicomItem<DicomCodeString>(ImageType)` | `DicomCodeString` [ "ORIGINAL", "PRIMARY", "AXIAL" ]

**DA, Date**

Call <= 3.0.2 | Call >= 4.0.0 | Result 
--- | --- | ---
`ds.Get<string>(StudyDate)` | `ds.GetSingleValue<string>(StudyDate)` | "19990509"
`ds.Get<DateTime>(StudyDate)` | `ds.GetSingleValue<DateTime>(StudyDate)` | `DateTime` May 9, 1999
`ds.Get<DicomDate>(StudyDate)` | `ds.GetDicomItem<DicomDate>(StudyDate)` | `DicomDate` May 9, 1999

**DS, Decimal String**

Call <= 3.0.2 | Call >= 4.0.0 | Result 
--- | --- | ---
`ds.Get<string>(ImagePositionPatient, -1)` | `ds.GetString(ImagePositionPatient)` | "0.5\\-3.5\4.25"
`ds.Get<float>(ImagePositionPatient)`|   | 0.5
`ds.Get<float>(ImagePositionPatient, 0)`| `ds.GetValue<float>(ImagePositionPatient, 0)` | 0.5
`ds.Get<double>(ImagePositionPatient, 2)`| `ds.GetValue<double>(ImagePositionPatient, 2)` | 4.25
`ds.Get<decimal[]>(ImagePositionPatient)`| `ds.GetValues<decimal>(ImagePositionPatient)` | { 0.5, -3.5, 4.25 }
`ds.Get<DicomDecimalString>(ImagePositionPatient)`| `ds.GetDicomItem<DicomDecimalString>(ImagePositionPatient)` | `DicomDecimalString` [ 0.5, -3.5, 4.25 ]

**SS, SignedShort**

Call <= 3.0.2 | Call >= 4.0.0 | Result 
--- | --- | ---
`ds.Get<short[]>(OverlayOrigin)` | `ds.GetValues<short>(OverlayOrigin)` | { 1, -2 }
`ds.Get<short>(OverlayOrigin)` |   | 1
`ds.Get<short>(OverlayOrigin, 0)` | `ds.GetValue<short>(OverlayOrigin, 0)` | 1
`ds.Get<long>(OverlayOrigin, 1)` | `ds.GetValue<long>(OverlayOrigin, 1)` | -2
`ds.Get<string>(OverlayOrigin, 1)` | `ds.GetValue<string>(OverlayOrigin, 1)` | "-2"
`ds.Get<string[]>(OverlayOrigin)` | `ds.GetValues<string>(OverlayOrigin)` | { "1", "-2" }
`ds.Get<DicomSignedShort>(OverlayOrigin)` | `ds.GetDicomItem<DicomSignedShort>(OverlayOrigin)` | `DicomSignedShort` [ 1, -2 ]

**TM, Time**

Call <= 3.0.2 | Call >= 4.0.0 | Result 
--- | --- | ---
`ds.Get<string>(StudyTime)` | `ds.GetString(StudyTime)` | "105234.530000"
`ds.Get<DateTime>(StudyTime)` | `ds.GetSingleValue<DateTime>(StudyTime)` | `DateTime` 10:52:34.53
`ds.Get<DicomTime>(StudyTime)` | `ds.GetDicomItem<DicomTime>(StudyTime)` | `DicomTime` 10:52:34.53
