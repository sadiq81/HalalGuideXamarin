using System;
using System.Xml.Serialization;

namespace HalalGuide.Services.RestDomain
{
	[XmlRoot]
	public class Postdistrikttype
	{
		[XmlElement (ElementName = "nr")]
		public string Nr { get; set; }

		[XmlElement (ElementName = "navn")]
		public string Navn { get; set; }

		[XmlElement (ElementName = "adresser")]
		public string Adresser { get; set; }

		public Postdistrikttype ()
		{
		}
	}
}

