// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.StructuredReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom
{

    public static class DicomItemsCollectionExtensions
    {

        /// <summary>
        /// Gets the <see cref="DicomItem"/> of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. Must inherit from <see cref="DicomItem"/>.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Item corresponding to <paramref name="tag"/> or <code>null</code> if the <paramref name="tag"/> is not contained in the instance.</returns>
        public static T GetDicomItem<T>(this IEnumerable<DicomItem> items, DicomTag tag) where T : DicomItem
        {
            tag = items.ValidatePrivate(tag);
            return items.TryGetValue(tag, out DicomItem dummyItem) ? dummyItem as T : null;
        }


        /// <summary>
        /// Gets the sequence of the specified <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Sequence of datasets corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/> or this is not a sequence.</exception>
        public static DicomSequence GetSequence(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            tag = items.ValidatePrivate(tag);
            if (items.TryGetValue(tag, out DicomItem item))
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


        public static DicomCodeItem GetCodeItem(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            tag = items.ValidatePrivate(tag);
            if (items.TryGetValue(tag, out DicomItem item))
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


        public static DicomMeasuredValue GetMeasuredValue(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            tag = items.ValidatePrivate(tag);
            if (items.TryGetValue(tag, out DicomItem item))
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


        public static DicomReferencedSOP GetReferencedSOP(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            tag = items.ValidatePrivate(tag);
            if (items.TryGetValue(tag, out DicomItem item))
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
        public static bool TryGetSequence(this IEnumerable<DicomItem> items, DicomTag tag, out DicomSequence sequence)
        {
            if (!items.TryValidatePrivate(ref tag))
            {
                sequence = null;
                return false;
            }
            if (items.TryGetValue(tag, out DicomItem item) && item is DicomSequence dummySequence)
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
        public static bool TryGetNonEmptySequence(this IEnumerable<DicomItem> items, DicomTag tag, out DicomSequence sequence)
        {
            if (items.TryGetSequence(tag, out DicomSequence dicomSequence) && dicomSequence.Items.Count > 0)
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
        public static int GetValueCount(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            tag = items.ValidatePrivate(tag);
            items.ValidateDicomTag(tag, out DicomItem item);

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
        public static T GetValue<T>(this IEnumerable<DicomItem> items, DicomTag tag, int index)
        {
            tag = items.ValidatePrivate(tag);
            if (index < 0) { throw new ArgumentOutOfRangeException(nameof(index), "index must be a non-negative value"); }
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException("T can't be an Array type. Use GetValues instead"); }

            items.ValidateDicomTag(tag, out DicomItem item);

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
        public static bool TryGetValue<T>(this IEnumerable<DicomItem> items, DicomTag tag, int index, out T elementValue)
        {
            if (index < 0 || typeof(T).GetTypeInfo().IsArray)
            {
                elementValue = default(T);
                return false;
            }

            if (!items.TryValidatePrivate(ref tag))
            {
                elementValue = default(T);
                return false;
            }
            if (!items.TryGetValue(tag, out DicomItem item))
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
        public static T GetValueOrDefault<T>(this IEnumerable<DicomItem> items, DicomTag tag, int index, T defaultValue)
        {
            return items.TryGetValue<T>(tag, index, out T dummy) ? dummy : defaultValue;
        }


        /// <summary>
        /// Gets the array of element values of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. This cannot be an array type.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Element values corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/>.</exception>
        public static T[] GetValues<T>(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException("T can't be an Array type."); }

            tag = items.ValidatePrivate(tag);
            items.ValidateDicomTag(tag, out DicomItem item);

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
        public static bool TryGetValues<T>(this IEnumerable<DicomItem> items, DicomTag tag, out T[] values)
        {
            if (typeof(T).GetTypeInfo().IsArray)
            {
                values = null;
                return false;
            }

            if (!items.TryValidatePrivate(ref tag))
            {
                values = null;
                return false;
            }
            if (!items.TryGetValue(tag, out DicomItem item))
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
        public static T GetSingleValue<T>(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException("T can't be an Array type. Use GetValues instead"); }

            tag = items.ValidatePrivate(tag);
            items.ValidateDicomTag(tag, out DicomItem item);

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
        public static bool TryGetSingleValue<T>(this IEnumerable<DicomItem> items, DicomTag tag, out T value)
        {
            if (typeof(T).GetTypeInfo().IsArray)
            {
                value = default(T);
                return false;
            }

            if (!items.TryValidatePrivate(ref tag))
            {
                value = default(T);
                return false;
            }
            if (!items.TryGetValue(tag, out DicomItem item))
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
        public static T GetSingleValueOrDefault<T>(this IEnumerable<DicomItem> items, DicomTag tag, T defaultValue)
        {
            return items.TryGetSingleValue<T>(tag, out T dummy) ? dummy : defaultValue;
        }


        /// <summary>
        /// Gets a string representation of the value of the specified <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>String representing the element value corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/>.</exception>
        public static string GetString(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            tag = items.ValidatePrivate(tag);
            items.ValidateDicomTag(tag, out DicomItem item);

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
        public static bool TryGetString(this IEnumerable<DicomItem> items, DicomTag tag, out string stringValue)
        {
            if (!items.TryValidatePrivate(ref tag))
            {
                stringValue = null;
                return false;
            }
            if (!items.TryGetValue(tag, out DicomItem item))
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


        private static bool TryValidatePrivate(this IEnumerable<DicomItem> items, ref DicomTag tag)
        {
            if (tag.IsPrivate)
            {
                var privateTag = items.GetPrivateTag(tag, false);
                if (privateTag == null)
                {
                    return false;
                }
                tag = privateTag;
            }
            return true;
        }


        /// <summary>
        /// Converts a dictionary tag to a valid private tag.
        /// </summary>
        /// <param name="tag">Dictionary DICOM tag</param>
        /// <param name="createTag">Whether the PrivateCreator tag should be created if needed.</param>
        /// <returns>Private DICOM tag, or null if all groups are already used or createTag is false and the
        /// PrivateCreator is not already in the dataset. </returns>
        internal static DicomTag GetPrivateTag(this IEnumerable<DicomItem> items, DicomTag tag, bool createTag)
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
                if (!items.Contains(creator))
                {
                    if (!createTag) continue;

                    if (items is DicomDataset dataset)
                    {
                        dataset.Add(new DicomLongString(creator, tag.PrivateCreator.Creator));
                    }
                    return new DicomTag(tag.Group, (ushort)((group << 8) + (tag.Element & 0xff)), tag.PrivateCreator);
                }

                var value = items.TryGetSingleValue(creator, out string tmpValue) ? tmpValue : string.Empty;
                if (tag.PrivateCreator.Creator == value) return new DicomTag(tag.Group, (ushort)((group << 8) + (tag.Element & 0xff)), tag.PrivateCreator);
            }

            return null;
        }



        private static DicomTag ValidatePrivate(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            if (items.TryValidatePrivate(ref tag))
            {
                return tag;
            }
            else
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        private static void ValidateDicomTag(this IEnumerable<DicomItem> items, DicomTag tag, out DicomItem item)
        {
            if (!items.TryGetValue(tag, out item))
            {
                throw new DicomDataException($"Tag: {tag} not found in dataset");
            }
        }


        private static bool TryGetValue(this IEnumerable<DicomItem> items, DicomTag key, out DicomItem value)
        {
            value = items.FirstOrDefault(i => i.Tag == key);
            return value != null;
        }

        /// <summary>
        /// Checks the DICOM dataset to determine if the dataset already contains an item with the specified tag.
        /// </summary>
        /// <param name="tag">DICOM tag to test</param>
        /// <returns><c>True</c> if a DICOM item with the specified tag already exists.</returns>
        public static bool Contains(this IEnumerable<DicomItem> items, DicomTag tag)
        {
            if (tag.IsPrivate)
            {
                var privateTag = items.GetPrivateTag(tag, false);
                return (privateTag != null) && items.Any(i => i.Tag.Equals(privateTag));
            }
            return items.Any(i => i.Tag == tag);
        }

    }
}
