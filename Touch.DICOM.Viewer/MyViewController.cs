using System;
using System.IO;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Dicom;
using Dicom.Imaging;

namespace Touch.DICOM.Viewer
{
	public partial class MyViewController : UIViewController
	{
		public MyViewController () : base ("MyViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void buttonClick (MonoTouch.UIKit.UIButton sender)
		{
			using (var stream = File.OpenRead("Images/CT-MONO2-16-ankle")) {
				var dcm = DicomFile.Open (stream);
				var image = new DicomImage (dcm.Dataset);
				dicomImageView.Image = UIImage.FromImage(image.RenderImageSource ());
			}
		}
	}
}

