using SQLite;
using HalalGuide.Domain.Enums;
using Newtonsoft.Json;
using Xamarin.Media;

namespace HalalGuide.Domain
{
	public class LocationPicture : BaseEntity
	{
		[JsonProperty (PropertyName = "locationId")]
		public string locationId { get; set; }

		[JsonProperty (PropertyName = "imageUri")]
		public string imageUri { get; set; }

		[JsonProperty (PropertyName = "submitterId")]
		public string submitterId { get; set; }

		[JsonProperty (PropertyName = "creationStatus")]
		public CreationStatus creationStatus { get; set; }

		public LocationPicture ()
		{
		}
	}
}

