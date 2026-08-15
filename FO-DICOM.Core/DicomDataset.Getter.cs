// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.StructuredReport;
using System;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom
{
    public partial class DicomDataset
    {



        /// <summary>
        /// Gets the <see cref="DicomItem"/> of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. Must inherit from <see cref="DicomItem"/>.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Item corresponding to <paramref name="tag"/> or <code>null</code> if the <paramref name="tag"/> is not contained in the instance.</returns>
        public T GetDicomItem<T>(DicomTag tag) where T : DicomItem
        {
            return (TryValidatePrivate(ref tag) && _items.TryGetValue(tag, out DicomItem dummyItem))
              ? dummyItem as T
              : null;
        }


        /// <summary>
        /// Gets the sequence of the specified <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Sequence of datasets corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/> or this is not a sequence.</exception>
        public DicomSequence GetSequence(DicomTag tag)
        {
            tag = ValidatePrivate(tag);
            if (_items.TryGetValue(tag, out DicomItem item))
            {
                if (item is DicomSequence sequence)
                {
                    return sequence;
                }
                else
                {
                    throw new DicomDataException($"DicomTag {tag} isn't a sequence.");
                }
            }
            else
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        public DicomCodeItem GetCodeItem(DicomTag tag)
        {
            tag = ValidatePrivate(tag);
            if (_items.TryGetValue(tag, out DicomItem item))
            {
                if (item is DicomSequence sequence)
                {
                    return new DicomCodeItem(sequence);
                }
                else
                {
                    throw new DicomDataException($"DicomTag {tag} isn't a sequence.");
                }
            }
            else
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        public DicomMeasuredValue GetMeasuredValue(DicomTag tag)
        {
            tag = ValidatePrivate(tag);
            if (_items.TryGetValue(tag, out DicomItem item))
            {
                if (item is DicomSequence sequence)
                {
                    return new DicomMeasuredValue(sequence);
                }
                else
                {
                    throw new DicomDataException($"DicomTag {tag} isn't a sequence.");
                }
            }
            else
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        public DicomReferencedSOP GetReferencedSOP(DicomTag tag)
        {
            tag = ValidatePrivate(tag);
            if (_items.TryGetValue(tag, out DicomItem item))
            {
                if (item is DicomSequence sequence)
                {
                    return new DicomReferencedSOP(sequence);
                }
                else
                {
                    throw new DicomDataException($"DicomTag {tag} isn't a sequence.");
                }
            }
            else
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        /// <summary>
        /// Gets the sequence of the specified <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="sequence">Sequence of datasets corresponding to <paramref name="tag"/>.</param>
        /// <returns>Returns <code>true</code> if the <paramref name="tag"/> could be returned as sequence, <code>false</code> otherwise.</returns>
        public bool TryGetSequence(DicomTag tag, out DicomSequence sequence)
        {
            if (!TryValidatePrivate(ref tag))
            {
                sequence = null;
                return false;
            }
            if (_items.TryGetValue(tag, out DicomItem item) && item is DicomSequence dummySequence)
            {
                sequence = dummySequence;
                return true;
            }
            else
            {
                sequence = null;
                return false;
            }
        }


        /// <summary>
        /// Gets the sequence of the specified <paramref name="tag"/> if it exists and is not empty.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="sequence">Sequence of datasets corresponding to <paramref name="tag"/>.</param>
        /// <returns>Returns <code>true</code> if the <paramref name="tag"/> exists and is not empty, <code>false</code> otherwise.</returns>
        public bool TryGetNonEmptySequence(DicomTag tag, out DicomSequence sequence)
        {
            if (TryGetSequence(tag, out DicomSequence dicomSequence) && dicomSequence.Items.Count > 0)
            {
                sequence = dicomSequence;
                return true;
            }

            sequence = null;
            return false;
        }


        /// <summary>        
        /// Returns the number of values in the specified <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/>.</exception>
        public int GetValueCount(DicomTag tag)
        {
            tag = ValidatePrivate(tag);
            ValidateDicomTag(tag, out DicomItem item);

            if (item is DicomElement element)
            {
                return element.Count;
            }
            else if (item is DicomSequence sequence)
            {
                return sequence.Items.Count;
            }
            else
            {
                //Are there any other cases where this method can be called for non DicomElement types?
                throw new DicomDataException("DicomTag doesn't support values.");
            }
        }


        /// <summary>
        /// Gets the <paramref name="index"/>-th element value of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="index">Item index (for multi-valued elements).</param>
        /// <returns>Element value corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/> or if the specified
        /// <paramref name="index">item index</paramref> is out-of-range.</exception>
        public T GetValue<T>(DicomTag tag, int index)
        {
            tag = ValidatePrivate(tag);
            if (index < 0) { throw new ArgumentOutOfRangeException(nameof(index), "index must be a non-negative value"); }
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException("T can't be an Array type. Use GetValues instead"); }

            ValidateDicomTag(tag, out DicomItem item);

            if (item is DicomElement element)
            {
                if (typeof(IByteBuffer).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())) { return (T)(object)element.Buffer; }

                if (index >= element.Count)
                {
                    throw new DicomDataException($"Index out of range: index {index} for Tag {tag} must be less than value count {element.Count}");
                }
                else
                {
                    return element.Get<T>(index);
                }
            }
            else
            {
                throw new DicomDataException("DicomTag doesn't support values.");
            }
        }


        /// <summary>
        /// Tries to get the <paramref name="index"/>-th element value of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="index">Item index (for multi-valued elements).</param>
        /// <param name="elementValue">Element value corresponding to <paramref name="tag"/>.</param>
        /// <returns>Returns <code>true</code> if the element value could be exctracted, otherwise <code>false</code>.</returns>
        public bool TryGetValue<T>(DicomTag tag, int index, out T elementValue)
        {
            if (index < 0 || typeof(T).GetTypeInfo().IsArray)
            {
                elementValue = default(T);
                return false;
            }

            if (!TryValidatePrivate(ref tag))
            {
                elementValue = default(T);
                return false;
            }
            if (!_items.TryGetValue(tag, out DicomItem item))
            {
                elementValue = default(T);
                return false;
            }

            if (item is DicomElement element && index < element.Count)
            {
                try
                {
                    elementValue = element.Get<T>(index);
                    return true;
                }
                catch
                {
                    elementValue = default(T);
                    return false;
                }
            }
            else
            {
                elementValue = default(T);
                return false;
            }
        }


        /// <summary>
        /// Gets the <paramref name="index"/>-th element value of the specified <paramref name="tag"/> or the provided <paramref name="defaultValue"/> if the requested value is not contained in the dataset.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="index">Item index (for multi-valued elements).</param>
        /// <param name="defaultValue">Value that is returned if the requested element value does not exist.</param>
        public T GetValueOrDefault<T>(DicomTag tag, int index, T defaultValue)
        {
            return TryGetValue<T>(tag, index, out T dummy) ? dummy : defaultValue;
        }


        /// <summary>
        /// Gets the array of element values of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Element values corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/>.</exception>
        public T[] GetValues<T>(DicomTag tag)
        {
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException("T can't be an Array type."); }

            tag = ValidatePrivate(tag);
            ValidateDicomTag(tag, out DicomItem item);

            if (item is DicomElement element)
            {
                if (typeof(T[]) == typeof(byte[])) { return (T[])(object)element.Buffer.Data; }

                return element.Get<T[]>(-1);
            }
            else
            {
                throw new DicomDataException("DicomTag doesn't support values.");
            }
        }


        /// <summary>
        /// Tries to get the array of element values of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="values">Element values corresponding to <paramref name="tag"/>.</param>
        /// <returns>Returns <code>true</code> if the element values could be extracted, otherwise <code>false</code>.</returns>
        public bool TryGetValues<T>(DicomTag tag, out T[] values)
        {
            if (typeof(T).GetTypeInfo().IsArray)
            {
                values = null;
                return false;
            }

            if (!TryValidatePrivate(ref tag))
            {
                values = null;
                return false;
            }
            if (!_items.TryGetValue(tag, out DicomItem item))
            {
                values = null;
                return false;
            }

            if (item is DicomElement element)
            {
                try
                {
                    values = element.Get<T[]>(-1);
                    return true;
                }
                catch
                {
                    values = null;
                    return false;
                }
            }
            else
            {
                values = null;
                return false;
            }
        }


        /// <summary>
        /// Gets the element value of the specified <paramref name="tag"/>, whose value multiplicity has to be 1.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Element values corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/>, is empty or is multi-valued.</exception>
        public T GetSingleValue<T>(DicomTag tag)
        {
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException("T can't be an Array type. Use GetValues instead"); }

            tag = ValidatePrivate(tag);
            ValidateDicomTag(tag, out DicomItem item);

            if (item is DicomElement element)
            {
                if (typeof(IByteBuffer).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())) { return (T)(object)element.Buffer; }

                if (element.Count != 1) { throw new DicomDataException($"DICOM element {tag} must contain a single value, but contains {element.Count}"); }

                return element.Get<T>(0);
            }
            else
            {
                throw new DicomDataException("DicomTag doesn't support values.");
            }
        }



        /// <summary>
        /// Tries to get the element value of the specified <paramref name="tag"/>, whose value multiplicity has to be 1.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="elementValue">Element value corresponding to <paramref name="tag"/>.</param>
        /// <returns>Returns <code>true</code> if the element values could be exctracted, otherwise <code>false</code>.</returns>
        public bool TryGetSingleValue<T>(DicomTag tag, out T value)
        {
            if (typeof(T).GetTypeInfo().IsArray)
            {
                value = default(T);
                return false;
            }

            if (!TryValidatePrivate(ref tag))
            {
                value = default(T);
                return false;
            }
            if (!_items.TryGetValue(tag, out DicomItem item))
            {
                value = default(T);
                return false;
            }

            if (item is DicomElement element && element.Count == 1)
            {
                try
                {
                    value = element.Get<T>(0);
                    return true;
                }
                catch
                {
                    value = default(T);
                    return false;
                }
            }
            else
            {
                value = default(T);
                return false;
            }
        }


        /// <summary>
        /// Gets the element value of the specified <paramref name="tag"/>, whose value multiplicity has to be 1, or the provided <paramref name="defaultValue"/> if the element value does not exist.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="defaultValue">Value that is returned if the requested element value does not exist.</param>
        public T GetSingleValueOrDefault<T>(DicomTag tag, T defaultValue)
        {
            return TryGetSingleValue<T>(tag, out T dummy) ? dummy : defaultValue;
        }


        /// <summary>
        /// Gets a string representation of the value of the specified <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>String representing the element value corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/>.</exception>
        public string GetString(DicomTag tag)
        {
            tag = ValidatePrivate(tag);
            ValidateDicomTag(tag, out DicomItem item);

            if (item is DicomElement element)
            {
                return element.Get<string>(-1);
            }
            else
            {
                throw new DicomDataException("DicomTag doesn't support values.");
            }
        }


        /// <summary>
        /// Tries to get a string representation of the value of the specified <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="stringValue">String representing the element value corresponding to <paramref name="tag"/>.</param>
        /// <returns>Returns <code>false</code> if the dataset does not contain the tag.</returns>
        public bool TryGetString(DicomTag tag, out string stringValue)
        {
            if (!TryValidatePrivate(ref tag))
            {
                stringValue = null;
                return false;
            }
            if (!_items.TryGetValue(tag, out DicomItem item))
            {
                stringValue = null;
                return false;
            }

            if (item is DicomElement element)
            {
                try
                {
                    stringValue =
                        element.Count == 0
                        ? string.Empty
                        : element.Get<string>(-1);
                    return true;
                }
                catch (DicomDataException)
                {
                    stringValue = null;
                    return false;
                }
            }
            else
            {
                stringValue = null;
                return false;
            }
        }

        private DicomTag ValidatePrivate(DicomTag tag)
        {
            if (TryValidatePrivate(ref tag))
            {
                return tag;
            }
            else
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        private bool TryValidatePrivate(ref DicomTag tag)
        {
            if (tag.IsPrivate)
            {
                var privateTag = GetPrivateTag(tag, false);
                if (privateTag == null)
                {
                    return false;
                }
                tag = privateTag;
            }
            return true;
        }


        private void ValidateDicomTag(DicomTag tag, out DicomItem item)
        {
            if (!_items.TryGetValue(tag, out item))
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        /// <summary>
        /// Converts a dictionary tag to a valid private tag. Creates the private creator tag if needed.
        /// </summary>
        /// <param name="tag">Dictionary DICOM tag</param>
        /// <returns>Private DICOM tag, or null if all groups are already used.</returns>
        public DicomTag GetPrivateTag(DicomTag tag)
        {
            return GetPrivateTag(tag, true);
        }

        /// <summary>
        /// Converts a dictionary tag to a valid private tag.
        /// </summary>
        /// <param name="tag">Dictionary DICOM tag</param>
        /// <param name="createTag">Whether the PrivateCreator tag should be created if needed.</param>
        /// <returns>Private DICOM tag, or null if all groups are already used or createTag is false and the
        /// PrivateCreator is not already in the dataset. </returns>
        internal DicomTag GetPrivateTag(DicomTag tag, bool createTag)
        {
            // not a private tag
            if (!tag.IsPrivate) return tag;

            // group length
            if (tag.Element == 0x0000) return tag;

            // private creator?
            if (tag.PrivateCreator == null) return tag;

            // already a valid private tag
            if (tag.Element > 0xff) return tag;

            ushort group = 0x0010;
            for (; group <= 0x00ff; group++)
            {
                var creator = new DicomTag(tag.Group, group);
                if (!Contains(creator))
                {
                    if (!createTag) continue;

                    Add(new DicomLongString(creator, tag.PrivateCreator.Creator));
                    return new DicomTag(tag.Group, (ushort)((group << 8) + (tag.Element & 0xff)), tag.PrivateCreator);
                }

                var value = TryGetSingleValue(creator, out string tmpValue) ? tmpValue : string.Empty;
                if (tag.PrivateCreator.Creator == value) return new DicomTag(tag.Group, (ushort)((group << 8) + (tag.Element & 0xff)), tag.PrivateCreator);
            }

            return null;
        }


        /// <summary>
        /// Checks the DICOM dataset to determine if the dataset already contains an item with the specified tag.
        /// </summary>
        /// <param name="tag">DICOM tag to test</param>
        /// <returns><c>True</c> if a DICOM item with the specified tag already exists.</returns>
        public bool Contains(DicomTag tag)
        {
            if (tag.IsPrivate)
            {
                var privateTag = GetPrivateTag(tag, false);
                return (privateTag != null) && _items.Any(kv => kv.Key.Equals(privateTag));
            }
            return _items.ContainsKey(tag);
        }


        /// <summary>
        /// Returns a DicomDataset, that contains all Tags accumulated from the Shared Functional Group Sequence and the Per-Frame Functional Group Sequence.
        /// </summary>
        /// <param name="frame">Zero-based frame index.</param>
        /// <returns></returns>
        /// <exception cref="DicomDataException"></exception>
        public DicomDataset FunctionalGroupValues(int frame)
        {
            // Validation should be disabled, because we will be copying data over from another dataset.
            // that means, these values are either loaded from a source, or they have already been
            // validated.
            var functionalDs = new DicomDataset { ValidateItems = false };

            // gets all items from SharedFunctionalGroups
            if (TryGetSequence(DicomTag.SharedFunctionalGroupsSequence, out var sharedFunctionalGroupsSequence)
                && sharedFunctionalGroupsSequence.Items.Count > 0)
            {
                var sharedFunctionGroupItem = sharedFunctionalGroupsSequence.Items[0] ?? throw new DicomDataException("unexpected empty SharedFunctionalGroupsSequence");
                foreach (var sequence in sharedFunctionGroupItem.OfType<DicomSequence>())
                {
                    if (sequence.Tag == DicomTag.ReferencedImageSequence)
                    {
                        functionalDs.AddOrUpdate(sequence);
                    }
                    else
                    {
                        // skip empty sequences
                        if (sequence.Items.Count <= 0)
                        {
                            continue;
                        }

                        foreach (var item in sequence.Items[0])
                        {
                            functionalDs.AddOrUpdate(item);
                        }
                    }
                }
            }

            // gets the specific items from PerFrameFunctionalGroups for this frame
            if (TryGetSequence(DicomTag.PerFrameFunctionalGroupsSequence, out var perFrameFunctionalGroupsSequence)
                && perFrameFunctionalGroupsSequence.Items.Count > frame)
            {
                var frameFunctionGroupItem = perFrameFunctionalGroupsSequence.Items[frame];
                foreach (var sequence in frameFunctionGroupItem.OfType<DicomSequence>())
                {
                    if (sequence.Tag == DicomTag.ReferencedImageSequence)
                    {
                        functionalDs.AddOrUpdate(sequence);
                    }
                    else
                    {
                        // skip empty sequences
                        if (sequence.Items.Count <= 0)
                        {
                            continue;
                        }

                        foreach (var item in sequence.Items[0])
                        {
                            functionalDs.AddOrUpdate(item);
                        }
                    }
                }

            }

            return functionalDs;
        }



        public DicomApplicationEntity GetElem(DicomTagAE tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomApplicationEntity ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomApplicationEntity GetElem(DicomTagAEs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomApplicationEntity ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomAgeString GetElem(DicomTagAS tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomAgeString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomAgeString GetElem(DicomTagASs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomAgeString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomAttributeTag GetElem(DicomTagAT tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomAttributeTag ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomAttributeTag GetElem(DicomTagATs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomAttributeTag ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomCodeString GetElem(DicomTagCS tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomCodeString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomCodeString GetElem(DicomTagCSs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomCodeString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomDate GetElem(DicomTagDA tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomDate ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomDate GetElem(DicomTagDAs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomDate ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomDecimalString GetElem(DicomTagDS tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomDecimalString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomDecimalString GetElem(DicomTagDSs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomDecimalString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomDateTime GetElem(DicomTagDT tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomDateTime ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomDateTime GetElem(DicomTagDTs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomDateTime ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomFloatingPointDouble GetElem(DicomTagFD tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomFloatingPointDouble ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomFloatingPointDouble GetElem(DicomTagFDs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomFloatingPointDouble ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomFloatingPointSingle GetElem(DicomTagFL tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomFloatingPointSingle ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomFloatingPointSingle GetElem(DicomTagFLs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomFloatingPointSingle ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomIntegerString GetElem(DicomTagIS tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomIntegerString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomIntegerString GetElem(DicomTagISs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomIntegerString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomLongString GetElem(DicomTagLO tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomLongString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomLongString GetElem(DicomTagLOs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomLongString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomLongText GetElem(DicomTagLT tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomLongText ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomOtherByte GetElem(DicomTagOB tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomOtherByte ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomOtherDouble GetElem(DicomTagOD tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomOtherDouble ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomOtherFloat GetElem(DicomTagOF tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomOtherFloat ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomOtherLong GetElem(DicomTagOL tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomOtherLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomOtherVeryLong GetElem(DicomTagOV tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomOtherVeryLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomOtherWord GetElem(DicomTagOW tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomOtherWord ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomPersonName GetElem(DicomTagPN tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomPersonName ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomPersonName GetElem(DicomTagPNs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomPersonName ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomShortString GetElem(DicomTagSH tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomShortString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomShortString GetElem(DicomTagSHs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomShortString ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomSignedLong GetElem(DicomTagSL tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomSignedLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomSignedLong GetElem(DicomTagSLs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomSignedLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomSequence GetElem(DicomTagSQ tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomSequence ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomSequence GetElem(DicomTagSQs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomSequence ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomSignedShort GetElem(DicomTagSS tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomSignedShort ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomSignedShort GetElem(DicomTagSSs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomSignedShort ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomShortText GetElem(DicomTagST tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomShortText ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomShortText GetElem(DicomTagSTs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomShortText ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomSignedVeryLong GetElem(DicomTagSVs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomSignedVeryLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomTime GetElem(DicomTagTM tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomTime ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomTime GetElem(DicomTagTMs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomTime ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnlimitedCharacters GetElem(DicomTagUC tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnlimitedCharacters ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnlimitedCharacters GetElem(DicomTagUCs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnlimitedCharacters ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUniqueIdentifier GetElem(DicomTagUI tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUniqueIdentifier ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUniqueIdentifier GetElem(DicomTagUIs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUniqueIdentifier ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnsignedLong GetElem(DicomTagUL tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnsignedLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnsignedLong GetElem(DicomTagULs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnsignedLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnknown GetElem(DicomTagUN tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnknown ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUniversalResource GetElem(DicomTagUR tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUniversalResource ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUniversalResource GetElem(DicomTagURs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUniversalResource ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnsignedShort GetElem(DicomTagUS tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnsignedShort ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnsignedShort GetElem(DicomTagUSs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnsignedShort ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnlimitedText GetElem(DicomTagUT tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnlimitedText ?? throw new DicomDataException("DicomTag doesn't support values.");
        }

        public DicomUnsignedVeryLong GetElem(DicomTagUVs tagparam)
        {
            var tag = ValidatePrivate(tagparam);
            ValidateDicomTag(tag, out DicomItem item);
            return item as DicomUnsignedVeryLong ?? throw new DicomDataException("DicomTag doesn't support values.");
        }


        public bool TryGetElem(DicomTagAE tagparam, out DicomApplicationEntity element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomApplicationEntity dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagAEs tagparam, out DicomApplicationEntity element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomApplicationEntity dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagAS tagparam, out DicomAgeString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomAgeString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagASs tagparam, out DicomAgeString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomAgeString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagAT tagparam, out DicomAttributeTag element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomAttributeTag dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagATs tagparam, out DicomAttributeTag element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomAttributeTag dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagCS tagparam, out DicomCodeString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomCodeString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagCSs tagparam, out DicomCodeString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomCodeString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagDA tagparam, out DicomDate element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomDate dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagDAs tagparam, out DicomDate element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomDate dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagDS tagparam, out DicomDecimalString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomDecimalString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagDSs tagparam, out DicomDecimalString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomDecimalString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagDT tagparam, out DicomDateTime element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomDateTime dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagDTs tagparam, out DicomDateTime element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomDateTime dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagFD tagparam, out DicomFloatingPointDouble element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomFloatingPointDouble dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagFDs tagparam, out DicomFloatingPointDouble element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomFloatingPointDouble dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagFL tagparam, out DicomFloatingPointSingle element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomFloatingPointSingle dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagFLs tagparam, out DicomFloatingPointSingle element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomFloatingPointSingle dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagIS tagparam, out DicomIntegerString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomIntegerString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagISs tagparam, out DicomIntegerString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomIntegerString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagLO tagparam, out DicomLongString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomLongString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagLOs tagparam, out DicomLongString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomLongString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagLT tagparam, out DicomLongText element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomLongText dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagOB tagparam, out DicomOtherByte element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomOtherByte dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagOD tagparam, out DicomOtherDouble element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomOtherDouble dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagOF tagparam, out DicomOtherFloat element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomOtherFloat dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagOL tagparam, out DicomOtherLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomOtherLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagOV tagparam, out DicomOtherVeryLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomOtherVeryLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagOW tagparam, out DicomOtherWord element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomOtherWord dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagPN tagparam, out DicomPersonName element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomPersonName dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagPNs tagparam, out DicomPersonName element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomPersonName dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSH tagparam, out DicomShortString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomShortString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSHs tagparam, out DicomShortString element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomShortString dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSL tagparam, out DicomSignedLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomSignedLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSLs tagparam, out DicomSignedLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomSignedLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSQ tagparam, out DicomSequence element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomSequence dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSQs tagparam, out DicomSequence element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomSequence dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSS tagparam, out DicomSignedShort element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomSignedShort dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSSs tagparam, out DicomSignedShort element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomSignedShort dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagST tagparam, out DicomShortText element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomShortText dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSTs tagparam, out DicomShortText element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomShortText dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagSVs tagparam, out DicomSignedVeryLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomSignedVeryLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagTM tagparam, out DicomTime element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomTime dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagTMs tagparam, out DicomTime element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomTime dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUC tagparam, out DicomUnlimitedCharacters element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnlimitedCharacters dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUCs tagparam, out DicomUnlimitedCharacters element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnlimitedCharacters dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUI tagparam, out DicomUniqueIdentifier element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUniqueIdentifier dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUIs tagparam, out DicomUniqueIdentifier element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUniqueIdentifier dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUL tagparam, out DicomUnsignedLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnsignedLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagULs tagparam, out DicomUnsignedLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnsignedLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUN tagparam, out DicomUnknown element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnknown dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUR tagparam, out DicomUniversalResource element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUniversalResource dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagURs tagparam, out DicomUniversalResource element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUniversalResource dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUS tagparam, out DicomUnsignedShort element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnsignedShort dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUSs tagparam, out DicomUnsignedShort element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnsignedShort dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUT tagparam, out DicomUnlimitedText element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnlimitedText dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

        public bool TryGetElem(DicomTagUVs tagparam, out DicomUnsignedVeryLong element)
        {
            DicomTag tag = tagparam;
            if (TryValidatePrivate(ref tag)
                && _items.TryGetValue(tagparam, out DicomItem dummyItem)
                && dummyItem is DicomUnsignedVeryLong dummyElement)
            {
                element = dummyElement;
                return true;
            }
            element = null;
            return false;
        }

    }
}
