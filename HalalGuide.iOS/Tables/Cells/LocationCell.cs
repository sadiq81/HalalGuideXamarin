using System;
using MonoTouch.UIKit;
using HalalGuide.Domain;
using HalalGuide.Util;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using HalalGuide.ViewModels;
using SimpleDBPersistence.Service;
using System.Globalization;

namespace HalalGuide.iOS.Tables.Cells
{
	public class LocationCell: UITableViewCell,ILocationCell
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
			category.Image = UIImage.FromBundle (l.LocationType.ToString ());

			Task.Factory.StartNew (() => 
				ViewModel.GetFirstImageForLocation (l).
				ContinueWith (t => {
				if (t.Result != null) {
					InvokeOnMainThread (delegate {
						category.Image = UIImage.LoadFromData (NSData.FromStream (t.Result));
					});
				}
			}));

			UILabel name = (UILabel)ViewWithTag (NameLabelTag);
			name.Text = l.Name;

			UILabel address1 = (UILabel)ViewWithTag (AddressLabelTag);
			address1.Text = l.AddressRoad + " " + l.AddressRoadNumber;
			UILabel address2 = (UILabel)ViewWithTag (PostalCodeLabelTag);
			address2.Text = l.AddressPostalCode + " " + l.AddressCity;

			UILabel km = (UILabel)ViewWithTag (KmTextTag);
			km.Text = 0.Equals (l.Distance) ? "N/A" : l.Distance.ToString (Constants.NumberFormat, CultureInfo.CurrentUICulture);
		}
	}
}

