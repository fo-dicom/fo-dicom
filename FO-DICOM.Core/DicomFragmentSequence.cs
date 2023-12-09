// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System.Collections.Generic;

namespace FellowOakDicom
{

    /// <summary>
    /// Abstract class for representing fragment sequences of DICOM pixel data.
    /// </summary>
    public abstract class DicomFragmentSequence : DicomItem, IEnumerable<IByteBuffer>
    {
        #region FIELDS

        private IList<uint> _offsetTable;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomFragmentSequence"/> abstract class.
        /// </summary>
        /// <param name="tag"></param>
        protected DicomFragmentSequence(DicomTag tag)
            : base(tag)
        {
            Fragments = new List<IByteBuffer>();
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the fragment offset table.
        /// </summary>
        public IList<uint> OffsetTable => _offsetTable ?? (_offsetTable = new List<uint>());

        /// <summary>
        /// Gets the collection of fragments.
        /// </summary>
        public IList<IByteBuffer> Fragments { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Adds a fragment to the collection of fragments.
        /// </summary>
        /// <param name="fragment">Fragment to be added to this sequence.</param>
        internal void Add(IByteBuffer fragment)
        {
            if (_offsetTable == null)
            {
                var en = ByteBufferEnumerator<uint>.Create(fragment);
                _offsetTable = new List<uint>(en);
                return;
            }
            Fragments.Add(fragment);
        }

        /// <inheritdoc />
        public IEnumerator<IByteBuffer> GetEnumerator()
        {
            return Fragments.GetEnumerator();
        }

        /// <inheritdoc />
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    /// <summary>
    /// Class representing a fragment sequence of Other Byte (OB) items.
    /// </summary>
    public class DicomOtherByteFragment : DicomFragmentSequence
    {
        public DicomOtherByteFragment(DicomTag tag)
            : base(tag)
        {
        }

        /// <inheritdoc />
        public override DicomVR ValueRepresentation => DicomVR.OB;
    }

    /// <summary>
    /// Class representing a fragment sequence of Other Word (OW) items.
    /// </summary>
    public class DicomOtherWordFragment : DicomFragmentSequence
    {
        public DicomOtherWordFragment(DicomTag tag)
            : base(tag)
        {
        }

        /// <inheritdoc />
        public override DicomVR ValueRepresentation => DicomVR.OW;
    }
}
