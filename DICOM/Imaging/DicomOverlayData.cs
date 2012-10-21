using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO.Buffer;

namespace Dicom.Imaging {
	public enum DicomOverlayType {
		/// <summary>Graphic overlay</summary>
		Graphics,

		/// <summary>Region of Interest</summary>
		ROI
	}

	/// <summary>
	/// DICOM image overlay class
	/// </summary>
	public class DicomOverlayData {
		#region Private Members
		private ushort _group;

		private int _rows;
		private int _columns;
		private DicomOverlayType _type;
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
		public DicomOverlayType Type {
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
				// ensure that 6000 group is actually an overlay group
				if (ds.Get<DicomElement>(new DicomTag(group, 0x0010)).ValueRepresentation != DicomVR.US)
					continue;

				try {
					DicomOverlayData overlay = new DicomOverlayData(ds, group);
					overlays.Add(overlay);
				} catch {
					// bail out if not an overlay group
				}
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
			
			var type = ds.Get<string>(OverlayTag(DicomTag.OverlayType), "Unknown");
			if (type.StartsWith("R"))
				_type = DicomOverlayType.ROI;
			else
				_type = DicomOverlayType.Graphics;

			DicomTag tag = OverlayTag(DicomTag.OverlayOrigin);
			if (ds.Contains(tag)) {
				_originX = ds.Get<short>(tag, 0, 1);
				_originY = ds.Get<short>(tag, 1, 1);
			}

			_bitsAllocated = ds.Get<ushort>(OverlayTag(DicomTag.OverlayBitsAllocated), 0, 1);
			_bitPosition = ds.Get<ushort>(OverlayTag(DicomTag.OverlayBitPosition), 0, 0);

			tag = OverlayTag(DicomTag.OverlayData);
			if (ds.Contains(tag)) {
				var elem = ds.FirstOrDefault(x => x.Tag == tag) as DicomElement;
				_data = elem.Buffer;
			} else {
				// overlay embedded in high bits of pixel data
				if (ds.InternalTransferSyntax.IsEncapsulated)
					throw new DicomImagingException("Attempted to extract embedded overlay from compressed pixel data. Decompress pixel data before attempting this operation.");

				var pixels = DicomPixelData.Create(ds);

				// (1,1) indicates top left pixel of image
				int ox = Math.Max(0, _originX - 1);
				int oy = Math.Max(0, _originY - 1);
				int ow = Math.Min(_rows, pixels.Width - _rows - ox);
				int oh = Math.Min(_columns, pixels.Height - _columns - oy);

				var frame = pixels.GetFrame(0);

				// calculate length of output buffer
				var count = (_rows * _columns) / 8;
				if (((_rows * _columns) % 8) != 0)
					count++;
				if ((count & 1) != 0)
					count++;

				var bytes = new byte[count];
				var bits = new BitArray(bytes);
				int mask = 1 << _bitPosition;

				if (pixels.BitsAllocated == 8) {
					var data = ByteBufferEnumerator<byte>.Create(frame).ToArray();

					for (int y = oy; y < oh; y++) {
						int n = (y * pixels.Width) + ox;
						int i = (y - oy) * _columns;
						for (int x = ox; x < ow; x++) {
							if ((data[n] & mask) != 0)
								bits[i] = true;
							n++;
							i++;
						}
					}
				} else if (pixels.BitsAllocated == 16) {
					// we don't really care if the pixel data is signed or not
					var data = ByteBufferEnumerator<ushort>.Create(frame).ToArray();

					for (int y = oy; y < oh; y++) {
						int n = (y * pixels.Width) + ox;
						int i = (y - oy) * _columns;
						for (int x = ox; x < ow; x++) {
							if ((data[n] & mask) != 0)
								bits[i] = true;
							n++;
							i++;
						}
					}
				} else {
					throw new DicomImagingException("Unable to extract embedded overlay from pixel data with bits stored greater than 16.");
				}

				_data = new MemoryByteBuffer(bytes);
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
