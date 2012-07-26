using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO.Buffer;

namespace Dicom.Imaging {
	/// <summary>
	/// DICOM Overlay
	/// </summary>
	public class DicomOverlayData {
		#region Private Members
		private ushort _group;

		private int _rows;
		private int _columns;
		private string _type;
		private int _originX;
		private int _originY;
		private int _bitsAllocated;
		private int _bitPosition;
		private IByteBuffer _data;

		private string _description;
		private string _subtype;
		private string _label;

		private int _frames;
		private int _frameOrigin;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initializes overlay from DICOM dataset and overlay group.
		/// </summary>
		/// <param name="ds">Dataset</param>
		/// <param name="group">Overlay group</param>
		public DicomOverlayData(DicomDataset ds, ushort group) {
			_group = group;
			Load(ds);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Overlay group
		/// </summary>
		public ushort Group {
			get { return _group; }
		}

		/// <summary>
		/// Number of rows in overlay
		/// </summary>
		public int Rows {
			get { return _rows; }
		}

		/// <summary>
		/// Number of columns in overlay
		/// </summary>
		public int Columns {
			get { return _columns; }
		}

		/// <summary>
		/// Overlay type
		/// </summary>
		public string Type {
			get { return _type; }
		}

		/// <summary>
		/// Position of the first column of an overlay
		/// </summary>
		public int OriginX {
			get { return _originX; }
		}

		/// <summary>
		/// Position of the first row of an overlay
		/// </summary>
		public int OriginY {
			get { return _originY; }
		}

		/// <summary>
		/// Number of bits allocated in overlay data
		/// </summary>
		public int BitsAllocated {
			get { return _bitsAllocated; }
		}

		/// <summary>
		/// Bit position of embedded overlay
		/// </summary>
		public int BitPosition {
			get { return _bitPosition; }
		}

		/// <summary>
		/// Overlay data
		/// </summary>
		public IByteBuffer Data {
			get { return _data; }
		}

		/// <summary>
		/// Description of overlay
		/// </summary>
		public string Description {
			get { return _description; }
		}

		/// <summary>
		/// Subtype
		/// </summary>
		public string Subtype {
			get { return _subtype; }
		}

		/// <summary>
		/// Overlay label
		/// </summary>
		public string Label {
			get { return _label; }
		}

		/// <summary>
		/// Number of frames
		/// </summary>
		public int NumberOfFrames {
			get { return _frames; }
		}

		/// <summary>
		/// First frame of overlay
		/// </summary>
		public int OriginFrame {
			get { return _frameOrigin; }
		}
		#endregion

		#region Public Members
		/// <summary>
		/// Gets the overlay data.
		/// </summary>
		/// <param name="bg">Background color</param>
		/// <param name="fg">Foreground color</param>
		/// <returns>Overlay data</returns>
		public int[] GetOverlayDataS32(int bg, int fg) {
			int[] overlay = new int[Rows * Columns];
			BitArray bits = new BitArray(_data.Data);
			if (bits.Length < overlay.Length)
				throw new DicomDataException("Invalid overlay length: " + bits.Length);
			for (int i = 0, c = overlay.Length; i < c; i++) {
				if (bits.Get(i))
					overlay[i] = fg;
				else
					overlay[i] = bg;
			}
			return overlay;
		}

		/// <summary>
		/// Gets all overlays in a DICOM dataset.
		/// </summary>
		/// <param name="ds">Dataset</param>
		/// <returns>Array of overlays</returns>
		public static DicomOverlayData[] FromDataset(DicomDataset ds) {
			var groups = new List<ushort>();
			groups.AddRange(ds.Where(x => x.Tag.Group >= 0x6000 && x.Tag.Group <= 0x60FF && x.Tag.Element == 0x0010).Select(x => x.Tag.Group));
			var overlays = new List<DicomOverlayData>();
			foreach (var group in groups) {
				DicomOverlayData overlay = new DicomOverlayData(ds, group);
				overlays.Add(overlay);
			}
			return overlays.ToArray();
		}
		#endregion

		#region Private Methods
		private DicomTag OverlayTag(DicomTag tag) {
			return new DicomTag(_group, tag.Element);
		}

		private void Load(DicomDataset ds) {
			_rows = ds.Get<ushort>(OverlayTag(DicomTag.OverlayRows));
			_columns = ds.Get<ushort>(OverlayTag(DicomTag.OverlayColumns));
			_type = ds.Get<string>(OverlayTag(DicomTag.OverlayType), "Unknown");

			DicomTag tag = OverlayTag(DicomTag.OverlayOrigin);
			if (ds.Contains(tag)) {
				short[] xy = ds.Get<short[]>(tag);
				if (xy != null && xy.Length == 2) {
					_originX = xy[0];
					_originY = xy[1];
				}
			}

			_bitsAllocated = ds.Get<ushort>(OverlayTag(DicomTag.OverlayBitsAllocated), 0, 1);
			_bitPosition = ds.Get<ushort>(OverlayTag(DicomTag.OverlayBitPosition), 0, 0);

			tag = OverlayTag(DicomTag.OverlayData);
			if (ds.Contains(tag)) {
				var elem = ds.FirstOrDefault(x => x.Tag == tag) as DicomElement;
				_data = elem.Buffer;
			}

			_description = ds.Get<string>(OverlayTag(DicomTag.OverlayDescription), String.Empty);
			_subtype = ds.Get<string>(OverlayTag(DicomTag.OverlaySubtype), String.Empty);
			_label = ds.Get<string>(OverlayTag(DicomTag.OverlayLabel), String.Empty);

			_frames = ds.Get<int>(OverlayTag(DicomTag.NumberOfFramesInOverlay), 0, 1);
			_frameOrigin = ds.Get<ushort>(OverlayTag(DicomTag.ImageFrameOrigin), 0, 1);

			//TODO: include ROI
		}
		#endregion
	}
}
