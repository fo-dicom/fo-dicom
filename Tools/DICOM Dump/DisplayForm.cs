// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Dicom.Imaging;

namespace Dicom.Dump
{
    public partial class DisplayForm : Form
    {
        private readonly DicomFile _file;

        private DicomImage _image;

        private bool _grayscale;

        private double _windowWidth;

        private double _windowCenter;

        private int _frame;

        private Image _current;

        private Image _previous;

        public DisplayForm(DicomFile file)
        {
            _file = file;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            // execute on ThreadPool to avoid STA WaitHandle.WaitAll exception
            ThreadPool.QueueUserWorkItem(
                delegate
                {
                    try
                    {
                        _image = new DicomImage(_file.Dataset);
                        _grayscale = _image.IsGrayscale;
                        if (_grayscale)
                        {
                            _windowWidth = _image.WindowWidth;
                            _windowCenter = _image.WindowCenter;
                        }
                        _frame = 0;
                        Invoke(new WaitCallback(SizeForImage), _image);
                        Invoke(new WaitCallback(DisplayImage), _image);
                    }
                    catch (Exception ex)
                    {
                        OnException(ex);
                    }
               });
        }

        private delegate void ExceptionHandler(Exception e);

        protected void OnException(Exception e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ExceptionHandler(OnException), e);
                return;
            }

            MessageBox.Show(this, (e.InnerException ?? e).Message, "Image Render Exception", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Close();
        }

        protected void DisplayImage(object state)
        {
            try
            {
                var image = (DicomImage)state;

                _previous = pbDisplay.Image;
                pbDisplay.Image = null;
                _current = image.RenderImage(_frame).Clone().AsBitmap();
                pbDisplay.Image = _current;

                Text = _grayscale
                    ? $"DICOM Image Display [scale: {Math.Round(image.Scale, 1)}, wc: {image.WindowCenter}, ww: {image.WindowWidth}]"
                    : $"DICOM Image Display [scale: {Math.Round(image.Scale, 1)}]";
            }
            catch (Exception e)
            {
                OnException(e);
            }
        }

        protected void SizeForImage(object state)
        {
            var image = (DicomImage)state;

            var max = SystemInformation.WorkingArea.Size;

            var maxW = max.Width - (Width - pbDisplay.Width);
            var maxH = max.Height - (Height - pbDisplay.Height);

            if (image.Width > maxW || image.Height > maxH) image.Scale = Math.Min((double)maxW / image.Width, (double)maxH / image.Height);
            else image.Scale = 1.0;

            Width = (int)(image.Width * image.Scale) + (Width - pbDisplay.Width);
            Height = (int)(image.Height * image.Scale) + (Height - pbDisplay.Height);

            if (Width >= (max.Width * 0.99) || Height >= (max.Height * 0.99)) CenterToScreen(); // center very large images on the screen
            else
            {
                CenterToParent();
                if (Bottom > max.Height) Top -= Bottom - max.Height;
                if (Top < 0) Top = 0;
                if (Right > max.Width) Left -= Right - max.Width;
                if (Left < 0) Left = 0;
            }
        }

        private bool _dragging = false;

        private Point _lastPosition = Point.Empty;

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!_grayscale) return;

            _lastPosition = e.Location;
            _dragging = true;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            _dragging = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;

            _image.WindowWidth += e.X - _lastPosition.X;
            _image.WindowCenter += e.Y - _lastPosition.Y;

            _lastPosition = e.Location;

            DisplayImage(_image);
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _image.WindowCenter = _windowCenter;
                _image.WindowWidth = _windowWidth;
            }

            DisplayImage(_image);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                _frame++;
                if (_frame >= _image.NumberOfFrames) _frame--;
                DisplayImage(_image);
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                _frame--;
                if (_frame < 0) _frame++;
                DisplayImage(_image);
                return;
            }

            if (e.KeyCode == Keys.O)
            {
                _image.ShowOverlays = !_image.ShowOverlays;
                DisplayImage(_image);
                return;
            }

            if (!_image.IsGrayscale) return;
            GrayscaleRenderOptions options = null;

            if (e.KeyCode == Keys.D0) options = GrayscaleRenderOptions.FromDataset(_file.Dataset);
            else if (e.KeyCode == Keys.D1) options = GrayscaleRenderOptions.FromWindowLevel(_file.Dataset);
            else if (e.KeyCode == Keys.D2) options = GrayscaleRenderOptions.FromImagePixelValueTags(_file.Dataset);
            else if (e.KeyCode == Keys.D3) options = GrayscaleRenderOptions.FromMinMax(_file.Dataset);
            else if (e.KeyCode == Keys.D4) options = GrayscaleRenderOptions.FromBitRange(_file.Dataset);
            else if (e.KeyCode == Keys.D5) options = GrayscaleRenderOptions.FromHistogram(_file.Dataset, 90);

            if (options != null)
            {
                _image.WindowWidth = options.WindowWidth;
                _image.WindowCenter = options.WindowCenter;

                DisplayImage(_image);
            }
        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            var image = _image;
            if (image == null || pbDisplay.Image == null) return;

            if (WindowState == FormWindowState.Normal)
            {
                if (pbDisplay.Width > pbDisplay.Height)
                {
                    if (image.Width > image.Height) image.Scale = (double)pbDisplay.Height / image.Height;
                    else image.Scale = (double)pbDisplay.Width / image.Width;
                }
                else
                {
                    if (image.Width > image.Height) image.Scale = (double)pbDisplay.Width / image.Width;
                    else image.Scale = (double)pbDisplay.Height / image.Height;
                }

                // scale viewing window to match rescaled image size
                Width = (int)(image.Width * image.Scale) + (Width - pbDisplay.Width);
                Height = (int)(image.Height * image.Scale) + (Height - pbDisplay.Height);
            }

            if (WindowState == FormWindowState.Maximized)
            {
                image.Scale = Math.Min(
                    (double)pbDisplay.Width / image.Width,
                    (double)pbDisplay.Height / image.Height);
            }

            DisplayImage(image);
        }
    }
}
