using System;
using System.Collections.Generic;
using HalalGuide.Domain.Enums;
using Newtonsoft.Json;

namespace HalalGuide.Util.Converter
{
	public class DiningCategoryListJsonConverter :JsonConverter
	{
		public override bool CanConvert (Type objectType)
		{
			return objectType == typeof(List<DiningCategory>);
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var reviewsAsString = serializer.Deserialize<string> (reader);
			return reviewsAsString == null ? null : JsonConvert.DeserializeObject<List<DiningCategory>> (reviewsAsString);
		}

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			var reviewsAsString = JsonConvert.SerializeObject (value);
			serializer.Serialize (writer, reviewsAsString);
		}

	}
}

