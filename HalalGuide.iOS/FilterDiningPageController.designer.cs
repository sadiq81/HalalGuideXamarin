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
	[Register ("FilterDiningPageController")]
	partial class FilterDiningPageController
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch AlcoholSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView CategoryTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ChooseButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch HalalSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel NumberOfCategoriesLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch PorkSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider Slider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel SliderValueLabel { get; set; }

		[Action ("AlcoholValueChanged:")]
		partial void AlcoholValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("ChooseCategory:")]
		partial void ChooseCategory (MonoTouch.UIKit.UIButton sender);

		[Action ("HalalValueChanged:")]
		partial void HalalValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("PorkValueChanged:")]
		partial void PorkValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("ResetCategory:")]
		partial void ResetCategory (MonoTouch.UIKit.UIButton sender);

		[Action ("SliderValueChanged:")]
		partial void SliderValueChanged (MonoTouch.UIKit.UISlider sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CategoryTableView != null) {
				CategoryTableView.Dispose ();
				CategoryTableView = null;
			}

			if (NumberOfCategoriesLabel != null) {
				NumberOfCategoriesLabel.Dispose ();
				NumberOfCategoriesLabel = null;
			}

			if (ChooseButton != null) {
				ChooseButton.Dispose ();
				ChooseButton = null;
			}

			if (Slider != null) {
				Slider.Dispose ();
				Slider = null;
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

			if (SliderValueLabel != null) {
				SliderValueLabel.Dispose ();
				SliderValueLabel = null;
			}
		}
	}
}
