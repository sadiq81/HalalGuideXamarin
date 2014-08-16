// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace HalalGuide.iOS
{
	[Register ("ReviewController")]
	partial class ReviewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView ReviewTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Star1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Star2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Star3 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Star4 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Star5 { get; set; }

		[Action ("Regreet:")]
		partial void Regreet (MonoTouch.Foundation.NSObject sender);

		[Action ("Save:")]
		partial void Save (MonoTouch.Foundation.NSObject sender);

		[Action ("StarPressed:")]
		partial void StarPressed (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ReviewTextField != null) {
				ReviewTextField.Dispose ();
				ReviewTextField = null;
			}

			if (Star1 != null) {
				Star1.Dispose ();
				Star1 = null;
			}

			if (Star2 != null) {
				Star2.Dispose ();
				Star2 = null;
			}

			if (Star3 != null) {
				Star3.Dispose ();
				Star3 = null;
			}

			if (Star4 != null) {
				Star4.Dispose ();
				Star4 = null;
			}

			if (Star5 != null) {
				Star5.Dispose ();
				Star5 = null;
			}

			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}
		}
	}
}
