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
		}
	}
}
