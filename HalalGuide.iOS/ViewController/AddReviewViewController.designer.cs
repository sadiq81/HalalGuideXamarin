// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace HalalGuide.iOS.ViewController
{
	[Register ("AddReviewViewController")]
	partial class AddReviewViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView Review { get; set; }

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
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Regreet (UIBarButtonItem sender);

		[Action ("Save:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Save (UIBarButtonItem sender);

		[Action ("StarPressed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void StarPressed (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
