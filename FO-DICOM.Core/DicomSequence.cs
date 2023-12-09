// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;

namespace FellowOakDicom
{

    /// <summary>
    /// Representation of a DICOM sequence of items.
    /// </summary>
    public class DicomSequence : DicomItem, IEnumerable<DicomDataset>
    {

        /// <summary>
        /// Initializes an instance of the <see cref="DicomSequence"/> class.
        /// </summary>
        /// <param name="tag">DICOM sequence tag.</param>
        /// <param name="items">Dataset items constituting the sequence.</param>
        public DicomSequence(DicomTag tag, params DicomDataset[] items)
            : base(tag)
        {
            Items = new List<DicomDataset>(items);
        }

        /// <inheritdoc />
        public override DicomVR ValueRepresentation => DicomVR.SQ;

        /// <summary>
        /// Gets the dataset items constituting the sequence.
        /// </summary>
        public IList<DicomDataset> Items { get; }

        /// <inheritdoc />
        public IEnumerator<DicomDataset> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <inheritdoc />
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public override void Validate()
        {
            Items?.Each(ds => ds?.Validate());
        }

    }
}
