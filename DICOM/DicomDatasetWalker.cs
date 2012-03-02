using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Dicom.IO.Buffer;

namespace Dicom {
    public delegate void DicomDatasetWalkerCallback();

    public interface IDicomDatasetWalker {
		void OnBeginWalk(DicomDatasetWalker walker, DicomDatasetWalkerCallback callback);
        bool OnElement(DicomElement element);
        bool OnBeginSequence(DicomSequence sequence);
        bool OnBeginSequenceItem(DicomDataset dataset);
        bool OnEndSequenceItem();
        bool OnEndSequence();
        bool OnBeginFragment(DicomFragmentSequence fragment);
        bool OnFragmentItem(IByteBuffer item);
        bool OnEndFragment();
		void OnEndWalk();
    }

    public class DicomDatasetWalker {
		#region State Items
		private class BeginDicomSequenceItem : DicomItem {
			public BeginDicomSequenceItem(DicomDataset item) : base(DicomTag.Unknown) {
				Dataset = item;
			}

			public DicomDataset Dataset {
				get;
				private set;
			}

			public override DicomVR ValueRepresentation {
				get { return DicomVR.NONE; }
			}
		}

		private class EndDicomSequenceItem : DicomItem {
			public EndDicomSequenceItem() : base(DicomTag.Unknown) { }

			public override DicomVR ValueRepresentation {
				get { return DicomVR.NONE; }
			}
		}

		private class EndDicomSequence : DicomItem {
			public EndDicomSequence() : base(DicomTag.Unknown) { }

			public override DicomVR ValueRepresentation {
				get { return DicomVR.NONE; }
			}
		}

		private class DicomFragmentItem : DicomItem {
			public DicomFragmentItem(IByteBuffer buffer) : base(DicomTag.Unknown) {
				Buffer = buffer;
			}

			public IByteBuffer Buffer {
				get;
				private set;
			}

			public override DicomVR ValueRepresentation {
				get { return DicomVR.NONE; }
			}
		}

		private class EndDicomFragment : DicomItem {
			public EndDicomFragment() : base(DicomTag.Unknown) { }

			public override DicomVR ValueRepresentation {
				get { return DicomVR.NONE; }
			}
		}
		#endregion

		private IEnumerable<DicomItem> _dataset;
        private Queue<DicomItem> _items;
        private IDicomDatasetWalker _walker;

        private EventAsyncResult _async;
        private Exception _exception;

        public DicomDatasetWalker(IEnumerable<DicomItem> dataset) {
			_dataset = dataset;
        }

        public void Walk(IDicomDatasetWalker walker) {
			EndWalk(BeginWalk(walker, null, null));
        }

        public IAsyncResult BeginWalk(IDicomDatasetWalker walker, AsyncCallback callback, object state) {
			_walker = walker;
			_exception = null;
			_async = new EventAsyncResult(callback, state);
			ThreadPool.QueueUserWorkItem(Walk, null);
			return _async;
        }

        public void EndWalk(IAsyncResult result) {
            result.AsyncWaitHandle.WaitOne();

            if (_exception != null)
                throw _exception;
        }

        private void NextWalkItem() {
			_items.Dequeue();
			ThreadPool.QueueUserWorkItem(Walk, null);
        }

		private void BuildWalkQueue(IEnumerable<DicomItem> dataset) {
			foreach (DicomItem item in dataset) {
				if (item is DicomElement) {
					_items.Enqueue(item);
				} else if (item is DicomFragmentSequence) {
					DicomFragmentSequence sq = item as DicomFragmentSequence;
					_items.Enqueue(item);
					foreach (IByteBuffer fragment in sq) {
						_items.Enqueue(new DicomFragmentItem(fragment));
					}
					_items.Enqueue(new EndDicomFragment());
				} else if (item is DicomSequence) {
					DicomSequence sq = item as DicomSequence;
					_items.Enqueue(item);
					foreach (DicomDataset sqi in sq) {
						_items.Enqueue(new BeginDicomSequenceItem(sqi));
						BuildWalkQueue(sqi);
						_items.Enqueue(new EndDicomSequenceItem());
					}
					_items.Enqueue(new EndDicomSequence());
				}
			}
		}

        private void Walk(object state) {
            try {
                if (_items == null) {
                    _items = new Queue<DicomItem>();
					BuildWalkQueue(_dataset);
					_walker.OnBeginWalk(this, NextWalkItem);
                }

				DicomItem item = null;
				while (_items.Count > 0) {
					item = _items.Peek();

					if (item is DicomElement) {
						if (!_walker.OnElement(item as DicomElement))
							return;
					} else if (item is DicomFragmentSequence) {
						if (!_walker.OnBeginFragment(item as DicomFragmentSequence))
							return;
					} else if (item is DicomFragmentItem) {
						if (!_walker.OnFragmentItem((item as DicomFragmentItem).Buffer))
							return;
					} else if (item is EndDicomFragment) {
						if (!_walker.OnEndFragment())
							return;
					} else if (item is DicomSequence) {
						if (!_walker.OnBeginSequence(item as DicomSequence))
							return;
					} else if (item is BeginDicomSequenceItem) {
						if (!_walker.OnBeginSequenceItem((item as BeginDicomSequenceItem).Dataset))
							return;
					} else if (item is EndDicomSequenceItem) {
						if (!_walker.OnEndSequenceItem())
							return;
					} else if (item is EndDicomSequence) {
						if (!_walker.OnEndSequence())
							return;
					}

					_items.Dequeue();
				}

				_walker.OnEndWalk();

				_items = null;
                _async.Set();
            } catch (Exception e) {
				try {
					_walker.OnEndWalk();
				} catch {
				}
                _exception = e;
				_items = null;
                _async.Set();
            }
        }
    }
}
