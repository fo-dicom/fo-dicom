using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Dicom.Imaging.Mathematics;
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
		#region Public Constructors
		/// <summary>
		/// Initializes overlay from DICOM dataset and overlay group.
		/// </summary>
		/// <param name="ds">Dataset</param>
		/// <param name="group">Overlay group</param>
		public DicomOverlayData(DicomDataset ds, ushort group) {
			Group = group;
			Dataset = ds;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// DICOM Dataset
		/// </summary>
		public DicomDataset Dataset {
			get;
			private set;
		}

		/// <summary>
		/// Overlay group
		/// </summary>
		public ushort Group {
			get;
			private set;
		}

		/// <summary>
		/// Number of rows in overlay
		/// </summary>
		public int Rows {
			get { return Dataset.Get<ushort>(OverlayTag(DicomTag.OverlayRows)); }
			set { Dataset.Add(OverlayTag(DicomTag.OverlayRows), (ushort)value); }
		}

		/// <summary>
		/// Number of columns in overlay
		/// </summary>
		public int Columns {
			get { return Dataset.Get<ushort>(OverlayTag(DicomTag.OverlayColumns)); }
			set { Dataset.Add(OverlayTag(DicomTag.OverlayColumns), (ushort)value); }
		}

		/// <summary>
		/// Overlay type
		/// </summary>
		public DicomOverlayType Type {
			get {
				var type = Dataset.Get<string>(OverlayTag(DicomTag.OverlayType), "Unknown");
				if (type.StartsWith("R"))
					return DicomOverlayType.ROI;
				else
					return DicomOverlayType.Graphics;
			}
			set {
				Dataset.Add(OverlayTag(DicomTag.OverlayType), value.ToString().ToUpper());
			}
		}

		/// <summary>
		/// Number of bits allocated in overlay data
		/// </summary>
		public int BitsAllocated {
			get { return Dataset.Get<ushort>(OverlayTag(DicomTag.OverlayBitsAllocated), 0, 1); }
			set { Dataset.Add(OverlayTag(DicomTag.OverlayBitsAllocated), (ushort)value); }
		}

		/// <summary>
		/// Bit position of embedded overlay
		/// </summary>
		public int BitPosition {
			get { return Dataset.Get<ushort>(OverlayTag(DicomTag.OverlayBitPosition), 0, 1); }
			set { Dataset.Add(OverlayTag(DicomTag.OverlayBitPosition), (ushort)value); }
		}

		/// <summary>
		/// Description of overlay
		/// </summary>
		public string Description {
			get { return Dataset.Get<string>(OverlayTag(DicomTag.OverlayDescription), String.Empty); }
			set { Dataset.Add(DicomTag.OverlayDescription, value); }
		}

		/// <summary>
		/// Subtype
		/// </summary>
		public string Subtype {
			get { return Dataset.Get<string>(OverlayTag(DicomTag.OverlaySubtype), String.Empty); }
			set { Dataset.Add(DicomTag.OverlaySubtype, value); }
		}

		/// <summary>
		/// Overlay label
		/// </summary>
		public string Label {
			get { return Dataset.Get<string>(OverlayTag(DicomTag.OverlayLabel), String.Empty); }
			set { Dataset.Add(DicomTag.OverlayLabel, value); }
		}

		/// <summary>
		/// Number of frames
		/// </summary>
		public int NumberOfFrames {
			get { return Dataset.Get<int>(OverlayTag(DicomTag.NumberOfFramesInOverlay), 0, 1); }
			set { Dataset.Add(OverlayTag(DicomTag.NumberOfFramesInOverlay), value); }
		}

		/// <summary>
		/// First frame of overlay
		/// </summary>
		public int OriginFrame {
			get { return Dataset.Get<ushort>(OverlayTag(DicomTag.ImageFrameOrigin), 0, 1); }
			set { Dataset.Add(OverlayTag(DicomTag.ImageFrameOrigin), (ushort)value); }
		}

		/// <summary>
		/// Position of the first column of an overlay
		/// </summary>
		public int OriginX {
			get { return Dataset.Get<short>(OverlayTag(DicomTag.OverlayOrigin), 0, 1); }
			set { Dataset.Add(OverlayTag(DicomTag.OverlayOrigin), (short)value, (short)OriginY); }
		}

		/// <summary>
		/// Position of the first row of an overlay
		/// </summary>
		public int OriginY {
			get { return Dataset.Get<short>(OverlayTag(DicomTag.OverlayOrigin), 1, 1); }
			set { Dataset.Add(OverlayTag(DicomTag.OverlayOrigin), (short)OriginX, (short)value); }
		}

		/// <summary>
		/// Overlay data
		/// </summary>
		public IByteBuffer Data {
			get { return Load(); }
			set { Dataset.Add(new DicomOtherWord(OverlayTag(DicomTag.OverlayData), value)); }
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
			BitArray bits = new BitArray(Data.Data);
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

		/// <summary>
		/// Creates a DICOM overlay from a GDI+ Bitmap.
		/// </summary>
		/// <param name="ds">Dataset</param>
		/// <param name="bitmap">Bitmap</param>
		/// <param name="mask">Color mask for overlay</param>
		/// <returns>DICOM overlay</returns>
		public static DicomOverlayData FromBitmap(DicomDataset ds, Bitmap bitmap, Color mask) {
			ushort group = 0x6000;
			while (ds.Contains(new DicomTag(group, DicomTag.OverlayBitPosition.Element)))
				group += 2;

			var overlay = new DicomOverlayData(ds, group);
			overlay.Type = DicomOverlayType.Graphics;
			overlay.Rows = bitmap.Height;
			overlay.Columns = bitmap.Width;
			overlay.OriginX = 1;
			overlay.OriginY = 1;
			overlay.BitsAllocated = 1;
			overlay.BitPosition = 1;

			var count = overlay.Rows * overlay.Columns / 8;
			if ((overlay.Rows * overlay.Columns) % 8 > 0)
				count++;

			var array = new BitList();
			array.Capacity = overlay.Rows * overlay.Columns;

			int p = 0;
			for (int y = 0; y < bitmap.Height; y++) {
				for (int x = 0; x < bitmap.Width; x++, p++) {
					if (bitmap.GetPixel(x, y).ToArgb() == mask.ToArgb())
						array[p] = true;
				}
			}

			overlay.Data = EvenLengthBuffer.Create(
									new MemoryByteBuffer(array.Array));

			return overlay;
		}

		public static bool HasEmbeddedOverlays(DicomDataset ds) {
			var groups = new List<ushort>();
			groups.AddRange(ds.Where(x => x.Tag.Group >= 0x6000 && x.Tag.Group <= 0x60FF && x.Tag.Element == 0x0010).Select(x => x.Tag.Group));

			foreach (var group in groups) {
				if (!ds.Contains(new DicomTag(group, DicomTag.OverlayData.Element)))
					return true;
			}

			return false;
		}
		#endregion

		#region Private Methods
		private DicomTag OverlayTag(DicomTag tag) {
			return new DicomTag(Group, tag.Element);
		}

		private IByteBuffer Load() {
			var tag = OverlayTag(DicomTag.OverlayData);
			if (Dataset.Contains(tag)) {
				var elem = Dataset.FirstOrDefault(x => x.Tag == tag) as DicomElement;
				return elem.Buffer;
			} else {
				// overlay embedded in high bits of pixel data
				if (Dataset.InternalTransferSyntax.IsEncapsulated)
					throw new DicomImagingException("Attempted to extract embedded overlay from compressed pixel data. Decompress pixel data before attempting this operation.");

				var pixels = DicomPixelData.Create(Dataset);

				// (1,1) indicates top left pixel of image
				int ox = Math.Max(0, OriginX - 1);
				int oy = Math.Max(0, OriginY - 1);
				int ow = Rows - (pixels.Width - Rows - ox);
				int oh = Columns - (pixels.Height - Columns - oy);

				var frame = pixels.GetFrame(0);

				var bits = new BitList();
				bits.Capacity = Rows * Columns;
				int mask = 1 << BitPosition;

				if (pixels.BitsAllocated == 8) {
					var data = ByteBufferEnumerator<byte>.Create(frame).ToArray();

					for (int y = oy; y < oh; y++) {
						int n = (y * pixels.Width) + ox;
						int i = (y - oy) * Columns;
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
						int i = (y - oy) * Columns;
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

				return new MemoryByteBuffer(bits.Array);
			}
		}
		#endregion
	}
}
