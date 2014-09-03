using SimpleDBPersistence.SimpleDB.Model;
using HalalGuide.Domain.Enum;
using SQLite;

namespace HalalGuide.Domain
{
	[Table ("Review")] 
	[SimpleDBDomain ("Review")]
	public class Review : DBEntity
	{
		public const string LocationIdIdentifier = "LocationId";

		[Column (LocationIdIdentifier)] 
		[SimpleDBFieldAttribute (LocationIdIdentifier)]
		public string LocationId { get; set; }

		public const string RatingIdIdentifier = "Rating";

		[Column (RatingIdIdentifier)] 
		[SimpleDBFieldAttribute (RatingIdIdentifier)]
		public int Rating { get; set; }

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

		public Review ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Review: LocationId={0}, Rating={1}, Submitter={2}]", LocationId, Rating, Submitter);
		}
		
		
	}
}

