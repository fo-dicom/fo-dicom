using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Dicom;
using Dicom.Imaging;
using Dicom.Imaging.Codec;
using Dicom.IO.Buffer;

namespace Dicom.Dump {
	public partial class MainForm : Form {
		private DicomFile _file;

		public MainForm() {
			InitializeComponent();
		}

		private void Reset() {
			lvDicom.Items.Clear();
			menuItemView.Enabled = false;
		}

		private delegate void AddItemDelegate(string tag, string vr, string length, string value);

		private void AddItem(string tag, string vr, string length, string value) {
			if (InvokeRequired) {
			    BeginInvoke(new AddItemDelegate(AddItem), tag, vr, length, value);
			    return;
			}

			var lvi = lvDicom.Items.Add(tag);
			lvi.SubItems.Add(vr);
			lvi.SubItems.Add(length);
			lvi.SubItems.Add(value);
		}

		public void OpenFile(string fileName) {
			DicomFile file = null;

			try {
				file = DicomFile.Open(fileName);
			} catch (DicomFileException ex) {
				file = ex.File;
				MessageBox.Show(this, "Exception while loading DICOM file: " + ex.Message, "Error loading DICOM file", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			OpenFile(file);
		}

		public void OpenFile(DicomFile file) {
			try {
				lvDicom.BeginUpdate();

				Reset();

				_file = file;

				new DicomDatasetWalker(_file.FileMetaInfo).Walk(new DumpWalker(this));
				new DicomDatasetWalker(_file.Dataset).Walk(new DumpWalker(this));

				if (_file.Dataset.Contains(DicomTag.PixelData))
					menuItemView.Enabled = true;
				menuItemSyntax.Enabled = true;
				menuItemSave.Enabled = true;
			} catch (Exception ex) {
				MessageBox.Show(this, "Exception while loading DICOM file: " + ex.Message, "Error loading DICOM file", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} finally {
				lvDicom.EndUpdate();
			}
		}

		private void OnClickOpen(object sender, EventArgs e) {
			var ofd = new OpenFileDialog();
			ofd.Filter = "DICOM Files (*.dcm;*.dic)|*.dcm;*.dic|All Files (*.*)|*.*";

			if (ofd.ShowDialog() == DialogResult.Cancel)
				return;

			OpenFile(ofd.FileName);
		}

		private void OnClickSave(object sender, EventArgs e) {
			var sfd = new SaveFileDialog();
			sfd.Filter = "DICOM (*.dcm)|*.dcm|Image (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif";

			if (sfd.ShowDialog() == DialogResult.Cancel)
				return;

			if (sfd.FilterIndex == 1) {
				var file = new DicomFile(_file.Dataset);
				file.Save(sfd.FileName);
			} else {
				var image = new DicomImage(_file.Dataset);
				image.RenderImage().Save(sfd.FileName);
			}
		}

		private class DumpWalker : IDicomDatasetWalker {
			int _level = 0;

			public DumpWalker(MainForm form) {
				Form = form;
				Level = 0;
			}

			public MainForm Form {
				get;
				set;
			}

			public int Level {
				get { return _level; }
				set {
					_level = value;
					Indent = String.Empty;
					for (int i = 0; i < _level; i++)
						Indent += "    ";
				}
			}

			private string Indent {
				get;
				set;
			}

			public void OnBeginWalk(DicomDatasetWalker walker, DicomDatasetWalkerCallback callback) {
			}

			public bool OnElement(DicomElement element) {
				var tag = String.Format("{0}{1}  {2}", Indent, element.Tag.ToString().ToUpper(), element.Tag.DictionaryEntry.Name);

				string value = "<large value not displayed>";
				if (element.Length <= 2048)
					value = String.Join("\\", element.Get<string[]>());

				if (element.ValueRepresentation == DicomVR.UI && element.Count > 0) {
					var uid = element.Get<DicomUID>(0);
					var name = uid.Name;
					if (name != "Unknown")
						value = String.Format("{0} ({1})", value, name);
				}

				Form.AddItem(tag, 
					element.ValueRepresentation.Code, 
					element.Length.ToString(), 
					value);
				return true;
			}

			public bool OnBeginSequence(DicomSequence sequence) {
				var tag = String.Format("{0}{1}  {2}", Indent, sequence.Tag.ToString().ToUpper(), sequence.Tag.DictionaryEntry.Name);

				Form.AddItem(tag, "SQ", String.Empty, String.Empty);

				Level++;
				return true;
			}

			public bool OnBeginSequenceItem(DicomDataset dataset) {
				var tag = String.Format("{0}Sequence Item:", Indent);

				Form.AddItem(tag, String.Empty, String.Empty, String.Empty);

				Level++;
				return true;
			}

			public bool OnEndSequenceItem() {
				Level--;
				return true;
			}

			public bool OnEndSequence() {
				Level--;
				return true;
			}

			public bool OnBeginFragment(DicomFragmentSequence fragment) {
				var tag = String.Format("{0}{1}  {2}", Indent, fragment.Tag.ToString().ToUpper(), fragment.Tag.DictionaryEntry.Name);

				Form.AddItem(tag, fragment.ValueRepresentation.Code, String.Empty, String.Empty);

				Level++;
				return true;
			}

			public bool OnFragmentItem(IByteBuffer item) {
				var tag = String.Format("{0}Fragment", Indent);

				Form.AddItem(tag, String.Empty, item.Size.ToString(), String.Empty);
				return true;
			}

			public bool OnEndFragment() {
				Level--;
				return true;
			}

			public void OnEndWalk() {
			}
		}

		private void OnClickView(object sender, EventArgs e) {
			var form = new DisplayForm(_file);
			form.ShowDialog(this);
		}

		private void OnContextMenuOpening(object sender, CancelEventArgs e) {
			var point = lvDicom.PointToClient(MousePosition);

			var item = lvDicom.GetItemAt(point.X, point.Y);
			if (item == null) {
				e.Cancel = true;
				return;
			}

			item.Selected = true;
		}

		private void OnClickContextMenuCopyValue(object sender, EventArgs e) {
			if (lvDicom.SelectedItems.Count == 0)
				return;

			var item = lvDicom.SelectedItems[0];
			var value = item.SubItems[3].Text;

			Clipboard.SetText(value);			
		}

		private void ChangeSyntax(DicomTransferSyntax syntax) {
			var file = _file.ChangeTransferSyntax(syntax);
			OpenFile(file);
		}

		private void OnClickExplicitVRLittleEndian(object sender, EventArgs e) {
			ChangeSyntax(DicomTransferSyntax.ExplicitVRLittleEndian);
		}

		private void OnClickImplicitVRLittleEndian(object sender, EventArgs e) {
			ChangeSyntax(DicomTransferSyntax.ImplicitVRLittleEndian);
		}

		private void OnClickExplicitVRBigEndian(object sender, EventArgs e) {
			ChangeSyntax(DicomTransferSyntax.ExplicitVRBigEndian);
		}

		private void OnClickJPEGLosslessP14SV1(object sender, EventArgs e) {
			ChangeSyntax(DicomTransferSyntax.JPEGProcess14SV1);
		}

		private void OnClickJPEG2000Lossless(object sender, EventArgs e) {
			ChangeSyntax(DicomTransferSyntax.JPEG2000Lossless);
		}

		private void OnClickJPEGLSLossless(object sender, EventArgs e) {
			ChangeSyntax(DicomTransferSyntax.JPEGLSLossless);
		}

		private void OnClickRLELossless(object sender, EventArgs e) {
			ChangeSyntax(DicomTransferSyntax.RLELossless);
		}
	}
}
