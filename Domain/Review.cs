using SQLite;
using HalalGuide.Domain.Enums;
using Newtonsoft.Json;

namespace HalalGuide.Domain
{
	public class Review : BaseEntity
	{
		[JsonProperty(PropertyName = "locationId")]
		public string locationId { get; set; }

		[JsonProperty(PropertyName = "rating")]
		public int rating { get; set; }

		[JsonProperty(PropertyName = "review")]
		public string review{ get; set; }

		[JsonProperty(PropertyName = "submitterId")]
		public string submitterId { get; set; }

		[JsonProperty(PropertyName = "creationStatus")]
		public CreationStatus creationStatus { get; set; }
	}
}

