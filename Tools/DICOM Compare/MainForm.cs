using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Dicom;
using Dicom.IO.Buffer;

namespace Dicom.Compare {
	public partial class MainForm : Form {
		private readonly static Color None = Color.Transparent;
		private readonly static Color Green = Color.FromArgb(190, 240, 190);
		private readonly static Color Yellow = Color.FromArgb(255, 255, 217);
		private readonly static Color Red = Color.FromArgb(255, 200, 200);
		private readonly static Color Gray = Color.FromArgb(200, 200, 200);

		private DicomFile _file1;
		private DicomFile _file2;

		private int _level = 0;
		private string _indent = String.Empty;

		public MainForm() {
			InitializeComponent();
		}

		public int Level {
			get { return _level; }
			set {
				_level = value;
				_indent = String.Empty;
				for (int i = 0; i < Level; i++)
					_indent += "    ";
			}
		}

		private void OnClickSelect(object sender, EventArgs e) {
			DicomFile file1 = null;
			while (true) {
				var ofd = new OpenFileDialog();
				ofd.Title = "Choose first DICOM file";
				ofd.Filter = "DICOM Files (*.dcm;*.dic)|*.dcm;*.dic|All Files (*.*)|*.*";

				if (ofd.ShowDialog(this) == DialogResult.Cancel)
					return;

				try {
					file1 = DicomFile.Open(ofd.FileName);
					break;
				} catch (Exception ex) {
					MessageBox.Show(this, ex.Message, "Error opening DICOM file", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			DicomFile file2 = null;
			while (true) {
				var ofd = new OpenFileDialog();
				ofd.Title = "Choose second DICOM file";
				ofd.Filter = "DICOM Files (*.dcm;*.dic)|*.dcm;*.dic|All Files (*.*)|*.*";

				if (ofd.ShowDialog(this) == DialogResult.Cancel)
					return;

				try {
					file2 = DicomFile.Open(ofd.FileName);
					break;
				} catch (Exception ex) {
					MessageBox.Show(this, ex.Message, "Error opening DICOM file", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			_file1 = file1;
			_file2 = file2;

			lblFile1.Text = _file1.File.Name;
			lblFile2.Text = _file2.File.Name;

			CompareFiles();
		}

		private void CompareFiles() {
			Level = 0;

			try {
				lvFile1.BeginUpdate();
				lvFile2.BeginUpdate();

				lvFile1.Items.Clear();
				lvFile2.Items.Clear();

				CompareDatasets(_file1.FileMetaInfo, _file2.FileMetaInfo);
				CompareDatasets(_file1.Dataset, _file2.Dataset);

				OnSizeChanged(lvFile1, EventArgs.Empty);
				OnSizeChanged(lvFile2, EventArgs.Empty);
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, "Error comparing DICOM files", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} finally {
				lvFile1.EndUpdate();
				lvFile2.EndUpdate();
			}
		}

		private void CompareDatasets(DicomDataset d1, DicomDataset d2) {
			var e1 = new Queue<DicomItem>(d1 ?? new DicomDataset());
			var e2 = new Queue<DicomItem>(d2 ?? new DicomDataset());

			while (e1.Count > 0 || e2.Count > 0) {
				DicomItem i1 = null;
				if (e1.Count > 0)
					i1 = e1.Peek();

				DicomItem i2 = null;
				if (e2.Count > 0)
					i2 = e2.Peek();

				if (i1 != null) {
					if (i2 != null) {
						if (i1.Tag.Group < i2.Tag.Group) {
							AddItem(i1, null);
							e1.Dequeue();
							continue;
						}

						if (i1.Tag.Group == i2.Tag.Group && i1.Tag.Element < i2.Tag.Element) {
							AddItem(i1, null);
							e1.Dequeue();
							continue;
						}
					}
				}

				if (i2 != null) {
					if (i1 != null) {
						if (i2.Tag.Group < i1.Tag.Group) {
							AddItem(null, i2);
							e2.Dequeue();
							continue;
						}

						if (i2.Tag.Group == i1.Tag.Group && i2.Tag.Element < i1.Tag.Element) {
							AddItem(null, i2);
							e2.Dequeue();
							continue;
						}
					}
				}

				AddItem(i1, i2);

				if (i1 != null)
					e1.Dequeue();

				if (i2 != null)
					e2.Dequeue();
			}
		}

		private void CompareSequences(DicomSequence s1, DicomSequence s2) {
			if (s1 == null) {
				AddItem(s1, lvFile1, Gray);
				AddItem(s2, lvFile2, Green);
			} else if (s2 == null) {
				AddItem(s1, lvFile1, Green);
				AddItem(s2, lvFile2, Gray);
			} else {
				AddItem(s1, lvFile1, None);
				AddItem(s2, lvFile2, None);
			}

			Level++;

			int count = 0;
			if (s1 != null)
				count = s1.Items.Count;
			if (s2 != null && s2.Items.Count > count)
				count = s2.Items.Count;

			for (int i = 0; i < count; i++) {
				DicomDataset d1 = null;
				if (s1 != null && i < s1.Items.Count)
					d1 = s1.Items[i];

				DicomDataset d2 = null;
				if (s2 != null && i < s2.Items.Count)
					d2 = s2.Items[i];

				if (d1 == null) {
					AddItem(String.Empty, UInt32.MaxValue, String.Empty, lvFile1, Gray);
					AddItem(GetTagName(DicomTag.Item), UInt32.MaxValue, String.Empty, lvFile2, Green);
				} else if (d2 == null) {
					AddItem(GetTagName(DicomTag.Item), UInt32.MaxValue, String.Empty, lvFile1, Green);
					AddItem(String.Empty, UInt32.MaxValue, String.Empty, lvFile2, Gray);
				} else {
					AddItem(GetTagName(DicomTag.Item), UInt32.MaxValue, String.Empty, lvFile1, None);
					AddItem(GetTagName(DicomTag.Item), UInt32.MaxValue, String.Empty, lvFile2, None);
				}

				Level++;
				CompareDatasets(d1, d2);
				Level--;
			}

			Level--;
		}

		private void CompareFragments(DicomItem i1, DicomItem i2) {
			DicomFragmentSequence s1 = null;
			DicomFragmentSequence s2 = null;

			bool pixel = cbIgnorePixelData.Checked && i1.Tag == DicomTag.PixelData;

			if (i1 == null) {
				AddItem(i1, lvFile1, Gray);
				AddItem(i2, lvFile2, Green);
				s2 = i2 as DicomFragmentSequence;
			} else if (i2 == null) {
				AddItem(i1, lvFile1, Green);
				AddItem(i2, lvFile2, Gray);
				s1 = i1 as DicomFragmentSequence;
			} else if (!(i1 is DicomFragmentSequence)) {
				AddItem(i1, lvFile1, pixel ? Yellow : Red);
				AddItem(i2, lvFile2, pixel ? Yellow : Red);
				s2 = i2 as DicomFragmentSequence;
			} else if (!(i2 is DicomFragmentSequence)) {
				AddItem(i1, lvFile1, pixel ? Yellow : Red);
				AddItem(i2, lvFile2, pixel ? Yellow : Red);
				s1 = i1 as DicomFragmentSequence;
			} else {
				AddItem(i1, lvFile1, pixel ? Yellow : None);
				AddItem(i2, lvFile2, pixel ? Yellow : None);
				s1 = i1 as DicomFragmentSequence;
				s2 = i2 as DicomFragmentSequence;
			}

			Level++;

			if (s1 == null) {
				AddItem(String.Empty, UInt32.MaxValue, String.Empty, lvFile1, Gray);
				AddItem(_indent + "Offset Table", (uint)s2.OffsetTable.Count * 4, String.Format("@entries={0}", s2.OffsetTable.Count), lvFile2, pixel ? Yellow : Red);
			} else if (s2 == null) {
				AddItem(_indent + "Offset Table", (uint)s1.OffsetTable.Count * 4, String.Format("@entries={0}", s1.OffsetTable.Count), lvFile1, pixel ? Yellow : Red);
				AddItem(String.Empty, UInt32.MaxValue, String.Empty, lvFile2, Gray);
			} else {
				Color c = None;
				if (s1.OffsetTable.Count != s2.OffsetTable.Count)
					c = Red;
				else {
					for (int i = 0; i < s1.OffsetTable.Count; i++) {
						if (s1.OffsetTable[i] != s2.OffsetTable[i]) {
							c = Red;
							break;
						}
					}
				}
				AddItem(_indent + "Offset Table", (uint)s2.OffsetTable.Count * 4, String.Format("@entries={0}", s1.OffsetTable.Count), lvFile1, pixel ? Yellow : c);
				AddItem(_indent + "Offset Table", (uint)s2.OffsetTable.Count * 4, String.Format("@entries={0}", s2.OffsetTable.Count), lvFile2, pixel ? Yellow : c);
			}

			int count = 0;
			if (s1 != null)
				count = s1.Fragments.Count;
			if (s2 != null && s2.Fragments.Count > count)
				count = s2.Fragments.Count;

			string name = _indent + "Fragment";

			for (int i = 0; i < count; i++) {
				IByteBuffer b1 = null;
				if (s1 != null && i < s1.Fragments.Count)
					b1 = s1.Fragments[i];

				IByteBuffer b2 = null;
				if (s2 != null && i < s2.Fragments.Count)
					b2 = s2.Fragments[i];

				if (b1 == null) {
					AddItem(String.Empty, UInt32.MaxValue, String.Empty, lvFile1, Gray);
					AddItem(name, b2.Size, String.Empty, lvFile2, pixel ? Yellow : Red);
					continue;
				} else if (b2 == null) {
					AddItem(name, b1.Size, String.Empty, lvFile1, pixel ? Yellow : Red);
					AddItem(String.Empty, UInt32.MaxValue, String.Empty, lvFile2, Gray);
					continue;
				}

				Color c = None;
				if (pixel)
					c = Yellow;
				else if (!Compare(b1.Data, b2.Data))
					c = Red;

				AddItem(name, b1.Size, String.Empty, lvFile1, c);
				AddItem(name, b2.Size, String.Empty, lvFile2, c);
			}

			Level--;
		}

		private string GetTagName(DicomTag t) {
			return String.Format("{0}{1}  {2}", _indent, t.ToString().ToUpper(), t.DictionaryEntry.Name);
		}

		private void AddItem(string t, uint l, string v, ListView lv, Color c) {
			var lvi = lv.Items.Add(t);
			lvi.SubItems.Add(!String.IsNullOrEmpty(t) ? "--" : String.Empty);
			if (l == UInt32.MaxValue)
				lvi.SubItems.Add(!String.IsNullOrEmpty(t) ? "-" : String.Empty);
			else
				lvi.SubItems.Add(l.ToString());
			lvi.SubItems.Add(v);
			lvi.UseItemStyleForSubItems = true;
			lvi.BackColor = c;
		}

		private void AddItem(DicomItem i, ListView lv, Color c) {
			ListViewItem lvi = null;

			if (i != null) {
				var tag = GetTagName(i.Tag);
				lvi = lv.Items.Add(tag);
				lvi.SubItems.Add(i.ValueRepresentation.Code);
				if (i is DicomElement) {
					var e = i as DicomElement;
					lvi.SubItems.Add(e.Length.ToString());
					string value = "<large value not displayed>";
					if (e.Length <= 2048)
						value = String.Join("\\", e.Get<string[]>());
					lvi.SubItems.Add(value);
				} else {
					lvi.SubItems.Add("-");
					lvi.SubItems.Add(String.Empty);
				}
				lvi.Tag = i;
			} else {
				lvi = lv.Items.Add(String.Empty);
				lvi.SubItems.Add(String.Empty);
				lvi.SubItems.Add(String.Empty);
				lvi.SubItems.Add(String.Empty);
			}

			lvi.UseItemStyleForSubItems = true;
			lvi.BackColor = c;
		}

		private void AddItem(DicomItem i1, DicomItem i2) {
			if (i1 is DicomSequence || i2 is DicomSequence) {
				CompareSequences(i1 as DicomSequence, i2 as DicomSequence);
				return;
			}

			if (i2 == null) {
				AddItem(i1, lvFile1, Green);
				AddItem(i2, lvFile2, Gray);
				return;
			}

			if (i1 == null) {
				AddItem(i1, lvFile1, Gray);
				AddItem(i2, lvFile2, Green);
				return;
			}

			if (i1 is DicomElement && i2 is DicomElement) {
				var e1 = i1 as DicomElement;
				var e2 = i2 as DicomElement;

				var c = None;
				if (!cbIgnoreVR.Checked && e1.ValueRepresentation != e2.ValueRepresentation)
					c = Red;
				else if(!Compare(e1.Buffer.Data, e2.Buffer.Data))
					c = Red;

				if (cbIgnoreGroupLengths.Checked && e1.Tag.Element == 0x0000)
					c = Yellow;

				if (cbIgnoreUIDs.Checked && e1.ValueRepresentation == DicomVR.UI) {
					var uid = (i1 as DicomElement).Get<DicomUID>(0);
					if (uid != null && (uid.Type == DicomUidType.SOPInstance || uid.Type == DicomUidType.Unknown))
						c = Yellow;
				}

				if (cbIgnorePixelData.Checked && i1.Tag == DicomTag.PixelData)
					c = Yellow;

				AddItem(i1, lvFile1, c);
				AddItem(i2, lvFile2, c);
				return;
			}

			if (i1 is DicomFragmentSequence || i2 is DicomFragmentSequence) {
				CompareFragments(i1, i2);
				return;
			}

			while (i1 is DicomElement || i2 is DicomElement) {
				AddItem(i1, lvFile1, Red);
				AddItem(i2, lvFile2, Red);
				return;
			}

			AddItem(i1, lvFile1, Yellow);
			AddItem(i2, lvFile2, Yellow);
		}

		private static bool Compare(byte[] b1, byte[] b2) {
			if (b1.Length != b2.Length)
				return false;

			for (int i = 0; i < b1.Length; i++) {
				if (b1[i] != b2[i])
					return false;
			}

			return true;
		}

		private void OnScroll(object sender, ScrollEventArgs e) {
			if (sender == lvFile1) {
				int index = lvFile1.TopItem.Index;
				lvFile2.TopItem = lvFile2.Items[index];
			} else {
				int index = lvFile2.TopItem.Index;
				lvFile1.TopItem = lvFile1.Items[index];
			}
		}

		private void OnSelect(object sender, EventArgs e) {
			if (sender == lvFile1) {
				if (lvFile1.SelectedIndices.Count > 0) {
					int index = lvFile1.SelectedIndices[0];
					lvFile2.Items[index].Selected = true;
				}
			} else {
				if (lvFile2.SelectedIndices.Count > 0) {
					int index = lvFile2.SelectedIndices[0];
					lvFile1.Items[index].Selected = true;
				}
			}
		}

		private void OnMouseEnter(object sender, EventArgs e) {
			((Control)sender).Focus();
		}

		private void OnSizeChanged(object sender, EventArgs e) {
			var lv = (ListViewEx)sender;
			var width = lv.Columns[0].Width + lv.Columns[1].Width + lv.Columns[2].Width;
			lv.Columns[3].Width = Math.Max(lv.ClientSize.Width - width, 440);
		}

		private void OnOptionChanged(object sender, EventArgs e) {
			int index = -1;
			if (lvFile1.TopItem != null)
				index = lvFile1.TopItem.Index;

			CompareFiles();

			if (index != -1 && index < lvFile1.Items.Count) {
				lvFile1.TopItem = lvFile1.Items[index];
				lvFile2.TopItem = lvFile2.Items[index];
			}
		}
	}
}
