// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Buffer;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FellowOakDicom.Dump
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DicomFile _file;

        private bool IsStructuredReport => _file != null
                                          && _file.Dataset.TryGetSingleValue(DicomTag.SOPClassUID, out DicomUID dummy)
                                          && dummy.StorageCategory == DicomStorageCategory.StructuredReport;


        public MainWindow()
        {
            InitializeComponent();

            new DicomSetupBuilder()
                .RegisterServices(s => s
                    .AddFellowOakDicom()
                    .AddImageManager<ImageSharpImageManager>())
                .Build();
        }


        public void OpenFile(string fileName)
        {
            DicomFile file;
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
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            if (file != null)
            {
                OpenFile(file);
            }
        }


        public void OpenFile(DicomFile file)
        {
            try
            {
                lvDicom.BeginInit();

                Reset();

                _file = file;

                new DicomDatasetWalker(_file.FileMetaInfo).Walk(new DumpWalker(AddItem));
                new DicomDatasetWalker(_file.Dataset).Walk(new DumpWalker(AddItem));

                if (_file.Dataset.Contains(DicomTag.PixelData) || IsStructuredReport)
                {
                    menuItemView.IsEnabled = true;
                    menuItemAnonymize.IsEnabled = true;
                }
                menuItemSyntax.IsEnabled = true;
                menuItemSave.IsEnabled = true;

                DisplayImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Exception while loading DICOM file: " + ex.Message,
                    "Error loading DICOM file",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                lvDicom.EndInit();
            }
        }

        private void Reset()
        {
            lvDicom.Items.Clear();
            menuItemView.IsEnabled = false;
            menuItemAnonymize.IsEnabled = false;
        }

        private void AddItem(string tag, string vr, string length, string value)
        {
            lvDicom.Items.Add(new { tag, vr, length, value });
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0) OpenFile(files[0]);
            }

        }


        private class DumpWalker : IDicomDatasetWalker
        {
            private int _level = 0;

            private Action<string, string, string, string> _addAction;

            public DumpWalker(Action<string, string, string, string> addAction)
            {
                _addAction = addAction;
                Level = 0;
            }

            public int Level
            {
                get => _level;
                set
                {
                    _level = value;
                    Indent = string.Empty.PadLeft(4 * _level);
                }
            }

            private string Indent { get; set; }

            public void OnBeginWalk()
            {
            }

            public bool OnElement(DicomElement element)
            {
                var tag = string.Format(
                    "{0}{1}  {2}",
                    Indent,
                    element.Tag.ToString().ToUpperInvariant(),
                    element.Tag.DictionaryEntry.Name);

                string value = "<large value not displayed>";
                if (element.Length <= 2048) value = string.Join("\\", element.Get<string[]>());

                if (element.ValueRepresentation == DicomVR.UI && element.Count > 0)
                {
                    var uid = element.Get<DicomUID>(0);
                    var name = uid.Name;
                    if (name != "Unknown") value = $"{value} ({name})";
                }

                _addAction(tag, element.ValueRepresentation.Code, element.Length.ToString(), value);
                return true;
            }

            public Task<bool> OnElementAsync(DicomElement element)
            {
                return Task.FromResult(OnElement(element));
            }

            public bool OnBeginSequence(DicomSequence sequence)
            {
                var tag = string.Format(
                    "{0}{1}  {2}",
                    Indent,
                    sequence.Tag.ToString().ToUpperInvariant(),
                    sequence.Tag.DictionaryEntry.Name);

                _addAction(tag, "SQ", string.Empty, string.Empty);

                Level++;
                return true;
            }

            public bool OnBeginSequenceItem(DicomDataset dataset)
            {
                var tag = $"{Indent}Sequence Item:";

                _addAction(tag, string.Empty, string.Empty, string.Empty);

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
                var tag = string.Format(
                    "{0}{1}  {2}",
                    Indent,
                    fragment.Tag.ToString().ToUpperInvariant(),
                    fragment.Tag.DictionaryEntry.Name);

                _addAction(tag, fragment.ValueRepresentation.Code, string.Empty, string.Empty);

                Level++;
                return true;
            }

            public bool OnFragmentItem(IByteBuffer item)
            {
                if (item != null)
                {
                    var tag = $"{Indent}Fragment";

                    _addAction(tag, string.Empty, item.Size.ToString(), string.Empty);
                }
                return true;
            }

            public Task<bool> OnFragmentItemAsync(IByteBuffer item)
            {
                return Task.FromResult(OnFragmentItem(item));
            }

            public bool OnEndFragment()
            {
                Level--;
                return true;
            }

            public void OnEndWalk()
            {
            }
        }

        private void menuItemOpenClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "DICOM Files (*.dcm;*.dic)|*.dcm;*.dic|All Files (*.*)|*.*" };
            if (ofd.ShowDialog() == true)
            {
                OpenFile(ofd.FileName);
            }
        }

        private void menuItemExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void DisplayImage()
        {
            imageView.Source = null;
            if (_file.Dataset.Contains(DicomTag.PixelData))
            {
                var dicomImage = new DicomImage(_file.Dataset);
                using var img = dicomImage.RenderImage();
                using var sharpImage = img.AsSharpImage();

                using var ms = new MemoryStream();
                sharpImage.Save(ms, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                ms.Position = 0;

                imageView.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

            }
        }


    }
}
