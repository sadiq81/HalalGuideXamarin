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
	[Register ("AddNewDiningViewController")]
	partial class AddNewDiningViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView AlcoholImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch AlcoholSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CategoriesLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView CategoriesTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField City { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView DiningImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView HalalImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch HalalSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField HomePage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField Name { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton PickImageButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView PorkImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch PorkSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField PostalCode { get; set; }

		[Outlet]
		HalalGuide.iOS.AutoCompleteUITextField Road { get; set; }

		[Outlet]
		HalalGuide.iOS.AutoCompleteUITextField RoadNumber { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView SwitchView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField Telephone { get; set; }

		[Action ("AlcoholValueChanged:")]
		partial void AlcoholValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("ChooseCategories:")]
		partial void ChooseCategories (MonoTouch.UIKit.UIButton sender);

		[Action ("HalalValueChanged:")]
		partial void HalalValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("PickImage:")]
		partial void PickImage (MonoTouch.UIKit.UIButton sender);

		[Action ("PorkValueChanged:")]
		partial void PorkValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("Regreet:")]
		partial void Regreet (MonoTouch.UIKit.UIBarButtonItem sender);

		[Action ("ResetCategories:")]
		partial void ResetCategories (MonoTouch.UIKit.UIButton sender);

		[Action ("Save:")]
		partial void Save (MonoTouch.UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}

			if (AlcoholImage != null) {
				AlcoholImage.Dispose ();
				AlcoholImage = null;
			}

			if (AlcoholSwitch != null) {
				AlcoholSwitch.Dispose ();
				AlcoholSwitch = null;
			}

			if (CategoriesLabel != null) {
				CategoriesLabel.Dispose ();
				CategoriesLabel = null;
			}

			if (CategoriesTableView != null) {
				CategoriesTableView.Dispose ();
				CategoriesTableView = null;
			}

			if (City != null) {
				City.Dispose ();
				City = null;
			}

			if (DiningImage != null) {
				DiningImage.Dispose ();
				DiningImage = null;
			}

			if (HalalImage != null) {
				HalalImage.Dispose ();
				HalalImage = null;
			}

			if (HalalSwitch != null) {
				HalalSwitch.Dispose ();
				HalalSwitch = null;
			}

			if (HomePage != null) {
				HomePage.Dispose ();
				HomePage = null;
			}

			if (Name != null) {
				Name.Dispose ();
				Name = null;
			}

			if (PickImageButton != null) {
				PickImageButton.Dispose ();
				PickImageButton = null;
			}

			if (PorkImage != null) {
				PorkImage.Dispose ();
				PorkImage = null;
			}

			if (PorkSwitch != null) {
				PorkSwitch.Dispose ();
				PorkSwitch = null;
			}

			if (PostalCode != null) {
				PostalCode.Dispose ();
				PostalCode = null;
			}

			if (Road != null) {
				Road.Dispose ();
				Road = null;
			}

			if (RoadNumber != null) {
				RoadNumber.Dispose ();
				RoadNumber = null;
			}

			if (SwitchView != null) {
				SwitchView.Dispose ();
				SwitchView = null;
			}

			if (Telephone != null) {
				Telephone.Dispose ();
				Telephone = null;
			}
		}
	}
}
