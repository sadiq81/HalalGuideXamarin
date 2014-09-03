// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace HalalGuide.iOS.ViewController
{
	[Register ("LandingViewController")]
	partial class LandingViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView LatestUpdatedTableView { get; set; }

		[Action ("GoToLogin:")]
		partial void GoToLogin (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (LatestUpdatedTableView != null) {
				LatestUpdatedTableView.Dispose ();
				LatestUpdatedTableView = null;
			}
		}
	}
}
