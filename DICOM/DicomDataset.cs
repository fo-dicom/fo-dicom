// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Dicom.IO.Buffer;
using Dicom.StructuredReport;

namespace Dicom
{
    /// <summary>
    /// A collection of <see cref="DicomItem">DICOM items</see>.
    /// </summary>
    public partial class DicomDataset : IEnumerable<DicomItem>
    {
        #region FIELDS

        private readonly IDictionary<DicomTag, DicomItem> _items;

        private DicomTransferSyntax _syntax;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomDataset"/> class with <see cref="InternalTransferSyntax"/>
        /// set to Explicit VR Little Endian (DICOM default transfer syntax).
        /// </summary>
        public DicomDataset() : this(DicomTransferSyntax.ExplicitVRLittleEndian)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomDataset"/> class.
        /// </summary>
        /// <param name="internalTransferSyntax">Internal transfer syntax representation of the dataset.</param>
        public DicomDataset(DicomTransferSyntax internalTransferSyntax)
        {
            _items = new SortedDictionary<DicomTag, DicomItem>();
            InternalTransferSyntax = internalTransferSyntax;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomDataset"/> class.
        /// </summary>
        /// <param name="items">An array of DICOM items.</param>
        public DicomDataset(params DicomItem[] items)
            : this((IEnumerable<DicomItem>)items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomDataset"/> class.
        /// </summary>
        /// <param name="items">A collection of DICOM items.</param>
        public DicomDataset(IEnumerable<DicomItem> items)
            : this()
        {
            if (items != null)
            {
                foreach (var item in items.Where(item => item != null))
                {
                    if (item.ValueRepresentation.Equals(DicomVR.SQ))
                    {
                        var tag = item.Tag;
                        if (tag.IsPrivate) tag = GetPrivateTag(tag);
                        var sequenceItems =
                            ((DicomSequence)item).Items.Where(dataset => dataset != null)
                                .Select(dataset => new DicomDataset(dataset))
                                .ToArray();
                        _items[tag] = new DicomSequence(tag, sequenceItems);
                    }
                    else
                    {
                        _items[item.Tag.IsPrivate ? GetPrivateTag(item.Tag) : item.Tag] = item;
                    }
                }
            }
        }

        #endregion

        #region PROPERTIES

        /// <summary>Gets the DICOM transfer syntax of this dataset.</summary>
        public DicomTransferSyntax InternalTransferSyntax
        {
            get => _syntax;
            internal set
            {
                _syntax = value;

                // update transfer syntax for sequence items
                foreach (var sq in this.Where(x => x.ValueRepresentation == DicomVR.SQ).Cast<DicomSequence>())
                {
                    foreach (var item in sq.Items)
                    {
                        item.InternalTransferSyntax = _syntax;
                    }
                }
            }
        }

        #endregion


        #region Get-Methods


        /// <summary>
        /// Gets the <see cref="DicomItem"/> of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value. Must inherit from <see cref="DicomItem"/>.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <returns>Item corresponding to <paramref name="tag"/> or <code>null</code> if the <paramref name="tag"/> is not contained in the instance.</returns>
        public T GetDicomItem<T>(DicomTag tag) where T:DicomItem
        {
            tag = ValidatePrivate(tag);
            if (_items.TryGetValue(tag, out DicomItem dummyItem))
            {
                return dummyItem as T;
            }
            else
            {
                return null;
            }
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
                //TODO: check if this is the correct. 
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
                if (index >= element.Count )
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
                catch (DicomDataException)
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
        /// <param name="elementValue">Element values corresponding to <paramref name="tag"/>.</param>
        /// <returns>Returns <code>true</code> if the element values could be exctracted, otherwise <code>false</code>.</returns>
        public bool TryGetValues<T>(DicomTag tag, out T[] values)
        {
            if (typeof(T).GetTypeInfo().IsArray) {
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
                catch(DicomDataException)
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
                if (element.Count != 1) { throw new DicomDataException("DICOM element must contains a single value"); }

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
                catch (DicomDataException)
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
                    stringValue = element.Get<string>(-1);
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
                throw new DicomDataException("Tag: {0} not found in dataset", tag);
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
                throw new DicomDataException("Tag: {0} not found in dataset", tag);
            }
        }


        #endregion


        #region METHODS

        /// <summary>
        /// Gets the item or element value of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="n">Item index (for multi-valued elements).</param>
        /// <returns>Item or element value corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the dataset does not contain <paramref name="tag"/> or if the specified
        /// <paramref name="n">item index</paramref> is out-of-range.</exception>
        [Obsolete("Use GetValue or GetValues instead")]
        public T Get<T>(DicomTag tag, int n = 0)
        {
            return Get<T>(tag, n, false, default(T));
        }

        /// <summary>
        /// Gets the integer element value of the specified <paramref name="tag"/>, or default value if dataset does not contain <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="defaultValue">Default value to apply if <paramref name="tag"/> is not contained in dataset.</param>
        /// <returns>Element value corresponding to <paramref name="tag"/>.</returns>
        /// <exception cref="DicomDataException">If the element corresponding to <paramref name="tag"/> cannot be converted to an integer.</exception>
        [Obsolete("Use GetValue or GetValues instead")]
        public int Get(DicomTag tag, int defaultValue)
        {
            return Get<int>(tag, 0, true, defaultValue);
        }

        /// <summary>
        /// Gets the item or element value of the specified <paramref name="tag"/>, or default value if dataset does not contain <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="defaultValue">Default value to apply if <paramref name="tag"/> is not contained in dataset.</param>
        /// <returns>Item or element value corresponding to <paramref name="tag"/>.</returns>
        /// <remarks>In code, consider to use this method with implicit type specification, since <typeparamref name="T"/> can be inferred from
        /// <paramref name="defaultValue"/>, e.g. prefer <code>dataset.Get(tag, "Default")</code> over <code>dataset.Get&lt;string&gt;(tag, "Default")</code>.</remarks>
        [Obsolete("Use GetValue or GetValues instead")]
        public T Get<T>(DicomTag tag, T defaultValue)
        {
            return Get<T>(tag, 0, true, defaultValue);
        }

        /// <summary>
        /// Gets the item or element value of the specified <paramref name="tag"/>, or default value if dataset does not contain <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="n">Item index (for multi-valued elements).</param>
        /// <param name="defaultValue">Default value to apply if <paramref name="tag"/> is not contained in dataset.</param>
        /// <returns>Item or element value corresponding to <paramref name="tag"/>.</returns>
        [Obsolete("Use GetValue or GetValues instead")]
        public T Get<T>(DicomTag tag, int n, T defaultValue)
        {
            return Get<T>(tag, n, true, defaultValue);
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
            if (tag.Element >= 0xff) return tag;

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
        /// Add a collection of DICOM items to the dataset.
        /// </summary>
        /// <param name="items">Collection of DICOM items to add.</param>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="ArgumentException">If tag of added item already exists in dataset.</exception>
        public DicomDataset Add(params DicomItem[] items)
        {
            return DoAdd(items, false);
        }

        /// <summary>
        /// Add a collection of DICOM items to the dataset.
        /// </summary>
        /// <param name="items">Collection of DICOM items to add.</param>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="ArgumentException">If tag of added item already exists in dataset.</exception>
        public DicomDataset Add(IEnumerable<DicomItem> items)
        {
            return DoAdd(items, false);
        }

        /// <summary>
        /// Add single DICOM item given by <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="values">Values of the added item.</param>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="ArgumentException">If tag already exists in dataset.</exception>
        public DicomDataset Add<T>(DicomTag tag, params T[] values)
        {
            return DoAdd(tag, DicomEncoding.Default, values, false);
        }

        /// <summary>
        /// Add single DICOM item given by <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="encoding">Encoding to be applied when adding item to the dataset.</param>
        /// <param name="values">Values of the added item.</param>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="ArgumentException">If tag already exists in dataset.</exception>
        public DicomDataset Add<T>(DicomTag tag, Encoding encoding, params T[] values)
        {
            return DoAdd(tag, encoding, values, false);
        }

        /// <summary>
        /// Add single DICOM item given by <paramref name="vr"/>, <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="vr">DICOM vr of the added item. Use when setting a private element.</param>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="values">Values of the added item.</param>
        /// <remarks>No validation is performed on the <paramref name="vr"/> matching the element <paramref name="tag"/>
        /// This method is useful when adding a private tag and need to explicitly set the VR of the created element.
        /// </remarks>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="ArgumentException">If tag already exists in dataset.</exception>
        public DicomDataset Add<T>(DicomVR vr, DicomTag tag, params T[] values)
        {
            return DoAdd(vr, tag, DicomEncoding.Default, values, false);
        }

        /// <summary>
        /// Add single DICOM item given by <paramref name="vr"/>, <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="vr">DICOM vr of the added item. Use when setting a private element.</param>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="encoding">Encoding to be applied when adding item to the dataset.</param>
        /// <param name="values">Values of the added item.</param>
        /// <remarks>No validation is performed on the <paramref name="vr"/> matching the element <paramref name="tag"/>
        /// This method is useful when adding a private tag and need to explicitly set the VR of the created element.
        /// </remarks>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="ArgumentException">If tag already exists in dataset.</exception>
        public DicomDataset Add<T>(DicomVR vr, DicomTag tag, Encoding encoding, params T[] values)
        {
            return DoAdd(vr, tag, encoding, values, false);
        }

        /// <summary>
        /// Add a collection of DICOM items to the dataset. Update existing items.
        /// </summary>
        /// <param name="items">Collection of DICOM items to add.</param>
        /// <returns>The dataset instance.</returns>
        public DicomDataset AddOrUpdate(params DicomItem[] items)
        {
            return DoAdd(items, true);
        }

        /// <summary>
        /// Add a collection of DICOM items to the dataset. Update existing items.
        /// </summary>
        /// <param name="items">Collection of DICOM items to add.</param>
        /// <returns>The dataset instance.</returns>
        public DicomDataset AddOrUpdate(IEnumerable<DicomItem> items)
        {
            return DoAdd(items, true);
        }

        /// <summary>
        /// Add or update a single DICOM item given by <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="values">Values of the added item.</param>
        /// <returns>The dataset instance.</returns>
        public DicomDataset AddOrUpdate<T>(DicomTag tag, params T[] values)
        {
            return DoAdd(tag, DicomEncoding.Default, values, true);
        }

        /// <summary>
        /// Add or update a single DICOM item given by <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="encoding">Encoding to be applied when adding item to the dataset.</param>
        /// <param name="values">Values of the added item.</param>
        /// <returns>The dataset instance.</returns>
        public DicomDataset AddOrUpdate<T>(DicomTag tag, Encoding encoding, params T[] values)
        {
            return DoAdd(tag, encoding, values, true);
        }

        /// <summary>
        /// Add or update a single DICOM item given by <paramref name="vr"/>, <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="vr">DICOM vr of the added item. Use when setting a private element.</param>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="values">Values of the added item.</param>
        /// <remarks>No validation is performed on the <paramref name="vr"/> matching the element <paramref name="tag"/>
        /// This method is useful when adding a private tag and need to explicitly set the VR of the created element.
        /// </remarks>
        /// <returns>The dataset instance.</returns>
        public DicomDataset AddOrUpdate<T>(DicomVR vr, DicomTag tag, params T[] values)
        {
            return DoAdd(vr, tag, DicomEncoding.Default, values, true);
        }

        /// <summary>
        /// Add or update a single DICOM item given by <paramref name="vr"/>, <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="vr">DICOM vr of the added item. Use when setting a private element.</param>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="encoding">Encoding to be applied when adding item to the dataset.</param>
        /// <param name="values">Values of the added item.</param>
        /// <remarks>No validation is performed on the <paramref name="vr"/> matching the element <paramref name="tag"/>
        /// This method is useful when adding a private tag and need to explicitly set the VR of the created element.
        /// </remarks>
        /// <returns>The dataset instance.</returns>
        public DicomDataset AddOrUpdate<T>(DicomVR vr, DicomTag tag, Encoding encoding, params T[] values)
        {
            return DoAdd(vr, tag, encoding, values, true);
        }

        /// <summary>
        /// Add or update the image pixel data element in the dataset
        /// </summary>
        /// <param name="vr">DICOM vr of the image pixel. For a PixelData element this value should be either DicomVR.OB or DicomVR.OW DICOM VR.</param>
        /// <param name="pixelData">An <see cref="IByteBuffer"/> that holds the image pixel data </param>
        /// <param name="transferSyntax">A DicomTransferSyntax object of the <paramref name="pixelData"/> parameter.
        /// If parameter is not provided (null), then the default TransferSyntax "ExplicitVRLittleEndian" will be applied to the dataset</param>
        /// <remarks>Use this method whenever you are attaching an external image pixel data to the dataset and provide the proper TransferSyntax</remarks>
        /// <returns>The dataset instance.</returns>
        [Obsolete("Use DicomPixelData.AddFrame(IByteBuffer) to add pixel data to underlying dataset.")]
        public DicomDataset AddOrUpdatePixelData(DicomVR vr, IByteBuffer pixelData,
            DicomTransferSyntax transferSyntax = null)
        {
            this.AddOrUpdate(vr, DicomTag.PixelData, pixelData);

            if (null != transferSyntax)
            {
                InternalTransferSyntax = transferSyntax;
            }

            return this;
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
                if (privateTag == null) return false;
                return _items.Any(kv => kv.Key.Equals(privateTag));
            }
            return _items.ContainsKey(tag);
        }

        /// <summary>
        /// Removes items for specified tags.
        /// </summary>
        /// <param name="tags">DICOM tags to remove</param>
        /// <returns>Current Dataset</returns>
        public DicomDataset Remove(params DicomTag[] tags)
        {
            foreach (DicomTag tag in tags)
            {
                if (tag.IsPrivate)
                {
                    var privateTag = GetPrivateTag(tag);
                    if (privateTag == null) continue;
                    _items.Remove(privateTag);
                }
                else
                {
                    _items.Remove(tag);
                }
            }
            return this;
        }

        /// <summary>
        /// Removes items where the selector function returns true.
        /// </summary>
        /// <param name="selector">Selector function</param>
        /// <returns>Current Dataset</returns>
        public DicomDataset Remove(Func<DicomItem, bool> selector)
        {
            foreach (DicomItem item in _items.Values.Where(selector).ToArray()) _items.Remove(item.Tag);
            return this;
        }

        /// <summary>
        /// Removes all items from the dataset.
        /// </summary>
        /// <returns>Current Dataset</returns>
        public DicomDataset Clear()
        {
            _items.Clear();
            return this;
        }

        /// <summary>
        /// Copies all items to the destination dataset.
        /// </summary>
        /// <param name="destination">Destination Dataset</param>
        /// <returns>Current Dataset</returns>
        public DicomDataset CopyTo(DicomDataset destination)
        {
            if (destination != null) destination.AddOrUpdate(this);
            return this;
        }

        /// <summary>
        /// Copies tags to the destination dataset.
        /// </summary>
        /// <param name="destination">Destination Dataset</param>
        /// <param name="tags">Tags to copy</param>
        /// <returns>Current Dataset</returns>
        public DicomDataset CopyTo(DicomDataset destination, params DicomTag[] tags)
        {
            if (destination != null)
            {
                foreach (var tag in tags) destination.AddOrUpdate(Get<DicomItem>(tag));
            }
            return this;
        }

        /// <summary>
        /// Copies tags matching mask to the destination dataset.
        /// </summary>
        /// <param name="destination">Destination Dataset</param>
        /// <param name="mask">Tags to copy</param>
        /// <returns>Current Dataset</returns>
        public DicomDataset CopyTo(DicomDataset destination, DicomMaskedTag mask)
        {
            destination?.AddOrUpdate(_items.Values.Where(x => mask.IsMatch(x.Tag)));
            return this;
        }

        /// <summary>
        /// Enumerates all DICOM items.
        /// </summary>
        /// <returns>Enumeration of DICOM items</returns>
        public IEnumerator<DicomItem> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        /// <summary>
        /// Enumerates all DICOM items.
        /// </summary>
        /// <returns>Enumeration of DICOM items</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"DICOM Dataset [{_items.Count} items]";
        }

        /// <summary>
        /// Gets the item or element value of the specified <paramref name="tag"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="tag">Requested DICOM tag.</param>
        /// <param name="n">Item index (for multi-valued elements).</param>
        /// <param name="useDefault">Indicates whether to use default value (true) or throw (false) if <paramref name="tag"/> is not contained in dataset.</param>
        /// <param name="defaultValue">Default value to apply if <paramref name="tag"/> is not contained in dataset and <paramref name="useDefault"/> is true.</param>
        /// <returns>Item or element value corresponding to <paramref name="tag"/>.</returns>
        private T Get<T>(DicomTag tag, int n, bool useDefault, T defaultValue)
        {
            if (tag.IsPrivate)
            {
                var privateTag = GetPrivateTag(tag, false);
                if (privateTag == null)
                {
                    if (useDefault) return defaultValue;
                    throw new DicomDataException("Tag: {0} not found in dataset", tag);
                }

                tag = privateTag;
            }

            if (!_items.TryGetValue(tag, out DicomItem item))
            {
                if (useDefault) return defaultValue;
                throw new DicomDataException("Tag: {0} not found in dataset", tag);
            }

            if (typeof(T) == typeof(DicomItem)) return (T)(object)item;

            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(DicomItem))) return (T)(object)item;

            if (typeof(T) == typeof(DicomVR)) return (T)(object)item.ValueRepresentation;

            if (item.GetType().GetTypeInfo().IsSubclassOf(typeof(DicomElement)))
            {
                DicomElement element = (DicomElement)item;

                if (typeof(IByteBuffer).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())) return (T)(object)element.Buffer;

                if (typeof(T) == typeof(byte[])) return (T)(object)element.Buffer.Data;

                if (!typeof(T).GetTypeInfo().IsArray && (n >= element.Count || element.Count == 0))
                {
                    if (useDefault) return defaultValue;
                    throw new DicomDataException("Element empty or index: {0} exceeds element count: {1}", n, element.Count);
                }

                return element.Get<T>(n);
            }

            if (item.GetType() == typeof(DicomSequence))
            {
                if (typeof(T) == typeof(DicomCodeItem)) return (T)(object)new DicomCodeItem((DicomSequence)item);

                if (typeof(T) == typeof(DicomMeasuredValue)) return (T)(object)new DicomMeasuredValue((DicomSequence)item);

                if (typeof(T) == typeof(DicomReferencedSOP)) return (T)(object)new DicomReferencedSOP((DicomSequence)item);
            }

            throw new DicomDataException(
                "Unable to get a value type of {0} from DICOM item of type {1}",
                typeof(T),
                item.GetType());
        }

        /// <summary>
        /// Add a collection of DICOM items to the dataset.
        /// </summary>
        /// <param name="items">Collection of DICOM items to add.</param>
        /// <param name="allowUpdate">True if existing tag can be updated, false if method should throw when trying to add already existing tag.</param>
        /// <returns>The dataset instance.</returns>
        private DicomDataset DoAdd(IEnumerable<DicomItem> items, bool allowUpdate)
        {
            if (items != null)
            {
                if (allowUpdate)
                {
                    foreach (var item in items.Where(i => i != null))
                    {
                        var tag = item.Tag;
                        if (tag.IsPrivate)
                        {
                            tag = GetPrivateTag(tag);
                            item.Tag = tag;
                        }

                        _items[tag] = item;
                    }
                }
                else
                {
                    foreach (var item in items.Where(i => i != null))
                    {
                        var tag = item.Tag;
                        if (tag.IsPrivate)
                        {
                            tag = GetPrivateTag(tag);
                            item.Tag = tag;
                        }

                        _items.Add(tag, item);
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Add single DICOM item to the dataset.
        /// </summary>
        /// <param name="item">DICOM item to add.</param>
        /// <param name="allowUpdate">True if existing tag can be updated, false if method should throw when trying to add already existing tag.</param>
        /// <returns>The dataset instance.</returns>
        private DicomDataset DoAdd(DicomItem item, bool allowUpdate)
        {
            if (item != null)
            {
                var tag = item.Tag;
                if (tag.IsPrivate)
                {
                    tag = GetPrivateTag(tag);
                    item.Tag = tag;
                }

                if (allowUpdate)
                {
                    _items[tag] = item;
                }
                else
                {
                    _items.Add(tag, item);
                }
            }
            return this;
        }

        /// <summary>
        /// Add single DICOM item given by <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="encoding"></param>
        /// <param name="values">Values of the added item.</param>
        /// <param name="allowUpdate">True if existing tag can be updated, false if method should throw when trying to add already existing tag.</param>
        /// <returns>The dataset instance.</returns>
        private DicomDataset DoAdd<T>(DicomTag tag, Encoding encoding, IList<T> values, bool allowUpdate)
        {
            var entry = DicomDictionary.Default[tag.IsPrivate ? GetPrivateTag(tag) : tag];
            if (entry == null)
                throw new DicomDataException(
                    "Tag {0} not found in DICOM dictionary. Only dictionary tags may be added implicitly to the dataset.",
                    tag);

            DicomVR vr = null;
            if (values != null) vr = entry.ValueRepresentations.FirstOrDefault(x => x.ValueType == typeof(T));
            if (vr == null) vr = entry.ValueRepresentations.First();

            return DoAdd(vr, tag, encoding, values, allowUpdate);
        }

        /// <summary>
        /// Add single DICOM item given by <paramref name="vr"/>, <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="vr">DICOM vr of the added item. Use when setting a private element.</param>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="encoding"></param>
        /// <param name="values">Values of the added item.</param>
        /// <param name="allowUpdate">True if existing tag can be updated, false if method should throw when trying to add already existing tag.</param>
        /// <remarks>No validation is performed on the <paramref name="vr"/> matching the element <paramref name="tag"/>
        /// This method is useful when adding a private tag and need to explicitly set the VR of the created element.
        /// </remarks>
        /// <returns>The dataset instance.</returns>
        private DicomDataset DoAdd<T>(DicomVR vr, DicomTag tag, Encoding encoding, IList<T> values, bool allowUpdate)
        {
            if (tag.IsPrivate) tag = GetPrivateTag(tag);
            if (vr == DicomVR.AE)
            {
                if (values == null) return DoAdd(new DicomApplicationEntity(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomApplicationEntity(tag, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.AS)
            {
                if (values == null) return DoAdd(new DicomAgeString(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomAgeString(tag, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.AT)
            {
                if (values == null) return DoAdd(new DicomAttributeTag(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(DicomTag)) return DoAdd(new DicomAttributeTag(tag, values.Cast<DicomTag>().ToArray()), allowUpdate);

                IEnumerable<DicomTag> parsedValues;
                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, DicomTag.Parse, out parsedValues))
                {
                    return DoAdd(new DicomAttributeTag(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.CS)
            {
                if (values == null) return DoAdd(new DicomCodeString(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomCodeString(tag, values.Cast<string>().ToArray()), allowUpdate);
                if (typeof(T).GetTypeInfo().IsEnum) return DoAdd(new DicomCodeString(tag, values.Select(x => x.ToString()).ToArray()), allowUpdate);
            }

            if (vr == DicomVR.DA)
            {
                if (values == null) return DoAdd(new DicomDate(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(DateTime)) return DoAdd(new DicomDate(tag, values.Cast<DateTime>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomDateRange))
                    return
                        DoAdd(new DicomDate(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomDate(tag, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.DS)
            {
                if (values == null) return DoAdd(new DicomDecimalString(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(decimal)) return DoAdd(new DicomDecimalString(tag, values.Cast<decimal>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomDecimalString(tag, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.DT)
            {
                if (values == null) return DoAdd(new DicomDateTime(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(DateTime)) return DoAdd(new DicomDateTime(tag, values.Cast<DateTime>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomDateRange))
                    return
                        DoAdd(
                            new DicomDateTime(
                                tag,
                                values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomDateTime(tag, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.FD)
            {
                if (values == null) return DoAdd(new DicomFloatingPointDouble(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(double)) return DoAdd(new DicomFloatingPointDouble(tag, values.Cast<double>().ToArray()), allowUpdate);

                IEnumerable<double> parsedValues;
                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, double.Parse, out parsedValues))
                {
                    return DoAdd(new DicomFloatingPointDouble(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.FL)
            {
                if (values == null) return DoAdd(new DicomFloatingPointSingle(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(float)) return DoAdd(new DicomFloatingPointSingle(tag, values.Cast<float>().ToArray()), allowUpdate);

                IEnumerable<float> parsedValues;
                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, float.Parse, out parsedValues))
                {
                    return DoAdd(new DicomFloatingPointSingle(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.IS)
            {
                if (values == null) return DoAdd(new DicomIntegerString(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(int)) return DoAdd(new DicomIntegerString(tag, values.Cast<int>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomIntegerString(tag, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.LO)
            {
                if (values == null) return DoAdd(new DicomLongString(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomLongString(tag, encoding, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.LT)
            {
                if (values == null) return DoAdd(new DicomLongText(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomLongText(tag, encoding, values.Cast<string>().First()), allowUpdate);
            }

            if (vr == DicomVR.OB)
            {
                if (values == null) return DoAdd(new DicomOtherByte(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(byte)) return DoAdd(new DicomOtherByte(tag, values.Cast<byte>().ToArray()), allowUpdate);

                if (typeof(T) == typeof(IByteBuffer) && values.Count == 1)
                {
                    return DoAdd(new DicomOtherByte(tag, (IByteBuffer)values[0]), allowUpdate);
                }
            }

            if (vr == DicomVR.OD)
            {
                if (values == null) return DoAdd(new DicomOtherDouble(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(double)) return DoAdd(new DicomOtherDouble(tag, values.Cast<double>().ToArray()), allowUpdate);

                if (typeof(T) == typeof(IByteBuffer) && values.Count == 1)
                {
                    return DoAdd(new DicomOtherDouble(tag, (IByteBuffer)values[0]), allowUpdate);
                }
            }

            if (vr == DicomVR.OF)
            {
                if (values == null) return DoAdd(new DicomOtherFloat(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(float)) return DoAdd(new DicomOtherFloat(tag, values.Cast<float>().ToArray()), allowUpdate);

                if (typeof(T) == typeof(IByteBuffer) && values.Count == 1)
                {
                    return DoAdd(new DicomOtherFloat(tag, (IByteBuffer)values[0]), allowUpdate);
                }
            }

            if (vr == DicomVR.OL)
            {
                if (values == null) return DoAdd(new DicomOtherLong(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(uint)) return DoAdd(new DicomOtherLong(tag, values.Cast<uint>().ToArray()), allowUpdate);

                if (typeof(T) == typeof(IByteBuffer) && values.Count == 1)
                {
                    return DoAdd(new DicomOtherLong(tag, (IByteBuffer)values[0]), allowUpdate);
                }
            }

            if (vr == DicomVR.OW)
            {
                if (values == null) return DoAdd(new DicomOtherWord(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(ushort)) return DoAdd(new DicomOtherWord(tag, values.Cast<ushort>().ToArray()), allowUpdate);

                if (typeof(T) == typeof(IByteBuffer) && values.Count == 1)
                {
                    return DoAdd(new DicomOtherWord(tag, (IByteBuffer)values[0]), allowUpdate);
                }
            }

            if (vr == DicomVR.PN)
            {
                if (values == null) return DoAdd(new DicomPersonName(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomPersonName(tag, encoding, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.SH)
            {
                if (values == null) return DoAdd(new DicomShortString(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomShortString(tag, encoding, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.SL)
            {
                if (values == null) return DoAdd(new DicomSignedLong(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(int)) return DoAdd(new DicomSignedLong(tag, values.Cast<int>().ToArray()), allowUpdate);

                IEnumerable<int> parsedValues;
                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, int.Parse, out parsedValues))
                {
                    return DoAdd(new DicomSignedLong(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.SQ)
            {
                if (values == null) return DoAdd(new DicomSequence(tag), allowUpdate);
                if (typeof(T) == typeof(DicomContentItem)) return DoAdd(new DicomSequence(tag, values.Cast<DicomContentItem>().Select(x => x.Dataset).ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomDataset) || typeof(T) == typeof(DicomCodeItem)
                    || typeof(T) == typeof(DicomMeasuredValue) || typeof(T) == typeof(DicomReferencedSOP)) return DoAdd(new DicomSequence(tag, values.Cast<DicomDataset>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.SS)
            {
                if (values == null) return DoAdd(new DicomSignedShort(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(short)) return DoAdd(new DicomSignedShort(tag, values.Cast<short>().ToArray()), allowUpdate);

                IEnumerable<short> parsedValues;
                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, short.Parse, out parsedValues))
                {
                    return DoAdd(new DicomSignedShort(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.ST)
            {
                if (values == null) return DoAdd(new DicomShortText(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomShortText(tag, encoding, values.Cast<string>().First()), allowUpdate);
            }

            if (vr == DicomVR.TM)
            {
                if (values == null) return DoAdd(new DicomTime(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(DateTime)) return DoAdd(new DicomTime(tag, values.Cast<DateTime>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomDateRange))
                    return
                        DoAdd(new DicomTime(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomTime(tag, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.UC)
            {
                if (values == null) return DoAdd(new DicomUnlimitedCharacters(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomUnlimitedCharacters(tag, encoding, values.Cast<string>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.UI)
            {
                if (values == null) return DoAdd(new DicomUniqueIdentifier(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomUniqueIdentifier(tag, values.Cast<string>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomUID)) return DoAdd(new DicomUniqueIdentifier(tag, values.Cast<DicomUID>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomTransferSyntax)) return DoAdd(new DicomUniqueIdentifier(tag, values.Cast<DicomTransferSyntax>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.UL)
            {
                if (values == null) return DoAdd(new DicomUnsignedLong(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(uint)) return DoAdd(new DicomUnsignedLong(tag, values.Cast<uint>().ToArray()), allowUpdate);

                IEnumerable<uint> parsedValues;
                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, uint.Parse, out parsedValues))
                {
                    return DoAdd(new DicomUnsignedLong(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.UN)
            {
                if (values == null) return DoAdd(new DicomUnknown(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(byte)) return DoAdd(new DicomUnknown(tag, values.Cast<byte>().ToArray()), allowUpdate);

                if (typeof(T) == typeof(IByteBuffer) && values.Count == 1)
                {
                    return DoAdd(new DicomUnknown(tag, (IByteBuffer)values[0]), allowUpdate);
                }
            }

            if (vr == DicomVR.UR)
            {
                if (values == null) return DoAdd(new DicomUniversalResource(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomUniversalResource(tag, encoding, values.Cast<string>().First()), allowUpdate);
            }

            if (vr == DicomVR.US)
            {
                if (values == null) return DoAdd(new DicomUnsignedShort(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(ushort)) return DoAdd(new DicomUnsignedShort(tag, values.Cast<ushort>().ToArray()), allowUpdate);

                IEnumerable<ushort> parsedValues;
                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, ushort.Parse, out parsedValues))
                {
                    return DoAdd(new DicomUnsignedShort(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.UT)
            {
                if (values == null) return DoAdd(new DicomUnlimitedText(tag, encoding, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomUnlimitedText(tag, encoding, values.Cast<string>().First()), allowUpdate);
            }

            throw new InvalidOperationException(
                $"Unable to create DICOM element of type {vr.Code} with values of type {typeof(T)}");
        }

        private static bool ParseVrValueFromString<T, TOut>(
            IEnumerable<T> values,
            DicomVM valueMultiplicity,
            Func<string, TOut> parser,
            out IEnumerable<TOut> parsedValues)
        {
            parsedValues = null;

            if (typeof(T) == typeof(string))
            {
                var stringValues = values.Cast<string>().ToArray();

                if (valueMultiplicity.Maximum > 1 && stringValues.Length == 1)
                {
                    stringValues = stringValues[0].Split('\\');
                }

                parsedValues = stringValues.Where(n => !string.IsNullOrEmpty(n?.Trim())).Select(parser);

                return true;
            }

            return false;
        }

        #endregion
    }
}
