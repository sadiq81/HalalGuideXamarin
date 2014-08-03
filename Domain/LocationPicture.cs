using System;
using SimpleDBPersistence.SimpleDB.Model;

namespace HalalGuide.Domain
{
	[SimpleDBDomain ("Location")]
	public class LocationPicture
	{
		[SimpleDBFieldAttribute ("LocationId")]
		public string LocationId { get; set; }

		[SimpleDBFieldAttribute ("S3Path")]
		public string S3Path { get; set; }

		public LocationPicture ()
		{
		}

		public LocationPicture (string locationId, string s3Path)
		{
			this.LocationId = locationId;
			this.S3Path = s3Path;
		}
		
	}
}

