using SimpleDBPersistence.SimpleDB.Model;
using SimpleDBPersistence.Domain;
using MonoTouch.CoreImage;

namespace HalalGuide.Domain
{
	[SimpleDBDomain ("LocationPicture")]
	public class LocationPicture : Entity
	{
		public const string LocationIdIdentifier = "LocationId";

		[SimpleDBFieldAttribute (LocationIdIdentifier)]
		public string LocationId { get; set; }

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

