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
	[Register ("AddNewDiningController")]
	partial class AddNewDiningController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch Alcohol { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView AlcoholImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField City { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView DiningImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch Halal { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView HalalImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField HomePage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField Name { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton PickImageButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch Pork { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView PorkImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField PostalCode { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField Road { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField RoadNumber { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField Telephone { get; set; }

		[Action ("AlcoholValueChanged:")]
		partial void AlcoholValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("EnteredPostalCode:")]
		partial void EnteredPostalCode (MonoTouch.UIKit.UITextField sender);

		[Action ("HalalValueChanged:")]
		partial void HalalValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("PickImage:")]
		partial void PickImage (MonoTouch.UIKit.UIButton sender);

		[Action ("PorkValueChanged:")]
		partial void PorkValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("Regreet:")]
		partial void Regreet (MonoTouch.Foundation.NSObject sender);

		[Action ("Save:")]
		partial void Save (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}

			if (Alcohol != null) {
				Alcohol.Dispose ();
				Alcohol = null;
			}

			if (AlcoholImage != null) {
				AlcoholImage.Dispose ();
				AlcoholImage = null;
			}

			if (City != null) {
				City.Dispose ();
				City = null;
			}

			if (DiningImage != null) {
				DiningImage.Dispose ();
				DiningImage = null;
			}

			if (Halal != null) {
				Halal.Dispose ();
				Halal = null;
			}

			if (HalalImage != null) {
				HalalImage.Dispose ();
				HalalImage = null;
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

			if (Pork != null) {
				Pork.Dispose ();
				Pork = null;
			}

			if (PorkImage != null) {
				PorkImage.Dispose ();
				PorkImage = null;
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

			if (Telephone != null) {
				Telephone.Dispose ();
				Telephone = null;
			}
		}
	}
}
