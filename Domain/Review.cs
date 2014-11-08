using SQLite;
using HalalGuide.Domain.Enums;
using Newtonsoft.Json;

namespace HalalGuide.Domain
{
	public class Review : BaseEntity
	{
		public Review ()
		{
		}

		public Review (string locationId, int rating, string review, string submitterId, CreationStatus creationStatus)
		{
			this.locationId = locationId;
			this.rating = rating;
			this.review = review;
			this.submitterId = submitterId;
			this.creationStatus = creationStatus;
		}


		[JsonProperty (PropertyName = "locationId")]
		public string locationId { get; set; }

		[JsonProperty (PropertyName = "rating")]
		public int rating { get; set; }

		[JsonProperty (PropertyName = "review")]
		public string review{ get; set; }

		[JsonProperty (PropertyName = "submitterId")]
		public string submitterId { get; set; }

		[JsonProperty (PropertyName = "creationStatus")]
		public CreationStatus creationStatus { get; set; }
	}
}

