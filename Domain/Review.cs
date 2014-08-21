using System;
using SimpleDBPersistence.SimpleDB.Model;
using SimpleDBPersistence.Domain;

namespace HalalGuide.Domain
{
	[SimpleDBDomain ("Review")]
	public class Review : Entity
	{

		public const string LocationIdIdentifier = "LocationId";

		[SimpleDBFieldAttribute (LocationIdIdentifier)]
		public string LocationId { get; set; }

		public const string RatingIdIdentifier = "Rating";

		[SimpleDBFieldAttribute (RatingIdIdentifier)]
		public int Rating { get; set; }

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

