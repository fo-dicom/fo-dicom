// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace FellowOakDicom.Dump
{
    /// <summary>
    /// Interaktionslogik für ImageViewer.xaml
    /// </summary>
    public partial class ImageViewer : UserControl
    {

        private Point _lastDownPosition;
        private DicomImage _image;
        private int _frameNumber;

        public DicomImage ImageToDisplay
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    _frameNumber = 0;
                    DisplayImage();
                }
            }
        }

        public ImageViewer()
        {
            InitializeComponent();
        }

        private void DisplayImage()
        {
            if (_image == null)
            {
                ImageView.Source = null;
                return;
            }

            var img = _image.RenderImage(_frameNumber);
            var sharpImage = img.AsWriteableBitmap();

            ImageView.Source = sharpImage;
        }

        private void ImageView_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                // pan
            }
            else if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                // window
                var point = e.GetPosition(ImageView);
                var delta = point - _lastDownPosition;
                _lastDownPosition = point;

                _image.WindowCenter = _image.WindowCenter + delta.X * 10;
                _image.WindowWidth = _image.WindowWidth + delta.Y * 10;
                DisplayImage();
            }
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _image.ShowOverlays = true;
            DisplayImage();
        }

        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _image.ShowOverlays = false;
            DisplayImage();
        }

        private void ImageView_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                // scroll down
                if (_image.NumberOfFrames > (_frameNumber +1))
                {
                    _frameNumber++;
                    DisplayImage();
                }
            }
            else if (e.Delta < 0)
            {
                if (_frameNumber > 0)
                {
                    _frameNumber--;
                    DisplayImage();
                }
            }
            e.Handled = true;
        }

        private void ImageView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _lastDownPosition = e.GetPosition(ImageView);
        }
    }
}
