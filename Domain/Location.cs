using SimpleDBPersistence.Domain;
using System.Collections.Generic;
using SimpleDBPersistence.SimpleDB.Model;
using System.Text;
using HalalGuide.Domain.Enum;
using System.Linq;
using MonoTouch.CoreImage;

namespace HalalGuide.Domain
{
	[SimpleDBDomain ("Location")]
	public class Location : Entity
	{
		public const string NameIdentifier = "Name";

		[SimpleDBFieldAttribute ("Name")]
		public string Name { get; set; }

		public const string AddressRoadIdentifier = "AddressRoad";

		[SimpleDBFieldAttribute (AddressRoadIdentifier)]
		public string AddressRoad { get; set; }

		public const string AddressRoadNumberIdentifier = "AddressRoadNumber";

		[SimpleDBFieldAttribute (AddressRoadNumberIdentifier)]
		public string AddressRoadNumber { get; set; }

		public const string AddressPostalCodeIdentifier = "AddressPostalCode";

		[SimpleDBFieldAttribute (AddressPostalCodeIdentifier)]
		public string AddressPostalCode { get; set; }

		public const string AddressCityIdentifier = "AddressCity";

		[SimpleDBFieldAttribute (AddressCityIdentifier)]
		public string AddressCity { get; set; }

		public const string LatitudeIdentifier = "Latitude";

		[SimpleDBFieldAttribute (LatitudeIdentifier)]
		public string Latitude { get; set; }

		public const string LongtitudeIdentifier = "Longtitude";

		[SimpleDBFieldAttribute (LongtitudeIdentifier)]
		public string Longtitude { get; set; }

		public const string TelephoneIdentifier = "Telephone";

		[SimpleDBFieldAttribute (TelephoneIdentifier)]
		public string Telephone { get; set; }

		public const string HomePageIdentifier = "HomePage";

		[SimpleDBFieldAttribute (HomePageIdentifier)]
		public string HomePage { get; set; }

		public const string LocationTypeIdentifier = "LocationType";

		[SimpleDBFieldAttribute (LocationTypeIdentifier)]
		public LocationType LocationType  { get; set; }


		//Only for Dining
		public const string DiningCategoryIdentifier = "DiningCategory";

		[SimpleDBListAttribute (DiningCategoryIdentifier)]
		public List<DiningCategory> Categories  { get; set; }

		//Only for Dining
		public const string NonHalalIdentifier = "NonHalal";

		[SimpleDBFieldAttribute (NonHalalIdentifier)]
		public bool NonHalal { get; set; }

		//Only for Dining
		public const string AlcoholIdentifier = "Alcohol";

		[SimpleDBFieldAttribute (AlcoholIdentifier)]
		public bool Alcohol { get; set; }

		//Only for Dining
		public const string PorkIdentifier = "Pork";

		[SimpleDBFieldAttribute (PorkIdentifier)]
		public bool Pork { get; set; }

		//Only for Mosque
		public const string LanguageIdentifier = "Language";

		[SimpleDBFieldAttribute (LanguageIdentifier)]
		public Language Language { get; set; }

		public const string LocationStatusIdentifier = "LocationStatus";

		[SimpleDBFieldAttribute (LocationStatusIdentifier)]
		public LocationStatus LocationStatus { get; set; }

		public string Submitter { 
			get {
				if (Id != null) {
					return Id.Split (new []{ '-' }, 2) [0];
				} else {
					return"";
				}
			} 
		}

		public double Distance { get; set; }

		public Location ()
		{
		}

		public Location (string name, string addressRoad, string addressRoadNumber, string addressPostalCode, string addressCity, string latitude, string longtitude, string telephone, string homePage, LocationType locationType, List<DiningCategory> categories, bool nonHalal, bool alcohol, bool pork, Language language, LocationStatus locationStatus)
		{
			this.Name = name;
			this.AddressRoad = addressRoad;
			this.AddressRoadNumber = addressRoadNumber;
			this.AddressPostalCode = addressPostalCode;
			this.AddressCity = addressCity;
			this.Latitude = latitude;
			this.Longtitude = longtitude;
			this.Telephone = telephone;
			this.HomePage = homePage;
			this.LocationType = locationType;
			this.Categories = categories;
			this.NonHalal = nonHalal;
			this.Alcohol = alcohol;
			this.Pork = pork;
			this.Language = language;
			this.LocationStatus = locationStatus;
		}

		public string GetCategoriesAsString ()
		{
			if (Categories == null || Categories.Count == 0) {
				return "";
			}

			StringBuilder sb = new StringBuilder ();
			foreach (DiningCategory cat in Categories) {
				sb.Append (string.Format ("{0},", cat.Title));
			}
			sb.Remove (sb.Length - 1, 1);
			return sb.ToString ();
		}

		public override string ToString ()
		{
			return string.Format ("[Location: Name={0}, AddressRoad={1}, AddressRoadNumber={2}, AddressPostalCode={3}, AddressCity={4}, Latitude={5}, Longtitude={6}, Telephone={7}, HomePage={8}, LocationType={9}, NonHalal={10}, Alcohol={11}, Pork={12}, Language={13}, LocationStatus={14}]", Name, AddressRoad, AddressRoadNumber, AddressPostalCode, AddressCity, Latitude, Longtitude, Telephone, HomePage, LocationType, NonHalal, Alcohol, Pork, Language, LocationStatus);
		}
		
		
	}
}

