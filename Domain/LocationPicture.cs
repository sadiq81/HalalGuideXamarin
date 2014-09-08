using SimpleDBPersistence.SimpleDB.Model;
using SQLite;
using HalalGuide.Domain.Enum;

namespace HalalGuide.Domain
{
	[Table (TableIdentifier)] 
	[SimpleDBDomain (TableIdentifier)]
	public class LocationPicture : DBEntity
	{
		public const string TableIdentifier = "LocationPicture";

		public const string LocationIdIdentifier = "LocationId";

		[Column (LocationIdIdentifier)] 
		[SimpleDBFieldAttribute (LocationIdIdentifier)]
		public string LocationId { get; set; }

		public const string CreationStatusIdentifier = "CreationStatus";

		[Column (CreationStatusIdentifier)] 
		[SimpleDBFieldAttribute (CreationStatusIdentifier)]
		public CreationStatus CreationStatus { get; set; }

		public string Submitter { 
			get {
				if (Id != null) {
					return Id.Split (new []{ '-' }, 2) [0];
				} else {
					return "";
				}
			} 
		}

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

