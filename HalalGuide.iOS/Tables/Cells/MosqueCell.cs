// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HalalGuide.Domain;
using HalalGuide.Util;
using HalalGuide.iOS.Tables.Cells;

namespace HalalGuide.iOS
{
	public partial class MosqueCell : LocationCell
	{



		public MosqueCell (IntPtr handle) : base (handle)
		{
		}

		public override void ConfigureLocation (Location l)
		{
			base.ConfigureLocation (l);
			UIImageView firstImage = (UIImageView)ViewWithTag (LaguageImageTag);
			firstImage.Image = UIImage.FromBundle (Constants.Flag + l.Language.ToString ());

			UILabel firstLabel = (UILabel)ViewWithTag (LanguageLabelTag);
			firstLabel.Text = l.Language.ToString ().ToLower ().FirstToUpper ();

		}
	}
}
