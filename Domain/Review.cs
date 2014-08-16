using System;
using SimpleDBPersistence.SimpleDB.Model;
using SimpleDBPersistence.Domain;

namespace HalalGuide.Domain
{
	[SimpleDBDomain ("Review")]
	public class Review : Entity
	{
		[SimpleDBFieldAttribute ("LocationId")]
		public string LocationId { get; set; }

		[SimpleDBFieldAttribute ("Rating")]
		public int Rating { get; set; }

		[SimpleDBFieldAttribute ("Submitter")]
		public string Submitter { get; set; }

		public Review ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Review: LocationId={0}, Rating={1}, Submitter={2}]", LocationId, Rating, Submitter);
		}
		
		
	}
}

