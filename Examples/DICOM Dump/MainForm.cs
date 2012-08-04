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

namespace Dicom.Dump {
	public partial class MainForm : Form {
		private string _fileName;

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

		private void OnClickOpen(object sender, EventArgs e) {
			var ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.Cancel)
				return;

			try {
				lvDicom.BeginUpdate();

				Reset();

				_fileName = ofd.FileName;

				var file = DicomFile.Open(_fileName);
				new DicomDatasetWalker(file.FileMetaInfo).Walk(new DumpWalker(this));
				new DicomDatasetWalker(file.Dataset).Walk(new DumpWalker(this));

				if (file.Dataset.Contains(DicomTag.PixelData))
					menuItemView.Enabled = true;
			} catch (Exception ex) {
				MessageBox.Show(this, "Exception while loading DICOM file: " + ex.Message, "Error loading DICOM file", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} finally {
				lvDicom.EndUpdate();
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

				if (element.ValueRepresentation == DicomVR.UI) {
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
			var form = new DisplayForm(_fileName);
			form.ShowDialog(this);
		}
	}
}
