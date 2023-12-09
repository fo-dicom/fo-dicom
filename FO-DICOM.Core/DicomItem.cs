// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    /// <summary>
    /// Abstract base class for representing one DICOM item.
    /// </summary>
    public abstract class DicomItem : IComparable<DicomItem>, IComparable
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomItem"/> class.
        /// </summary>
        /// <param name="tag">Tag associated with the item.</param>
        protected DicomItem(DicomTag tag)
        {
            Tag = tag;
        }

        /// <summary>
        /// Gets the DICOM tag associated with the item.
        /// </summary>
        public DicomTag Tag { get; internal set; }

        /// <summary>
        /// Gets the Value Representation of the item.
        /// </summary>
        public abstract DicomVR ValueRepresentation { get; }

        /// <summary>
        /// Compare this <see cref="DicomItem"/> with the <paramref name="other"/>. 
        /// Comparison is purely based on the item's tag.
        /// </summary>
        /// <param name="other">DICOM item to compare against.</param>
        /// <returns>Value less than zero if this item's tag is less than <paramref name="other"/>'s, zero if the items' tags are equal and
        /// value greater than zero if this item's tag is greater than <paramref name="other"/>'s.</returns>
        public int CompareTo(DicomItem other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Tag.CompareTo(other.Tag);
        }

        /// <summary>
        /// Compare this <see cref="DicomItem"/> with <paramref name="obj"/>.
        /// Comparison will only succeed if <paramref name="obj"/> also is a <see cref="DicomItem"/>. Otherwise method throws.
        /// </summary>
        /// <param name="obj">Object to compare against, required to be a <see cref="DicomItem"/>.</param>
        /// <returns>If <paramref name="obj"/> is a <see cref="DicomItem"/>, value less than zero if this item's tag is less than 
        /// <paramref name="obj"/>'s, zero if the items' tags are equal and value greater than zero if this item's tag is greater than 
        /// <paramref name="obj"/>'s.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (!(obj is DicomItem))
            {
                throw new ArgumentException("Only comparison with DICOM items is supported.", nameof(obj));
            }

            return CompareTo((DicomItem)obj);
        }

        /// <summary>
        /// Returns a string summary of the DICOM item.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => Tag.DictionaryEntry != null
                ? $"{Tag} {ValueRepresentation} {Tag.DictionaryEntry.Name}"
                : $"{Tag} {ValueRepresentation} Unknown";

        public virtual void Validate()
        { }

    }
}
