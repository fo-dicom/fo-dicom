// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Touch.DICOM.Viewer
{
	[Register ("MyViewController")]
	partial class MyViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView dicomImageView { get; set; }

		[Action ("buttonClick:")]
		partial void buttonClick (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (dicomImageView != null) {
				dicomImageView.Dispose ();
				dicomImageView = null;
			}
		}
	}
}
