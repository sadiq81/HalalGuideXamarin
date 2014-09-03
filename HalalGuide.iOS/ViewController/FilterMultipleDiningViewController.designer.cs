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
	[Register ("FilterMultipleDiningViewController")]
	partial class FilterMultipleDiningViewController
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch AlcoholSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CountLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DistanceLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider DistanceSlider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch HalalSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UINavigationItem NavigationItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch PorkSwitch { get; set; }

		[Action ("Choose:")]
		partial void Choose (MonoTouch.Foundation.NSObject sender);

		[Action ("DistanceSliderValueChanged:")]
		partial void DistanceSliderValueChanged (MonoTouch.UIKit.UISlider sender);

		[Action ("Done:")]
		partial void Done (MonoTouch.Foundation.NSObject sender);

		[Action ("Reset:")]
		partial void Reset (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (DistanceSlider != null) {
				DistanceSlider.Dispose ();
				DistanceSlider = null;
			}

			if (DistanceLabel != null) {
				DistanceLabel.Dispose ();
				DistanceLabel = null;
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

			if (CountLabel != null) {
				CountLabel.Dispose ();
				CountLabel = null;
			}

			if (NavigationItem != null) {
				NavigationItem.Dispose ();
				NavigationItem = null;
			}
		}
	}
}
