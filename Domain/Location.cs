using SimpleDBPersistence.Domain;
using System.Collections.Generic;
using SimpleDBPersistence.SimpleDB.Model;
using System.Text;
using HalalGuide.Domain.Enum;
using System.Linq;

namespace HalalGuide.Domain
{
	[SimpleDBDomain ("Location")]
	public class Location : Entity
	{
		[SimpleDBFieldAttribute ("Name")]
		public string Name { get; set; }

		[SimpleDBFieldAttribute ("AddressRoad")]
		public string AddressRoad { get; set; }

		[SimpleDBFieldAttribute ("AddressRoadNumber")]
		public string AddressRoadNumber { get; set; }

		[SimpleDBFieldAttribute ("AddressPostalCode")]
		public string AddressPostalCode { get; set; }

		[SimpleDBFieldAttribute ("AddressCity")]
		public string AddressCity { get; set; }

		[SimpleDBFieldAttribute ("Latitude")]
		public string Latitude { get; set; }

		[SimpleDBFieldAttribute ("Longtitude")]
		public string Longtitude { get; set; }

		[SimpleDBFieldAttribute ("Telephone")]
		public string Telephone { get; set; }

		[SimpleDBFieldAttribute ("HomePage")]
		public string HomePage { get; set; }

		[SimpleDBFieldAttribute ("LocationType")]
		public LocationType LocationType  { get; set; }

		//Only for Dining
		[SimpleDBListAttribute ("DiningCategory")]
		public List<DiningCategory> Categories  { get; set; }

		//Only for Dining
		[SimpleDBFieldAttribute ("NonHalal")]
		public bool NonHalal { get; set; }

		//Only for Dining
		[SimpleDBFieldAttribute ("Alcohol")]
		public bool Alcohol { get; set; }

		//Only for Dining
		[SimpleDBFieldAttribute ("Pork")]
		public bool Pork { get; set; }

		//Only for Mosque
		[SimpleDBFieldAttribute ("Language")]
		public Language Language { get; set; }

		[SimpleDBFieldAttribute ("LocationStatus")]
		public LocationStatus LocationStatus { get; set; }

		[SimpleDBFieldAttribute ("Submitter")]
		public string Submitter { get; set; }

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

		public override string ToString ()
		{
			return string.Format ("[Location: Name={0}, AddressRoad={1}, AddressRoadNumber={2}, AddressPostalCode={3}, AddressCity={4}, Latitude={5}, Longtitude={6}, Telephone={7}, HomePage={8}, LocationType={9}, NonHalal={10}, Alcohol={11}, Pork={12}, Language={13}, LocationStatus={14}]", Name, AddressRoad, AddressRoadNumber, AddressPostalCode, AddressCity, Latitude, Longtitude, Telephone, HomePage, LocationType, NonHalal, Alcohol, Pork, Language, LocationStatus);
		}
		
		
	}
}

