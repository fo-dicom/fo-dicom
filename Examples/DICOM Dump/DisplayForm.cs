using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Dicom;
using Dicom.Imaging;

namespace Dicom.Dump {
	public partial class DisplayForm : Form {
		private DicomFile _file;
		private DicomImage _image;
		private bool _grayscale;
		private double _windowWidth;
		private double _windowCenter;
		private int _frame;

		public DisplayForm(DicomFile file) {
			_file = file;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			// execute on ThreadPool to avoid STA WaitHandle.WaitAll exception
			ThreadPool.QueueUserWorkItem(delegate(object s) {
					_image = new DicomImage(_file.Dataset);
					_grayscale = !_image.PhotometricInterpretation.IsColor;
					if (_grayscale) {
						_windowWidth = _image.WindowWidth;
						_windowCenter = _image.WindowCenter;
					}
					_frame = 0;
			        Invoke(new WaitCallback(DisplayImage), _image);
			                             });
			
		}

		private delegate void ExceptionHandler(Exception e);

		protected void OnException(Exception e) {
			if (InvokeRequired) {
				BeginInvoke(new ExceptionHandler(OnException), e);
				return;
			}

			MessageBox.Show(this, e.ToString(), "Image Render Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Close();
		}

		protected void DisplayImage(object state) {
			try {
				var image = (DicomImage)state;

				double scale = 1.0;
				Size max = SystemInformation.WorkingArea.Size;

				int maxW = max.Width - (Width - pbDisplay.Width);
				int maxH = max.Height - (Height - pbDisplay.Height);

				if (image.Width > image.Height) {
					if (image.Width > maxW)
						scale = (double)maxW / (double)image.Width;
				} else {
					if (image.Height > maxH)
						scale = (double)maxH / (double)image.Height;
				}

				if (scale != 1.0)
					image.Scale = scale;

				Width = (int)(image.Width * scale) + (Width - pbDisplay.Width);
				Height = (int)(image.Height * scale) + (Height - pbDisplay.Height);

				if (Width >= (max.Width * 0.99) || Height >= (max.Height * 0.99))
					CenterToScreen(); // center very large images on the screen
				else {
					CenterToParent();
					if (Bottom > max.Height)
						Top -= Bottom - max.Height;
					if (Top < 0)
						Top = 0;
					if (Right > max.Width)
						Left -= Right - max.Width;
					if (Left < 0)
						Left = 0;
				}

				pbDisplay.Image = image.RenderImage(_frame);

				if (_grayscale)
					Text = String.Format("DICOM Image Display [wc: {0}, ww: {1}]", image.WindowCenter, image.WindowWidth);
			} catch (Exception e) {
				OnException(e);
			}
		}

		private bool _dragging = false;
		private Point _lastPosition = Point.Empty;

		private void OnMouseDown(object sender, MouseEventArgs e) {
			if (!_grayscale)
				return;

			_lastPosition = e.Location;
			_dragging = true;
		}

		private void OnMouseUp(object sender, MouseEventArgs e) {
			_dragging = false;
		}

		private void OnMouseLeave(object sender, EventArgs e) {
			_dragging = false;
		}

		private void OnMouseMove(object sender, MouseEventArgs e) {
			if (!_dragging)
				return;

			_image.WindowWidth += e.X - _lastPosition.X;
			_image.WindowCenter += e.Y - _lastPosition.Y;

			_lastPosition = e.Location;

			DisplayImage(_image);
		}

		private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
			_image.WindowCenter = _windowCenter;
			_image.WindowWidth = _windowWidth;

			DisplayImage(_image);
		}

		private void OnKeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Right) {
				_frame++;
				if (_frame >= _image.NumberOfFrames)
					_frame--;
				DisplayImage(_image);
				return;
			}

			if (e.KeyCode == Keys.Left) {
				_frame--;
				if (_frame < 0)
					_frame++;
				DisplayImage(_image);
				return;
			}
		}
	}
}
