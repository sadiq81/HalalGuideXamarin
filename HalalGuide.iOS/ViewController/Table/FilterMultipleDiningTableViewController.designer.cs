// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;


namespace HalalGuide.iOS.ViewController.Table
{
	[Register ("FilterMultipleDiningTableViewController")]
	partial class FilterMultipleDiningTableViewController
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch AlcoholSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Choose { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Count { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DistanceLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider DistanceSlider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch HalalSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch PorkSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton Reset { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DistanceLabel != null) {
				DistanceLabel.Dispose ();
				DistanceLabel = null;
			}

			if (DistanceSlider != null) {
				DistanceSlider.Dispose ();
				DistanceSlider = null;
			}

			if (PorkSwitch != null) {
				PorkSwitch.Dispose ();
				PorkSwitch = null;
			}

			if (AlcoholSwitch != null) {
				AlcoholSwitch.Dispose ();
				AlcoholSwitch = null;
			}

			if (HalalSwitch != null) {
				HalalSwitch.Dispose ();
				HalalSwitch = null;
			}

			if (Choose != null) {
				Choose.Dispose ();
				Choose = null;
			}

			if (Reset != null) {
				Reset.Dispose ();
				Reset = null;
			}

			if (Count != null) {
				Count.Dispose ();
				Count = null;
			}
		}
	}
}
