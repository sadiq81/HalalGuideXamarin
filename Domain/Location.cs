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
		[SimpleDBFieldAttribute ("Halal")]
		public bool Halal { get; set; }

		//Only for Dining
		[SimpleDBFieldAttribute ("Alcohol")]
		public bool Alcohol { get; set; }

		//Only for Dining
		[SimpleDBFieldAttribute ("Pork")]
		public bool Pork { get; set; }

		//Only for Mosque
		[SimpleDBFieldAttribute ("Language")]
		public Language Language { get; set; }

		public double Distance { get; set; }

		public Location ()
		{
		}

	}
}

