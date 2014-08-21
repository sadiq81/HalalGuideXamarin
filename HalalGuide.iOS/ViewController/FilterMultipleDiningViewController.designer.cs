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
	[Register ("FilterMultipleDiningViewController")]
	partial class FilterMultipleDiningViewController
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch AlcoholSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CategoriesLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView CategoryTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DistanceLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider DistanceSlider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch HalalSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch PorkSwitch { get; set; }

		[Action ("ChooseCategories:")]
		partial void ChooseCategories (MonoTouch.UIKit.UIButton sender);

		[Action ("DistanceSliderValueChanged:")]
		partial void DistanceSliderValueChanged (MonoTouch.UIKit.UISlider sender);

		[Action ("Done:")]
		partial void Done (MonoTouch.Foundation.NSObject sender);

		[Action ("ResetCategories:")]
		partial void ResetCategories (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AlcoholSwitch != null) {
				AlcoholSwitch.Dispose ();
				AlcoholSwitch = null;
			}

			if (CategoriesLabel != null) {
				CategoriesLabel.Dispose ();
				CategoriesLabel = null;
			}

			if (CategoryTableView != null) {
				CategoryTableView.Dispose ();
				CategoryTableView = null;
			}

			if (DistanceLabel != null) {
				DistanceLabel.Dispose ();
				DistanceLabel = null;
			}

			if (DistanceSlider != null) {
				DistanceSlider.Dispose ();
				DistanceSlider = null;
			}

			if (HalalSwitch != null) {
				HalalSwitch.Dispose ();
				HalalSwitch = null;
			}

			if (PorkSwitch != null) {
				PorkSwitch.Dispose ();
				PorkSwitch = null;
			}
		}
	}
}
