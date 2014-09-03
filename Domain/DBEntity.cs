using SimpleDBPersistence.Domain;
using SimpleDBPersistence.SimpleDB.Model;
using System;
using SQLite;

namespace HalalGuide.Domain
{
	public class DBEntity : Entity
	{
		[PrimaryKey]
		[SimpleDBIdAttribute]
		public override string Id { get; set; }

		[Column (CreatedIdentifier)] 
		[SimpleDBFieldAttribute (CreatedIdentifier)]
		public override DateTime Created{ get; set; }

		[Column (UpdatedIdentifier)] 
		[SimpleDBFieldAttribute (UpdatedIdentifier)]
		public override DateTime LastUpdated{ get; set; }
	}
}

