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
		MonoTouch.UIKit.UILabel Eat { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Enumber { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LatestUpdated { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView LatestUpdatedTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Mosque { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Settings { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Shopping { get; set; }

		[Action ("GoToLogin:")]
		partial void GoToLogin (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (LatestUpdatedTableView != null) {
				LatestUpdatedTableView.Dispose ();
				LatestUpdatedTableView = null;
			}

			if (Shopping != null) {
				Shopping.Dispose ();
				Shopping = null;
			}

			if (Enumber != null) {
				Enumber.Dispose ();
				Enumber = null;
			}

			if (Eat != null) {
				Eat.Dispose ();
				Eat = null;
			}

			if (Mosque != null) {
				Mosque.Dispose ();
				Mosque = null;
			}

			if (Settings != null) {
				Settings.Dispose ();
				Settings = null;
			}

			if (LatestUpdated != null) {
				LatestUpdated.Dispose ();
				LatestUpdated = null;
			}
		}
	}
}
