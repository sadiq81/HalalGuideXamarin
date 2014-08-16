using SimpleDBPersistence.SimpleDB.Model;
using SimpleDBPersistence.Domain;

namespace HalalGuide.Domain
{
	[SimpleDBDomain ("LocationPicture")]
	public class LocationPicture : Entity
	{
		[SimpleDBFieldAttribute ("LocationId")]
		public string LocationId { get; set; }

		[SimpleDBFieldAttribute ("Submitter")]
		public string Submitter { get; set; }

		public LocationPicture ()
		{
		}

		public LocationPicture (string locationId)
		{
			this.LocationId = locationId;
		}

		public override string ToString ()
		{
			return string.Format ("[LocationPicture: LocationId={0}]", LocationId);
		}
		
		
	}
}

