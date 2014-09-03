using System;
using SimpleDBPersistence.Domain;
using SimpleDBPersistence.SimpleDB.Model;
using SQLite;

namespace HalalGuide.Domain
{
	[Table ("FacebookUser")] 
	[SimpleDBDomain ("FacebookUser")]
	public class FacebookUser :DBEntity
	{
		public const string NameIdentifier = "Name";

		[Column (NameIdentifier)] 
		[SimpleDBFieldAttribute ("Name")]
		public string Name { get; set; }

		public FacebookUser ()
		{
		}
	}
}

