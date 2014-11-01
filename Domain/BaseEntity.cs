using System;
using Newtonsoft.Json;
using SQLite;

namespace HalalGuide.Domain
{
	public class BaseEntity
	{
		[PrimaryKey]
		public string id { get; set; }

		[JsonProperty (PropertyName = "__updatedAt")]
		public DateTime updatedAt { get; set; }

		[JsonProperty (PropertyName = "__createdAt")]
		public DateTime createdAt { get; set; }

		[JsonProperty (PropertyName = "__deleted")]
		public bool deleted { get; set; }

	}
}

