// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.NativeCodec;
using Microsoft.Win32;
using System;
using System.Windows;

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
                    .AddTranscoderManager<NativeTranscoderManager>()
                    .AddImageManager<WPFImageManager>())
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
                LvDicom.BeginInit();

                Reset();

                _file = file;

                new DicomDatasetWalker(_file.FileMetaInfo).Walk(new DumpWalker(AddItem));
                new DicomDatasetWalker(_file.Dataset).Walk(new DumpWalker(AddItem));

                if (_file.Dataset.Contains(DicomTag.PixelData) || IsStructuredReport)
                {
                    MenuItemView.IsEnabled = true;
                    MenuItemAnonymize.IsEnabled = true;
                }
                MenuItemSyntax.IsEnabled = true;
                MenuItemSave.IsEnabled = true;

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
                LvDicom.EndInit();
            }
        }


        private void Reset()
        {
            LvDicom.Items.Clear();
            MenuItemView.IsEnabled = false;
            MenuItemAnonymize.IsEnabled = false;
        }


        private void AddItem(string tag, string vr, string length, string value)
        {
            LvDicom.Items.Add(new { tag, vr, length, value });
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
                if (files.Length > 0)
                {
                    OpenFile(files[0]);
                }
            }
        }


        private void HandleMenuItemOpenClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "DICOM Files (*.dcm;*.dic)|*.dcm;*.dic|All Files (*.*)|*.*" };
            if (ofd.ShowDialog() == true)
            {
                OpenFile(ofd.FileName);
            }
        }


        private void HandleMenuItemExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void DisplayImage()
        {
            ImageView.ImageToDisplay = null;
            if (_file.Dataset.Contains(DicomTag.PixelData))
            {
                var dicomImage = new DicomImage(_file.Dataset);
                ImageView.ImageToDisplay = dicomImage;
            }
        }


    }
}
