using System;
using MonoTouch.UIKit;
using HalalGuide.Domain;
using HalalGuide.Util;
using System.Threading.Tasks;
using HalalGuide.ViewModels;
using System.Globalization;
using HalalGuide.iOS.Util;
using HalalGuide.Services;
using SDWebImage;
using MonoTouch.Foundation;

namespace HalalGuide.iOS.Tables.Cells
{
	public class LocationCell: BaseTableViewCell,ILocationCell
	{
		//Common
		protected static readonly int CategoryImageTag = 101;
		protected static readonly int KmTextTag = 200;
		protected static readonly int NameLabelTag = 201;
		protected static readonly int AddressLabelTag = 202;
		protected static readonly int PostalCodeLabelTag = 203;

		//Dining
		protected static readonly int PigImageTag = 102;
		protected static readonly int AlcoholImageTag = 103;
		protected static readonly int HalalImageTag = 104;
		protected static readonly int PigLabelTag = 204;
		protected static readonly int AlcoholLabelTag = 205;
		protected static readonly int HalalLabelTag = 206;

		//Mosque
		protected static readonly int LaguageImageTag = 102;
		protected static readonly int LanguageLabelTag = 204;

		public SingleDiningViewModel ViewModel = ServiceContainer.Resolve<SingleDiningViewModel> ();

		public LocationCell (IntPtr handle) : base (handle)
		{
		}

		public virtual void ConfigureLocation (Location l)
		{
			UIImageView category = (UIImageView)ViewWithTag (CategoryImageTag);

			string uri = ViewModel.GetFirstImageUriForLocation (l);
			if (uri != null) {
				category.SetImage (
					url: new NSUrl (uri), 
					placeholder: UIImage.FromBundle (l.locationType.ToString ())
				);
			} else {
				category.Image = UIImage.FromBundle (l.locationType.ToString ());
			}

			UILabel name = (UILabel)ViewWithTag (NameLabelTag);
			name.Text = l.name;

			UILabel address1 = (UILabel)ViewWithTag (AddressLabelTag);
			address1.Text = l.addressRoad + " " + l.addressRoadNumber;
			UILabel address2 = (UILabel)ViewWithTag (PostalCodeLabelTag);
			address2.Text = l.addressPostalCode + " " + l.addressCity;

			UILabel km = (UILabel)ViewWithTag (KmTextTag);
			km.Text = 0.Equals (l.distance) ? "N/A" : l.distance.ToString (Constants.NumberFormat, CultureInfo.CurrentUICulture);
		}
	}
}

