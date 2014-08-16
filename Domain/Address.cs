using System;
using System.Collections.Generic;

namespace HalalGuide.Domain
{
	public class Address
	{

		public string StreetName{ get; set; }

		public List<string> StreetNumbers{ get; set; }

		public string PostalCode { get; set; }

		public Address (string streetName, string postalCode, string streetNumber)
		{
			this.StreetName = streetName;
			this.PostalCode = postalCode;
			this.StreetNumbers = new List<string> (){ streetNumber };
		}

		public override string ToString ()
		{
			return string.Format ("[Address: StreetName={0}, PostalCode={1}]", StreetName, PostalCode);
		}
		
		

	}
}

