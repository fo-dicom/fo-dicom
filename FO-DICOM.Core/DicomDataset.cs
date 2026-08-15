// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.StructuredReport;
using FellowOakDicom.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FellowOakDicom
{

    /// <summary>
    /// A collection of <see cref="DicomItem">DICOM items</see>.
    /// </summary>
    public partial class DicomDataset : IEnumerable<DicomItem>
    {
        #region Static Properties

        /// <summary>
        /// Gets or sets how two DicomDatasets are compared if dataset1 == dataset2 is called
        /// If this property is true, then all items are iterated and the content is compared. Then two DicomDatasets are equal if the content is equal.
        /// If this property is false, then the equalitycheck tests if the DicomDatasets are the same instance.
        /// </summary>
        public static bool CompareInstancesByContent { get; set; } = true;

        #endregion

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
            : this(internalTransferSyntax, 64)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomDataset"/> class.
        /// </summary>
        /// <param name="internalTransferSyntax">Internal transfer syntax representation of the dataset.</param>
        /// <param name="capacity">Initial capacity of the internal collection.</param>
        internal DicomDataset(DicomTransferSyntax internalTransferSyntax, int capacity)
        {
            _items = new SortedList<DicomTag, DicomItem>(capacity);
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
            : this(items, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomDataset"/> class.
        /// </summary>
        /// <param name="items">A collection of DICOM items.</param>
        internal DicomDataset(IEnumerable<DicomItem> items, bool validate)
            : this()
        {
            ValidateItems = validate;
            if (items != null)
            {
                foreach (var item in items.Where(item => item != null))
                {
                    if (item.ValueRepresentation.Equals(DicomVR.SQ))
                    {
                        var tag = item.Tag;
                        if (tag.IsPrivate)
                        {
                            tag = GetPrivateTag(tag);
                        }
                        var sequenceItems =
                            ((DicomSequence)item).Items.Where(dataset => dataset != null)
                                .Select(dataset => new DicomDataset(dataset, validate))
                                .ToArray();
                        _items[tag] = new DicomSequence(tag, sequenceItems);
                    }
                    else
                    {
                        if (ValidateItems)
                        {
                            item.Validate();
                        }
                        _items[item.Tag.IsPrivate ? GetPrivateTag(item.Tag) : item.Tag] = item;
                    }
                }
            }
            ValidateItems = true;
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

        /// <summary>
        /// Gets or sets the fallback encodings that are used for string-based values if the dataset does not contain an explicit SpecificCharacterSet entry.
        /// This value is set before serializing the Dataset into a stream and when some encodings are inherited from parent datasets.
        /// </summary>
        internal Encoding[] FallbackEncodings { get; set; } = DicomEncoding.DefaultArray;

        /// <summary>
        /// Gets the encodings used for string-based values by evaluating SpecificCharacterSet value or by using the fallback-encoding if there is no explicit Tag.
        /// This method is intended to be called before serializing the dataset into a stream to determine the encoding to be used.
        /// </summary>
        /// <returns></returns>
        internal Encoding[] GetEncodingsForSerialization()
        {
            return TryGetValues<string>(DicomTag.SpecificCharacterSet, out var charsets)
                ? DicomEncoding.GetEncodings(charsets)
                : FallbackEncodings;
        }


        internal bool _validateItems = true;
        internal bool ValidateItems
        {
            get => _validateItems && DicomValidation.PerformValidation;
            set => _validateItems = value;
        }

        /// <summary>
        /// Gets or sets if the content of DicomItems shall be validated as soon as they are added to the DicomDataset
        /// </summary>
        [Obsolete("Use this property with care. You can suppress validation, but be aware you might create invalid Datasets if you need to set this property.", false)]
        public bool AutoValidate
        {
            get => _validateItems;
            set => ValidateItems = value;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Performs a validation of all DICOM items that are contained in this DicomDataset. This explicit call for validation ignores the
        /// gobal DicomValidation.AutoValidate and DicomDataset.AutoValidate property.
        /// </summary>
        /// <exception cref="DicomValidationException">A exception is thrown if one of the items does not pass the valiation</exception>
        public void Validate()
        {
            foreach (var item in this)
            {
                item.Validate();
            }
        }


        /// <summary>
        /// Adds a DICOM item to the dataset.
        /// </summary>
        /// <param name="item">DICOM item to add.</param>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="System.ArgumentException">If tag of added item already exists in dataset.</exception>
        public DicomDataset Add(DicomItem item)
        {
            return DoAdd(item, false);
        }

        /// <summary>
        /// Add a collection of DICOM items to the dataset.
        /// </summary>
        /// <param name="items">Collection of DICOM items to add.</param>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="System.ArgumentException">If tag of added item already exists in dataset.</exception>
        public DicomDataset Add(params DicomItem[] items)
        {
            return DoAdd(items, false);
        }

        /// <summary>
        /// Add a collection of DICOM items to the dataset.
        /// </summary>
        /// <param name="items">Collection of DICOM items to add.</param>
        /// <returns>The dataset instance.</returns>
        /// <exception cref="System.ArgumentException">If tag of added item already exists in dataset.</exception>
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
        /// <exception cref="System.ArgumentException">If tag already exists in dataset.</exception>
        public DicomDataset Add<T>(DicomTag tag, params T[] values)
        {
            return DoAdd(tag, values, false);
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
        /// <exception cref="System.ArgumentException">If tag already exists in dataset.</exception>
        public DicomDataset Add<T>(DicomVR vr, DicomTag tag, params T[] values)
        {
            return DoAdd(vr, tag, values, false);
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
        /// Add a DICOM item to the dataset. Update existing items.
        /// </summary>
        /// <param name="item">DICOM item to add.</param>
        /// <returns>The dataset instance.</returns>
        public DicomDataset AddOrUpdate(DicomItem item)
        {
            return DoAdd(item, true);
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
            return DoAdd(tag, values, true);
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
            return DoAdd(vr, tag, values, true);
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
                    if (privateTag == null)
                    {
                        continue;
                    }

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
            List<DicomTag> toRemove = null;
            foreach (var item in _items.Values)
            {
                if (selector(item))
                {
                    (toRemove ??= new List<DicomTag>()).Add(item.Tag);
                }
            }
            if (toRemove != null)
            {
                foreach (var tag in toRemove)
                {
                    _items.Remove(tag);
                }
            }
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
            destination?.AddOrUpdate(this);
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
                foreach (var tag in tags)
                {
                    destination.AddOrUpdate(GetDicomItem<DicomItem>(tag));
                }
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
        /// Does nothing. Can be overwritten in derived classes to check if the tag is allowed.
        /// </summary>
        protected virtual void ValidateTag(DicomTag tag)
        {
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
                        ValidateTag(tag);
                        if (tag.IsPrivate)
                        {
                            tag = GetPrivateTag(tag);
                            item.Tag = tag;
                        }

                        if (ValidateItems) item.Validate();
                        _items[tag] = item;
                    }
                }
                else
                {
                    foreach (var item in items.Where(i => i != null))
                    {
                        var tag = item.Tag;
                        ValidateTag(tag);
                        if (tag.IsPrivate)
                        {
                            tag = GetPrivateTag(tag);
                            item.Tag = tag;
                        }

                        if (ValidateItems) item.Validate();
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
                ValidateTag(tag);
                if (tag.IsPrivate)
                {
                    tag = GetPrivateTag(tag);
                    item.Tag = tag;
                }
                if (ValidateItems) item.Validate();

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
        /// <param name="values">Values of the added item.</param>
        /// <param name="allowUpdate">True if existing tag can be updated, false if method should throw when trying to add already existing tag.</param>
        /// <returns>The dataset instance.</returns>
        private DicomDataset DoAdd<T>(DicomTag tag, IList<T> values, bool allowUpdate)
        {
            var entry = DicomDictionary.Default[tag.IsPrivate ? GetPrivateTag(tag) : tag];
            if (entry == DicomDictionary.UnknownTag && tag.IsPrivate)
            {
                string groupNumber = tag.Group.ToString("X4");
                string elementNumber = tag.Element.ToString("X4");
                throw new DicomDataException($"Unknown private tag <{tag.PrivateCreator}> ({groupNumber}, {elementNumber}) has no VR defined.");
            }
            if (entry == DicomDictionary.UnknownTag && !tag.IsPrivate)
            {
                throw new DicomDataException($"Tag {tag} not found in DICOM dictionary. Only dictionary tags may be added implicitly to the dataset.");
            }

            DicomVR vr = null;
            if (values != null) vr = Array.Find(entry.ValueRepresentations, x => x.ValueType == typeof(T));
            if (vr == null)
            {
                vr = entry.ValueRepresentations[0];
            }
            return DoAdd(vr, tag, values, allowUpdate);
        }

        /// <summary>
        /// Add single DICOM item given by <paramref name="vr"/>, <paramref name="tag"/> and <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">Type of added values.</typeparam>
        /// <param name="vr">DICOM vr of the added item. Use when setting a private element.</param>
        /// <param name="tag">DICOM tag of the added item.</param>
        /// <param name="values">Values of the added item.</param>
        /// <param name="allowUpdate">True if existing tag can be updated, false if method should throw when trying to add already existing tag.</param>
        /// <remarks>No validation is performed on the <paramref name="vr"/> matching the element <paramref name="tag"/>
        /// This method is useful when adding a private tag and need to explicitly set the VR of the created element.
        /// </remarks>
        /// <returns>The dataset instance.</returns>
        private DicomDataset DoAdd<T>(DicomVR vr, DicomTag tag, IList<T> values, bool allowUpdate)
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

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, DicomTag.Parse, out IEnumerable<DicomTag> parsedValues))
                {
                    return DoAdd(new DicomAttributeTag(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.CS)
            {
                if (values == null) return DoAdd(new DicomCodeString(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomCodeString(tag, values.Cast<string>().ToArray()), allowUpdate);
                if (typeof(T).GetTypeInfo().IsEnum) return DoAdd(new DicomCodeString(tag, values.Select(x => x.ToString().ToUpperInvariant()).ToArray()), allowUpdate);
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
                if (typeof(T) == typeof(float)) return DoAdd(new DicomDecimalString(tag, values.Cast<float>().Select(Convert.ToDecimal).ToArray()), allowUpdate);
                if (typeof(T) == typeof(double)) return DoAdd(new DicomDecimalString(tag, values.Cast<double>().Select(Convert.ToDecimal).ToArray()), allowUpdate);
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
                if (typeof(T) == typeof(float)) return DoAdd(new DicomFloatingPointDouble(tag, values.Cast<float>().Select(Convert.ToDouble).ToArray()), allowUpdate);
                if (typeof(T) == typeof(double)) return DoAdd(new DicomFloatingPointDouble(tag, values.Cast<double>().ToArray()), allowUpdate);

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, double.Parse, out IEnumerable<double> parsedValues))
                {
                    return DoAdd(new DicomFloatingPointDouble(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.FL)
            {
                if (values == null) return DoAdd(new DicomFloatingPointSingle(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(float)) return DoAdd(new DicomFloatingPointSingle(tag, values.Cast<float>().ToArray()), allowUpdate);
                if (typeof(T) == typeof(double)) return DoAdd(new DicomFloatingPointSingle(tag, values.Cast<double>().Select(Convert.ToSingle).ToArray()), allowUpdate);

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, float.Parse, out IEnumerable<float> parsedValues))
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
                if (values == null) return DoAdd(new DicomLongString(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomLongString(tag, values.Cast<string>().ToArray()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
            }

            if (vr == DicomVR.LT)
            {
                if (values == null) return DoAdd(new DicomLongText(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomLongText(tag, values.Cast<string>().FirstOrDefault()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
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

            if (vr == DicomVR.OV)
            {
                if (values == null) return DoAdd(new DicomOtherVeryLong(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(ulong)) return DoAdd(new DicomOtherVeryLong(tag, values.Cast<ulong>().ToArray()), allowUpdate);

                if (typeof(T) == typeof(IByteBuffer) && values.Count == 1)
                {
                    return DoAdd(new DicomOtherVeryLong(tag, (IByteBuffer)values[0]), allowUpdate);
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
                if (values == null) return DoAdd(new DicomPersonName(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomPersonName(tag, values.Cast<string>().ToArray()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
            }

            if (vr == DicomVR.SH)
            {
                if (values == null) return DoAdd(new DicomShortString(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomShortString(tag, values.Cast<string>().ToArray()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
            }

            if (vr == DicomVR.SL)
            {
                if (values == null) return DoAdd(new DicomSignedLong(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(int)) return DoAdd(new DicomSignedLong(tag, values.Cast<int>().ToArray()), allowUpdate);

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, int.Parse, out IEnumerable<int> parsedValues))
                {
                    return DoAdd(new DicomSignedLong(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.SQ)
            {
                if (values == null) return DoAdd(new DicomSequence(tag), allowUpdate);
                if (typeof(T) == typeof(DicomSequence) && values.Count == 1) return DoAdd(new DicomSequence(tag, (values[0] as DicomSequence).Items.ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomContentItem)) return DoAdd(new DicomSequence(tag, values.Cast<DicomContentItem>().Select(x => x.Dataset).ToArray()), allowUpdate);
                if (typeof(T) == typeof(DicomDataset) || typeof(T) == typeof(DicomCodeItem)
                    || typeof(T) == typeof(DicomMeasuredValue) || typeof(T) == typeof(DicomReferencedSOP)) return DoAdd(new DicomSequence(tag, values.Cast<DicomDataset>().ToArray()), allowUpdate);
            }

            if (vr == DicomVR.SS)
            {
                if (values == null) return DoAdd(new DicomSignedShort(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(short)) return DoAdd(new DicomSignedShort(tag, values.Cast<short>().ToArray()), allowUpdate);

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, short.Parse, out IEnumerable<short> parsedValues))
                {
                    return DoAdd(new DicomSignedShort(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.ST)
            {
                if (values == null) return DoAdd(new DicomShortText(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomShortText(tag, values.Cast<string>().FirstOrDefault()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
            }

            if (vr == DicomVR.SV)
            {
                if (values == null) return DoAdd(new DicomSignedVeryLong(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(long)) return DoAdd(new DicomSignedVeryLong(tag, values.Cast<long>().ToArray()), allowUpdate);

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, long.Parse, out IEnumerable<long> parsedValues))
                {
                    return DoAdd(new DicomSignedVeryLong(tag, parsedValues.ToArray()), allowUpdate);
                }
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
                if (values == null) return DoAdd(new DicomUnlimitedCharacters(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomUnlimitedCharacters(tag, values.Cast<string>().ToArray()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
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

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, uint.Parse, out IEnumerable<uint> parsedValues))
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
                if (values == null) return DoAdd(new DicomUniversalResource(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomUniversalResource(tag, values.Cast<string>().FirstOrDefault()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
            }

            if (vr == DicomVR.US)
            {
                if (values == null) return DoAdd(new DicomUnsignedShort(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(ushort)) return DoAdd(new DicomUnsignedShort(tag, values.Cast<ushort>().ToArray()), allowUpdate);

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, ushort.Parse, out IEnumerable<ushort> parsedValues))
                {
                    return DoAdd(new DicomUnsignedShort(tag, parsedValues.ToArray()), allowUpdate);
                }
            }

            if (vr == DicomVR.UT)
            {
                if (values == null) return DoAdd(new DicomUnlimitedText(tag, DicomEncoding.DefaultArray, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(string)) return DoAdd(new DicomUnlimitedText(tag, values.Cast<string>().FirstOrDefault()) { TargetEncodings = DicomEncoding.DefaultArray }, allowUpdate);
            }

            if (vr == DicomVR.UV)
            {
                if (values == null) return DoAdd(new DicomUnsignedVeryLong(tag, EmptyBuffer.Value), allowUpdate);
                if (typeof(T) == typeof(ulong)) return DoAdd(new DicomUnsignedVeryLong(tag, values.Cast<ulong>().ToArray()), allowUpdate);

                if (ParseVrValueFromString(values, tag.DictionaryEntry.ValueMultiplicity, ulong.Parse, out IEnumerable<ulong> parsedValues))
                {
                    return DoAdd(new DicomUnsignedVeryLong(tag, parsedValues.ToArray()), allowUpdate);
                }
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


        private void SetTargetEncodingsToStringElements(Encoding[] values)
        {
            foreach (var item in this)
            {
                if (item is DicomStringElement txt)
                {
                    txt.TargetEncodings = values;
                }
            }
        }


        internal void OnBeforeSerializing()
        {
            // first evaluate the encoding, and then apply
            SetTargetEncodingsToStringElements(GetEncodingsForSerialization());
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals(obj as DicomDataset);
        }

        private bool Equals(DicomDataset other) => CompareInstancesByContent
                ? DicomDatasetComparer.DefaultInstance.Equals(this, other)
                : ReferenceEquals(this, other);

        public static bool operator ==(DicomDataset a, DicomDataset b)
        {
            if (((object)a == null) && ((object)b == null)) return true;
            if (((object)a == null) || ((object)b == null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(DicomDataset a, DicomDataset b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var element in this)
            {
                hashCode.Add(element);
            }

            return hashCode.ToHashCode();
        }

        #endregion
    }


    public class UnvalidatedScope : IDisposable
    {
        private DicomDataset _dataset;
        private readonly bool _validation;

        public UnvalidatedScope(DicomDataset dataSet)
        {
            _dataset = dataSet;
            _validation = dataSet.ValidateItems;
            _dataset.ValidateItems = false;
        }

        #region IDisposable Support

        private bool _disposedValue = false; // for detecting redundant calling of Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dataset.ValidateItems = _validation;
                    _dataset = null;
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }

}
