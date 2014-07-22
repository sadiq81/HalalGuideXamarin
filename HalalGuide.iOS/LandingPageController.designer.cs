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
	[Register ("LandingPageController")]
	partial class LandingPageController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView LatestUpdated { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchBar SearchBar { get; set; }

		[Action ("Navigate:")]
		partial void Navigate (MonoTouch.Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (SearchBar != null) {
				SearchBar.Dispose ();
				SearchBar = null;
			}

			if (LatestUpdated != null) {
				LatestUpdated.Dispose ();
				LatestUpdated = null;
			}
		}
	}
}
