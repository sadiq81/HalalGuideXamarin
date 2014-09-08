// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace HalalGuide.iOS.Tables.Cells
{
	[Register ("ReviewCell")]
	partial class ReviewCell
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView ProfilePicture { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Review { get; set; }

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

		[Outlet]
		MonoTouch.UIKit.UILabel Submitter { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ProfilePicture != null) {
				ProfilePicture.Dispose ();
				ProfilePicture = null;
			}

			if (Review != null) {
				Review.Dispose ();
				Review = null;
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

			if (Submitter != null) {
				Submitter.Dispose ();
				Submitter = null;
			}
		}
	}
}
