using SimpleDBPersistence.SimpleDB.Model;
using HalalGuide.Domain.Enum;
using SQLite;

namespace HalalGuide.Domain
{
	[Table (TableIdentifier)] 
	[SimpleDBDomain (TableIdentifier)]
	public class Location : DBEntity
	{

		public const string TableIdentifier = "Location";

		public const string NameIdentifier = "Name";

		[Column (NameIdentifier)] 
		[SimpleDBFieldAttribute (NameIdentifier)]
		public string Name { get; set; }

		public const string AddressRoadIdentifier = "AddressRoad";

		[Column (AddressRoadIdentifier)] 
		[SimpleDBFieldAttribute (AddressRoadIdentifier)]
		public string AddressRoad { get; set; }

		public const string AddressRoadNumberIdentifier = "AddressRoadNumber";

		[Column (AddressRoadNumberIdentifier)] 
		[SimpleDBFieldAttribute (AddressRoadNumberIdentifier)]
		public string AddressRoadNumber { get; set; }

		public const string AddressPostalCodeIdentifier = "AddressPostalCode";

		[Column (AddressPostalCodeIdentifier)] 
		[SimpleDBFieldAttribute (AddressPostalCodeIdentifier)]
		public string AddressPostalCode { get; set; }

		public const string AddressCityIdentifier = "AddressCity";

		[Column (AddressCityIdentifier)] 
		[SimpleDBFieldAttribute (AddressCityIdentifier)]
		public string AddressCity { get; set; }

		public const string LatitudeIdentifier = "Latitude";

		[Column (LatitudeIdentifier)] 
		[SimpleDBFieldAttribute (LatitudeIdentifier)]
		public double Latitude { get; set; }

		public const string LongtitudeIdentifier = "Longtitude";

		[Column (LongtitudeIdentifier)] 
		[SimpleDBFieldAttribute (LongtitudeIdentifier)]
		public double Longtitude { get; set; }

		public const string TelephoneIdentifier = "Telephone";

		[Column (TelephoneIdentifier)] 
		[SimpleDBFieldAttribute (TelephoneIdentifier)]
		public string Telephone { get; set; }

		public const string HomePageIdentifier = "HomePage";

		[Column (HomePageIdentifier)] 
		[SimpleDBFieldAttribute (HomePageIdentifier)]
		public string HomePage { get; set; }

		public const string LocationTypeIdentifier = "LocationType";

		[Column (LocationTypeIdentifier)] 
		[SimpleDBFieldAttribute (LocationTypeIdentifier)]
		public LocationType LocationType  { get; set; }

		//Only for Dining
		public const string DiningCategoryIdentifier = "DiningCategory";

		[Column (DiningCategoryIdentifier)] 
		[SimpleDBFieldAttribute (DiningCategoryIdentifier)]
		public string Categories  { get; set; }

		//Only for Dining
		public const string NonHalalIdentifier = "NonHalal";

		[Column (NonHalalIdentifier)] 
		[SimpleDBFieldAttribute (NonHalalIdentifier)]
		public bool NonHalal { get; set; }

		//Only for Dining
		public const string AlcoholIdentifier = "Alcohol";

		[Column (AlcoholIdentifier)] 
		[SimpleDBFieldAttribute (AlcoholIdentifier)]
		public bool Alcohol { get; set; }

		//Only for Dining
		public const string PorkIdentifier = "Pork";

		[Column (PorkIdentifier)] 
		[SimpleDBFieldAttribute (PorkIdentifier)]
		public bool Pork { get; set; }

		//Only for Mosque
		public const string LanguageIdentifier = "Language";

		[Column (LanguageIdentifier)] 
		[SimpleDBFieldAttribute (LanguageIdentifier)]
		public Language Language { get; set; }

		public const string CreationStatusIdentifier = "CreationStatus";

		[Column (CreationStatusIdentifier)] 
		[SimpleDBFieldAttribute (CreationStatusIdentifier)]
		public CreationStatus CreationStatus { get; set; }

		[Ignore]
		public string Submitter { 
			get {
				if (Id != null) {
					return Id.Split (new []{ '-' }, 2) [0];
				} else {
					return"";
				}
			} 
		}

		[Ignore]
		public double Distance { get; set; }

		public Location ()
		{
		}

		public Location (string name, string addressRoad, string addressRoadNumber, string addressPostalCode, string addressCity, double latitude, double longtitude, string telephone, string homePage, LocationType locationType, string categories, bool nonHalal, bool alcohol, bool pork, Language language, CreationStatus creationStatus)
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
			this.CreationStatus = creationStatus;
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Location))
				return false;
			Location other = (Location)obj;
			return Id == other.Id;
		}


		public override int GetHashCode ()
		{
			unchecked {
				return (Name != null ? Name.GetHashCode () : 0) ^ (AddressRoad != null ? AddressRoad.GetHashCode () : 0);
			}
		}


		public override string ToString ()
		{
			return string.Format ("[Location: Name={0}, AddressRoad={1}, AddressRoadNumber={2}, AddressPostalCode={3}, AddressCity={4}, Latitude={5}, Longtitude={6}, Telephone={7}, HomePage={8}, LocationType={9}, NonHalal={10}, Alcohol={11}, Pork={12}, Language={13}, LocationStatus={14}]", Name, AddressRoad, AddressRoadNumber, AddressPostalCode, AddressCity, Latitude, Longtitude, Telephone, HomePage, LocationType, NonHalal, Alcohol, Pork, Language, CreationStatus);
		}
		
		
	}
}

