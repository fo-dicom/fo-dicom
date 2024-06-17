// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FellowOakDicom
{

    /// <summary>
    /// Interface for traversing a DICOM dataset.
    /// </summary>
    public interface IDicomDatasetWalker
    {
        /// <summary>
        /// Handler for beginning the traversal.
        /// </summary>
        void OnBeginWalk();

        /// <summary>
        /// Handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnElement(DicomElement element);

        /// <summary>
        /// Asynchronous handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        Task<bool> OnElementAsync(DicomElement element);

        /// <summary>
        /// Handler for traversing beginning of sequence.
        /// </summary>
        /// <param name="sequence">Sequence to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnBeginSequence(DicomSequence sequence);

        /// <summary>
        /// Handler for traversing beginning of sequence item.
        /// </summary>
        /// <param name="dataset">Item dataset.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnBeginSequenceItem(DicomDataset dataset);

        /// <summary>
        /// Handler for traversing end of sequence item.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnEndSequenceItem();

        /// <summary>
        /// Handler for traversing end of sequence.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnEndSequence();

        /// <summary>
        /// Handler for traversing beginning of fragment.
        /// </summary>
        /// <param name="fragment">Fragment sequence.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnBeginFragment(DicomFragmentSequence fragment);

        /// <summary>
        /// Handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnFragmentItem(IByteBuffer item);

        /// <summary>
        /// Asynchronous handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        Task<bool> OnFragmentItemAsync(IByteBuffer item);

        /// <summary>
        /// Handler for traversing end of fragment.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        bool OnEndFragment();

        /// <summary>
        /// Handler for end of traversal.
        /// </summary>
        void OnEndWalk();
    }

    /// <summary>
    /// Worker class for performing DICOM dataset traversal.
    /// </summary>
    public class DicomDatasetWalker
    {
        #region State Items

        private class BeginDicomSequenceItem : DicomItem
        {
            public BeginDicomSequenceItem(DicomDataset item)
                : base(DicomTag.Unknown)
            {
                Dataset = item;
            }

            public DicomDataset Dataset { get; private set; }

            public override DicomVR ValueRepresentation => DicomVR.NONE;
        }

        private class EndDicomSequenceItem : DicomItem
        {
            public EndDicomSequenceItem()
                : base(DicomTag.Unknown)
            {
            }

            public override DicomVR ValueRepresentation => DicomVR.NONE;
        }

        private class EndDicomSequence : DicomItem
        {
            public EndDicomSequence()
                : base(DicomTag.Unknown)
            {
            }

            public override DicomVR ValueRepresentation => DicomVR.NONE;
        }

        private class DicomFragmentItem : DicomItem
        {
            public DicomFragmentItem(IByteBuffer buffer)
                : base(DicomTag.Unknown)
            {
                Buffer = buffer;
            }

            public IByteBuffer Buffer { get; private set; }

            public override DicomVR ValueRepresentation => DicomVR.NONE;
        }

        private class EndDicomFragment : DicomItem
        {
            public EndDicomFragment()
                : base(DicomTag.Unknown)
            {
            }

            public override DicomVR ValueRepresentation => DicomVR.NONE;
        }

        #endregion

        #region Fields

        private readonly DicomDataset _dataset;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of a <see cref="DicomDatasetWalker"/>.
        /// </summary>
        /// <param name="dataset"></param>
        public DicomDatasetWalker(DicomDataset dataset)
        {
            _dataset = dataset;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Perform a synchronous "walk" across the DICOM dataset provided in the <see cref="DicomDatasetWalker"/> constructor.
        /// </summary>
        /// <param name="walker">Dataset walker implementation to be used for dataset traversal.</param>
        public void Walk(IDicomDatasetWalker walker)
        {
            _dataset.OnBeforeSerializing();
            var items = new Queue<DicomItem>();
            BuildWalkQueue(_dataset, items);

            DoWalk(walker, items);
        }

        /// <summary>
        /// Perform an asynchronous "walk" across the DICOM dataset provided in the <see cref="DicomDatasetWalker"/> constructor.
        /// </summary>
        /// <param name="walker">Dataset walker implementation to be used for dataset traversal.</param>
        /// <returns>Awaitable <see cref="System.Threading.Tasks.Task"/>.</returns>
        public Task WalkAsync(IDicomDatasetWalker walker)
        {
            _dataset.OnBeforeSerializing();
            var items = new Queue<DicomItem>();
            BuildWalkQueue(_dataset, items);

            return DoWalkAsync(walker, items);
        }

        /// <summary>
        /// Populate the <paramref name="items"/> queue.
        /// </summary>
        /// <param name="dataset">Source of population.</param>
        /// <param name="items">Destination of population.</param>
        private static void BuildWalkQueue(DicomDataset dataset, Queue<DicomItem> items)
        {
            foreach (var item in dataset)
            {
                if (item is DicomElement)
                {
                    items.Enqueue(item);
                }
                else if (item is DicomFragmentSequence)
                {
                    var sq = item as DicomFragmentSequence;
                    items.Enqueue(item);
                    foreach (var fragment in sq)
                    {
                        items.Enqueue(new DicomFragmentItem(fragment));
                    }
                    items.Enqueue(new EndDicomFragment());
                }
                else if (item is DicomSequence)
                {
                    var sq = item as DicomSequence;
                    items.Enqueue(item);
                    foreach (var sqi in sq)
                    {
                        sqi.FallbackEncodings = dataset.GetEncodingsForSerialization();
                        sqi.OnBeforeSerializing();
                        items.Enqueue(new BeginDicomSequenceItem(sqi));
                        BuildWalkQueue(sqi, items);
                        items.Enqueue(new EndDicomSequenceItem());
                    }
                    items.Enqueue(new EndDicomSequence());
                }
            }
        }

        /// <summary>
        /// Perform a dataset walk.
        /// </summary>
        /// <param name="walker">Walker implementation.</param>
        /// <param name="items">Queue of internal items; must be initialized and empty when called from external method.</param>
        private static void DoWalk(IDicomDatasetWalker walker, Queue<DicomItem> items)
        {
            try
            {
                walker.OnBeginWalk();

                while (items.Count > 0)
                {
                    var item = items.Dequeue();

                    if (item is DicomElement)
                    {
                        walker.OnElement(item as DicomElement);
                    }
                    else if (item is DicomFragmentSequence)
                    {
                        walker.OnBeginFragment(item as DicomFragmentSequence);
                    }
                    else if (item is DicomFragmentItem)
                    {
                        walker.OnFragmentItem((item as DicomFragmentItem).Buffer);
                    }
                    else if (item is EndDicomFragment)
                    {
                        walker.OnEndFragment();
                    }
                    else if (item is DicomSequence)
                    {
                        walker.OnBeginSequence(item as DicomSequence);
                    }
                    else if (item is BeginDicomSequenceItem)
                    {
                        walker.OnBeginSequenceItem((item as BeginDicomSequenceItem).Dataset);
                    }
                    else if (item is EndDicomSequenceItem)
                    {
                        walker.OnEndSequenceItem();
                    }
                    else if (item is EndDicomSequence)
                    {
                        walker.OnEndSequence();
                    }
                }

                walker.OnEndWalk();
            }
            catch (Exception e)
            {
                try
                {
                    walker.OnEndWalk();
                    throw;
                }
                catch
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Perform an asynchronous dataset walk.
        /// </summary>
        /// <param name="walker">Walker implementation.</param>
        /// <param name="items">Queue of internal items; must be initialized and empty when called from external method.</param>
        private static async Task DoWalkAsync(IDicomDatasetWalker walker, Queue<DicomItem> items)
        {
            try
            {
                walker.OnBeginWalk();

                while (items.Count > 0)
                {
                    var item = items.Dequeue();

                    if (item is DicomElement)
                    {
                        await walker.OnElementAsync(item as DicomElement).ConfigureAwait(false);
                    }
                    else if (item is DicomFragmentSequence)
                    {
                        walker.OnBeginFragment(item as DicomFragmentSequence);
                    }
                    else if (item is DicomFragmentItem)
                    {
                        await walker.OnFragmentItemAsync((item as DicomFragmentItem).Buffer).ConfigureAwait(false);
                    }
                    else if (item is EndDicomFragment)
                    {
                        walker.OnEndFragment();
                    }
                    else if (item is DicomSequence)
                    {
                        walker.OnBeginSequence(item as DicomSequence);
                    }
                    else if (item is BeginDicomSequenceItem)
                    {
                        walker.OnBeginSequenceItem((item as BeginDicomSequenceItem).Dataset);
                    }
                    else if (item is EndDicomSequenceItem)
                    {
                        walker.OnEndSequenceItem();
                    }
                    else if (item is EndDicomSequence)
                    {
                        walker.OnEndSequence();
                    }
                }
            }
            finally
            {
                walker.OnEndWalk();
            }
        }

        #endregion
    }
}
