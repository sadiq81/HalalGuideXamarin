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
	[Register ("SingleDiningViewController")]
	partial class SingleDiningViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView AlcoholImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel AlcoholLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CategoryLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CityLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DistanceLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView HalalImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel HalalLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UICollectionView PictureCollectionView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView PorkImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel PorkLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel RoadLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView Star1Image { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView Star2Image { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView Star3Image { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView Star4Image { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView Star5Image { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PictureCollectionView != null) {
				PictureCollectionView.Dispose ();
				PictureCollectionView = null;
			}

			if (Star1Image != null) {
				Star1Image.Dispose ();
				Star1Image = null;
			}

			if (Star2Image != null) {
				Star2Image.Dispose ();
				Star2Image = null;
			}

			if (Star3Image != null) {
				Star3Image.Dispose ();
				Star3Image = null;
			}

			if (Star4Image != null) {
				Star4Image.Dispose ();
				Star4Image = null;
			}

			if (Star5Image != null) {
				Star5Image.Dispose ();
				Star5Image = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (RoadLabel != null) {
				RoadLabel.Dispose ();
				RoadLabel = null;
			}

			if (CityLabel != null) {
				CityLabel.Dispose ();
				CityLabel = null;
			}

			if (CategoryLabel != null) {
				CategoryLabel.Dispose ();
				CategoryLabel = null;
			}

			if (PorkImage != null) {
				PorkImage.Dispose ();
				PorkImage = null;
			}

			if (AlcoholImage != null) {
				AlcoholImage.Dispose ();
				AlcoholImage = null;
			}

			if (HalalImage != null) {
				HalalImage.Dispose ();
				HalalImage = null;
			}

			if (PorkLabel != null) {
				PorkLabel.Dispose ();
				PorkLabel = null;
			}

			if (AlcoholLabel != null) {
				AlcoholLabel.Dispose ();
				AlcoholLabel = null;
			}

			if (HalalLabel != null) {
				HalalLabel.Dispose ();
				HalalLabel = null;
			}

			if (DistanceLabel != null) {
				DistanceLabel.Dispose ();
				DistanceLabel = null;
			}
		}
	}
}
