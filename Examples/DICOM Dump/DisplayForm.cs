using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Dicom;
using Dicom.Imaging;

namespace Dicom.Dump {
	public partial class DisplayForm : Form {
		private readonly DicomFile _file;

		public DisplayForm(DicomFile file) {
			_file = file;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			// execute on ThreadPool to avoid STA WaitHandle.WaitAll exception
			ThreadPool.QueueUserWorkItem(delegate(object s) {
					var image = new DicomImage(_file.Dataset);
			        Invoke(new WaitCallback(DisplayImage), image);
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

				pbDisplay.Image = image.RenderImage();
			} catch (Exception e) {
				OnException(e);
			}
		}
	}
}
