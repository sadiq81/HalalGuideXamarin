// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AddressBook;
using MonoTouch.CoreImage;
using HalalGuide.Domain;
using HalalGuide.Util;
using HalalGuide.iOS.Tables.Cells;

namespace HalalGuide.iOS
{
	public partial class DiningCell : LocationCell
	{


		public DiningCell (IntPtr handle) : base (handle)
		{
		}

		public override void ConfigureLocation (Location l)
		{
			base.ConfigureLocation (l);

			UIImageView firstImage = (UIImageView)ViewWithTag (PigImageTag);
			firstImage.Image = UIImage.FromBundle (Images.Pig + l.Pork);

			UIImageView secondImage = (UIImageView)ViewWithTag (AlcoholImageTag);
			secondImage.Image = UIImage.FromBundle (Images.Alcohol + l.Alcohol);

			UIImageView thirdImage = (UIImageView)ViewWithTag (HalalImageTag);
			thirdImage.Image = UIImage.FromBundle (Images.NonHalal + l.NonHalal);

			UILabel firstLabel = (UILabel)ViewWithTag (PigLabelTag);
			firstLabel.TextColor = l.Pork ? UIColor.Red : UIColor.Green;

			UILabel secondLabel = (UILabel)ViewWithTag (AlcoholLabelTag);
			secondLabel.TextColor = l.Alcohol ? UIColor.Red : UIColor.Green;

			UILabel thirdLabel = (UILabel)ViewWithTag (HalalLabelTag);
			thirdLabel.TextColor = l.NonHalal ? UIColor.Red : UIColor.Green;


		}
	}
}