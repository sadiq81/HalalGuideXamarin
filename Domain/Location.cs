using HalalGuide.Domain.Enums;
using SQLite;
using Newtonsoft.Json;
using System.Collections.Generic;
using MonoTouch.AddressBook;
using Newtonsoft.Json.Converters;
using HalalGuide.Util.Converter;

namespace HalalGuide.Domain
{
	public class Location : BaseEntity
	{
		[JsonProperty (PropertyName = "name")]
		public string name { get; set; }

		[JsonProperty (PropertyName = "addressRoad")]
		public string addressRoad { get; set; }

		[JsonProperty (PropertyName = "addressRoadNumber")]
		public string addressRoadNumber { get; set; }

		[JsonProperty (PropertyName = "addressPostalCode")]
		public string addressPostalCode { get; set; }

		[JsonProperty (PropertyName = "addressCity")]
		public string addressCity { get; set; }

		[JsonProperty (PropertyName = "latitude")]
		public double latitude { get; set; }

		[JsonProperty (PropertyName = "longtitude")]
		public double longtitude { get; set; }

		[JsonProperty (PropertyName = "telephone")]
		public string telephone { get; set; }

		[JsonProperty (PropertyName = "homePage")]
		public string homePage { get; set; }


		[JsonProperty (PropertyName = "locationType")]
		public LocationType locationType  { get; set; }

		[JsonIgnoreAttribute]
		[IgnoreAttribute]
		public List<DiningCategory> categories  { get; set; }

		[JsonProperty (PropertyName = "categories")]
		public string categoriesForDatabase {
			get { 
				return JsonConvert.SerializeObject (categories);
			}
			set {
				categories = JsonConvert.DeserializeObject<List<DiningCategory>> (value);
			}
		}

		[JsonProperty (PropertyName = "nonHalal")]
		public bool nonHalal { get; set; }

		[JsonProperty (PropertyName = "alcohol")]
		public bool alcohol { get; set; }

		[JsonProperty (PropertyName = "pork")]
		public bool pork { get; set; }

		[JsonProperty (PropertyName = "language")]
		public Language language { get; set; }

		[JsonProperty (PropertyName = "creationStatus")]
		public CreationStatus creationStatus { get; set; }

		[JsonProperty (PropertyName = "submitterId")]
		public string submitterId { get; set; }

		[IgnoreAttribute]
		[JsonIgnoreAttribute]
		public double distance { get; set; }

		public Location ()
		{
		}

		public Location (string name, string addressRoad, string addressRoadNumber, string addressPostalCode, string addressCity, double latitude, double longtitude, string telephone, string homePage, LocationType locationType, List<DiningCategory> categories, bool nonHalal, bool alcohol, bool pork, Language language, CreationStatus creationStatus)
		{
			this.name = name;
			this.addressRoad = addressRoad;
			this.addressRoadNumber = addressRoadNumber;
			this.addressPostalCode = addressPostalCode;
			this.addressCity = addressCity;
			this.latitude = latitude;
			this.longtitude = longtitude;
			this.telephone = telephone;
			this.homePage = homePage;
			this.locationType = locationType;
			this.categories = categories;
			this.nonHalal = nonHalal;
			this.alcohol = alcohol;
			this.pork = pork;
			this.language = language;
			this.creationStatus = creationStatus;
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
			return id == other.id;
		}


		public override int GetHashCode ()
		{
			unchecked {
				return (name != null ? name.GetHashCode () : 0) ^ (addressRoad != null ? addressRoad.GetHashCode () : 0);
			}
		}

	}
}

