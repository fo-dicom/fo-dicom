## GetValue-Methods in DicomDataset

In *fo-dicom* there is a API with various options to get data from
DicomDataset in a typed way. 

* `public T GetDicomItem<T>(DicomTag tag) where T:DicomItem`

  Gets the DicomItem of the specified tag.   
  *param T:* Type of the return value. Must inherit from DicomItem.
  *returns:* Item corresponding to tag or `null` if the tag is not contained in
  the instance.
  
  
* `public DicomSequence GetSequence(DicomTag tag)`

  Gets the sequence of the specified tag.
  *param tag:* Requested DICOM tag.
  *returns:* Sequence of datasets corresponding to tag
  *exception DicomDataException:* If the dataset does not contain tag or this
  is not a sequence


* `public bool TryGetSequence(DicomTag tag, out DicomSequence sequence)`

  Gets the sequence of the specifiedtag.
  *param tag:* Requested DICOM tag.
  *param sequence:* Sequence of datasets corresponding to tag.
  *returns:* Returns true if the tag could be returned as sequence, false
  otherwise.


* `public int GetValueCount(DicomTag tag)`

  Returns the number of values in the specified tag.
  *param tag:* Requested DICOM tag.
  *exception DicomDataException:* If the dataset does not contain tag.


* `public T GetValue<T>(DicomTag tag, int index)`

  Gets the index-th element value of the specified tag.
  *type T:* Type of the return value. This cannot be an array type.
  *param tag:* Requested DICOM tag.
  *param index:* Item index (for multi-valued elements).
  *returns:* Element value corresponding to tag.    
  *exception DicomDataException:* If the dataset does not contain tag or if the
  specified /// <paramref name="index">item index</paramref> is out-of-range.


* `public bool TryGetValue<T>(DicomTag tag, int index, out T elementValue)`

  Tries to get the index-th element value of the specified tag.
  *type T:* Type of the return value. This cannot be an array type.    
  *param tag:* Requested DICOM tag.    
  *param index:* Item index (for multi-valued elements).    
  *param elementValue:* Element value corresponding to tag.    
  *returns:* Returns `true` if the element value could be exctracted,
  otherwise `false`.


* `public T GetValueOrDefault<T>(DicomTag tag, int index, T defaultValue)`

  Gets the index-th element value of the specified tag or the provided
  defaultValue if the requested value is not contained in the dataset.
  *type T:* Type of the return value. This cannot be an array type.    
  *param tag:* Requested DICOM tag.    
  *param index:* Item index (for multi-valued elements).    
  *param defaultValue:* Value that is returned if the requested element value
  does not exist.


* `public T[] GetValues<T>(DicomTag tag)`

  Gets the array of element values of the specified tag.
  *type T:* Type of the return value. This cannot be an array type.    
  *param tag:* Requested DICOM tag.    
  *returns:* Element values corresponding to tag.    
  *exception DicomDataException:* If the dataset does not contain tag.


* `public bool TryGetValues<T>(DicomTag tag, out T[] values)`

  Tries to get the array of element values of the specified tag.
  *type T:* Type of the return value. This cannot be an array type.    
  *param tag:* Requested DICOM tag.    
  *param elementValue">Element values corresponding to tag.    
  *returns:* Returns `true` if the element values could be exctracted,
  otherwise `false`.


* `public T GetSingleValue<T>(DicomTag tag)`

  Gets the element value of the specified tag, whose value multiplicity has to be 1.
  *type T:* Type of the return value. This cannot be an array type.    
  *param tag:* Requested DICOM tag.    
  *returns:* Element values corresponding to tag.    
  *exception DicomDataException:* If the dataset does not contain tag, is empty or is multi-valued.


* `public bool TryGetSingleValue<T>(DicomTag tag, out T value)`

  Tries to get the element value of the specified tag, whose value multiplicity has to be 1.
  *type T:* Type of the return value. This cannot be an array type.    
  *param tag:* Requested DICOM tag.    
  *param elementValue:* Element value corresponding to tag.    
  *returns:* Returns `true` if the element values could be exctracted, otherwise `false`.


* `public T GetSingleValueOrDefault<T>(DicomTag tag, T defaultValue)`

  Gets the element value of the specified tag, whose value multiplicity has to
  be 1, or the provided defaultValue if the element value does not exist.
  *type T:* Type of the return value. This cannot be an array type.    
  *param tag:* Requested DICOM tag.    
  *param defaultValue:* Value that is returned if the requested element value
  does not exist.
  

* `public string GetString(DicomTag tag)`

  Gets a string representation of the value of the specified tag.  
  *param tag:* Requested DICOM tag.    
  *returns:* String representing the element value corresponding to tag.    
  *exception DicomDataException:* If the dataset does not contain tag.


* `public bool TryGetString(DicomTag tag, out string stringValue)`

  Tries to get a string representation of the value of the specified tag.
  *param tag:* Requested DICOM tag.    
  *param stringValue:* String representing the element value corresponding to
  tag.    
  *returns:* Returns `false` if the dataset does not contain the tag.    
