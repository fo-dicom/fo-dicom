﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Dump
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;

#if !NET35
    using System.Threading.Tasks;
#endif

    using System.Windows.Forms;

    using Dicom.Imaging;
    using Dicom.Imaging.Codec;
    using Dicom.IO.Buffer;


    public partial class MainForm : Form
    {
        private DicomFile _file;
        private DicomAnonymizer _anonymizer;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            DicomDictionary.EnsureDefaultDictionariesLoaded(ModifierKeys != Keys.Shift);
            _anonymizer = new DicomAnonymizer();

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                OpenFile(args[1]);
            }

            base.OnLoad(e);
        }

        private void Reset()
        {
            lvDicom.Items.Clear();
            menuItemView.Enabled = false;
            menuItemAnonymize.Enabled = false;
        }

        private delegate void AddItemDelegate(string tag, string vr, string length, string value);

        private void AddItem(string tag, string vr, string length, string value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new AddItemDelegate(AddItem), tag, vr, length, value);
                return;
            }

            var lvi = lvDicom.Items.Add(tag);
            lvi.SubItems.Add(vr);
            lvi.SubItems.Add(length);
            lvi.SubItems.Add(value);
        }

        private bool IsStructuredReport => this._file != null
                                           && this._file.Dataset.Get<DicomUID>(DicomTag.SOPClassUID, null)?.StorageCategory
                                           == DicomStorageCategory.StructuredReport;

        public void OpenFile(string fileName)
        {
            DicomFile file = null;

            try
            {
                file = DicomFile.Open(fileName);
            }
            catch (DicomFileException ex)
            {
                file = ex.File;
                MessageBox.Show(
                    this,
                    "Exception while loading DICOM file: " + ex.Message,
                    "Error loading DICOM file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (file != null) OpenFile(file);
        }

        public void OpenFile(DicomFile file)
        {
            try
            {
                lvDicom.BeginUpdate();

                Reset();

                _file = file;

                new DicomDatasetWalker(_file.FileMetaInfo).Walk(new DumpWalker(this));
                new DicomDatasetWalker(_file.Dataset).Walk(new DumpWalker(this));

                if (_file.Dataset.Contains(DicomTag.PixelData) || IsStructuredReport)
                {
                    menuItemView.Enabled = true;
                    menuItemAnonymize.Enabled = true;
                }
                menuItemSyntax.Enabled = true;
                menuItemSave.Enabled = true;

                menuItemJpegLossy.Enabled = _file.Dataset.Get<int>(DicomTag.BitsStored, 0, 16) <= 12;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Exception while loading DICOM file: " + ex.Message,
                    "Error loading DICOM file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                lvDicom.EndUpdate();
            }
        }

        private void OnClickOpen(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "DICOM Files (*.dcm;*.dic)|*.dcm;*.dic|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.Cancel) return;

            OpenFile(ofd.FileName);
        }

        private void OnClickSave(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "DICOM (*.dcm)|*.dcm|Image (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif";

            if (sfd.ShowDialog() == DialogResult.Cancel) return;

            if (sfd.FilterIndex == 1)
            {
                var file = new DicomFile(_file.Dataset);
                file.Save(sfd.FileName);
            }
            else
            {
                var image = new DicomImage(_file.Dataset);
                image.RenderImage().As<Image>().Save(sfd.FileName);
            }
        }

        private class DumpWalker : IDicomDatasetWalker
        {
            private int _level = 0;

            public DumpWalker(MainForm form)
            {
                Form = form;
                Level = 0;
            }

            public MainForm Form { get; set; }

            public int Level
            {
                get
                {
                    return _level;
                }
                set
                {
                    _level = value;
                    Indent = String.Empty;
                    for (int i = 0; i < _level; i++) Indent += "    ";
                }
            }

            private string Indent { get; set; }

            public void OnBeginWalk()
            {
            }

            public bool OnElement(DicomElement element)
            {
                var tag = String.Format(
                    "{0}{1}  {2}",
                    Indent,
                    element.Tag.ToString().ToUpper(),
                    element.Tag.DictionaryEntry.Name);

                string value = "<large value not displayed>";
                if (element.Length <= 2048) value = String.Join("\\", element.Get<string[]>());

                if (element.ValueRepresentation == DicomVR.UI && element.Count > 0)
                {
                    var uid = element.Get<DicomUID>(0);
                    var name = uid.Name;
                    if (name != "Unknown") value = String.Format("{0} ({1})", value, name);
                }

                Form.AddItem(tag, element.ValueRepresentation.Code, element.Length.ToString(), value);
                return true;
            }

#if !NET35
            public Task<bool> OnElementAsync(DicomElement element)
            {
                return Task.FromResult(this.OnElement(element));
            }
#endif

            public bool OnBeginSequence(DicomSequence sequence)
            {
                var tag = String.Format(
                    "{0}{1}  {2}",
                    Indent,
                    sequence.Tag.ToString().ToUpper(),
                    sequence.Tag.DictionaryEntry.Name);

                Form.AddItem(tag, "SQ", String.Empty, String.Empty);

                Level++;
                return true;
            }

            public bool OnBeginSequenceItem(DicomDataset dataset)
            {
                var tag = String.Format("{0}Sequence Item:", Indent);

                Form.AddItem(tag, String.Empty, String.Empty, String.Empty);

                Level++;
                return true;
            }

            public bool OnEndSequenceItem()
            {
                Level--;
                return true;
            }

            public bool OnEndSequence()
            {
                Level--;
                return true;
            }

            public bool OnBeginFragment(DicomFragmentSequence fragment)
            {
                var tag = String.Format(
                    "{0}{1}  {2}",
                    Indent,
                    fragment.Tag.ToString().ToUpper(),
                    fragment.Tag.DictionaryEntry.Name);

                Form.AddItem(tag, fragment.ValueRepresentation.Code, String.Empty, String.Empty);

                Level++;
                return true;
            }

            public bool OnFragmentItem(IByteBuffer item)
            {
                var tag = String.Format("{0}Fragment", Indent);

                Form.AddItem(tag, String.Empty, item.Size.ToString(), String.Empty);
                return true;
            }

#if !NET35
            public Task<bool> OnFragmentItemAsync(IByteBuffer item)
            {
                return Task.FromResult(this.OnFragmentItem(item));
            }
#endif

            public bool OnEndFragment()
            {
                Level--;
                return true;
            }

            public void OnEndWalk()
            {
            }
        }

        private void OnClickView(object sender, EventArgs e)
        {
            if (IsStructuredReport)
            {
                var form = new ReportForm(_file);
                form.ShowDialog(this);
            }
            else
            {
                var form = new DisplayForm(_file);
                form.ShowDialog(this);
            }
        }

        private void OnContextMenuOpening(object sender, CancelEventArgs e)
        {
            var point = lvDicom.PointToClient(MousePosition);

            var item = lvDicom.GetItemAt(point.X, point.Y);
            if (item == null)
            {
                e.Cancel = true;
                return;
            }

            item.Selected = true;
        }

        private void OnClickContextMenuCopyValue(object sender, EventArgs e)
        {
            if (lvDicom.SelectedItems.Count == 0) return;

            var item = lvDicom.SelectedItems[0];
            var value = item.SubItems[3].Text;

            Clipboard.SetText(value);
        }

        private void OnClickContextMenuCopyTag(object sender, EventArgs e)
        {
            if (lvDicom.SelectedItems.Count == 0) return;

            var item = lvDicom.SelectedItems[0];
            var value = item.Text.Substring(1);
            value = value.Substring(0, value.IndexOf(')'));

            Clipboard.SetText(value);
        }

        private void ChangeSyntax(DicomTransferSyntax syntax, DicomCodecParams param = null)
        {
            var file = _file.Clone(syntax, param);
            OpenFile(file);
        }

        private void OnClickExplicitVRLittleEndian(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.ExplicitVRLittleEndian);
        }

        private void OnClickImplicitVRLittleEndian(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.ImplicitVRLittleEndian);
        }

        private void OnClickExplicitVRBigEndian(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.ExplicitVRBigEndian);
        }

        private void OnClickJPEGLosslessP14(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGProcess14);
        }

        private void OnClickJPEGLosslessP14SV1(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGProcess14SV1);
        }

        private void OnClickJPEGLossyQuality100(object sender, EventArgs e)
        {
            var bits = _file.Dataset.Get<int>(DicomTag.BitsAllocated, 0, 8);
            var syntax = DicomTransferSyntax.JPEGProcess1;
            if (bits == 16) syntax = DicomTransferSyntax.JPEGProcess2_4;
            ChangeSyntax(syntax, new DicomJpegParams { Quality = 100 });
        }

        private void OnClickJPEGLossyQuality90(object sender, EventArgs e)
        {
            var bits = _file.Dataset.Get<int>(DicomTag.BitsAllocated, 0, 8);
            var syntax = DicomTransferSyntax.JPEGProcess1;
            if (bits == 16) syntax = DicomTransferSyntax.JPEGProcess2_4;
            ChangeSyntax(syntax, new DicomJpegParams { Quality = 90 });
        }

        private void OnClickJPEGLossyQuality80(object sender, EventArgs e)
        {
            var bits = _file.Dataset.Get<int>(DicomTag.BitsAllocated, 0, 8);
            var syntax = DicomTransferSyntax.JPEGProcess1;
            if (bits == 16) syntax = DicomTransferSyntax.JPEGProcess2_4;
            ChangeSyntax(syntax, new DicomJpegParams { Quality = 80 });
        }

        private void OnClickJPEGLossyQuality75(object sender, EventArgs e)
        {
            var bits = _file.Dataset.Get<int>(DicomTag.BitsAllocated, 0, 8);
            var syntax = DicomTransferSyntax.JPEGProcess1;
            if (bits == 16) syntax = DicomTransferSyntax.JPEGProcess2_4;
            ChangeSyntax(syntax, new DicomJpegParams { Quality = 75 });
        }

        private void OnClickJPEGLossyQuality70(object sender, EventArgs e)
        {
            var bits = _file.Dataset.Get<int>(DicomTag.BitsAllocated, 0, 8);
            var syntax = DicomTransferSyntax.JPEGProcess1;
            if (bits == 16) syntax = DicomTransferSyntax.JPEGProcess2_4;
            ;
            ChangeSyntax(syntax, new DicomJpegParams { Quality = 70 });
        }

        private void OnClickJPEGLossyQuality60(object sender, EventArgs e)
        {
            var bits = _file.Dataset.Get<int>(DicomTag.BitsAllocated, 0, 8);
            var syntax = DicomTransferSyntax.JPEGProcess1;
            if (bits == 16) syntax = DicomTransferSyntax.JPEGProcess2_4;
            ChangeSyntax(syntax, new DicomJpegParams { Quality = 60 });
        }

        private void OnClickJPEGLossyQuality50(object sender, EventArgs e)
        {
            var bits = _file.Dataset.Get<int>(DicomTag.BitsAllocated, 0, 8);
            var syntax = DicomTransferSyntax.JPEGProcess1;
            if (bits == 16) syntax = DicomTransferSyntax.JPEGProcess2_4;
            ChangeSyntax(syntax, new DicomJpegParams { Quality = 50 });
        }

        private void OnClickJPEG2000Lossless(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEG2000Lossless);
        }

        private void OnClickJPEG2000LossyRate5(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEG2000Lossy, new DicomJpeg2000Params { Rate = 5 });
        }

        private void OnClickJPEG2000LossyRate10(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEG2000Lossy, new DicomJpeg2000Params { Rate = 10 });
        }

        private void OnClickJPEG2000LossyRate20(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEG2000Lossy, new DicomJpeg2000Params { Rate = 20 });
        }

        private void OnClickJPEG2000LossyRate40(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEG2000Lossy, new DicomJpeg2000Params { Rate = 40 });
        }

        private void OnClickJPEG2000LossyRate80(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEG2000Lossy, new DicomJpeg2000Params { Rate = 80 });
        }

        private void OnClickJPEGLSLossless(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGLSLossless);
        }

        private void OnClickJPEGLSNearLosslessError2(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGLSNearLossless, new DicomJpegLsParams { AllowedError = 2 });
        }

        private void OnClickJPEGLSNearLosslessError3(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGLSNearLossless, new DicomJpegLsParams { AllowedError = 3 });
        }

        private void OnClickJPEGLSNearLosslessError4(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGLSNearLossless, new DicomJpegLsParams { AllowedError = 4 });
        }

        private void OnClickJPEGLSNearLosslessError5(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGLSNearLossless, new DicomJpegLsParams { AllowedError = 5 });
        }

        private void OnClickJPEGLSNearLosslessError10(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.JPEGLSNearLossless, new DicomJpegLsParams { AllowedError = 10 });
        }

        private void OnClickRLELossless(object sender, EventArgs e)
        {
            ChangeSyntax(DicomTransferSyntax.RLELossless);
        }

        private void OnClickExportPixelData(object sender, EventArgs e)
        {
            try
            {
                var pixel = DicomPixelData.Create(_file.Dataset);
                var frame = pixel.GetFrame(0);

                var sfd = new SaveFileDialog();
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, frame.Data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Unable to extract raw pixel data: " + ex.Message,
                    "Export Pixel Data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0) OpenFile(files[0]);
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnClickAnonymize(object sender, EventArgs e)
        {
            var file = _anonymizer.Anonymize(_file);
            OpenFile(file);
        }
    }
}
