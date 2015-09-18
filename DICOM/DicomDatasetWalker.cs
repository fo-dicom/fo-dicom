// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Threading;

using Dicom.IO.Buffer;

namespace Dicom
{
    /// <summary>
    /// Callback delegate for DICOM dataset traversal.
    /// </summary>
    public delegate void DicomDatasetWalkerCallback();

    /// <summary>
    /// Interface for traversing a DICOM dataset.
    /// </summary>
    public interface IDicomDatasetWalker
    {
        /// <summary>
        /// Handler for beginning the traversal.
        /// </summary>
        /// <param name="walker">Walker performing the actual operations.</param>
        /// <param name="callback">Callback method to be called when an On... method returns false.</param>
        void OnBeginWalk(DicomDatasetWalker walker, DicomDatasetWalkerCallback callback);

        /// <summary>
        /// Handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnElement(DicomElement element);

        /// <summary>
        /// Handler for traversing beginning of sequence.
        /// </summary>
        /// <param name="sequence">Sequence to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnBeginSequence(DicomSequence sequence);

        /// <summary>
        /// Handler for traversing beginning of sequence item.
        /// </summary>
        /// <param name="dataset">Item dataset.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnBeginSequenceItem(DicomDataset dataset);

        /// <summary>
        /// Handler for traversing end of sequence item.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnEndSequenceItem();

        /// <summary>
        /// Handler for traversing end of sequence.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnEndSequence();

        /// <summary>
        /// Handler for traversing beginning of fragment.
        /// </summary>
        /// <param name="fragment">Fragment sequence.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnBeginFragment(DicomFragmentSequence fragment);

        /// <summary>
        /// Handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnFragmentItem(IByteBuffer item);

        /// <summary>
        /// Handler for traversing end of fragment.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="OnBeginWalk"/> before returning.</remarks>
        bool OnEndFragment();

        /// <summary>
        /// Handler for end of traversal.
        /// </summary>
        void OnEndWalk();
    }

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

            public override DicomVR ValueRepresentation
            {
                get
                {
                    return DicomVR.NONE;
                }
            }
        }

        private class EndDicomSequenceItem : DicomItem
        {
            public EndDicomSequenceItem()
                : base(DicomTag.Unknown)
            {
            }

            public override DicomVR ValueRepresentation
            {
                get
                {
                    return DicomVR.NONE;
                }
            }
        }

        private class EndDicomSequence : DicomItem
        {
            public EndDicomSequence()
                : base(DicomTag.Unknown)
            {
            }

            public override DicomVR ValueRepresentation
            {
                get
                {
                    return DicomVR.NONE;
                }
            }
        }

        private class DicomFragmentItem : DicomItem
        {
            public DicomFragmentItem(IByteBuffer buffer)
                : base(DicomTag.Unknown)
            {
                Buffer = buffer;
            }

            public IByteBuffer Buffer { get; private set; }

            public override DicomVR ValueRepresentation
            {
                get
                {
                    return DicomVR.NONE;
                }
            }
        }

        private class EndDicomFragment : DicomItem
        {
            public EndDicomFragment()
                : base(DicomTag.Unknown)
            {
            }

            public override DicomVR ValueRepresentation
            {
                get
                {
                    return DicomVR.NONE;
                }
            }
        }

        #endregion

        #region Fields

        private readonly IEnumerable<DicomItem> _dataset;

        private Queue<DicomItem> _items;

        private IDicomDatasetWalker _walker;

        private EventAsyncResult _async;

        private Exception _exception;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of a <see cref="DicomDatasetWalker"/>.
        /// </summary>
        /// <param name="dataset"></param>
        public DicomDatasetWalker(IEnumerable<DicomItem> dataset)
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
            var items = new Queue<DicomItem>();
            Walk(walker, this._dataset, items);
        }

        public IAsyncResult BeginWalk(IDicomDatasetWalker walker, AsyncCallback callback, object state)
        {
            _walker = walker;
            _exception = null;
            _async = new EventAsyncResult(callback, state);
            ThreadPool.QueueUserWorkItem(Walk, null);
            return _async;
        }

        public void EndWalk(IAsyncResult result)
        {
            result.AsyncWaitHandle.WaitOne();

            if (_exception != null) throw _exception;
        }

        private void NextWalkItem()
        {
            _items.Dequeue();
            ThreadPool.QueueUserWorkItem(Walk, null);
        }

        private void BuildWalkQueue(IEnumerable<DicomItem> dataset)
        {
            foreach (DicomItem item in dataset)
            {
                if (item is DicomElement)
                {
                    _items.Enqueue(item);
                }
                else if (item is DicomFragmentSequence)
                {
                    DicomFragmentSequence sq = item as DicomFragmentSequence;
                    _items.Enqueue(item);
                    foreach (IByteBuffer fragment in sq)
                    {
                        _items.Enqueue(new DicomFragmentItem(fragment));
                    }
                    _items.Enqueue(new EndDicomFragment());
                }
                else if (item is DicomSequence)
                {
                    DicomSequence sq = item as DicomSequence;
                    _items.Enqueue(item);
                    foreach (DicomDataset sqi in sq)
                    {
                        _items.Enqueue(new BeginDicomSequenceItem(sqi));
                        BuildWalkQueue(sqi);
                        _items.Enqueue(new EndDicomSequenceItem());
                    }
                    _items.Enqueue(new EndDicomSequence());
                }
            }
        }

        private void Walk(object state)
        {
            try
            {
                if (_items == null)
                {
                    _items = new Queue<DicomItem>();
                    BuildWalkQueue(_dataset);
                    _walker.OnBeginWalk(this, NextWalkItem);
                }

                DicomItem item = null;
                while (_items.Count > 0)
                {
                    item = _items.Peek();

                    if (item is DicomElement)
                    {
                        if (!_walker.OnElement(item as DicomElement)) return;
                    }
                    else if (item is DicomFragmentSequence)
                    {
                        if (!_walker.OnBeginFragment(item as DicomFragmentSequence)) return;
                    }
                    else if (item is DicomFragmentItem)
                    {
                        if (!_walker.OnFragmentItem((item as DicomFragmentItem).Buffer)) return;
                    }
                    else if (item is EndDicomFragment)
                    {
                        if (!_walker.OnEndFragment()) return;
                    }
                    else if (item is DicomSequence)
                    {
                        if (!_walker.OnBeginSequence(item as DicomSequence)) return;
                    }
                    else if (item is BeginDicomSequenceItem)
                    {
                        if (!_walker.OnBeginSequenceItem((item as BeginDicomSequenceItem).Dataset)) return;
                    }
                    else if (item is EndDicomSequenceItem)
                    {
                        if (!_walker.OnEndSequenceItem()) return;
                    }
                    else if (item is EndDicomSequence)
                    {
                        if (!_walker.OnEndSequence()) return;
                    }

                    _items.Dequeue();
                }

                _walker.OnEndWalk();

                _items = null;
                _async.Set();
            }
            catch (Exception e)
            {
                try
                {
                    _walker.OnEndWalk();
                }
                catch
                {
                }
                _exception = e;
                _items = null;
                _async.Set();
            }
        }

        /// <summary>
        /// Perform a dataset walk.
        /// </summary>
        /// <param name="walker">Walker implementation.</param>
        /// <param name="dataset">DICOM dataset subject to traversal.</param>
        /// <param name="items">Queue of internal items; must be initialized and empty when called from external method.</param>
        /// <param name="initialize">True for initializing the walk, false otherwise. Must be true when called from external method.</param>
        private static void Walk(
            IDicomDatasetWalker walker,
            IEnumerable<DicomItem> dataset,
            Queue<DicomItem> items,
            bool initialize = true)
        {
            try
            {
                if (initialize)
                {
                    BuildWalkQueue(dataset, items);
                    walker.OnBeginWalk(
                        null,
                        () =>
                            {
                                items.Dequeue();
                                Walk(walker, dataset, items, false);
                            });
                }

                while (items.Count > 0)
                {
                    var item = items.Peek();

                    if (item is DicomElement)
                    {
                        if (!walker.OnElement(item as DicomElement)) return;
                    }
                    else if (item is DicomFragmentSequence)
                    {
                        if (!walker.OnBeginFragment(item as DicomFragmentSequence)) return;
                    }
                    else if (item is DicomFragmentItem)
                    {
                        if (!walker.OnFragmentItem((item as DicomFragmentItem).Buffer)) return;
                    }
                    else if (item is EndDicomFragment)
                    {
                        if (!walker.OnEndFragment()) return;
                    }
                    else if (item is DicomSequence)
                    {
                        if (!walker.OnBeginSequence(item as DicomSequence)) return;
                    }
                    else if (item is BeginDicomSequenceItem)
                    {
                        if (!walker.OnBeginSequenceItem((item as BeginDicomSequenceItem).Dataset)) return;
                    }
                    else if (item is EndDicomSequenceItem)
                    {
                        if (!walker.OnEndSequenceItem()) return;
                    }
                    else if (item is EndDicomSequence)
                    {
                        if (!walker.OnEndSequence()) return;
                    }

                    items.Dequeue();
                }

                walker.OnEndWalk();
            }
            catch (Exception e)
            {
                try
                {
                    walker.OnEndWalk();
                }
                finally
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Populate the <paramref name="items"/> queue.
        /// </summary>
        /// <param name="dataset">Source of population.</param>
        /// <param name="items">Destination of polpulation.</param>
        private static void BuildWalkQueue(IEnumerable<DicomItem> dataset, Queue<DicomItem> items)
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
                        items.Enqueue(new BeginDicomSequenceItem(sqi));
                        BuildWalkQueue(sqi, items);
                        items.Enqueue(new EndDicomSequenceItem());
                    }
                    items.Enqueue(new EndDicomSequence());
                }
            }
        }

        #endregion
    }
}
