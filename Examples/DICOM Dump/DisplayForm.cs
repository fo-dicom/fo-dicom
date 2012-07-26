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
			Width = image.Width;
			Height = image.Height;
			pbDisplay.Image = image;
		}
	}
}
