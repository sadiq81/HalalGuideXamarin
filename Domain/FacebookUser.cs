using SQLite;
using Newtonsoft.Json;

namespace HalalGuide.Domain
{
	public class FacebookUser : BaseEntity
	{
		[JsonProperty(PropertyName = "name")]
		public string name { get; set; }

		[JsonProperty(PropertyName = "image")]
		public string image { get; set; }

		public FacebookUser ()
		{
		}
	}
}

