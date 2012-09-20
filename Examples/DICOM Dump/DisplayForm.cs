using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Dicom.Imaging;

namespace Dicom.Dump {
	public partial class DisplayForm : Form {
		private readonly string _fileName;

		public DisplayForm(string fileName) {
			_fileName = fileName;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			// execute on ThreadPool to avoid STA WaitHandle.WaitAll exception
			ThreadPool.QueueUserWorkItem(delegate(object s) {
					DicomImage image = new DicomImage(_fileName);
			        Invoke(new WaitCallback(DisplayImage), image.RenderImage());
			                             });
			
		}

		protected void DisplayImage(object state) {
			Image image = (Image)state;

			double scale = 1.0;
			Size max = SystemInformation.WorkingArea.Size;

			if (image.Width > image.Height) {
				if (image.Width > max.Width)
					scale = (double)max.Width / (double)image.Width;
			} else {
				if (image.Height > max.Height)
					scale = (double)max.Height / (double)image.Height;
			}

			Width = (int)(image.Width * scale);
			Height = (int)(image.Height * scale);

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

			pbDisplay.Image = image;
		}
	}
}
